using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public static class SystemInterop
{
    private delegate bool IsWow64ProcessDelegate([In] IntPtr handle, out bool isWow64Process);

    private static IsWow64ProcessDelegate GetIsWow64ProcessDelegate()
    {
        var hModule = NativeMethods.LoadLibrary("kernel32");
        if (hModule != IntPtr.Zero)
        {
            var procAddress = NativeMethods.GetProcAddress(hModule, "IsWow64Process");
            if (procAddress != IntPtr.Zero)
            {
                return (IsWow64ProcessDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(IsWow64ProcessDelegate));
            }
        }
        return null;
    }

    public static bool Is64BitWindows()
    {
        if (IntPtr.Size == 0x8)
        {
            return true;
        }
        var delegate2 = GetIsWow64ProcessDelegate();
        if (delegate2 == null)
        {
            return false;
        }
        if (!delegate2(Process.GetCurrentProcess().Handle, out bool flag))
        {
            return false;
        }
        return flag;
    }
}