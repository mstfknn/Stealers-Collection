namespace PEngine.Helpers
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;

    public class NetControl
    {
        private static WebClient web = new WebClient();

        public static bool CheckConnectOne(string OpenClient)
        {
            try
            {
                using (web.OpenRead(OpenClient))
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
                using (var response = (HttpWebResponse)request?.GetResponse())
                {
                    return response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch (TimeoutException) { return false; }
            catch (WebException) { return false; }
        }

        public static string GetData(string uri, string method = "GET")
        {
            try
            {
                HttpWebRequest request = WebRequest.CreateHttp(uri);
                request.KeepAlive = false;
                request.Method = method;
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    try
                    {
                        using (var reader = new StreamReader(response?.GetResponseStream()))
                        {
                            return reader?.ReadToEnd();
                        }
                    }
                    catch { return null; }
                }
            }
            catch { return null; }
        }

        public static string GetPublicIP(string IP_Url)
        {
            try
            {
                return Encoding.ASCII.GetString(web.DownloadData(new Uri(IP_Url)));
            }
            catch (WebException) { return "N/A"; }
            catch (Exception) { return "N/A"; }
        }
    }
}