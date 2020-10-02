#region

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using KoiVM.Runtime.Execution.Internal;

#endregion

namespace KoiVM.Runtime.Execution
{
    internal class DarksVMStack
    {
        private const int SectionSize = 6; // 1 << 6 = 64
        private const int IndexMask = (1 << SectionSize) - 1;
        private LocallocNode localPool;

        private readonly List<DarksVMSlot[]> sections = new List<DarksVMSlot[]>();
        private uint topPos;

        public DarksVMSlot this[uint pos]
        {
            get
            {
                if(pos > this.topPos)
                    return DarksVMSlot.Null;
                uint sectionIndex = pos >> SectionSize;
                return this.sections[(int) sectionIndex][pos & IndexMask];
            }
            set
            {
                if(pos > this.topPos)
                    return;
                uint sectionIndex = pos >> SectionSize;
                this.sections[(int) sectionIndex][pos & IndexMask] = value;
            }
        }

        public void SetTopPosition(uint topPos)
        {
            if(topPos > 0x7fffffff)
                throw new StackOverflowException();

            uint sectionIndex = topPos >> SectionSize;
            if(sectionIndex >= this.sections.Count)
                do
                {
                    this.sections.Add(new DarksVMSlot[1 << SectionSize]);
                } while(sectionIndex >= this.sections.Count);
            else if(sectionIndex < this.sections.Count - 2)
                do
                {
                    this.sections.RemoveAt(this.sections.Count - 1);
                } while(sectionIndex < this.sections.Count - 2);

            // Clear stack object references
            uint stackIndex = (topPos & IndexMask) + 1;
            DarksVMSlot[] section = this.sections[(int) sectionIndex];
            while(stackIndex < section.Length && section[stackIndex].O != null)
                section[stackIndex++] = DarksVMSlot.Null;
            if(stackIndex == section.Length && sectionIndex + 1 < this.sections.Count)
            {
                stackIndex = 0;
                section = this.sections[(int) sectionIndex + 1];
                while(stackIndex < section.Length && section[stackIndex].O != null)
                    section[stackIndex++] = DarksVMSlot.Null;
            }
            this.topPos = topPos;

            this.CheckFreeLocalloc();
        }

        private void CheckFreeLocalloc()
        {
            while(this.localPool != null && this.localPool.GuardPos > this.topPos)
                this.localPool = this.localPool.Free();
        }

        public IntPtr Localloc(uint guardPos, uint size)
        {
            var node = new LocallocNode
            {
                GuardPos = guardPos,
                Memory = Marshal.AllocHGlobal((int) size)
            };
            LocallocNode insert = this.localPool;
            while(insert != null)
            {
                if(insert.Next == null || insert.Next.GuardPos < guardPos)
                    break;
                insert = insert.Next;
            }
            if(insert == null)
            {
                this.localPool = node;
            }
            else
            {
                node.Next = insert.Next;
                insert.Next = node;
            }
            return node.Memory;
        }

        public void FreeAllLocalloc()
        {
            LocallocNode node = this.localPool;
            while(node != null)
                node = node.Free();
            this.localPool = null;
        }

        ~DarksVMStack()
        {
            this.FreeAllLocalloc();
        }

        public void ToTypedReference(uint pos, TypedRefPtr typedRef, Type type)
        {
            if(pos > this.topPos)
                throw new ExecutionEngineException();
            DarksVMSlot[] section = this.sections[(int) (pos >> SectionSize)];
            uint index = pos & IndexMask;
            if(type.IsEnum)
                type = Enum.GetUnderlyingType(type);
            if(type.IsPrimitive || type.IsPointer)
            {
                section[index].ToTypedReferencePrimitive(typedRef);
                TypedReferenceHelpers.CastTypedRef(typedRef, type);
            }
            else
            {
                section[index].ToTypedReferenceObject(typedRef, type);
            }
        }

        private class LocallocNode
        {
            public uint GuardPos;
            public IntPtr Memory;
            public LocallocNode Next;

            ~LocallocNode()
            {
                if(this.Memory != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(this.Memory);
                    this.Memory = IntPtr.Zero;
                }
            }

            public LocallocNode Free()
            {
                if(this.Memory != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(this.Memory);
                    this.Memory = IntPtr.Zero;
                }
                return this.Next;
            }
        }
    }
}