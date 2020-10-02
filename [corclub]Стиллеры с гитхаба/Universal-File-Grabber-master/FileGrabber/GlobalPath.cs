namespace FileGrabber
{
    using System;
    using System.IO;

    public class GlobalPath
    {
        public static readonly string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public static readonly string DefaultTemp = string.Concat(Environment.GetEnvironmentVariable("temp"), '\\');
        public static readonly string User_Name = string.Concat(DefaultTemp, Environment.UserName);
        public static readonly string GrabberDir = Path.Combine(User_Name, "All_Files");
    }
}