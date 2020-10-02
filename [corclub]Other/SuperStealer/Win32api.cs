namespace SuperStealer
{
    using System;
    using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.ComTypes;
    using System.Text;

    public class Win32api
    {
        public const uint FILE_ATTRIBUTRE_NORMAL = 0x4000;
        public const uint ILC_COLORDDB = 1;
        public const uint ILC_MASK = 0;
        public const uint ILD_TRANSPARENT = 1;
        public const uint SHGFI_ATTR_SPECIFIED = 0x20000;
        public const uint SHGFI_ATTRIBUTES = 0x800;
        public const uint SHGFI_DISPLAYNAME = 0x200;
        public const uint SHGFI_EXETYPE = 0x2000;
        public const uint SHGFI_ICON = 0x100;
        public const uint SHGFI_ICONLOCATION = 0x1000;
        public const uint SHGFI_LARGEICON = 0;
        public const uint SHGFI_PIDL = 8;
        public const uint SHGFI_SHELLICONSIZE = 4;
        public const uint SHGFI_SMALLICON = 1;
        public const uint SHGFI_SYSICONINDEX = 0x4000;
        public const uint SHGFI_TYPENAME = 0x400;
        public const uint SHGFI_USEFILEATTRIBUTES = 0x10;

        public static string CannonializeURL(string pszUrl, shlwapi_URL dwFlags)
        {
            StringBuilder pszCanonicalized = new StringBuilder(260);
            int capacity = pszCanonicalized.Capacity;
            if (UrlCanonicalize(pszUrl, pszCanonicalized, ref capacity, dwFlags) != 0)
            {
                pszCanonicalized.Capacity = capacity;
                int num2 = UrlCanonicalize(pszUrl, pszCanonicalized, ref capacity, dwFlags);
            }
            return pszCanonicalized.ToString();
        }

        [DllImport("Kernel32.dll")]
        public static extern int CompareFileTime([In] ref System.Runtime.InteropServices.ComTypes.FILETIME lpFileTime1, [In] ref System.Runtime.InteropServices.ComTypes.FILETIME lpFileTime2);
        public static System.Runtime.InteropServices.ComTypes.FILETIME DateTimeToFileTime(DateTime datetime)
        {
            System.Runtime.InteropServices.ComTypes.FILETIME filetime;
            SYSTEMTIME lpSystemTime = new SYSTEMTIME();
            lpSystemTime.Year = (short) datetime.Year;
            lpSystemTime.Month = (short) datetime.Month;
            lpSystemTime.Day = (short) datetime.Day;
            lpSystemTime.Hour = (short) datetime.Hour;
            lpSystemTime.Minute = (short) datetime.Minute;
            lpSystemTime.Second = (short) datetime.Second;
            lpSystemTime.Milliseconds = (short) datetime.Millisecond;
            SystemTimeToFileTime(ref lpSystemTime, out filetime);
            return filetime;
        }

        public static DateTime FileTimeToDateTime(System.Runtime.InteropServices.ComTypes.FILETIME filetime)
        {
            SYSTEMTIME systemTime = new SYSTEMTIME();
            FileTimeToSystemTime(ref filetime, ref systemTime);
            try
            {
                return new DateTime(systemTime.Year, systemTime.Month, systemTime.Day, systemTime.Hour, systemTime.Minute, systemTime.Second, systemTime.Milliseconds);
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }

        [DllImport("Kernel32.dll", CharSet=CharSet.Auto)]
        private static extern bool FileTimeToSystemTime(ref System.Runtime.InteropServices.ComTypes.FILETIME FileTime, ref SYSTEMTIME SystemTime);
        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);
        [DllImport("Kernel32.dll", CharSet=CharSet.Auto)]
        private static extern bool SystemTimeToFileTime([In] ref SYSTEMTIME lpSystemTime, out System.Runtime.InteropServices.ComTypes.FILETIME lpFileTime);
        [DllImport("shlwapi.dll")]
        public static extern int UrlCanonicalize(string pszUrl, StringBuilder pszCanonicalized, ref int pcchCanonicalized, shlwapi_URL dwFlags);

        [Flags]
        public enum shlwapi_URL : uint
        {
            URL_DONT_SIMPLIFY = 0x8000000,
            URL_ESCAPE_PERCENT = 0x1000,
            URL_ESCAPE_SPACES_ONLY = 0x4000000,
            URL_ESCAPE_UNSAFE = 0x20000000,
            URL_PLUGGABLE_PROTOCOL = 0x40000000,
            URL_UNESCAPE = 0x10000000
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME
        {
            public short Day;
            public short DayOfWeek;
            public short Hour;
            public short Milliseconds;
            public short Minute;
            public short Month;
            public short Second;
            public short Year;
        }
    }
}

