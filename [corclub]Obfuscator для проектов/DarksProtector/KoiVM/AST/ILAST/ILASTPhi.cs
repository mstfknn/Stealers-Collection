#region

using System.Text;

#endregion

namespace KoiVM.AST.ILAST
{
    public class ILASTPhi : ASTNode, IILASTStatement
    {
        public ILASTVariable Variable
        {
            get;
            set;
        }

        public ILASTVariable[] SourceVariables
        {
            get;
            set;
        }

        public override string ToString()
        {
            var ret = new StringBuilder();
            ret.AppendFormat("{0} = [", this.Variable);
            for(int i = 0; i < this.SourceVariables.Length; i++)
            {
                if(i != 0)
                    ret.Append(", ");
                ret.Append(this.SourceVariables[i]);
            }
            ret.Append("]");
            return ret.ToString();
        }
    }
}