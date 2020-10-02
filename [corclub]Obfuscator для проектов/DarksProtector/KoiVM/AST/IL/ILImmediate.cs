namespace KoiVM.AST.IL
{
    public class ILImmediate : ASTConstant, IILOperand
    {
        public static ILImmediate Create(object value, ASTType type) => new ILImmediate
        {
            Value = value,
            Type = type
        };
    }
}