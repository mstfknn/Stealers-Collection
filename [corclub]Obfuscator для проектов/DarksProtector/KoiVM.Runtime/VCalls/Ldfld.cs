#region

using System;
using System.Reflection;
using KoiVM.Runtime.Dynamic;
using KoiVM.Runtime.Execution;

#endregion

namespace KoiVM.Runtime.VCalls
{
    internal class Ldfld : IVCall
    {
        public byte Code => DarksVMConstants.VCALL_LDFLD;

        public void Load(DarksVMContext ctx, out ExecutionState state)
        {
            uint sp = ctx.Registers[DarksVMConstants.REG_SP].U4;
            DarksVMSlot fieldSlot = ctx.Stack[sp--];
            DarksVMSlot objSlot = ctx.Stack[sp];

            var field = (FieldInfo) ctx.Instance.Data.LookupReference(fieldSlot.U4 & 0x7fffffff);
            if(!field.IsStatic && objSlot.O == null)
                throw new NullReferenceException();

            if (!((fieldSlot.U4 & 0x80000000) != 0))
            {
                object instance = field.DeclaringType.IsValueType && objSlot.O is IReference
                    ? ((IReference)objSlot.O).GetValue(ctx, PointerType.OBJECT).ToObject(field.DeclaringType)
                    : objSlot.ToObject(field.DeclaringType);
                ctx.Stack[sp] = DarksVMSlot.FromObject(field.GetValue(instance), field.FieldType);
            }
            else
            {
                ctx.Stack[sp] = new DarksVMSlot { O = new FieldRef(objSlot.O, field) };
            }

            ctx.Stack.SetTopPosition(sp);
            ctx.Registers[DarksVMConstants.REG_SP].U4 = sp;
            state = ExecutionState.Next;
        }
    }
}