using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace BrowserPasswords.PStoreLib
{
	// Token: 0x02000035 RID: 53
	[ComConversionLoss]
	[Guid("5A6F1EC0-2DB1-11D0-8C39-00C04FD9126B")]
	[InterfaceType(1)]
	[ComImport]
	public interface IPStore
	{
		// Token: 0x0600017E RID: 382
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetInfo([In] [Out] IntPtr ppProperties);

		// Token: 0x0600017F RID: 383
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetProvParam([In] int dwParam, [In] [Out] ref int pcbData, [In] [Out] ref int ppbData, [In] int dwFlags);

		// Token: 0x06000180 RID: 384
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetProvParam([In] int dwParam, [In] int cbData, [In] int pbData, [In] int dwFlags);

		// Token: 0x06000181 RID: 385
		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateType([In] PST_KEY Key, [In] ref Guid pType, [In] ref PST_TYPEINFO pInfo, [In] int dwFlags);

		// Token: 0x06000182 RID: 386
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetTypeInfo([In] PST_KEY Key, [In] ref Guid pType, [In] [Out] ref int ppInfo, [In] int dwFlags);

		// Token: 0x06000183 RID: 387
		[MethodImpl(MethodImplOptions.InternalCall)]
		void DeleteType([In] PST_KEY Key, [In] ref Guid pType, [In] int dwFlags);

		// Token: 0x06000184 RID: 388
		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateSubtype([In] PST_KEY Key, [In] ref Guid pType, [In] ref Guid pSubtype, [In] ref PST_TYPEINFO pInfo, [In] ref PST_ACCESSRULESET pRules, [In] int dwFlags);

		// Token: 0x06000185 RID: 389
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetSubtypeInfo([In] PST_KEY Key, [In] ref Guid pType, [In] ref Guid pSubtype, [In] [Out] ref int ppInfo, [In] int dwFlags);

		// Token: 0x06000186 RID: 390
		[MethodImpl(MethodImplOptions.InternalCall)]
		void DeleteSubtype([In] PST_KEY Key, [In] ref Guid pType, [In] ref Guid pSubtype, [In] int dwFlags);

		// Token: 0x06000187 RID: 391
		[MethodImpl(MethodImplOptions.InternalCall)]
		void ReadAccessRuleset([In] PST_KEY Key, [In] ref Guid pType, [In] ref Guid pSubtype, [In] [Out] ref int ppRules, [In] int dwFlags);

		// Token: 0x06000188 RID: 392
		[MethodImpl(MethodImplOptions.InternalCall)]
		void WriteAccessRuleset([In] PST_KEY Key, [In] ref Guid pType, [In] ref Guid pSubtype, [In] ref PST_ACCESSRULESET pRules, [In] int dwFlags);

		// Token: 0x06000189 RID: 393
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IEnumPStoreTypes EnumTypes([In] PST_KEY Key, [In] int dwFlags);

		// Token: 0x0600018A RID: 394
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IEnumPStoreTypes EnumSubtypes([In] PST_KEY Key, [In] ref Guid pType, [In] int dwFlags);

		// Token: 0x0600018B RID: 395
		[MethodImpl(MethodImplOptions.InternalCall)]
		void DeleteItem([In] PST_KEY Key, [In] ref Guid pItemType, [In] ref Guid pItemSubtype, [MarshalAs(UnmanagedType.LPWStr)] [In] string szItemName, [In] int pPromptInfo, [In] int dwFlags);

		// Token: 0x0600018C RID: 396
		[MethodImpl(MethodImplOptions.InternalCall)]
		void ReadItem([In] PST_KEY Key, [In] ref Guid pItemType, [In] ref Guid pItemSubtype, [MarshalAs(UnmanagedType.LPWStr)] [In] string szItemName, [In] [Out] ref int pcbData, [In] [Out] ref IntPtr ppbData, [In] int pPromptInfo, [In] int dwFlags);

		// Token: 0x0600018D RID: 397
		[MethodImpl(MethodImplOptions.InternalCall)]
		void WriteItem([In] PST_KEY Key, [In] ref Guid pItemType, [In] ref Guid pItemSubtype, [MarshalAs(UnmanagedType.LPWStr)] [In] string szItemName, [In] int cbData, [In] int pbData, [In] ref PST_PROMPTINFO pPromptInfo, [In] int dwDefaultConfirmationStyle, [In] int dwFlags);

		// Token: 0x0600018E RID: 398
		[MethodImpl(MethodImplOptions.InternalCall)]
		void OpenItem([In] PST_KEY Key, [In] ref Guid pItemType, [In] ref Guid pItemSubtype, [MarshalAs(UnmanagedType.LPWStr)] [In] string szItemName, [In] int ModeFlags, [In] ref PST_PROMPTINFO pPromptInfo, [In] int dwFlags);

		// Token: 0x0600018F RID: 399
		[MethodImpl(MethodImplOptions.InternalCall)]
		void CloseItem([In] PST_KEY Key, [In] ref Guid pItemType, [In] ref Guid pItemSubtype, [MarshalAs(UnmanagedType.LPWStr)] [In] string szItemName, [In] int dwFlags);

		// Token: 0x06000190 RID: 400
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IEnumPStoreItems EnumItems([In] PST_KEY Key, [In] ref Guid pItemType, [In] ref Guid pItemSubtype, [In] int dwFlags);
	}
}
