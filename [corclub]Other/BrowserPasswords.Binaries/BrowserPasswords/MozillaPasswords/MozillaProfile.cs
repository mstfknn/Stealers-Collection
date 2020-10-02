using System;

namespace BrowserPasswords.MozillaPasswords
{
	// Token: 0x02000025 RID: 37
	public class MozillaProfile
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00004B34 File Offset: 0x00002D34
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00004B4C File Offset: 0x00002D4C
		public string Path
		{
			get
			{
				return this.m_profile_path;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00004B64 File Offset: 0x00002D64
		public bool IsRelative
		{
			get
			{
				return this.m_relative;
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00004B7C File Offset: 0x00002D7C
		public MozillaProfile(MozillaSoft soft, string profiles_ini_dir, string name, string path, bool relative)
		{
			this.m_name = name;
			this.m_relative = relative;
			this.m_profile_path = path;
			this.m_profiles_ini_dir = profiles_ini_dir;
			this.m_soft = soft;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004BAC File Offset: 0x00002DAC
		public MozillaSignons GetSignons(MozillaProfile.MozillaVersion version)
		{
			string profile_path;
			if (this.m_relative)
			{
				profile_path = this.m_profiles_ini_dir + "\\" + this.m_profile_path;
			}
			else
			{
				profile_path = this.m_profile_path;
			}
			return new MozillaSignons(this.m_soft, profile_path, version);
		}

		// Token: 0x0400010F RID: 271
		private bool m_relative;

		// Token: 0x04000110 RID: 272
		private string m_profile_path;

		// Token: 0x04000111 RID: 273
		private string m_name;

		// Token: 0x04000112 RID: 274
		private string m_profiles_ini_dir;

		// Token: 0x04000113 RID: 275
		private MozillaSoft m_soft;

		// Token: 0x02000026 RID: 38
		public enum MozillaVersion
		{
			// Token: 0x04000115 RID: 277
			Txt2c,
			// Token: 0x04000116 RID: 278
			Txt2d,
			// Token: 0x04000117 RID: 279
			Txt2e,
			// Token: 0x04000118 RID: 280
			Sqlite
		}
	}
}
