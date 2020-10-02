namespace DaddyRecovery.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public static class ByteHelper
    {
        public static byte[] ConvertHexStringToByteArray(string hexString)
        {
            if (hexString.Length % 2 == 0)
            {
                byte[] HexAsBytes = new byte[hexString.Length / 2];
                try
                {
                    for (int index = 0; index < HexAsBytes.Length; index++)
                    {
                        HexAsBytes[index] = byte.Parse(hexString.Substring(index * 2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                    }
                }
                catch { }

                return HexAsBytes;
            }
            throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", hexString));
        }

        public static string CalcHex(byte[] input)
        {
            string text = string.Empty;
            try
            {
                foreach (char c in BitConverter.ToString(input).Replace("-", string.Empty))
                {
                    if (!char.IsLetter(c)) text += c.ToString();
                    else text += char.ToLower(c).ToString();
                }
            }
            catch { }
            return text;
        }

        public static string CalcHex(string input)
        {
            string result = string.Empty;
            if (!string.IsNullOrWhiteSpace(input))
            {
                try
                {
                    result = CalcHex(Encoding.Default.GetBytes(input));
                }
                catch { }
            }
            return result;
        }
    }
}