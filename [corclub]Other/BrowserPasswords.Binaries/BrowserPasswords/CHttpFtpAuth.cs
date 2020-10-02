using System;

namespace BrowserPasswords
{
	// Token: 0x0200000C RID: 12
	internal class CHttpFtpAuth
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x000024E4 File Offset: 0x000006E4
		internal CHttpFtpAuth(CProtectStore PStore, string szResourceName, string szUserName, string szPasswd, string szDescription, string szURL, int dwType)
		{
			this.guidWinInet = new Guid("{5E7E8100-9138-11D1-945A-00C04FC308FF}");
			this.subguidWinInet = new Guid("{00000000-0000-0000-0000-000000000000}");
			if (this.m_IPStore == null)
			{
				this.m_IPStore = PStore;
			}
			this.m_szDescription = szDescription;
			this.m_dwType = dwType;
			this.m_szPassword = szPasswd;
			this.m_szResourceName = szResourceName;
			this.m_szUserName = szUserName;
			this.m_szURL = szURL;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00002554 File Offset: 0x00000754
		public void DeletePassword()
		{
			this.m_IPStore.DeleteItem(this.guidWinInet, this.subguidWinInet, this.m_szResourceName);
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00002574 File Offset: 0x00000774
		public string ResourceName
		{
			get
			{
				return this.m_szResourceName;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x0000258C File Offset: 0x0000078C
		public string UserName
		{
			get
			{
				return this.m_szUserName;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x000025A4 File Offset: 0x000007A4
		public string Password
		{
			get
			{
				return this.m_szPassword;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x000025BC File Offset: 0x000007BC
		public string Description
		{
			get
			{
				return this.m_szDescription;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x000025D4 File Offset: 0x000007D4
		public string URL
		{
			get
			{
				return this.m_szURL;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000025EC File Offset: 0x000007EC
		public int PasswdType
		{
			get
			{
				return this.m_dwType;
			}
		}

		// Token: 0x04000064 RID: 100
		private readonly Guid guidWinInet;

		// Token: 0x04000065 RID: 101
		private readonly Guid subguidWinInet;

		// Token: 0x04000066 RID: 102
		private CProtectStore m_IPStore;

		// Token: 0x04000067 RID: 103
		private string m_szResourceName;

		// Token: 0x04000068 RID: 104
		private string m_szURL;

		// Token: 0x04000069 RID: 105
		private string m_szUserName;

		// Token: 0x0400006A RID: 106
		private string m_szPassword;

		// Token: 0x0400006B RID: 107
		private string m_szDescription;

		// Token: 0x0400006C RID: 108
		private int m_dwType;
	}
}
