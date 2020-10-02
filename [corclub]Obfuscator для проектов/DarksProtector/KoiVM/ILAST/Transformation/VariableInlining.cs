#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using dnlib.DotNet.Emit;
using KoiVM.AST.ILAST;

#endregion

namespace KoiVM.ILAST.Transformation
{
    public class VariableInlining : ITransformationHandler
    {
        public void Initialize(ILASTTransformer tr)
        {
        }

        public void Transform(ILASTTransformer tr)
        {
            var varUsage = new Dictionary<ILASTVariable, int>();

            for(int i = 0; i < tr.Tree.Count; i++)
            {
                IILASTStatement st = tr.Tree[i];
                ILASTExpression expr = GetExpression(st);
                if(expr == null)
                    continue;

                if(st is ILASTExpression && expr.ILCode == Code.Nop)
                {
                    tr.Tree.RemoveAt(i);
                    i--;
                    continue;
                }
                if (st is ILASTAssignment assignment)
                {
                    if (Array.IndexOf(tr.Tree.StackRemains, assignment.Variable) != -1)
                        continue;
                    Debug.Assert(assignment.Variable.VariableType == ILASTVariableType.StackVar);
                }

                foreach (IILASTNode arg in expr.Arguments)
                {
                    Debug.Assert(arg is ILASTVariable);
                    var argVar = (ILASTVariable) arg;
                    if(argVar.VariableType == ILASTVariableType.StackVar)
                        varUsage.Increment(argVar);
                }
            }

            // If a variable is remained on stack, it cannot be inlined since it would be pushed on the stack.
            foreach(ILASTVariable remain in tr.Tree.StackRemains)
                varUsage.Remove(remain);

            var simpleVars = new HashSet<ILASTVariable>(varUsage.Where(usage => usage.Value == 1).Select(pair => pair.Key));
            bool modified;
            do
            {
                modified = false;

                for(int i = 0; i < tr.Tree.Count - 1; i++)
                {
                    if (!(tr.Tree[i] is ILASTAssignment assignment))
                        continue;

                    if (!simpleVars.Contains(assignment.Variable))
                        continue;

                    ILASTExpression expr = GetExpression(tr.Tree[i + 1]);
                    if(expr == null || expr.ILCode.ToOpCode().Name.StartsWith("stelem"))
                        continue;

                    for(int argIndex = 0; argIndex < expr.Arguments.Length; argIndex++)
                    {
                        // If previous arguments are not variables (ie. expression),
                        // there might be side-effect inlining succeeding arguments.
                        if (!(expr.Arguments[argIndex] is ILASTVariable argVar))
                            break;

                        if (argVar == assignment.Variable)
                        {
                            expr.Arguments[argIndex] = assignment.Value;
                            tr.Tree.RemoveAt(i);
                            i--;
                            modified = true;
                            break;
                        }
                    }

                    // Ensure the block is processed sequentially.
                    if(modified)
                        break;
                }
            } while(modified);
        }

        public static ILASTExpression GetExpression(IILASTStatement node)
        {
            if (node is ILASTExpression expr)
            {
                if (expr.ILCode == Code.Pop && expr.Arguments[0] is ILASTExpression)
                    expr = (ILASTExpression)expr.Arguments[0];
                return expr;
            }
            return node is ILASTAssignment ? ((ILASTAssignment) node).Value : null;
        }
    }
}