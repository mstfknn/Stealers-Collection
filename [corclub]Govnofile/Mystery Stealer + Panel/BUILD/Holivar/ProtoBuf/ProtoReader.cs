using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ProtoBuf.Meta;

namespace ProtoBuf
{
	// Token: 0x0200002C RID: 44
	public sealed class ProtoReader : IDisposable
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00002A5B File Offset: 0x00000C5B
		public int FieldNumber
		{
			get
			{
				return this.fieldNumber;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00002A63 File Offset: 0x00000C63
		public WireType WireType
		{
			get
			{
				return this.wireType;
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00002A6B File Offset: 0x00000C6B
		public ProtoReader(Stream source, TypeModel model, SerializationContext context)
		{
			ProtoReader.Init(this, source, model, context, -1);
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00002A7D File Offset: 0x00000C7D
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00002A85 File Offset: 0x00000C85
		public bool InternStrings
		{
			get
			{
				return this.internStrings;
			}
			set
			{
				this.internStrings = value;
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00002A8E File Offset: 0x00000C8E
		public ProtoReader(Stream source, TypeModel model, SerializationContext context, int length)
		{
			ProtoReader.Init(this, source, model, context, length);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000B55C File Offset: 0x0000975C
		private static void Init(ProtoReader reader, Stream source, TypeModel model, SerializationContext context, int length)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (!source.CanRead)
			{
				throw new ArgumentException("Cannot read from stream", "source");
			}
			reader.source = source;
			reader.ioBuffer = BufferPool.GetBuffer();
			reader.model = model;
			bool flag = length >= 0;
			reader.isFixedLength = flag;
			reader.dataRemaining = (flag ? length : 0);
			if (context == null)
			{
				context = SerializationContext.Default;
			}
			else
			{
				context.Freeze();
			}
			reader.context = context;
			reader.position = (reader.available = (reader.depth = (reader.fieldNumber = (reader.ioIndex = 0))));
			reader.blockEnd = int.MaxValue;
			reader.internStrings = true;
			reader.wireType = WireType.None;
			reader.trapCount = 1u;
			if (reader.netCache == null)
			{
				reader.netCache = new NetObjectCache();
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00002AA1 File Offset: 0x00000CA1
		public SerializationContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000B640 File Offset: 0x00009840
		public void Dispose()
		{
			this.source = null;
			this.model = null;
			BufferPool.ReleaseBufferToPool(ref this.ioBuffer);
			if (this.stringInterner != null)
			{
				this.stringInterner.Clear();
			}
			if (this.netCache != null)
			{
				this.netCache.Clear();
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000B68C File Offset: 0x0000988C
		internal int TryReadUInt32VariantWithoutMoving(bool trimNegative, out uint value)
		{
			if (this.available < 10)
			{
				this.Ensure(10, false);
			}
			if (this.available == 0)
			{
				value = 0u;
				return 0;
			}
			int num = this.ioIndex;
			value = (uint)this.ioBuffer[num++];
			if ((value & 128u) == 0u)
			{
				return 1;
			}
			value &= 127u;
			if (this.available == 1)
			{
				throw ProtoReader.EoF(this);
			}
			uint num2 = (uint)this.ioBuffer[num++];
			value |= (num2 & 127u) << 7;
			if ((num2 & 128u) == 0u)
			{
				return 2;
			}
			if (this.available == 2)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (uint)this.ioBuffer[num++];
			value |= (num2 & 127u) << 14;
			if ((num2 & 128u) == 0u)
			{
				return 3;
			}
			if (this.available == 3)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (uint)this.ioBuffer[num++];
			value |= (num2 & 127u) << 21;
			if ((num2 & 128u) == 0u)
			{
				return 4;
			}
			if (this.available == 4)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (uint)this.ioBuffer[num];
			value |= num2 << 28;
			if ((num2 & 240u) == 0u)
			{
				return 5;
			}
			if (trimNegative && (num2 & 240u) == 240u && this.available >= 10 && this.ioBuffer[++num] == 255 && this.ioBuffer[++num] == 255 && this.ioBuffer[++num] == 255 && this.ioBuffer[++num] == 255 && this.ioBuffer[num + 1] == 1)
			{
				return 10;
			}
			throw ProtoReader.AddErrorData(new OverflowException(), this);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000B830 File Offset: 0x00009A30
		private uint ReadUInt32Variant(bool trimNegative)
		{
			uint result;
			int num = this.TryReadUInt32VariantWithoutMoving(trimNegative, out result);
			if (num > 0)
			{
				this.ioIndex += num;
				this.available -= num;
				this.position += num;
				return result;
			}
			throw ProtoReader.EoF(this);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000B880 File Offset: 0x00009A80
		private bool TryReadUInt32Variant(out uint value)
		{
			int num = this.TryReadUInt32VariantWithoutMoving(false, out value);
			if (num > 0)
			{
				this.ioIndex += num;
				this.available -= num;
				this.position += num;
				return true;
			}
			return false;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000B8C8 File Offset: 0x00009AC8
		public uint ReadUInt32()
		{
			WireType wireType = this.wireType;
			if (wireType == WireType.Variant)
			{
				return this.ReadUInt32Variant(false);
			}
			if (wireType == WireType.Fixed64)
			{
				return checked((uint)this.ReadUInt64());
			}
			if (wireType != WireType.Fixed32)
			{
				throw this.CreateWireTypeException();
			}
			if (this.available < 4)
			{
				this.Ensure(4, true);
			}
			this.position += 4;
			this.available -= 4;
			byte[] array = this.ioBuffer;
			int num = this.ioIndex;
			this.ioIndex = num + 1;
			uint num2 = array[num];
			byte[] array2 = this.ioBuffer;
			num = this.ioIndex;
			this.ioIndex = num + 1;
			uint num3 = num2 | array2[num] << 8;
			byte[] array3 = this.ioBuffer;
			num = this.ioIndex;
			this.ioIndex = num + 1;
			uint num4 = num3 | array3[num] << 16;
			byte[] array4 = this.ioBuffer;
			num = this.ioIndex;
			this.ioIndex = num + 1;
			return num4 | array4[num] << 24;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00002AA9 File Offset: 0x00000CA9
		public int Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000B9A0 File Offset: 0x00009BA0
		internal void Ensure(int count, bool strict)
		{
			if (count > this.ioBuffer.Length)
			{
				BufferPool.ResizeAndFlushLeft(ref this.ioBuffer, count, this.ioIndex, this.available);
				this.ioIndex = 0;
			}
			else if (this.ioIndex + count >= this.ioBuffer.Length)
			{
				Helpers.BlockCopy(this.ioBuffer, this.ioIndex, this.ioBuffer, 0, this.available);
				this.ioIndex = 0;
			}
			count -= this.available;
			int num = this.ioIndex + this.available;
			int num2 = this.ioBuffer.Length - num;
			if (this.isFixedLength && this.dataRemaining < num2)
			{
				num2 = this.dataRemaining;
			}
			int num3;
			while (count > 0 && num2 > 0 && (num3 = this.source.Read(this.ioBuffer, num, num2)) > 0)
			{
				this.available += num3;
				count -= num3;
				num2 -= num3;
				num += num3;
				if (this.isFixedLength)
				{
					this.dataRemaining -= num3;
				}
			}
			if (strict && count > 0)
			{
				throw ProtoReader.EoF(this);
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00002AB1 File Offset: 0x00000CB1
		public short ReadInt16()
		{
			return checked((short)this.ReadInt32());
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00002ABA File Offset: 0x00000CBA
		public ushort ReadUInt16()
		{
			return checked((ushort)this.ReadUInt32());
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00002AC3 File Offset: 0x00000CC3
		public byte ReadByte()
		{
			return checked((byte)this.ReadUInt32());
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00002ACC File Offset: 0x00000CCC
		public sbyte ReadSByte()
		{
			return checked((sbyte)this.ReadInt32());
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000BAAC File Offset: 0x00009CAC
		public int ReadInt32()
		{
			WireType wireType = this.wireType;
			if (wireType <= WireType.Fixed64)
			{
				if (wireType == WireType.Variant)
				{
					return (int)this.ReadUInt32Variant(true);
				}
				if (wireType == WireType.Fixed64)
				{
					return checked((int)this.ReadInt64());
				}
			}
			else
			{
				if (wireType == WireType.Fixed32)
				{
					if (this.available < 4)
					{
						this.Ensure(4, true);
					}
					this.position += 4;
					this.available -= 4;
					byte[] array = this.ioBuffer;
					int num = this.ioIndex;
					this.ioIndex = num + 1;
					int num2 = array[num];
					byte[] array2 = this.ioBuffer;
					num = this.ioIndex;
					this.ioIndex = num + 1;
					int num3 = num2 | array2[num] << 8;
					byte[] array3 = this.ioBuffer;
					num = this.ioIndex;
					this.ioIndex = num + 1;
					int num4 = num3 | array3[num] << 16;
					byte[] array4 = this.ioBuffer;
					num = this.ioIndex;
					this.ioIndex = num + 1;
					return num4 | array4[num] << 24;
				}
				if (wireType == WireType.SignedVariant)
				{
					return ProtoReader.Zag(this.ReadUInt32Variant(true));
				}
			}
			throw this.CreateWireTypeException();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000BBA0 File Offset: 0x00009DA0
		private static int Zag(uint ziggedValue)
		{
			return (int)(-(ziggedValue & 1u) ^ (uint)((int)ziggedValue >> 1 & int.MaxValue));
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000BBC0 File Offset: 0x00009DC0
		private static long Zag(ulong ziggedValue)
		{
			return (long)(-(long)(ziggedValue & 1UL) ^ (ziggedValue >> 1 & 9223372036854775807UL));
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000BBE4 File Offset: 0x00009DE4
		public long ReadInt64()
		{
			WireType wireType = this.wireType;
			if (wireType <= WireType.Fixed64)
			{
				if (wireType == WireType.Variant)
				{
					return (long)this.ReadUInt64Variant();
				}
				if (wireType == WireType.Fixed64)
				{
					if (this.available < 8)
					{
						this.Ensure(8, true);
					}
					this.position += 8;
					this.available -= 8;
					byte[] array = this.ioBuffer;
					int num = this.ioIndex;
					this.ioIndex = num + 1;
					long num2 = (long)array[num];
					byte[] array2 = this.ioBuffer;
					num = this.ioIndex;
					this.ioIndex = num + 1;
					long num3 = num2 | (long)((long)array2[num] << 8);
					byte[] array3 = this.ioBuffer;
					num = this.ioIndex;
					this.ioIndex = num + 1;
					long num4 = num3 | (long)((long)array3[num] << 16);
					byte[] array4 = this.ioBuffer;
					num = this.ioIndex;
					this.ioIndex = num + 1;
					long num5 = num4 | (long)((long)array4[num] << 24);
					byte[] array5 = this.ioBuffer;
					num = this.ioIndex;
					this.ioIndex = num + 1;
					long num6 = num5 | (long)((long)array5[num] << 32);
					byte[] array6 = this.ioBuffer;
					num = this.ioIndex;
					this.ioIndex = num + 1;
					long num7 = num6 | (long)((long)array6[num] << 40);
					byte[] array7 = this.ioBuffer;
					num = this.ioIndex;
					this.ioIndex = num + 1;
					long num8 = num7 | (long)((long)array7[num] << 48);
					byte[] array8 = this.ioBuffer;
					num = this.ioIndex;
					this.ioIndex = num + 1;
					return num8 | (long)((long)array8[num] << 56);
				}
			}
			else
			{
				if (wireType == WireType.Fixed32)
				{
					return (long)this.ReadInt32();
				}
				if (wireType == WireType.SignedVariant)
				{
					return ProtoReader.Zag(this.ReadUInt64Variant());
				}
			}
			throw this.CreateWireTypeException();
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000BD4C File Offset: 0x00009F4C
		private int TryReadUInt64VariantWithoutMoving(out ulong value)
		{
			if (this.available < 10)
			{
				this.Ensure(10, false);
			}
			if (this.available == 0)
			{
				value = 0UL;
				return 0;
			}
			int num = this.ioIndex;
			value = (ulong)this.ioBuffer[num++];
			if ((value & 128UL) == 0UL)
			{
				return 1;
			}
			value &= 127UL;
			if (this.available == 1)
			{
				throw ProtoReader.EoF(this);
			}
			ulong num2 = (ulong)this.ioBuffer[num++];
			value |= (num2 & 127UL) << 7;
			if ((num2 & 128UL) == 0UL)
			{
				return 2;
			}
			if (this.available == 2)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (ulong)this.ioBuffer[num++];
			value |= (num2 & 127UL) << 14;
			if ((num2 & 128UL) == 0UL)
			{
				return 3;
			}
			if (this.available == 3)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (ulong)this.ioBuffer[num++];
			value |= (num2 & 127UL) << 21;
			if ((num2 & 128UL) == 0UL)
			{
				return 4;
			}
			if (this.available == 4)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (ulong)this.ioBuffer[num++];
			value |= (num2 & 127UL) << 28;
			if ((num2 & 128UL) == 0UL)
			{
				return 5;
			}
			if (this.available == 5)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (ulong)this.ioBuffer[num++];
			value |= (num2 & 127UL) << 35;
			if ((num2 & 128UL) == 0UL)
			{
				return 6;
			}
			if (this.available == 6)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (ulong)this.ioBuffer[num++];
			value |= (num2 & 127UL) << 42;
			if ((num2 & 128UL) == 0UL)
			{
				return 7;
			}
			if (this.available == 7)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (ulong)this.ioBuffer[num++];
			value |= (num2 & 127UL) << 49;
			if ((num2 & 128UL) == 0UL)
			{
				return 8;
			}
			if (this.available == 8)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (ulong)this.ioBuffer[num++];
			value |= (num2 & 127UL) << 56;
			if ((num2 & 128UL) == 0UL)
			{
				return 9;
			}
			if (this.available == 9)
			{
				throw ProtoReader.EoF(this);
			}
			num2 = (ulong)this.ioBuffer[num];
			value |= num2 << 63;
			if ((num2 & 18446744073709551614UL) != 0UL)
			{
				throw ProtoReader.AddErrorData(new OverflowException(), this);
			}
			return 10;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000BF98 File Offset: 0x0000A198
		private ulong ReadUInt64Variant()
		{
			ulong result;
			int num = this.TryReadUInt64VariantWithoutMoving(out result);
			if (num > 0)
			{
				this.ioIndex += num;
				this.available -= num;
				this.position += num;
				return result;
			}
			throw ProtoReader.EoF(this);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000BFE4 File Offset: 0x0000A1E4
		private string Intern(string value)
		{
			if (value == null)
			{
				return null;
			}
			if (value.Length == 0)
			{
				return "";
			}
			string text;
			if (this.stringInterner == null)
			{
				this.stringInterner = new Dictionary<string, string>();
				this.stringInterner.Add(value, value);
			}
			else if (this.stringInterner.TryGetValue(value, out text))
			{
				value = text;
			}
			else
			{
				this.stringInterner.Add(value, value);
			}
			return value;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000C04C File Offset: 0x0000A24C
		public string ReadString()
		{
			if (this.wireType != WireType.String)
			{
				throw this.CreateWireTypeException();
			}
			int num = (int)this.ReadUInt32Variant(false);
			if (num == 0)
			{
				return "";
			}
			if (this.available < num)
			{
				this.Ensure(num, true);
			}
			string text = ProtoReader.encoding.GetString(this.ioBuffer, this.ioIndex, num);
			if (this.internStrings)
			{
				text = this.Intern(text);
			}
			this.available -= num;
			this.position += num;
			this.ioIndex += num;
			return text;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000C0E0 File Offset: 0x0000A2E0
		public void ThrowEnumException(Type type, int value)
		{
			string str = (type == null) ? "<null>" : type.FullName;
			throw ProtoReader.AddErrorData(new ProtoException("No " + str + " enum is mapped to the wire-value " + value.ToString()), this);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00002AD5 File Offset: 0x00000CD5
		private Exception CreateWireTypeException()
		{
			return this.CreateException("Invalid wire-type; this usually means you have over-written a file without truncating or setting the length; see http://stackoverflow.com/q/2152978/23354");
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00002AE2 File Offset: 0x00000CE2
		private Exception CreateException(string message)
		{
			return ProtoReader.AddErrorData(new ProtoException(message), this);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000C120 File Offset: 0x0000A320
		public unsafe double ReadDouble()
		{
			WireType wireType = this.wireType;
			if (wireType == WireType.Fixed64)
			{
				long num = this.ReadInt64();
				return *(double*)(&num);
			}
			if (wireType == WireType.Fixed32)
			{
				return (double)this.ReadSingle();
			}
			throw this.CreateWireTypeException();
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00002AF0 File Offset: 0x00000CF0
		public static object ReadObject(object value, int key, ProtoReader reader)
		{
			return ProtoReader.ReadTypedObject(value, key, reader, null);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000C158 File Offset: 0x0000A358
		internal static object ReadTypedObject(object value, int key, ProtoReader reader, Type type)
		{
			if (reader.model == null)
			{
				throw ProtoReader.AddErrorData(new InvalidOperationException("Cannot deserialize sub-objects unless a model is provided"), reader);
			}
			SubItemToken token = ProtoReader.StartSubItem(reader);
			if (key >= 0)
			{
				value = reader.model.Deserialize(key, value, reader);
			}
			else if (type == null || !reader.model.TryDeserializeAuxiliaryType(reader, DataFormat.Default, 1, type, ref value, true, false, true, false))
			{
				TypeModel.ThrowUnexpectedType(type);
			}
			ProtoReader.EndSubItem(token, reader);
			return value;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000C1C4 File Offset: 0x0000A3C4
		public static void EndSubItem(SubItemToken token, ProtoReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			int value = token.value;
			WireType wireType = reader.wireType;
			if (wireType == WireType.EndGroup)
			{
				if (value >= 0)
				{
					throw ProtoReader.AddErrorData(new ArgumentException("token"), reader);
				}
				if (-value != reader.fieldNumber)
				{
					throw reader.CreateException("Wrong group was ended");
				}
				reader.wireType = WireType.None;
				reader.depth--;
				return;
			}
			else
			{
				if (value < reader.position)
				{
					throw reader.CreateException("Sub-message not read entirely");
				}
				if (reader.blockEnd != reader.position && reader.blockEnd != 2147483647)
				{
					throw reader.CreateException("Sub-message not read correctly");
				}
				reader.blockEnd = value;
				reader.depth--;
				return;
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000C284 File Offset: 0x0000A484
		public static SubItemToken StartSubItem(ProtoReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			WireType wireType = reader.wireType;
			if (wireType != WireType.String)
			{
				if (wireType == WireType.StartGroup)
				{
					reader.wireType = WireType.None;
					reader.depth++;
					return new SubItemToken(-reader.fieldNumber);
				}
				throw reader.CreateWireTypeException();
			}
			else
			{
				int num = (int)reader.ReadUInt32Variant(false);
				if (num < 0)
				{
					throw ProtoReader.AddErrorData(new InvalidOperationException(), reader);
				}
				int value = reader.blockEnd;
				reader.blockEnd = reader.position + num;
				reader.depth++;
				return new SubItemToken(value);
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000C318 File Offset: 0x0000A518
		public int ReadFieldHeader()
		{
			if (this.blockEnd <= this.position || this.wireType == WireType.EndGroup)
			{
				return 0;
			}
			uint num;
			if (this.TryReadUInt32Variant(out num))
			{
				this.wireType = (WireType)(num & 7u);
				this.fieldNumber = (int)(num >> 3);
				if (this.fieldNumber < 1)
				{
					throw new ProtoException("Invalid field in source data: " + this.fieldNumber.ToString());
				}
			}
			else
			{
				this.wireType = WireType.None;
				this.fieldNumber = 0;
			}
			if (this.wireType != WireType.EndGroup)
			{
				return this.fieldNumber;
			}
			if (this.depth > 0)
			{
				return 0;
			}
			throw new ProtoException("Unexpected end-group in source data; this usually means the source data is corrupt");
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000C3B4 File Offset: 0x0000A5B4
		public bool TryReadFieldHeader(int field)
		{
			if (this.blockEnd <= this.position || this.wireType == WireType.EndGroup)
			{
				return false;
			}
			uint num2;
			int num = this.TryReadUInt32VariantWithoutMoving(false, out num2);
			WireType wireType;
			if (num > 0 && (int)num2 >> 3 == field && (wireType = (WireType)(num2 & 7u)) != WireType.EndGroup)
			{
				this.wireType = wireType;
				this.fieldNumber = field;
				this.position += num;
				this.ioIndex += num;
				this.available -= num;
				return true;
			}
			return false;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00002AFB File Offset: 0x00000CFB
		public TypeModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00002B03 File Offset: 0x00000D03
		public void Hint(WireType wireType)
		{
			if (this.wireType != wireType && (wireType & (WireType)7) == this.wireType)
			{
				this.wireType = wireType;
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00002B20 File Offset: 0x00000D20
		public void Assert(WireType wireType)
		{
			if (this.wireType == wireType)
			{
				return;
			}
			if ((wireType & (WireType)7) == this.wireType)
			{
				this.wireType = wireType;
				return;
			}
			throw this.CreateWireTypeException();
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000C434 File Offset: 0x0000A634
		public void SkipField()
		{
			switch (this.wireType)
			{
			case WireType.Variant:
			case WireType.SignedVariant:
				this.ReadUInt64Variant();
				return;
			case WireType.Fixed64:
				if (this.available < 8)
				{
					this.Ensure(8, true);
				}
				this.available -= 8;
				this.ioIndex += 8;
				this.position += 8;
				return;
			case WireType.String:
			{
				int num = (int)this.ReadUInt32Variant(false);
				if (num <= this.available)
				{
					this.available -= num;
					this.ioIndex += num;
					this.position += num;
					return;
				}
				this.position += num;
				num -= this.available;
				this.ioIndex = (this.available = 0);
				if (this.isFixedLength)
				{
					if (num > this.dataRemaining)
					{
						throw ProtoReader.EoF(this);
					}
					this.dataRemaining -= num;
				}
				ProtoReader.Seek(this.source, num, this.ioBuffer);
				return;
			}
			case WireType.StartGroup:
			{
				int num2 = this.fieldNumber;
				this.depth++;
				while (this.ReadFieldHeader() > 0)
				{
					this.SkipField();
				}
				this.depth--;
				if (this.wireType == WireType.EndGroup && this.fieldNumber == num2)
				{
					this.wireType = WireType.None;
					return;
				}
				throw this.CreateWireTypeException();
			}
			case WireType.Fixed32:
				if (this.available < 4)
				{
					this.Ensure(4, true);
				}
				this.available -= 4;
				this.ioIndex += 4;
				this.position += 4;
				return;
			}
			throw this.CreateWireTypeException();
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000C5F4 File Offset: 0x0000A7F4
		public ulong ReadUInt64()
		{
			WireType wireType = this.wireType;
			if (wireType == WireType.Variant)
			{
				return this.ReadUInt64Variant();
			}
			if (wireType == WireType.Fixed64)
			{
				if (this.available < 8)
				{
					this.Ensure(8, true);
				}
				this.position += 8;
				this.available -= 8;
				byte[] array = this.ioBuffer;
				int num = this.ioIndex;
				this.ioIndex = num + 1;
				ulong num2 = array[num];
				byte[] array2 = this.ioBuffer;
				num = this.ioIndex;
				this.ioIndex = num + 1;
				ulong num3 = num2 | array2[num] << 8;
				byte[] array3 = this.ioBuffer;
				num = this.ioIndex;
				this.ioIndex = num + 1;
				ulong num4 = num3 | array3[num] << 16;
				byte[] array4 = this.ioBuffer;
				num = this.ioIndex;
				this.ioIndex = num + 1;
				ulong num5 = num4 | array4[num] << 24;
				byte[] array5 = this.ioBuffer;
				num = this.ioIndex;
				this.ioIndex = num + 1;
				ulong num6 = num5 | array5[num] << 32;
				byte[] array6 = this.ioBuffer;
				num = this.ioIndex;
				this.ioIndex = num + 1;
				ulong num7 = num6 | array6[num] << 40;
				byte[] array7 = this.ioBuffer;
				num = this.ioIndex;
				this.ioIndex = num + 1;
				ulong num8 = num7 | array7[num] << 48;
				byte[] array8 = this.ioBuffer;
				num = this.ioIndex;
				this.ioIndex = num + 1;
				return num8 | array8[num] << 56;
			}
			if (wireType != WireType.Fixed32)
			{
				throw this.CreateWireTypeException();
			}
			return (ulong)this.ReadUInt32();
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000C740 File Offset: 0x0000A940
		public unsafe float ReadSingle()
		{
			WireType wireType = this.wireType;
			if (wireType != WireType.Fixed64)
			{
				if (wireType == WireType.Fixed32)
				{
					int num = this.ReadInt32();
					return *(float*)(&num);
				}
				throw this.CreateWireTypeException();
			}
			else
			{
				double num2 = this.ReadDouble();
				float num3 = (float)num2;
				if (Helpers.IsInfinity(num3) && !Helpers.IsInfinity(num2))
				{
					throw ProtoReader.AddErrorData(new OverflowException(), this);
				}
				return num3;
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000C794 File Offset: 0x0000A994
		public bool ReadBoolean()
		{
			uint num = this.ReadUInt32();
			if (num == 0u)
			{
				return false;
			}
			if (num != 1u)
			{
				throw this.CreateException("Unexpected boolean value");
			}
			return true;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000C7C0 File Offset: 0x0000A9C0
		public static byte[] AppendBytes(byte[] value, ProtoReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			WireType wireType = reader.wireType;
			if (wireType != WireType.String)
			{
				throw reader.CreateWireTypeException();
			}
			int i = (int)reader.ReadUInt32Variant(false);
			reader.wireType = WireType.None;
			if (i != 0)
			{
				int num;
				if (value == null || value.Length == 0)
				{
					num = 0;
					value = new byte[i];
				}
				else
				{
					num = value.Length;
					byte[] array = new byte[value.Length + i];
					Helpers.BlockCopy(value, 0, array, 0, value.Length);
					value = array;
				}
				reader.position += i;
				while (i > reader.available)
				{
					if (reader.available > 0)
					{
						Helpers.BlockCopy(reader.ioBuffer, reader.ioIndex, value, num, reader.available);
						i -= reader.available;
						num += reader.available;
						reader.ioIndex = (reader.available = 0);
					}
					int num2 = (i > reader.ioBuffer.Length) ? reader.ioBuffer.Length : i;
					if (num2 > 0)
					{
						reader.Ensure(num2, true);
					}
				}
				if (i > 0)
				{
					Helpers.BlockCopy(reader.ioBuffer, reader.ioIndex, value, num, i);
					reader.ioIndex += i;
					reader.available -= i;
				}
				return value;
			}
			if (value != null)
			{
				return value;
			}
			return ProtoReader.EmptyBlob;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00002B45 File Offset: 0x00000D45
		private static int ReadByteOrThrow(Stream source)
		{
			int num = source.ReadByte();
			if (num < 0)
			{
				throw ProtoReader.EoF(null);
			}
			return num;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000C8F8 File Offset: 0x0000AAF8
		public static int ReadLengthPrefix(Stream source, bool expectHeader, PrefixStyle style, out int fieldNumber)
		{
			int num;
			return ProtoReader.ReadLengthPrefix(source, expectHeader, style, out fieldNumber, out num);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00002B58 File Offset: 0x00000D58
		public static int DirectReadLittleEndianInt32(Stream source)
		{
			return ProtoReader.ReadByteOrThrow(source) | ProtoReader.ReadByteOrThrow(source) << 8 | ProtoReader.ReadByteOrThrow(source) << 16 | ProtoReader.ReadByteOrThrow(source) << 24;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00002B7D File Offset: 0x00000D7D
		public static int DirectReadBigEndianInt32(Stream source)
		{
			return ProtoReader.ReadByteOrThrow(source) << 24 | ProtoReader.ReadByteOrThrow(source) << 16 | ProtoReader.ReadByteOrThrow(source) << 8 | ProtoReader.ReadByteOrThrow(source);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000C910 File Offset: 0x0000AB10
		public static int DirectReadVarintInt32(Stream source)
		{
			uint result;
			if (ProtoReader.TryReadUInt32Variant(source, out result) <= 0)
			{
				throw ProtoReader.EoF(null);
			}
			return (int)result;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000C930 File Offset: 0x0000AB30
		public static void DirectReadBytes(Stream source, byte[] buffer, int offset, int count)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			int num;
			while (count > 0 && (num = source.Read(buffer, offset, count)) > 0)
			{
				count -= num;
				offset += num;
			}
			if (count > 0)
			{
				throw ProtoReader.EoF(null);
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000C974 File Offset: 0x0000AB74
		public static byte[] DirectReadBytes(Stream source, int count)
		{
			byte[] array = new byte[count];
			ProtoReader.DirectReadBytes(source, array, 0, count);
			return array;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000C994 File Offset: 0x0000AB94
		public static string DirectReadString(Stream source, int length)
		{
			byte[] array = new byte[length];
			ProtoReader.DirectReadBytes(source, array, 0, length);
			return Encoding.UTF8.GetString(array, 0, length);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000C9C0 File Offset: 0x0000ABC0
		public static int ReadLengthPrefix(Stream source, bool expectHeader, PrefixStyle style, out int fieldNumber, out int bytesRead)
		{
			fieldNumber = 0;
			switch (style)
			{
			case PrefixStyle.None:
				bytesRead = 0;
				return int.MaxValue;
			case PrefixStyle.Base128:
				bytesRead = 0;
				if (expectHeader)
				{
					uint num2;
					int num = ProtoReader.TryReadUInt32Variant(source, out num2);
					bytesRead += num;
					if (num <= 0)
					{
						bytesRead = 0;
						return -1;
					}
					if ((num2 & 7u) != 2u)
					{
						throw new InvalidOperationException();
					}
					fieldNumber = (int)(num2 >> 3);
					num = ProtoReader.TryReadUInt32Variant(source, out num2);
					bytesRead += num;
					if (bytesRead == 0)
					{
						throw ProtoReader.EoF(null);
					}
					return (int)num2;
				}
				else
				{
					uint num2;
					int num = ProtoReader.TryReadUInt32Variant(source, out num2);
					bytesRead += num;
					if (bytesRead >= 0)
					{
						return (int)num2;
					}
					return -1;
				}
				break;
			case PrefixStyle.Fixed32:
			{
				int num3 = source.ReadByte();
				if (num3 < 0)
				{
					bytesRead = 0;
					return -1;
				}
				bytesRead = 4;
				return num3 | ProtoReader.ReadByteOrThrow(source) << 8 | ProtoReader.ReadByteOrThrow(source) << 16 | ProtoReader.ReadByteOrThrow(source) << 24;
			}
			case PrefixStyle.Fixed32BigEndian:
			{
				int num4 = source.ReadByte();
				if (num4 < 0)
				{
					bytesRead = 0;
					return -1;
				}
				bytesRead = 4;
				return num4 << 24 | ProtoReader.ReadByteOrThrow(source) << 16 | ProtoReader.ReadByteOrThrow(source) << 8 | ProtoReader.ReadByteOrThrow(source);
			}
			default:
				throw new ArgumentOutOfRangeException("style");
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000CAD4 File Offset: 0x0000ACD4
		private static int TryReadUInt32Variant(Stream source, out uint value)
		{
			value = 0u;
			int num = source.ReadByte();
			if (num < 0)
			{
				return 0;
			}
			value = (uint)num;
			if ((value & 128u) == 0u)
			{
				return 1;
			}
			value &= 127u;
			num = source.ReadByte();
			if (num < 0)
			{
				throw ProtoReader.EoF(null);
			}
			value |= (uint)((uint)(num & 127) << 7);
			if ((num & 128) == 0)
			{
				return 2;
			}
			num = source.ReadByte();
			if (num < 0)
			{
				throw ProtoReader.EoF(null);
			}
			value |= (uint)((uint)(num & 127) << 14);
			if ((num & 128) == 0)
			{
				return 3;
			}
			num = source.ReadByte();
			if (num < 0)
			{
				throw ProtoReader.EoF(null);
			}
			value |= (uint)((uint)(num & 127) << 21);
			if ((num & 128) == 0)
			{
				return 4;
			}
			num = source.ReadByte();
			if (num < 0)
			{
				throw ProtoReader.EoF(null);
			}
			value |= (uint)((uint)num << 28);
			if ((num & 240) == 0)
			{
				return 5;
			}
			throw new OverflowException();
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000CBAC File Offset: 0x0000ADAC
		internal static void Seek(Stream source, int count, byte[] buffer)
		{
			if (source.CanSeek)
			{
				source.Seek((long)count, SeekOrigin.Current);
				count = 0;
			}
			else if (buffer != null)
			{
				while (count > buffer.Length)
				{
					int num;
					if ((num = source.Read(buffer, 0, buffer.Length)) <= 0)
					{
						break;
					}
					count -= num;
				}
				while (count > 0)
				{
					int num;
					if ((num = source.Read(buffer, 0, count)) <= 0)
					{
						break;
					}
					count -= num;
				}
			}
			else
			{
				buffer = BufferPool.GetBuffer();
				try
				{
					int num2;
					while (count > buffer.Length)
					{
						if ((num2 = source.Read(buffer, 0, buffer.Length)) <= 0)
						{
							break;
						}
						count -= num2;
					}
					while (count > 0 && (num2 = source.Read(buffer, 0, count)) > 0)
					{
						count -= num2;
					}
				}
				finally
				{
					BufferPool.ReleaseBufferToPool(ref buffer);
				}
			}
			if (count > 0)
			{
				throw ProtoReader.EoF(null);
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000CC70 File Offset: 0x0000AE70
		internal static Exception AddErrorData(Exception exception, ProtoReader source)
		{
			if (exception != null && source != null && !exception.Data.Contains("protoSource"))
			{
				exception.Data.Add("protoSource", string.Format("tag={0}; wire-type={1}; offset={2}; depth={3}", new object[]
				{
					source.fieldNumber,
					source.wireType,
					source.position,
					source.depth
				}));
			}
			return exception;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00002BA2 File Offset: 0x00000DA2
		private static Exception EoF(ProtoReader source)
		{
			return ProtoReader.AddErrorData(new EndOfStreamException(), source);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000CCF0 File Offset: 0x0000AEF0
		public void AppendExtensionData(IExtensible instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			IExtension extensionObject = instance.GetExtensionObject(true);
			bool commit = false;
			Stream stream = extensionObject.BeginAppend();
			try
			{
				using (ProtoWriter protoWriter = new ProtoWriter(stream, this.model, null))
				{
					this.AppendExtensionField(protoWriter);
					protoWriter.Close();
				}
				commit = true;
			}
			finally
			{
				extensionObject.EndAppend(stream, commit);
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000CD6C File Offset: 0x0000AF6C
		private void AppendExtensionField(ProtoWriter writer)
		{
			ProtoWriter.WriteFieldHeader(this.fieldNumber, this.wireType, writer);
			switch (this.wireType)
			{
			case WireType.Variant:
			case WireType.Fixed64:
			case WireType.SignedVariant:
				ProtoWriter.WriteInt64(this.ReadInt64(), writer);
				return;
			case WireType.String:
				ProtoWriter.WriteBytes(ProtoReader.AppendBytes(null, this), writer);
				return;
			case WireType.StartGroup:
			{
				SubItemToken token = ProtoReader.StartSubItem(this);
				SubItemToken token2 = ProtoWriter.StartSubItem(null, writer);
				while (this.ReadFieldHeader() > 0)
				{
					this.AppendExtensionField(writer);
				}
				ProtoReader.EndSubItem(token, this);
				ProtoWriter.EndSubItem(token2, writer);
				return;
			}
			case WireType.Fixed32:
				ProtoWriter.WriteInt32(this.ReadInt32(), writer);
				return;
			}
			throw this.CreateWireTypeException();
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00002BAF File Offset: 0x00000DAF
		public static bool HasSubValue(WireType wireType, ProtoReader source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (source.blockEnd <= source.position || wireType == WireType.EndGroup)
			{
				return false;
			}
			source.wireType = wireType;
			return true;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00002BDB File Offset: 0x00000DDB
		internal int GetTypeKey(ref Type type)
		{
			return this.model.GetKey(ref type);
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00002BE9 File Offset: 0x00000DE9
		internal NetObjectCache NetCache
		{
			get
			{
				return this.netCache;
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00002BF1 File Offset: 0x00000DF1
		internal Type DeserializeType(string value)
		{
			return TypeModel.DeserializeType(this.model, value);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00002BFF File Offset: 0x00000DFF
		internal void SetRootObject(object value)
		{
			this.netCache.SetKeyedObject(0, value);
			this.trapCount -= 1u;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00002C1C File Offset: 0x00000E1C
		public static void NoteObject(object value, ProtoReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (reader.trapCount != 0u)
			{
				reader.netCache.RegisterTrappedObject(value);
				reader.trapCount -= 1u;
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00002C4E File Offset: 0x00000E4E
		public Type ReadType()
		{
			return TypeModel.DeserializeType(this.model, this.ReadString());
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00002C61 File Offset: 0x00000E61
		internal void TrapNextObject(int newObjectKey)
		{
			this.trapCount += 1u;
			this.netCache.SetKeyedObject(newObjectKey, null);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00002C7E File Offset: 0x00000E7E
		internal void CheckFullyConsumed()
		{
			if (this.isFixedLength)
			{
				if (this.dataRemaining != 0)
				{
					throw new ProtoException("Incorrect number of bytes consumed");
				}
			}
			else if (this.available != 0)
			{
				throw new ProtoException("Unconsumed data left in the buffer; this suggests corrupt input");
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000CE24 File Offset: 0x0000B024
		public static object Merge(ProtoReader parent, object from, object to)
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			TypeModel typeModel = parent.Model;
			SerializationContext serializationContext = parent.Context;
			if (typeModel == null)
			{
				throw new InvalidOperationException("Types cannot be merged unless a type-model has been specified");
			}
			object result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				typeModel.Serialize(memoryStream, from, serializationContext);
				memoryStream.Position = 0L;
				result = typeModel.Deserialize(memoryStream, to, null);
			}
			return result;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000CE9C File Offset: 0x0000B09C
		internal static ProtoReader Create(Stream source, TypeModel model, SerializationContext context, int len)
		{
			ProtoReader recycled = ProtoReader.GetRecycled();
			if (recycled == null)
			{
				return new ProtoReader(source, model, context, len);
			}
			ProtoReader.Init(recycled, source, model, context, len);
			return recycled;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00002CAE File Offset: 0x00000EAE
		private static ProtoReader GetRecycled()
		{
			ProtoReader result = ProtoReader.lastReader;
			ProtoReader.lastReader = null;
			return result;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00002CBB File Offset: 0x00000EBB
		internal static void Recycle(ProtoReader reader)
		{
			if (reader != null)
			{
				reader.Dispose();
				ProtoReader.lastReader = reader;
			}
		}

		// Token: 0x040000A3 RID: 163
		private Stream source;

		// Token: 0x040000A4 RID: 164
		private byte[] ioBuffer;

		// Token: 0x040000A5 RID: 165
		private TypeModel model;

		// Token: 0x040000A6 RID: 166
		private int fieldNumber;

		// Token: 0x040000A7 RID: 167
		private int depth;

		// Token: 0x040000A8 RID: 168
		private int dataRemaining;

		// Token: 0x040000A9 RID: 169
		private int ioIndex;

		// Token: 0x040000AA RID: 170
		private int position;

		// Token: 0x040000AB RID: 171
		private int available;

		// Token: 0x040000AC RID: 172
		private int blockEnd;

		// Token: 0x040000AD RID: 173
		private WireType wireType;

		// Token: 0x040000AE RID: 174
		private bool isFixedLength;

		// Token: 0x040000AF RID: 175
		private bool internStrings;

		// Token: 0x040000B0 RID: 176
		private NetObjectCache netCache;

		// Token: 0x040000B1 RID: 177
		private uint trapCount;

		// Token: 0x040000B2 RID: 178
		internal const int TO_EOF = -1;

		// Token: 0x040000B3 RID: 179
		private SerializationContext context;

		// Token: 0x040000B4 RID: 180
		private const long Int64Msb = -9223372036854775808L;

		// Token: 0x040000B5 RID: 181
		private const int Int32Msb = -2147483648;

		// Token: 0x040000B6 RID: 182
		private Dictionary<string, string> stringInterner;

		// Token: 0x040000B7 RID: 183
		private static readonly UTF8Encoding encoding = new UTF8Encoding();

		// Token: 0x040000B8 RID: 184
		private static readonly byte[] EmptyBlob = new byte[0];

		// Token: 0x040000B9 RID: 185
		[ThreadStatic]
		private static ProtoReader lastReader;
	}
}
