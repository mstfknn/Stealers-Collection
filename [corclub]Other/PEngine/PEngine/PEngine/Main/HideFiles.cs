namespace PEngine.Main
{
    using System;
    using System.Diagnostics;
    using System.IO;

    public class HideFiles
    {
        public HideFiles() { }

        public static void Hide(string PathFile, FileAttributes attributes)
        {
            try
            {
                File.SetAttributes(PathFile, attributes);
            }
            catch (UnauthorizedAccessException) { }
            catch (IOException) { }
            catch (NotSupportedException) { }
        }

        public static bool RunAsHidden(string FileExname, ProcessWindowStyle style = ProcessWindowStyle.Hidden)
        {
            try
            {
                using (var process = new Process
                {
                    StartInfo =
                    {
                        WindowStyle = style,
                        FileName = FileExname,
                        CreateNoWindow = true,
                        UseShellExecute = false,
                    }
                })
                {
                    process.Start();
                    return true;
                }
            }
            catch { return false; }
        }
    }
}