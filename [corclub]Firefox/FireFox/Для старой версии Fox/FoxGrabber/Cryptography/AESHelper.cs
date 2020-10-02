namespace FoxGrabber.Cryptography
{
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public class AESHelper
    {
        private const string secretKey = "N@ncyl€|";

        public AESHelper() { }

        public static byte[] AESEncryptor(string value) => AESEncryptorMain(Encoding.Unicode.GetBytes(value));

        public static string AESDecryptor(byte[] value) => Encoding.Unicode.GetString(AESDecryptorMain(value));

        private static byte[] AESEncryptorMain(byte[] value)
        {
            try
            {
                var keyGenerator = new Rfc2898DeriveBytes(secretKey, 8);
                var aes = Rijndael.Create();
                aes.IV = keyGenerator.GetBytes(aes.BlockSize / 8);
                aes.Key = keyGenerator.GetBytes(aes.KeySize / 8);

                using (var memoryStream = new MemoryStream())
                using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    memoryStream.Write(keyGenerator.Salt, 0, keyGenerator.Salt.Length);
                    cryptoStream.Write(value, 0, value.Length);
                    return memoryStream.ToArray();
                }
            }
            catch (CryptographicException)
            {
                byte[] salt = new byte[1];
                for (int i = 0; i < salt.Length; i++)
                {
                    salt[i] = 0;
                }
                return salt;
            }
        }

        private static byte[] AESDecryptorMain(byte[] value)
        {
            try
            {
                byte[] salt = new byte[8];
                for (int i = 0; i < salt.Length; i++)
                {
                    salt[i] = value[i];
                }
                var keyGenerator = new Rfc2898DeriveBytes(secretKey, salt);
                var aes = Rijndael.Create();
                aes.IV = keyGenerator.GetBytes(aes.BlockSize / 8);
                aes.Key = keyGenerator.GetBytes(aes.KeySize / 8);

                using (var memoryStream = new MemoryStream())
                using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(value, 8, value.Length - 8);
                    return memoryStream.ToArray();
                }
            }
            catch (CryptographicException)
            {
                byte[] salt = new byte[1];
                for (int i = 0; i < salt.Length; i++)
                {
                    salt[i] = 0;
                }
                return salt;
            }
        }

    }
}