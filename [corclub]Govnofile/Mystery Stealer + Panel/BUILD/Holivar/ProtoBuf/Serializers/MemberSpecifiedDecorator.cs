using System;
using System.Reflection;

namespace ProtoBuf.Serializers
{
	// Token: 0x0200004A RID: 74
	internal sealed class MemberSpecifiedDecorator : ProtoDecoratorBase
	{
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600021A RID: 538 RVA: 0x000032C8 File Offset: 0x000014C8
		public override Type ExpectedType
		{
			get
			{
				return this.Tail.ExpectedType;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600021B RID: 539 RVA: 0x000032D5 File Offset: 0x000014D5
		public override bool RequiresOldValue
		{
			get
			{
				return this.Tail.RequiresOldValue;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600021C RID: 540 RVA: 0x000032E2 File Offset: 0x000014E2
		public override bool ReturnsValue
		{
			get
			{
				return this.Tail.ReturnsValue;
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000351C File Offset: 0x0000171C
		public MemberSpecifiedDecorator(MethodInfo getSpecified, MethodInfo setSpecified, IProtoSerializer tail) : base(tail)
		{
			if (getSpecified == null && setSpecified == null)
			{
				throw new InvalidOperationException();
			}
			this.getSpecified = getSpecified;
			this.setSpecified = setSpecified;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000353F File Offset: 0x0000173F
		public override void Write(object value, ProtoWriter dest)
		{
			if (this.getSpecified == null || (bool)this.getSpecified.Invoke(value, null))
			{
				this.Tail.Write(value, dest);
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000EEBC File Offset: 0x0000D0BC
		public override object Read(object value, ProtoReader source)
		{
			object result = this.Tail.Read(value, source);
			if (this.setSpecified != null)
			{
				this.setSpecified.Invoke(value, new object[]
				{
					true
				});
			}
			return result;
		}

		// Token: 0x04000106 RID: 262
		private readonly MethodInfo getSpecified;

		// Token: 0x04000107 RID: 263
		private readonly MethodInfo setSpecified;
	}
}
