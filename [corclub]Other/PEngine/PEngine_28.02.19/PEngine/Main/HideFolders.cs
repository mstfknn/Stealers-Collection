namespace PEngine.Main
{
    using Microsoft.Win32;
    using PEngine.Helpers;
    using System;
    using System.IO;
    using System.Security;

    public class HideFolders
    {
        public static bool Enabled(int value)
        {
            try
            {
                using (var hklmHive = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32))
                {
                    using (RegistryKey Key = hklmHive.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", Environment.Is64BitOperatingSystem ? true : false))
                    {
                        Key?.SetValue("Hidden", value, RegistryValueKind.DWord);
                        ProcessKiller.RefreshProcess("Explorer".ToLower());
                    }
                    return true;
                }
            }
            catch { return false; }
        }

        public static void WinDir(DirectoryInfo info)
        {
            try
            {
                if ((info.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                {
                    info.Attributes |= FileAttributes.Hidden;
                    ProcessKiller.RefreshProcess("Explorer".ToLower());
                }
            }
            catch (IOException) { }
            catch (ArgumentException) { }
            catch (SecurityException) { }
        }
    }
}