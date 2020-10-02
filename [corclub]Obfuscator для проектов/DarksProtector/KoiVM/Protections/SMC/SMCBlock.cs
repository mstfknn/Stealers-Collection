#region

using dnlib.DotNet;
using KoiVM.AST;
using KoiVM.AST.IL;
using KoiVM.RT;

#endregion

namespace KoiVM.Protections.SMC
{
    internal class SMCBlock : ILBlock
    {
        internal static readonly InstrAnnotation CounterInit = new InstrAnnotation("SMC_COUNTER");
        internal static readonly InstrAnnotation EncryptionKey = new InstrAnnotation("SMC_KEY");
        internal static readonly InstrAnnotation AddressPart1 = new InstrAnnotation("SMC_PART1");
        internal static readonly InstrAnnotation AddressPart2 = new InstrAnnotation("SMC_PART2");

        public SMCBlock(int id, ILInstrList content)
            : base(id, content)
        {
        }

        public byte Key
        {
            get;
            set;
        }

        public ILImmediate CounterOperand
        {
            get;
            set;
        }

        public override IKoiChunk CreateChunk(DarksVMRuntime rt, MethodDef method) => new SMCBlockChunk(rt, method, this);
    }

    internal class SMCBlockChunk : BasicBlockChunk, IKoiChunk
    {
        public SMCBlockChunk(DarksVMRuntime rt, MethodDef method, SMCBlock block)
            : base(rt, method, block) => block.CounterOperand.Value = this.Length + 1;

        uint IKoiChunk.Length => Length + 1;

        void IKoiChunk.OnOffsetComputed(uint offset) => OnOffsetComputed(offset + 1);

        byte[] IKoiChunk.GetData()
        {
            byte[] data = this.GetData();
            byte[] newData = new byte[data.Length + 1];
            byte key = ((SMCBlock)this.Block).Key;

            for(int i = 0; i < data.Length; i++)
                newData[i + 1] = (byte) (data[i] ^ key);
            newData[0] = key;
            return newData;
        }
    }

    internal class SMCBlockRef : ILRelReference
    {
        public SMCBlockRef(IHasOffset target, IHasOffset relBase, uint key)
            : base(target, relBase) => this.Key = key;

        public uint Key
        {
            get;
            set;
        }

        public override uint Resolve(DarksVMRuntime runtime) => base.Resolve(runtime) ^ this.Key;
    }
}