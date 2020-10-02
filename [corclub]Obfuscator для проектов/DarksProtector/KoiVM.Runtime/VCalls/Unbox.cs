#region

using System;
using KoiVM.Runtime.Dynamic;
using KoiVM.Runtime.Execution;
using KoiVM.Runtime.Execution.Internal;

#endregion

namespace KoiVM.Runtime.VCalls
{
    internal class Unbox : IVCall
    {
        public byte Code => DarksVMConstants.VCALL_UNBOX;

        public void Load(DarksVMContext ctx, out ExecutionState state)
        {
            uint sp = ctx.Registers[DarksVMConstants.REG_SP].U4;
            DarksVMSlot typeSlot = ctx.Stack[sp--];
            DarksVMSlot valSlot = ctx.Stack[sp];

            bool unboxPtr = (typeSlot.U4 & 0x80000000) != 0;
            var valType = (Type) ctx.Instance.Data.LookupReference(typeSlot.U4 & ~0x80000000);
            if (!unboxPtr)
            {
                if (valType == typeof(object) && valSlot.O != null)
                    valType = valSlot.O.GetType();
                valSlot = DarksVMSlot.FromObject(valSlot.O, valType);
                ctx.Stack[sp] = valSlot;
            }
            else
            {
                unsafe
                {
                    TypedReference typedRef;
                    TypedReferenceHelpers.UnboxTypedRef(valSlot.O, &typedRef);
                    var reference = new TypedRef(typedRef);
                    valSlot = DarksVMSlot.FromObject(valSlot.O, valType);
                    ctx.Stack[sp] = valSlot;
                }
            }

            ctx.Stack.SetTopPosition(sp);
            ctx.Registers[DarksVMConstants.REG_SP].U4 = sp;
            state = ExecutionState.Next;
        }
    }
}