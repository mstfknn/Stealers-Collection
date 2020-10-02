using System;

namespace BrowserPasswords.MozillaPasswords
{
	// Token: 0x02000024 RID: 36
	public class MozillaPassword
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00004914 File Offset: 0x00002B14
		public string FormSubmitURL
		{
			get
			{
				return this.m_formSubmitURL;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600012F RID: 303 RVA: 0x0000492C File Offset: 0x00002B2C
		// (set) Token: 0x06000130 RID: 304 RVA: 0x00004944 File Offset: 0x00002B44
		public string SDRPassword
		{
			get
			{
				return this.m_sdr_password;
			}
			set
			{
				this.m_sdr_password = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00004950 File Offset: 0x00002B50
		public string SiteName
		{
			get
			{
				return this.m_sitename;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00004968 File Offset: 0x00002B68
		public string UsernameField
		{
			get
			{
				return this.m_username_field;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00004980 File Offset: 0x00002B80
		public string PasswordField
		{
			get
			{
				return this.m_password_field;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00004998 File Offset: 0x00002B98
		public string Username
		{
			get
			{
				string result;
				if (this.m_soft == MozillaSoft.Thunderbird)
				{
					result = new Uri(this.SiteName).UserInfo;
				}
				else
				{
					MozillaSDR mozillaSDR = new MozillaSDR(this.m_profile_dir, this.m_sdr_password);
					string text = mozillaSDR.DecryptPassword(this.m_username);
					mozillaSDR.Dispose();
					result = text;
				}
				return result;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000135 RID: 309 RVA: 0x000049F0 File Offset: 0x00002BF0
		public string Password
		{
			get
			{
				MozillaSDR mozillaSDR = new MozillaSDR(this.m_profile_dir, this.m_sdr_password);
				string result;
				if (this.m_soft == MozillaSoft.Thunderbird)
				{
					string text = mozillaSDR.DecryptPassword(this.m_password);
					if (string.IsNullOrEmpty(text))
					{
						text = mozillaSDR.DecodePassword(this.m_password);
					}
					mozillaSDR.Dispose();
					result = text;
				}
				else
				{
					string text2 = mozillaSDR.DecryptPassword(this.m_password);
					mozillaSDR.Dispose();
					result = text2;
				}
				return result;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00004A60 File Offset: 0x00002C60
		public string Domain
		{
			get
			{
				return this.m_domain;
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004A78 File Offset: 0x00002C78
		public MozillaPassword(MozillaSoft soft, string profile_dir, string sitename, string username_field, string password_field, string username, string password, string domain, string formSubmitURL)
		{
			this.m_sitename = sitename;
			if (this.m_sitename.Contains(" ("))
			{
				this.m_sitename = this.m_sitename.Substring(0, this.m_sitename.IndexOf(" ("));
			}
			this.m_username_field = username_field;
			this.m_password_field = password_field;
			this.m_username = username;
			this.m_password = password;
			this.m_domain = domain;
			this.m_profile_dir = profile_dir;
			this.m_soft = soft;
			this.m_formSubmitURL = formSubmitURL;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004B04 File Offset: 0x00002D04
		public string GetPasswordText(string password)
		{
			return this.Password;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004B1C File Offset: 0x00002D1C
		public string GetUsernameText(string password)
		{
			return this.Username;
		}

		// Token: 0x04000105 RID: 261
		private string m_sitename;

		// Token: 0x04000106 RID: 262
		private string m_username_field;

		// Token: 0x04000107 RID: 263
		private string m_password_field;

		// Token: 0x04000108 RID: 264
		private string m_username;

		// Token: 0x04000109 RID: 265
		private string m_password;

		// Token: 0x0400010A RID: 266
		private string m_domain;

		// Token: 0x0400010B RID: 267
		private string m_profile_dir;

		// Token: 0x0400010C RID: 268
		private MozillaSoft m_soft;

		// Token: 0x0400010D RID: 269
		private string m_sdr_password;

		// Token: 0x0400010E RID: 270
		private string m_formSubmitURL;
	}
}
