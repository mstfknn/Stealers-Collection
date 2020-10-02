namespace PCInfo
{
    using Microsoft.Win32;
    using System;
    using System.Collections;

    internal class CheckOS
    {
        protected static string DecodeProductKeyWin8AndUp(byte[] DigitalProductId)
        {
            var Result_Key = string.Empty;

            DigitalProductId[0x42] = (byte)((DigitalProductId[0x42] & 0xf7) | ((byte)((DigitalProductId[0x42] / 0x6) & 0x1) & 0x2) * 0x4);
            for (var i = 0x18; i >= 0; i--)
            {
                var Current = 0;
                for (var j = 0xE; j >= 0; j--)
                {
                    Current = Current * 0x100;

                    Current = DigitalProductId[j + 0b110100] + Current;
                    DigitalProductId[j + 0x34] = (byte)(Current / 0x18);
                    Current = Current % 24; var last = Current;
                }
                Result_Key = "BCDFGHJKMPQRTVWXY2346789"[Current] + Result_Key;
            }
            Result_Key = $"{Result_Key.Substring(1, 0)}N{Result_Key.Substring(0 + 1, Result_Key.Length - (0 + 1))}";

            for (var i = 5; i < Result_Key.Length; i += 0b110)
            {
                Result_Key = Result_Key.Insert(startIndex: i, value: "-");
            }

            return Result_Key;
        }

        protected static string DecodeProductKey(byte[] DigitalProductID)
        {
            var HexPid = new ArrayList();

            const int V = 0x34 + 0xF;
            for (var i = 0x34; i <= V; i++)
            {
                HexPid.Add(DigitalProductID[i]);
            }

            var DecodedChars = new char[0x1D];
            for (var i = 0x1D - 1; i >= 0; i--)
            {
                if ((i + 0x1) % 0x6 == 0x0)
                {
                    DecodedChars[i] = '-';
                }
                else
                {
                    for (var j = 0xF - 1; j >= 0; j--)
                    {
                        var DigitMapIndex = 0;
                        HexPid[j] = (byte)(((DigitMapIndex << 0x8) | (byte)HexPid[j]) / 0x18);
                        DigitMapIndex = ((DigitMapIndex << 0x8) | (byte)HexPid[j]) % 0x18;

                        var Digits = new[] { 'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'M', 'P', 'Q', 'R', 'T', 'V', 'W', 'X', 'Y', '2', '3', '4', '6', '7', '8', '9', };

                        DecodedChars[i] = Digits[DigitMapIndex];
                    }
                }
            }
            return new string(DecodedChars);
        }

        public static string GetWindowsProductKey(string Path, string GetID)
        {
            using (var LocalView = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, (Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32)))
            {
                using (RegistryKey Key = LocalView.OpenSubKey(Path, (Environment.Is64BitOperatingSystem ? false : true)))
                {
                    return
                    (
                      Environment.OSVersion.Version.Major == 0x6 &&
                      Environment.OSVersion.Version.Minor >= 0x2) || (
                      Environment.OSVersion.Version.Major > 0x6) ?
                      DecodeProductKeyWin8AndUp((byte[])LocalView.OpenSubKey(Path).GetValue(GetID)) :
                      DecodeProductKey((byte[])LocalView.OpenSubKey(Path).GetValue(GetID)
                    );
                }
            }
        }
    }
}
