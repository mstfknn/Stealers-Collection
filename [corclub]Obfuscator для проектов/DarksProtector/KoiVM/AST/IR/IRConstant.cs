namespace KoiVM.AST.IR
{
    public class IRConstant : ASTConstant, IIROperand
    {
        ASTType IIROperand.Type => Type.Value;

        public static IRConstant FromI4(int value) => new IRConstant
        {
            Value = value,
            Type = ASTType.I4
        };

        public static IRConstant FromI8(long value) => new IRConstant
        {
            Value = value,
            Type = ASTType.I8
        };

        public static IRConstant FromR4(float value) => new IRConstant
        {
            Value = value,
            Type = ASTType.R4
        };

        public static IRConstant FromR8(double value) => new IRConstant
        {
            Value = value,
            Type = ASTType.R8
        };

        public static IRConstant FromString(string value) => new IRConstant
        {
            Value = value,
            Type = ASTType.O
        };

        public static IRConstant Null() => new IRConstant
        {
            Value = null,
            Type = ASTType.O
        };
    }
}