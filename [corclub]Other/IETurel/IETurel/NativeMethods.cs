using System;
using System.Runtime.InteropServices;
using System.Text;

internal static class NativeMethods
{
    [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
    public static extern int UrlCanonicalize(string pszUrl, StringBuilder pszCanonicalized, ref int pcchCanonicalized, Enums.Shlwapi_URL dwFlags);

    [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
    public static extern bool SystemTimeToFileTime([In] ref Structures.SYSTEMTIME lpSystemTime, out System.Runtime.InteropServices.ComTypes.FILETIME lpFileTime);

    [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
    public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref Structures.SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

    [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
    public static extern bool FileTimeToSystemTime(ref System.Runtime.InteropServices.ComTypes.FILETIME FileTime, ref Structures.SYSTEMTIME SystemTime);

    [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
    public static extern int CompareFileTime([In] ref System.Runtime.InteropServices.ComTypes.FILETIME lpFileTime1, [In] ref System.Runtime.InteropServices.ComTypes.FILETIME lpFileTime2);

    [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

    [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPWStr)] string lpFileName);
}