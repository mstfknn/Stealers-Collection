using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000043 RID: 67
	internal sealed class Int16Serializer : IProtoSerializer
	{
		// Token: 0x060001EA RID: 490 RVA: 0x000022E5 File Offset: 0x000004E5
		public Int16Serializer(TypeModel model)
		{
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001EB RID: 491 RVA: 0x000033EA File Offset: 0x000015EA
		public Type ExpectedType
		{
			get
			{
				return Int16Serializer.expectedType;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001EC RID: 492 RVA: 0x000031DF File Offset: 0x000013DF
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00003147 File Offset: 0x00001347
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000033F1 File Offset: 0x000015F1
		public object Read(object value, ProtoReader source)
		{
			return source.ReadInt16();
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000033FE File Offset: 0x000015FE
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteInt16((short)value, dest);
		}

		// Token: 0x040000F5 RID: 245
		private static readonly Type expectedType = typeof(short);
	}
}
