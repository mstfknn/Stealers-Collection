#region

using dnlib.DotNet;
using dnlib.DotNet.Emit;
using KoiVM.AST.ILAST;
using KoiVM.AST.IR;

#endregion

namespace KoiVM.VMIR.Translation
{
    public class LdcI4Handler : ITranslationHandler
    {
        public Code ILCode => Code.Ldc_I4;

        public IIROperand Translate(ILASTExpression expr, IRTranslator tr) => IRConstant.FromI4((int)expr.Operand);
    }

    public class LdcI8Handler : ITranslationHandler
    {
        public Code ILCode => Code.Ldc_I8;

        public IIROperand Translate(ILASTExpression expr, IRTranslator tr) => IRConstant.FromI8((long)expr.Operand);
    }

    public class LdcR4Handler : ITranslationHandler
    {
        public Code ILCode => Code.Ldc_R4;

        public IIROperand Translate(ILASTExpression expr, IRTranslator tr) => IRConstant.FromR4((float)expr.Operand);
    }

    public class LdcR8Handler : ITranslationHandler
    {
        public Code ILCode => Code.Ldc_R8;

        public IIROperand Translate(ILASTExpression expr, IRTranslator tr) => IRConstant.FromR8((double)expr.Operand);
    }

    public class LdnullHandler : ITranslationHandler
    {
        public Code ILCode => Code.Ldnull;

        public IIROperand Translate(ILASTExpression expr, IRTranslator tr) => IRConstant.Null();
    }

    public class LdstrHandler : ITranslationHandler
    {
        public Code ILCode => Code.Ldstr;

        public IIROperand Translate(ILASTExpression expr, IRTranslator tr) => IRConstant.FromString((string)expr.Operand);
    }

    public class LdtokenHandler : ITranslationHandler
    {
        public Code ILCode => Code.Ldtoken;

        public IIROperand Translate(ILASTExpression expr, IRTranslator tr)
        {
            IRVariable retVar = tr.Context.AllocateVRegister(expr.Type.Value);
            int refId = (int) tr.VM.Data.GetId((IMemberRef) expr.Operand);
            int ecallId = tr.VM.Runtime.VMCall.TOKEN;
            tr.Instructions.Add(new IRInstruction(IROpCode.VCALL, IRConstant.FromI4(ecallId), IRConstant.FromI4(refId)));
            tr.Instructions.Add(new IRInstruction(IROpCode.POP, retVar));

            return retVar;
        }
    }
}