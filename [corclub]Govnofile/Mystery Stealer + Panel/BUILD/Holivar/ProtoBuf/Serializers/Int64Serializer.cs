using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000045 RID: 69
	internal sealed class Int64Serializer : IProtoSerializer
	{
		// Token: 0x060001F8 RID: 504 RVA: 0x000022E5 File Offset: 0x000004E5
		public Int64Serializer(TypeModel model)
		{
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00003450 File Offset: 0x00001650
		public Type ExpectedType
		{
			get
			{
				return Int64Serializer.expectedType;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001FA RID: 506 RVA: 0x000031DF File Offset: 0x000013DF
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00003147 File Offset: 0x00001347
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00003457 File Offset: 0x00001657
		public object Read(object value, ProtoReader source)
		{
			return source.ReadInt64();
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00003464 File Offset: 0x00001664
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteInt64((long)value, dest);
		}

		// Token: 0x040000F7 RID: 247
		private static readonly Type expectedType = typeof(long);
	}
}
