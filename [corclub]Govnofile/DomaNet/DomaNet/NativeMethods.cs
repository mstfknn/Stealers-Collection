namespace DomaNet
{
    using System;
    using System.Runtime.InteropServices;

    public class NativeMethods
    {
        [DllImport("Kernel32.dll")]
        internal static extern bool GetProductInfo(int osMajorVersion, int osMinorVersion, int spMajorVersion, int spMinorVersion, out int edition);

        [DllImport("kernel32.dll")]
        internal static extern void GetSystemInfo([MarshalAs(UnmanagedType.Struct)] ref SystemInfo.Structures.SYSTEM_INFO lpSystemInfo);

        [DllImport("kernel32.dll")]
        internal static extern bool GetVersionEx(ref SystemInfo.Structures.OSVERSIONINFOEX osVersionInfo);

        [DllImport("user32")]
        internal static extern int GetSystemMetrics(int nIndex);

        [DllImport("wininet.dll")]
        internal static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);

        [DllImport("kernel32.dll")]
        internal static extern IntPtr GetModuleHandle([MarshalAs(UnmanagedType.LPWStr)] string lpModuleName);
    }
}