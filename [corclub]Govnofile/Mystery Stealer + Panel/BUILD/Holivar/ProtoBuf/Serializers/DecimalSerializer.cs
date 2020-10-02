using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x0200003B RID: 59
	internal sealed class DecimalSerializer : IProtoSerializer
	{
		// Token: 0x060001BA RID: 442 RVA: 0x000022E5 File Offset: 0x000004E5
		public DecimalSerializer(TypeModel model)
		{
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00003295 File Offset: 0x00001495
		public Type ExpectedType
		{
			get
			{
				return DecimalSerializer.expectedType;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001BC RID: 444 RVA: 0x000031DF File Offset: 0x000013DF
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00003147 File Offset: 0x00001347
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000329C File Offset: 0x0000149C
		public object Read(object value, ProtoReader source)
		{
			return BclHelpers.ReadDecimal(source);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000032A9 File Offset: 0x000014A9
		public void Write(object value, ProtoWriter dest)
		{
			BclHelpers.WriteDecimal((decimal)value, dest);
		}

		// Token: 0x040000E6 RID: 230
		private static readonly Type expectedType = typeof(decimal);
	}
}
