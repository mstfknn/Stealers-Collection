using System;
using System.Text;

public class Helpers
{
    public static string CannonializeURL(string pszUrl, Enums.Shlwapi_URL dwFlags)
    {
        var pszCanonicalized = new StringBuilder(260);
        var capacity = pszCanonicalized.Capacity;
        if (NativeMethods.UrlCanonicalize(pszUrl, pszCanonicalized, ref capacity, dwFlags) != 0)
        {
            pszCanonicalized.Capacity = capacity;
            int num2 = NativeMethods.UrlCanonicalize(pszUrl, pszCanonicalized, ref capacity, dwFlags);
        }
        return pszCanonicalized.ToString();
    }

    public static DateTime FileTimeToDateTime(System.Runtime.InteropServices.ComTypes.FILETIME filetime)
    {
        var systemTime = new Structures.SYSTEMTIME();
        NativeMethods.FileTimeToSystemTime(ref filetime, ref systemTime);
        try
        {
            return new DateTime(systemTime.Year, systemTime.Month, systemTime.Day, systemTime.Hour, systemTime.Minute, systemTime.Second, systemTime.Milliseconds);
        }
        catch (Exception)
        {
            return DateTime.Now;
        }
    }
}