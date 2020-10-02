using System;
using System.Runtime.InteropServices;

namespace BrowserPasswords.PStoreLib
{
	// Token: 0x02000031 RID: 49
	public abstract class guidType
	{
		// Token: 0x0400014E RID: 334
		[MarshalAs(UnmanagedType.LPStr)]
		public const string PST_CONFIGDATA_TYPE_STRING = "Configuration Data";

		// Token: 0x0400014F RID: 335
		[MarshalAs(UnmanagedType.LPStr)]
		public const string PST_PROTECTEDSTORAGE_SUBTYPE_STRING = "Protected Storage";

		// Token: 0x04000150 RID: 336
		[MarshalAs(UnmanagedType.LPStr)]
		public const string PST_PSTORE_PROVIDERS_SUBTYPE_STRING = "Protected Storage Provider List";
	}
}
