using System;

namespace ProtoBuf
{
	public static class BclHelpers
	{
		[Flags]
		public enum NetObjectOptions : byte
		{
			None = 0x0,
			AsReference = 0x1,
			DynamicType = 0x2,
			UseConstructor = 0x4,
			LateSet = 0x8
		}

		private const int FieldTimeSpanValue = 1;

		private const int FieldTimeSpanScale = 2;

		internal static readonly DateTime EpochOrigin = new DateTime(1970, 1, 1, 0, 0, 0, 0);

		private const int FieldDecimalLow = 1;

		private const int FieldDecimalHigh = 2;

		private const int FieldDecimalSignScale = 3;

		private const int FieldGuidLow = 1;

		private const int FieldGuidHigh = 2;

		private const int FieldExistingObjectKey = 1;

		private const int FieldNewObjectKey = 2;

		private const int FieldExistingTypeKey = 3;

		private const int FieldNewTypeKey = 4;

		private const int FieldTypeName = 8;

		private const int FieldObject = 10;

		public static object GetUninitializedObject(Type type)
		{
			throw new NotSupportedException("Constructor-skipping is not supported on this platform");
		}

		public static void WriteTimeSpan(TimeSpan timeSpan, ProtoWriter dest)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			switch (dest.WireType)
			{
			case WireType.String:
			case WireType.StartGroup:
			{
				long num = timeSpan.Ticks;
				TimeSpanScale timeSpanScale;
				if (timeSpan == TimeSpan.MaxValue)
				{
					num = 1L;
					timeSpanScale = TimeSpanScale.MinMax;
				}
				else if (timeSpan == TimeSpan.MinValue)
				{
					num = -1L;
					timeSpanScale = TimeSpanScale.MinMax;
				}
				else if (num % 864000000000L == 0L)
				{
					timeSpanScale = TimeSpanScale.Days;
					num /= 864000000000L;
				}
				else if (num % 36000000000L == 0L)
				{
					timeSpanScale = TimeSpanScale.Hours;
					num /= 36000000000L;
				}
				else if (num % 600000000 == 0L)
				{
					timeSpanScale = TimeSpanScale.Minutes;
					num /= 600000000;
				}
				else if (num % 10000000 == 0L)
				{
					timeSpanScale = TimeSpanScale.Seconds;
					num /= 10000000;
				}
				else if (num % 10000 == 0L)
				{
					timeSpanScale = TimeSpanScale.Milliseconds;
					num /= 10000;
				}
				else
				{
					timeSpanScale = TimeSpanScale.Ticks;
				}
				SubItemToken token = ProtoWriter.StartSubItem(null, dest);
				if (num != 0L)
				{
					ProtoWriter.WriteFieldHeader(1, WireType.SignedVariant, dest);
					ProtoWriter.WriteInt64(num, dest);
				}
				if (timeSpanScale != 0)
				{
					ProtoWriter.WriteFieldHeader(2, WireType.Variant, dest);
					ProtoWriter.WriteInt32((int)timeSpanScale, dest);
				}
				ProtoWriter.EndSubItem(token, dest);
				break;
			}
			case WireType.Fixed64:
				ProtoWriter.WriteInt64(timeSpan.Ticks, dest);
				break;
			default:
				throw new ProtoException("Unexpected wire-type: " + dest.WireType.ToString());
			}
		}

		public static TimeSpan ReadTimeSpan(ProtoReader source)
		{
			long num = ReadTimeSpanTicks(source);
			switch (num)
			{
			case long.MinValue:
				return TimeSpan.MinValue;
			case long.MaxValue:
				return TimeSpan.MaxValue;
			default:
				return TimeSpan.FromTicks(num);
			}
		}

		public static DateTime ReadDateTime(ProtoReader source)
		{
			long num = ReadTimeSpanTicks(source);
			switch (num)
			{
			case long.MinValue:
				return DateTime.MinValue;
			case long.MaxValue:
				return DateTime.MaxValue;
			default:
				return EpochOrigin.AddTicks(num);
			}
		}

