namespace DomaNet.Steam
{
    using Microsoft.Win32;

    internal class SteamPath
    {
        private static readonly string SteamPath_x64 = @"SOFTWARE\Wow6432Node\Valve\Steam";
        private static readonly string SteamPath_x32 = @"Software\Valve\Steam";

        public static string GetLocationSteam(string Inst = "InstallPath", string Source = "SourceModInstallPath", bool Recursive = true)
        {
            using (var Key = Registry.LocalMachine.OpenSubKey(name: SteamPath_x64, writable: Recursive))
            {
                using (var Key2 = Registry.LocalMachine.OpenSubKey(name: SteamPath_x32, writable: Recursive))
                {
                    return Key?.GetValue(name: Inst)?.ToString() ?? Key2?.GetValue(name: Source)?.ToString();
                }
            }
        }
    }
}