namespace PCInfo
{
    using System.IO;
    using System.Reflection;

    internal static partial class Program
    {
        private static readonly string GetPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Info.html");

        private static void Main() => 
            InfoGrabber.CreateTable(GetPath);
    }
}