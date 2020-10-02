#region

using System;
using KoiVM.Runtime.Dynamic;
using KoiVM.Runtime.Execution;

#endregion

namespace KoiVM.Runtime.VCalls
{
    internal class Ckfinite : IVCall
    {
        public byte Code => DarksVMConstants.VCALL_CKFINITE;

        public void Load(DarksVMContext ctx, out ExecutionState state)
        {
            uint sp = ctx.Registers[DarksVMConstants.REG_SP].U4;
            DarksVMSlot valueSlot = ctx.Stack[sp--];

            switch (ctx.Registers[DarksVMConstants.REG_FL].U1 & DarksVMConstants.FL_UNSIGNED)
            {
                case 0:
                    {
                        double v = valueSlot.R8;
                        if (double.IsNaN(v) || double.IsInfinity(v))
                            throw new ArithmeticException();
                        break;
                    }

                default:
                    {
                        float v = valueSlot.R4;
                        if (float.IsNaN(v) || float.IsInfinity(v))
                            throw new ArithmeticException();
                        break;
                    }
            }

            ctx.Stack.SetTopPosition(sp);
            ctx.Registers[DarksVMConstants.REG_SP].U4 = sp;
            state = ExecutionState.Next;
        }
    }
}