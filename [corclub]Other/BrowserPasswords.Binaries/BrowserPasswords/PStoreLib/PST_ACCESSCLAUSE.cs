using System;
using System.Runtime.InteropServices;

namespace BrowserPasswords.PStoreLib
{
	// Token: 0x02000036 RID: 54
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct PST_ACCESSCLAUSE
	{
		// Token: 0x04000151 RID: 337
		public int cbSize;

		// Token: 0x04000152 RID: 338
		public int ClauseType;

		// Token: 0x04000153 RID: 339
		public int cbClauseData;

		// Token: 0x04000154 RID: 340
		public int pbClauseData;
	}
}
