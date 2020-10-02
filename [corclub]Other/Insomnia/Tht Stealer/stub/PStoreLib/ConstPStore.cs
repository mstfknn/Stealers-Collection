// Decompiled with JetBrains decompiler
// Type: PStoreLib.ConstPStore
// Assembly: winlogan.exe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 318DE2DF-1405-4E2E-8CC4-399298931BFA
// Assembly location: C:\Users\ZetSving\Desktop\На разбор Стиллеров\Tht Stealer\Tht Stealer\stub.exe

using System.Diagnostics;

namespace PStoreLib
{
  public abstract class ConstPStore
  {
    public const int PST_AC_IMMEDIATE_CALLER = 2;
    public const int PST_AC_SINGLE_CALLER = 0;
    public const int PST_AC_TOP_LEVEL_CALLER = 1;
    public const int PST_AUTHENTICODE = 1;
    public const int PST_BINARY_CHECK = 2;
    public const int PST_CF_DEFAULT = 0;
    public const int PST_CF_NONE = 1;
    public const int PST_NO_OVERWRITE = 2;
    public const int PST_NO_UI_MIGRATION = 16;
    public const int PST_PC_HARDWARE = 2;
    public const int PST_PC_MULTIPLE_REPOSITORIES = 16;
    public const int PST_PC_PCMCIA = 8;
    public const int PST_PC_PFX = 1;
    public const int PST_PC_ROAMABLE = 32;
    public const int PST_PC_SMARTCARD = 4;
    public const int PST_PF_ALWAYS_SHOW = 1;
    public const int PST_PF_NEVER_SHOW = 2;
    public const int PST_PP_FLUSH_PW_CACHE = 1;
    public const int PST_PROMPT_QUERY = 8;
    public const int PST_RC_REMOVABLE = -2147483648;
    public const int PST_READ = 1;
    public const int PST_SECURITY_DESCRIPTOR = 4;
    public const int PST_SELF_RELATIVE_CLAUSE = -2147483648;
    public const int PST_UNRESTRICTED_ITEMDATA = 4;
    public const int PST_WRITE = 2;

    [DebuggerNonUserCode]
    protected ConstPStore()
    {
    }
  }
}
