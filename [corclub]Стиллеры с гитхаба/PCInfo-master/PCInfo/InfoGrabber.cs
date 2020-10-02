namespace PCInfo
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Management;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    public class InfoGrabber
    {
        private static readonly List<string> InfoWrite = new List<string>();

        private static readonly string[] Links = new string[]
        {
          "https://api.ipify.org/","http://icanhazip.com/",
          "https://ipinfo.io/ip", "http://checkip.amazonaws.com/"
        };

        public static void CreateTable(string HomePath)
        {
            InfoWrite.Add("<!DOCTYPE html>");
            InfoWrite.Add("<html>");
            InfoWrite.Add("<head>");
            InfoWrite.Add("<title>SystemInfo</title>");
            InfoWrite.Add("<link rel=\"icon\" type=\"image/png\" href=\"http://s1.iconbird.com/ico/0612/customicondesignoffice2/w48h481339870371Personalinformation48.png\" sizes=\"32x32\">");
            InfoWrite.Add("</head>");
            InfoWrite.Add("<body style=\"width:100%; height:100%; margin: 0; background: url(http://radiographer.co.il/wp-content/uploads/2015/11/backgroundMain.jpg) #191919\">");
            InfoWrite.Add("<center><h1 style=\"color:#85AB70; margin:50px 0\">Системная Информация о Компьютере</h1></center>");
            InfoWrite.Add($"<center><span style=\"color:#6294B3\">Операционная система: </span><span style=\"color:#BA9201\">{OSInfo.osVersion} [ { OSInfo.OSName} ] </span></center>");
            InfoWrite.Add($"<center><span style=\"color:#6294B3\">Имя Компьютера: </span><span style=\"color:#BA9201\">{OSInfo.MachineName}</span></center>");
            InfoWrite.Add($"<center><span style=\"color:#6294B3\">Имя Пользователя: </span><span style=\"color:#BA9201\">{OSInfo.UserName}</span></center>");
            InfoWrite.Add($"<center><span style=\"color:#6294B3\">Логических Процессоров: </span><span style=\"color:#BA9201\">{OSInfo.ProcessorCount}</span></center>");
            InfoWrite.Add($"<center><span style=\"color:#6294B3\">Системная Директория: </span><span style=\"color:#BA9201\">{OSInfo.SystemDir}</span></center>");
            InfoWrite.Add($"<center><span style=\"color:#6294B3\">Разрядность системы: </span><span style=\"color:#BA9201\">{OSInfo.OSBit}</span></center>");

            try
            {
                using (ManagementObjectCollection Secret = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_Processor").Get())
                {
                    foreach (ManagementBaseObject cr in Secret)
                    {
                        var Text = (cr["Name"]?.ToString());
                        InfoWrite.Add($"<center><span style=\"color:#6294B3\">Процессор: </span><span style=\"color:#BA9201\">{Text} [ <a title=\"Производитель: {Text}\"</span>{Text} ]</a title></center>");
                        break;
                    }
                }
                using (ManagementObjectCollection MonitorEx = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_DesktopMonitor").Get())
                {
                    foreach (ManagementBaseObject queryObj in MonitorEx)
                    {
                        if (queryObj["ScreenWidth"] == null || queryObj["ScreenHeight"] == null)
                        {
                            continue;
                        }
                        var DesktopMonitorX = (queryObj["ScreenWidth"]?.ToString());
                        var DesktopMonitorY = (queryObj["ScreenHeight"]?.ToString());
                        InfoWrite.Add($"<center><span style=\"color:#6294B3\">Разрешение экрана: </span><span style=\"color:#BA9201\">{DesktopMonitorX} x {DesktopMonitorY} Пикселей</span></center>");
                        break;
                    }
                }
                using (ManagementObjectCollection OS = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_OperatingSystem").Get())
                {
                    foreach (ManagementBaseObject z in OS)
                    {
                        InfoWrite.Add($"<center><span style=\"color:#6294B3\">Зарегистрированный пользователь: </span><span style=\"color:#BA9201\">{z["RegisteredUser"]?.ToString()}</span></center>");
                        InfoWrite.Add($"<center><span style=\"color:#6294B3\">Серийный ключ Windows: </span><span style=\"color:#BA9201\">{CheckOS.GetWindowsProductKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "DigitalProductId")}</span></center>");
                        InfoWrite.Add($"<center><span style=\"color:#6294B3\">Код продукта Windows: </span><span style=\"color:#BA9201\">{z["SerialNumber"]?.ToString()}</span></center>");
                        break;
                    }
                }
                using (ManagementObjectCollection Bios = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_BIOS").Get())
                {
                    foreach (ManagementBaseObject obj in Bios)
                    {
                        try
                        {
                            var BiosVersion = (((string[])obj["BIOSVersion"])[0]);
                            var BiosVersionTwo = (((string[])obj["BIOSVersion"])[1]);
                            var Manufacturer = obj["Manufacturer"]?.ToString();

                            if (((string[])obj["BIOSVersion"]).Length > 1)
                            {
                                InfoWrite.Add($"<center><span style=\"color:#6294B3\">Версия Биос: <span style=\"color:#BA9201\">{BiosVersion} - {BiosVersionTwo} [ <a title=\"Производитель: {Manufacturer}\">{Manufacturer}</a title> ]</span><span style=\"color:#BA9201\"></span></center>");
                            }
                            else
                            {
                                InfoWrite.Add($"<center><span style=\"color:#6294B3\">Версия Биос: <span style=\"color:#BA9201\">{BiosVersion} [ <a title=\"Производитель: {0x2}\">{0x2}</a title> ]</span></center>");
                            }
                        }
                        catch { break; }
                    }
                }
                using (ManagementObjectCollection wmiData = new ManagementObjectSearcher(@"root\SecurityCenter2", "SELECT * FROM AntiVirusProduct").Get())
                {
                    foreach (ManagementBaseObject item in wmiData)
                    {
                        InfoWrite.Add($"<center><span style=\"color:#6294B3\">Установленный антивирус: </span><span style=\"color:#BA9201\">{item["displayName"]?.ToString()}</span></center>");
                    }
                }
                using (ManagementObjectCollection FireWall = new ManagementObjectSearcher(@"root\SecurityCenter2", "SELECT * FROM FirewallProduct").Get())
                {
                    foreach (ManagementBaseObject item in FireWall)
                    {
                        InfoWrite.Add($"<center><span style=\"color:#6294B3\">Установленный Межсетевой экран (FireWall): </span><span style=\"color:#BA9201\">{item["displayName"]?.ToString()}</span></center>");
                    }
                }
                using (ManagementObjectCollection TotalMemory = new ManagementObjectSearcher(@"root\CIMV2", "SELECT TotalPhysicalMemory FROM Win32_ComputerSystem").Get())
                {
                    foreach (ManagementBaseObject Total in TotalMemory)
                    {
                        try
                        {

                            var ConverterMB = (Convert.ToString(Math.Round((Convert.ToDouble(Total["TotalPhysicalMemory"])) / 0x4000_0000 * 0x3E8, 0x2)));
                            var ConverterGB = (Convert.ToString(Math.Round((Convert.ToDouble(Total["TotalPhysicalMemory"])) / 0x4000_0000, 0x2)));
                            if (Convert.ToDouble(Total["TotalPhysicalMemory"]) / 0x4000_0000 > 0x1)
                            {
                                InfoWrite.Add($"<center><span style=\"color:#6294B3\">Физической памяти: </span><span style=\"color:#BA9201\">({ConverterMB} MB)</span></center>");

                            }
                            else
                            {
                                InfoWrite.Add($"<center><span style=\"color:#6294B3\">Физической памяти: </span><span style=\"color:#BA9201\">({ConverterGB} GB)</span></center>");
                            }
                        }
                        catch { break; }
                    }
                }
                using (ManagementObjectCollection Type = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_PhysicalMemory").Get())
                {
                    foreach (ManagementBaseObject ObjType in Type)
                    {
                        var MemoryType = GetMemoryType(int.Parse(ObjType["MemoryType"].ToString()));
                        var ConverterMB = (Convert.ToString(Math.Round((Convert.ToDouble(ObjType["Capacity"])) / 0x4000_0000 * 0x3E8, 0x2)));
                        var ConverterGB = (Convert.ToString(Math.Round((Convert.ToDouble(ObjType["Capacity"])) / 0x4000_0000, 0x2)));
                        if (Convert.ToDouble(ObjType["Capacity"]) / 0x4000_0000 > 0x1)
                        {
                            InfoWrite.Add($"<center><span style=\"color:#6294B3\">Тип памяти: </span><span style=\"color:#BA9201\">{MemoryType} ( <a title=\"Объём памяти: {ConverterGB} GB (Производитель: {ObjType["Manufacturer"]})\"</span>{ConverterGB} GB )</a title></span></center>");
                        }
                        else
                        {
                            InfoWrite.Add($"<center><span style=\"color:#6294B3\">Тип памяти: </span><span style=\"color:#BA9201\">{MemoryType} ( <a title=\"Объём памяти: {ConverterMB} MB (Производитель: {ObjType["Manufacturer"]})\"</span>{ConverterMB} MB )</a title></span></center>");
                        }
                    }
                }
                using (ManagementObjectCollection VC = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_VideoController").Get())
                {
                    foreach (ManagementBaseObject w in VC)
                    {
                        string container = null;
                        try
                        {
                            switch ((Convert.ToDouble(w["AdapterRam"]) / 0x10_0000) % 0x400)
                            {
                                case 0:
                                    container = string.Concat(Convert.ToDouble(w.Properties["AdapterRam"].Value) / 0x10_0000 / 0x400, " GB");
                                    break;
                                default:
                                    container = string.Concat((Convert.ToDouble(w.Properties["AdapterRam"].Value) / 0x10_0000).ToString("F2"), " MB");
                                    break;
                            }
                        }
                        catch { }

                        InfoWrite.Add($"<center><span style=\"color:#6294B3\">Видеокарта: </span><span style=\"color:#BA9201\">{w["Caption"]?.ToString()}  -  <a title=\"Память видеокарты: {container}\"</span>({container})</a title></tr></span></center>");
                    }
                }
                using (ManagementObjectCollection SysPC = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_ComputerSystem").Get())
                {
                    foreach (ManagementBaseObject gob in SysPC)
                    {
                        var Mode = $"<center><span style=\"color:#6294B3\">Модель компьютера: </span><span style=\"color:#BA9201\">{gob["Manufacturer"]?.ToString()}{gob["Model"]?.ToString()}</span></center>";
                        InfoWrite.Add(System.Text.RegularExpressions.Regex.Replace(Mode, @"\s{2,}", " "));
                        InfoWrite.Add($"<center><span style=\"color:#6294B3\">Изготовитель модели компьютера: </span><span style=\"color:#BA9201\">{gob["Manufacturer"]?.ToString()}</span></center>");
                    }
                }
                foreach (IPAddress ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        InfoWrite.Add($"<center><span style=\"color:#6294B3\">Локальный IP: </span><span style=\"color:#BA9201\">{ip?.ToString()}</span></center>");
                    }
                }
                foreach (var v in Links)
                {
                    if (NetControl.CheckURL(v))
                    {
                        InfoWrite.Add($"<center><span style=\"color:#6294B3\">Внешний IP: </span><span style=\"color:#BA9201\">{NetControl.GetPublicIP(v?.ToString())}</span></center>");
                        break;
                    }
                }
            }
            catch { }

            InfoWrite.Add($"<center><span style=\"color:#6294B3\"><details><summary>Показать</span><span style=\"color:#BA9201\">Скрыть Запущенные процессы пользователя</summary><pre>{GetProcessPC(new StringBuilder())}</span></center>");
            InfoWrite.Add("</table>");
            InfoWrite.Add("</html>");
            try
            {
                File.WriteAllLines(HomePath, InfoWrite);
            }
            catch (UnauthorizedAccessException) { }
            catch (IOException) { }
        }
        private static string GetProcessPC(StringBuilder Proclist)
        {
            try
            {
                Proclist.AppendLine($"<center><span style=\"color:#6294B3\">Количество запущенных процессов:</span><span style=\"color:#BA9201\">[{Process.GetProcesses().Length}]</span></center>");
                Array.Sort(Process.GetProcesses(), (p1, p2) => p1.ProcessName.CompareTo(p2.ProcessName));
                foreach (Process item in Process.GetProcesses())
                {
                    if (Process.GetCurrentProcess().Id == item.Id || item.Id == 0)
                    {
                        continue;
                    }
                    Proclist.AppendLine($"<span style=\"color:#6294B3\"><a title=\"Имя процесса: {item.ProcessName}.exe\"</a title>{item.ProcessName}.exe</span><a title=\"ID процесса: {item.Id}\"</a title><span style=\"color:#BA9201\">[{item.Id}]</span>");
                }
            }
            catch (InvalidOperationException) { }
            catch (Exception) { }

            return Proclist.ToString();
        }
        private static string GetMemoryType(int MemoryType)
        {
            switch (MemoryType)
            {
                case 0x0:
                    {
                        return "DDR-4";
                    }
                case 0x1:
                    {
                        return "Other";
                    }
                case 0x2:
                    {
                        return "DRAM";
                    }
                case 0x3:
                    {
                        return "Synchronous DRAM";
                    }
                case 0x4:
                    {
                        return "Cache DRAM";
                    }
                case 0x5:
                    {
                        return "EDO";
                    }
                case 0x6:
                    {
                        return "EDRAM";
                    }
                case 0x7:
                    {
                        return "VRAM";
                    }
                case 0x8:
                    {
                        return "SRAM";
                    }
                case 0x9:
                    {
                        return "RAM";
                    }
                case 0xA:
                    {
                        return "ROM";
                    }
                case 0xB:
                    {
                        return "Flash";
                    }
                case 0xC:
                    {
                        return "EEPROM";
                    }
                case 0xD:
                    {
                        return "FEPROM";
                    }
                case 0xE:
                    {
                        return "EPROM";
                    }
                case 0xF:
                    {
                        return "CDRAM";
                    }
                case 0x10:
                    {
                        return "3DRAM";
                    }
                case 0x11:
                    {
                        return "SDRAM";
                    }
                case 0x12:
                    {
                        return "SGRAM";
                    }
                case 0x13:
                    {
                        return "RDRAM";
                    }
                case 0x14:
                    {
                        return "DDR";
                    }
                case 0x15:
                    {
                        return "DDR-2";
                    }
                default:
                    if (MemoryType == 0x1 || MemoryType > 0x16)
                    {
                        return "DDR-3";
                    }
                    else if (MemoryType == 0x19)
                    {
                        return "FBD2";
                    }
                    else if (MemoryType > 0x19)
                    {
                        return "Неизвестно";
                    }
                    else
                    {
                        return "Планка не установлена";
                    }
            }
        }
    }
}