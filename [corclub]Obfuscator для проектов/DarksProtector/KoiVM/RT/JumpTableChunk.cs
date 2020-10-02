#region

using System;
using KoiVM.AST.IL;

#endregion

namespace KoiVM.RT
{
    public class JumpTableChunk : IKoiChunk
    {
        internal DarksVMRuntime runtime;

        public JumpTableChunk(ILJumpTable table)
        {
            this.Table = table;
            if(table.Targets.Length > ushort.MaxValue)
                throw new NotSupportedException("Jump table too large.");
        }

        public ILJumpTable Table
        {
            get;
        }

        public uint Offset
        {
            get;
            private set;
        }

        uint IKoiChunk.Length => (uint)this.Table.Targets.Length * 4 + 2;

        void IKoiChunk.OnOffsetComputed(uint offset) => this.Offset = offset + 2;

        byte[] IKoiChunk.GetData()
        {
            byte[] data = new byte[this.Table.Targets.Length * 4 + 2];
            ushort len = (ushort)this.Table.Targets.Length;
            int ptr = 0;
            data[ptr++] = (byte)this.Table.Targets.Length;
            data[ptr++] = (byte) (this.Table.Targets.Length >> 8);

            uint relBase = this.Table.RelativeBase.Offset;
            relBase += this.runtime.serializer.ComputeLength(this.Table.RelativeBase);
            for(int i = 0; i < this.Table.Targets.Length; i++)
            {
                uint offset = ((ILBlock)this.Table.Targets[i]).Content[0].Offset;
                offset -= relBase;
                data[ptr++] = (byte) (offset >> 0);
                data[ptr++] = (byte) (offset >> 8);
                data[ptr++] = (byte) (offset >> 16);
                data[ptr++] = (byte) (offset >> 24);
            }
            return data;
        }
    }
}