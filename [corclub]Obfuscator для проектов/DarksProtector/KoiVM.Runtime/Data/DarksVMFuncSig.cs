#region

using What_a_great_VM;
using System;
using System.Reflection;

#endregion

namespace KoiVM.Runtime.Data
{
    internal class DarksVMFuncSig
    {
        private readonly int[] paramToks;
        private readonly int retTok;

        public byte Flags;

        private readonly Module module;
        private Type[] paramTypes;
        private Type retType;

        public unsafe DarksVMFuncSig(ref byte* ptr, Module module)
        {
            this.module = module;

            this.Flags = *ptr++;
            this.paramToks = new int[Utils.ReadCompressedUInt(ref ptr)];
            for(int i = 0; i < this.paramToks.Length; i++) this.paramToks[i] = (int) Utils.FromCodedToken(Utils.ReadCompressedUInt(ref ptr));
            this.retTok = (int) Utils.FromCodedToken(Utils.ReadCompressedUInt(ref ptr));
        }

        public Type[] ParamTypes
        {
            get
            {
                if(this.paramTypes != null)
                    return this.paramTypes;

                var p = new Type[this.paramToks.Length];
                for(int i = 0; i < p.Length; i++) p[i] = this.module.ResolveType(this.paramToks[i]);
                this.paramTypes = p;
                return p;
            }
        }

        public Type RetType => this.retType ?? (this.retType = this.module.ResolveType(this.retTok));
    }
}