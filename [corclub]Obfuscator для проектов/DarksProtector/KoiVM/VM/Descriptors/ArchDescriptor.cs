#region

using System;

#endregion

namespace KoiVM.VM
{
    public class ArchDescriptor
    {
        public ArchDescriptor(Random random)
        {
            this.OpCodes = new OpCodeDescriptor(random);
            this.Flags = new FlagDescriptor(random);
            this.Registers = new RegisterDescriptor(random);
        }

        public OpCodeDescriptor OpCodes
        {
            get;
        }

        public FlagDescriptor Flags
        {
            get;
        }

        public RegisterDescriptor Registers
        {
            get;
        }
    }
}