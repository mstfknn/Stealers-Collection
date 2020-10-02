#region

using KoiVM.Runtime.Dynamic;
using KoiVM.Runtime.Execution;

#endregion

namespace KoiVM.Runtime.VCalls
{
    internal class Throw : IVCall
    {
        public byte Code => DarksVMConstants.VCALL_THROW;

        public void Load(DarksVMContext ctx, out ExecutionState state)
        {
            uint sp = ctx.Registers[DarksVMConstants.REG_SP].U4;
            uint type = ctx.Stack[sp--].U4;
            ctx.Registers[DarksVMConstants.REG_SP].U4 = sp;
            state = type == 1 ? ExecutionState.Rethrow : ExecutionState.Throw;
        }
    }
}