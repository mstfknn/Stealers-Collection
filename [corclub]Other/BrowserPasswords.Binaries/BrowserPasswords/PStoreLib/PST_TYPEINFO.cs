using System;
using System.Runtime.InteropServices;

namespace BrowserPasswords.PStoreLib
{
	// Token: 0x0200003F RID: 63
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct PST_TYPEINFO
	{
		// Token: 0x04000188 RID: 392
		public int cbSize;

		// Token: 0x04000189 RID: 393
		public IntPtr szDisplayName;
	}
}
