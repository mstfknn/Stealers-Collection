#region

using System;
using System.Reflection;
using KoiVM.Runtime.Execution.Internal;

#endregion

namespace KoiVM.Runtime.Execution
{
    internal class FieldRef : IReference
    {
        private readonly FieldInfo field;
        private readonly object instance;

        public FieldRef(object instance, FieldInfo field)
        {
            this.instance = instance;
            this.field = field;
        }

        public DarksVMSlot GetValue(DarksVMContext ctx, PointerType type)
        {
            object inst = this.instance;
            if(this.field.DeclaringType.IsValueType && this.instance is IReference)
                inst = ((IReference)this.instance).GetValue(ctx, PointerType.OBJECT).ToObject(this.field.DeclaringType);
            return DarksVMSlot.FromObject(this.field.GetValue(inst), this.field.FieldType);
        }

        public unsafe void SetValue(DarksVMContext ctx, DarksVMSlot slot, PointerType type)
        {
            if(this.field.DeclaringType.IsValueType && this.instance is IReference)
            {
                TypedReference typedRef;
                ((IReference)this.instance).ToTypedReference(ctx, &typedRef, this.field.DeclaringType);
                this.field.SetValueDirect(typedRef, slot.ToObject(this.field.FieldType));
            }
            else
            {
                this.field.SetValue(this.instance, slot.ToObject(this.field.FieldType));
            }
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
            TypedReferenceHelpers.GetFieldAddr(ctx, this.instance, this.field, typedRef);
        }
    }
}