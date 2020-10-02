// Decompiled with JetBrains decompiler
// Type: PStoreLib.IPStore
// Assembly: winlogan.exe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 318DE2DF-1405-4E2E-8CC4-399298931BFA
// Assembly location: C:\Users\ZetSving\Desktop\На разбор Стиллеров\Tht Stealer\Tht Stealer\stub.exe

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace PStoreLib
{
  [InterfaceType((short) 1)]
  [ComConversionLoss]
  [Guid("5A6F1EC0-2DB1-11D0-8C39-00C04FD9126B")]
  [ComImport]
  public interface IPStore
  {
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void GetInfo([In, Out] IntPtr ppProperties);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void GetProvParam([In] int dwParam, [In, Out] ref int pcbData, [In, Out] ref int ppbData, [In] int dwFlags);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void SetProvParam([In] int dwParam, [In] int cbData, [In] int pbData, [In] int dwFlags);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void CreateType([In] PST_KEY Key, [In] ref Guid pType, [In] ref PST_TYPEINFO pInfo, [In] int dwFlags);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void GetTypeInfo([In] PST_KEY Key, [In] ref Guid pType, [In, Out] ref int ppInfo, [In] int dwFlags);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void DeleteType([In] PST_KEY Key, [In] ref Guid pType, [In] int dwFlags);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void CreateSubtype([In] PST_KEY Key, [In] ref Guid pType, [In] ref Guid pSubtype, [In] ref PST_TYPEINFO pInfo, [In] ref PST_ACCESSRULESET pRules, [In] int dwFlags);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void GetSubtypeInfo([In] PST_KEY Key, [In] ref Guid pType, [In] ref Guid pSubtype, [In, Out] ref int ppInfo, [In] int dwFlags);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void DeleteSubtype([In] PST_KEY Key, [In] ref Guid pType, [In] ref Guid pSubtype, [In] int dwFlags);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void ReadAccessRuleset([In] PST_KEY Key, [In] ref Guid pType, [In] ref Guid pSubtype, [In, Out] ref int ppRules, [In] int dwFlags);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void WriteAccessRuleset([In] PST_KEY Key, [In] ref Guid pType, [In] ref Guid pSubtype, [In] ref PST_ACCESSRULESET pRules, [In] int dwFlags);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IEnumPStoreTypes EnumTypes([In] PST_KEY Key, [In] int dwFlags);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IEnumPStoreTypes EnumSubtypes([In] PST_KEY Key, [In] ref Guid pType, [In] int dwFlags);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void DeleteItem([In] PST_KEY Key, [In] ref Guid pItemType, [In] ref Guid pItemSubtype, [MarshalAs(UnmanagedType.LPWStr), In] string szItemName, [In] int pPromptInfo, [In] int dwFlags);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void ReadItem([In] PST_KEY Key, [In] ref Guid pItemType, [In] ref Guid pItemSubtype, [MarshalAs(UnmanagedType.LPWStr), In] string szItemName, [In, Out] ref int pcbData, [In, Out] ref IntPtr ppbData, [In] int pPromptInfo, [In] int dwFlags);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void WriteItem([In] PST_KEY Key, [In] ref Guid pItemType, [In] ref Guid pItemSubtype, [MarshalAs(UnmanagedType.LPWStr), In] string szItemName, [In] int cbData, [In] int pbData, [In] ref PST_PROMPTINFO pPromptInfo, [In] int dwDefaultConfirmationStyle, [In] int dwFlags);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void OpenItem([In] PST_KEY Key, [In] ref Guid pItemType, [In] ref Guid pItemSubtype, [MarshalAs(UnmanagedType.LPWStr), In] string szItemName, [In] int ModeFlags, [In] ref PST_PROMPTINFO pPromptInfo, [In] int dwFlags);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void CloseItem([In] PST_KEY Key, [In] ref Guid pItemType, [In] ref Guid pItemSubtype, [MarshalAs(UnmanagedType.LPWStr), In] string szItemName, [In] int dwFlags);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IEnumPStoreItems EnumItems([In] PST_KEY Key, [In] ref Guid pItemType, [In] ref Guid pItemSubtype, [In] int dwFlags);
  }
}
