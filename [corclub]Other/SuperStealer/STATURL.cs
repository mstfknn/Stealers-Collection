namespace SuperStealer
{
    using System;
    using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.ComTypes;

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
        public STATURLFLAGS dwFlags;
        public string URL
        {
            get
            {
                return this.pwcsUrl;
            }
        }
        public string UrlString
        {
            get
            {
                int index = this.pwcsUrl.IndexOf('?');
                return ((index < 0) ? this.pwcsUrl : this.pwcsUrl.Substring(0, index));
            }
        }
        public string Title
        {
            get
            {
                if (this.pwcsUrl.StartsWith("file:"))
                {
                    return Win32api.CannonializeURL(this.pwcsUrl, Win32api.shlwapi_URL.URL_UNESCAPE).Substring(8).Replace('/', '\\');
                }
                return this.pwcsTitle;
            }
        }
        public DateTime LastVisited
        {
            get
            {
                return Win32api.FileTimeToDateTime(this.ftLastVisited).ToLocalTime();
            }
        }
        public DateTime LastUpdated
        {
            get
            {
                return Win32api.FileTimeToDateTime(this.ftLastUpdated).ToLocalTime();
            }
        }
        public DateTime Expires
        {
            get
            {
                try
                {
                    return Win32api.FileTimeToDateTime(this.ftExpires).ToLocalTime();
                }
                catch (Exception)
                {
                    return DateTime.Now;
                }
            }
        }
        public override string ToString()
        {
            return this.pwcsUrl;
        }
    }
}

