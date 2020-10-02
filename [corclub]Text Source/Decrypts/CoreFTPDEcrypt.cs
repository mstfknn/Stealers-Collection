using System;
using System.Security.Cryptography;
using System.Text;

internal class CoreFTPDEcrypt
{
    private static string DecryptCoreFTPPassword(string HexString)
    {
        StringBuilder buffer = new StringBuilder(HexString.Length * 3 / 2);
        for (int i = 0; i < HexString.Length; i++)
        {
            if ((i > 0) & (i % 2 == 0))
                buffer.Append("-");
            buffer.Append(HexString[i]);
        }

        string Reversed = buffer.ToString();

        int length = (Reversed.Length + 1) / 3;
        byte[] arr = new byte[length];
        for (int i = 0; i < length; i++)
        {
            arr[i] = Convert.ToByte(Reversed.Substring(3 * i, 2), 16);
        }
        RijndaelManaged AES = new RijndaelManaged()
        {
            Mode = CipherMode.ECB,
            Key = Encoding.ASCII.GetBytes("hdfzpysvpzimorhk"),
            Padding = PaddingMode.Zeros,
        };
        ICryptoTransform Transform = AES.CreateDecryptor(AES.Key, AES.IV);
        return Encoding.UTF8.GetString(Transform.TransformFinalBlock(arr, 0, arr.Length));
    }
}