#region

using System;
using System.Collections.Generic;
using dnlib.DotNet;
using KoiVM.AST.IL;
using KoiVM.CFG;
using KoiVM.RT;
using KoiVM.VMIL.Transforms;

#endregion

namespace KoiVM.VMIL
{
    public class ILPostTransformer
    {
        private IPostTransform[] pipeline;

        public ILPostTransformer(MethodDef method, ScopeBlock rootScope, DarksVMRuntime runtime)
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

        private IPostTransform[] InitPipeline() => new IPostTransform[]
            {
                new SaveRegistersTransform(),
                new FixMethodRefTransform(),
                new BlockKeyTransform()
            };

        public void Transform()
        {
            if(this.pipeline == null)
                throw new InvalidOperationException("Transformer already used.");

            foreach(IPostTransform handler in this.pipeline)
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