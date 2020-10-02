using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000052 RID: 82
	internal sealed class StringSerializer : IProtoSerializer
	{
		// Token: 0x06000251 RID: 593 RVA: 0x000022E5 File Offset: 0x000004E5
		public StringSerializer(TypeModel model)
		{
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000370F File Offset: 0x0000190F
		public Type ExpectedType
		{
			get
			{
				return StringSerializer.expectedType;
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00003716 File Offset: 0x00001916
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteString((string)value, dest);
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000254 RID: 596 RVA: 0x000031DF File Offset: 0x000013DF
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00003147 File Offset: 0x00001347
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00003724 File Offset: 0x00001924
		public object Read(object value, ProtoReader source)
		{
			return source.ReadString();
		}

		// Token: 0x04000115 RID: 277
		private static readonly Type expectedType = typeof(string);
	}
}
