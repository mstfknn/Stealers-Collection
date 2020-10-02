#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using KoiVM.AST;
using KoiVM.AST.ILAST;
using KoiVM.CFG;

#endregion

namespace KoiVM.ILAST
{
    #region

    using CILBlock = BasicBlock<CILInstrList>;

    #endregion

    public class ILASTBuilder
    {
        private readonly IList<CILBlock> basicBlocks;
        private readonly Dictionary<Instruction, CILBlock> blockHeaders;
        private readonly Dictionary<CILBlock, BlockState> blockStates;
        private readonly CilBody body;
        private readonly List<ILASTExpression> instrReferences;
        private readonly MethodDef method;
        private readonly ScopeBlock scope;

        private ILASTBuilder(MethodDef method, CilBody body, ScopeBlock scope)
        {
            this.method = method;
            this.body = body;
            this.scope = scope;

            this.basicBlocks = scope.GetBasicBlocks().Cast<CILBlock>().ToList();
            this.blockHeaders = this.basicBlocks.ToDictionary(block => block.Content[0], block => block);
            this.blockStates = new Dictionary<CILBlock, BlockState>();
            this.instrReferences = new List<ILASTExpression>();
            Debug.Assert(this.basicBlocks.Count > 0);
        }

        public static void BuildAST(MethodDef method, CilBody body, ScopeBlock scope)
        {
            var builder = new ILASTBuilder(method, body, scope);
            var basicBlocks = scope.GetBasicBlocks().Cast<CILBlock>().ToList();
            builder.BuildAST();
        }

        private void BuildAST()
        {
            this.BuildASTInternal();
            this.BuildPhiNodes();
            Dictionary<CILBlock, BasicBlock<ILASTTree>> blockMap = this.scope.UpdateBasicBlocks((CILBlock block) => this.blockStates[block].ASTTree);
            var newBlockMap = this.blockHeaders.ToDictionary(pair => pair.Key, pair => blockMap[pair.Value]);
            foreach(ILASTExpression expr in this.instrReferences)
                if(expr.Operand is Instruction)
                    expr.Operand = newBlockMap[(Instruction) expr.Operand];
                else
                    expr.Operand = ((Instruction[]) expr.Operand).Select(instr => (IBasicBlock) newBlockMap[instr]).ToArray();
        }

        private void BuildASTInternal()
        {
            var workList = new Stack<CILBlock>();
            this.PopulateBeginStates(workList);

            var visited = new HashSet<CILBlock>();
            while(workList.Count > 0)
            {
                CILBlock block = workList.Pop();
                if(visited.Contains(block))
                    continue;
                visited.Add(block);

                Debug.Assert(this.blockStates.ContainsKey(block));
                BlockState state = this.blockStates[block];
                Debug.Assert(state.ASTTree == null);

                ILASTTree tree = this.BuildAST(block.Content, state.BeginStack);
                ILASTVariable[] remains = tree.StackRemains;
                state.ASTTree = tree;
                this.blockStates[block] = state;

                // Propagate stack states
                foreach(CILBlock successor in block.Targets)
                {
                    if (!this.blockStates.TryGetValue(successor, out BlockState successorState))
                    {
                        var blockVars = new ILASTVariable[remains.Length];
                        for (int i = 0; i < blockVars.Length; i++)
                            blockVars[i] = new ILASTVariable
                            {
                                Name = string.Format("ph_{0:x2}_{1:x2}", successor.Id, i),
                                Type = remains[i].Type,
                                VariableType = ILASTVariableType.PhiVar
                            };
                        successorState = new BlockState
                        {
                            BeginStack = blockVars
                        };
                        this.blockStates[successor] = successorState;
                    }
                    else
                    {
                        if (successorState.BeginStack.Length != remains.Length)
                            throw new InvalidProgramException("Inconsistent stack depth.");
                    }
                    workList.Push(successor);
                }
            }
        }

