namespace PCInfo
{
    using System.Runtime.InteropServices;

    internal static class NativeMethods
    {
        [DllImport("kernel32.dll")]
        public static extern bool GetVersionEx(ref Structures.OSVERSIONINFOEX osVersionInfo);
    }
}