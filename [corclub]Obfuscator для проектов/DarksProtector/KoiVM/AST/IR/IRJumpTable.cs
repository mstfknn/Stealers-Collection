#region

using KoiVM.CFG;

#endregion

namespace KoiVM.AST.IR
{
    public class IRJumpTable : IIROperand
    {
        public IRJumpTable(IBasicBlock[] targets) => this.Targets = targets;

        public IBasicBlock[] Targets
        {
            get;
            set;
        }

        public ASTType Type => ASTType.Ptr;

        public override string ToString() => string.Format("[..{0}..]", this.Targets.Length);
    }
}