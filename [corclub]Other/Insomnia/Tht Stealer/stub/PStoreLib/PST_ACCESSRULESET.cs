// Decompiled with JetBrains decompiler
// Type: PStoreLib.PST_ACCESSRULESET
// Assembly: winlogan.exe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 318DE2DF-1405-4E2E-8CC4-399298931BFA
// Assembly location: C:\Users\ZetSving\Desktop\На разбор Стиллеров\Tht Stealer\Tht Stealer\stub.exe

using System.Runtime.InteropServices;

namespace PStoreLib
{
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  public struct PST_ACCESSRULESET
  {
    public int cbSize;
    public int cRules;
    public int rgRules;
  }
}
