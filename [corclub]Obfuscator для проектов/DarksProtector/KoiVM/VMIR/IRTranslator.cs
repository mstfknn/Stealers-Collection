#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using dnlib.DotNet.Emit;
using KoiVM.AST.ILAST;
using KoiVM.AST.IR;
using KoiVM.CFG;
using KoiVM.RT;
using KoiVM.VM;

#endregion

namespace KoiVM.VMIR
{
    public class IRTranslator
    {
        private static readonly Dictionary<Code, ITranslationHandler> handlers;

        static IRTranslator()
        {
            handlers = new Dictionary<Code, ITranslationHandler>();
            foreach(Type type in typeof(IRTranslator).Assembly.GetExportedTypes())
                if(typeof(ITranslationHandler).IsAssignableFrom(type) && !type.IsAbstract)
                {
                    var handler = (ITranslationHandler) Activator.CreateInstance(type);
                    handlers.Add(handler.ILCode, handler);
                }
        }

        public IRTranslator(IRContext ctx, DarksVMRuntime runtime)
        {
            this.Context = ctx;
            this.Runtime = runtime;
        }

        public ScopeBlock RootScope
        {
            get;
            private set;
        }

        public IRContext Context
        {
            get;
        }

        public DarksVMRuntime Runtime
        {
            get;
        }

        public VMDescriptor VM => this.Runtime.Descriptor;

        public ArchDescriptor Arch => this.VM.Architecture;

        internal BasicBlock<ILASTTree> Block
        {
            get;
            private set;
        }

        internal IRInstrList Instructions
        {
            get;
            private set;
        }

        internal IIROperand Translate(IILASTNode node)
        {
            if (node is ILASTExpression expr)
            {
                try
                {
                    if (!handlers.TryGetValue(expr.ILCode, out ITranslationHandler handler))
                        throw new NotSupportedException(expr.ILCode.ToString());

                    int i = this.Instructions.Count;
                    IIROperand operand = handler.Translate(expr, this);
                    while (i < this.Instructions.Count)
                    {
                        this.Instructions[i].ILAST = expr;
                        i++;
                    }
                    return operand;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to translate expr {expr.CILInstr} @ {expr.CILInstr.GetOffset():x4}.", ex);
                }
            }
            if (node is ILASTVariable) return this.Context.ResolveVRegister((ILASTVariable) node);
            throw new NotSupportedException();
        }

        private IRInstrList Translate(BasicBlock<ILASTTree> block)
        {
            this.Block = block;
            this.Instructions = new IRInstrList();

            bool seenJump = false;
            foreach(IILASTStatement st in block.Content)
                if(st is ILASTPhi)
                {
                    ILASTVariable variable = ((ILASTPhi) st).Variable;
                    this.Instructions.Add(new IRInstruction(IROpCode.POP)
                    {
                        Operand1 = this.Context.ResolveVRegister(variable),
                        ILAST = st
                    });
                }
                else if (st is ILASTAssignment assignment)
                {
                    IIROperand valueVar = this.Translate(assignment.Value);
                    this.Instructions.Add(new IRInstruction(IROpCode.MOV)
                    {
                        Operand1 = this.Context.ResolveVRegister(assignment.Variable),
                        Operand2 = valueVar,
                        ILAST = st
                    });
                }
                else if (st is ILASTExpression expr)
                {
                    var opCode = expr.ILCode.ToOpCode();
                    if (!seenJump && (opCode.FlowControl == FlowControl.Cond_Branch ||
                                     opCode.FlowControl == FlowControl.Branch ||
                                     opCode.FlowControl == FlowControl.Return ||
                                     opCode.FlowControl == FlowControl.Throw))
                    {
                        // Add stack remain before jumps
                        foreach (ILASTVariable remain in block.Content.StackRemains)
                            this.Instructions.Add(new IRInstruction(IROpCode.PUSH)
                            {
                                Operand1 = this.Context.ResolveVRegister(remain),
                                ILAST = st
                            });
                        seenJump = true;
                    }
                    this.Translate((ILASTExpression)st);
                }
                else
                {
                    throw new NotSupportedException();
                }
            Debug.Assert(seenJump);

            IRInstrList ret = this.Instructions;
            this.Instructions = null;
            return ret;
        }

        public void Translate(ScopeBlock rootScope)
        {
            this.RootScope = rootScope;
            Dictionary<BasicBlock<ILASTTree>, BasicBlock<IRInstrList>> blockMap = rootScope.UpdateBasicBlocks<ILASTTree, IRInstrList>(block => { return this.Translate(block); });
            rootScope.ProcessBasicBlocks<IRInstrList>(block =>
            {
                foreach(IRInstruction instr in block.Content)
                {
                    if(instr.Operand1 is IRBlockTarget)
                    {
                        var op = (IRBlockTarget) instr.Operand1;
                        op.Target = blockMap[(BasicBlock<ILASTTree>) op.Target];
                    }
                    else if (instr.Operand1 is IRJumpTable op)
                    {
                        for (int i = 0; i < op.Targets.Length; i++)
                            op.Targets[i] = blockMap[(BasicBlock<ILASTTree>)op.Targets[i]];
                    }

                    if (instr.Operand2 is IRBlockTarget)
                    {
                        var op = (IRBlockTarget) instr.Operand2;
                        op.Target = blockMap[(BasicBlock<ILASTTree>) op.Target];
                    }
                    else if (instr.Operand2 is IRJumpTable op)
                    {
                        for (int i = 0; i < op.Targets.Length; i++)
                            op.Targets[i] = blockMap[(BasicBlock<ILASTTree>)op.Targets[i]];
                    }
                }
            });
        }
    }
}