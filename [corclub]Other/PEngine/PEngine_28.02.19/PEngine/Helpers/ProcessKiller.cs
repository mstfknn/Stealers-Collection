namespace PEngine.Helpers
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;

    public class ProcessKiller
    {
        public static bool ProcessCompareProductName(Process p, string match)
        {
            try
            {
                return p.MainModule.FileVersionInfo.ProductName.ToLower().Equals(match.ToLower()) ? true : false;
            }
            catch (Win32Exception) { return false; }
            catch (InvalidOperationException) { return false; }
            catch (NullReferenceException) { return false; }
        }

        public static void ClosingCycle(string names, string dnames)
        {
            foreach (Process process in Process.GetProcesses())
            {
                if (ProcessCompareProductName(process, names.ToLower()) != process.ProcessName.Contains(dnames))
                {
                    try
                    {
                        process.CloseMainWindow();
                    }
                    catch (InvalidOperationException) { continue; }
                    catch (PlatformNotSupportedException) { break; }
                }
            }
        }

        public static void Closing(string name)
        {
            foreach (Process process in Process.GetProcesses())
            {
                try
                {
                    if (process.ProcessName.Contains(name))
                    {
                        try
                        {
                            process.CloseMainWindow();
                            if (!process.HasExited)
                            {
                                try
                                {
                                    process?.Kill();
                                }
                                catch (Win32Exception) { break; }
                            }
                        }
                        catch (InvalidOperationException) { continue; }
                        catch (PlatformNotSupportedException) { break; }
                    }
                }
                catch { }
            }
        }

        public static void Delete(string arg, string selfpath, string filename = "cmd.exe", bool tr = true) => RunningWithInfo(new ProcessStartInfo
        {
            Arguments = $"{arg} {selfpath}",
            WindowStyle = ProcessWindowStyle.Hidden,
            CreateNoWindow = tr,
            FileName = filename
        });

        public static void Running(string name, bool discheck)
        {
            try
            {
                switch (discheck)
                {
                    case false:
                        Process.Start(name);
                        break;
                    default:
                        Process.Start(name).WaitForExit();
                        break;
                }
            }
            catch (Win32Exception) { }
            catch (IOException) { }
        }

        public static void RunningWithInfo(ProcessStartInfo name)
        {
            try
            {
                Process.Start(name).Dispose();
                Process.GetCurrentProcess().Kill();
            }
            catch (Win32Exception) { }
            catch (IOException) { }
        }

        public static void RefreshProcess(string procname)
        {
            foreach (Process name in Process.GetProcesses())
            {
                try
                {
                    if (name.ProcessName.Contains(procname))
                    {
                        name?.Refresh();
                    }
                }
                catch (InvalidOperationException) { continue; }
                catch (PlatformNotSupportedException) { break; }
            }
        }
    }
}