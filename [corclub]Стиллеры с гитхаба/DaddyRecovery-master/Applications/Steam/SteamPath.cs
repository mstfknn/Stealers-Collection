namespace DaddyRecovery.Applications.Steam
{
    using Helpers;
    using Microsoft.Win32;
    using System;

    public static class SteamPath
    {
        private const string SteamX = @"SOFTWARE\Wow6432Node\Valve\Steam", SteamY = @"Software\Valve\Steam";
        public static string GetLocationSteam(string Inst = "InstallPath", string Source = "SourceModInstallPath")
        {
            RegistryHive registryHive = RunChecker.IsAdmin ? RegistryHive.LocalMachine : RegistryHive.CurrentUser;
            RegistryView registryView = RunChecker.IsWin64 ? RegistryView.Registry64 : RegistryView.Registry32;
            string result = string.Empty;
            try
            {
                using (var BaseSteam = RegistryKey.OpenBaseKey(registryHive, registryView))
                using (RegistryKey Key = BaseSteam.OpenSubKey(SteamX, RunChecker.IsWin64))
                using (RegistryKey Key2 = BaseSteam.OpenSubKey(SteamY, RunChecker.IsWin64))
                {
                    result = Key?.GetValue(Inst)?.ToString() ?? Key2?.GetValue(Source)?.ToString();
                }
            }
            catch (Exception) { }
            return result;
        }
    }
}