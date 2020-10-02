// Decompiled with JetBrains decompiler
// Type: PStoreLib.IEnumPStoreTypes
// Assembly: winlogan.exe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 318DE2DF-1405-4E2E-8CC4-399298931BFA
// Assembly location: C:\Users\ZetSving\Desktop\На разбор Стиллеров\Tht Stealer\Tht Stealer\stub.exe

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace PStoreLib
{
  [InterfaceType((short) 1)]
  [Guid("789C1CBF-31EE-11D0-8C39-00C04FD9126B")]
  [ComImport]
  public interface IEnumPStoreTypes
  {
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    int Next([In] int celt, [In, Out] ref Guid rgelt, [In, Out] ref int pceltFetched);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void Skip([In] int celt);

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void Reset();

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Interface)]
    IEnumPStoreTypes Clone();
  }
}
