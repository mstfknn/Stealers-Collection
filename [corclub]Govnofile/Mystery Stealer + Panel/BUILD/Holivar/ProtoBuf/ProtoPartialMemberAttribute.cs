using System;

namespace ProtoBuf
{
	// Token: 0x0200002B RID: 43
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public sealed class ProtoPartialMemberAttribute : ProtoMemberAttribute
	{
		// Token: 0x060000EB RID: 235 RVA: 0x00002A30 File Offset: 0x00000C30
		public ProtoPartialMemberAttribute(int tag, string memberName) : base(tag)
		{
			if (Helpers.IsNullOrEmpty(memberName))
			{
				throw new ArgumentNullException("memberName");
			}
			this.memberName = memberName;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00002A53 File Offset: 0x00000C53
		public string MemberName
		{
			get
			{
				return this.memberName;
			}
		}

		// Token: 0x040000A2 RID: 162
		private readonly string memberName;
	}
}
