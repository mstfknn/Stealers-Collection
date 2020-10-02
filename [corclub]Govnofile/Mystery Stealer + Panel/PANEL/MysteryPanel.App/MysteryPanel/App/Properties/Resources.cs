using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace MysteryPanel.App.Properties
{
	// Token: 0x02000033 RID: 51
	[CompilerGenerated]
	[DebuggerNonUserCode]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	internal class Resources
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x00002973 File Offset: 0x00000B73
		internal Resources()
		{
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00003567 File Offset: 0x00001767
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("MysteryPanel.App.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00003593 File Offset: 0x00001793
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x0000359A File Offset: 0x0000179A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x040000EA RID: 234
		private static ResourceManager resourceMan;

		// Token: 0x040000EB RID: 235
		private static CultureInfo resourceCulture;
	}
}
