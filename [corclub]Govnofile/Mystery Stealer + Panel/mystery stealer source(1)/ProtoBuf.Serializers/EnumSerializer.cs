using System;

namespace ProtoBuf.Serializers
{
	internal sealed class EnumSerializer : IProtoSerializer
	{
		public struct EnumPair
		{
			public readonly object RawValue;

			public readonly Enum TypedValue;

			public readonly int WireValue;

			public EnumPair(int wireValue, object raw, Type type)
			{
				WireValue = wireValue;
				RawValue = raw;
				TypedValue = (Enum)Enum.ToObject(type, raw);
			}
		}

		private readonly Type enumType;

		private readonly EnumPair[] map;

		public Type ExpectedType => enumType;

		bool IProtoSerializer.RequiresOldValue => false;

		bool IProtoSerializer.ReturnsValue => true;

		public EnumSerializer(Type enumType, EnumPair[] map)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			this.enumType = enumType;
			this.map = map;
			if (map == null)
			{
				return;
			}
			for (int i = 1; i < map.Length; i++)
			{
				for (int j = 0; j < i; j++)
				{
					if (map[i].WireValue == map[j].WireValue && !object.Equals(map[i].RawValue, map[j].RawValue))
					{
						throw new ProtoException("Multiple enums with wire-value " + map[i].WireValue.ToString());
					}
					if (object.Equals(map[i].RawValue, map[j].RawValue) && map[i].WireValue != map[j].WireValue)
					{
						throw new ProtoException("Multiple enums with deserialized-value " + map[i].RawValue);
					}
				}
			}
		}

		private ProtoTypeCode GetTypeCode()
		{
			Type underlyingType = Helpers.GetUnderlyingType(enumType);
			if (underlyingType == null)
			{
				underlyingType = enumType;
			}
			return Helpers.GetTypeCode(underlyingType);
		}

		private int EnumToWire(object value)
		{
			switch (GetTypeCode())
			{
			case ProtoTypeCode.Byte:
				return (byte)value;
			case ProtoTypeCode.SByte:
				return (sbyte)value;
			case ProtoTypeCode.Int16:
				return (short)value;
			case ProtoTypeCode.Int32:
				return (int)value;
			case ProtoTypeCode.Int64:
				return (int)(long)value;
			case ProtoTypeCode.UInt16:
				return (ushort)value;
			case ProtoTypeCode.UInt32:
				return (int)(uint)value;
			case ProtoTypeCode.UInt64:
				return (int)(ulong)value;
			default:
				throw new InvalidOperationException();
			}
		}

		private object WireToEnum(int value)
		{
			switch (GetTypeCode())
			{
			case ProtoTypeCode.Byte:
				return Enum.ToObject(enumType, (byte)value);
			case ProtoTypeCode.SByte:
				return Enum.ToObject(enumType, (sbyte)value);
			case ProtoTypeCode.Int16:
				return Enum.ToObject(enumType, (short)value);
			case ProtoTypeCode.Int32:
				return Enum.ToObject(enumType, value);
			case ProtoTypeCode.Int64:
				return Enum.ToObject(enumType, (long)value);
			case ProtoTypeCode.UInt16:
				return Enum.ToObject(enumType, (ushort)value);
			case ProtoTypeCode.UInt32:
				return Enum.ToObject(enumType, (uint)value);
			case ProtoTypeCode.UInt64:
				return Enum.ToObject(enumType, (ulong)value);
			default:
				throw new InvalidOperationException();
			}
		}

		public object Read(object value, ProtoReader source)
		{
			int num = source.ReadInt32();
			if (map == null)
			{
				return WireToEnum(num);
			}
			for (int i = 0; i < map.Length; i++)
			{
				if (map[i].WireValue == num)
				{
					return map[i].TypedValue;
				}
			}
			source.ThrowEnumException(ExpectedType, num);
			return null;
		}

		public void Write(object value, ProtoWriter dest)
		{
			if (map == null)
			{
				ProtoWriter.WriteInt32(EnumToWire(value), dest);
				return;
			}
			for (int i = 0; i < map.Length; i++)
			{
				if (object.Equals(map[i].TypedValue, value))
				{
					ProtoWriter.WriteInt32(map[i].WireValue, dest);
					return;
				}
			}
			ProtoWriter.ThrowEnumException(dest, value);
		}
	}
}
