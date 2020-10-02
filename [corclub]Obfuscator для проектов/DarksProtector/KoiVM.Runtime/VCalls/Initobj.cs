#region

using System;
using System.Runtime.Serialization;
using KoiVM.Runtime.Dynamic;
using KoiVM.Runtime.Execution;
using KoiVM.Runtime.Execution.Internal;

#endregion

namespace KoiVM.Runtime.VCalls
{
    internal class Initobj : IVCall
    {
        public byte Code => DarksVMConstants.VCALL_INITOBJ;

        public void Load(DarksVMContext ctx, out ExecutionState state)
        {
            uint sp = ctx.Registers[DarksVMConstants.REG_SP].U4;
            DarksVMSlot typeSlot = ctx.Stack[sp--];
            DarksVMSlot addrSlot = ctx.Stack[sp--];

            var type = (Type) ctx.Instance.Data.LookupReference(typeSlot.U4);
            if (!(addrSlot.O is IReference reference))
            {
                throw new NotSupportedException();
            }
            else
            {
                var slot = new DarksVMSlot();
                if (!type.IsValueType)
                {
                    slot.O = null;
                }
                else
                {
                    object def = null;
                    if (Nullable.GetUnderlyingType(type) == null)
                        def = FormatterServices.GetUninitializedObject(type);
                    slot.O = ValueTypeBox.Box(def, type);
                }
                reference.SetValue(ctx, slot, PointerType.OBJECT);
            }

            ctx.Stack.SetTopPosition(sp);
            ctx.Registers[DarksVMConstants.REG_SP].U4 = sp;
            state = ExecutionState.Next;
        }
    }
}