#region

using System;
using System.Collections.Generic;
using System.Reflection;
using KoiVM.Runtime.Dynamic;
using KoiVM.Runtime.Execution;
using KoiVM.Runtime.Execution.Internal;

#endregion

namespace KoiVM.Runtime.VCalls
{
    internal class Ldftn : IVCall
    {
        public byte Code => DarksVMConstants.VCALL_LDFTN;

        public void Load(DarksVMContext ctx, out ExecutionState state)
        {
            uint sp = ctx.Registers[DarksVMConstants.REG_SP].U4;
            DarksVMSlot methodSlot = ctx.Stack[sp--];
            DarksVMSlot objectSlot = ctx.Stack[sp];

            if(objectSlot.O != null)
            {
                var method = (MethodInfo) ctx.Instance.Data.LookupReference(methodSlot.U4);
                Type type = objectSlot.O.GetType();

                var baseTypes = new List<Type>();
                do
                {
                    baseTypes.Add(type);
                    type = type.BaseType;
                } while(type != null && type != method.DeclaringType);
                baseTypes.Reverse();

                MethodInfo found = method;
                const BindingFlags fl = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
                foreach(Type baseType in baseTypes)
                foreach(MethodInfo m in baseType.GetMethods(fl))
                    if(m.GetBaseDefinition() == found)
                    {
                        found = m;
                        break;
                    }

                ctx.Stack[sp] = new DarksVMSlot {U8 = (ulong) found.MethodHandle.GetFunctionPointer()};
            }
            if(objectSlot.U8 != 0)
            {
                // intra linking
                uint entryKey = ctx.Stack[--sp].U4;
                ulong codeAdr = methodSlot.U8;
                Data.DarksVMFuncSig sig = ctx.Instance.Data.LookupExport(objectSlot.U4).Signature;
                IntPtr ptr = DarksVMTrampoline.CreateTrampoline(ctx.Instance.Data.Module, codeAdr, entryKey, sig, objectSlot.U4);
                ctx.Stack[sp] = new DarksVMSlot {U8 = (ulong) ptr};
            }
            else
            {
                var method = (MethodBase) ctx.Instance.Data.LookupReference(methodSlot.U4);
                ctx.Stack[sp] = new DarksVMSlot {U8 = (ulong) method.MethodHandle.GetFunctionPointer()};
            }

            ctx.Stack.SetTopPosition(sp);
            ctx.Registers[DarksVMConstants.REG_SP].U4 = sp;
            state = ExecutionState.Next;
        }
    }
}