		public static void WriteDateTime(DateTime value, ProtoWriter dest)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			WireType wireType = dest.WireType;
			TimeSpan timeSpan = ((uint)(wireType - 2) > 1u) ? (value - EpochOrigin) : ((value == DateTime.MaxValue) ? TimeSpan.MaxValue : ((!(value == DateTime.MinValue)) ? (value - EpochOrigin) : TimeSpan.MinValue));
			WriteTimeSpan(timeSpan, dest);
		}

		private static long ReadTimeSpanTicks(ProtoReader source)
		{
			switch (source.WireType)
			{
			case WireType.String:
			case WireType.StartGroup:
			{
				SubItemToken token = ProtoReader.StartSubItem(source);
				TimeSpanScale timeSpanScale = TimeSpanScale.Days;
				long num = 0L;
				int num2;
				while ((num2 = source.ReadFieldHeader()) > 0)
				{
					switch (num2)
					{
					case 2:
						timeSpanScale = (TimeSpanScale)source.ReadInt32();
						break;
					case 1:
						source.Assert(WireType.SignedVariant);
						num = source.ReadInt64();
						break;
					default:
						source.SkipField();
						break;
					}
				}
				ProtoReader.EndSubItem(token, source);
				switch (timeSpanScale)
				{
				case TimeSpanScale.Days:
					return num * 864000000000L;
				case TimeSpanScale.Hours:
					return num * 36000000000L;
				case TimeSpanScale.Minutes:
					return num * 600000000;
				case TimeSpanScale.Seconds:
					return num * 10000000;
				case TimeSpanScale.Milliseconds:
					return num * 10000;
				case TimeSpanScale.Ticks:
					return num;
				case TimeSpanScale.MinMax:
					switch (num)
					{
					case 1L:
						return long.MaxValue;
					case -1L:
						return long.MinValue;
					default:
						throw new ProtoException("Unknown min/max value: " + num.ToString());
					}
				default:
					throw new ProtoException("Unknown timescale: " + timeSpanScale.ToString());
				}
			}
			case WireType.Fixed64:
				return source.ReadInt64();
			default:
				throw new ProtoException("Unexpected wire-type: " + source.WireType.ToString());
			}
		}

		public static decimal ReadDecimal(ProtoReader reader)
		{
			ulong num = 0uL;
			uint num2 = 0u;
			uint num3 = 0u;
			SubItemToken token = ProtoReader.StartSubItem(reader);
			int num4;
			while ((num4 = reader.ReadFieldHeader()) > 0)
			{
				switch (num4)
				{
				case 1:
					num = reader.ReadUInt64();
					break;
				case 2:
					num2 = reader.ReadUInt32();
					break;
				case 3:
					num3 = reader.ReadUInt32();
					break;
				default:
					reader.SkipField();
					break;
				}
			}
			ProtoReader.EndSubItem(token, reader);
			if (num == 0L && num2 == 0)
			{
				return decimal.Zero;
			}
			int lo = (int)(num & uint.MaxValue);
			int mid = (int)((num >> 32) & uint.MaxValue);
			int hi = (int)num2;
			bool isNegative = (num3 & 1) == 1;
			byte scale = (byte)((num3 & 0x1FE) >> 1);
			return new decimal(lo, mid, hi, isNegative, scale);
		}

		public static void WriteDecimal(decimal value, ProtoWriter writer)
		{
			int[] bits = decimal.GetBits(value);
			long num = (long)bits[1] << 32;
			ulong num2 = (ulong)(bits[0] & uint.MaxValue);
			ulong num3 = (ulong)(num | (long)num2);
			uint num4 = (uint)bits[2];
			uint num5 = (uint)(((bits[3] >> 15) & 0x1FE) | ((bits[3] >> 31) & 1));
			SubItemToken token = ProtoWriter.StartSubItem(null, writer);
			if (num3 != 0L)
			{
				ProtoWriter.WriteFieldHeader(1, WireType.Variant, writer);
				ProtoWriter.WriteUInt64(num3, writer);
			}
			if (num4 != 0)
			{
				ProtoWriter.WriteFieldHeader(2, WireType.Variant, writer);
				ProtoWriter.WriteUInt32(num4, writer);
			}
			if (num5 != 0)
			{
				ProtoWriter.WriteFieldHeader(3, WireType.Variant, writer);
				ProtoWriter.WriteUInt32(num5, writer);
			}
			ProtoWriter.EndSubItem(token, writer);
		}

		public static void WriteGuid(Guid value, ProtoWriter dest)
		{
			byte[] data = value.ToByteArray();
			SubItemToken token = ProtoWriter.StartSubItem(null, dest);
			if (value != Guid.Empty)
			{
				ProtoWriter.WriteFieldHeader(1, WireType.Fixed64, dest);
				ProtoWriter.WriteBytes(data, 0, 8, dest);
				ProtoWriter.WriteFieldHeader(2, WireType.Fixed64, dest);
				ProtoWriter.WriteBytes(data, 8, 8, dest);
			}
			ProtoWriter.EndSubItem(token, dest);
		}

		public static Guid ReadGuid(ProtoReader source)
		{
			ulong num = 0uL;
			ulong num2 = 0uL;
			SubItemToken token = ProtoReader.StartSubItem(source);
			int num3;
			while ((num3 = source.ReadFieldHeader()) > 0)
			{
				switch (num3)
				{
				case 1:
					num = source.ReadUInt64();
					break;
				case 2:
					num2 = source.ReadUInt64();
					break;
				default:
					source.SkipField();
					break;
				}
			}
			ProtoReader.EndSubItem(token, source);
			if (num == 0L && num2 == 0L)
			{
				return Guid.Empty;
			}
			uint num4 = (uint)(num >> 32);
			int a = (int)num;
			uint num5 = (uint)(num2 >> 32);
			uint num6 = (uint)num2;
			return new Guid(a, (short)num4, (short)(num4 >> 16), (byte)num6, (byte)(num6 >> 8), (byte)(num6 >> 16), (byte)(num6 >> 24), (byte)num5, (byte)(num5 >> 8), (byte)(num5 >> 16), (byte)(num5 >> 24));
		}

		public static object ReadNetObject(object value, ProtoReader source, int key, Type type, NetObjectOptions options)
		{
			SubItemToken token = ProtoReader.StartSubItem(source);
			int num = -1;
			int num2 = -1;
			int num3;
			while ((num3 = source.ReadFieldHeader()) > 0)
			{
				switch (num3)
				{
				case 1:
				{
					int key2 = source.ReadInt32();
					value = source.NetCache.GetKeyedObject(key2);
					break;
				}
				case 2:
					num = source.ReadInt32();
					break;
				case 3:
				{
					int key2 = source.ReadInt32();
					type = (Type)source.NetCache.GetKeyedObject(key2);
					key = source.GetTypeKey(ref type);
					break;
				}
				case 4:
					num2 = source.ReadInt32();
					break;
				case 8:
				{
					string text = source.ReadString();
					type = source.DeserializeType(text);
					if (type == null)
					{
						throw new ProtoException("Unable to resolve type: " + text + " (you can use the TypeModel.DynamicTypeFormatting event to provide a custom mapping)");
					}
					if (type == typeof(string))
					{
						key = -1;
						break;
					}
					key = source.GetTypeKey(ref type);
					if (key >= 0)
					{
						break;
					}
					throw new InvalidOperationException("Dynamic type is not a contract-type: " + type.Name);
				}
				case 10:
				{
					bool flag = type == typeof(string);
					bool flag2 = value == null;
					bool flag3 = flag2 && (flag || (options & NetObjectOptions.LateSet) != NetObjectOptions.None);
					if (num >= 0 && !flag3)
					{
						if (value == null)
						{
							source.TrapNextObject(num);
						}
						else
						{
							source.NetCache.SetKeyedObject(num, value);
						}
						if (num2 >= 0)
						{
							source.NetCache.SetKeyedObject(num2, type);
						}
					}
					object obj = value;
					value = ((!flag) ? ProtoReader.ReadTypedObject(obj, key, source, type) : source.ReadString());
					if (num >= 0)
					{
						if (flag2 && !flag3)
						{
							obj = source.NetCache.GetKeyedObject(num);
						}
						if (flag3)
						{
							source.NetCache.SetKeyedObject(num, value);
							if (num2 >= 0)
							{
								source.NetCache.SetKeyedObject(num2, type);
							}
						}
					}
					if (num >= 0 && !flag3 && obj != value)
					{
						throw new ProtoException("A reference-tracked object changed reference during deserialization");
					}
					if (num < 0 && num2 >= 0)
					{
						source.NetCache.SetKeyedObject(num2, type);
					}
					break;
				}
				default:
					source.SkipField();
					break;
				}
			}
			if (num >= 0 && (options & NetObjectOptions.AsReference) == NetObjectOptions.None)
			{
				throw new ProtoException("Object key in input stream, but reference-tracking was not expected");
			}
			ProtoReader.EndSubItem(token, source);
			return value;
		}

		public static void WriteNetObject(object value, ProtoWriter dest, int key, NetObjectOptions options)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			bool flag = (options & NetObjectOptions.DynamicType) != NetObjectOptions.None;
			bool num = (options & NetObjectOptions.AsReference) != NetObjectOptions.None;
			WireType wireType = dest.WireType;
			SubItemToken token = ProtoWriter.StartSubItem(null, dest);
			bool flag2 = true;
			if (num)
			{
				bool existing;
				int value2 = dest.NetCache.AddObjectKey(value, out existing);
				ProtoWriter.WriteFieldHeader(existing ? 1 : 2, WireType.Variant, dest);
				ProtoWriter.WriteInt32(value2, dest);
				if (existing)
				{
					flag2 = false;
				}
			}
			if (flag2)
			{
				if (flag)
				{
					Type type = value.GetType();
					if (!(value is string))
					{
						key = dest.GetTypeKey(ref type);
						if (key < 0)
						{
							throw new InvalidOperationException("Dynamic type is not a contract-type: " + type.Name);
						}
					}
					bool existing2;
					int value3 = dest.NetCache.AddObjectKey(type, out existing2);
					ProtoWriter.WriteFieldHeader(existing2 ? 3 : 4, WireType.Variant, dest);
					ProtoWriter.WriteInt32(value3, dest);
					if (!existing2)
					{
						ProtoWriter.WriteFieldHeader(8, WireType.String, dest);
						ProtoWriter.WriteString(dest.SerializeType(type), dest);
					}
				}
				ProtoWriter.WriteFieldHeader(10, wireType, dest);
				if (value is string)
				{
					ProtoWriter.WriteString((string)value, dest);
				}
				else
				{
					ProtoWriter.WriteObject(value, key, dest);
				}
			}
			ProtoWriter.EndSubItem(token, dest);
		}
	}
}
