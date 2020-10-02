#region

using System;
using System.Diagnostics;
using System.IO;
using dnlib.DotNet;
using dnlib.DotNet.MD;

#endregion

namespace KoiVM.RT
{
    internal class HeaderChunk : IKoiChunk
    {
        private byte[] data;

        public HeaderChunk(DarksVMRuntime rt) => this.Length = this.ComputeLength(rt);

        public uint Length
        {
            get;
            set;
        }

        public void OnOffsetComputed(uint offset)
        {
        }

        public byte[] GetData() => this.data;

        private uint GetCodedLen(MDToken token)
        {
            switch(token.Table)
            {
                case Table.TypeDef:
                case Table.TypeRef:
                case Table.TypeSpec:
                case Table.MemberRef:
                case Table.Method:
                case Table.Field:
                case Table.MethodSpec:
                    return Utils.GetCompressedUIntLength(token.Rid << 3);
                default:
                    throw new NotSupportedException();
            }
        }

        private uint GetCodedToken(MDToken token)
        {
            switch(token.Table)
            {
                case Table.TypeDef:
                    return (token.Rid << 3) | 1;
                case Table.TypeRef:
                    return (token.Rid << 3) | 2;
                case Table.TypeSpec:
                    return (token.Rid << 3) | 3;
                case Table.MemberRef:
                    return (token.Rid << 3) | 4;
                case Table.Method:
                    return (token.Rid << 3) | 5;
                case Table.Field:
                    return (token.Rid << 3) | 6;
                case Table.MethodSpec:
                    return (token.Rid << 3) | 7;
                default:
                    throw new NotSupportedException();
            }
        }

        private uint ComputeLength(DarksVMRuntime rt)
        {
            uint len = 16;
            foreach(System.Collections.Generic.KeyValuePair<IMemberRef, uint> reference in rt.Descriptor.Data.refMap) len += Utils.GetCompressedUIntLength(reference.Value) + this.GetCodedLen(reference.Key.MDToken);
            foreach(System.Collections.Generic.KeyValuePair<string, uint> str in rt.Descriptor.Data.strMap)
            {
                len += Utils.GetCompressedUIntLength(str.Value);
                len += Utils.GetCompressedUIntLength((uint) str.Key.Length);
                len += (uint) str.Key.Length * 2;
            }
            foreach(VM.DataDescriptor.FuncSigDesc sig in rt.Descriptor.Data.sigs)
            {
                len += Utils.GetCompressedUIntLength(sig.Id);
                len += 4;
                if(sig.Method != null)
                    len += 4;
                uint paramCount = (uint) sig.FuncSig.ParamSigs.Length;
                len += 1 + Utils.GetCompressedUIntLength(paramCount);

                foreach(ITypeDefOrRef param in sig.FuncSig.ParamSigs)
                    len += this.GetCodedLen(param.MDToken);
                len += this.GetCodedLen(sig.FuncSig.RetType.MDToken);
            }
            return len;
        }

        internal void WriteData(DarksVMRuntime rt)
        {
            var stream = new MemoryStream();
            var writer = new BinaryWriter(stream);

            writer.Write((uint) 0x68736966);
            writer.Write(rt.Descriptor.Data.refMap.Count);
            writer.Write(rt.Descriptor.Data.strMap.Count);
            writer.Write(rt.Descriptor.Data.sigs.Count);

            foreach(System.Collections.Generic.KeyValuePair<IMemberRef, uint> refer in rt.Descriptor.Data.refMap)
            {
                writer.WriteCompressedUInt(refer.Value);
                writer.WriteCompressedUInt(this.GetCodedToken(refer.Key.MDToken));
            }

            foreach(System.Collections.Generic.KeyValuePair<string, uint> str in rt.Descriptor.Data.strMap)
            {
                writer.WriteCompressedUInt(str.Value);
                writer.WriteCompressedUInt((uint) str.Key.Length);
                foreach(char chr in str.Key)
                    writer.Write((ushort) chr);
            }

            foreach(VM.DataDescriptor.FuncSigDesc sig in rt.Descriptor.Data.sigs)
            {
                writer.WriteCompressedUInt(sig.Id);
                if(sig.Method != null)
                {
                    AST.IL.ILBlock entry = rt.methodMap[sig.Method].Item2;
                    uint entryOffset = entry.Content[0].Offset;
                    Debug.Assert(entryOffset != 0);
                    writer.Write(entryOffset);

                    uint key = (uint) rt.Descriptor.Random.Next();
                    key = (key << 8) | rt.Descriptor.Data.LookupInfo(sig.Method).EntryKey;
                    writer.Write(key);
                }
                else
                {
                    writer.Write(0u);
                }

                writer.Write(sig.FuncSig.Flags);
                writer.WriteCompressedUInt((uint) sig.FuncSig.ParamSigs.Length);
                foreach(ITypeDefOrRef paramType in sig.FuncSig.ParamSigs) writer.WriteCompressedUInt(this.GetCodedToken(paramType.MDToken));
                writer.WriteCompressedUInt(this.GetCodedToken(sig.FuncSig.RetType.MDToken));
            }

            this.data = stream.ToArray();
            Debug.Assert(this.data.Length == this.Length);
        }
    }
}