﻿#region

using System.Diagnostics;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using KoiVM.AST.ILAST;
using KoiVM.AST.IR;

#endregion

namespace KoiVM.VMIR.Translation
{
    public class LdfldHandler : ITranslationHandler
    {
        public Code ILCode => Code.Ldfld;

        public IIROperand Translate(ILASTExpression expr, IRTranslator tr)
        {
            Debug.Assert(expr.Arguments.Length == 1);
            IIROperand obj = tr.Translate(expr.Arguments[0]);

            IRVariable retVar = tr.Context.AllocateVRegister(expr.Type.Value);
            int fieldId = (int) tr.VM.Data.GetId((IField) expr.Operand);
            int ecallId = tr.VM.Runtime.VMCall.LDFLD;
            tr.Instructions.Add(new IRInstruction(IROpCode.PUSH, obj));
            tr.Instructions.Add(new IRInstruction(IROpCode.VCALL, IRConstant.FromI4(ecallId), IRConstant.FromI4(fieldId)));
            tr.Instructions.Add(new IRInstruction(IROpCode.POP, retVar));
            return retVar;
        }
    }

    public class StfldHandler : ITranslationHandler
    {
        public Code ILCode => Code.Stfld;

        public IIROperand Translate(ILASTExpression expr, IRTranslator tr)
        {
            Debug.Assert(expr.Arguments.Length == 2);
            IIROperand obj = tr.Translate(expr.Arguments[0]);
            IIROperand val = tr.Translate(expr.Arguments[1]);

            int fieldId = (int) tr.VM.Data.GetId((IField) expr.Operand);
            int ecallId = tr.VM.Runtime.VMCall.STFLD;
            tr.Instructions.Add(new IRInstruction(IROpCode.PUSH, obj));
            tr.Instructions.Add(new IRInstruction(IROpCode.PUSH, val));
            tr.Instructions.Add(new IRInstruction(IROpCode.VCALL, IRConstant.FromI4(ecallId), IRConstant.FromI4(fieldId)));
            return null;
        }
    }

    public class LdsfldHandler : ITranslationHandler
    {
        public Code ILCode => Code.Ldsfld;

        public IIROperand Translate(ILASTExpression expr, IRTranslator tr)
        {
            IRVariable retVar = tr.Context.AllocateVRegister(expr.Type.Value);
            int fieldId = (int) tr.VM.Data.GetId((IField) expr.Operand);
            int ecallId = tr.VM.Runtime.VMCall.LDFLD;
            tr.Instructions.Add(new IRInstruction(IROpCode.PUSH, IRConstant.Null()));
            tr.Instructions.Add(new IRInstruction(IROpCode.VCALL, IRConstant.FromI4(ecallId), IRConstant.FromI4(fieldId)));
            tr.Instructions.Add(new IRInstruction(IROpCode.POP, retVar));
            return retVar;
        }
    }

    public class StsfldHandler : ITranslationHandler
    {
        public Code ILCode => Code.Stsfld;

        public IIROperand Translate(ILASTExpression expr, IRTranslator tr)
        {
            Debug.Assert(expr.Arguments.Length == 1);
            IIROperand val = tr.Translate(expr.Arguments[0]);

            int fieldId = (int) tr.VM.Data.GetId((IField) expr.Operand);
            int ecallId = tr.VM.Runtime.VMCall.STFLD;
            tr.Instructions.Add(new IRInstruction(IROpCode.PUSH, IRConstant.Null()));
            tr.Instructions.Add(new IRInstruction(IROpCode.PUSH, val));
            tr.Instructions.Add(new IRInstruction(IROpCode.VCALL, IRConstant.FromI4(ecallId), IRConstant.FromI4(fieldId)));
            return null;
        }
    }

    public class LdfldaHandler : ITranslationHandler
    {
        public Code ILCode => Code.Ldflda;

        public IIROperand Translate(ILASTExpression expr, IRTranslator tr)
        {
            Debug.Assert(expr.Arguments.Length == 1);
            IIROperand obj = tr.Translate(expr.Arguments[0]);

            IRVariable retVar = tr.Context.AllocateVRegister(expr.Type.Value);
            int fieldId = (int) (tr.VM.Data.GetId((IField) expr.Operand) | 0x80000000);
            int ecallId = tr.VM.Runtime.VMCall.LDFLD;
            tr.Instructions.Add(new IRInstruction(IROpCode.PUSH, obj));
            tr.Instructions.Add(new IRInstruction(IROpCode.VCALL, IRConstant.FromI4(ecallId), IRConstant.FromI4(fieldId)));
            tr.Instructions.Add(new IRInstruction(IROpCode.POP, retVar));
            return retVar;
        }
    }

    public class LdsfldaHandler : ITranslationHandler
    {
        public Code ILCode => Code.Ldsflda;

        public IIROperand Translate(ILASTExpression expr, IRTranslator tr)
        {
            IRVariable retVar = tr.Context.AllocateVRegister(expr.Type.Value);
            int fieldId = (int) (tr.VM.Data.GetId((IField) expr.Operand) | 0x80000000);
            int ecallId = tr.VM.Runtime.VMCall.LDFLD;
            tr.Instructions.Add(new IRInstruction(IROpCode.PUSH, IRConstant.Null()));
            tr.Instructions.Add(new IRInstruction(IROpCode.VCALL, IRConstant.FromI4(ecallId), IRConstant.FromI4(fieldId)));
            tr.Instructions.Add(new IRInstruction(IROpCode.POP, retVar));
            return retVar;
        }
    }
}