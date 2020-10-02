namespace KoiVM.AST.IR
{
    public class IRPointer : IIROperand
    {
        public IRRegister Register
        {
            get;
            set;
        }

        public int Offset
        {
            get;
            set;
        }

        public IRVariable SourceVariable
        {
            get;
            set;
        }

        public ASTType Type
        {
            get;
            set;
        }

        public override string ToString()
        {
            string prefix = this.Type.ToString();
            string offsetStr = "";
            if (this.Offset <= 0)
            {
                if (this.Offset < 0)
                    offsetStr = $" - {-this.Offset:x}h";
            }
            else offsetStr = $" + {this.Offset:x}h";
            return $"{prefix}:[{this.Register}{offsetStr}]";
        }
    }
}