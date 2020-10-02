#region

using System.Collections.Generic;
using System.IO;
using dnlib.DotNet.Writer;

#endregion

namespace KoiVM.RT
{
    internal class KoiHeap : HeapBase
    {
        private readonly List<byte[]> chunks = new List<byte[]>();
        private uint currentLen;

        public override string Name => "#DarksVM";

        public uint AddChunk(byte[] chunk)
        {
            uint offset = this.currentLen;
            this.chunks.Add(chunk);
            this.currentLen += (uint) chunk.Length;
            return offset;
        }

        public override uint GetRawLength() => this.currentLen;

        protected override void WriteToImpl(BinaryWriter writer)
        {
            foreach(byte[] chunk in this.chunks)
                writer.Write(chunk);
        }
    }
}