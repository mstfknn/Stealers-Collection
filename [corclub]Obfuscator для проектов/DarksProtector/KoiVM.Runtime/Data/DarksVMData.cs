#region

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using What_a_great_VM;

#endregion

namespace KoiVM.Runtime.Data
{
    internal unsafe class DarksVMData
    {
        private static readonly Dictionary<Module, DarksVMData> moduleVMData = new Dictionary<Module, DarksVMData>();
        private readonly Dictionary<uint, DarksVMExportInfo> exports;

        private readonly Dictionary<uint, RefInfo> references;
        private readonly Dictionary<uint, string> strings;

        public DarksVMData(Module module, void* data)
        {
            var header = (VMDAT_HEADER*) data;
            if(header->MAGIC != 0x68736966)
                throw new InvalidProgramException();

            this.references = new Dictionary<uint, RefInfo>();
            this.strings = new Dictionary<uint, string>();
            this.exports = new Dictionary<uint, DarksVMExportInfo>();

            byte* ptr = (byte*) (header + 1);
            for(int i = 0; i < header->MD_COUNT; i++)
            {
                uint id = Utils.ReadCompressedUInt(ref ptr);
                int token = (int) Utils.FromCodedToken(Utils.ReadCompressedUInt(ref ptr));
                this.references[id] = new RefInfo
                {
                    module = module,
                    token = token
                };
            }
            for(int i = 0; i < header->STR_COUNT; i++)
            {
                uint id = Utils.ReadCompressedUInt(ref ptr);
                uint len = Utils.ReadCompressedUInt(ref ptr);
                this.strings[id] = new string((char*) ptr, 0, (int) len);
                ptr += len << 1;
            }
            for(int i = 0; i < header->EXP_COUNT; i++) this.exports[Utils.ReadCompressedUInt(ref ptr)] = new DarksVMExportInfo(ref ptr, module);

            this.KoiSection = (byte*) data;

            this.Module = module;
            moduleVMData[module] = this;
        }

        public Module Module
        {
            get;
        }

        public byte* KoiSection
        {
            get;
            set;
        }

        public static DarksVMData Instance(Module module)
        {
            DarksVMData data;
            lock(moduleVMData)
            {
                if(!moduleVMData.TryGetValue(module, out data))
                    data = moduleVMData[module] = DarksVMDataInitializer.GetData(module);
            }
            return data;
        }

        public MemberInfo LookupReference(uint id) => this.references[id].Member;

        public string LookupString(uint id) => id == 0 ? null : this.strings[id];

        public DarksVMExportInfo LookupExport(uint id) => this.exports[id];

        [StructLayout(LayoutKind.Sequential)]
        private struct VMDAT_HEADER
        {
            public readonly uint MAGIC, MD_COUNT, STR_COUNT, EXP_COUNT;
        }

        private class RefInfo
        {
            public Module module;
            public MemberInfo resolved;
            public int token;

            public MemberInfo Member => this.resolved ?? (this.resolved = this.module.ResolveMember(this.token));
        }
    }
}