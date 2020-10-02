#region

using System;
using System.Reflection;
using KoiVM.Runtime.Dynamic;
using KoiVM.Runtime.Execution;
using KoiVM.Runtime.Execution.Internal;

#endregion

namespace KoiVM.Runtime.VCalls
{
    internal class Token : IVCall
    {
        public byte Code => DarksVMConstants.VCALL_TOKEN;

        public void Load(DarksVMContext ctx, out ExecutionState state)
        {
            uint sp = ctx.Registers[DarksVMConstants.REG_SP].U4;
            DarksVMSlot typeSlot = ctx.Stack[sp];

            MemberInfo reference = ctx.Instance.Data.LookupReference(typeSlot.U4);
            switch (reference)
            {
                case Type _:
                    typeSlot.O = ValueTypeBox.Box(((Type)reference).TypeHandle, typeof(RuntimeTypeHandle));
                    break;
                case MethodBase _:
                    typeSlot.O = ValueTypeBox.Box(((MethodBase)reference).MethodHandle, typeof(RuntimeMethodHandle));
                    break;
                case FieldInfo _:
                    typeSlot.O = ValueTypeBox.Box(((FieldInfo)reference).FieldHandle, typeof(RuntimeFieldHandle));
                    break;
            }
            ctx.Stack[sp] = typeSlot;

            state = ExecutionState.Next;
        }
    }
}