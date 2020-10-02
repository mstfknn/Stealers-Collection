using System;
using System.Runtime.InteropServices;

public partial class Structures
{
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
    [StructLayout(LayoutKind.Sequential)]
    public struct UUID
    {
        public int Data1;
        public short Data2;
        public short Data3;
        public byte[] Data4;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct SHFILEINFO
    {
        internal IntPtr hIcon;
        internal IntPtr iIcon;
        public uint dwAttributes;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szDisplayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        public string szTypeName;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct IEAutoComplteSecretHeader
    {
        public uint dwSize;
        public uint dwSecretInfoSize;
        public uint dwSecretSize;
        public IESecretInfoHeader IESecretHeader;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct IESecretInfoHeader
    {
        public uint dwIdHeader;
        public uint dwSize;
        public uint dwTotalSecrets;
        public uint unknown;
        public uint id4;
        public uint unknownZero;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct STATURL
    {
        public int cbSize;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pwcsUrl;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pwcsTitle;
        public System.Runtime.InteropServices.ComTypes.FILETIME ftLastVisited;
        public System.Runtime.InteropServices.ComTypes.FILETIME ftLastUpdated;
        public System.Runtime.InteropServices.ComTypes.FILETIME ftExpires;
        public Enums.STATURLFLAGS dwFlags;
        public string URL
        {
            get
            {
                return pwcsUrl;
            }
        }
        public string UrlString
        {
            get
            {
                var index = pwcsUrl.IndexOf('?');
                return ((index < 0) ? pwcsUrl : pwcsUrl.Substring(0, index));
            }
        }
        public string Title
        {
            get
            {
                if (pwcsUrl.StartsWith("file:"))
                {
                    return Helpers.CannonializeURL(this.pwcsUrl, Enums.Shlwapi_URL.URL_UNESCAPE).Substring(8).Replace('/', '\\');
                }
                return pwcsTitle;
            }
        }
        public DateTime LastVisited
        {
            get
            {
                return Helpers.FileTimeToDateTime(ftLastVisited).ToLocalTime();
            }
        }
        public DateTime LastUpdated
        {
            get
            {
                return Helpers.FileTimeToDateTime(ftLastUpdated).ToLocalTime();
            }
        }
        public DateTime Expires
        {
            get
            {
                try
                {
                    return Helpers.FileTimeToDateTime(ftExpires).ToLocalTime();
                }
                catch (Exception)
                {
                    return DateTime.Now;
                }
            }
        }
        public override string ToString()
        {
            return pwcsUrl;
        }
    }
    [StructLayout(LayoutKind.Explicit)]
    public struct SecretEntry
    {
        [FieldOffset(12)]
        public uint dwLength;
        [FieldOffset(0)]
        public uint dwOffset;
        [FieldOffset(4)]
        public byte SecretId;
        [FieldOffset(5)]
        public byte SecretId1;
        [FieldOffset(6)]
        public byte SecretId2;
        [FieldOffset(7)]
        public byte SecretId3;
        [FieldOffset(8)]
        public byte SecretId4;
        [FieldOffset(9)]
        public byte SecretId5;
        [FieldOffset(10)]
        public byte SecretId6;
        [FieldOffset(11)]
        public byte SecretId7;
    }
}