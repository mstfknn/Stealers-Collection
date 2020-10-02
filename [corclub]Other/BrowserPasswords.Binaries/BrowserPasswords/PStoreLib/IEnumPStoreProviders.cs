using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace BrowserPasswords.PStoreLib
{
	// Token: 0x02000033 RID: 51
	[Guid("5A6F1EBF-2DB1-11D0-8C39-00C04FD9126B")]
	[InterfaceType(1)]
	[ComImport]
	public interface IEnumPStoreProviders
	{
		// Token: 0x06000176 RID: 374
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int Next([In] int celt, [In] [Out] ref int rgelt, [In] [Out] ref int pceltFetched);

		// Token: 0x06000177 RID: 375
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Skip([In] int celt);

		// Token: 0x06000178 RID: 376
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Reset();

		// Token: 0x06000179 RID: 377
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IEnumPStoreProviders Clone();
	}
}
