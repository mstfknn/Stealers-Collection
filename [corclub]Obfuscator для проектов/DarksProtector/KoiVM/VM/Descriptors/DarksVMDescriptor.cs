#region

using System;

#endregion

namespace KoiVM.VM
{
    public class VMDescriptor
    {
        public VMDescriptor(IDarksVMSettings settings)
        {
            this.Random = new Random(settings.Seed);
            this.Settings = settings;
            this.Architecture = new ArchDescriptor(this.Random);
            this.Runtime = new RuntimeDescriptor(this.Random);
            this.Data = new DataDescriptor(this.Random);
        }

        public Random Random
        {
            get;
        }

        public IDarksVMSettings Settings
        {
            get;
        }

        public ArchDescriptor Architecture
        {
            get;
        }

        public RuntimeDescriptor Runtime
        {
            get;
        }

        public DataDescriptor Data
        {
            get;
            private set;
        }

        public void ResetData() => this.Data = new DataDescriptor(this.Random);
    }
}