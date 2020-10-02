using System;

namespace Reborn
{
	// Token: 0x02000019 RID: 25
	public class PassData
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00002C87 File Offset: 0x00000E87
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00002C8F File Offset: 0x00000E8F
		public string Url
		{
			get;
			set;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00002C98 File Offset: 0x00000E98
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00002CA0 File Offset: 0x00000EA0
		public string Login
		{
			get;
			set;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00002CA9 File Offset: 0x00000EA9
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00002CB1 File Offset: 0x00000EB1
		public string Password
		{
			get;
			set;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00002CBA File Offset: 0x00000EBA
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00002CC2 File Offset: 0x00000EC2
		public string Program
		{
			get;
			set;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00007BFC File Offset: 0x00005DFC
		public override string ToString()
		{
			return string.Format("{0}\n{1}\n{2}\n{3}\n", new object[]
			{
				this.Login,
				this.Password,
				this.Url,
				this.Program
			});
		}
	}
}
