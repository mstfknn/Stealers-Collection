using System;

namespace ProtoBuf
{
	// Token: 0x0200000D RID: 13
	public static class BclHelpers
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002375 File Offset: 0x00000575
		public static object GetUninitializedObject(Type type)
		{
			throw new NotSupportedException("Constructor-skipping is not supported on this platform");
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000A028 File Offset: 0x00008228
		public static void WriteTimeSpan(TimeSpan timeSpan, ProtoWriter dest)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			WireType wireType = dest.WireType;
			if (wireType == WireType.Fixed64)
			{
				ProtoWriter.WriteInt64(timeSpan.Ticks, dest);
				return;
			}
			if (wireType - WireType.String <= 1)
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
				else if (num % 600000000L == 0L)
				{
					timeSpanScale = TimeSpanScale.Minutes;
					num /= 600000000L;
				}
				else if (num % 10000000L == 0L)
				{
					timeSpanScale = TimeSpanScale.Seconds;
					num /= 10000000L;
				}
				else if (num % 10000L == 0L)
				{
					timeSpanScale = TimeSpanScale.Milliseconds;
					num /= 10000L;
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
				if (timeSpanScale != TimeSpanScale.Days)
				{
					ProtoWriter.WriteFieldHeader(2, WireType.Variant, dest);
					ProtoWriter.WriteInt32((int)timeSpanScale, dest);
				}
				ProtoWriter.EndSubItem(token, dest);
				return;
			}
			throw new ProtoException("Unexpected wire-type: " + dest.WireType.ToString());
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000A178 File Offset: 0x00008378
		public static TimeSpan ReadTimeSpan(ProtoReader source)
		{
			long num = BclHelpers.ReadTimeSpanTicks(source);
			if (num == -9223372036854775808L)
			{
				return TimeSpan.MinValue;
			}
			if (num == 9223372036854775807L)
			{
				return TimeSpan.MaxValue;
			}
			return TimeSpan.FromTicks(num);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000A1B8 File Offset: 0x000083B8
		public static DateTime ReadDateTime(ProtoReader source)
		{
			long num = BclHelpers.ReadTimeSpanTicks(source);
			if (num == -9223372036854775808L)
			{
				return DateTime.MinValue;
			}
			if (num == 9223372036854775807L)
			{
				return DateTime.MaxValue;
			}
			return BclHelpers.EpochOrigin.AddTicks(num);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000A200 File Offset: 0x00008400
		public static void WriteDateTime(DateTime value, ProtoWriter dest)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			WireType wireType = dest.WireType;
			TimeSpan timeSpan;
			if (wireType - WireType.String <= 1)
			{
				if (value == DateTime.MaxValue)
				{
					timeSpan = TimeSpan.MaxValue;
				}
				else if (value == DateTime.MinValue)
				{
					timeSpan = TimeSpan.MinValue;
				}
				else
				{
					timeSpan = value - BclHelpers.EpochOrigin;
				}
			}
			else
			{
				timeSpan = value - BclHelpers.EpochOrigin;
			}
			BclHelpers.WriteTimeSpan(timeSpan, dest);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000A274 File Offset: 0x00008474
		private static long ReadTimeSpanTicks(ProtoReader source)
		{
			WireType wireType = source.WireType;
			if (wireType == WireType.Fixed64)
			{
				return source.ReadInt64();
			}
			if (wireType - WireType.String > 1)
			{
				throw new ProtoException("Unexpected wire-type: " + source.WireType.ToString());
			}
			SubItemToken token = ProtoReader.StartSubItem(source);
			TimeSpanScale timeSpanScale = TimeSpanScale.Days;
			long num = 0L;
			int num2;
			while ((num2 = source.ReadFieldHeader()) > 0)
			{
				if (num2 != 1)
				{
					if (num2 == 2)
					{
						timeSpanScale = (TimeSpanScale)source.ReadInt32();
					}
					else
					{
						source.SkipField();
					}
				}
				else
				{
					source.Assert(WireType.SignedVariant);
					num = source.ReadInt64();
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
				return num * 600000000L;
			case TimeSpanScale.Seconds:
				return num * 10000000L;
			case TimeSpanScale.Milliseconds:
				return num * 10000L;
			case TimeSpanScale.Ticks:
				return num;
			default:
				if (timeSpanScale != TimeSpanScale.MinMax)
				{
					throw new ProtoException("Unknown timescale: " + timeSpanScale.ToString());
				}
				if (num == -1L)
				{
					return long.MinValue;
				}
				if (num == 1L)
				{
					return long.MaxValue;
				}
				throw new ProtoException("Unknown min/max value: " + num.ToString());
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000A3C0 File Offset: 0x000085C0
		public static decimal ReadDecimal(ProtoReader reader)
		{
			ulong num = 0UL;
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
			if (num == 0UL && num2 == 0u)
			{
				return 0m;
			}
			int lo = (int)(num & (ulong)-1);
			int mid = (int)(num >> 32 & (ulong)-1);
			int hi = (int)num2;
			bool isNegative = (num3 & 1u) == 1u;
			byte scale = (byte)((num3 & 510u) >> 1);
			return new decimal(lo, mid, hi, isNegative, scale);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000A46C File Offset: 0x0000866C
		public static void WriteDecimal(decimal value, ProtoWriter writer)
		{
			int[] bits = decimal.GetBits(value);
			ulong num = (ulong)((ulong)((long)bits[1]) << 32);
			ulong num2 = (ulong)((long)bits[0] & (long)((ulong)-1));
			ulong num3 = num | num2;
			uint num4 = (uint)bits[2];
			uint num5 = (uint)((bits[3] >> 15 & 510) | (bits[3] >> 31 & 1));
			SubItemToken token = ProtoWriter.StartSubItem(null, writer);
			if (num3 != 0UL)
			{
				ProtoWriter.WriteFieldHeader(1, WireType.Variant, writer);
				ProtoWriter.WriteUInt64(num3, writer);
			}
			if (num4 != 0u)
			{
				ProtoWriter.WriteFieldHeader(2, WireType.Variant, writer);
				ProtoWriter.WriteUInt32(num4, writer);
			}
			if (num5 != 0u)
			{
				ProtoWriter.WriteFieldHeader(3, WireType.Variant, writer);
				ProtoWriter.WriteUInt32(num5, writer);
			}
			ProtoWriter.EndSubItem(token, writer);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000A4F4 File Offset: 0x000086F4
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

		// Token: 0x0600003D RID: 61 RVA: 0x0000A548 File Offset: 0x00008748
		public static Guid ReadGuid(ProtoReader source)
		{
			ulong num = 0UL;
			ulong num2 = 0UL;
			SubItemToken token = ProtoReader.StartSubItem(source);
			int num3;
			while ((num3 = source.ReadFieldHeader()) > 0)
			{
				if (num3 != 1)
				{
					if (num3 != 2)
					{
						source.SkipField();
					}
					else
					{
						num2 = source.ReadUInt64();
					}
				}
				else
				{
					num = source.ReadUInt64();
				}
			}
			ProtoReader.EndSubItem(token, source);
			if (num == 0UL && num2 == 0UL)
			{
				return Guid.Empty;
			}
			uint num4 = (uint)(num >> 32);
			int a = (int)((uint)num);
			uint num5 = (uint)(num2 >> 32);
			uint num6 = (uint)num2;
			return new Guid(a, (short)num4, (short)(num4 >> 16), (byte)num6, (byte)(num6 >> 8), (byte)(num6 >> 16), (byte)(num6 >> 24), (byte)num5, (byte)(num5 >> 8), (byte)(num5 >> 16), (byte)(num5 >> 24));
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000A5F0 File Offset: 0x000087F0
		public static object ReadNetObject(object value, ProtoReader source, int key, Type type, BclHelpers.NetObjectOptions options)
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
					continue;
				}
				case 2:
					num = source.ReadInt32();
					continue;
				case 3:
				{
					int key2 = source.ReadInt32();
					type = (Type)source.NetCache.GetKeyedObject(key2);
					key = source.GetTypeKey(ref type);
					continue;
				}
				case 4:
					num2 = source.ReadInt32();
					continue;
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
						continue;
					}
					key = source.GetTypeKey(ref type);
					if (key < 0)
					{
						throw new InvalidOperationException("Dynamic type is not a contract-type: " + type.Name);
					}
					continue;
				}
				case 10:
				{
					bool flag = type == typeof(string);
					bool flag2 = value == null;
					bool flag3 = flag2 && (flag || (options & BclHelpers.NetObjectOptions.LateSet) > BclHelpers.NetObjectOptions.None);
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
					if (flag)
					{
						value = source.ReadString();
					}
					else
					{
						value = ProtoReader.ReadTypedObject(obj, key, source, type);
					}
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
						continue;
					}
					continue;
				}
				}
				source.SkipField();
			}
			if (num >= 0 && (options & BclHelpers.NetObjectOptions.AsReference) == BclHelpers.NetObjectOptions.None)
			{
				throw new ProtoException("Object key in input stream, but reference-tracking was not expected");
			}
			ProtoReader.EndSubItem(token, source);
			return value;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000A820 File Offset: 0x00008A20
		public static void WriteNetObject(object value, ProtoWriter dest, int key, BclHelpers.NetObjectOptions options)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			bool flag = (options & BclHelpers.NetObjectOptions.DynamicType) > BclHelpers.NetObjectOptions.None;
			bool flag2 = (options & BclHelpers.NetObjectOptions.AsReference) > BclHelpers.NetObjectOptions.None;
			WireType wireType = dest.WireType;
			SubItemToken token = ProtoWriter.StartSubItem(null, dest);
			bool flag3 = true;
			if (flag2)
			{
				bool flag4;
				int value2 = dest.NetCache.AddObjectKey(value, out flag4);
				ProtoWriter.WriteFieldHeader(flag4 ? 1 : 2, WireType.Variant, dest);
				ProtoWriter.WriteInt32(value2, dest);
				if (flag4)
				{
					flag3 = false;
				}
			}
			if (flag3)
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
					bool flag5;
					int value3 = dest.NetCache.AddObjectKey(type, out flag5);
					ProtoWriter.WriteFieldHeader(flag5 ? 3 : 4, WireType.Variant, dest);
					ProtoWriter.WriteInt32(value3, dest);
					if (!flag5)
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

		// Token: 0x04000019 RID: 25
		private const int FieldTimeSpanValue = 1;

		// Token: 0x0400001A RID: 26
		private const int FieldTimeSpanScale = 2;

		// Token: 0x0400001B RID: 27
		internal static readonly DateTime EpochOrigin = new DateTime(1970, 1, 1, 0, 0, 0, 0);

		// Token: 0x0400001C RID: 28
		private const int FieldDecimalLow = 1;

		// Token: 0x0400001D RID: 29
		private const int FieldDecimalHigh = 2;

		// Token: 0x0400001E RID: 30
		private const int FieldDecimalSignScale = 3;

		// Token: 0x0400001F RID: 31
		private const int FieldGuidLow = 1;

		// Token: 0x04000020 RID: 32
		private const int FieldGuidHigh = 2;

		// Token: 0x04000021 RID: 33
		private const int FieldExistingObjectKey = 1;

		// Token: 0x04000022 RID: 34
		private const int FieldNewObjectKey = 2;

		// Token: 0x04000023 RID: 35
		private const int FieldExistingTypeKey = 3;

		// Token: 0x04000024 RID: 36
		private const int FieldNewTypeKey = 4;

		// Token: 0x04000025 RID: 37
		private const int FieldTypeName = 8;

		// Token: 0x04000026 RID: 38
		private const int FieldObject = 10;

		// Token: 0x0200000E RID: 14
		[Flags]
		public enum NetObjectOptions : byte
		{
			// Token: 0x04000028 RID: 40
			None = 0,
			// Token: 0x04000029 RID: 41
			AsReference = 1,
			// Token: 0x0400002A RID: 42
			DynamicType = 2,
			// Token: 0x0400002B RID: 43
			UseConstructor = 4,
			// Token: 0x0400002C RID: 44
			LateSet = 8
		}
	}
}
