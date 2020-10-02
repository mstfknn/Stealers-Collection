// Decompiled with JetBrains decompiler
// Type: CUtils
// Assembly: winlogan.exe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 318DE2DF-1405-4E2E-8CC4-399298931BFA
// Assembly location: C:\Users\ZetSving\Desktop\На разбор Стиллеров\Tht Stealer\Tht Stealer\stub.exe

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class CUtils
{
  [DebuggerNonUserCode]
  public CUtils()
  {
  }

  protected string CopyString(IntPtr ptr)
  {
    return Marshal.PtrToStringUni(ptr);
  }

  protected int getStrLengthA(byte[] str)
  {
    int num1 = 0;
    byte[] numArray = str;
    int index = 0;
    while (index < numArray.Length)
    {
      byte num2 = numArray[index];
      checked { ++num1; }
      if ((int) num2 == 0)
        return num1;
      checked { ++index; }
    }
    return num1;
  }
}
