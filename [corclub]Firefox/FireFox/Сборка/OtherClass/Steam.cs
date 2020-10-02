using Microsoft.Win32;

public class Steam
{
    public static string GetPath()
    {
        string str = "";
        try
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Valve\Steam", true);
            if (key != null)
            {
                str = key.GetValue("InstallPath").ToString();
            }
        }
        catch
        {
            str = GetPath();
        }
        return str;
    }
}