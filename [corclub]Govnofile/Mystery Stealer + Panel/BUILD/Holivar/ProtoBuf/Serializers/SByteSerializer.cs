using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000050 RID: 80
	internal sealed class SByteSerializer : IProtoSerializer
	{
		// Token: 0x06000243 RID: 579 RVA: 0x000022E5 File Offset: 0x000004E5
		public SByteSerializer(TypeModel model)
		{
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000244 RID: 580 RVA: 0x000036A9 File Offset: 0x000018A9
		public Type ExpectedType
		{
			get
			{
				return SByteSerializer.expectedType;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000245 RID: 581 RVA: 0x000031DF File Offset: 0x000013DF
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00003147 File Offset: 0x00001347
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000036B0 File Offset: 0x000018B0
		public object Read(object value, ProtoReader source)
		{
			return source.ReadSByte();
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000036BD File Offset: 0x000018BD
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteSByte((sbyte)value, dest);
		}

		// Token: 0x04000113 RID: 275
		private static readonly Type expectedType = typeof(sbyte);
	}
}
