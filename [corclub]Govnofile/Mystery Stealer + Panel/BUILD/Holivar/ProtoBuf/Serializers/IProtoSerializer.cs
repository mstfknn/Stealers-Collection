using System;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000046 RID: 70
	internal interface IProtoSerializer
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001FF RID: 511
		Type ExpectedType { get; }

		// Token: 0x06000200 RID: 512
		void Write(object value, ProtoWriter dest);

		// Token: 0x06000201 RID: 513
		object Read(object value, ProtoReader source);

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000202 RID: 514
		bool RequiresOldValue { get; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000203 RID: 515
		bool ReturnsValue { get; }
	}
}
