#region

using KoiVM.AST.IR;

#endregion

namespace KoiVM.VMIR.Transforms
{
    public class LogicTransform : ITransform
    {
        public void Initialize(IRTransformer tr)
        {
        }

        public void Transform(IRTransformer tr) => tr.Instructions.VisitInstrs(this.VisitInstr, tr);

        private void VisitInstr(IRInstrList instrs, IRInstruction instr, ref int index, IRTransformer tr)
        {
            switch (instr.OpCode)
            {
                case IROpCode.__NOT:
                    instrs.Replace(index, new[]
 {
                    new IRInstruction(IROpCode.NOR, instr.Operand1, instr.Operand1, instr)
                });
                    break;
                case IROpCode.__AND:
                    {
                        IRVariable tmp = tr.Context.AllocateVRegister(instr.Operand2.Type);
                        instrs.Replace(index, new[]
                        {
                    new IRInstruction(IROpCode.MOV, tmp, instr.Operand2, instr),
                    new IRInstruction(IROpCode.NOR, instr.Operand1, instr.Operand1, instr),
                    new IRInstruction(IROpCode.NOR, tmp, tmp, instr),
                    new IRInstruction(IROpCode.NOR, instr.Operand1, tmp, instr)
                });
                        break;
                    }

                case IROpCode.__OR:
                    instrs.Replace(index, new[]
  {
                    new IRInstruction(IROpCode.NOR, instr.Operand1, instr.Operand2, instr),
                    new IRInstruction(IROpCode.NOR, instr.Operand1, instr.Operand1, instr)
                });
                    break;
                case IROpCode.__XOR:
                    {
                        IRVariable tmp1 = tr.Context.AllocateVRegister(instr.Operand2.Type);
                        IRVariable tmp2 = tr.Context.AllocateVRegister(instr.Operand2.Type);
                        instrs.Replace(index, new[]
                        {
                    new IRInstruction(IROpCode.MOV, tmp1, instr.Operand1, instr),
                    new IRInstruction(IROpCode.NOR, tmp1, instr.Operand2, instr),
                    new IRInstruction(IROpCode.MOV, tmp2, instr.Operand2, instr),
                    new IRInstruction(IROpCode.NOR, instr.Operand1, instr.Operand1, instr),
                    new IRInstruction(IROpCode.NOR, tmp2, tmp2, instr),
                    new IRInstruction(IROpCode.NOR, instr.Operand1, tmp2, instr),
                    new IRInstruction(IROpCode.NOR, instr.Operand1, tmp1, instr)
                });
                        break;
                    }
            }
        }
    }
}