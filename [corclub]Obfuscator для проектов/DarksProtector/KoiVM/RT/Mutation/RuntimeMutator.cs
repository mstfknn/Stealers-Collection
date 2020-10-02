#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using dnlib.DotNet;
using dnlib.DotNet.Writer;

#endregion

namespace KoiVM.RT.Mutation
{
    internal class RuntimeMutator : IModuleWriterListener
    {
        internal RTConstants constants;
        private RuntimeHelpers helpers;
        private readonly MethodPatcher methodPatcher;
        private readonly DarksVMRuntime rt;
        private MetaData rtMD;
        private ModuleWriterBase rtWriter;

        public RuntimeMutator(ModuleDef module, DarksVMRuntime rt)
        {
            this.RTModule = module;
            this.rt = rt;
            this.methodPatcher = new MethodPatcher(module);

            this.constants = new RTConstants();
            this.helpers = new RuntimeHelpers(this.constants, rt, module);
            this.constants.InjectConstants(module, rt.Descriptor, this.helpers);
            this.helpers.AddHelpers();
        }

        public ModuleDef RTModule
        {
            get;
            set;
        }

        public byte[] RuntimeLib
        {
            get;
            private set;
        }

        public byte[] RuntimeSym
        {
            get;
            private set;
        }

        void IModuleWriterListener.OnWriterEvent(ModuleWriterBase writer, ModuleWriterEvent evt)
        {
            this.rtWriter = writer;
            this.rtMD = writer.MetaData;
            if(evt == ModuleWriterEvent.MDEndCreateTables)
            {
                this.MutateMetadata();
                var request = new RequestKoiEventArgs();
                RequestKoi(this, request);
                writer.TheOptions.MetaDataOptions.OtherHeaps.Add(request.Heap);

                this.rt.ResetData();
            }
        }

        public void InitHelpers()
        {
            this.helpers = new RuntimeHelpers(this.constants, this.rt, this.RTModule);
            this.helpers.AddHelpers();
        }

        public void CommitRuntime(ModuleDef targetModule)
        {
            this.MutateRuntime();

            if(targetModule == null)
            {
                var stream = new MemoryStream();
                var pdbStream = new MemoryStream();

                var options = new ModuleWriterOptions(this.RTModule);

                this.RTModule.Write(stream, options);
                this.RuntimeLib = stream.ToArray();
                this.RuntimeSym = new byte[0];
            }
            else
            {
                var types = this.RTModule.Types.Where(t => !t.IsGlobalModuleType).ToList();
                this.RTModule.Types.Clear();
                foreach(TypeDef type in types) targetModule.Types.Add(type);
            }
        }

        public IModuleWriterListener CommitModule(ModuleDef module)
        {
            this.ImportReferences(module);
            return this;
        }

        public void ReplaceMethodStub(MethodDef method) => this.methodPatcher.PatchMethodStub(method, this.rt.Descriptor.Data.GetExportId(method));

        public event EventHandler<RequestKoiEventArgs> RequestKoi;

        private void MutateRuntime()
        {
            IDarksVMSettings settings = this.rt.Descriptor.Settings;
            RuntimePatcher.Patch(this.RTModule, settings.ExportDbgInfo, settings.DoStackWalk);
            this.constants.InjectConstants(this.RTModule, this.rt.Descriptor, this.helpers);
            new Renamer(this.rt.Descriptor.Random.Next()).Process(this.RTModule);
        }

        private void ImportReferences(ModuleDef module)
        {
            var refCopy = this.rt.Descriptor.Data.refMap.ToList();
            this.rt.Descriptor.Data.refMap.Clear();
            foreach(KeyValuePair<IMemberRef, uint> mdRef in refCopy)
            {
                object item;
                if(mdRef.Key is ITypeDefOrRef)
                    item = module.Import((ITypeDefOrRef) mdRef.Key);
                else if(mdRef.Key is MemberRef)
                    item = module.Import((MemberRef) mdRef.Key);
                else if(mdRef.Key is MethodDef)
                    item = module.Import((MethodDef) mdRef.Key);
                else if(mdRef.Key is MethodSpec)
                    item = module.Import((MethodSpec) mdRef.Key);
                else if(mdRef.Key is FieldDef)
                    item = module.Import((FieldDef) mdRef.Key);
                else
                    item = mdRef.Key;
                this.rt.Descriptor.Data.refMap.Add((IMemberRef) item, mdRef.Value);
            }
            foreach(VM.DataDescriptor.FuncSigDesc sig in this.rt.Descriptor.Data.sigs)
            {
                MethodSig methodSig = sig.Signature;
                VM.FuncSig funcSig = sig.FuncSig;

                if(methodSig.HasThis)
                    funcSig.Flags |= this.rt.Descriptor.Runtime.RTFlags.INSTANCE;

                var paramTypes = new List<ITypeDefOrRef>();
                if(methodSig.HasThis && !methodSig.ExplicitThis)
                {
                    IType thisType;
                    if(sig.DeclaringType.IsValueType)
                        thisType = module.Import(new ByRefSig(sig.DeclaringType.ToTypeSig()).ToTypeDefOrRef());
                    else
                        thisType = module.Import(sig.DeclaringType);
                    paramTypes.Add((ITypeDefOrRef) thisType);
                }
                foreach(TypeSig param in methodSig.Params)
                {
                    var paramType = (ITypeDefOrRef) module.Import(param.ToTypeDefOrRef());
                    paramTypes.Add(paramType);
                }
                funcSig.ParamSigs = paramTypes.ToArray();

                var retType = (ITypeDefOrRef) module.Import(methodSig.RetType.ToTypeDefOrRef());
                funcSig.RetType = retType;
            }
        }

        private void MutateMetadata()
        {
            foreach(KeyValuePair<IMemberRef, uint> mdRef in this.rt.Descriptor.Data.refMap)
                mdRef.Key.Rid = this.rtMD.GetToken(mdRef.Key).Rid;

            foreach(VM.DataDescriptor.FuncSigDesc sig in this.rt.Descriptor.Data.sigs)
            {
                VM.FuncSig funcSig = sig.FuncSig;

                foreach(ITypeDefOrRef paramType in funcSig.ParamSigs)
                    paramType.Rid = this.rtMD.GetToken(paramType).Rid;

                funcSig.RetType.Rid = this.rtMD.GetToken(funcSig.RetType).Rid;
            }
        }
    }

    internal class RequestKoiEventArgs : EventArgs
    {
        public KoiHeap Heap
        {
            get;
            set;
        }
    }
}