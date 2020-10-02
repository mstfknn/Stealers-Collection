using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static KiddyAPI.Injector.NativeAPI;

namespace KiddyAPI.Injector
{
    public class Injector
    {
        private static bool is64BitProcess = (IntPtr.Size == 8);
        private static bool is64BitOperatingSystem = is64BitProcess || InternalCheckIsWow64();
        private static bool isInjected = false;

        const uint MEM_COMMIT = 0x00001000;
        const uint MEM_RESERVE = 0x00002000;
        const uint PAGE_READWRITE = 4;

        private static int Inject(string pathDLL, Process process) //Приватный метод, который выполняет инжект, вынес для удобства
        {
            var targetProc = process;
            string dllName = pathDLL;
            var processHandle = OpenProcess(
                ProcessAccessFlags.PROCESS_CREATE_THREAD | ProcessAccessFlags.PROCESS_QUERY_INFORMATION |
                ProcessAccessFlags.PROCESS_VM_OPERATION | ProcessAccessFlags.PROCESS_VM_WRITE |
                ProcessAccessFlags.PROCESS_VM_WRITE | ProcessAccessFlags.PROCESS_VM_READ, false, targetProc.Id);
            IntPtr libAddres = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
            IntPtr allocMemoryAdress = VirtualAllocEx(processHandle, IntPtr.Zero,
                (uint)((dllName.Length + 1) * Marshal.SizeOf(typeof(char))), MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);
            UIntPtr bytesWritten;
            WriteProcessMemory(processHandle, allocMemoryAdress, Encoding.Default.GetBytes(dllName),
                (uint)((dllName.Length + 1) * Marshal.SizeOf(typeof(char))), out bytesWritten);
            CreateRemoteThread(processHandle, IntPtr.Zero, 0, libAddres, allocMemoryAdress, 0, IntPtr.Zero);
            return 0;
        }
        /// <summary>
        /// Инъекция DLL в процесс
        /// </summary>
        /// <param name="pathDLL">Путь к нужной DLL</param>
        /// <param name="process">Имя процесса, в который инжектим</param>
        public static void Execute(string pathDLL, Process process)
        {
            string stockDLL = string.Empty;
            Inject(pathDLL, process);
            isInjected = true;

        }
        /// <summary>
        /// 64Bit или нет
        /// </summary>
        /// <returns></returns>
        private static bool InternalCheckIsWow64()
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
    }
}
