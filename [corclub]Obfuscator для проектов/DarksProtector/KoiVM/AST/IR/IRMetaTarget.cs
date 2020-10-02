namespace KoiVM.AST.IR
{
    public class IRMetaTarget : IIROperand
    {
        public IRMetaTarget(object mdItem) => this.MetadataItem = mdItem;

        public object MetadataItem
        {
            get;
            set;
        }

        public bool LateResolve
        {
            get;
            set;
        }

        public ASTType Type => ASTType.Ptr;

        public override string ToString() => this.MetadataItem.ToString();
    }
}