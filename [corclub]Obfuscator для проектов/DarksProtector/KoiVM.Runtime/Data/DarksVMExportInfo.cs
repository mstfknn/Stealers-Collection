#region

using System.Reflection;

#endregion

namespace KoiVM.Runtime.Data
{
    internal struct DarksVMExportInfo
    {
        public unsafe DarksVMExportInfo(ref byte* ptr, Module module)
        {
            this.CodeOffset = *(uint*) ptr;
            ptr += 4;
            if(this.CodeOffset != 0)
            {
                this.EntryKey = *(uint*) ptr;
                ptr += 4;
            }
            else
            {
                this.EntryKey = 0;
            }
            this.Signature = new DarksVMFuncSig(ref ptr, module);
        }

        public readonly uint CodeOffset;
        public readonly uint EntryKey;
        public readonly DarksVMFuncSig Signature;
    }
}