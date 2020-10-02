namespace PEngine.Engine.Browsers.Chromium
{
    using System.Collections.Generic;
    using System.IO;

    public class GetApplication
    {
        public static string GetBrowserName(string path)
        {
            if (path.Contains("Chrome"))
            {
                return "Google Chrome";
            }

            if (path.Contains("Citrio"))
            {
                return "Citrio Browser";
            }

            if (path.Contains("MapleStudio"))
            {
                return "CoolNovo";
            }

            if (path.Contains("Avant Profiles"))
            {
                return "Avant Webkit";
            }

            if (path.Contains("Chromium"))
            {
                return "SRWare Iron";
            }

            if (path.Contains("Iridium"))
            {
                return "Iridium";
            }

            if (path.Contains("Yandex"))
            {
                return "Yandex";
            }

            if (path.Contains("Orbitum"))
            {
                return "Orbitum";
            }

            if (path.Contains("Opera Stable"))
            {
                return "Opera";
            }

            if (path.Contains("Kinza"))
            {
                return "Kinza";
            }

            if (path.Contains("Brave-Browser"))
            {
                return "Brave";
            }

            if (path.Contains("Opera Developer"))
            {
                return "Opera Next";
            }

            if (path.Contains("Amigo"))
            {
                return "Amigo";
            }

            if (path.Contains("Torch"))
            {
                return "Torch";
            }

            if (path.Contains("Comodo"))
            {
                return "Comodo Dragon";
            }

            if (path.Contains("Kometa"))
            {
                return "Kometa";
            }

            if (path.Contains("Vivaldi"))
            {
                return "Vivaldi";
            }

            if (path.Contains("Nichrome"))
            {
                return "Nichrome Rambler";
            }

            if (path.Contains("Epic"))
            {
                return "Epic Privacy";
            }

            if (path.Contains("CocCoc"))
            {
                return "CocCoc";
            }

            if (path.Contains("360Browser"))
            {
                return "360Browser";
            }

            if (path.Contains("Sputnik"))
            {
                return "Sputnik";
            }

            if (path.Contains("Uran"))
            {
                return "Uran";
            }

            if (path.Contains("CentBrowser"))
            {
                return "CentBrowser";
            }

            if (path.Contains("7Star"))
            {
                return "7Star";
            }

            if (path.Contains("Elements"))
            {
                return "Elements";
            }

            if (path.Contains("Superbird"))
            {
                return "Superbird";
            }

            if (path.Contains("Chedot"))
            {
                return "Chedot";
            }

            if (path.Contains("Suhba"))
            {
                return "Suhba";
            }

            if (path.Contains("Mustang"))
            {
                return "Mustang";
            }

            if (path.Contains("Neon"))
            {
                return "Opera Neon";
            }

            return "Unknown Browser";
        }

        public static string GetBrowserTitle(List<string> Item)
        {
            foreach (string Br in Item)
            {
                if (Directory.Exists(Path.GetDirectoryName(Br)))
                {
                    return Path.GetFileName(Path.GetDirectoryName(Br));
                }
            }
            return "Unknown Browser";
        }
    }
}