using System;
using System.Runtime.InteropServices;

internal class DPAPI
{
    [DllImport("crypt32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern bool CryptUnprotectData(ref DATA_BLOB pCipherText, ref string pszDescription, ref DATA_BLOB pEntropy, IntPtr pReserved, ref CRYPTPROTECT_PROMPTSTRUCT pPrompt, int dwFlags, ref DATA_BLOB pPlainText);

    [DllImport("crypt32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern bool CryptProtectData(ref DATA_BLOB pPlainText, ref string pszDescription, ref DATA_BLOB pEntropy, IntPtr pReserved, ref CRYPTPROTECT_PROMPTSTRUCT pPrompt, int dwFlags, ref DATA_BLOB pCipherText);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct CRYPTPROTECT_PROMPTSTRUCT
    {
        public int cbSize;
        public int dwPromptFlags;
        public IntPtr hwndApp;
        public string szPrompt;
    }

    public static byte[] decrypt(byte[] cipherTextBytes)
    {
        try
        {
            DATA_BLOB plainTextBlob = new DATA_BLOB();
            DATA_BLOB cipherTextBlob = new DATA_BLOB();
            DATA_BLOB entropyBlob = new DATA_BLOB();
            CRYPTPROTECT_PROMPTSTRUCT prompt = new CRYPTPROTECT_PROMPTSTRUCT();
            string description = String.Empty;
            InitPrompt(ref prompt);
            try
            {
                InitBLOB(cipherTextBytes, ref cipherTextBlob);
            }
            catch { }
            int flags = 0x1;
            bool success = CryptUnprotectData(ref cipherTextBlob,
                                              ref description,
                                              ref entropyBlob,
                                                  IntPtr.Zero,
                                              ref prompt,
                                                  flags,
                                              ref plainTextBlob);
            if (success)
            {
                byte[] plainTextBytes = new byte[plainTextBlob.cbData];
                Marshal.Copy(plainTextBlob.pbData,
                         plainTextBytes,
                         0,
                         plainTextBlob.cbData);
                return plainTextBytes;
            }
        }
        catch { }
        return null;
    }

    public static byte[] encrypt(byte[] plainTextBytes)
    {
        try
        {
            DATA_BLOB plainTextBlob = new DATA_BLOB();
            DATA_BLOB cipherTextBlob = new DATA_BLOB();
            DATA_BLOB entropyBlob = new DATA_BLOB();
            CRYPTPROTECT_PROMPTSTRUCT prompt = new CRYPTPROTECT_PROMPTSTRUCT();
            string description = String.Empty;
            InitPrompt(ref prompt);
            try
            {
                InitBLOB(plainTextBytes, ref plainTextBlob);
            }
            catch { }
            int flags = 0x1;
            bool success = CryptProtectData(ref plainTextBlob,
                                              ref description,
                                              ref entropyBlob,
                                                  IntPtr.Zero,
                                              ref prompt,
                                                  flags,
                                              ref cipherTextBlob);
            if (success)
            {
                byte[] cipherTextBytes = new byte[cipherTextBlob.cbData];
                Marshal.Copy(cipherTextBlob.pbData,
                         cipherTextBytes,
                         0,
                         cipherTextBlob.cbData);
                return cipherTextBytes;
            }
        }
        catch { }
        return null;
    }

    private static void InitPrompt(ref CRYPTPROTECT_PROMPTSTRUCT ps)
    {
        ps.cbSize = Marshal.SizeOf(
                                  typeof(CRYPTPROTECT_PROMPTSTRUCT));
        ps.dwPromptFlags = 0;
        ps.hwndApp = ((IntPtr)((int)(0)));
        ps.szPrompt = null;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct DATA_BLOB
    {
        public int cbData;
        public IntPtr pbData;
    }

    private static void InitBLOB(byte[] data, ref DATA_BLOB blob)
    {
        blob.pbData = Marshal.AllocHGlobal(data.Length);
        if (blob.pbData == IntPtr.Zero)
            throw new Exception("Unable to allocate buffer for BLOB data.");
        blob.cbData = data.Length;
        Marshal.Copy(data, 0, blob.pbData, data.Length);
    }
}