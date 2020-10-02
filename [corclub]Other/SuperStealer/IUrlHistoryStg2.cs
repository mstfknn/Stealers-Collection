namespace SuperStealer
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("AFA0DC11-C313-11D0-831A-00C04FD5AE38"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IUrlHistoryStg2 : IUrlHistoryStg
    {
        void AddUrl(string pocsUrl, string pocsTitle, ADDURL_FLAG dwFlags);
        void DeleteUrl(string pocsUrl, int dwFlags);
        void QueryUrl([MarshalAs(UnmanagedType.LPWStr)] string pocsUrl, STATURL_QUERYFLAGS dwFlags, ref STATURL lpSTATURL);
        void BindToObject([In] string pocsUrl, [In] UUID riid, IntPtr ppvOut);
        object EnumUrls { [return: MarshalAs(UnmanagedType.IUnknown)] get; }
        void AddUrlAndNotify(string pocsUrl, string pocsTitle, int dwFlags, int fWriteHistory, object poctNotify, object punkISFolder);
        void ClearHistory();
    }
}

