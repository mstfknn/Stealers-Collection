using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace BrowserPasswords.PStoreLib
{
	// Token: 0x02000034 RID: 52
	[InterfaceType(1)]
	[Guid("789C1CBF-31EE-11D0-8C39-00C04FD9126B")]
	[ComImport]
	public interface IEnumPStoreTypes
	{
		// Token: 0x0600017A RID: 378
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int Next([In] int celt, [In] [Out] ref Guid rgelt, [In] [Out] ref int pceltFetched);

		// Token: 0x0600017B RID: 379
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Skip([In] int celt);

		// Token: 0x0600017C RID: 380
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Reset();

		// Token: 0x0600017D RID: 381
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IEnumPStoreTypes Clone();
	}
}
