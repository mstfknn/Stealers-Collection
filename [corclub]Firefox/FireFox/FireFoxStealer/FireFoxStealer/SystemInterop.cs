using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public static class SystemInterop
{
    private static IsWow64ProcessDelegate GetIsWow64ProcessDelegate()
    {
        IntPtr hModule = LoadLibrary("kernel32");
        if (hModule != IntPtr.Zero)
        {
            IntPtr procAddress = GetProcAddress(hModule, "IsWow64Process");
            if (procAddress != IntPtr.Zero)
            {
                return (IsWow64ProcessDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(IsWow64ProcessDelegate));
            }
        }
        return null;
    }
    public static bool Is64BitWindows()
    {
        bool flag;
        if (IntPtr.Size == 8)
        {
            return true;
        }
        IsWow64ProcessDelegate delegate2 = GetIsWow64ProcessDelegate();
        if (delegate2 == null)
        {
            return false;
        }
        if (!delegate2(Process.GetCurrentProcess().Handle, out flag))
        {
            return false;
        }
        return flag;
    }
    #region DLL_Imports and Delegate
    [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpFileName);
    [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
    private delegate bool IsWow64ProcessDelegate([In] IntPtr handle, out bool isWow64Process);
    #endregion DLL_Imports and Delegate
}