        private void PopulateBeginStates(Stack<CILBlock> workList)
        {
            for(int i = 0; i < this.body.ExceptionHandlers.Count; i++)
            {
                ExceptionHandler eh = this.body.ExceptionHandlers[i];
                this.blockStates[this.blockHeaders[eh.TryStart]] = new BlockState
                {
                    BeginStack = new ILASTVariable[0]
                };

                CILBlock handlerBlock = this.blockHeaders[eh.HandlerStart];
                workList.Push(handlerBlock);
                if(eh.HandlerType == ExceptionHandlerType.Fault ||
                   eh.HandlerType == ExceptionHandlerType.Finally)
                {
                    this.blockStates[handlerBlock] = new BlockState
                    {
                        BeginStack = new ILASTVariable[0]
                    };
                }
                else
                {
                    var type = TypeInference.ToASTType(eh.CatchType.ToTypeSig());
                    // Do not process overlapped handler blocks twice
                    if(!this.blockStates.ContainsKey(handlerBlock))
                    {
                        var exVar = new ILASTVariable
                        {
                            Name = string.Format("ex_{0:x2}", i),
                            Type = type,
                            VariableType = ILASTVariableType.ExceptionVar,
                            Annotation = eh
                        };
                        this.blockStates[handlerBlock] = new BlockState
                        {
                            BeginStack = new[] {exVar}
                        };
                    }
                    else
                    {
                        Debug.Assert(this.blockStates[handlerBlock].BeginStack.Length == 1);
                    }

                    if(eh.FilterStart != null)
                    {
                        var filterVar = new ILASTVariable
                        {
                            Name = string.Format("ef_{0:x2}", i),
                            Type = type,
                            VariableType = ILASTVariableType.FilterVar,
                            Annotation = eh
                        };
                        CILBlock filterBlock = this.blockHeaders[eh.FilterStart];
                        workList.Push(filterBlock);
                        this.blockStates[filterBlock] = new BlockState
                        {
                            BeginStack = new[] {filterVar}
                        };
                    }
                }
            }
            this.blockStates[this.basicBlocks[0]] = new BlockState
            {
                BeginStack = new ILASTVariable[0]
            };
            workList.Push(this.basicBlocks[0]);
            foreach(CILBlock block in this.basicBlocks)
            {
                if(block.Sources.Count > 0)
                    continue;
                if(workList.Contains(block))
                    continue;
                this.blockStates[block] = new BlockState
                {
                    BeginStack = new ILASTVariable[0]
                };
                workList.Push(block);
            }
        }

        private void BuildPhiNodes()
        {
            foreach(KeyValuePair<CILBlock, BlockState> statePair in this.blockStates)
            {
                CILBlock block = statePair.Key;
                BlockState state = statePair.Value;
                // source count = 0 => eh handlers begin state, having ex object
                if(block.Sources.Count == 0 && state.BeginStack.Length > 0)
                {
                    Debug.Assert(state.BeginStack.Length == 1);
                    var phi = new ILASTPhi
                    {
                        Variable = state.BeginStack[0],
                        SourceVariables = new[] {state.BeginStack[0]}
                    };
                    state.ASTTree.Insert(0, phi);
                }
                else if(state.BeginStack.Length > 0)
                {
                    for(int varIndex = 0; varIndex < state.BeginStack.Length; varIndex++)
                    {
                        var phi = new ILASTPhi {Variable = state.BeginStack[varIndex]};
                        phi.SourceVariables = new ILASTVariable[block.Sources.Count];
                        for(int i = 0; i < phi.SourceVariables.Length; i++) phi.SourceVariables[i] = this.blockStates[block.Sources[i]].ASTTree.StackRemains[varIndex];
                        // reverse phi nodes => pop in correct order
                        state.ASTTree.Insert(0, phi);
                    }
                }
            }
        }

        private ILASTTree BuildAST(CILInstrList instrs, ILASTVariable[] beginStack)
        {
            var tree = new ILASTTree();
            var evalStack = new Stack<ILASTVariable>(beginStack);
            Func<int, IILASTNode[]> popArgs = numArgs =>
            {
                var args = new IILASTNode[numArgs];
                for(int i = numArgs - 1; i >= 0; i--)
                    args[i] = evalStack.Pop();
                return args;
            };

            var prefixes = new List<Instruction>();
            foreach(Instruction instr in instrs)
            {
                if(instr.OpCode.OpCodeType == OpCodeType.Prefix)
                {
                    prefixes.Add(instr);
                    continue;
                }

                int pushes, pops;
                ILASTExpression expr;
                if(instr.OpCode.Code == Code.Dup)
                {
                    pushes = pops = 1;

                    ILASTVariable arg = evalStack.Peek();
                    expr = new ILASTExpression
                    {
                        ILCode = Code.Dup,
                        Operand = null,
                        Arguments = new IILASTNode[] {arg}
                    };
                }
                else
                {
                    instr.CalculateStackUsage(this.method.ReturnType.ElementType != ElementType.Void, out pushes, out pops);
                    Debug.Assert(pushes == 0 || pushes == 1);

                    if(pops == -1)
                    {
                        evalStack.Clear();
                        pops = 0;
                    }

                    expr = new ILASTExpression
                    {
                        ILCode = instr.OpCode.Code,
                        Operand = instr.Operand,
                        Arguments = popArgs(pops)
                    };
                    if(expr.Operand is Instruction || expr.Operand is Instruction[])
                        this.instrReferences.Add(expr);
                }
                expr.CILInstr = instr;
                if(prefixes.Count > 0)
                {
                    expr.Prefixes = prefixes.ToArray();
                    prefixes.Clear();
                }

                if(pushes == 1)
                {
                    var variable = new ILASTVariable
                    {
                        Name = string.Format("s_{0:x4}", instr.Offset),
                        VariableType = ILASTVariableType.StackVar
                    };
                    evalStack.Push(variable);

                    tree.Add(new ILASTAssignment
                    {
                        Variable = variable,
                        Value = expr
                    });
                }
                else
                {
                    tree.Add(expr);
                }
            }
            tree.StackRemains = evalStack.Reverse().ToArray();
            return tree;
        }

        private struct BlockState
        {
            public ILASTVariable[] BeginStack;
            public ILASTTree ASTTree;
        }
    }
}