using System;

namespace BrowserPasswords
{
	// Token: 0x02000010 RID: 16
	internal class CIE7Password
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x00002900 File Offset: 0x00000B00
		internal CIE7Password(string szResourceName, string szURL, string szUserName, string szPasswd, DateTime dtAddDate, int dwType, string szDescription)
		{
			this.m_dtAddDate = dtAddDate;
			this.m_dwType = dwType;
			this.m_szPassword = szPasswd;
			this.m_szResourceName = szResourceName;
			this.m_szUserName = szUserName;
			this.m_szURL = szURL;
			this.m_szDescription = szDescription;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00002940 File Offset: 0x00000B40
		public string ResourceName
		{
			get
			{
				return this.m_szResourceName;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00002958 File Offset: 0x00000B58
		public string URL
		{
			get
			{
				return this.m_szURL;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00002970 File Offset: 0x00000B70
		public string Description
		{
			get
			{
				return this.m_szDescription;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00002988 File Offset: 0x00000B88
		public string UserName
		{
			get
			{
				return this.m_szUserName;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000EB RID: 235 RVA: 0x000029A0 File Offset: 0x00000BA0
		public string Password
		{
			get
			{
				return this.m_szPassword;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000EC RID: 236 RVA: 0x000029B8 File Offset: 0x00000BB8
		public DateTime AddDate
		{
			get
			{
				return this.m_dtAddDate;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000ED RID: 237 RVA: 0x000029D0 File Offset: 0x00000BD0
		public int PasswdType
		{
			get
			{
				return this.m_dwType;
			}
		}

		// Token: 0x0400007A RID: 122
		private string m_szResourceName;

		// Token: 0x0400007B RID: 123
		private string m_szUserName;

		// Token: 0x0400007C RID: 124
		private string m_szPassword;

		// Token: 0x0400007D RID: 125
		private DateTime m_dtAddDate;

		// Token: 0x0400007E RID: 126
		private int m_dwType;

		// Token: 0x0400007F RID: 127
		private string m_szDescription;

		// Token: 0x04000080 RID: 128
		private string m_szURL;
	}
}
