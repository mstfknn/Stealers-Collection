namespace DomaNet
{
    using Microsoft.Win32;
    using System;

    public class BlockIE
    {
        public static void Enabled(string RegPath, int Block_internet = 0, string ProxyIP = "2.23.143.150:443", string ProxyEnable = "ProxyEnable", string ProxyServer = "ProxyServer")
        {
            using (var hklmHive_x64 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, (Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32)))
            {
                using (var runKey = hklmHive_x64.OpenSubKey(RegPath, (Environment.Is64BitOperatingSystem ? true : false)))
                {
                    runKey.SetValue(ProxyEnable, Block_internet);
                    runKey.SetValue(ProxyServer, ProxyIP);

                    NativeMethods.InternetSetOption(IntPtr.Zero, 0x27, IntPtr.Zero, 0);
                    NativeMethods.InternetSetOption(IntPtr.Zero, 0x25, IntPtr.Zero, 0);
                }
            }
        }
    }
}