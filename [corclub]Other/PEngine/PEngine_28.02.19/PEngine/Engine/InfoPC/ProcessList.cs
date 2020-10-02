namespace PEngine.Engine.InfoPC
{
    using System;
    using System.Diagnostics;
    using System.Text;

    public class ProcessList
    {
        public ProcessList() { }

        public static string GetProcessPC(StringBuilder Proclist)
        {
            try
            {
                Process[] Processes = Process.GetProcesses();
                Proclist.AppendLine($"<center><span style=\"color:#6294B3\">Number of running processes:</span><span style=\"color:#BA9201\">[{Processes.Length}]</span></center>");
                Array.Sort(Processes, (p1, p2) => p1.ProcessName.CompareTo(p2.ProcessName));
                foreach (Process item in Processes)
                {
                    if (Process.GetCurrentProcess().Id == item.Id || item.Id == 0)
                    {
                        continue;
                    }
                    Proclist.AppendFormat($"<span style=\"color:#6294B3\"><a title=\"Process name: {{0}}.exe\"</a title>{{0}}.exe</span><a title=\"Process ID: {{1}}\"</a title><span style=\"color:#BA9201\">[{{1}}]</span>{Environment.NewLine}", item.ProcessName.ToLower(), item.Id);
                }
            }
            catch (InvalidOperationException) { }
            catch (Exception) { }

            return Proclist?.ToString();
        }
    }
}