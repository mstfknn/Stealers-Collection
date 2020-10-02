using System;
using System.IO;
using System.Text;
using ProtoBuf.Meta;

namespace ProtoBuf
{
	// Token: 0x0200002D RID: 45
	public sealed class ProtoWriter : IDisposable
	{
		// Token: 0x06000137 RID: 311 RVA: 0x0000CEC8 File Offset: 0x0000B0C8
		public static void WriteObject(object value, int key, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (writer.model == null)
			{
				throw new InvalidOperationException("Cannot serialize sub-objects unless a model is provided");
			}
			SubItemToken token = ProtoWriter.StartSubItem(value, writer);
			if (key >= 0)
			{
				writer.model.Serialize(key, value, writer);
			}
			else if (writer.model == null || !writer.model.TrySerializeAuxiliaryType(writer, value.GetType(), DataFormat.Default, 1, value, false))
			{
				TypeModel.ThrowUnexpectedType(value.GetType());
			}
			ProtoWriter.EndSubItem(token, writer);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00002CE3 File Offset: 0x00000EE3
		public static void WriteRecursionSafeObject(object value, int key, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (writer.model == null)
			{
				throw new InvalidOperationException("Cannot serialize sub-objects unless a model is provided");
			}
			SubItemToken token = ProtoWriter.StartSubItem(null, writer);
			writer.model.Serialize(key, value, writer);
			ProtoWriter.EndSubItem(token, writer);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000CF44 File Offset: 0x0000B144
		internal static void WriteObject(object value, int key, ProtoWriter writer, PrefixStyle style, int fieldNumber)
		{
			if (writer.model == null)
			{
				throw new InvalidOperationException("Cannot serialize sub-objects unless a model is provided");
			}
			if (writer.wireType != WireType.None)
			{
				throw ProtoWriter.CreateException(writer);
			}
			if (style != PrefixStyle.Base128)
			{
				if (style - PrefixStyle.Fixed32 > 1)
				{
					throw new ArgumentOutOfRangeException("style");
				}
				writer.fieldNumber = 0;
				writer.wireType = WireType.Fixed32;
			}
			else
			{
				writer.wireType = WireType.String;
				writer.fieldNumber = fieldNumber;
				if (fieldNumber > 0)
				{
					ProtoWriter.WriteHeaderCore(fieldNumber, WireType.String, writer);
				}
			}
			SubItemToken token = ProtoWriter.StartSubItem(value, writer, true);
			if (key < 0)
			{
				if (!writer.model.TrySerializeAuxiliaryType(writer, value.GetType(), DataFormat.Default, 1, value, false))
				{
					TypeModel.ThrowUnexpectedType(value.GetType());
				}
			}
			else
			{
				writer.model.Serialize(key, value, writer);
			}
			ProtoWriter.EndSubItem(token, writer, style);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00002D21 File Offset: 0x00000F21
		internal int GetTypeKey(ref Type type)
		{
			return this.model.GetKey(ref type);
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00002D2F File Offset: 0x00000F2F
		internal NetObjectCache NetCache
		{
			get
			{
				return this.netCache;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00002D37 File Offset: 0x00000F37
		internal WireType WireType
		{
			get
			{
				return this.wireType;
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000D000 File Offset: 0x0000B200
		public static void WriteFieldHeader(int fieldNumber, WireType wireType, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (writer.wireType != WireType.None)
			{
				throw new InvalidOperationException(string.Concat(new string[]
				{
					"Cannot write a ",
					wireType.ToString(),
					" header until the ",
					writer.wireType.ToString(),
					" data has been written"
				}));
			}
			if (fieldNumber < 0)
			{
				throw new ArgumentOutOfRangeException("fieldNumber");
			}
			if (writer.packedFieldNumber == 0)
			{
				writer.fieldNumber = fieldNumber;
				writer.wireType = wireType;
				ProtoWriter.WriteHeaderCore(fieldNumber, wireType, writer);
				return;
			}
			if (writer.packedFieldNumber != fieldNumber)
			{
				throw new InvalidOperationException("Field mismatch during packed encoding; expected " + writer.packedFieldNumber.ToString() + " but received " + fieldNumber.ToString());
			}
			if (wireType > WireType.Fixed64 && wireType != WireType.Fixed32 && wireType != WireType.SignedVariant)
			{
				throw new InvalidOperationException("Wire-type cannot be encoded as packed: " + wireType.ToString());
			}
			writer.fieldNumber = fieldNumber;
			writer.wireType = wireType;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00002D3F File Offset: 0x00000F3F
		internal static void WriteHeaderCore(int fieldNumber, WireType wireType, ProtoWriter writer)
		{
			ProtoWriter.WriteUInt32Variant((uint)(fieldNumber << 3 | (int)(wireType & (WireType)7)), writer);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00002D4E File Offset: 0x00000F4E
		public static void WriteBytes(byte[] data, ProtoWriter writer)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			ProtoWriter.WriteBytes(data, 0, data.Length, writer);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000D108 File Offset: 0x0000B308
		public static void WriteBytes(byte[] data, int offset, int length, ProtoWriter writer)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			switch (writer.wireType)
			{
			case WireType.Fixed64:
				if (length != 8)
				{
					throw new ArgumentException("length");
				}
				goto IL_AE;
			case WireType.String:
				ProtoWriter.WriteUInt32Variant((uint)length, writer);
				writer.wireType = WireType.None;
				if (length == 0)
				{
					return;
				}
				if (writer.flushLock == 0 && length > writer.ioBuffer.Length)
				{
					ProtoWriter.Flush(writer);
					writer.dest.Write(data, offset, length);
					writer.position += length;
					return;
				}
				goto IL_AE;
			case WireType.Fixed32:
				if (length != 4)
				{
					throw new ArgumentException("length");
				}
				goto IL_AE;
			}
			throw ProtoWriter.CreateException(writer);
			IL_AE:
			ProtoWriter.DemandSpace(length, writer);
			Helpers.BlockCopy(data, offset, writer.ioBuffer, writer.ioIndex, length);
			ProtoWriter.IncrementedAndReset(length, writer);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000D1E8 File Offset: 0x0000B3E8
		private static void CopyRawFromStream(Stream source, ProtoWriter writer)
		{
			byte[] array = writer.ioBuffer;
			int num = array.Length - writer.ioIndex;
			int num2 = 1;
			while (num > 0 && (num2 = source.Read(array, writer.ioIndex, num)) > 0)
			{
				writer.ioIndex += num2;
				writer.position += num2;
				num -= num2;
			}
			if (num2 <= 0)
			{
				return;
			}
			if (writer.flushLock == 0)
			{
				ProtoWriter.Flush(writer);
				while ((num2 = source.Read(array, 0, array.Length)) > 0)
				{
					writer.dest.Write(array, 0, num2);
					writer.position += num2;
				}
				return;
			}
			for (;;)
			{
				ProtoWriter.DemandSpace(128, writer);
				if ((num2 = source.Read(writer.ioBuffer, writer.ioIndex, writer.ioBuffer.Length - writer.ioIndex)) <= 0)
				{
					break;
				}
				writer.position += num2;
				writer.ioIndex += num2;
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00002D69 File Offset: 0x00000F69
		private static void IncrementedAndReset(int length, ProtoWriter writer)
		{
			writer.ioIndex += length;
			writer.position += length;
			writer.wireType = WireType.None;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00002D8E File Offset: 0x00000F8E
		public static SubItemToken StartSubItem(object instance, ProtoWriter writer)
		{
			return ProtoWriter.StartSubItem(instance, writer, false);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000D2D4 File Offset: 0x0000B4D4
		private void CheckRecursionStackAndPush(object instance)
		{
			int num;
			if (this.recursionStack == null)
			{
				this.recursionStack = new MutableList();
			}
			else if (instance != null && (num = this.recursionStack.IndexOfReference(instance)) >= 0)
			{
				throw new ProtoException("Possible recursion detected (offset: " + (this.recursionStack.Count - num).ToString() + " level(s)): " + instance.ToString());
			}
			this.recursionStack.Add(instance);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00002D98 File Offset: 0x00000F98
		private void PopRecursionStack()
		{
			this.recursionStack.RemoveLast();
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000D348 File Offset: 0x0000B548
		private static SubItemToken StartSubItem(object instance, ProtoWriter writer, bool allowFixed)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			int num = writer.depth + 1;
			writer.depth = num;
			if (num > 25)
			{
				writer.CheckRecursionStackAndPush(instance);
			}
			if (writer.packedFieldNumber != 0)
			{
				throw new InvalidOperationException("Cannot begin a sub-item while performing packed encoding");
			}
			switch (writer.wireType)
			{
			case WireType.String:
				writer.wireType = WireType.None;
				ProtoWriter.DemandSpace(32, writer);
				writer.flushLock++;
				writer.position++;
				num = writer.ioIndex;
				writer.ioIndex = num + 1;
				return new SubItemToken(num);
			case WireType.StartGroup:
				writer.wireType = WireType.None;
				return new SubItemToken(-writer.fieldNumber);
			case WireType.Fixed32:
			{
				if (!allowFixed)
				{
					throw ProtoWriter.CreateException(writer);
				}
				ProtoWriter.DemandSpace(32, writer);
				writer.flushLock++;
				SubItemToken result = new SubItemToken(writer.ioIndex);
				ProtoWriter.IncrementedAndReset(4, writer);
				return result;
			}
			}
			throw ProtoWriter.CreateException(writer);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00002DA5 File Offset: 0x00000FA5
		public static void EndSubItem(SubItemToken token, ProtoWriter writer)
		{
			ProtoWriter.EndSubItem(token, writer, PrefixStyle.Base128);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000D448 File Offset: 0x0000B648
		private static void EndSubItem(SubItemToken token, ProtoWriter writer, PrefixStyle style)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (writer.wireType != WireType.None)
			{
				throw ProtoWriter.CreateException(writer);
			}
			int value = token.value;
			if (writer.depth <= 0)
			{
				throw ProtoWriter.CreateException(writer);
			}
			int num = writer.depth;
			writer.depth = num - 1;
			if (num > 25)
			{
				writer.PopRecursionStack();
			}
			writer.packedFieldNumber = 0;
			if (value < 0)
			{
				ProtoWriter.WriteHeaderCore(-value, WireType.EndGroup, writer);
				writer.wireType = WireType.None;
				return;
			}
			switch (style)
			{
			case PrefixStyle.Base128:
			{
				int num2 = writer.ioIndex - value - 1;
				int num3 = 0;
				uint num4 = (uint)num2;
				while ((num4 >>= 7) != 0u)
				{
					num3++;
				}
				if (num3 == 0)
				{
					writer.ioBuffer[value] = (byte)(num2 & 127);
				}
				else
				{
					ProtoWriter.DemandSpace(num3, writer);
					byte[] array = writer.ioBuffer;
					Helpers.BlockCopy(array, value + 1, array, value + 1 + num3, num2);
					num4 = (uint)num2;
					do
					{
						array[value++] = (byte)((num4 & 127u) | 128u);
					}
					while ((num4 >>= 7) != 0u);
					array[value - 1] = (byte)((int)array[value - 1] & -129);
					writer.position += num3;
					writer.ioIndex += num3;
				}
				break;
			}
			case PrefixStyle.Fixed32:
			{
				int num2 = writer.ioIndex - value - 4;
				ProtoWriter.WriteInt32ToBuffer(num2, writer.ioBuffer, value);
				break;
			}
			case PrefixStyle.Fixed32BigEndian:
			{
				int num2 = writer.ioIndex - value - 4;
				byte[] array2 = writer.ioBuffer;
				ProtoWriter.WriteInt32ToBuffer(num2, array2, value);
				byte b = array2[value];
				array2[value] = array2[value + 3];
				array2[value + 3] = b;
				b = array2[value + 1];
				array2[value + 1] = array2[value + 2];
				array2[value + 2] = b;
				break;
			}
			default:
				throw new ArgumentOutOfRangeException("style");
			}
			num = writer.flushLock - 1;
			writer.flushLock = num;
			if (num == 0 && writer.ioIndex >= 1024)
			{
				ProtoWriter.Flush(writer);
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000D624 File Offset: 0x0000B824
		public ProtoWriter(Stream dest, TypeModel model, SerializationContext context)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			if (!dest.CanWrite)
			{
				throw new ArgumentException("Cannot write to stream", "dest");
			}
			this.dest = dest;
			this.ioBuffer = BufferPool.GetBuffer();
			this.model = model;
			this.wireType = WireType.None;
			if (context == null)
			{
				context = SerializationContext.Default;
			}
			else
			{
				context.Freeze();
			}
			this.context = context;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00002DAF File Offset: 0x00000FAF
		public SerializationContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00002DB7 File Offset: 0x00000FB7
		void IDisposable.Dispose()
		{
			this.Dispose();
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00002DBF File Offset: 0x00000FBF
		public void Dispose()
		{
			if (this.dest != null)
			{
				ProtoWriter.Flush(this);
				this.dest.Dispose();
				this.dest = null;
			}
			this.model = null;
			BufferPool.ReleaseBufferToPool(ref this.ioBuffer);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00002DF3 File Offset: 0x00000FF3
		internal static int GetPosition(ProtoWriter writer)
		{
			return writer.position;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000D6A4 File Offset: 0x0000B8A4
		private static void DemandSpace(int required, ProtoWriter writer)
		{
			if (writer.ioBuffer.Length - writer.ioIndex < required)
			{
				if (writer.flushLock == 0)
				{
					ProtoWriter.Flush(writer);
					if (writer.ioBuffer.Length - writer.ioIndex >= required)
					{
						return;
					}
				}
				BufferPool.ResizeAndFlushLeft(ref writer.ioBuffer, required + writer.ioIndex, 0, writer.ioIndex);
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00002DFB File Offset: 0x00000FFB
		public void Close()
		{
			if (this.depth != 0 || this.flushLock != 0)
			{
				throw new InvalidOperationException("Unable to close stream in an incomplete state");
			}
			this.Dispose();
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00002E1E File Offset: 0x0000101E
		internal void CheckDepthFlushlock()
		{
			if (this.depth != 0 || this.flushLock != 0)
			{
				throw new InvalidOperationException("The writer is in an incomplete state");
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00002E3B File Offset: 0x0000103B
		public TypeModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00002E43 File Offset: 0x00001043
		internal static void Flush(ProtoWriter writer)
		{
			if (writer.flushLock == 0 && writer.ioIndex != 0)
			{
				writer.dest.Write(writer.ioBuffer, 0, writer.ioIndex);
				writer.ioIndex = 0;
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000D700 File Offset: 0x0000B900
		private static void WriteUInt32Variant(uint value, ProtoWriter writer)
		{
			ProtoWriter.DemandSpace(5, writer);
			int num = 0;
			do
			{
				byte[] array = writer.ioBuffer;
				int num2 = writer.ioIndex;
				writer.ioIndex = num2 + 1;
				array[num2] = (byte)((value & 127u) | 128u);
				num++;
			}
			while ((value >>= 7) != 0u);
			byte[] array2 = writer.ioBuffer;
			int num3 = writer.ioIndex - 1;
			array2[num3] &= 127;
			writer.position += num;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00002E74 File Offset: 0x00001074
		internal static uint Zig(int value)
		{
			return (uint)(value << 1 ^ value >> 31);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00002E7E File Offset: 0x0000107E
		internal static ulong Zig(long value)
		{
			return (ulong)(value << 1 ^ value >> 63);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000D770 File Offset: 0x0000B970
		private static void WriteUInt64Variant(ulong value, ProtoWriter writer)
		{
			ProtoWriter.DemandSpace(10, writer);
			int num = 0;
			do
			{
				byte[] array = writer.ioBuffer;
				int num2 = writer.ioIndex;
				writer.ioIndex = num2 + 1;
				array[num2] = (byte)((value & 127UL) | 128UL);
				num++;
			}
			while ((value >>= 7) != 0UL);
			byte[] array2 = writer.ioBuffer;
			int num3 = writer.ioIndex - 1;
			array2[num3] &= 127;
			writer.position += num;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000D7E0 File Offset: 0x0000B9E0
		public static void WriteString(string value, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (writer.wireType != WireType.String)
			{
				throw ProtoWriter.CreateException(writer);
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.Length == 0)
			{
				ProtoWriter.WriteUInt32Variant(0u, writer);
				writer.wireType = WireType.None;
				return;
			}
			int byteCount = ProtoWriter.encoding.GetByteCount(value);
			ProtoWriter.WriteUInt32Variant((uint)byteCount, writer);
			ProtoWriter.DemandSpace(byteCount, writer);
			ProtoWriter.IncrementedAndReset(ProtoWriter.encoding.GetBytes(value, 0, value.Length, writer.ioBuffer, writer.ioIndex), writer);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000D86C File Offset: 0x0000BA6C
		public static void WriteUInt64(ulong value, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			WireType wireType = writer.wireType;
			if (wireType == WireType.Variant)
			{
				ProtoWriter.WriteUInt64Variant(value, writer);
				writer.wireType = WireType.None;
				return;
			}
			if (wireType == WireType.Fixed64)
			{
				ProtoWriter.WriteInt64((long)value, writer);
				return;
			}
			if (wireType != WireType.Fixed32)
			{
				throw ProtoWriter.CreateException(writer);
			}
			ProtoWriter.WriteUInt32(checked((uint)value), writer);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000D8C4 File Offset: 0x0000BAC4
		public static void WriteInt64(long value, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			WireType wireType = writer.wireType;
			if (wireType <= WireType.Fixed64)
			{
				if (wireType != WireType.Variant)
				{
					if (wireType == WireType.Fixed64)
					{
						ProtoWriter.DemandSpace(8, writer);
						byte[] array = writer.ioBuffer;
						int num = writer.ioIndex;
						array[num] = (byte)value;
						array[num + 1] = (byte)(value >> 8);
						array[num + 2] = (byte)(value >> 16);
						array[num + 3] = (byte)(value >> 24);
						array[num + 4] = (byte)(value >> 32);
						array[num + 5] = (byte)(value >> 40);
						array[num + 6] = (byte)(value >> 48);
						array[num + 7] = (byte)(value >> 56);
						ProtoWriter.IncrementedAndReset(8, writer);
						return;
					}
				}
				else
				{
					if (value >= 0L)
					{
						ProtoWriter.WriteUInt64Variant((ulong)value, writer);
						writer.wireType = WireType.None;
						return;
					}
					ProtoWriter.DemandSpace(10, writer);
					byte[] array2 = writer.ioBuffer;
					int num = writer.ioIndex;
					array2[num] = (byte)(value | 128L);
					array2[num + 1] = (byte)((int)(value >> 7) | 128);
					array2[num + 2] = (byte)((int)(value >> 14) | 128);
					array2[num + 3] = (byte)((int)(value >> 21) | 128);
					array2[num + 4] = (byte)((int)(value >> 28) | 128);
					array2[num + 5] = (byte)((int)(value >> 35) | 128);
					array2[num + 6] = (byte)((int)(value >> 42) | 128);
					array2[num + 7] = (byte)((int)(value >> 49) | 128);
					array2[num + 8] = (byte)((int)(value >> 56) | 128);
					array2[num + 9] = 1;
					ProtoWriter.IncrementedAndReset(10, writer);
					return;
				}
			}
			else
			{
				if (wireType == WireType.Fixed32)
				{
					ProtoWriter.WriteInt32(checked((int)value), writer);
					return;
				}
				if (wireType == WireType.SignedVariant)
				{
					ProtoWriter.WriteUInt64Variant(ProtoWriter.Zig(value), writer);
					writer.wireType = WireType.None;
					return;
				}
			}
			throw ProtoWriter.CreateException(writer);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000DA5C File Offset: 0x0000BC5C
		public static void WriteUInt32(uint value, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			WireType wireType = writer.wireType;
			if (wireType == WireType.Variant)
			{
				ProtoWriter.WriteUInt32Variant(value, writer);
				writer.wireType = WireType.None;
				return;
			}
			if (wireType == WireType.Fixed64)
			{
				ProtoWriter.WriteInt64((long)value, writer);
				return;
			}
			if (wireType == WireType.Fixed32)
			{
				ProtoWriter.WriteInt32((int)value, writer);
				return;
			}
			throw ProtoWriter.CreateException(writer);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00002E88 File Offset: 0x00001088
		public static void WriteInt16(short value, ProtoWriter writer)
		{
			ProtoWriter.WriteInt32((int)value, writer);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00002E91 File Offset: 0x00001091
		public static void WriteUInt16(ushort value, ProtoWriter writer)
		{
			ProtoWriter.WriteUInt32((uint)value, writer);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00002E91 File Offset: 0x00001091
		public static void WriteByte(byte value, ProtoWriter writer)
		{
			ProtoWriter.WriteUInt32((uint)value, writer);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00002E88 File Offset: 0x00001088
		public static void WriteSByte(sbyte value, ProtoWriter writer)
		{
			ProtoWriter.WriteInt32((int)value, writer);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00002E9A File Offset: 0x0000109A
		private static void WriteInt32ToBuffer(int value, byte[] buffer, int index)
		{
			buffer[index] = (byte)value;
			buffer[index + 1] = (byte)(value >> 8);
			buffer[index + 2] = (byte)(value >> 16);
			buffer[index + 3] = (byte)(value >> 24);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000DAB0 File Offset: 0x0000BCB0
		public static void WriteInt32(int value, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			WireType wireType = writer.wireType;
			if (wireType <= WireType.Fixed64)
			{
				if (wireType != WireType.Variant)
				{
					if (wireType == WireType.Fixed64)
					{
						ProtoWriter.DemandSpace(8, writer);
						byte[] array = writer.ioBuffer;
						int num = writer.ioIndex;
						array[num] = (byte)value;
						array[num + 1] = (byte)(value >> 8);
						array[num + 2] = (byte)(value >> 16);
						array[num + 3] = (byte)(value >> 24);
						array[num + 4] = (array[num + 5] = (array[num + 6] = (array[num + 7] = 0)));
						ProtoWriter.IncrementedAndReset(8, writer);
						return;
					}
				}
				else
				{
					if (value >= 0)
					{
						ProtoWriter.WriteUInt32Variant((uint)value, writer);
						writer.wireType = WireType.None;
						return;
					}
					ProtoWriter.DemandSpace(10, writer);
					byte[] array = writer.ioBuffer;
					int num = writer.ioIndex;
					array[num] = (byte)(value | 128);
					array[num + 1] = (byte)(value >> 7 | 128);
					array[num + 2] = (byte)(value >> 14 | 128);
					array[num + 3] = (byte)(value >> 21 | 128);
					array[num + 4] = (byte)(value >> 28 | 128);
					array[num + 5] = (array[num + 6] = (array[num + 7] = (array[num + 8] = byte.MaxValue)));
					array[num + 9] = 1;
					ProtoWriter.IncrementedAndReset(10, writer);
					return;
				}
			}
			else
			{
				if (wireType == WireType.Fixed32)
				{
					ProtoWriter.DemandSpace(4, writer);
					ProtoWriter.WriteInt32ToBuffer(value, writer.ioBuffer, writer.ioIndex);
					ProtoWriter.IncrementedAndReset(4, writer);
					return;
				}
				if (wireType == WireType.SignedVariant)
				{
					ProtoWriter.WriteUInt32Variant(ProtoWriter.Zig(value), writer);
					writer.wireType = WireType.None;
					return;
				}
			}
			throw ProtoWriter.CreateException(writer);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000DC34 File Offset: 0x0000BE34
		public unsafe static void WriteDouble(double value, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			WireType wireType = writer.wireType;
			if (wireType == WireType.Fixed64)
			{
				ProtoWriter.WriteInt64(*(long*)(&value), writer);
				return;
			}
			if (wireType != WireType.Fixed32)
			{
				throw ProtoWriter.CreateException(writer);
			}
			float value2 = (float)value;
			if (Helpers.IsInfinity(value2) && !Helpers.IsInfinity(value))
			{
				throw new OverflowException();
			}
			ProtoWriter.WriteSingle(value2, writer);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000DC90 File Offset: 0x0000BE90
		public unsafe static void WriteSingle(float value, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			WireType wireType = writer.wireType;
			if (wireType == WireType.Fixed64)
			{
				ProtoWriter.WriteDouble((double)value, writer);
				return;
			}
			if (wireType == WireType.Fixed32)
			{
				ProtoWriter.WriteInt32(*(int*)(&value), writer);
				return;
			}
			throw ProtoWriter.CreateException(writer);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000DCD4 File Offset: 0x0000BED4
		public static void ThrowEnumException(ProtoWriter writer, object enumValue)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			string str = (enumValue == null) ? "<null>" : (enumValue.GetType().FullName + "." + enumValue.ToString());
			throw new ProtoException("No wire-value is mapped to the enum " + str + " at position " + writer.position.ToString());
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00002EBE File Offset: 0x000010BE
		internal static Exception CreateException(ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			return new ProtoException("Invalid serialization operation with wire-type " + writer.wireType.ToString() + " at position " + writer.position.ToString());
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00002EFE File Offset: 0x000010FE
		public static void WriteBoolean(bool value, ProtoWriter writer)
		{
			ProtoWriter.WriteUInt32(value ? 1u : 0u, writer);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000DD38 File Offset: 0x0000BF38
		public static void AppendExtensionData(IExtensible instance, ProtoWriter writer)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (writer.wireType != WireType.None)
			{
				throw ProtoWriter.CreateException(writer);
			}
			IExtension extensionObject = instance.GetExtensionObject(false);
			if (extensionObject != null)
			{
				Stream stream = extensionObject.BeginQuery();
				try
				{
					ProtoWriter.CopyRawFromStream(stream, writer);
				}
				finally
				{
					extensionObject.EndQuery(stream);
				}
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00002F0D File Offset: 0x0000110D
		public static void SetPackedField(int fieldNumber, ProtoWriter writer)
		{
			if (fieldNumber <= 0)
			{
				throw new ArgumentOutOfRangeException("fieldNumber");
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.packedFieldNumber = fieldNumber;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00002F33 File Offset: 0x00001133
		internal string SerializeType(Type type)
		{
			return TypeModel.SerializeType(this.model, type);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00002F41 File Offset: 0x00001141
		public void SetRootObject(object value)
		{
			this.NetCache.SetKeyedObject(0, value);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00002F50 File Offset: 0x00001150
		public static void WriteType(Type value, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			ProtoWriter.WriteString(writer.SerializeType(value), writer);
		}

		// Token: 0x040000BA RID: 186
		private Stream dest;

		// Token: 0x040000BB RID: 187
		private TypeModel model;

		// Token: 0x040000BC RID: 188
		private readonly NetObjectCache netCache = new NetObjectCache();

		// Token: 0x040000BD RID: 189
		private int fieldNumber;

		// Token: 0x040000BE RID: 190
		private int flushLock;

		// Token: 0x040000BF RID: 191
		private WireType wireType;

		// Token: 0x040000C0 RID: 192
		private int depth;

		// Token: 0x040000C1 RID: 193
		private const int RecursionCheckDepth = 25;

		// Token: 0x040000C2 RID: 194
		private MutableList recursionStack;

		// Token: 0x040000C3 RID: 195
		private readonly SerializationContext context;

		// Token: 0x040000C4 RID: 196
		private byte[] ioBuffer;

		// Token: 0x040000C5 RID: 197
		private int ioIndex;

		// Token: 0x040000C6 RID: 198
		private int position;

		// Token: 0x040000C7 RID: 199
		private static readonly UTF8Encoding encoding = new UTF8Encoding();

		// Token: 0x040000C8 RID: 200
		private int packedFieldNumber;
	}
}
