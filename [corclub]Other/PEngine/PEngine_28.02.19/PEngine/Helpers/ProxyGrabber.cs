namespace PEngine.Helpers
{
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;

    public class ProxyGrabber
    {
        public static StringBuilder builder = new StringBuilder();
        public static void Parse(string uri)
        {
            using (var wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;
                Match match = new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}:\d{1,6}").Match(wc.DownloadString(uri));
                if (match.Success)
                {
                    builder.Append($"\"{match.Value.Replace(":", "\", ")}");
                }
            }
        }
    }
}