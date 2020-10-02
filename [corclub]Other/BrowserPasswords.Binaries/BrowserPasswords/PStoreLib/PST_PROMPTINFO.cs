using System;
using System.Runtime.InteropServices;

namespace BrowserPasswords.PStoreLib
{
	// Token: 0x0200003D RID: 61
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct PST_PROMPTINFO
	{
		// Token: 0x04000180 RID: 384
		public int cbSize;

		// Token: 0x04000181 RID: 385
		public int dwPromptFlags;

		// Token: 0x04000182 RID: 386
		public int hwndApp;

		// Token: 0x04000183 RID: 387
		public int szPrompt;
	}
}
