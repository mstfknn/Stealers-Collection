using System.Security.Cryptography;
using System.Text;

namespace Loki.Gecko
{
    public static class TripleDESHelper
    {
        public static string DESCBCDecryptor(byte[] key, byte[] iv, byte[] input, PaddingMode paddingMode = PaddingMode.None)
        {
            using (var tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider())
            {
                tripleDESCryptoServiceProvider.Key = key;
                tripleDESCryptoServiceProvider.IV = iv;
                tripleDESCryptoServiceProvider.Mode = CipherMode.CBC;
                tripleDESCryptoServiceProvider.Padding = paddingMode;
                using (ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateDecryptor(key, iv))
                {
                    return Encoding.Default.GetString(cryptoTransform.TransformFinalBlock(input, 0, input.Length));
                }
            }
        }
    }
}
