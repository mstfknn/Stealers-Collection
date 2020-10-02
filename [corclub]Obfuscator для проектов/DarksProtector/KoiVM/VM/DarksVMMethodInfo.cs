#region

using System.Collections.Generic;
using KoiVM.CFG;

#endregion

namespace KoiVM.VM
{
    public class DarksVMMethodInfo
    {
        public readonly Dictionary<IBasicBlock, VMBlockKey> BlockKeys;
        public readonly HashSet<DarksVMRegisters> UsedRegister;
        public byte EntryKey, ExitKey;

        public ScopeBlock RootScope;

        public DarksVMMethodInfo()
        {
            this.BlockKeys = new Dictionary<IBasicBlock, VMBlockKey>();
            this.UsedRegister = new HashSet<DarksVMRegisters>();
        }
    }

    public struct VMBlockKey
    {
        public byte EntryKey, ExitKey;
    }
}