using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace loki.loki.Utilies.CryptoGrafy
{
    class crypt
    {
        public static string password_aes = "EAAAALZtWlYn5RSRzzQv25kWmX6INGcLlC5iBzugw0VI7IKL + 7wOaADOJ/daOYUHJx8wkw==";
        public static string password = "goisjgpoerkjgokkbjiushgporwagmwibuts0gp[mvkntiusopjfij";
        private static byte[] _salt = Encoding.ASCII.GetBytes("4326443888886662222");
        public static string loki_decrypt()
        {

            if (string.IsNullOrEmpty(password_aes))
                throw new ArgumentNullException("sifreliMetin");
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("sifre");

            RijndaelManaged aes = null;
            string cozulmusMetin = null;

            try
            {

                var key = new Rfc2898DeriveBytes(password, _salt);

                byte[] bytes = Convert.FromBase64String(password_aes);
                using (var msDecrypt = new MemoryStream(bytes))
                {

                    aes = new RijndaelManaged();
                    aes.Key = key.GetBytes(aes.KeySize / 8);
                    aes.IV = ReadByteArray(msDecrypt);

                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))

                            cozulmusMetin = srDecrypt.ReadToEnd();
                    }
                }
            }
            finally
            {
                if (aes != null)
                    aes.Clear();
            }
            return cozulmusMetin;
        }
        public static string AESDecript(string stringa)
        {
            if (string.IsNullOrEmpty(stringa))
                throw new ArgumentNullException("sifreliMetin");
            if (string.IsNullOrEmpty(loki_decrypt()))
                throw new ArgumentNullException("sifre");

            RijndaelManaged aes = null;
            string cozulmusMetin = null;

            try
            {

                var key = new Rfc2898DeriveBytes(loki_decrypt(), _salt);

                byte[] bytes = Convert.FromBase64String(stringa);
                using (var msDecrypt = new MemoryStream(bytes))
                {

                    aes = new RijndaelManaged();
                    aes.Key = key.GetBytes(aes.KeySize / 8);
                    aes.IV = ReadByteArray(msDecrypt);

                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))

                            cozulmusMetin = srDecrypt.ReadToEnd();
                    }
                }
            }
            finally
            {
                if (aes != null)
                    aes.Clear();
            }
            return cozulmusMetin;
        }
        private static byte[] ReadByteArray(MemoryStream ms)
        {
            byte[] rawLength = new byte[sizeof(int)];
            if (ms.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
            {
                throw new SystemException("Stream did not contain properly formatted byte array");
            }

            byte[] buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
            if (ms.Read(buffer, 0, buffer.Length) != buffer.Length)
            {
                throw new SystemException("Did not read byte array properly");
            }

            return buffer;
        }
    }
}