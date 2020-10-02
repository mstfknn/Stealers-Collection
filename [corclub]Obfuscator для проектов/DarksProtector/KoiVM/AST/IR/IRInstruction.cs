#region

using System.Text;
using KoiVM.AST.ILAST;
using KoiVM.VMIR;

#endregion

namespace KoiVM.AST.IR
{
    public class IRInstruction : ASTNode
    {
        public IRInstruction(IROpCode opCode) => this.OpCode = opCode;

        public IRInstruction(IROpCode opCode, IIROperand op1)
        {
            this.OpCode = opCode;
            this.Operand1 = op1;
        }

        public IRInstruction(IROpCode opCode, IIROperand op1, IIROperand op2)
        {
            this.OpCode = opCode;
            this.Operand1 = op1;
            this.Operand2 = op2;
        }

        public IRInstruction(IROpCode opCode, IIROperand op1, IIROperand op2, object annotation)
        {
            this.OpCode = opCode;
            this.Operand1 = op1;
            this.Operand2 = op2;
            this.Annotation = annotation;
        }

        public IRInstruction(IROpCode opCode, IIROperand op1, IIROperand op2, IRInstruction origin)
        {
            this.OpCode = opCode;
            this.Operand1 = op1;
            this.Operand2 = op2;
            this.Annotation = origin.Annotation;
            this.ILAST = origin.ILAST;
        }

        public IROpCode OpCode
        {
            get;
            set;
        }

        public IILASTStatement ILAST
        {
            get;
            set;
        }

        public IIROperand Operand1
        {
            get;
            set;
        }

        public IIROperand Operand2
        {
            get;
            set;
        }

        public object Annotation
        {
            get;
            set;
        }

        public override string ToString()
        {
            var ret = new StringBuilder();
            ret.AppendFormat("{0}", this.OpCode.ToString().PadLeft(16));
            if(this.Operand1 != null)
            {
                ret.AppendFormat(" {0}", this.Operand1);
                if(this.Operand2 != null)
                    ret.AppendFormat(", {0}", this.Operand2);
            }
            if(this.Annotation != null)
                ret.AppendFormat("    ; {0}", this.Annotation);
            return ret.ToString();
        }
    }
}