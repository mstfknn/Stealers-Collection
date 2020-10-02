#region

using System;

#endregion

namespace KoiVM.VM
{
    public class RuntimeDescriptor
    {
        public RuntimeDescriptor(Random random)
        {
            this.VMCall = new VMCallDescriptor(random);
            this.VCallOps = new VCallOpsDescriptor(random);
            this.RTFlags = new RTFlagDescriptor(random);
        }

        public VMCallDescriptor VMCall
        {
            get;
        }

        public VCallOpsDescriptor VCallOps
        {
            get;
        }

        public RTFlagDescriptor RTFlags
        {
            get;
        }
    }
}