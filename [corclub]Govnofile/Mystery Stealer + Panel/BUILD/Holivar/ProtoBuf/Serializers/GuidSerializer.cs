using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000041 RID: 65
	internal sealed class GuidSerializer : IProtoSerializer
	{
		// Token: 0x060001DE RID: 478 RVA: 0x000022E5 File Offset: 0x000004E5
		public GuidSerializer(TypeModel model)
		{
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001DF RID: 479 RVA: 0x000033B7 File Offset: 0x000015B7
		public Type ExpectedType
		{
			get
			{
				return GuidSerializer.expectedType;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x000031DF File Offset: 0x000013DF
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00003147 File Offset: 0x00001347
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000033BE File Offset: 0x000015BE
		public void Write(object value, ProtoWriter dest)
		{
			BclHelpers.WriteGuid((Guid)value, dest);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000033CC File Offset: 0x000015CC
		public object Read(object value, ProtoReader source)
		{
			return BclHelpers.ReadGuid(source);
		}

		// Token: 0x040000F0 RID: 240
		private static readonly Type expectedType = typeof(Guid);
	}
}
