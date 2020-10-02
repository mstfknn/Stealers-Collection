#region

using System;
using System.Collections.Generic;
using KoiVM.AST.IL;
using KoiVM.AST.IR;
using KoiVM.CFG;
using KoiVM.RT;
using KoiVM.VM;
using KoiVM.VMIR;

#endregion

namespace KoiVM.VMIL
{
    public class ILTranslator
    {
        private static readonly Dictionary<IROpCode, ITranslationHandler> handlers;

        static ILTranslator()
        {
            handlers = new Dictionary<IROpCode, ITranslationHandler>();
            foreach(Type type in typeof(ILTranslator).Assembly.GetExportedTypes())
                if(typeof(ITranslationHandler).IsAssignableFrom(type) && !type.IsAbstract)
                {
                    var handler = (ITranslationHandler) Activator.CreateInstance(type);
                    handlers.Add(handler.IRCode, handler);
                }
        }

        public ILTranslator(DarksVMRuntime runtime) => this.Runtime = runtime;

        public DarksVMRuntime Runtime
        {
            get;
        }

        public VMDescriptor VM => this.Runtime.Descriptor;

        internal ILInstrList Instructions
        {
            get;
            private set;
        }

        public ILInstrList Translate(IRInstrList instrs)
        {
            this.Instructions = new ILInstrList();

            int i = 0;
            foreach(IRInstruction instr in instrs)
            {
                if (!handlers.TryGetValue(instr.OpCode, out ITranslationHandler handler))
                    throw new NotSupportedException(instr.OpCode.ToString());
                try
                {
                    handler.Translate(instr, this);
                }
                catch(Exception ex)
                {
                    throw new Exception(string.Format("Failed to translate ir {0}.", instr.ILAST), ex);
                }
                while(i < this.Instructions.Count)
                {
                    this.Instructions[i].IR = instr;
                    i++;
                }
            }

            ILInstrList ret = this.Instructions;
            this.Instructions = null;
            return ret;
        }

        public void Translate(ScopeBlock rootScope)
        {
            Dictionary<BasicBlock<IRInstrList>, BasicBlock<ILInstrList>> blockMap = rootScope.UpdateBasicBlocks<IRInstrList, ILInstrList>(
                block => { return this.Translate(block.Content); },
                (id, content) => new ILBlock(id, content));

            rootScope.ProcessBasicBlocks<ILInstrList>(block =>
            {
                foreach(ILInstruction instr in block.Content)
                    if(instr.Operand is ILBlockTarget)
                    {
                        var op = (ILBlockTarget) instr.Operand;
                        op.Target = blockMap[(BasicBlock<IRInstrList>) op.Target];
                    }
                    else if (instr.Operand is ILJumpTable op)
                    {
                        for (int i = 0; i < op.Targets.Length; i++)
                            op.Targets[i] = blockMap[(BasicBlock<IRInstrList>)op.Targets[i]];
                    }
            });
        }
    }
}