#region

using System;
using KoiVM.Runtime.Dynamic;
using KoiVM.Runtime.Execution;

#endregion

namespace KoiVM.Runtime.VCalls
{
    internal class Cast : IVCall
    {
        public byte Code => DarksVMConstants.VCALL_CAST;

        public void Load(DarksVMContext ctx, out ExecutionState state)
        {
            uint sp = ctx.Registers[DarksVMConstants.REG_SP].U4;
            DarksVMSlot typeSlot = ctx.Stack[sp--];
            DarksVMSlot valSlot = ctx.Stack[sp];

            var castType = (Type) ctx.Instance.Data.LookupReference(typeSlot.U4 & ~0x80000000);
            if (Type.GetTypeCode(castType) != TypeCode.String || valSlot.O != null)
            {
                if (valSlot.O != null)
                {
                    if (!castType.IsInstanceOfType(valSlot.O))
                    {
                        valSlot.O = null;
                        if ((typeSlot.U4 & 0x80000000) != 0)
                            throw new InvalidCastException();
                    }
                }
                else
                {
                    valSlot.O = null;
                }
            }
            else
            {
                valSlot.O = ctx.Instance.Data.LookupString(valSlot.U4);
            }
            ctx.Stack[sp] = valSlot;

            ctx.Stack.SetTopPosition(sp);
            ctx.Registers[DarksVMConstants.REG_SP].U4 = sp;
            state = ExecutionState.Next;
        }
    }
}