using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.CompilerServices;

// Token: 0x0200001F RID: 31
[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
[EditorBrowsable(EditorBrowsableState.Advanced)]
[CompilerGenerated]
internal sealed partial class Class10 : ApplicationSettingsBase
{
	// Token: 0x0600007A RID: 122 RVA: 0x0000219F File Offset: 0x0000039F
	static Class10()
	{
		// Note: this type is marked as 'beforefieldinit'.
	}

	// Token: 0x0600007B RID: 123 RVA: 0x000021C4 File Offset: 0x000003C4
	public Class10()
	{
	}

	// Token: 0x0600007C RID: 124 RVA: 0x000021CC File Offset: 0x000003CC
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DebuggerNonUserCode]
	private static void smethod_0(object sender, EventArgs e)
	{
		if (Class1.Form0_0.SaveMySettingsOnExit)
		{
			Class11.Class10_0.Save();
		}
	}

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x0600007D RID: 125 RVA: 0x000051AC File Offset: 0x000033AC
	public static Class10 Class10_0
	{
		get
		{
			if (!Class10.bool_0)
			{
				object obj = Class10.object_0;
				ObjectFlowControl.CheckForSyncLockOnValueType(obj);
				lock (obj)
				{
					if (!Class10.bool_0)
					{
						Class1.Form0_0.Shutdown += Class10.smethod_0;
						Class10.bool_0 = true;
					}
				}
			}
			return Class10.class10_0;
		}
	}

	// Token: 0x04000038 RID: 56
	private static Class10 class10_0 = (Class10)SettingsBase.Synchronized(new Class10());

	// Token: 0x04000039 RID: 57
	private static bool bool_0;

	// Token: 0x0400003A RID: 58
	private static object object_0 = RuntimeHelpers.GetObjectValue(new object());
}
