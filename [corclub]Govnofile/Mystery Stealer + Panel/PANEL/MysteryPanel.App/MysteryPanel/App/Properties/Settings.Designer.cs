using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace MysteryPanel.App.Properties
{
	// Token: 0x02000034 RID: 52
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
	[CompilerGenerated]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x000035A2 File Offset: 0x000017A2
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x040000EC RID: 236
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
