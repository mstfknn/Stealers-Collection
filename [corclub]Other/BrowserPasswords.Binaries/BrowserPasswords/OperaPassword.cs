using System;

namespace BrowserPasswords
{
	// Token: 0x0200002C RID: 44
	public class OperaPassword
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600015F RID: 351 RVA: 0x000054D8 File Offset: 0x000036D8
		public string URL
		{
			get
			{
				return this.m_URL;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000160 RID: 352 RVA: 0x000054F0 File Offset: 0x000036F0
		public string ActionURL
		{
			get
			{
				return this.m_ActionURL;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00005508 File Offset: 0x00003708
		public string UserElementName
		{
			get
			{
				return this.m_UserElementName;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00005520 File Offset: 0x00003720
		public string UsernameValue
		{
			get
			{
				return this.m_UserValue;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00005538 File Offset: 0x00003738
		public string PasswordElementName
		{
			get
			{
				return this.m_PasswordElementName;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00005550 File Offset: 0x00003750
		public string PasswordValue
		{
			get
			{
				return this.m_PasswordValue;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00005568 File Offset: 0x00003768
		public string CreationDate
		{
			get
			{
				return this.m_CreationDate;
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00005580 File Offset: 0x00003780
		internal OperaPassword(string url, string action, string userElementName, string userValue, string passElementName, string passValue, string creation_date)
		{
			this.m_URL = url;
			this.m_ActionURL = action;
			this.m_PasswordElementName = passElementName;
			this.m_PasswordValue = passValue;
			this.m_UserElementName = userElementName;
			this.m_UserValue = userValue;
			this.m_CreationDate = creation_date;
		}

		// Token: 0x04000124 RID: 292
		private string m_URL;

		// Token: 0x04000125 RID: 293
		private string m_ActionURL;

		// Token: 0x04000126 RID: 294
		private string m_UserElementName;

		// Token: 0x04000127 RID: 295
		private string m_UserValue;

		// Token: 0x04000128 RID: 296
		private string m_PasswordElementName;

		// Token: 0x04000129 RID: 297
		private string m_PasswordValue;

		// Token: 0x0400012A RID: 298
		private string m_CreationDate;
	}
}
