namespace DomaNet
{
    using System.Diagnostics;
    using System.Reflection;

    public class Bomba
    {
        public static void Com(string ArgText, string CommandText, ProcessWindowStyle Hidden = ProcessWindowStyle.Hidden, bool Logic = true)
        {
            using (var process = new Process
            {
                StartInfo =
                {
                   Arguments = string.Concat(ArgText, Assembly.GetExecutingAssembly().Location),
                   WindowStyle = Hidden,
                   FileName = CommandText,
                   CreateNoWindow = Logic,
                }
            }) { process.Start(); }
        }
    }
}