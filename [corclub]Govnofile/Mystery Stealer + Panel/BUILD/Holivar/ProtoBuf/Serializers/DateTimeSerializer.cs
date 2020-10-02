using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x0200003A RID: 58
	internal sealed class DateTimeSerializer : IProtoSerializer
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00003262 File Offset: 0x00001462
		public Type ExpectedType
		{
			get
			{
				return DateTimeSerializer.expectedType;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x000031DF File Offset: 0x000013DF
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00003147 File Offset: 0x00001347
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000022E5 File Offset: 0x000004E5
		public DateTimeSerializer(TypeModel model)
		{
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00003269 File Offset: 0x00001469
		public object Read(object value, ProtoReader source)
		{
			return BclHelpers.ReadDateTime(source);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00003276 File Offset: 0x00001476
		public void Write(object value, ProtoWriter dest)
		{
			BclHelpers.WriteDateTime((DateTime)value, dest);
		}

		// Token: 0x040000E5 RID: 229
		private static readonly Type expectedType = typeof(DateTime);
	}
}
