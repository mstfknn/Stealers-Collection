#region

using System;
using System.Reflection;
using KoiVM.Runtime.Dynamic;
using KoiVM.Runtime.Execution;
using KoiVM.Runtime.Execution.Internal;

#endregion

namespace KoiVM.Runtime.VCalls
{
    internal class Stfld : IVCall
    {
        public byte Code => DarksVMConstants.VCALL_STFLD;

        public unsafe void Load(DarksVMContext ctx, out ExecutionState state)
        {
            uint sp = ctx.Registers[DarksVMConstants.REG_SP].U4;
            DarksVMSlot fieldSlot = ctx.Stack[sp--];
            DarksVMSlot valSlot = ctx.Stack[sp--];
            DarksVMSlot objSlot = ctx.Stack[sp--];

            var field = (FieldInfo) ctx.Instance.Data.LookupReference(fieldSlot.U4);
            if(!field.IsStatic && objSlot.O == null)
                throw new NullReferenceException();

            object value = Type.GetTypeCode(field.FieldType) == TypeCode.String && valSlot.O == null
                ? ctx.Instance.Data.LookupString(valSlot.U4)
                : valSlot.ToObject(field.FieldType);
            if (field.DeclaringType.IsValueType && objSlot.O is IReference)
            {
                TypedReference typedRef;
                ((IReference) objSlot.O).ToTypedReference(ctx, &typedRef, field.DeclaringType);
                TypedReferenceHelpers.CastTypedRef(&typedRef, field.DeclaringType);
                field.SetValueDirect(typedRef, value);
            }
            else
            {
                field.SetValue(objSlot.ToObject(field.DeclaringType), value);
            }

            ctx.Stack.SetTopPosition(sp);
            ctx.Registers[DarksVMConstants.REG_SP].U4 = sp;
            state = ExecutionState.Next;
        }
    }
}