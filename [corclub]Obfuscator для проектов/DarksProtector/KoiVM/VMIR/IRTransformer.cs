#region

using System;
using System.Collections.Generic;
using KoiVM.AST.IR;
using KoiVM.CFG;
using KoiVM.RT;
using KoiVM.VM;
using KoiVM.VMIR.Transforms;

#endregion

namespace KoiVM.VMIR
{
    public class IRTransformer
    {
        private ITransform[] pipeline;

        public IRTransformer(ScopeBlock rootScope, IRContext ctx, DarksVMRuntime runtime)
        {
            this.RootScope = rootScope;
            this.Context = ctx;
            this.Runtime = runtime;

            this.Annotations = new Dictionary<object, object>();
            this.InitPipeline();
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

        public ScopeBlock RootScope
        {
            get;
        }

        internal Dictionary<object, object> Annotations
        {
            get;
        }

        internal BasicBlock<IRInstrList> Block
        {
            get;
            private set;
        }

        internal IRInstrList Instructions => this.Block.Content;

        private void InitPipeline() => this.pipeline = new ITransform[]
            {
                // new SMCIRTransform(),
                this.Context.IsRuntime ? null : new GuardBlockTransform(),
                this.Context.IsRuntime ? null : new EHTransform(),
                new InitLocalTransform(),
                new ConstantTypePromotionTransform(),
                new GetSetFlagTransform(),
                new LogicTransform(),
                new InvokeTransform(),
                new MetadataTransform(),
                this.Context.IsRuntime ? null : new RegisterAllocationTransform(),
                this.Context.IsRuntime ? null : new StackFrameTransform(),
                new LeaTransform(),
                this.Context.IsRuntime ? null : new MarkReturnRegTransform()
            };

        public void Transform()
        {
            if(this.pipeline == null)
                throw new InvalidOperationException("Transformer already used.");

            foreach(ITransform handler in this.pipeline)
            {
                if(handler == null)
                    continue;
                handler.Initialize(this);

                this.RootScope.ProcessBasicBlocks<IRInstrList>(block =>
                {
                    this.Block = block;
                    handler.Transform(this);
                });
            }

            this.pipeline = null;
        }
    }
}