
using System.IO;

namespace Soph.Stealer
{
  internal static class Helper
  {
    public static string GetRandomString()
    {
      return Path.GetRandomFileName().Replace(".", "");
    }
  }
}
