using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x0200005C RID: 92
	internal sealed class UInt64Serializer : IProtoSerializer
	{
		// Token: 0x060002B0 RID: 688 RVA: 0x000022E5 File Offset: 0x000004E5
		public UInt64Serializer(TypeModel model)
		{
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00003A15 File Offset: 0x00001C15
		public Type ExpectedType
		{
			get
			{
				return UInt64Serializer.expectedType;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x000031DF File Offset: 0x000013DF
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x00003147 File Offset: 0x00001347
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00003A1C File Offset: 0x00001C1C
		public object Read(object value, ProtoReader source)
		{
			return source.ReadUInt64();
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00003A29 File Offset: 0x00001C29
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteUInt64((ulong)value, dest);
		}

		// Token: 0x04000135 RID: 309
		private static readonly Type expectedType = typeof(ulong);
	}
}
