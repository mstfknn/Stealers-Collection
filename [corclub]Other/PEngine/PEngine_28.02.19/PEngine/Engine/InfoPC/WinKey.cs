namespace PEngine.Engine.InfoPC
{
    using Microsoft.Win32;
    using PEngine.Helpers;
    using System;
    using System.Collections;
    using System.Security;

    internal class WinKey
    {
        public WinKey() { }
    
        protected static string DecodeProductKey(byte[] DigitalProductID)
        {
            int DigitMapIndex = 0;

            var HexPid = new ArrayList();
            for (int i = (int)Enums.KeyIndexWin.KEY_START_INDEX; i <= (int)Enums.KeyIndexWin.KEY_END_INDEX; i++)
            {
                HexPid.Add(DigitalProductID[i]);
            }
            char[] DecodedChars = new char[(int)Enums.KeyIndexWin.DECODE_LENGTH];
            for (int i = (int)Enums.KeyIndexWin.DECODE_LENGTH - 1; i >= 0; i--)
            {
                if ((i + 1) % 0x6 == 0)
                {
                    DecodedChars[i] = '-';
                }
                else
                {
                    for (int j = (int)Enums.KeyIndexWin.DECODE_STRING - 1; j >= 0; j--)
                    {
                        HexPid[j] = (byte)(((DigitMapIndex << 0b1000) | (byte)HexPid[j]) / 0x18);
                        DigitMapIndex = ((DigitMapIndex << 0b1000) | (byte)HexPid[j]) % 0x18;

                        char[] Digits = new[] { 'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'M', 'P', 'Q', 'R', 'T', 'V', 'W', 'X', 'Y', '2', '3', '4', '6', '7', '8', '9', };

                        DecodedChars[i] = Digits[DigitMapIndex];
                    }
                }
            }
            return new string(DecodedChars);
        }
        protected static string DecodeProductKeyWin8AndUp(byte[] DigitalProductId)
        {
            string Result_Key = string.Empty;

            DigitalProductId[0x42] = (byte)((DigitalProductId[0x42] & 0xf7) | (((byte)((DigitalProductId[0x42] / 0x6) & 0x1) & 0x2) * 0x4));
            for (int i = 0x18; i >= 0; i--)
            {
                int Current = 0;
                for (int j = 0xE; j >= 0; j--)
                {
                    Current = Current * 0x100;

                    Current = DigitalProductId[j + 0x34] + Current;
                    DigitalProductId[j + 0x34] = (byte)(Current / 0x18);
                    Current = Current % 24;
                    int last = Current;
                }
                Result_Key = "BCDFGHJKMPQRTVWXY2346789"[Current] + Result_Key;
            }
            Result_Key = $"{Result_Key.Substring(1, 0)}N{Result_Key.Substring(0 + 1, Result_Key.Length - (0 + 1))}";

            for (int i = 5; i < Result_Key.Length; i += 0x6)
            {
                Result_Key = Result_Key.Insert(i,"-");
            }

            return Result_Key;
        }

        public static string GetWindowsProductKey(string Path, string GetID)
        {
            try
            {
                using (var hklmHive = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32))
                {
                    try
                    {
                        using (RegistryKey Key = hklmHive.OpenSubKey(Path, Environment.Is64BitOperatingSystem ? true : false))
                        {
                            return (Environment.OSVersion.Version.Major == 0x6 && Environment.OSVersion.Version.Minor >= 0x2) || (Environment.OSVersion.Version.Major > 0x6) ?
                            DecodeProductKeyWin8AndUp((byte[])Key.GetValue(GetID)) : null;
                        }
                    }
                    catch { return null; }
                }
            }
            catch (SecurityException) { return null; }
        }
    }
}