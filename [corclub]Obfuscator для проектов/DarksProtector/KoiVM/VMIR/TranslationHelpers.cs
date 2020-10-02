#region

using System.Diagnostics;
using KoiVM.AST;
using KoiVM.AST.IR;

#endregion

namespace KoiVM.VMIR
{
    public static class TranslationHelpers
    {
        public static void EmitCompareEq(IRTranslator tr, ASTType type, IIROperand a, IIROperand b)
        {
            switch (type)
            {
                case ASTType.O:
                case ASTType.ByRef:
                case ASTType.R4:
                case ASTType.R8:
                    tr.Instructions.Add(new IRInstruction(IROpCode.CMP, a, b));
                    break;
                default:
                    // I4/I8/Ptr
                    Debug.Assert(type == ASTType.I4 || type == ASTType.I8 || type == ASTType.Ptr);
                    tr.Instructions.Add(new IRInstruction(IROpCode.CMP, a, b));
                    break;
            }
        }
    }
}