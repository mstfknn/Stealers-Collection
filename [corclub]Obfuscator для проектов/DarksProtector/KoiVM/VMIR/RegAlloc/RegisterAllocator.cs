#region

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using KoiVM.AST.IR;
using KoiVM.CFG;
using KoiVM.VM;

#endregion

namespace KoiVM.VMIR.RegAlloc
{
    public class RegisterAllocator
    {
        private Dictionary<IRVariable, object> allocation;
        private int baseOffset;
        private Dictionary<IRVariable, StackSlot> globalVars;

        private Dictionary<BasicBlock<IRInstrList>, BlockLiveness> liveness;

        // TODO: Cross basic block allocation
        private readonly IRTransformer transformer;

        public RegisterAllocator(IRTransformer transformer) => this.transformer = transformer;

        public int LocalSize
        {
            get;
            set;
        }

        public void Initialize()
        {
            var blocks = this.transformer.RootScope.GetBasicBlocks().Cast<BasicBlock<IRInstrList>>().ToList();
            this.liveness = LivenessAnalysis.ComputeLiveness(blocks);

            var stackVars = new HashSet<IRVariable>();
            foreach(KeyValuePair<BasicBlock<IRInstrList>, BlockLiveness> blockLiveness in this.liveness)
            {
                foreach(IRInstruction instr in blockLiveness.Key.Content)
                {
                    if(instr.OpCode != IROpCode.__LEA)
                        continue;

                    var variable = (IRVariable) instr.Operand2;
                    if(variable.VariableType != IRVariableType.Argument)
                        stackVars.Add(variable);
                }
                stackVars.UnionWith(blockLiveness.Value.OutLive);
            }

            // [BP - 2] = last argument
            // [BP - 1] = return address
            // [BP    ] = old BP
            // [BP + 1] = first local

            int offset = 1;
            this.globalVars = stackVars.ToDictionary(var => var, var => new StackSlot(offset++, var));
            this.baseOffset = offset;
            this.LocalSize = this.baseOffset - 1;

            offset = -2;
            IRVariable[] parameters = this.transformer.Context.GetParameters();
            for(int i = parameters.Length - 1; i >= 0; i--)
            {
                IRVariable paramVar = parameters[i];
                this.globalVars[paramVar] = new StackSlot(offset--, paramVar);
            }

            this.allocation = this.globalVars.ToDictionary(pair => pair.Key, pair => (object) pair.Value);
        }

        public void Allocate(BasicBlock<IRInstrList> block)
        {
            BlockLiveness blockLiveness = this.liveness[block];
            Dictionary<IRInstruction, HashSet<IRVariable>> instrLiveness = LivenessAnalysis.ComputeLiveness(block, blockLiveness);
            var pool = RegisterPool.Create(this.baseOffset, this.globalVars);

            for(int i = 0; i < block.Content.Count; i++)
            {
                IRInstruction instr = block.Content[i];
                pool.CheckLiveness(instrLiveness[instr]);

                // Allocates
                if(instr.Operand1 != null)
                    instr.Operand1 = this.AllocateOperand(instr.Operand1, pool);
                if(instr.Operand2 != null)
                    instr.Operand2 = this.AllocateOperand(instr.Operand2, pool);
            }
            if(pool.SpillOffset - 1 > this.LocalSize)
                this.LocalSize = pool.SpillOffset - 1;
            this.baseOffset = pool.SpillOffset;
        }

        private IIROperand AllocateOperand(IIROperand operand, RegisterPool pool)
        {
            if (operand is IRVariable variable)
            {
                DarksVMRegisters? reg = this.AllocateVariable(pool, variable, out StackSlot? slot);
                if (reg != null)
                    return new IRRegister(reg.Value)
                    {
                        SourceVariable = variable,
                        Type = variable.Type
                    };
                variable.Annotation = slot.Value;
                return new IRPointer
                {
                    Register = IRRegister.BP,
                    Offset = slot.Value.Offset,
                    SourceVariable = variable,
                    Type = variable.Type
                };
            }
            return operand;
        }

        private DarksVMRegisters? AllocateVariable(RegisterPool pool, IRVariable var, out StackSlot? stackSlot)
        {
            stackSlot = pool.CheckSpill(var);
            if(stackSlot == null)
            {
                DarksVMRegisters? allocReg = var.Annotation == null ? (DarksVMRegisters?) null : (DarksVMRegisters) var.Annotation;
                if(allocReg == null)
                    allocReg = pool.Allocate(var);
                if(allocReg != null)
                {
                    if(var.Annotation == null)
                        var.Annotation = allocReg.Value;
                    return allocReg;
                }
                // Spill variable
                stackSlot = pool.SpillVariable(var);
            }
            return null;
        }

        private struct StackSlot
        {
            public readonly int Offset;
            public readonly IRVariable Variable;

            public StackSlot(int offset, IRVariable var)
            {
                this.Offset = offset;
                this.Variable = var;
            }
        }

        private class RegisterPool
        {
            private const int NumRegisters = 8;

            private IRVariable[] regAlloc;
            private Dictionary<IRVariable, StackSlot> spillVars;

            public int SpillOffset
            {
                get;
                set;
            }

            private static DarksVMRegisters ToRegister(int regId) => (DarksVMRegisters)regId;

            private static int FromRegister(DarksVMRegisters reg) => (int)reg;

            public static RegisterPool Create(int baseOffset, Dictionary<IRVariable, StackSlot> globalVars)
            {
                var pool = new RegisterPool
                {
                    regAlloc = new IRVariable[NumRegisters],
                    spillVars = new Dictionary<IRVariable, StackSlot>(globalVars),
                    SpillOffset = baseOffset
                };
                return pool;
            }

            public DarksVMRegisters? Allocate(IRVariable var)
            {
                for(int i = 0; i < this.regAlloc.Length; i++)
                    if(this.regAlloc[i] == null)
                    {
                        this.regAlloc[i] = var;
                        return ToRegister(i);
                    }
                return null;
            }

            public void Deallocate(IRVariable var, DarksVMRegisters reg)
            {
                Debug.Assert(this.regAlloc[FromRegister(reg)] == var);
                this.regAlloc[FromRegister(reg)] = null;
            }

            public void CheckLiveness(HashSet<IRVariable> live)
            {
                for(int i = 0; i < this.regAlloc.Length; i++)
                    if(this.regAlloc[i] != null && !live.Contains(this.regAlloc[i]))
                    {
                        this.regAlloc[i].Annotation = null;
                        this.regAlloc[i] = null;
                    }
            }

            public StackSlot SpillVariable(IRVariable var)
            {
                var slot = new StackSlot(this.SpillOffset++, var);
                this.spillVars[var] = slot;
                return slot;
            }

            public StackSlot? CheckSpill(IRVariable var) => !this.spillVars.TryGetValue(var, out StackSlot ret) ? null : (StackSlot?)ret;
        }
    }
}