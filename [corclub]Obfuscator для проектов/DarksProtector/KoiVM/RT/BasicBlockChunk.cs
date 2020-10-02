#region

using System.Diagnostics;
using System.IO;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using KoiVM.AST;
using KoiVM.AST.IL;
using KoiVM.VMIL;

#endregion

namespace KoiVM.RT
{
    internal class BasicBlockChunk : IKoiChunk
    {
        private readonly MethodDef method;
        private readonly DarksVMRuntime rt;

        public BasicBlockChunk(DarksVMRuntime rt, MethodDef method, ILBlock block)
        {
            this.rt = rt;
            this.method = method;
            this.Block = block;
            this.Length = rt.serializer.ComputeLength(block);
        }

        public ILBlock Block
        {
            get;
            set;
        }

        public uint Length
        {
            get;
            set;
        }

        public void OnOffsetComputed(uint offset)
        {
            uint len = this.rt.serializer.ComputeOffset(this.Block, offset);
            Debug.Assert(len - offset == this.Length);
        }

        public byte[] GetData()
        {
            var stream = new MemoryStream();
            this.rt.serializer.WriteData(this.Block, new BinaryWriter(stream));
            return this.Encrypt(stream.ToArray());
        }

        private byte[] Encrypt(byte[] data)
        {
            VM.VMBlockKey blockKey = this.rt.Descriptor.Data.LookupInfo(this.method).BlockKeys[this.Block];
            byte currentKey = blockKey.EntryKey;

            ILInstruction firstInstr = this.Block.Content[0];
            ILInstruction lastInstr = this.Block.Content[this.Block.Content.Count - 1];
            foreach(ILInstruction instr in this.Block.Content)
            {
                uint instrStart = instr.Offset - firstInstr.Offset;
                uint instrEnd = instrStart + this.rt.serializer.ComputeLength(instr);

                // Encrypt OpCode
                {
                    byte b = data[instrStart];
                    data[instrStart] ^= currentKey;
                    currentKey = (byte) (currentKey * 7 + b);
                }

                byte? fixupTarget = null;
                if(instr.Annotation == InstrAnnotation.JUMP ||
                   instr == lastInstr)
                {
                    fixupTarget = blockKey.ExitKey;
                }
                else if(instr.OpCode == ILOpCode.LEAVE)
                {
                    ExceptionHandler eh = ((EHInfo) instr.Annotation).ExceptionHandler;
                    if(eh.HandlerType == ExceptionHandlerType.Finally) fixupTarget = blockKey.ExitKey;
                }
                else if(instr.OpCode == ILOpCode.CALL)
                {
                    var callInfo = (InstrCallInfo) instr.Annotation;
                    VM.DarksVMMethodInfo info = this.rt.Descriptor.Data.LookupInfo((MethodDef) callInfo.Method);
                    fixupTarget = info.EntryKey;
                }

                if(fixupTarget != null)
                {
                    byte fixup = CalculateFixupByte(fixupTarget.Value, data, currentKey, instrStart + 1, instrEnd);
                    data[instrStart + 1] = fixup;
                }

                // Encrypt rest of instruction
                for(uint i = instrStart + 1; i < instrEnd; i++)
                {
                    byte b = data[i];
                    data[i] ^= currentKey;
                    currentKey = (byte) (currentKey * 7 + b);
                }
                if(fixupTarget != null)
                    Debug.Assert(currentKey == fixupTarget.Value);

                if(instr.OpCode == ILOpCode.CALL)
                {
                    var callInfo = (InstrCallInfo) instr.Annotation;
                    VM.DarksVMMethodInfo info = this.rt.Descriptor.Data.LookupInfo((MethodDef) callInfo.Method);
                    currentKey = info.ExitKey;
                }
            }

            return data;
        }

        private static byte CalculateFixupByte(byte target, byte[] data, uint currentKey, uint rangeStart, uint rangeEnd)
        {
            // Calculate fixup byte
            // f = k3 * 7 + d3
            // f = (k2 * 7 + d2) * 7 + d3
            // f = ((k1 * 7 + d1) * 7 + d2) * 7 + d3
            // f = (((k0 * 7 + d0) * 7 + d1) * 7 + d2) * 7 + d3
            // 7 ^ -1 (mod 256) = 183
            byte fixupByte = target;
            for(uint i = rangeEnd - 1; i > rangeStart; i--) fixupByte = (byte) ((fixupByte - data[i]) * 183);
            fixupByte -= (byte) (currentKey * 7);
            return fixupByte;
        }
    }
}