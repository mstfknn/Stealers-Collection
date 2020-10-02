#region

using System;
using System.Collections.Generic;
using dnlib.DotNet;

#endregion

namespace KoiVM.VM
{
    public class DataDescriptor
    {
        private readonly Dictionary<MethodDef, uint> exportMap = new Dictionary<MethodDef, uint>();
        private readonly Dictionary<MethodDef, DarksVMMethodInfo> methodInfos = new Dictionary<MethodDef, DarksVMMethodInfo>();

        private uint nextRefId;
        private uint nextSigId;
        private uint nextStrId;
        private readonly Random random;

        internal Dictionary<IMemberRef, uint> refMap = new Dictionary<IMemberRef, uint>();
        private readonly Dictionary<MethodSig, uint> sigMap = new Dictionary<MethodSig, uint>(SignatureEqualityComparer.Instance);
        internal List<FuncSigDesc> sigs = new List<FuncSigDesc>();
        internal Dictionary<string, uint> strMap = new Dictionary<string, uint>(StringComparer.Ordinal);

        public DataDescriptor(Random random)
        {
            // 0 = null, 1 = ""
            this.strMap[""] = 1;
            this.nextStrId = 2;

            this.nextRefId = 1;
            this.nextSigId = 8u * 1;

            this.random = random;
        }

        public uint GetId(IMemberRef memberRef)
        {
            if (!this.refMap.TryGetValue(memberRef, out uint ret))
                this.refMap[memberRef] = ret = this.nextRefId++;
            return ret;
        }

        public void ReplaceReference(IMemberRef old, IMemberRef @new)
        {
            if (!this.refMap.TryGetValue(old, out uint id))
                return;
            this.refMap.Remove(old);
            this.refMap[@new] = id;
        }

        public uint GetId(string str)
        {
            if (!this.strMap.TryGetValue(str, out uint ret))
                this.strMap[str] = ret = this.nextStrId++;
            return ret;
        }

        public uint GetId(ITypeDefOrRef declType, MethodSig methodSig)
        {
            if (!this.sigMap.TryGetValue(methodSig, out uint ret))
            {
                uint id = this.nextSigId++;
                this.sigMap[methodSig] = ret = id;
                this.sigs.Add(new FuncSigDesc(id, declType, methodSig));
            }
            return ret;
        }

        public uint GetExportId(MethodDef method)
        {
            if (!this.exportMap.TryGetValue(method, out uint ret))
            {
                uint id = this.nextSigId++;
                this.exportMap[method] = ret = id;
                this.sigs.Add(new FuncSigDesc(id, method));
            }
            return ret;
        }

        public DarksVMMethodInfo LookupInfo(MethodDef method)
        {
            if (!this.methodInfos.TryGetValue(method, out DarksVMMethodInfo ret))
            {
                int k = this.random.Next();
                ret = new DarksVMMethodInfo
                {
                    EntryKey = (byte)k,
                    ExitKey = (byte)(k >> 8)
                };
                this.methodInfos[method] = ret;
            }
            return ret;
        }

        public void SetInfo(MethodDef method, DarksVMMethodInfo info) => this.methodInfos[method] = info;

        internal class FuncSigDesc
        {
            public readonly ITypeDefOrRef DeclaringType;
            public readonly FuncSig FuncSig;
            public readonly uint Id;
            public readonly MethodDef Method;
            public readonly MethodSig Signature;

            public FuncSigDesc(uint id, MethodDef method)
            {
                this.Id = id;
                this.Method = method;
                this.DeclaringType = method.DeclaringType;
                this.Signature = method.MethodSig;
                this.FuncSig = new FuncSig();
            }

            public FuncSigDesc(uint id, ITypeDefOrRef declType, MethodSig sig)
            {
                this.Id = id;
                this.Method = null;
                this.DeclaringType = declType;
                this.Signature = sig;
                this.FuncSig = new FuncSig();
            }
        }
    }
}