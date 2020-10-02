using System;
using System.IO;
using Microsoft.Win32;

public class Path
{
    public static string GetFireFoxPath()
    {
        string result;
        try
        {
            string text = $"{Environment.GetEnvironmentVariable("APPDATA")}\\Mozilla\\Firefox\\";
            string text2 = File.ReadAllText($"{text}profiles.ini");
            text2 = text2.Substring(text2.IndexOf("Path=") + 5);
            text2 = text2.Remove(text2.IndexOf("\r"));
            string[] array = text2.Split(new char[]
				{
					'/'
				});
            text2 = $"{array[0]}\\{array[1]}";
            text += text2;
            result = text;
        }
        catch
        {
            result = null;
        }
        return result;
    }

    public static string GetFireFoxExePath()
    {
        string result;
        try
        {
            string text = new DirectoryInfo((string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\firefox.exe", "Path", null)).FullName;
            text += "\\";
            result = text;
        }
        catch
        {
            result = null;
        }
        return result;
    }
}