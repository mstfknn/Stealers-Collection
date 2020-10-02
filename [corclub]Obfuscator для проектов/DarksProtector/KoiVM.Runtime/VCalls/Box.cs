#region

using System;
using System.Diagnostics;
using KoiVM.Runtime.Dynamic;
using KoiVM.Runtime.Execution;

#endregion

namespace KoiVM.Runtime.VCalls
{
    internal class Box : IVCall
    {
        public byte Code => DarksVMConstants.VCALL_BOX;

        public void Load(DarksVMContext ctx, out ExecutionState state)
        {
            uint sp = ctx.Registers[DarksVMConstants.REG_SP].U4;
            DarksVMSlot typeSlot = ctx.Stack[sp--];
            DarksVMSlot valSlot = ctx.Stack[sp];

            var valType = (Type) ctx.Instance.Data.LookupReference(typeSlot.U4);
            switch (Type.GetTypeCode(valType))
            {
                case TypeCode.String when valSlot.O == null:
                    valSlot.O = ctx.Instance.Data.LookupString(valSlot.U4);
                    break;
                default:
                    Debug.Assert(valType.IsValueType);
                    valSlot.O = valSlot.ToObject(valType);
                    break;
            }
            ctx.Stack[sp] = valSlot;

            ctx.Stack.SetTopPosition(sp);
            ctx.Registers[DarksVMConstants.REG_SP].U4 = sp;
            state = ExecutionState.Next;
        }
    }
}