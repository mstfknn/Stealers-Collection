#region

using System;
using System.Runtime.InteropServices;
using KoiVM.Runtime.Execution.Internal;

#endregion

namespace KoiVM.Runtime.Execution
{
    internal unsafe class TypedRef : IReference
    {
        private TypedRefPtr? _ptr;
        private readonly PseudoTypedRef _typedRef;

        public TypedRef(TypedRefPtr ptr)
        {
            this._ptr = ptr;
        }

        public TypedRef(TypedReference typedRef)
        {
            this._ptr = null;
            this._typedRef = *(PseudoTypedRef*) &typedRef;
        }

        public DarksVMSlot GetValue(DarksVMContext ctx, PointerType type)
        {
            TypedReference typedRef;
            if(this._ptr != null)
                *&typedRef = *(TypedReference*)this._ptr.Value;
            else
                *(PseudoTypedRef*) &typedRef = this._typedRef;
            return DarksVMSlot.FromObject(TypedReference.ToObject(typedRef), __reftype(typedRef));
        }

        public void SetValue(DarksVMContext ctx, DarksVMSlot slot, PointerType type)
        {
            TypedReference typedRef;
            if(this._ptr != null)
                *&typedRef = *(TypedReference*)this._ptr.Value;
            else
                *(PseudoTypedRef*) &typedRef = this._typedRef;

            Type refType = __reftype(typedRef);
            object value = slot.ToObject(refType);
            TypedReferenceHelpers.SetTypedRef(value, &typedRef);
        }

        public IReference Add(uint value)
        {
            return this;
        }

        public IReference Add(ulong value)
        {
            return this;
        }

        public void ToTypedReference(DarksVMContext ctx, TypedRefPtr typedRef, Type type)
        {
            if(this._ptr != null)
                *(TypedReference*) typedRef = *(TypedReference*)this._ptr.Value;
            else
                *(PseudoTypedRef*) typedRef = this._typedRef;
        }

        // TODO: compat with mono?
        [StructLayout(LayoutKind.Sequential)]
        private struct PseudoTypedRef
        {
            public readonly IntPtr Type;
            public readonly IntPtr Value;
        }
    }
}