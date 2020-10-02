namespace DomaNet.SystemInfo
{
    using System;
    using System.Diagnostics;
    using System.Text;

    public class ProcessList
    {
        public static string GetProcessPC(StringBuilder Proclist)
        {
            try
            {
                Proclist.AppendLine($"<center><span style=\"color:#6294B3\">Количество запущенных процессов:</span><span style=\"color:#BA9201\">[{Process.GetProcesses().Length}]</span></center>");
                Array.Sort(Process.GetProcesses(), (p1, p2) => p1.ProcessName.CompareTo(p2.ProcessName));
                foreach (var item in Process.GetProcesses())
                {
                    if (Process.GetCurrentProcess().Id == item.Id || item.Id == 0)
                    {
                        continue;
                    }
                    Proclist.AppendFormat("<span style=\"color:#6294B3\"><a title=\"Имя процесса: {0}.exe\"</a title>{0}.exe</span><a title=\"ID процесса: {1}\"</a title><span style=\"color:#BA9201\">[{1}]</span>" + Environment.NewLine, item.ProcessName, item.Id);
                }
            }
            catch (InvalidOperationException) { }
            catch (Exception) { }

            return Proclist.ToString();
        }
    }
}