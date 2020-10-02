namespace DaddyRecovery.Helpers
{
    using System;
    using System.Net;
    using System.Text;

    public static class InetControl
    {
        public static bool CheckURL(string url, int TimeOut = 0x3A98, string Head = "HEAD")
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = TimeOut;
            request.Method = Head;
            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    return response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch (TimeoutException) { return false; }
            catch (WebException) { return false; }
        }

        public static string GetPublicIP(string url)
        {
            try
            {
                var uri = new Uri(url);
                using (var client = new WebClient())
                {
                    return Encoding.ASCII.GetString(client.DownloadData(uri));
                }
            }
            catch (WebException) { return "N/A"; }
        }
    }
}