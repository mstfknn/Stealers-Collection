using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000051 RID: 81
	internal sealed class SingleSerializer : IProtoSerializer
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600024A RID: 586 RVA: 0x000036DC File Offset: 0x000018DC
		public Type ExpectedType
		{
			get
			{
				return SingleSerializer.expectedType;
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000022E5 File Offset: 0x000004E5
		public SingleSerializer(TypeModel model)
		{
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600024C RID: 588 RVA: 0x000031DF File Offset: 0x000013DF
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00003147 File Offset: 0x00001347
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x000036E3 File Offset: 0x000018E3
		public object Read(object value, ProtoReader source)
		{
			return source.ReadSingle();
		}

		// Token: 0x0600024F RID: 591 RVA: 0x000036F0 File Offset: 0x000018F0
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteSingle((float)value, dest);
		}

		// Token: 0x04000114 RID: 276
		private static readonly Type expectedType = typeof(float);
	}
}
