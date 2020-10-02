using System;
using System.Runtime.InteropServices;

namespace BrowserPasswords.PStoreLib
{
	// Token: 0x02000039 RID: 57
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct PST_AUTHENTICODEDATA
	{
		// Token: 0x0400015C RID: 348
		public int cbSize;

		// Token: 0x0400015D RID: 349
		public int dwModifiers;

		// Token: 0x0400015E RID: 350
		public int szRootCA;

		// Token: 0x0400015F RID: 351
		public int szIssuer;

		// Token: 0x04000160 RID: 352
		public int szPublisher;

		// Token: 0x04000161 RID: 353
		public int szProgramName;
	}
}
