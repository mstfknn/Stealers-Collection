#region

using System.Collections.Generic;
using KoiVM.AST.IL;
using KoiVM.VM;

#endregion

namespace KoiVM.VMIL.Transforms
{
    public class FixMethodRefTransform : IPostTransform
    {
        private HashSet<DarksVMRegisters> saveRegs;

        public void Initialize(ILPostTransformer tr) => this.saveRegs = tr.Runtime.Descriptor.Data.LookupInfo(tr.Method).UsedRegister;

        public void Transform(ILPostTransformer tr) => tr.Instructions.VisitInstrs(this.VisitInstr, tr);

        private void VisitInstr(ILInstrList instrs, ILInstruction instr, ref int index, ILPostTransformer tr)
        {
            if (!(instr.Operand is ILRelReference rel))
                return;

            if (!(rel.Target is ILMethodTarget methodRef))
                return;

            methodRef.Resolve(tr.Runtime);
        }
    }
}