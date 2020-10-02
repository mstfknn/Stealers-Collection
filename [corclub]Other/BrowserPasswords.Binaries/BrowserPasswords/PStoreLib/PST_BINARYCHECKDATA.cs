using System;
using System.Runtime.InteropServices;

namespace BrowserPasswords.PStoreLib
{
	// Token: 0x0200003A RID: 58
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct PST_BINARYCHECKDATA
	{
		// Token: 0x04000162 RID: 354
		public int cbSize;

		// Token: 0x04000163 RID: 355
		public int dwModifiers;

		// Token: 0x04000164 RID: 356
		public int szFilePath;
	}
}
