// Decompiled with JetBrains decompiler
// Type: CSubType
// Assembly: winlogan.exe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 318DE2DF-1405-4E2E-8CC4-399298931BFA
// Assembly location: C:\Users\ZetSving\Desktop\На разбор Стиллеров\Tht Stealer\Tht Stealer\stub.exe

using PStoreLib;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

internal class CSubType : CUtils
{
  private IPStore m_IPStore;
  private Guid m_Guid;
  private Guid m_GuidSub;
  private string m_DisplayName;
  private PST_KEY m_KeyType;

  public string TypeGuid
  {
    get
    {
      return this.m_GuidSub.ToString();
    }
  }

  public string DisplayName
  {
    get
    {
      return this.m_DisplayName;
    }
  }

  [DebuggerNonUserCode]
  public CSubType()
  {
    this.m_Guid = new Guid();
    this.m_GuidSub = new Guid();
  }

  internal void Init(IPStore PStore, PST_KEY KeyType, ref Guid guidType, ref Guid guidSubType)
  {
    this.m_IPStore = PStore;
    this.m_Guid = guidType;
    this.m_GuidSub = guidSubType;
    int ppInfo;
    this.m_IPStore.GetSubtypeInfo(KeyType, ref guidType, ref guidSubType, out ppInfo, 0);
    if (ppInfo <= 0)
      return;
    IntPtr ptr = new IntPtr(ppInfo);
    PST_TYPEINFO pstTypeinfo1;
    object obj = Marshal.PtrToStructure(ptr, pstTypeinfo1.GetType());
    PST_TYPEINFO pstTypeinfo2;
    this.m_DisplayName = this.CopyString((obj != null ? (PST_TYPEINFO) obj : pstTypeinfo2).szDisplayName);
    ptr = new IntPtr(ppInfo);
    Marshal.FreeCoTaskMem(ptr);
  }

  public void Delete()
  {
    this.m_IPStore.DeleteSubtype(this.m_KeyType, ref this.m_Guid, ref this.m_GuidSub, 0);
  }
}
