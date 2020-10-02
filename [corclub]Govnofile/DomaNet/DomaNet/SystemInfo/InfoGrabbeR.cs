namespace DomaNet.SystemInfo
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Management;
    using System.Net;
    using System.Text;

    public class InfoGrabbeR
    {
        static List<string> Log = new List<string>();

        static string[] MassLink = 
        {
            "https://api.ipify.org/",
            "http://icanhazip.com/",
            "https://ipinfo.io/ip",
            "http://checkip.amazonaws.com/"
        };

        public static string GetInternalIP() => Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();

        public static void CreateTable(string HomePath)
        {
            #region MainInfo

            Log.Add("<!DOCTYPE html>");
            Log.Add("<html>");
            Log.Add("<head>");
            Log.Add("<title>SystemInfo</title>");
            Log.Add("<link rel=\"icon\" type=\"image/png\" href=\"http://s1.iconbird.com/ico/0612/customicondesignoffice2/w48h481339870371Personalinformation48.png\" sizes=\"32x32\">");
            Log.Add("</head>");
            Log.Add("<body style=\"width:100%; height:100%; margin: 0; background: url(http://radiographer.co.il/wp-content/uploads/2015/11/backgroundMain.jpg) #191919\">");
            Log.Add("<center><h1 style=\"color:#85AB70; margin:50px 0\">Системная Информация о Компьютере</h1></center>");
            Log.Add($"<center><span style=\"color:#6294B3\">Операционная система: </span><span style=\"color:#BA9201\">{OSInfo.osVersion} [ {OSInfo.Name} {OSInfo.Edition} ] </span></center>");
            Log.Add($"<center><span style=\"color:#6294B3\">Имя Компьютера: </span><span style=\"color:#BA9201\">{OSInfo.MachineName}</span></center>");
            Log.Add($"<center><span style=\"color:#6294B3\">Имя Пользователя: </span><span style=\"color:#BA9201\">{OSInfo.UserName}</span></center>");
            Log.Add($"<center><span style=\"color:#6294B3\">Логических Процессоров: </span><span style=\"color:#BA9201\">{OSInfo.ProcessorCount}</span></center>");
            Log.Add($"<center><span style=\"color:#6294B3\">Системная Директория: </span><span style=\"color:#BA9201\">{OSInfo.SystemDir}</span></center>");
            Log.Add($"<center><span style=\"color:#6294B3\">Разрядность системы: </span><span style=\"color:#BA9201\">{OSInfo.OSBit}</span></center>");

            #endregion

            #region GetProcessor
            try
            {
                using (var Secret = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_Processor"))
                {
                    foreach (var cr in Secret.Get())
                    {
                        Log.Add($"<center><span style=\"color:#6294B3\">Процессор: </span><span style=\"color:#BA9201\">{(cr["Name"]).ToString()} [ <a title=\"Производитель: {(cr["Manufacturer"]).ToString()}\"</span>{(cr["Manufacturer"]).ToString()} ]</a title></center>");
                        break;
                    }
                }
            }
            catch { }

            #endregion

            #region GetScreen_Resolution

            try
            {
                using (var MonitorEx = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_DesktopMonitor"))
                {
                    foreach (var queryObj in MonitorEx.Get())
                    {
                        if (queryObj["ScreenWidth"] == null || queryObj["ScreenHeight"] == null)
                        {
                            continue;
                        }

                        Log.Add($"<center><span style=\"color:#6294B3\">Разрешение экрана: </span><span style=\"color:#BA9201\">{queryObj["ScreenWidth"]} x {queryObj["ScreenHeight"]}</span></center>");
                        break;
                    }
                }
            }
            catch { }

            #endregion

            #region GetWinKey

            try
            {
                using (var OS = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_OperatingSystem"))
                {
                    foreach (var z in OS.Get())
                    {
                        Log.Add($"<center><span style=\"color:#6294B3\">Зарегистрированный пользователь: </span><span style=\"color:#BA9201\">{z["RegisteredUser"].ToString()}</span></center>");
                        Log.Add($"<center><span style=\"color:#6294B3\">Серийный ключ Windows: </span><span style=\"color:#BA9201\">{WinKey.GetWindowsProductKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "DigitalProductId")}</span></center>");
                        Log.Add($"<center><span style=\"color:#6294B3\">Код продукта Windows: </span><span style=\"color:#BA9201\">{z["SerialNumber"].ToString()}</span></center>");
                        break;
                    }
                }
            }
            catch { }

            #endregion

            #region GetBiosVersion

            try
            {
                using (var Bios = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_BIOS"))
                {
                    foreach (var obj in Bios.Get())
                    {
                        if (((string[])obj["BIOSVersion"]).Length > 1)
                        {
                            Log.Add($"<center><span style=\"color:#6294B3\">Версия Биос: <span style=\"color:#BA9201\">{((string[])obj["BIOSVersion"])[0]} - {((string[])obj["BIOSVersion"])[1]} [ <a title=\"Производитель: {obj["Manufacturer"].ToString()}\">{obj["Manufacturer"].ToString()}</a title> ]</span><span style=\"color:#BA9201\"></span></center>");
                        }
                        else
                        {
                            Log.Add($"<center><span style=\"color:#6294B3\">Версия Биос: <span style=\"color:#BA9201\">{((string[])obj["BIOSVersion"])[0]} [ <a title=\"Производитель: {2}\">{2}</a title> ]</span></center>");
                        }
                        break;
                    }
                }
            }
            catch { }

            #endregion

            #region GetInstallAntivirus

            try
            {
                using (var wmiData = new ManagementObjectSearcher(@"root\SecurityCenter2", "SELECT * FROM AntiVirusProduct"))
                {
                    foreach (var item in wmiData.Get())
                    {
                        Log.Add($"<center><span style=\"color:#6294B3\">Установленный антивирус: </span><span style=\"color:#BA9201\">{item["displayName"].ToString()}</span></center>");
                    }
                }
            }
            catch { }

            #endregion

            #region GetPhysicalMemory

            try
            {
                using (var TotalMemory = new ManagementObjectSearcher(@"root\CIMV2", "SELECT TotalPhysicalMemory FROM Win32_ComputerSystem"))
                {
                    foreach (var Total in TotalMemory.Get())
                    {
                        if ((Convert.ToDouble(Total["TotalPhysicalMemory"])) / 0x4000_0000 > 0x1)
                        {
                            Log.Add($"<center><span style=\"color:#6294B3\">Физической памяти: </span><span style=\"color:#BA9201\">({$"{Convert.ToString(Math.Round((Convert.ToDouble(Total["TotalPhysicalMemory"])) / 0x4000_0000 * 0x3E8, 0x2))} MB"})</span></center>");
                        }
                        else
                        {
                            Log.Add($"({$"{Convert.ToString(Math.Round((Convert.ToDouble(Total["TotalPhysicalMemory"])) / 0x4000_0000, 0x2))} GB"})");
                        }
                        break;
                    }
                }
            }
            catch { }

            #endregion

            #region GetVideoCard

            try
            {
                using (var VC = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_VideoController"))
                {
                    foreach (var w in VC.Get())
                    {
                        string container;
                        switch ((Convert.ToDouble(w["AdapterRam"]) / 0x10_0000) % 0x400)
                        {
                            case 0:
                                container = string.Concat(((Convert.ToDouble(w["AdapterRam"]) / 0x100000) / 0x400), " GB");
                                break;
                            default:
                                container = string.Concat((Convert.ToDouble(w["AdapterRam"]) / 0x100000).ToString(), " MB");
                                break;
                        }
                        Log.Add($"<center><span style=\"color:#6294B3\">Видеокарта: </span><span style=\"color:#BA9201\">{w["Caption"]?.ToString()}  -  <a title=\"Память видеокарты: {container}\"</span>({container})</a title></tr></span></center>");
                    }
                }
            }
            catch { }

            #endregion

            #region GetIP

            try
            {
                for (var i = 0; i < MassLink.Length; i++)
                {
                    if (NetControl.CheckURL(MassLink[i]))
                    {
                        Log.Add($"<center><span style=\"color:#6294B3\">Внутренний IP: </span><span style=\"color:#BA9201\">{NetControl.GetPublicIP(MassLink[i].ToString())}</span></center>");
                        break;
                    }
                }
            }
            catch { }

            #endregion

            #region ListProcess

            Log.Add($"<center><span style=\"color:#6294B3\"><details><summary>Показать</span><span style=\"color:#BA9201\">Скрыть Запущенные процессы пользователя</summary><pre>{ProcessList.GetProcessPC(new StringBuilder())}</span></center>");

            #endregion

            #region TheEndTable

            Log.Add("</html>");
            Log.Add("</table>");
            File.WriteAllLines(HomePath, Log);
            #endregion
        }
    }
}