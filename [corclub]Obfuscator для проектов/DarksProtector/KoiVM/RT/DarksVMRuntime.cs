#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using dnlib.DotNet;
using dnlib.DotNet.Writer;
using KoiVM.AST;
using KoiVM.AST.IL;
using KoiVM.CFG;
using KoiVM.RT.Mutation;
using KoiVM.VM;

#endregion

namespace KoiVM.RT
{
    public class DarksVMRuntime
    {
        private List<Tuple<MethodDef, ILBlock>> basicBlocks;

        internal DbgWriter dbgWriter;

        private List<IKoiChunk> extraChunks;
        private List<IKoiChunk> finalChunks;
        internal Dictionary<MethodDef, Tuple<ScopeBlock, ILBlock>> methodMap;

        private RuntimeMutator rtMutator;
        internal BasicBlockSerializer serializer;
        private readonly IDarksVMSettings settings;

        public DarksVMRuntime(IDarksVMSettings settings, ModuleDef rt)
        {
            this.settings = settings;
            this.Init(rt);
        }

        public ModuleDef Module => this.rtMutator.RTModule;

        public VMDescriptor Descriptor
        {
            get;
            private set;
        }

        public byte[] RuntimeLibrary
        {
            get;
            private set;
        }

        public byte[] RuntimeSymbols
        {
            get;
            private set;
        }

        public byte[] DebugInfo => this.dbgWriter.GetDbgInfo();

        [Obfuscation(Exclude = false, Feature = "+koi;-ref proxy")]
        private void Init(ModuleDef rt)
        {
            this.Descriptor = new VMDescriptor(this.settings);
            this.methodMap = new Dictionary<MethodDef, Tuple<ScopeBlock, ILBlock>>();
            this.basicBlocks = new List<Tuple<MethodDef, ILBlock>>();

            this.extraChunks = new List<IKoiChunk>();
            this.finalChunks = new List<IKoiChunk>();
            this.serializer = new BasicBlockSerializer(this);

            this.rtMutator = new RuntimeMutator(rt, this);
            this.rtMutator.RequestKoi += this.OnKoiRequested;
        }

        public void AddMethod(MethodDef method, ScopeBlock rootScope)
        {
            ILBlock entry = null;
            foreach(ILBlock block in rootScope.GetBasicBlocks())
            {
                if(block.Id == 0)
                    entry = block;
                this.basicBlocks.Add(Tuple.Create(method, block));
            }
            Debug.Assert(entry != null);
            this.methodMap[method] = Tuple.Create(rootScope, entry);
        }

        internal void AddHelper(MethodDef method, ScopeBlock rootScope, ILBlock entry) => this.methodMap[method] = Tuple.Create(rootScope, entry);

        public void AddBlock(MethodDef method, ILBlock block) => this.basicBlocks.Add(Tuple.Create(method, block));

        public ScopeBlock LookupMethod(MethodDef method)
        {
            Tuple<ScopeBlock, ILBlock> m = this.methodMap[method];
            return m.Item1;
        }

        public ScopeBlock LookupMethod(MethodDef method, out ILBlock entry)
        {
            Tuple<ScopeBlock, ILBlock> m = this.methodMap[method];
            entry = m.Item2;
            return m.Item1;
        }

        public void AddChunk(IKoiChunk chunk) => this.extraChunks.Add(chunk);

        public void ExportMethod(MethodDef method) => this.rtMutator.ReplaceMethodStub(method);

        public IModuleWriterListener CommitModule(ModuleDefMD module) => this.rtMutator.CommitModule(module);

        public void CommitRuntime(ModuleDef targetModule = null)
        {
            this.rtMutator.CommitRuntime(targetModule);
            this.RuntimeLibrary = this.rtMutator.RuntimeLib;
            this.RuntimeSymbols = this.rtMutator.RuntimeSym;
        }

        [Obfuscation(Exclude = false, Feature = "+koi;-ref proxy")]
        private void OnKoiRequested(object sender, RequestKoiEventArgs e)
        {
            var header = new HeaderChunk(this);

            foreach(Tuple<MethodDef, ILBlock> block in this.basicBlocks) this.finalChunks.Add(block.Item2.CreateChunk(this, block.Item1));
            this.finalChunks.AddRange(this.extraChunks);
            //finalChunks.Add(new BinaryChunk(Watermark.GenerateWatermark((uint) settings.Seed)));
            this.Descriptor.Random.Shuffle(this.finalChunks);
            this.finalChunks.Insert(0, header);

            this.ComputeOffsets();
            this.FixupReferences();
            header.WriteData(this);
            e.Heap = this.CreateHeap();
        }

        private void ComputeOffsets()
        {
            uint offset = 0;
            foreach(IKoiChunk chunk in this.finalChunks)
            {
                chunk.OnOffsetComputed(offset);
                offset += chunk.Length;
            }
        }

        private void FixupReferences()
        {
            foreach(Tuple<MethodDef, ILBlock> block in this.basicBlocks)
            foreach(ILInstruction instr in block.Item2.Content)
                    if (instr.Operand is ILRelReference reference)
                    {
                        instr.Operand = ILImmediate.Create(reference.Resolve(this), ASTType.I4);
                    }
        }

        private KoiHeap CreateHeap()
        {
            if(this.settings.ExportDbgInfo)
                this.dbgWriter = new DbgWriter();

            var heap = new KoiHeap();
            foreach(IKoiChunk chunk in this.finalChunks) heap.AddChunk(chunk.GetData());
            if(this.dbgWriter != null)
                using(DbgWriter.DbgSerializer serializer = this.dbgWriter.GetSerializer())
                {
                    foreach(IKoiChunk chunk in this.finalChunks)
                        serializer.WriteBlock(chunk as BasicBlockChunk);
                }
            return heap;
        }

        public void ResetData()
        {
            this.methodMap = new Dictionary<MethodDef, Tuple<ScopeBlock, ILBlock>>();
            this.basicBlocks = new List<Tuple<MethodDef, ILBlock>>();

            this.extraChunks = new List<IKoiChunk>();
            this.finalChunks = new List<IKoiChunk>();
            this.Descriptor.ResetData();

            this.rtMutator.InitHelpers();
        }
    }
}