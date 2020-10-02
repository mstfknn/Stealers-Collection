using System;

namespace BrowserPasswords.PStoreLib
{
	// Token: 0x02000030 RID: 48
	public abstract class guidProvider
	{
		// Token: 0x0400014A RID: 330
		public static readonly string MS_BASE_PSTPROVIDER_NAME = "System Protected Storage";

		// Token: 0x0400014B RID: 331
		public static readonly Guid MS_BASE_PSTPROVIDER_SZID = new Guid("{8a078c30-3755-11d0-a0bd-00aa0061426a}");

		// Token: 0x0400014C RID: 332
		public static readonly string MS_PFX_PSTPROVIDER_NAME = "PFX Storage Provider";

		// Token: 0x0400014D RID: 333
		public static readonly Guid MS_PFX_PSTPROVIDER_SZID = new Guid("{3ca94f30-7ac1-11d0-8c42-00c04fc299eb}");
	}
}
