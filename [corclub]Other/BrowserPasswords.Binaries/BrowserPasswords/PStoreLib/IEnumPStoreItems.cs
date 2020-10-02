using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace BrowserPasswords.PStoreLib
{
	// Token: 0x02000032 RID: 50
	[InterfaceType(1)]
	[Guid("5A6F1EC1-2DB1-11D0-8C39-00C04FD9126B")]
	[ComImport]
	public interface IEnumPStoreItems
	{
		// Token: 0x06000172 RID: 370
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int Next([In] int celt, [In] [Out] ref int rgelt, [In] [Out] ref int pceltFetched);

		// Token: 0x06000173 RID: 371
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Skip([In] int celt);

		// Token: 0x06000174 RID: 372
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Reset();

		// Token: 0x06000175 RID: 373
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IEnumPStoreItems Clone();
	}
}
