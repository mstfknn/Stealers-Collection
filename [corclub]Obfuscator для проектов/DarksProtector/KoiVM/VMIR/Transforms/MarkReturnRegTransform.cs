#region

using KoiVM.AST;
using KoiVM.AST.IR;

#endregion

namespace KoiVM.VMIR.Transforms
{
    public class MarkReturnRegTransform : ITransform
    {
        public void Initialize(IRTransformer tr)
        {
        }

        public void Transform(IRTransformer tr) => tr.Instructions.VisitInstrs(this.VisitInstr, tr);

        private void VisitInstr(IRInstrList instrs, IRInstruction instr, ref int index, IRTransformer tr)
        {
            if (!(instr.Annotation is InstrCallInfo callInfo) || callInfo.ReturnValue == null)
                return;

            if (instr.Operand1 is IRRegister && ((IRRegister) instr.Operand1).SourceVariable == callInfo.ReturnValue) callInfo.ReturnRegister = (IRRegister) instr.Operand1;
            else if(instr.Operand1 is IRPointer && ((IRPointer) instr.Operand1).SourceVariable == callInfo.ReturnValue) callInfo.ReturnSlot = (IRPointer) instr.Operand1;
        }
    }
}