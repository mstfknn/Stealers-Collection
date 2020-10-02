#region

using System.Collections.Generic;
using What_a_great_VM;
using KoiVM.Runtime.Dynamic;

#endregion

namespace KoiVM.Runtime.Execution
{
    internal class DarksVMContext
    {
        private const int NumRegisters = 16;
        public readonly List<EHFrame> EHStack = new List<EHFrame>();
        public readonly List<EHState> EHStates = new List<EHState>();
        public readonly DarksVMInstance Instance;

        public readonly DarksVMSlot[] Registers = new DarksVMSlot[16];
        public readonly DarksVMStack Stack = new DarksVMStack();

        public DarksVMContext(DarksVMInstance inst)
        {
            this.Instance = inst;
        }

        public unsafe byte ReadByte()
        {
            uint key = this.Registers[DarksVMConstants.REG_K1].U4;
            byte* ip = (byte*)this.Registers[DarksVMConstants.REG_IP].U8++;
            byte b = (byte) (*ip ^ key);
            key = key * 7 + b;
            this.Registers[DarksVMConstants.REG_K1].U4 = key;
            return b;
        }
    }
}