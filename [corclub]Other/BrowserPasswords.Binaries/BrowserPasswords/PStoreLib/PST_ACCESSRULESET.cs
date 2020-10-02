using System;
using System.Runtime.InteropServices;

namespace BrowserPasswords.PStoreLib
{
	// Token: 0x02000038 RID: 56
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct PST_ACCESSRULESET
	{
		// Token: 0x04000159 RID: 345
		public int cbSize;

		// Token: 0x0400015A RID: 346
		public int cRules;

		// Token: 0x0400015B RID: 347
		public int rgRules;
	}
}
