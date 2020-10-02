using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MysteryPanel.App
{
	// Token: 0x02000030 RID: 48
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0")]
	[CompilerGenerated]
	internal sealed partial class AccountSettings : ApplicationSettingsBase
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000344D File Offset: 0x0000164D
		public static AccountSettings Default
		{
			get
			{
				return AccountSettings.defaultInstance;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00003454 File Offset: 0x00001654
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00003466 File Offset: 0x00001666
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		public string CachedLogin
		{
			get
			{
				return (string)this["CachedLogin"];
			}
			set
			{
				this["CachedLogin"] = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00003474 File Offset: 0x00001674
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00003486 File Offset: 0x00001686
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		public string CachedPassword
		{
			get
			{
				return (string)this["CachedPassword"];
			}
			set
			{
				this["CachedPassword"] = value;
			}
		}

		// Token: 0x040000DF RID: 223
		private static AccountSettings defaultInstance = (AccountSettings)SettingsBase.Synchronized(new AccountSettings());
	}
}
