namespace PCInfo
{
    using System;
    using System.Net;
    using System.Text;

    public class NetControl
    {
        private static readonly bool False = false;

        public static bool CheckURL(string url, int TimeOut = 0x3A98, string Head = "HEAD")
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = TimeOut;
            request.Method = Head;
            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    try
                    {
                        return response.StatusCode == HttpStatusCode.OK;
                    }
                    catch (ObjectDisposedException) { return False; }
                }
            }
            catch (TimeoutException) { return False; }
            catch (WebException) { return False; }
        }

        public static string GetPublicIP(string IP_Url)
        {
            try
            {
                return Encoding.ASCII.GetString(new WebClient().DownloadData(IP_Url));
            }
            catch (WebException) { return "N/A"; }
        }

    }
}