namespace DaddyRecovery.Helpers
{
    using System.Linq;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;

    public static class ProcessControl
    {
        public static bool RunFile(string filename)
        {
            if (!string.IsNullOrWhiteSpace(filename))
            {
                try
                {
                    var startInfo = new ProcessStartInfo
                    {
                        FileName = filename,
                        CreateNoWindow = false,
                        WindowStyle = ProcessWindowStyle.Hidden
                    };
                    using (var info = Process.Start(startInfo)) { info.Refresh(); }
                    return true;
                }
                catch (Exception) { return false; }
            }
            return true;
        }

        public static void Closing(string name)
        {
            try
            {
                foreach (var process in Process.GetProcesses().Where(process => process.ProcessName.Contains(name)).Select(process => process))
                {
                    try
                    {
                        process.CloseMainWindow();
                        if (!process.HasExited)
                        {
                            try
                            {
                                process.Kill();
                            }
                            catch (Win32Exception) { break; }
                        }
                    }
                    catch (Exception) { }
                }
            }
            catch { }
        }
    }
}