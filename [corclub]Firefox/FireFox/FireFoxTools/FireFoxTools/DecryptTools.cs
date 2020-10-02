using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace FireFoxTools
{
    public class DecryptTools
    {
        private static IntPtr _nssModule { get; set; }
        private static IntPtr moduleHandle;

        #region FireFox_DLL

        private static string[] Massive = new string[]
        {
          "mozglue.dll", "softokn3.dll", "nspr4.dll", 
          "mozsqlite3.dll",  "nssutil3.dll", "nssutil3.dll",
          "plc4.dll", "plds4.dll"
        };

        #endregion

        public static void InitDelegates()
        {
            try
            {
                foreach (var CheckDLL in Massive)
                {
                    if (File.Exists(Path.Combine(FireFoxPath.GetRegistryFireFox(), CheckDLL)))
                    {
                        Console.WriteLine(Path.Combine(FireFoxPath.GetRegistryFireFox(), CheckDLL));
                    }
                }
                if (File.Exists(Path.Combine(FireFoxPath.GetRegistryFireFox(), "nss3.dll")))
                {
                    Console.WriteLine(Path.Combine(FireFoxPath.GetRegistryFireFox(), "nss3.dll"));
                }
            }
            catch { }
        }

        private static IntPtr LoadDLL(string PathLibrary)
        {
            moduleHandle = NativeMethods.LoadLibrary(PathLibrary);
            if (moduleHandle == IntPtr.Zero)
            {
                throw new Exception($"Failed to load library (ErrorCode: {Marshal.GetLastWin32Error()})");
            }
            return moduleHandle;
        }

        public static void DisposeLibrary()
        {
            if (moduleHandle != IntPtr.Zero)
            {
                NativeMethods.FreeLibrary(moduleHandle);
            }
        }
        /*
        public static string Decrypt(string cypherText)
        {
            StringBuilder sb = new StringBuilder(cypherText);
            Structures.TSECItem tSecDec = new Structures.TSECItem();
            Structures.TSECItem item = (Structures.TSECItem)Marshal.PtrToStructure(new IntPtr(NSSBase64_DecodeBuffer(IntPtr.Zero, IntPtr.Zero, sb, sb.Length)), typeof(Structures.TSECItem));
            if (PK11SDR_Decrypt(ref item, ref tSecDec, 0) == 0)
            {
                if (tSecDec.SECItemLen != 0)
                {
                    Marshal.Copy(new IntPtr(tSecDec.SECItemData), new byte[tSecDec.SECItemLen], 0, tSecDec.SECItemLen);
                    return Encoding.UTF8.GetString(new byte[tSecDec.SECItemLen]);
                }
            }
            return "Error";
        }

        public static string NewDecrypt(string cypherText)
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

        #region Delegates

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

    */
    
        private static IntPtr NSS3;

        public static long NSS_Init(string path)
        {
            NativeMethods.LoadLibrary(Path.Combine(FireFoxPath.GetRegistryFireFox(), "mozglue.dll")); 
            NativeMethods.LoadLibrary(Path.Combine(FireFoxPath.GetRegistryFireFox(), "nssutil3.dll"));
            NativeMethods.LoadLibrary(Path.Combine(FireFoxPath.GetRegistryFireFox(), "plc4.dll"));
            NativeMethods.LoadLibrary(Path.Combine(FireFoxPath.GetRegistryFireFox(), "nspr4.dll"));
            NativeMethods.LoadLibrary(Path.Combine(FireFoxPath.GetRegistryFireFox(), "MOZCRT19.dll"));
           // NativeMethods.LoadLibrary(Path.Combine(FireFoxPath.GetRegistryFireFox(), ""));
            NSS3 = NativeMethods.LoadLibrary(Path.Combine(FireFoxPath.GetRegistryFireFox(), "nss3.dll"));
            IntPtr procAddress = NativeMethods.GetProcAddress(NSS3, "NSS_Init");
            DLLFunctionDelegate dLLFunctionDelegate = (DLLFunctionDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(DLLFunctionDelegate));
            return dLLFunctionDelegate(path);
        }

        public static int NSSBase64_DecodeBuffer(IntPtr arenaOpt, IntPtr outItemOpt, string inStr, int inLen)
        {
            IntPtr procAddress = NativeMethods.GetProcAddress(NSS3, "NSSBase64_DecodeBuffer");
            DLLFunctionDelegate2 dLLFunctionDelegate = (DLLFunctionDelegate2)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(DLLFunctionDelegate2));
            return dLLFunctionDelegate(arenaOpt, outItemOpt, inStr, inLen);
        }

        public static long PK11_GetInternalKeySlot()
        {
            IntPtr procAddress = NativeMethods.GetProcAddress(NSS3, "PK11_GetInternalKeySlot");
            DLLFunctionDelegate3 dLLFunctionDelegate = (DLLFunctionDelegate3)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(DLLFunctionDelegate3));
            return dLLFunctionDelegate();
        }

        public static long PK11_Authenticate(long slot, bool loadCerts, long wincx)
        {
            IntPtr procAddress = NativeMethods.GetProcAddress(NSS3, "PK11_Authenticate");
            DLLFunctionDelegate4 dLLFunctionDelegate = (DLLFunctionDelegate4)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(DLLFunctionDelegate4));
            return dLLFunctionDelegate(slot, loadCerts, wincx);
        }

        public static int PK11SDR_Decrypt(ref Structures.TSECItem data, ref Structures.TSECItem result, int cx)
        {
            IntPtr procAddress = NativeMethods.GetProcAddress(NSS3, "PK11SDR_Decrypt");
            DLLFunctionDelegate5 dLLFunctionDelegate = (DLLFunctionDelegate5)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(DLLFunctionDelegate5));
            return dLLFunctionDelegate(ref data, ref result, cx);
        }

        public static int NSS_Shutdown()
        {
            IntPtr procAddress = NativeMethods.GetProcAddress(NSS3, "NSS_Shutdown");
            DLLFunctionDelegate6 dLLFunctionDelegate = (DLLFunctionDelegate6)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(DLLFunctionDelegate6));
            return dLLFunctionDelegate();
        }

        public static int PK11_FreeSlot(long slot)
        {
            IntPtr procAddress = NativeMethods.GetProcAddress(NSS3, "PK11_FreeSlot");
            DLLFunctionDelegate7 dLLFunctionDelegate = (DLLFunctionDelegate7)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(DLLFunctionDelegate7));
            return dLLFunctionDelegate(slot);
        }

        public static string Decrypt(string S)
        {
            Structures.TSECItem tSECItem = default(Structures.TSECItem);
            NSS_Init(FireFoxPath.GetRegistryFireFox());
            long num = PK11_GetInternalKeySlot();
            if (num == 0)
            {
                return string.Empty;
            }
            PK11_Authenticate(num, true, 0L);
            int value = NSSBase64_DecodeBuffer(IntPtr.Zero, IntPtr.Zero, S, S.Length);
            Structures.TSECItem tSECItem2 = (Structures.TSECItem)Marshal.PtrToStructure(new IntPtr(value), typeof(Structures.TSECItem));
            if (PK11SDR_Decrypt(ref tSECItem2, ref tSECItem, 0) != 0)
            {
                return string.Empty;
            }
            if (tSECItem.SECItemLen != 0)
            {
                byte[] array = new byte[tSECItem.SECItemLen];
                Marshal.Copy(new IntPtr(tSECItem.SECItemData), array, 0, tSECItem.SECItemLen);
                return Encoding.UTF8.GetString(array);
            }
            return string.Empty;
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long DLLFunctionDelegate(string path);

        public delegate int DLLFunctionDelegate2(IntPtr arenaOpt, IntPtr outItemOpt, string inStr, int inLen);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long DLLFunctionDelegate3();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long DLLFunctionDelegate4(long slot, bool loadCerts, long wincx);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int DLLFunctionDelegate5(ref Structures.TSECItem data, ref Structures.TSECItem result, int cx);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int DLLFunctionDelegate6();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int DLLFunctionDelegate7(long slot);
    }
}