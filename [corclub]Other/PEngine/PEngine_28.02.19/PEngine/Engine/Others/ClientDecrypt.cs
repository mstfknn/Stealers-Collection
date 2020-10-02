namespace PEngine.Engine.Others
{
    using System;
    using System.Globalization;
    using System.Text;

    public class ClientDecrypt
    {
        public static string DecryptDynDns(string encrypted)
        {
            string decoded = string.Empty;
            for (int i = 0; i < encrypted.Length; i += 2)
            {
                decoded += (char)int.Parse(encrypted.Substring(i, 2), NumberStyles.HexNumber);
            }
            char[] outcome = decoded.ToCharArray(), chars = new char[decoded.Length];
            int lPtr = 0;
            for (int i = 0; i < chars.Length; i++)
            {
                try
                {
                    int second = Convert.ToChar("t6KzXhCh".Substring(lPtr, 1));
                    chars[i] = (char)(outcome[i] ^ second);
                    lPtr = (lPtr + 1) % 8;
                }
                catch (FormatException) { }
                catch (ArgumentOutOfRangeException) { }
            }
            return new string(chars);
        }

        public static string DecryptFZ(string Text) => 
            Encoding.UTF8.GetString(Convert.FromBase64String(Text));

    }
}