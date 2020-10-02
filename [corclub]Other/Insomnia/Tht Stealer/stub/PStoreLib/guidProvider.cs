// Decompiled with JetBrains decompiler
// Type: PStoreLib.guidProvider
// Assembly: winlogan.exe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 318DE2DF-1405-4E2E-8CC4-399298931BFA
// Assembly location: C:\Users\ZetSving\Desktop\На разбор Стиллеров\Tht Stealer\Tht Stealer\stub.exe

using System;
using System.Diagnostics;

namespace PStoreLib
{
  public abstract class guidProvider
  {
    public static readonly string MS_BASE_PSTPROVIDER_NAME = "System Protected Storage";
    public static readonly Guid MS_BASE_PSTPROVIDER_SZID = new Guid("{8a078c30-3755-11d0-a0bd-00aa0061426a}");
    public static readonly string MS_PFX_PSTPROVIDER_NAME = "PFX Storage Provider";
    public static readonly Guid MS_PFX_PSTPROVIDER_SZID = new Guid("{3ca94f30-7ac1-11d0-8c42-00c04fc299eb}");

    [DebuggerNonUserCode]
    protected guidProvider()
    {
    }
  }
}
