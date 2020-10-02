using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000057 RID: 87
	internal sealed class TimeSpanSerializer : IProtoSerializer
	{
		// Token: 0x06000280 RID: 640 RVA: 0x000022E5 File Offset: 0x000004E5
		public TimeSpanSerializer(TypeModel model)
		{
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000281 RID: 641 RVA: 0x00003908 File Offset: 0x00001B08
		public Type ExpectedType
		{
			get
			{
				return TimeSpanSerializer.expectedType;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000282 RID: 642 RVA: 0x000031DF File Offset: 0x000013DF
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000283 RID: 643 RVA: 0x00003147 File Offset: 0x00001347
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000390F File Offset: 0x00001B0F
		public object Read(object value, ProtoReader source)
		{
			return BclHelpers.ReadTimeSpan(source);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000391C File Offset: 0x00001B1C
		public void Write(object value, ProtoWriter dest)
		{
			BclHelpers.WriteTimeSpan((TimeSpan)value, dest);
		}

		// Token: 0x04000123 RID: 291
		private static readonly Type expectedType = typeof(TimeSpan);
	}
}
