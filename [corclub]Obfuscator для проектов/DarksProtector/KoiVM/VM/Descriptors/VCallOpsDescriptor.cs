#region

using System;

#endregion

namespace KoiVM.VM
{
    public class VCallOpsDescriptor
    {
        private readonly uint[] ecallOrder = {0, 1, 2, 3};

        public VCallOpsDescriptor(Random random) => random.Shuffle(this.ecallOrder);

        public uint ECALL_CALL => this.ecallOrder[0];

        public uint ECALL_CALLVIRT => this.ecallOrder[1];

        public uint ECALL_NEWOBJ => this.ecallOrder[2];

        public uint ECALL_CALLVIRT_CONSTRAINED => this.ecallOrder[3];
    }
}