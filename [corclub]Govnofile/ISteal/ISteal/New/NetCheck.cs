using System.Net;

namespace ISteal.New
{
    public class NetCheck
    {
        public static bool GetCheckForInternetConnection(string OpenClient)
        {
            try
            {
                using (new WebClient().OpenRead(OpenClient))
                {
                    return true;
                }
            }
            catch { return false; }
        }
    }
}