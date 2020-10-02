#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using dnlib.DotNet.Emit;
using KoiVM.AST;
using KoiVM.AST.IR;
using KoiVM.CFG;

#endregion

namespace KoiVM.VMIR.Transforms
{
    public class EHTransform : ITransform
    {
        private ScopeBlock[] thisScopes;

        public void Initialize(IRTransformer tr)
        {
        }

        public void Transform(IRTransformer tr)
        {
            this.thisScopes = tr.RootScope.SearchBlock(tr.Block);
            this.AddTryStart(tr);
            if(this.thisScopes[this.thisScopes.Length - 1].Type == ScopeType.Handler)
            {
                ScopeBlock tryScope = this.SearchForTry(tr.RootScope, this.thisScopes[this.thisScopes.Length - 1].ExceptionHandler);
                ScopeBlock[] scopes = tr.RootScope.SearchBlock(tryScope.GetBasicBlocks().First());
                this.thisScopes = scopes.TakeWhile(s => s != tryScope).ToArray();
            }
            tr.Instructions.VisitInstrs(this.VisitInstr, tr);
        }

        private void SearchForHandlers(ScopeBlock scope, ExceptionHandler eh, ref IBasicBlock handler, ref IBasicBlock filter)
        {
            if(scope.ExceptionHandler == eh)
                if(scope.Type == ScopeType.Handler)
                    handler = scope.GetBasicBlocks().First();
                else if(scope.Type == ScopeType.Filter)
                    filter = scope.GetBasicBlocks().First();
            foreach(ScopeBlock child in scope.Children)
                this.SearchForHandlers(child, eh, ref handler, ref filter);
        }

        private void AddTryStart(IRTransformer tr)
        {
            var tryStartInstrs = new List<IRInstruction>();
            for(int i = 0; i < this.thisScopes.Length; i++)
            {
                ScopeBlock scope = this.thisScopes[i];
                if(scope.Type != ScopeType.Try)
                    continue;
                if(scope.GetBasicBlocks().First() != tr.Block)
                    continue;

                // Search for handler/filter
                IBasicBlock handler = null, filter = null;
                this.SearchForHandlers(tr.RootScope, scope.ExceptionHandler, ref handler, ref filter);
                Debug.Assert(handler != null &&
                             (scope.ExceptionHandler.HandlerType != ExceptionHandlerType.Filter || filter != null));

                // Add instructions
                tryStartInstrs.Add(new IRInstruction(IROpCode.PUSH, new IRBlockTarget(handler)));

                IIROperand tryOperand = null;
                int ehType;
                if(scope.ExceptionHandler.HandlerType == ExceptionHandlerType.Catch)
                {
                    tryOperand = IRConstant.FromI4((int) tr.VM.Data.GetId(scope.ExceptionHandler.CatchType));
                    ehType = tr.VM.Runtime.RTFlags.EH_CATCH;
                }
                else if(scope.ExceptionHandler.HandlerType == ExceptionHandlerType.Filter)
                {
                    tryOperand = new IRBlockTarget(filter);
                    ehType = tr.VM.Runtime.RTFlags.EH_FILTER;
                }
                else if(scope.ExceptionHandler.HandlerType == ExceptionHandlerType.Fault)
                {
                    ehType = tr.VM.Runtime.RTFlags.EH_FAULT;
                }
                else if(scope.ExceptionHandler.HandlerType == ExceptionHandlerType.Finally)
                {
                    ehType = tr.VM.Runtime.RTFlags.EH_FINALLY;
                }
                else
                {
                    throw new InvalidProgramException();
                }

                tryStartInstrs.Add(new IRInstruction(IROpCode.TRY, IRConstant.FromI4(ehType), tryOperand)
                {
                    Annotation = new EHInfo(scope.ExceptionHandler)
                });
            }
            tr.Instructions.InsertRange(0, tryStartInstrs);
        }

        private ScopeBlock SearchForTry(ScopeBlock scope, ExceptionHandler eh)
        {
            if(scope.ExceptionHandler == eh && scope.Type == ScopeType.Try)
                return scope;
            foreach(ScopeBlock child in scope.Children)
            {
                ScopeBlock s = this.SearchForTry(child, eh);
                if(s != null)
                    return s;
            }
            return null;
        }


        private static ScopeBlock FindCommonAncestor(ScopeBlock[] a, ScopeBlock[] b)
        {
            ScopeBlock ret = null;
            for(int i = 0; i < a.Length && i < b.Length; i++)
                if(a[i] == b[i])
                    ret = a[i];
                else
                    break;
            return ret;
        }

        private void VisitInstr(IRInstrList instrs, IRInstruction instr, ref int index, IRTransformer tr)
        {
            if(instr.OpCode != IROpCode.__LEAVE)
                return;

            ScopeBlock[] targetScopes = tr.RootScope.SearchBlock(((IRBlockTarget) instr.Operand1).Target);

            ScopeBlock escapeTarget = FindCommonAncestor(this.thisScopes, targetScopes);
            var leaveInstrs = new List<IRInstruction>();
            for(int i = this.thisScopes.Length - 1; i >= 0; i--)
            {
                if(this.thisScopes[i] == escapeTarget)
                    break;
                if(this.thisScopes[i].Type != ScopeType.Try)
                    continue;

                IBasicBlock handler = null, filter = null;
                this.SearchForHandlers(tr.RootScope, this.thisScopes[i].ExceptionHandler, ref handler, ref filter);
                if(handler == null)
                    throw new InvalidProgramException();

                leaveInstrs.Add(new IRInstruction(IROpCode.LEAVE, new IRBlockTarget(handler))
                {
                    Annotation = new EHInfo(this.thisScopes[i].ExceptionHandler)
                });
            }
            instr.OpCode = IROpCode.JMP;
            leaveInstrs.Add(instr);
            instrs.Replace(index, leaveInstrs);
        }
    }
}