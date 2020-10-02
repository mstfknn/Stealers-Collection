using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x0200005B RID: 91
	internal sealed class UInt32Serializer : IProtoSerializer
	{
		// Token: 0x060002A9 RID: 681 RVA: 0x000022E5 File Offset: 0x000004E5
		public UInt32Serializer(TypeModel model)
		{
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002AA RID: 682 RVA: 0x000039E2 File Offset: 0x00001BE2
		public Type ExpectedType
		{
			get
			{
				return UInt32Serializer.expectedType;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002AB RID: 683 RVA: 0x000031DF File Offset: 0x000013DF
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002AC RID: 684 RVA: 0x00003147 File Offset: 0x00001347
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x000039E9 File Offset: 0x00001BE9
		public object Read(object value, ProtoReader source)
		{
			return source.ReadUInt32();
		}

		// Token: 0x060002AE RID: 686 RVA: 0x000039F6 File Offset: 0x00001BF6
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteUInt32((uint)value, dest);
		}

		// Token: 0x04000134 RID: 308
		private static readonly Type expectedType = typeof(uint);
	}
}
