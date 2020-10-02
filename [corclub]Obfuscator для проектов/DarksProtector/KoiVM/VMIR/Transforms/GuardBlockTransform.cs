#region

using KoiVM.AST.IR;
using KoiVM.CFG;

#endregion

namespace KoiVM.VMIR.Transforms
{
    public class GuardBlockTransform : ITransform
    {
        private BasicBlock<IRInstrList> epilog;
        private BasicBlock<IRInstrList> prolog;

        public void Initialize(IRTransformer tr)
        {
            int maxId = 0;
            BasicBlock<IRInstrList> entry = null;
            tr.RootScope.ProcessBasicBlocks<IRInstrList>(block =>
            {
                block.Id++;
                if(block.Id > maxId)
                    maxId = block.Id;
                if(entry == null)
                    entry = block;
            });

            this.prolog = new BasicBlock<IRInstrList>(0, new IRInstrList
            {
                new IRInstruction(IROpCode.__ENTRY),
                new IRInstruction(IROpCode.JMP, new IRBlockTarget(entry))
            });
            this.prolog.Targets.Add(entry);
            entry.Sources.Add(this.prolog);

            this.epilog = new BasicBlock<IRInstrList>(maxId + 1, new IRInstrList
            {
                new IRInstruction(IROpCode.__EXIT)
            });
            this.InsertProlog(tr.RootScope);
            this.InsertEpilog(tr.RootScope);
        }

        public void Transform(IRTransformer tr) => tr.Instructions.VisitInstrs(this.VisitInstr, tr);

        private void InsertProlog(ScopeBlock block)
        {
            if(block.Children.Count > 0)
                if(block.Children[0].Type == ScopeType.None)
                {
                    this.InsertProlog(block.Children[0]);
                }
                else
                {
                    var prologScope = new ScopeBlock();
                    prologScope.Content.Add(this.prolog);
                    block.Children.Insert(0, prologScope);
                }
            else block.Content.Insert(0, this.prolog);
        }

        private void InsertEpilog(ScopeBlock block)
        {
            if(block.Children.Count > 0)
                if(block.Children[block.Children.Count - 1].Type == ScopeType.None)
                {
                    this.InsertEpilog(block.Children[block.Children.Count - 1]);
                }
                else
                {
                    var epilogScope = new ScopeBlock();
                    epilogScope.Content.Add(this.epilog);
                    block.Children.Insert(block.Children.Count, epilogScope);
                }
            else block.Content.Insert(block.Content.Count, this.epilog);
        }

        private void VisitInstr(IRInstrList instrs, IRInstruction instr, ref int index, IRTransformer tr)
        {
            if(instr.OpCode == IROpCode.RET)
            {
                instrs.Replace(index, new[]
                {
                    new IRInstruction(IROpCode.JMP, new IRBlockTarget(this.epilog))
                });
                if(!tr.Block.Targets.Contains(this.epilog))
                {
                    tr.Block.Targets.Add(this.epilog);
                    this.epilog.Sources.Add(tr.Block);
                }
            }
        }
    }
}