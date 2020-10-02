using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000038 RID: 56
	internal sealed class ByteSerializer : IProtoSerializer
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x000031F3 File Offset: 0x000013F3
		public Type ExpectedType
		{
			get
			{
				return ByteSerializer.expectedType;
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x000022E5 File Offset: 0x000004E5
		public ByteSerializer(TypeModel model)
		{
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x000031DF File Offset: 0x000013DF
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00003147 File Offset: 0x00001347
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000031FA File Offset: 0x000013FA
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteByte((byte)value, dest);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00003208 File Offset: 0x00001408
		public object Read(object value, ProtoReader source)
		{
			return source.ReadByte();
		}

		// Token: 0x040000E3 RID: 227
		private static readonly Type expectedType = typeof(byte);
	}
}
