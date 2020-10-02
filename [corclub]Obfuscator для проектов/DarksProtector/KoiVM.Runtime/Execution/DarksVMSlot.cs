#region

using System;
using System.Reflection;
using System.Runtime.InteropServices;
using What_a_great_VM;
using KoiVM.Runtime.Execution.Internal;

#endregion

namespace KoiVM.Runtime.Execution
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct DarksVMSlot
    {
        [FieldOffset(0)] private ulong u8;
        [FieldOffset(0)] private double r8;
        [FieldOffset(0)] private uint u4;
        [FieldOffset(0)] private float r4;
        [FieldOffset(0)] private ushort u2;
        [FieldOffset(0)] private byte u1;
        [FieldOffset(8)] private object o;

        public ulong U8
        {
            get { return this.u8; }
            set
            {
                this.u8 = value;
                this.o = null;
            }
        }

        public uint U4
        {
            get { return this.u4; }
            set
            {
                this.u4 = value;
                this.o = null;
            }
        }

        public ushort U2
        {
            get { return this.u2; }
            set
            {
                this.u2 = value;
                this.o = null;
            }
        }

        public byte U1
        {
            get { return this.u1; }
            set
            {
                this.u1 = value;
                this.o = null;
            }
        }

        public double R8
        {
            get { return this.r8; }
            set
            {
                this.r8 = value;
                this.o = null;
            }
        }

        public float R4
        {
            get { return this.r4; }
            set
            {
                this.r4 = value;
                this.o = null;
            }
        }

        public object O
        {
            get { return this.o; }
            set
            {
                this.o = value;
                this.u8 = 0;
            }
        }

        public static readonly DarksVMSlot Null;

        public static unsafe DarksVMSlot FromObject(object obj, Type type)
        {
            if(type.IsEnum)
            {
                Type elemType = Enum.GetUnderlyingType(type);
                return FromObject(Convert.ChangeType(obj, elemType), elemType);
            }

            switch(Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                    return new DarksVMSlot {u1 = (byte) obj};
                case TypeCode.SByte:
                    return new DarksVMSlot {u1 = (byte) (sbyte) obj};
                case TypeCode.Boolean:
                    return new DarksVMSlot {u1 = (byte) ((bool) obj ? 1 : 0)};

                case TypeCode.UInt16:
                    return new DarksVMSlot {u2 = (ushort) obj};
                case TypeCode.Int16:
                    return new DarksVMSlot {u2 = (ushort) (short) obj};
                case TypeCode.Char:
                    return new DarksVMSlot {u2 = (char) obj};

                case TypeCode.UInt32:
                    return new DarksVMSlot {u4 = (uint) obj};
                case TypeCode.Int32:
                    return new DarksVMSlot {u4 = (uint) (int) obj};

                case TypeCode.UInt64:
                    return new DarksVMSlot {u8 = (ulong) obj};
                case TypeCode.Int64:
                    return new DarksVMSlot {u8 = (ulong) (long) obj};

                case TypeCode.Single:
                    return new DarksVMSlot {r4 = (float) obj};
                case TypeCode.Double:
                    return new DarksVMSlot {r8 = (double) obj};

                default:
                    if(obj is Pointer)
                        return new DarksVMSlot {u8 = (ulong) Pointer.Unbox(obj)};
                    if(obj is IntPtr)
                        return new DarksVMSlot {u8 = (ulong) (IntPtr) obj};
                    if(obj is UIntPtr)
                        return new DarksVMSlot {u8 = (ulong) (UIntPtr) obj};
                    if(type.IsValueType)
                        return new DarksVMSlot {o = ValueTypeBox.Box(obj, type)};
                    return new DarksVMSlot {o = obj};
            }
        }

        public unsafe void ToTypedReferencePrimitive(TypedRefPtr typedRef)
        {
            *(TypedReference*) typedRef = __makeref(this.u4);
        }

        public unsafe void ToTypedReferenceObject(TypedRefPtr typedRef, Type type)
        {
            if(this.o is ValueType && type.IsValueType)
                TypedReferenceHelpers.UnboxTypedRef(this.o, typedRef);
            else
                *(TypedReference*) typedRef = __makeref(this.o);
        }

        public unsafe object ToObject(Type type)
        {
            if(type.IsEnum)
                return Enum.ToObject(type, this.ToObject(Enum.GetUnderlyingType(type)));

            switch(Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                    return this.u1;
                case TypeCode.SByte:
                    return (sbyte)this.u1;
                case TypeCode.Boolean:
                    return this.u1 != 0;

                case TypeCode.UInt16:
                    return this.u2;
                case TypeCode.Int16:
                    return (short)this.u2;
                case TypeCode.Char:
                    return (char)this.u2;

                case TypeCode.UInt32:
                    return this.u4;
                case TypeCode.Int32:
                    return (int)this.u4;

                case TypeCode.UInt64:
                    return this.u8;
                case TypeCode.Int64:
                    return (long)this.u8;

                case TypeCode.Single:
                    return this.r4;
                case TypeCode.Double:
                    return this.r8;

                default:
                    if(type.IsPointer)
                        return Pointer.Box((void*)this.u8, type);
                    if(type == typeof(IntPtr))
                        return Platform.x64 ? new IntPtr((long)this.u8) : new IntPtr((int)this.u4);
                    if(type == typeof(UIntPtr))
                        return Platform.x64 ? new UIntPtr(this.u8) : new UIntPtr(this.u4);
                    return ValueTypeBox.Unbox(this.o);
            }
        }
    }
}