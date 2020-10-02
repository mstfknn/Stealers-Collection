#region

using System;
using What_a_great_VM;
using KoiVM.Runtime.Dynamic;
using KoiVM.Runtime.Execution;

#endregion

namespace KoiVM.Runtime.OpCodes
{
    internal class SindByte : IOpCode
    {
        public byte Code => DarksVMConstants.OP_SIND_BYTE;

        public unsafe void Load(DarksVMContext ctx, out ExecutionState state)
        {
            uint sp = ctx.Registers[DarksVMConstants.REG_SP].U4;
            DarksVMSlot adrSlot = ctx.Stack[sp--];
            DarksVMSlot valSlot = ctx.Stack[sp--];
            ctx.Stack.SetTopPosition(sp);
            ctx.Registers[DarksVMConstants.REG_SP].U4 = sp;

            if(adrSlot.O is IReference)
            {
                ((IReference) adrSlot.O).SetValue(ctx, valSlot, PointerType.BYTE);
            }
            else
            {
                byte value = valSlot.U1;
                byte* ptr = (byte*) adrSlot.U8;
                *ptr = value;
            }
            state = ExecutionState.Next;
        }
    }

    internal class SindWord : IOpCode
    {
        public byte Code => DarksVMConstants.OP_SIND_WORD;

        public unsafe void Load(DarksVMContext ctx, out ExecutionState state)
        {
            uint sp = ctx.Registers[DarksVMConstants.REG_SP].U4;
            DarksVMSlot adrSlot = ctx.Stack[sp--];
            DarksVMSlot valSlot = ctx.Stack[sp--];
            ctx.Stack.SetTopPosition(sp);
            ctx.Registers[DarksVMConstants.REG_SP].U4 = sp;

            if(adrSlot.O is IReference)
            {
                ((IReference) adrSlot.O).SetValue(ctx, valSlot, PointerType.WORD);
            }
            else
            {
                ushort value = valSlot.U2;
                ushort* ptr = (ushort*) adrSlot.U8;
                *ptr = value;
            }
            state = ExecutionState.Next;
        }
    }

    internal class SindDword : IOpCode
    {
        public byte Code => DarksVMConstants.OP_SIND_DWORD;

        public unsafe void Load(DarksVMContext ctx, out ExecutionState state)
        {
            uint sp = ctx.Registers[DarksVMConstants.REG_SP].U4;
            DarksVMSlot adrSlot = ctx.Stack[sp--];
            DarksVMSlot valSlot = ctx.Stack[sp--];
            ctx.Stack.SetTopPosition(sp);
            ctx.Registers[DarksVMConstants.REG_SP].U4 = sp;

            if(adrSlot.O is IReference)
            {
                ((IReference) adrSlot.O).SetValue(ctx, valSlot, PointerType.DWORD);
            }
            else
            {
                uint value = valSlot.U4;
                uint* ptr = (uint*) adrSlot.U8;
                *ptr = value;
            }
            state = ExecutionState.Next;
        }
    }

    internal class SindQword : IOpCode
    {
        public byte Code => DarksVMConstants.OP_SIND_QWORD;

        public unsafe void Load(DarksVMContext ctx, out ExecutionState state)
        {
            uint sp = ctx.Registers[DarksVMConstants.REG_SP].U4;
            DarksVMSlot adrSlot = ctx.Stack[sp--];
            DarksVMSlot valSlot = ctx.Stack[sp--];
            ctx.Stack.SetTopPosition(sp);
            ctx.Registers[DarksVMConstants.REG_SP].U4 = sp;

            if(adrSlot.O is IReference)
            {
                ((IReference) adrSlot.O).SetValue(ctx, valSlot, PointerType.QWORD);
            }
            else
            {
                ulong value = valSlot.U8;
                ulong* ptr = (ulong*) adrSlot.U8;
                *ptr = value;
            }
            state = ExecutionState.Next;
        }
    }

    internal class SindObject : IOpCode
    {
        public byte Code => DarksVMConstants.OP_SIND_OBJECT;

        public void Load(DarksVMContext ctx, out ExecutionState state)
        {
            uint sp = ctx.Registers[DarksVMConstants.REG_SP].U4;
            DarksVMSlot adrSlot = ctx.Stack[sp--];
            DarksVMSlot valSlot = ctx.Stack[sp--];
            ctx.Stack.SetTopPosition(sp);
            ctx.Registers[DarksVMConstants.REG_SP].U4 = sp;

            if(adrSlot.O is IReference) ((IReference) adrSlot.O).SetValue(ctx, valSlot, PointerType.OBJECT);
            else throw new ExecutionEngineException();
            state = ExecutionState.Next;
        }
    }

    internal class SindPtr : IOpCode
    {
        public byte Code => DarksVMConstants.OP_SIND_PTR;

        public unsafe void Load(DarksVMContext ctx, out ExecutionState state)
        {
            uint sp = ctx.Registers[DarksVMConstants.REG_SP].U4;
            DarksVMSlot adrSlot = ctx.Stack[sp--];
            DarksVMSlot valSlot = ctx.Stack[sp--];
            ctx.Stack.SetTopPosition(sp);
            ctx.Registers[DarksVMConstants.REG_SP].U4 = sp;

            if(adrSlot.O is IReference)
            {
                ((IReference) adrSlot.O).SetValue(ctx, valSlot, Platform.x64 ? PointerType.QWORD : PointerType.DWORD);
            }
            else
            {
                if(Platform.x64)
                {
                    ulong* ptr = (ulong*) adrSlot.U8;
                    *ptr = valSlot.U8;
                }
                else
                {
                    uint* ptr = (uint*) adrSlot.U8;
                    *ptr = valSlot.U4;
                }
            }
            state = ExecutionState.Next;
        }
    }
}