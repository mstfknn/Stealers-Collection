using System;
using System.Runtime.InteropServices;

namespace BrowserPasswords.PStoreLib
{
	// Token: 0x02000037 RID: 55
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct PST_ACCESSRULE
	{
		// Token: 0x04000155 RID: 341
		public int cbSize;

		// Token: 0x04000156 RID: 342
		public int AccessModeFlags;

		// Token: 0x04000157 RID: 343
		public int cClauses;

		// Token: 0x04000158 RID: 344
		public int rgClauses;
	}
}
