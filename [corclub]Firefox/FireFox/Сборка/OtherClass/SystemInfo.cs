using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Text;

internal class SystemInfo
{
    #region Strings
    static readonly string savePath = @"C:\SysInfo.txt";
    static string ss = "=======================================================================================================";
    static string pcCPU;
    static string pcGPU;
    static string pcLocalIP;
    static string pcExternalIP;
    #endregion Strings
    public static void Writes()
    {
        try
        {
            Process[] processes = Process.GetProcesses();
            if (!File.Exists(savePath))
                using (FileStream file = new FileStream(savePath, FileMode.Append))
                {
                   // File.SetAttributes(file.Name, FileAttributes.Hidden | FileAttributes.System);
                    using (StreamWriter textFileWriter = new StreamWriter(file, Encoding.UTF8))
                    {
                        textFileWriter.WriteLine(ss + "\r\n" + "                            Системная информация компьютера" + "\r\n"
                       + ss
                       + "\r\nОперационная система: " + "[" + Environment.OSVersion.ToString() + "]" + " - " + "# " + OsWiN.Name + "  " + OsWiN.Edition + " #"
                       + "\r\nИмя Пользователя: " + Environment.UserName.ToString()
                       + "\r\nИмя Компьютера: " + Environment.MachineName.ToString()
                       + "\r\nКол-во Процессов: " + Environment.ProcessorCount.ToString()
                       + "\r\nСистемная Директория: " + Environment.SystemDirectory.ToString()
                       + "\r\nРазрядность системы: " + OsWiN.GetOSBit());
                        ManagementObjectSearcher cpuSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
                        foreach (ManagementObject cpuResult in cpuSearcher.Get())
                        {
                            pcCPU = (cpuResult["Name"]).ToString();
                            textFileWriter.WriteLine("Процессор: " + pcCPU.ToString());
                        }
                        try
                        {
                            ManagementObjectSearcher searcher5 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_OperatingSystem");
                            foreach (ManagementObject z in searcher5.Get())
                            {
                                textFileWriter.Write("Виртуальныя память: " + z["FreeVirtualMemory"] + "\r\n");
                                textFileWriter.Write("Серийный номер Windows: " + z["SerialNumber"] + "\r\n");
                            }
                        }
                        catch { }
                        try
                        {
                            ManagementObjectSearcher gpuSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DisplayConfiguration");
                            foreach (ManagementObject gpuResult in gpuSearcher.Get())
                            {
                                foreach (PropertyData property in gpuResult.Properties)
                                {
                                    if (property.Name == "Description")
                                    {
                                        pcGPU = property.Value.ToString();
                                        textFileWriter.WriteLine("Видеоадаптер: " + pcGPU.ToString());
                                    }
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            ManagementObjectSearcher searcher =
                                new ManagementObjectSearcher("root\\CIMV2",
                                "SELECT * FROM Win32_ComputerSystem");
                            foreach (ManagementObject queryObj in searcher.Get())
                            {
                                textFileWriter.WriteLine("Модель компьютера: " + queryObj["Manufacturer"] + queryObj["Model"]);
                            }
                        }
                        catch { }
                        try
                        {
                            pcExternalIP = (new WebClient()).DownloadString("http://ipecho.net/plain");
                            textFileWriter.Write("Внутренний IP: " + pcExternalIP.ToString() + "\r\n");
                        }
                        catch { }
                        try
                        {
                            IPHostEntry host;
                            host = Dns.GetHostEntry(Dns.GetHostName());
                            foreach (IPAddress IP in host.AddressList)
                            {
                                if (IP.AddressFamily == AddressFamily.InterNetwork)
                                {
                                    pcLocalIP = IP.ToString();
                                    textFileWriter.Write("Локальный IP: " + pcLocalIP.ToString() + "\r\n");
                                    break;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            textFileWriter.Write(ss + "\r\n" + "                                Процессы пользователя" + "\r\n");
                            foreach (Process process in processes)
                            {
                                string windowTitle = process.MainWindowTitle;
                                if (process.MainWindowTitle == "")
                                {
                                    windowTitle = "- N/A";
                                }
                                float processMemory = (process.VirtualMemorySize64 / 1024f) / 1024f / 10f;
                                textFileWriter.WriteLine(ss + "\r\n{0}.exe:\r\nPID: {1}\r\nTitle: {2}\r\nRAM: {3} MB", process.ProcessName, process.Id, windowTitle, processMemory.ToString());
                            }
                            textFileWriter.Close();
                            textFileWriter.Dispose();
                        }
                        catch { }
                    }
                }
        }
        catch { }
    }
}