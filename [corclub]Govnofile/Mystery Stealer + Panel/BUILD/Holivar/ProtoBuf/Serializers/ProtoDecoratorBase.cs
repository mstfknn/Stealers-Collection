using System;

namespace ProtoBuf.Serializers
{
	// Token: 0x0200004F RID: 79
	internal abstract class ProtoDecoratorBase : IProtoSerializer
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600023D RID: 573
		public abstract Type ExpectedType { get; }

		// Token: 0x0600023E RID: 574 RVA: 0x0000369A File Offset: 0x0000189A
		protected ProtoDecoratorBase(IProtoSerializer tail)
		{
			this.Tail = tail;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600023F RID: 575
		public abstract bool ReturnsValue { get; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000240 RID: 576
		public abstract bool RequiresOldValue { get; }

		// Token: 0x06000241 RID: 577
		public abstract void Write(object value, ProtoWriter dest);

		// Token: 0x06000242 RID: 578
		public abstract object Read(object value, ProtoReader source);

		// Token: 0x04000112 RID: 274
		protected readonly IProtoSerializer Tail;
	}
}
