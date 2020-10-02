#region

using System;
using System.Collections.Generic;
using dnlib.DotNet;
using KoiVM.AST.IL;
using KoiVM.CFG;
using KoiVM.RT;
using KoiVM.VM;
using KoiVM.VMIL.Transforms;

#endregion

namespace KoiVM.VMIL
{
    public class ILTransformer
    {
        private ITransform[] pipeline;

        public ILTransformer(MethodDef method, ScopeBlock rootScope, DarksVMRuntime runtime)
        {
            this.RootScope = rootScope;
            this.Method = method;
            this.Runtime = runtime;

            this.Annotations = new Dictionary<object, object>();
            this.pipeline = this.InitPipeline();
        }

        public DarksVMRuntime Runtime
        {
            get;
        }

        public MethodDef Method
        {
            get;
        }

        public ScopeBlock RootScope
        {
            get;
        }

        public VMDescriptor VM => this.Runtime.Descriptor;

        internal Dictionary<object, object> Annotations
        {
            get;
        }

        internal ILBlock Block
        {
            get;
            private set;
        }

        internal ILInstrList Instructions => this.Block.Content;

        private ITransform[] InitPipeline() => new ITransform[]
            {
                // new SMCILTransform(),
                new ReferenceOffsetTransform(),
                new EntryExitTransform(),
                new SaveInfoTransform()
            };

        public void Transform()
        {
            if(this.pipeline == null)
                throw new InvalidOperationException("Transformer already used.");

            foreach(ITransform handler in this.pipeline)
            {
                handler.Initialize(this);

                this.RootScope.ProcessBasicBlocks<ILInstrList>(block =>
                {
                    this.Block = (ILBlock) block;
                    handler.Transform(this);
                });
            }

            this.pipeline = null;
        }
    }
}