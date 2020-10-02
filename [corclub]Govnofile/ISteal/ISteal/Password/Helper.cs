using System.IO;

namespace ISteal.Password
{
    class Helper
    {
        public static string GetRandomString()
        {
            return Path.GetRandomFileName().Replace(".", "");
        }
    }
}