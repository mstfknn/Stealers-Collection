using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000036 RID: 54
	internal sealed class BlobSerializer : IProtoSerializer
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00003164 File Offset: 0x00001364
		public Type ExpectedType
		{
			get
			{
				return BlobSerializer.expectedType;
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000316B File Offset: 0x0000136B
		public BlobSerializer(TypeModel model, bool overwriteList)
		{
			this.overwriteList = overwriteList;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000317A File Offset: 0x0000137A
		public object Read(object value, ProtoReader source)
		{
			return ProtoReader.AppendBytes(this.overwriteList ? null : ((byte[])value), source);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00003193 File Offset: 0x00001393
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteBytes((byte[])value, dest);
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600019D RID: 413 RVA: 0x000031A1 File Offset: 0x000013A1
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return !this.overwriteList;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00003147 File Offset: 0x00001347
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x040000E0 RID: 224
		private static readonly Type expectedType = typeof(byte[]);

		// Token: 0x040000E1 RID: 225
		private readonly bool overwriteList;
	}
}
