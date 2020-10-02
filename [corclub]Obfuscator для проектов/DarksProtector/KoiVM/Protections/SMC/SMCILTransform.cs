#region

using System.Linq;
using KoiVM.AST.IL;
using KoiVM.VMIL;

#endregion

namespace KoiVM.Protections.SMC
{
    internal class SMCILTransform : ITransform
    {
        private int adrKey;
        private SMCBlock newTrampoline;
        private ILBlock trampoline;

        public void Initialize(ILTransformer tr)
        {
            this.trampoline = null;
            tr.RootScope.ProcessBasicBlocks<ILInstrList>(b =>
            {
                if(b.Content.Any(instr => instr.IR != null && instr.IR.Annotation == SMCBlock.AddressPart2))
                    this.trampoline = (ILBlock) b;
            });
            if(this.trampoline == null)
                return;

            CFG.ScopeBlock scope = tr.RootScope.SearchBlock(this.trampoline).Last();
            this.newTrampoline = new SMCBlock(this.trampoline.Id, this.trampoline.Content);
            scope.Content[scope.Content.IndexOf(this.trampoline)] = this.newTrampoline;

            this.adrKey = tr.VM.Random.Next();
            this.newTrampoline.Key = (byte) tr.VM.Random.Next();
        }

        public void Transform(ILTransformer tr)
        {
            if(tr.Block.Targets.Contains(this.trampoline))
                tr.Block.Targets[tr.Block.Targets.IndexOf(this.trampoline)] = this.newTrampoline;

            if(tr.Block.Sources.Contains(this.trampoline))
                tr.Block.Sources[tr.Block.Sources.IndexOf(this.trampoline)] = this.newTrampoline;

            tr.Instructions.VisitInstrs(this.VisitInstr, tr);
        }

        private void VisitInstr(ILInstrList instrs, ILInstruction instr, ref int index, ILTransformer tr)
        {
            if(instr.Operand is ILBlockTarget)
            {
                var target = (ILBlockTarget) instr.Operand;
                if(target.Target == this.trampoline)
                    target.Target = this.newTrampoline;
            }
            else if(instr.IR == null)
            {
                return;
            }

            if (instr.IR.Annotation != SMCBlock.CounterInit || instr.OpCode != ILOpCode.PUSHI_DWORD)
            {
                if (instr.IR.Annotation != SMCBlock.EncryptionKey || instr.OpCode != ILOpCode.PUSHI_DWORD)
                {
                    if (instr.IR.Annotation == SMCBlock.AddressPart1 && instr.OpCode == ILOpCode.PUSHI_DWORD &&
                                           instr.Operand is ILBlockTarget)
                    {
                        var target = (ILBlockTarget)instr.Operand;

                        var relBase = new ILInstruction(ILOpCode.PUSHR_QWORD, ILRegister.IP, instr);
                        instr.OpCode = ILOpCode.PUSHI_DWORD;
                        instr.Operand = new SMCBlockRef(target, relBase, (uint)this.adrKey);

                        instrs.Replace(index, new[]
                        {
                    relBase,
                    instr,
                    new ILInstruction(ILOpCode.ADD_QWORD, null, instr)
                });
                    }
                    else if (instr.IR.Annotation == SMCBlock.AddressPart2 && instr.OpCode == ILOpCode.PUSHI_DWORD)
                    {
                        var imm = (ILImmediate)instr.Operand;
                        if ((int)imm.Value == 0x0f000003) imm.Value = this.adrKey;
                    }
                }
                else
                {
                    var imm = (ILImmediate)instr.Operand;
                    if ((int)imm.Value == 0x0f000002) imm.Value = (int)this.newTrampoline.Key;
                }
            }
            else
            {
                var imm = (ILImmediate)instr.Operand;
                if ((int)imm.Value == 0x0f000001) this.newTrampoline.CounterOperand = imm;
            }
        }
    }
}