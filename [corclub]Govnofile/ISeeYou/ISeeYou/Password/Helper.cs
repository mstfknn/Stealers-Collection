using System.IO;

namespace I_See_you
{
    class Helper
    {public static string GetRandomString()
        {
            string randomFileName = Path.GetRandomFileName();
            return randomFileName.Replace(".", "");
        }
    }
}
