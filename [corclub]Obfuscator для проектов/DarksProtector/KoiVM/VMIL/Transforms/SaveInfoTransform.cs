#region

using KoiVM.AST.IL;
using KoiVM.VM;

#endregion

namespace KoiVM.VMIL.Transforms
{
    public class SaveInfoTransform : ITransform
    {
        private DarksVMMethodInfo methodInfo;

        public void Initialize(ILTransformer tr)
        {
            this.methodInfo = tr.VM.Data.LookupInfo(tr.Method);
            this.methodInfo.RootScope = tr.RootScope;
            tr.VM.Data.SetInfo(tr.Method, this.methodInfo);
        }

        public void Transform(ILTransformer tr) => tr.Instructions.VisitInstrs(this.VisitInstr, tr);

        private void VisitInstr(ILInstrList instrs, ILInstruction instr, ref int index, ILTransformer tr)
        {
            if(instr.Operand is ILRegister)
            {
                DarksVMRegisters reg = ((ILRegister) instr.Operand).Register;
                if(reg.IsGPR())
                    this.methodInfo.UsedRegister.Add(reg);
            }
        }
    }
}