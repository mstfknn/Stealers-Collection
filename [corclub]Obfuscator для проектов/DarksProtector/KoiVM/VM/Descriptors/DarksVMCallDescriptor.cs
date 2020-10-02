#region

using System;
using System.Linq;

#endregion

namespace KoiVM.VM
{
    public class VMCallDescriptor
    {
        private readonly int[] callOrder = Enumerable.Range(0, 256).ToArray();

        public VMCallDescriptor(Random random) => random.Shuffle(this.callOrder);

        public int this[DarksVMCalls call] => this.callOrder[(int) call];

        public int EXIT => this.callOrder[0];

        public int BREAK => this.callOrder[1];

        public int ECALL => this.callOrder[2];

        public int CAST => this.callOrder[3];

        public int CKFINITE => this.callOrder[4];

        public int CKOVERFLOW => this.callOrder[5];

        public int RANGECHK => this.callOrder[6];

        public int INITOBJ => this.callOrder[7];

        public int LDFLD => this.callOrder[8];

        public int LDFTN => this.callOrder[9];

        public int TOKEN => this.callOrder[10];

        public int THROW => this.callOrder[11];

        public int SIZEOF => this.callOrder[12];

        public int STFLD => this.callOrder[13];

        public int BOX => this.callOrder[14];

        public int UNBOX => this.callOrder[15];

        public int LOCALLOC => this.callOrder[16];
    }
}