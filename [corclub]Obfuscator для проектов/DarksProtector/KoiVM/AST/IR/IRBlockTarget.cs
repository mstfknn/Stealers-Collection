#region

using KoiVM.CFG;

#endregion

namespace KoiVM.AST.IR
{
    public class IRBlockTarget : IIROperand
    {
        public IRBlockTarget(IBasicBlock target) => this.Target = target;

        public IBasicBlock Target
        {
            get;
            set;
        }

        public ASTType Type => ASTType.Ptr;

        public override string ToString() => $"Block_{this.Target.Id:x2}";
    }
}