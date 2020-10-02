using System;

namespace ProtoBuf.Serializers
{
	// Token: 0x0200003E RID: 62
	internal sealed class EnumSerializer : IProtoSerializer
	{
		// Token: 0x060001CE RID: 462 RVA: 0x0000E1A8 File Offset: 0x0000C3A8
		public EnumSerializer(Type enumType, EnumSerializer.EnumPair[] map)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			this.enumType = enumType;
			this.map = map;
			if (map != null)
			{
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
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000E2B8 File Offset: 0x0000C4B8
		private ProtoTypeCode GetTypeCode()
		{
			Type underlyingType = Helpers.GetUnderlyingType(this.enumType);
			if (underlyingType == null)
			{
				underlyingType = this.enumType;
			}
			return Helpers.GetTypeCode(underlyingType);
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000334E File Offset: 0x0000154E
		public Type ExpectedType
		{
			get
			{
				return this.enumType;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x000031DF File Offset: 0x000013DF
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00003147 File Offset: 0x00001347
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000E2E4 File Offset: 0x0000C4E4
		private int EnumToWire(object value)
		{
			switch (this.GetTypeCode())
			{
			case ProtoTypeCode.SByte:
				return (int)((sbyte)value);
			case ProtoTypeCode.Byte:
				return (int)((byte)value);
			case ProtoTypeCode.Int16:
				return (int)((short)value);
			case ProtoTypeCode.UInt16:
				return (int)((ushort)value);
			case ProtoTypeCode.Int32:
				return (int)value;
			case ProtoTypeCode.UInt32:
				return (int)((uint)value);
			case ProtoTypeCode.Int64:
				return (int)((long)value);
			case ProtoTypeCode.UInt64:
				return (int)((ulong)value);
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000E364 File Offset: 0x0000C564
		private object WireToEnum(int value)
		{
			switch (this.GetTypeCode())
			{
			case ProtoTypeCode.SByte:
				return Enum.ToObject(this.enumType, (sbyte)value);
			case ProtoTypeCode.Byte:
				return Enum.ToObject(this.enumType, (byte)value);
			case ProtoTypeCode.Int16:
				return Enum.ToObject(this.enumType, (short)value);
			case ProtoTypeCode.UInt16:
				return Enum.ToObject(this.enumType, (ushort)value);
			case ProtoTypeCode.Int32:
				return Enum.ToObject(this.enumType, value);
			case ProtoTypeCode.UInt32:
				return Enum.ToObject(this.enumType, (uint)value);
			case ProtoTypeCode.Int64:
				return Enum.ToObject(this.enumType, (long)value);
			case ProtoTypeCode.UInt64:
				return Enum.ToObject(this.enumType, (ulong)((long)value));
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000E418 File Offset: 0x0000C618
		public object Read(object value, ProtoReader source)
		{
			int num = source.ReadInt32();
			if (this.map == null)
			{
				return this.WireToEnum(num);
			}
			for (int i = 0; i < this.map.Length; i++)
			{
				if (this.map[i].WireValue == num)
				{
					return this.map[i].TypedValue;
				}
			}
			source.ThrowEnumException(this.ExpectedType, num);
			return null;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000E484 File Offset: 0x0000C684
		public void Write(object value, ProtoWriter dest)
		{
			if (this.map == null)
			{
				ProtoWriter.WriteInt32(this.EnumToWire(value), dest);
				return;
			}
			for (int i = 0; i < this.map.Length; i++)
			{
				if (object.Equals(this.map[i].TypedValue, value))
				{
					ProtoWriter.WriteInt32(this.map[i].WireValue, dest);
					return;
				}
			}
			ProtoWriter.ThrowEnumException(dest, value);
		}

		// Token: 0x040000E9 RID: 233
		private readonly Type enumType;

		// Token: 0x040000EA RID: 234
		private readonly EnumSerializer.EnumPair[] map;

		// Token: 0x0200003F RID: 63
		public struct EnumPair
		{
			// Token: 0x060001D7 RID: 471 RVA: 0x00003356 File Offset: 0x00001556
			public EnumPair(int wireValue, object raw, Type type)
			{
				this.WireValue = wireValue;
				this.RawValue = raw;
				this.TypedValue = (Enum)Enum.ToObject(type, raw);
			}

			// Token: 0x040000EB RID: 235
			public readonly object RawValue;

			// Token: 0x040000EC RID: 236
			public readonly Enum TypedValue;

			// Token: 0x040000ED RID: 237
			public readonly int WireValue;
		}
	}
}
