// Decompiled with JetBrains decompiler
// Type: CSubTypes
// Assembly: winlogan.exe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 318DE2DF-1405-4E2E-8CC4-399298931BFA
// Assembly location: C:\Users\ZetSving\Desktop\На разбор Стиллеров\Tht Stealer\Tht Stealer\stub.exe

using Microsoft.VisualBasic;
using PStoreLib;
using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;

internal class CSubTypes : CUtils, IEnumerable, IEnumerator
{
  private IPStore m_IPStore;
  private PST_KEY m_KeyType;
  private Guid m_TypeGuid;
  private IEnumPStoreTypes m_IEnumType;
  private CSubType m_Current;

  public object Current
  {
    get
    {
      return (object) this.m_Current;
    }
  }

  [DebuggerNonUserCode]
  public CSubTypes()
  {
    this.m_TypeGuid = new Guid();
  }

  [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
  private static extern int lstrlenA(int lpString);

  [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
  private static extern int lstrlenW(int lpString);

  internal void Init(IPStore PStore, PST_KEY KeyType, ref Guid guidType)
  {
    this.m_IPStore = PStore;
    this.m_KeyType = KeyType;
    this.m_TypeGuid = guidType;
    this.m_IEnumType = PStore.EnumSubtypes(KeyType, ref guidType, 0);
  }

  public CSubType get_Item(int Index)
  {
    // ISSUE: unable to decompile the method.
  }

  public int get_Count(int Index)
  {
    // ISSUE: unable to decompile the method.
  }

  public void Delete(Guid guidSubType)
  {
    this.m_IPStore.DeleteSubtype(this.m_KeyType, ref this.m_TypeGuid, ref guidSubType, 0);
  }

  public void Add(Guid guidSubType, string szDisplayName)
  {
    PST_TYPEINFO pInfo;
    pInfo.cbSize = Strings.Len((object) pInfo);
    pInfo.szDisplayName = Marshal.StringToHGlobalUni(szDisplayName);
    PST_ACCESSRULESET pRules;
    pRules.cbSize = Strings.Len((object) pRules);
    pRules.cRules = 0;
    pRules.rgRules = 0;
    this.m_IPStore.CreateSubtype(this.m_KeyType, ref this.m_TypeGuid, ref guidSubType, ref pInfo, ref pRules, 0);
    Marshal.FreeHGlobal(pInfo.szDisplayName);
  }

  public IEnumerator GetEnumerator()
  {
    this.Reset();
    return (IEnumerator) this;
  }

  public void Reset()
  {
    this.m_IEnumType.Reset();
  }

  public bool MoveNext()
  {
    Guid rgelt = new Guid();
    int pceltFetched;
    if (this.m_IEnumType.Next(1, out rgelt, out pceltFetched) != 0)
      return false;
    this.m_Current = new CSubType();
    this.m_Current.Init(this.m_IPStore, this.m_KeyType, ref this.m_TypeGuid, ref rgelt);
    return true;
  }
}
