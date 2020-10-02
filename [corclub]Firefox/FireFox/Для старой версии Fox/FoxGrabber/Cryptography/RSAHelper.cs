namespace FoxGrabber.Cryptography
{
    using System.Security.Cryptography;
    using System.Text;

    public class RSAHelper
    {
        public static string RSAEncryptor(string value)
        {
            var manager = new RSAKeyManager();
            using (RSACryptoServiceProvider RSA = manager.KeyFromContainer)
            {
                try
                {
                    return RSA.Encrypt(Encoding.Unicode.GetBytes(value), false).ToString();
                }
                catch (CryptographicException)
                {
                    return string.Empty;
                }
            }
        }

        public static string RSADecryptor(string value)
        {
            var manager = new RSAKeyManager();
            using (RSACryptoServiceProvider RSA = manager.KeyFromContainer)
            {
                try
                {
                    return string.IsNullOrEmpty(value) ? string.Empty : RSA.Decrypt(Encoding.Unicode.GetBytes(value), false).ToString();
                }
                catch (CryptographicException)
                {
                    return string.Empty;
                }
            }
        }
    }
}