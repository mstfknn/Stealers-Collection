namespace PEngine.Engine.Applications.Steam
{
    using Microsoft.Win32;
    using System;

    public class SteamPath
    {
        private static readonly string SteamX = @"SOFTWARE\Wow6432Node\Valve\Steam"; // x64
        private static readonly string SteamY = @"Software\Valve\Steam"; // x32

        public static string GetLocationSteam(string Inst = "InstallPath", string Source = "SourceModInstallPath")
        {
            try
            {
                using (var BaseSteam = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32))
                {
                    using (RegistryKey Key = BaseSteam.OpenSubKey(SteamX, Environment.Is64BitOperatingSystem ? true : false))
                    {
                        using (RegistryKey Key2 = BaseSteam.OpenSubKey(SteamY, Environment.Is64BitOperatingSystem ? true : false))
                        {
                            return Key?.GetValue(Inst)?.ToString() ?? Key2?.GetValue(Source)?.ToString();
                        }
                    }
                }
            }
            catch { return string.Empty; }
        }
    }
}