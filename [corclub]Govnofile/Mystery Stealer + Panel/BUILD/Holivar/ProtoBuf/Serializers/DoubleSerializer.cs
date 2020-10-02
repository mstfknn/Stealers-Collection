using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x0200003D RID: 61
	internal sealed class DoubleSerializer : IProtoSerializer
	{
		// Token: 0x060001C7 RID: 455 RVA: 0x000022E5 File Offset: 0x000004E5
		public DoubleSerializer(TypeModel model)
		{
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000331B File Offset: 0x0000151B
		public Type ExpectedType
		{
			get
			{
				return DoubleSerializer.expectedType;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x000031DF File Offset: 0x000013DF
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00003147 File Offset: 0x00001347
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00003322 File Offset: 0x00001522
		public object Read(object value, ProtoReader source)
		{
			return source.ReadDouble();
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000332F File Offset: 0x0000152F
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteDouble((double)value, dest);
		}

		// Token: 0x040000E8 RID: 232
		private static readonly Type expectedType = typeof(double);
	}
}
