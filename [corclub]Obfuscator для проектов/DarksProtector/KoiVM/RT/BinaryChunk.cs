#region

using System;

#endregion

namespace KoiVM.RT
{
    public class BinaryChunk : IKoiChunk
    {
        public EventHandler<OffsetComputeEventArgs> OffsetComputed;

        public BinaryChunk(byte[] data) => this.Data = data;

        public byte[] Data
        {
            get;
        }

        public uint Offset
        {
            get;
            private set;
        }

        uint IKoiChunk.Length => (uint)this.Data.Length;

        void IKoiChunk.OnOffsetComputed(uint offset)
        {
            if(this.OffsetComputed != null)
                this.OffsetComputed(this, new OffsetComputeEventArgs(offset));
            this.Offset = offset;
        }

        byte[] IKoiChunk.GetData() => this.Data;
    }

    public class OffsetComputeEventArgs : EventArgs
    {
        internal OffsetComputeEventArgs(uint offset) => this.Offset = offset;

        public uint Offset
        {
            get;
        }
    }
}