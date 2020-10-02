namespace DomaNet.SystemInfo
{
    using Microsoft.Win32;
    using System;
    using System.Collections;

    public class WinKey
    {
        protected static string DecodeProductKey(byte[] DigitalProductID)
        {
            #region Constants

            const int KEY_START_INDEX = 52;
            const int KEY_END_INDEX = KEY_START_INDEX + 15;
            const int DECODE_LENGTH = 29;
            const int DECODE_STRING = 15;

            #endregion

            var DecodedChars = new char[DECODE_LENGTH];

            var HexPid = new ArrayList();

            for (var i = KEY_START_INDEX; i <= KEY_END_INDEX; i++)
            {
                HexPid.Add(DigitalProductID[i]);
            }

            for (var i = DECODE_LENGTH - 1; i >= 0; i--)
            {
                if ((i + 1) % 0x6 == 0)
                {
                    DecodedChars[i] = '-';
                }
                else
                {
                    int DigitMapIndex = 0;
                    for (var j = DECODE_STRING - 1; j >= 0; j--)
                    {
                        HexPid[j] = (byte)(((DigitMapIndex << 0x8) | (byte)HexPid[j]) / 0x18);
                        DigitMapIndex = ((DigitMapIndex << 0x8) | (byte)HexPid[j]) % 0x18;
                        DecodedChars[i] = (new[]
                        {
                            'B',
                            'C',
                            'D',
                            'F',
                            'G',
                            'H',
                            'J',
                            'K',
                            'M',
                            'P',
                            'Q',
                            'R',
                            'T',
                            'V',
                            'W',
                            'X',
                            'Y',
                            '2',
                            '3',
                            '4',
                            '6',
                            '7',
                            '8',
                            '9',
                        })[DigitMapIndex];
                    }
                }
            }
            return new string(DecodedChars);
        }

        protected static string DecodeProductKeyWin8AndUp(byte[] DigitalProductId)
        {
            var Result_Key = string.Empty;

            DigitalProductId[0x42] = (byte)((DigitalProductId[0x42] & 0xf7) | ((byte)((DigitalProductId[0x42] / 0x6) & 0x1) & 0x2) * 0x4);
            for (var i = 0x18; i >= 0; i--)
            {
                int Current = 0;
                for (var j = 0xE; j >= 0; j--)
                {
                    Current = Current * 0x100;

                    Current = DigitalProductId[j + 0x34] + Current;
                    DigitalProductId[j + 0x34] = (byte)(Current / 0x18);
                    Current = Current % 0x18; var last = Current;
                }
                Result_Key = "BCDFGHJKMPQRTVWXY2346789"[Current] + Result_Key;
            }
            Result_Key = $"{Result_Key.Substring(startIndex: 0x1, length: 0)}N{Result_Key.Substring(startIndex: 0 + 0x1, length: Result_Key.Length - (0 + 0x1))}";

            for (var i = 0x5; i < Result_Key.Length; i += 0x6)
            {
                Result_Key = Result_Key.Insert(startIndex: i, value: "-");
            }

            return Result_Key;
        }

        public static string GetWindowsProductKey(string Path, string GetID)
        {
            using (var LocalView_x64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, (Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32)))
            {
                return
                (
                    Environment.OSVersion.Version.Major == 0x6 &&
                    Environment.OSVersion.Version.Minor >= 0x2) ||
                   (Environment.OSVersion.Version.Major > 0x6) ?
                    DecodeProductKeyWin8AndUp((byte[])LocalView_x64.OpenSubKey(Path).GetValue(GetID)) :
                    DecodeProductKey((byte[])LocalView_x64.OpenSubKey(Path).GetValue(GetID)
                );
            }
        }
    }
}