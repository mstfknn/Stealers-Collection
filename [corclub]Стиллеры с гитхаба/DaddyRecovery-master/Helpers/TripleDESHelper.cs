namespace DaddyRecovery.Helpers
{
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public static class TripleDESHelper
    {
        public static string DESCBCDecryptor(byte[] key, byte[] iv, byte[] input, PaddingMode paddingMode = PaddingMode.None)
        {
            string result = string.Empty;
            try
            {
                using (var tripleDES = new TripleDESCryptoServiceProvider())
                {
                    tripleDES.Key = key;
                    tripleDES.IV = iv;
                    tripleDES.Mode = CipherMode.CBC;
                    tripleDES.Padding = paddingMode;
                    using (ICryptoTransform cryptoTransform = tripleDES.CreateDecryptor(key, iv))
                    {
                        result = Encoding.Default.GetString(cryptoTransform.TransformFinalBlock(input, 0, input.Length));
                    }
                }
            }
            catch { }
            return result;
        }

        public static byte[] DESCBCDecryptorByte(byte[] key, byte[] iv, byte[] input)
        {
            byte[] decrypted = new byte[512];
            try
            {
                using (var tdsAlg = new TripleDESCryptoServiceProvider())
                {
                    tdsAlg.Key = key;
                    tdsAlg.IV = iv;
                    tdsAlg.Mode = CipherMode.CBC;
                    tdsAlg.Padding = PaddingMode.None;
                    using (ICryptoTransform decryptor = tdsAlg.CreateDecryptor(tdsAlg.Key, tdsAlg.IV))
                    {
                        using (var msDecrypt = new MemoryStream(input))
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            csDecrypt.Read(decrypted, 0, decrypted.Length);
                        }
                    }
                }
            }
            catch { }
            return decrypted;
        }
    }
}