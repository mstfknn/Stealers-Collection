using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackWidow.Browser
{
    class GetApplication
    {
        public static string BrowserName(string path)
        {
            if (path.Contains("Chrome"))
                return "Google Chrome";

            if (path.Contains("Yandex"))
                return "Yandex Browser";

            if (path.Contains("Orbitum"))
                return "Orbitum Browser";

            if (path.Contains("Opera"))
                return "Opera Browser";

            if (path.Contains("Amigo"))
                return "Amigo Browser";

            if (path.Contains("Torch"))
                return "Torch Browser";

            if (path.Contains("Comodo"))
                return "Comodo Dragon Browser";

            if (path.Contains("Kometa"))
                return "Kometa Browser";

            if (path.Contains("Vivaldi"))
                return "Vivaldi Browser";

            if (path.Contains("Nichrome"))
                return "Nichrome Browser";

            if (path.Contains("Vivaldi"))
                return "Vivaldi Browser";

            if (path.Contains("Epic"))
                return "Epic Privacy Browser";

            if (path.Contains("CocCoc"))
                return "CocCoc Browser";

            if (path.Contains("360Browser"))
                return "360Browser";

            if (path.Contains("Sputnik"))
                return "Sputnik Browser";

            if (path.Contains("Uran"))
                return "Uran Browser";

            if (path.Contains("CentBrowser"))
                return "CentBrowser";

            if (path.Contains("7Star"))
                return "7Star Browser";

            if (path.Contains("Elements"))
                return "Elements Browser";

            if (path.Contains("Superbird"))
                return "Superbird Browser";

            if (path.Contains("Chedot"))
                return "Chedot Browser";

            if (path.Contains("Suhba"))
                return "Suhba Browser";

            if (path.Contains("Mustang"))
                return "Mustang Browser";

            if (path.Contains("Maxthon5"))
                return "Maxthon Browser";

            if (path.Contains("Neon"))
                return "Opera Neon Browser";

            return null;
        }
    }
}
