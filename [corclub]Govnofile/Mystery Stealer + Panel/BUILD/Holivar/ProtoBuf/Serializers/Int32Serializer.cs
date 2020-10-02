using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000044 RID: 68
	internal sealed class Int32Serializer : IProtoSerializer
	{
		// Token: 0x060001F1 RID: 497 RVA: 0x000022E5 File Offset: 0x000004E5
		public Int32Serializer(TypeModel model)
		{
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000341D File Offset: 0x0000161D
		public Type ExpectedType
		{
			get
			{
				return Int32Serializer.expectedType;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x000031DF File Offset: 0x000013DF
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00003147 File Offset: 0x00001347
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00003424 File Offset: 0x00001624
		public object Read(object value, ProtoReader source)
		{
			return source.ReadInt32();
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00003431 File Offset: 0x00001631
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteInt32((int)value, dest);
		}

		// Token: 0x040000F6 RID: 246
		private static readonly Type expectedType = typeof(int);
	}
}
