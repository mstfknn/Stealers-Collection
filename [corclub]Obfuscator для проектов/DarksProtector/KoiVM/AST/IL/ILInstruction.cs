#region

using System.Text;
using KoiVM.AST.IR;
using KoiVM.VMIL;

#endregion

namespace KoiVM.AST.IL
{
    public class ILInstruction : ASTNode, IHasOffset
    {
        public ILInstruction(ILOpCode opCode) => this.OpCode = opCode;

        public ILInstruction(ILOpCode opCode, IILOperand operand)
        {
            this.OpCode = opCode;
            this.Operand = operand;
        }

        public ILInstruction(ILOpCode opCode, IILOperand operand, object annotation)
        {
            this.OpCode = opCode;
            this.Operand = operand;
            this.Annotation = annotation;
        }

        public ILInstruction(ILOpCode opCode, IILOperand operand, ILInstruction origin)
        {
            this.OpCode = opCode;
            this.Operand = operand;
            this.Annotation = origin.Annotation;
            this.IR = origin.IR;
        }

        public IRInstruction IR
        {
            get;
            set;
        }

        public ILOpCode OpCode
        {
            get;
            set;
        }

        public IILOperand Operand
        {
            get;
            set;
        }

        public object Annotation
        {
            get;
            set;
        }

        public uint Offset
        {
            get;
            set;
        }

        public override string ToString()
        {
            var ret = new StringBuilder();
            ret.AppendFormat("{0}", this.OpCode.ToString().PadLeft(16));
            if(this.Operand != null) ret.AppendFormat(" {0}", this.Operand);
            if(this.Annotation != null)
                ret.AppendFormat("    ; {0}", this.Annotation);
            return ret.ToString();
        }
    }
}