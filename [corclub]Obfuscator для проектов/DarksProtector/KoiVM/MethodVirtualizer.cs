#region

using System;
using System.Reflection;
using dnlib.DotNet;
using KoiVM.CFG;
using KoiVM.ILAST;
using KoiVM.RT;
using KoiVM.VMIL;
using KoiVM.VMIR;

#endregion

namespace KoiVM
{
    [Obfuscation(Exclude = false, Feature = "+koi;-ref proxy")]
    public class MethodVirtualizer
    {
        public MethodVirtualizer(DarksVMRuntime runtime) => this.Runtime = runtime;

        protected DarksVMRuntime Runtime
        {
            get;
        }

        protected MethodDef Method
        {
            get;
            private set;
        }

        protected ScopeBlock RootScope
        {
            get;
            private set;
        }

        protected IRContext IRContext
        {
            get;
            private set;
        }

        protected bool IsExport
        {
            get;
            private set;
        }

        public ScopeBlock Load(MethodDef method, bool isExport)
        {
            try
            {
                this.Method = method;
                this.IsExport = isExport;

                this.Init();
                this.BuildILAST();
                this.TransformILAST();
                this.BuildVMIR();
                this.TransformVMIR();
                this.BuildVMIL();
                this.TransformVMIL();
                this.Deinitialize();

                ScopeBlock scope = this.RootScope;
                this.RootScope = null;
                this.Method = null;
                return scope;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Failed to translate method {method}.", ex);
                ScopeBlock scope = this.RootScope;
                this.RootScope = null;
                this.Method = null;
                return scope;
                //throw new Exception(string.Format("Failed to translate method {0}.", method), ex);
            }
        }

        protected virtual void Init()
        {
            this.RootScope = BlockParser.Parse(this.Method, this.Method.Body);
            this.IRContext = new IRContext(this.Method, this.Method.Body);
        }

        protected virtual void BuildILAST() => ILASTBuilder.BuildAST(this.Method, this.Method.Body, this.RootScope);

        protected virtual void TransformILAST()
        {
            var transformer = new ILASTTransformer(this.Method, this.RootScope, this.Runtime);
            transformer.Transform();
        }

        protected virtual void BuildVMIR()
        {
            var translator = new IRTranslator(this.IRContext, this.Runtime);
            translator.Translate(this.RootScope);
        }

        protected virtual void TransformVMIR()
        {
            var transformer = new IRTransformer(this.RootScope, this.IRContext, this.Runtime);
            transformer.Transform();
        }

        protected virtual void BuildVMIL()
        {
            var translator = new ILTranslator(this.Runtime);
            translator.Translate(this.RootScope);
        }

        protected virtual void TransformVMIL()
        {
            var transformer = new ILTransformer(this.Method, this.RootScope, this.Runtime);
            transformer.Transform();
        }

        protected virtual void Deinitialize()
        {
            this.IRContext = null;
            this.Runtime.AddMethod(this.Method, this.RootScope);
            if(this.IsExport)
                this.Runtime.ExportMethod(this.Method);
        }
    }
}