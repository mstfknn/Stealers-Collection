#region

using System;
using System.Collections.Generic;
using dnlib.DotNet;
using KoiVM.AST.ILAST;
using KoiVM.CFG;
using KoiVM.ILAST.Transformation;
using KoiVM.RT;
using KoiVM.VM;

#endregion

namespace KoiVM.ILAST
{
    public class ILASTTransformer
    {
        private ITransformationHandler[] pipeline;

        public ILASTTransformer(MethodDef method, ScopeBlock rootScope, DarksVMRuntime runtime)
        {
            this.RootScope = rootScope;
            this.Method = method;
            this.Runtime = runtime;

            this.Annotations = new Dictionary<object, object>();
            this.InitPipeline();
        }

        public MethodDef Method
        {
            get;
        }

        public ScopeBlock RootScope
        {
            get;
        }

        public DarksVMRuntime Runtime
        {
            get;
        }

        public VMDescriptor VM => this.Runtime.Descriptor;

        internal Dictionary<object, object> Annotations
        {
            get;
        }

        internal BasicBlock<ILASTTree> Block
        {
            get;
            private set;
        }

        internal ILASTTree Tree => this.Block.Content;

        private void InitPipeline() => this.pipeline = new ITransformationHandler[]
            {
                new VariableInlining(),
                new StringTransform(),
                new ArrayTransform(),
                new IndirectTransform(),
                new ILASTTypeInference(),
                new NullTransform(),
                new BranchTransform()
            };

        public void Transform()
        {
            if(this.pipeline == null)
                throw new InvalidOperationException("Transformer already used.");

            foreach(ITransformationHandler handler in this.pipeline)
            {
                handler.Initialize(this);

                this.RootScope.ProcessBasicBlocks<ILASTTree>(block =>
                {
                    this.Block = block;
                    handler.Transform(this);
                });
            }

            this.pipeline = null;
        }
    }
}