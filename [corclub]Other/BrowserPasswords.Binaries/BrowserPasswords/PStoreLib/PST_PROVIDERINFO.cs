using System;
using System.Runtime.InteropServices;

namespace BrowserPasswords.PStoreLib
{
	// Token: 0x0200003E RID: 62
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct PST_PROVIDERINFO
	{
		// Token: 0x04000184 RID: 388
		public int cbSize;

		// Token: 0x04000185 RID: 389
		public Guid ID;

		// Token: 0x04000186 RID: 390
		public int Capabilities;

		// Token: 0x04000187 RID: 391
		public IntPtr szProviderName;
	}
}
