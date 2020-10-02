using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace xoxoxo.Chromium
{
    public static class ChromiumDecryptor
    {
        private const int CryptprotectUiForbidden = 1;
        private static readonly IntPtr NullPtr = (IntPtr) 0;

        [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool CryptUnprotectData(ref DataBlob pCipherText, ref string pszDescription,
            ref DataBlob pEntropy, IntPtr pReserved, ref CryptprotectPromptstruct pPrompt, int dwFlags,
            ref DataBlob pPlainText);

        private static void InitPrompt(ref CryptprotectPromptstruct ps)
        {
            ps.cbSize = Marshal.SizeOf(typeof(CryptprotectPromptstruct));
            ps.dwPromptFlags = 0;
            ps.hwndApp = NullPtr;
            ps.szPrompt = null;
        }

        private static void InitBlob(byte[] data, ref DataBlob blob)
        {
            if (data == null)
                data = new byte[0];
            blob.pbData = Marshal.AllocHGlobal(data.Length);
            if (blob.pbData == IntPtr.Zero)
                throw new Exception("Unable to allocate data buffer for BLOB structure.");
            blob.cbData = data.Length;
            Marshal.Copy(data, 0, blob.pbData, data.Length);
        }

        public static string Decrypt(string cipherText)
        {
            string description;
            return Decrypt(cipherText, string.Empty, out description);
        }

        private static string Decrypt(string cipherText, string entropy, out string description)
        {
            if (entropy == null)
                entropy = string.Empty;
            return Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(cipherText),
                Encoding.UTF8.GetBytes(entropy), out description));
        }

        private static byte[] Decrypt(byte[] cipherTextBytes, byte[] entropyBytes, out string description)
        {
            var pPlainText = new DataBlob();
            var dataBlob1 = new DataBlob();
            var dataBlob2 = new DataBlob();
            var cryptprotectPromptstruct = new CryptprotectPromptstruct();
            InitPrompt(ref cryptprotectPromptstruct);
            description = string.Empty;
            try
            {
                try
                {
                    InitBlob(cipherTextBytes, ref dataBlob1);
                }
                catch (Exception ex)
                {
                    throw new Exception("Cannot initialize ciphertext BLOB.", ex);
                }

                try
                {
                    InitBlob(entropyBytes, ref dataBlob2);
                }
                catch (Exception ex)
                {
                    throw new Exception("Cannot initialize entropy BLOB.", ex);
                }

                if (!CryptUnprotectData(ref dataBlob1, ref description, ref dataBlob2, IntPtr.Zero,
                    ref cryptprotectPromptstruct, 1, ref pPlainText))
                    throw new Exception("CryptUnprotectData failed.", new Win32Exception(Marshal.GetLastWin32Error()));
                var destination = new byte[pPlainText.cbData];
                Marshal.Copy(pPlainText.pbData, destination, 0, pPlainText.cbData);
                return destination;
            }
            catch (Exception ex)
            {
                throw new Exception("Dpapi was unable to decrypt data.", ex);
            }
            finally
            {
                if (pPlainText.pbData != IntPtr.Zero)
                    Marshal.FreeHGlobal(pPlainText.pbData);
                if (dataBlob1.pbData != IntPtr.Zero)
                    Marshal.FreeHGlobal(dataBlob1.pbData);
                if (dataBlob2.pbData != IntPtr.Zero)
                    Marshal.FreeHGlobal(dataBlob2.pbData);
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct DataBlob
        {
            public int cbData;
            public IntPtr pbData;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct CryptprotectPromptstruct
        {
            public int cbSize;
            public int dwPromptFlags;
            public IntPtr hwndApp;
            public string szPrompt;
        }
    }
}