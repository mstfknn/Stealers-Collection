using System;

namespace ProtoBuf
{
	// Token: 0x02000027 RID: 39
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public sealed class ProtoPartialIgnoreAttribute : ProtoIgnoreAttribute
	{
		// Token: 0x060000CA RID: 202 RVA: 0x0000280A File Offset: 0x00000A0A
		public ProtoPartialIgnoreAttribute(string memberName)
		{
			if (Helpers.IsNullOrEmpty(memberName))
			{
				throw new ArgumentNullException("memberName");
			}
			this.memberName = memberName;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000CB RID: 203 RVA: 0x0000282C File Offset: 0x00000A2C
		public string MemberName
		{
			get
			{
				return this.memberName;
			}
		}

		// Token: 0x04000090 RID: 144
		private readonly string memberName;
	}
}
