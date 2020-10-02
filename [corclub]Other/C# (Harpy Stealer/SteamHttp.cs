using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;

public class SteamHttp
{
    public static bool ObtainsessionID(CookieContainer cookie)
    {
        HttpWebResponse response = null;
        StreamReader reader = null;
        HttpWebRequest request = null;
        try
        {
            request = (HttpWebRequest) WebRequest.Create("http://steamcommunity.com");
            request.Method = "GET";
            request.Headers["Origin"] = "http://steamcommunity.com";
            request.Referer = "http://steamcommunity.com";
            request.Headers["Accept-Encoding"] = "gzip,deflate";
            request.Headers["Accept-Language"] = "en-us,en";
            request.Headers["Accept-Charset"] = "iso-8859-1,*,utf-8";
            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.3; en-US; Valve Steam Client/1393366296; ) AppleWebKit/535.19 (KHTML, like Gecko) Chrome/18.0.1025.166 Safari/535.19";
            request.CookieContainer = cookie;
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            response = (HttpWebResponse) request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                reader = new StreamReader(response.GetResponseStream());
                string str = reader.ReadToEnd();
                return !(reader.ReadToEnd().Contains("g_steamID = false;") || (response.Cookies["sessionid"] == null));
            }
        }
        catch (Exception)
        {
        }
        finally
        {
            if (reader != null)
            {
                reader.Close();
            }
            if (response != null)
            {
                response.Close();
            }
        }
        return false;
    }

    public static string SteamWebRequest(CookieContainer cookie, string url, string data = null, string lasturl = "")
    {
        HttpWebResponse response = null;
        StreamReader reader = null;
        HttpWebRequest request = null;
        try
        {
            request = null;
            if (url.StartsWith("http"))
            {
                request = (HttpWebRequest) WebRequest.Create(url);
            }
            else
            {
                request = (HttpWebRequest) WebRequest.Create("http" + ((data != null) ? "s" : "") + "://steamcommunity.com/" + url);
            }
            request.Referer = "http://steamcommunity.com/" + lasturl;
            request.Accept = (data != null) ? "*/*" : "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.Method = (data != null) ? "POST" : "GET";
            if (data != null)
            {
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            }
            request.Headers["Accept-Charset"] = "iso-8859-1,*,utf-8";
            request.Headers["Origin"] = "https://steamcommunity.com";
            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.3; en-US; Valve Steam Client/1393366296; ) AppleWebKit/535.19 (KHTML, like Gecko) Chrome/18.0.1025.166 Safari/535.19";
            request.Headers["Accept-Language"] = "en-us,en";
            request.Headers["Accept-Encoding"] = "gzip,deflate";
            request.CookieContainer = cookie;
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            if (data != null)
            {
                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(data);
                }
            }
            response = (HttpWebResponse) request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                reader = new StreamReader(response.GetResponseStream());
                return reader.ReadToEnd();
            }
        }
        catch (WebException exception)
        {
            if (exception.Response != null)
            {
                HttpWebResponse response2 = (HttpWebResponse) exception.Response;
                return new StreamReader(response2.GetResponseStream()).ReadToEnd();
            }
        }
        finally
        {
            if (reader != null)
            {
                reader.Close();
            }
            if (response != null)
            {
                response.Close();
            }
        }
        return null;
    }
}

