#region

using System;
using System.Linq;

#endregion

namespace KoiVM.VM
{
    public class RTFlagDescriptor
    {
        private readonly byte[] ehOrder = Enumerable.Range(0, 4).Select(x => (byte) x).ToArray();
        private readonly byte[] flagOrder = Enumerable.Range(1, 7).Select(x => (byte) x).ToArray();

        public RTFlagDescriptor(Random random)
        {
            random.Shuffle(this.flagOrder);
            random.Shuffle(this.ehOrder);
        }

        public byte INSTANCE => this.flagOrder[0];

        public byte EH_CATCH => this.ehOrder[0];

        public byte EH_FILTER => this.ehOrder[1];

        public byte EH_FAULT => this.ehOrder[2];

        public byte EH_FINALLY => this.ehOrder[3];
    }
}