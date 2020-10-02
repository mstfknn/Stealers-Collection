using ProtoBuf.Meta;
using System;
using System.IO;
using System.Text;

namespace ProtoBuf
{
	public sealed class ProtoWriter : IDisposable
	{
		private Stream dest;

		private TypeModel model;

		private readonly NetObjectCache netCache = new NetObjectCache();

		private int fieldNumber;

		private int flushLock;

		private WireType wireType;

		private int depth;

		private const int RecursionCheckDepth = 25;

		private MutableList recursionStack;

		private readonly SerializationContext context;

		private byte[] ioBuffer;

		private int ioIndex;

		private int position;

		private static readonly UTF8Encoding encoding = new UTF8Encoding();

		private int packedFieldNumber;

		internal NetObjectCache NetCache => netCache;

		internal WireType WireType => wireType;

		public SerializationContext Context => context;

		public TypeModel Model => model;

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
			SubItemToken token = StartSubItem(value, writer);
			if (key >= 0)
			{
				writer.model.Serialize(key, value, writer);
			}
			else if (writer.model == null || !writer.model.TrySerializeAuxiliaryType(writer, value.GetType(), DataFormat.Default, 1, value, isInsideList: false))
			{
				TypeModel.ThrowUnexpectedType(value.GetType());
			}
			EndSubItem(token, writer);
		}

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
			SubItemToken token = StartSubItem(null, writer);
			writer.model.Serialize(key, value, writer);
			EndSubItem(token, writer);
		}

		internal static void WriteObject(object value, int key, ProtoWriter writer, PrefixStyle style, int fieldNumber)
		{
			if (writer.model == null)
			{
				throw new InvalidOperationException("Cannot serialize sub-objects unless a model is provided");
			}
			if (writer.wireType != WireType.None)
			{
				throw CreateException(writer);
			}
			switch (style)
			{
			case PrefixStyle.Base128:
				writer.wireType = WireType.String;
				writer.fieldNumber = fieldNumber;
				if (fieldNumber > 0)
				{
					WriteHeaderCore(fieldNumber, WireType.String, writer);
				}
				break;
			case PrefixStyle.Fixed32:
			case PrefixStyle.Fixed32BigEndian:
				writer.fieldNumber = 0;
				writer.wireType = WireType.Fixed32;
				break;
			default:
				throw new ArgumentOutOfRangeException("style");
			}
			SubItemToken token = StartSubItem(value, writer, allowFixed: true);
			if (key < 0)
			{
				if (!writer.model.TrySerializeAuxiliaryType(writer, value.GetType(), DataFormat.Default, 1, value, isInsideList: false))
				{
					TypeModel.ThrowUnexpectedType(value.GetType());
				}
			}
			else
			{
				writer.model.Serialize(key, value, writer);
			}
			EndSubItem(token, writer, style);
		}

		internal int GetTypeKey(ref Type type)
		{
			return model.GetKey(ref type);
		}

		public static void WriteFieldHeader(int fieldNumber, WireType wireType, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (writer.wireType != WireType.None)
			{
				throw new InvalidOperationException("Cannot write a " + wireType.ToString() + " header until the " + writer.wireType.ToString() + " data has been written");
			}
			if (fieldNumber < 0)
			{
				throw new ArgumentOutOfRangeException("fieldNumber");
			}
			if (writer.packedFieldNumber == 0)
			{
				writer.fieldNumber = fieldNumber;
				writer.wireType = wireType;
				WriteHeaderCore(fieldNumber, wireType, writer);
				return;
			}
			if (writer.packedFieldNumber == fieldNumber)
			{
				if ((uint)wireType > 1u && wireType != WireType.Fixed32 && wireType != WireType.SignedVariant)
				{
					throw new InvalidOperationException("Wire-type cannot be encoded as packed: " + wireType.ToString());
				}
				writer.fieldNumber = fieldNumber;
				writer.wireType = wireType;
				return;
			}
			throw new InvalidOperationException("Field mismatch during packed encoding; expected " + writer.packedFieldNumber.ToString() + " but received " + fieldNumber.ToString());
		}

		internal static void WriteHeaderCore(int fieldNumber, WireType wireType, ProtoWriter writer)
		{
			WriteUInt32Variant((uint)((fieldNumber << 3) | (int)(wireType & (WireType)7)), writer);
		}

		public static void WriteBytes(byte[] data, ProtoWriter writer)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			WriteBytes(data, 0, data.Length, writer);
		}

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
			case WireType.Fixed32:
				if (length != 4)
				{
					throw new ArgumentException("length");
				}
				break;
			case WireType.Fixed64:
				if (length != 8)
				{
					throw new ArgumentException("length");
				}
				break;
			case WireType.String:
				WriteUInt32Variant((uint)length, writer);
				writer.wireType = WireType.None;
				if (length == 0)
				{
					return;
				}
				if (writer.flushLock == 0 && length > writer.ioBuffer.Length)
				{
					Flush(writer);
					writer.dest.Write(data, offset, length);
					writer.position += length;
					return;
				}
				break;
			default:
				throw CreateException(writer);
			}
			DemandSpace(length, writer);
			Helpers.BlockCopy(data, offset, writer.ioBuffer, writer.ioIndex, length);
			IncrementedAndReset(length, writer);
		}

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
				Flush(writer);
				while ((num2 = source.Read(array, 0, array.Length)) > 0)
				{
					writer.dest.Write(array, 0, num2);
					writer.position += num2;
				}
				return;
			}
			while (true)
			{
				DemandSpace(128, writer);
				if ((num2 = source.Read(writer.ioBuffer, writer.ioIndex, writer.ioBuffer.Length - writer.ioIndex)) > 0)
				{
					writer.position += num2;
					writer.ioIndex += num2;
					continue;
				}
				break;
			}
		}

		private static void IncrementedAndReset(int length, ProtoWriter writer)
		{
			writer.ioIndex += length;
			writer.position += length;
			writer.wireType = WireType.None;
		}

		public static SubItemToken StartSubItem(object instance, ProtoWriter writer)
		{
			return StartSubItem(instance, writer, allowFixed: false);
		}

		private void CheckRecursionStackAndPush(object instance)
		{
			int num;
			if (recursionStack == null)
			{
				recursionStack = new MutableList();
			}
			else if (instance != null && (num = recursionStack.IndexOfReference(instance)) >= 0)
			{
				throw new ProtoException("Possible recursion detected (offset: " + (recursionStack.Count - num).ToString() + " level(s)): " + instance.ToString());
			}
			recursionStack.Add(instance);
		}

		private void PopRecursionStack()
		{
			recursionStack.RemoveLast();
		}

		private static SubItemToken StartSubItem(object instance, ProtoWriter writer, bool allowFixed)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (++writer.depth > 25)
			{
				writer.CheckRecursionStackAndPush(instance);
			}
			if (writer.packedFieldNumber != 0)
			{
				throw new InvalidOperationException("Cannot begin a sub-item while performing packed encoding");
			}
			switch (writer.wireType)
			{
			case WireType.StartGroup:
				writer.wireType = WireType.None;
				return new SubItemToken(-writer.fieldNumber);
			case WireType.String:
				writer.wireType = WireType.None;
				DemandSpace(32, writer);
				writer.flushLock++;
				writer.position++;
				return new SubItemToken(writer.ioIndex++);
			case WireType.Fixed32:
			{
				if (!allowFixed)
				{
					throw CreateException(writer);
				}
				DemandSpace(32, writer);
				writer.flushLock++;
				SubItemToken result = new SubItemToken(writer.ioIndex);
				IncrementedAndReset(4, writer);
				return result;
			}
			default:
				throw CreateException(writer);
			}
		}

		public static void EndSubItem(SubItemToken token, ProtoWriter writer)
		{
			EndSubItem(token, writer, PrefixStyle.Base128);
		}

		private static void EndSubItem(SubItemToken token, ProtoWriter writer, PrefixStyle style)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (writer.wireType != WireType.None)
			{
				throw CreateException(writer);
			}
			int num = token.value;
			if (writer.depth <= 0)
			{
				throw CreateException(writer);
			}
			if (writer.depth-- > 25)
			{
				writer.PopRecursionStack();
			}
			writer.packedFieldNumber = 0;
			if (num < 0)
			{
				WriteHeaderCore(-num, WireType.EndGroup, writer);
				writer.wireType = WireType.None;
				return;
			}
			switch (style)
			{
			case PrefixStyle.Fixed32:
			{
				int num2 = writer.ioIndex - num - 4;
				WriteInt32ToBuffer(num2, writer.ioBuffer, num);
				break;
			}
			case PrefixStyle.Fixed32BigEndian:
			{
				int num2 = writer.ioIndex - num - 4;
				byte[] array2 = writer.ioBuffer;
				WriteInt32ToBuffer(num2, array2, num);
				byte b = array2[num];
				array2[num] = array2[num + 3];
				array2[num + 3] = b;
				b = array2[num + 1];
				array2[num + 1] = array2[num + 2];
				array2[num + 2] = b;
				break;
			}
			case PrefixStyle.Base128:
			{
				int num2 = writer.ioIndex - num - 1;
				int num3 = 0;
				uint num4 = (uint)num2;
				while ((num4 >>= 7) != 0)
				{
					num3++;
				}
				if (num3 == 0)
				{
					writer.ioBuffer[num] = (byte)(num2 & 0x7F);
					break;
				}
				DemandSpace(num3, writer);
				byte[] array = writer.ioBuffer;
				Helpers.BlockCopy(array, num + 1, array, num + 1 + num3, num2);
				num4 = (uint)num2;
				do
				{
					array[num++] = (byte)((num4 & 0x7F) | 0x80);
				}
				while ((num4 >>= 7) != 0);
				array[num - 1] = (byte)(array[num - 1] & -129);
				writer.position += num3;
				writer.ioIndex += num3;
				break;
			}
			default:
				throw new ArgumentOutOfRangeException("style");
			}
			if (--writer.flushLock == 0 && writer.ioIndex >= 1024)
			{
				Flush(writer);
			}
		}

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
			ioBuffer = BufferPool.GetBuffer();
			this.model = model;
			wireType = WireType.None;
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

		void IDisposable.Dispose()
		{
			Dispose();
		}

		public void Dispose()
		{
			if (dest != null)
			{
				Flush(this);
				dest.Dispose();
				dest = null;
			}
			model = null;
			BufferPool.ReleaseBufferToPool(ref ioBuffer);
		}

		internal static int GetPosition(ProtoWriter writer)
		{
			return writer.position;
		}

		private static void DemandSpace(int required, ProtoWriter writer)
		{
			if (writer.ioBuffer.Length - writer.ioIndex >= required)
			{
				return;
			}
			if (writer.flushLock == 0)
			{
				Flush(writer);
				if (writer.ioBuffer.Length - writer.ioIndex >= required)
				{
					return;
				}
			}
			BufferPool.ResizeAndFlushLeft(ref writer.ioBuffer, required + writer.ioIndex, 0, writer.ioIndex);
		}

		public void Close()
		{
			if (depth != 0 || flushLock != 0)
			{
				throw new InvalidOperationException("Unable to close stream in an incomplete state");
			}
			Dispose();
		}

		internal void CheckDepthFlushlock()
		{
			if (depth != 0 || flushLock != 0)
			{
				throw new InvalidOperationException("The writer is in an incomplete state");
			}
		}

		internal static void Flush(ProtoWriter writer)
		{
			if (writer.flushLock == 0 && writer.ioIndex != 0)
			{
				writer.dest.Write(writer.ioBuffer, 0, writer.ioIndex);
				writer.ioIndex = 0;
			}
		}

		private static void WriteUInt32Variant(uint value, ProtoWriter writer)
		{
			DemandSpace(5, writer);
			int num = 0;
			do
			{
				writer.ioBuffer[writer.ioIndex++] = (byte)((value & 0x7F) | 0x80);
				num++;
			}
			while ((value >>= 7) != 0);
			writer.ioBuffer[writer.ioIndex - 1] &= 127;
			writer.position += num;
		}

		internal static uint Zig(int value)
		{
			return (uint)((value << 1) ^ (value >> 31));
		}

		internal static ulong Zig(long value)
		{
			return (ulong)((value << 1) ^ (value >> 63));
		}

		private static void WriteUInt64Variant(ulong value, ProtoWriter writer)
		{
			DemandSpace(10, writer);
			int num = 0;
			do
			{
				writer.ioBuffer[writer.ioIndex++] = (byte)((value & 0x7F) | 0x80);
				num++;
			}
			while ((value >>= 7) != 0L);
			writer.ioBuffer[writer.ioIndex - 1] &= 127;
			writer.position += num;
		}

		public static void WriteString(string value, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (writer.wireType != WireType.String)
			{
				throw CreateException(writer);
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.Length == 0)
			{
				WriteUInt32Variant(0u, writer);
				writer.wireType = WireType.None;
				return;
			}
			int byteCount = encoding.GetByteCount(value);
			WriteUInt32Variant((uint)byteCount, writer);
			DemandSpace(byteCount, writer);
			IncrementedAndReset(encoding.GetBytes(value, 0, value.Length, writer.ioBuffer, writer.ioIndex), writer);
		}

		public static void WriteUInt64(ulong value, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			switch (writer.wireType)
			{
			case WireType.Fixed64:
				WriteInt64((long)value, writer);
				break;
			case WireType.Variant:
				WriteUInt64Variant(value, writer);
				writer.wireType = WireType.None;
				break;
			case WireType.Fixed32:
				WriteUInt32(checked((uint)value), writer);
				break;
			default:
				throw CreateException(writer);
			}
		}

		public static void WriteInt64(long value, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			switch (writer.wireType)
			{
			case WireType.Fixed64:
			{
				DemandSpace(8, writer);
				byte[] array2 = writer.ioBuffer;
				int num = writer.ioIndex;
				array2[num] = (byte)value;
				array2[num + 1] = (byte)(value >> 8);
				array2[num + 2] = (byte)(value >> 16);
				array2[num + 3] = (byte)(value >> 24);
				array2[num + 4] = (byte)(value >> 32);
				array2[num + 5] = (byte)(value >> 40);
				array2[num + 6] = (byte)(value >> 48);
				array2[num + 7] = (byte)(value >> 56);
				IncrementedAndReset(8, writer);
				break;
			}
			case WireType.SignedVariant:
				WriteUInt64Variant(Zig(value), writer);
				writer.wireType = WireType.None;
				break;
			case WireType.Variant:
			{
				if (value >= 0)
				{
					WriteUInt64Variant((ulong)value, writer);
					writer.wireType = WireType.None;
					break;
				}
				DemandSpace(10, writer);
				byte[] array = writer.ioBuffer;
				int num = writer.ioIndex;
				array[num] = (byte)(value | 0x80);
				array[num + 1] = (byte)((int)(value >> 7) | 0x80);
				array[num + 2] = (byte)((int)(value >> 14) | 0x80);
				array[num + 3] = (byte)((int)(value >> 21) | 0x80);
				array[num + 4] = (byte)((int)(value >> 28) | 0x80);
				array[num + 5] = (byte)((int)(value >> 35) | 0x80);
				array[num + 6] = (byte)((int)(value >> 42) | 0x80);
				array[num + 7] = (byte)((int)(value >> 49) | 0x80);
				array[num + 8] = (byte)((int)(value >> 56) | 0x80);
				array[num + 9] = 1;
				IncrementedAndReset(10, writer);
				break;
			}
			case WireType.Fixed32:
				WriteInt32(checked((int)value), writer);
				break;
			default:
				throw CreateException(writer);
			}
		}

		public static void WriteUInt32(uint value, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			switch (writer.wireType)
			{
			case WireType.Fixed32:
				WriteInt32((int)value, writer);
				break;
			case WireType.Fixed64:
				WriteInt64((int)value, writer);
				break;
			case WireType.Variant:
				WriteUInt32Variant(value, writer);
				writer.wireType = WireType.None;
				break;
			default:
				throw CreateException(writer);
			}
		}

		public static void WriteInt16(short value, ProtoWriter writer)
		{
			WriteInt32(value, writer);
		}

		public static void WriteUInt16(ushort value, ProtoWriter writer)
		{
			WriteUInt32(value, writer);
		}

		public static void WriteByte(byte value, ProtoWriter writer)
		{
			WriteUInt32(value, writer);
		}

		public static void WriteSByte(sbyte value, ProtoWriter writer)
		{
			WriteInt32(value, writer);
		}

		private static void WriteInt32ToBuffer(int value, byte[] buffer, int index)
		{
			buffer[index] = (byte)value;
			buffer[index + 1] = (byte)(value >> 8);
			buffer[index + 2] = (byte)(value >> 16);
			buffer[index + 3] = (byte)(value >> 24);
		}

		public static void WriteInt32(int value, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			switch (writer.wireType)
			{
			case WireType.Fixed32:
				DemandSpace(4, writer);
				WriteInt32ToBuffer(value, writer.ioBuffer, writer.ioIndex);
				IncrementedAndReset(4, writer);
				break;
			case WireType.Fixed64:
			{
				DemandSpace(8, writer);
				byte[] array = writer.ioBuffer;
				int num = writer.ioIndex;
				array[num] = (byte)value;
				array[num + 1] = (byte)(value >> 8);
				array[num + 2] = (byte)(value >> 16);
				array[num + 3] = (byte)(value >> 24);
				byte[] array5 = array;
				int num5 = num + 4;
				byte[] array6 = array;
				int num6 = num + 5;
				byte b;
				array[num + 6] = (b = (array[num + 7] = 0));
				array6[num6] = (b = b);
				array5[num5] = b;
				IncrementedAndReset(8, writer);
				break;
			}
			case WireType.SignedVariant:
				WriteUInt32Variant(Zig(value), writer);
				writer.wireType = WireType.None;
				break;
			case WireType.Variant:
			{
				if (value >= 0)
				{
					WriteUInt32Variant((uint)value, writer);
					writer.wireType = WireType.None;
					break;
				}
				DemandSpace(10, writer);
				byte[] array = writer.ioBuffer;
				int num = writer.ioIndex;
				array[num] = (byte)(value | 0x80);
				array[num + 1] = (byte)((value >> 7) | 0x80);
				array[num + 2] = (byte)((value >> 14) | 0x80);
				array[num + 3] = (byte)((value >> 21) | 0x80);
				array[num + 4] = (byte)((value >> 28) | 0x80);
				byte[] array2 = array;
				int num2 = num + 5;
				byte[] array3 = array;
				int num3 = num + 6;
				byte[] array4 = array;
				int num4 = num + 7;
				byte b;
				array[num + 8] = (b = byte.MaxValue);
				array4[num4] = (b = b);
				array3[num3] = (b = b);
				array2[num2] = b;
				array[num + 9] = 1;
				IncrementedAndReset(10, writer);
				break;
			}
			default:
				throw CreateException(writer);
			}
		}

		public unsafe static void WriteDouble(double value, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			switch (writer.wireType)
			{
			case WireType.Fixed32:
			{
				float value2 = (float)value;
				if (Helpers.IsInfinity(value2) && !Helpers.IsInfinity(value))
				{
					throw new OverflowException();
				}
				WriteSingle(value2, writer);
				break;
			}
			case WireType.Fixed64:
				WriteInt64(*(long*)(&value), writer);
				break;
			default:
				throw CreateException(writer);
			}
		}

		public unsafe static void WriteSingle(float value, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			switch (writer.wireType)
			{
			case WireType.Fixed32:
				WriteInt32(*(int*)(&value), writer);
				break;
			case WireType.Fixed64:
				WriteDouble(value, writer);
				break;
			default:
				throw CreateException(writer);
			}
		}

		public static void ThrowEnumException(ProtoWriter writer, object enumValue)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			string str = (enumValue == null) ? "<null>" : (enumValue.GetType().FullName + "." + enumValue.ToString());
			throw new ProtoException("No wire-value is mapped to the enum " + str + " at position " + writer.position.ToString());
		}

		internal static Exception CreateException(ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			return new ProtoException("Invalid serialization operation with wire-type " + writer.wireType.ToString() + " at position " + writer.position.ToString());
		}

		public static void WriteBoolean(bool value, ProtoWriter writer)
		{
			WriteUInt32(value ? 1u : 0u, writer);
		}

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
				throw CreateException(writer);
			}
			IExtension extensionObject = instance.GetExtensionObject(createIfMissing: false);
			if (extensionObject != null)
			{
				Stream stream = extensionObject.BeginQuery();
				try
				{
					CopyRawFromStream(stream, writer);
				}
				finally
				{
					extensionObject.EndQuery(stream);
				}
			}
		}

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

		internal string SerializeType(Type type)
		{
			return TypeModel.SerializeType(model, type);
		}

		public void SetRootObject(object value)
		{
			NetCache.SetKeyedObject(0, value);
		}

		public static void WriteType(Type value, ProtoWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			WriteString(writer.SerializeType(value), writer);
		}
	}
}
