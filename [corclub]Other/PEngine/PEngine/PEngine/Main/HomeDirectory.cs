namespace PEngine.Main
{
    using System;
    using System.IO;
    using System.Security;

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
                catch (SecurityException) { }
            }
            else
            {
                DirHome.Attributes |= FileAttributes.Normal;
                foreach (FileInfo FileHome in DirHome.GetFiles())
                {
                    try
                    {
                        FileHome?.Delete();
                    }
                    catch (IOException) { }
                    catch (SecurityException) { }
                    catch (UnauthorizedAccessException) { }
                }

                foreach (DirectoryInfo SubHome in DirHome.GetDirectories())
                {
                    try
                    {
                        SubHome?.Delete(Recursive);
                    }
                    catch (IOException) { }
                    catch (SecurityException) { }
                    catch (UnauthorizedAccessException) { }
                }
                try
                {
                    DirHome.Attributes |= FileAttributes.Hidden;
                }
                catch (IOException) { }
                catch (SecurityException) { }
            }
        }
    }
}