#region

using System.Linq;
using System.Text;
using dnlib.DotNet.Emit;
using KoiVM.CFG;

#endregion

namespace KoiVM.AST.ILAST
{
    public class ILASTExpression : ASTExpression, IILASTNode, IILASTStatement
    {
        public Code ILCode
        {
            get;
            set;
        }

        public Instruction CILInstr
        {
            get;
            set;
        }

        public object Operand
        {
            get;
            set;
        }

        public IILASTNode[] Arguments
        {
            get;
            set;
        }

        public Instruction[] Prefixes
        {
            get;
            set;
        }

        public override string ToString()
        {
            var ret = new StringBuilder();
            ret.AppendFormat("{0}{1}(", this.ILCode.ToOpCode().Name, this.Type == null ? "" : ":" + this.Type.Value);
            if(this.Operand != null)
            {
                if(this.Operand is string)
                {
                    ASTConstant.EscapeString(ret, (string)this.Operand, true);
                }
                else if(this.Operand is IBasicBlock)
                {
                    ret.AppendFormat("Block_{0:x2}", ((IBasicBlock)this.Operand).Id);
                }
                else if(this.Operand is IBasicBlock[])
                {
                    System.Collections.Generic.IEnumerable<string> targets = ((IBasicBlock[])this.Operand).Select(block => string.Format("Block_{0:x2}", block.Id));
                    ret.AppendFormat("[{0}]", string.Join(", ", targets));
                }
                else
                {
                    ret.Append(this.Operand);
                }
                if(this.Arguments.Length > 0)
                    ret.Append(";");
            }
            for(int i = 0; i < this.Arguments.Length; i++)
            {
                if(i != 0)
                    ret.Append(",");
                ret.Append(this.Arguments[i]);
            }
            ret.Append(")");
            return ret.ToString();
        }

        public ILASTExpression Clone() => (ILASTExpression)this.MemberwiseClone();
    }
}