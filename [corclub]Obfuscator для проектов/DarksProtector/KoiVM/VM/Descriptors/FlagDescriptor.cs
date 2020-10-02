#region

using System;
using System.Linq;

#endregion

namespace KoiVM.VM
{
    public class FlagDescriptor
    {
        private readonly int[] flagOrder = Enumerable.Range(0, (int)DarksVMFlags.Max).ToArray();

        public FlagDescriptor(Random random) => random.Shuffle(this.flagOrder);

        public int this[DarksVMFlags flag] => this.flagOrder[(int) flag];

        public int OVERFLOW => this.flagOrder[0];

        public int CARRY => this.flagOrder[1];

        public int ZERO => this.flagOrder[2];

        public int SIGN => this.flagOrder[3];

        public int UNSIGNED => this.flagOrder[4];

        public int BEHAV1 => this.flagOrder[5];

        public int BEHAV2 => this.flagOrder[6];

        public int BEHAV3 => this.flagOrder[7];
    }
}