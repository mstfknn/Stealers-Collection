#region

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace KoiVM.AST.ILAST
{
    public class ILASTTree : List<IILASTStatement>
    {
        public ILASTVariable[] StackRemains
        {
            get;
            set;
        }

        public override string ToString()
        {
            var ret = new StringBuilder();
            foreach(IILASTStatement st in this)
                ret.AppendLine(st.ToString());
            ret.AppendLine();
            ret.Append("[");
            for(int i = 0; i < this.StackRemains.Length; i++)
            {
                if(i != 0)
                    ret.Append(", ");
                ret.Append(this.StackRemains[i]);
            }
            ret.AppendLine("]");
            return ret.ToString();
        }

        public void TraverseTree<T>(Action<ILASTExpression, T> visitFunc, T state)
        {
            foreach(IILASTStatement st in this)
                if(st is ILASTExpression)
                    this.TraverseTreeInternal((ILASTExpression) st, visitFunc, state);
                else if(st is ILASTAssignment)
                    this.TraverseTreeInternal(((ILASTAssignment) st).Value, visitFunc, state);
        }

        private void TraverseTreeInternal<T>(ILASTExpression expr, Action<ILASTExpression, T> visitFunc, T state)
        {
            foreach(IILASTNode arg in expr.Arguments)
                if(arg is ILASTExpression)
                    this.TraverseTreeInternal((ILASTExpression) arg, visitFunc, state);
            visitFunc(expr, state);
        }
    }
}