namespace DomaNet
{
    using System;
    using System.Net;
    using System.Text;

    public class NetControl
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
            catch (Exception) { return false; }
        }

        public static string GetPublicIP(string IP_Url)
        {
            try
            {
                return Encoding.ASCII.GetString(bytes: new WebClient().DownloadData(IP_Url));
            }
            catch (WebException) { return "N/A"; }
            catch (Exception) { return "N/A"; }
        }
    }
}