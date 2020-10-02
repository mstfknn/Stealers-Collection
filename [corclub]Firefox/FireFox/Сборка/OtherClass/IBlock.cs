using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;

internal class IBlock
{
    #region Constants
    public const int INTERNET_OPTION_REFRESH = 0x25;
    public const int INTERNET_OPTION_SETTINGS_CHANGED = 0x27;
    #endregion
    public static void Enabled(int block_internet = 0)
    {
        try
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings", true);
            key.SetValue("ProxyEnable", block_internet);
            key.SetValue("ProxyServer", "2.23.143.150:443");
            InternetSetOption(IntPtr.Zero, 0x27, IntPtr.Zero, 0);
            InternetSetOption(IntPtr.Zero, 0x25, IntPtr.Zero, 0);
        }
        catch { }
    }
    #region DLL_Imports
    [DllImport("wininet.dll")]
    public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);
    #endregion
}