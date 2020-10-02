using System;
using System.Runtime.InteropServices;

internal static class SafeNativeMethods
{
    [return: MarshalAs(UnmanagedType.Bool)]
    [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern bool CryptAcquireContext(out IntPtr phProv, string pszContainer, string pszProvider, uint dwProvType, uint dwFlags);

    [return: MarshalAs(UnmanagedType.Bool)]
    [DllImport("advapi32.dll", SetLastError = true)]
    public static extern bool CryptCreateHash(IntPtr hProv, Enums.ALG_ID algid, IntPtr hKey, uint dwFlags, ref IntPtr phHash);

    [return: MarshalAs(UnmanagedType.Bool)]
    [DllImport("advapi32.dll")]
    public static extern bool CryptDestroyHash(IntPtr hHash);

    [return: MarshalAs(UnmanagedType.Bool)]
    [DllImport("advapi32.dll", SetLastError = true)]
    public static extern bool CryptGetHashParam(IntPtr hHash, Enums.HashParameters dwParam, byte[] pbData, ref uint pdwDataLen, uint dwFlags);

    [return: MarshalAs(UnmanagedType.Bool)]
    [DllImport("advapi32.dll")]
    public static extern bool CryptHashData(IntPtr hHash, byte[] pbData, int dwDataLen, uint dwFlags);

    [return: MarshalAs(UnmanagedType.Bool)]
    [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool CryptReleaseContext(IntPtr hProv, uint dwFlags);
}