using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace workout
{
    public class DpApi
    {
        [DllImport("crypt32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern bool CryptProtectData(ref DATA_BLOB pPlainText, string szDescription, ref DATA_BLOB pEntropy, IntPtr pReserved, ref CRYPTPROTECT_PROMPTSTRUCT pPrompt, int dwFlags, ref DATA_BLOB pCipherText);
        [DllImport("crypt32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern
        bool CryptUnprotectData(ref DATA_BLOB pCipherText, ref string pszDescription, ref DATA_BLOB pEntropy, IntPtr pReserved, ref CRYPTPROTECT_PROMPTSTRUCT pPrompt, int dwFlags, ref DATA_BLOB pPlainText);
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct DATA_BLOB
        {
            public int cbData;
            public IntPtr pbData;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CRYPTPROTECT_PROMPTSTRUCT
        {
            public int cbSize;
            public int dwPromptFlags;
            public IntPtr hwndApp;
            public string szPrompt;
        }
        public static byte[] Decrypt(byte[] cipherTextBytes, byte[] entropyBytes, out string description)
        {
            DATA_BLOB plainTextBlob = new DATA_BLOB();
            DATA_BLOB cipherTextBlob = new DATA_BLOB();
            DATA_BLOB entropyBlob = new DATA_BLOB();
            CRYPTPROTECT_PROMPTSTRUCT prompt = new CRYPTPROTECT_PROMPTSTRUCT();
            prompt.cbSize = Marshal.SizeOf(typeof(CRYPTPROTECT_PROMPTSTRUCT));
            prompt.dwPromptFlags = 0;
            prompt.hwndApp = IntPtr.Zero;
            prompt.szPrompt = null;
            description = String.Empty;
            try
            {
                try
                {
                    if (cipherTextBytes == null)
                        cipherTextBytes = new byte[0];
                    cipherTextBlob.pbData = Marshal.AllocHGlobal(cipherTextBytes.Length);
                    if (cipherTextBlob.pbData == IntPtr.Zero) throw new Exception(String.Empty);
                    cipherTextBlob.cbData = cipherTextBytes.Length;
                    Marshal.Copy(cipherTextBytes, 0, cipherTextBlob.pbData, cipherTextBytes.Length);
                }
                catch (Exception ex)
                {
                    throw new Exception(String.Empty, ex);
                }
                try
                {
                    if (entropyBytes == null)
                        entropyBytes = new byte[0];
                    entropyBlob.pbData = Marshal.AllocHGlobal(entropyBytes.Length);
                    if (entropyBlob.pbData == IntPtr.Zero) throw new Exception(String.Empty);
                    entropyBlob.cbData = entropyBytes.Length;
                    Marshal.Copy(entropyBytes, 0, entropyBlob.pbData, entropyBytes.Length);
                }
                catch (Exception ex)
                {
                    throw new Exception(String.Empty, ex);
                }
                int flags = 0x1;
                bool success = CryptUnprotectData(ref cipherTextBlob, ref description, ref entropyBlob, IntPtr.Zero, ref prompt, flags, ref plainTextBlob);
                if (!success)
                {
                    int errCode = Marshal.GetLastWin32Error();
                    throw new Exception(String.Empty, new Win32Exception(errCode));
                }
                byte[] plainTextBytes = new byte[plainTextBlob.cbData];
                Marshal.Copy(plainTextBlob.pbData, plainTextBytes, 0, plainTextBlob.cbData);
                return plainTextBytes;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Empty, ex);
            }
            finally
            {
                if (plainTextBlob.pbData != IntPtr.Zero)
                    Marshal.FreeHGlobal(plainTextBlob.pbData);
                if (cipherTextBlob.pbData != IntPtr.Zero)
                    Marshal.FreeHGlobal(cipherTextBlob.pbData);
                if (entropyBlob.pbData != IntPtr.Zero)
                    Marshal.FreeHGlobal(entropyBlob.pbData);
            }
        }
    }
}