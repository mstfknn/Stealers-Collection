#region

using System;
using KoiVM.Runtime.Execution.Internal;

#endregion

namespace KoiVM.Runtime.Execution
{
    internal class StackRef : IReference
    {
        public StackRef(uint pos)
        {
            this.StackPos = pos;
        }

        public uint StackPos
        {
            get;
            set;
        }

        public DarksVMSlot GetValue(DarksVMContext ctx, PointerType type)
        {
            DarksVMSlot slot = ctx.Stack[this.StackPos];
            if(type == PointerType.BYTE)
                slot.U8 = slot.U1;
            else if(type == PointerType.WORD)
                slot.U8 = slot.U2;
            else if(type == PointerType.DWORD)
                slot.U8 = slot.U4;
            else if(slot.O is IValueTypeBox) slot.O = ((IValueTypeBox) slot.O).Clone();
            return slot;
        }

        public void SetValue(DarksVMContext ctx, DarksVMSlot slot, PointerType type)
        {
            if(type == PointerType.BYTE)
                slot.U8 = slot.U1;
            else if(type == PointerType.WORD)
                slot.U8 = slot.U2;
            else if(type == PointerType.DWORD)
                slot.U8 = slot.U4;
            ctx.Stack[this.StackPos] = slot;
        }

        public IReference Add(uint value)
        {
            return new StackRef(this.StackPos + value);
        }

        public IReference Add(ulong value)
        {
            return new StackRef(this.StackPos + (uint) (long) value);
        }

        public void ToTypedReference(DarksVMContext ctx, TypedRefPtr typedRef, Type type)
        {
            ctx.Stack.ToTypedReference(this.StackPos, typedRef, type);
        }
    }
}