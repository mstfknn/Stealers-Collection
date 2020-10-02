#region

using System;

#endregion

namespace KoiVM.Runtime.Execution
{
    internal struct EHFrame
    {
        public byte EHType;
        public ulong FilterAddr, HandlerAddr;
        public Type CatchType;

        public DarksVMSlot BP, SP;
    }
}