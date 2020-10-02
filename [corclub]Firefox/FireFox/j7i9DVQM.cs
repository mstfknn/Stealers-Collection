using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Runtime.InteropServices;

namespace FirefoxPasswordExtractor
{
    class Program
    {
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
                    Console.WriteLine("===================Created by MOD================");
                    Console.WriteLine(reader[0].ToString());
                    Console.WriteLine(reader[1].ToString());
                    Console.WriteLine(reader[2].ToString());
                    Console.WriteLine(reader[3].ToString());
                    Console.WriteLine(reader[4].ToString());
                    Console.WriteLine((byte[])reader[5]);
                    var item = reader[5];
                    File.WriteAllBytes(@"c:\data.dmp", (byte[])item);
                    Console.WriteLine(reader[6].ToString());
                    Console.WriteLine(reader[7].ToString());
                    Console.WriteLine(reader[8].ToString());
                    Console.WriteLine(reader[9].ToString());
                    Console.WriteLine(reader[10].ToString());
                    Console.WriteLine(reader[11].ToString());
                    Console.WriteLine(reader[12].ToString());
                }
                Console.ReadLine();
            }

        }


        private static string GetProfile()
        {
            //CHANGE THIS LINE TO RETURN THE PATH OF YOUR PROFILE
            return @"C:\Users\Rami\Local Settings\Application Data\Google\Chrome\User Data\Default\";
        }
    }
}