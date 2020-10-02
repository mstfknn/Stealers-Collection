using System.Runtime.InteropServices;

namespace Confuser.Runtime
{
    internal static class SafeNativeMethods
    {
        [DllImport("kernel32.dll")]
        public static extern unsafe bool VirtualProtect(byte* lpAddress, int dwSize, uint flNewProtect, out uint lpflOldProtect);
    }
}