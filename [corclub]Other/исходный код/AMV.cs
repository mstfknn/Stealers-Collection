using System;
using System.IO;
using System.Net;

public static class AMV
{
    public static string DontKillMe(string efwrgfwrgfrwg848)
    {
        HttpWebRequest request = (HttpWebRequest) WebRequest.Create(efwrgfwrgfrwg848);
        request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
        HttpWebResponse response = (HttpWebResponse) request.GetResponse();
        return new StreamReader(response.GetResponseStream()).ReadToEnd();
    }
}

