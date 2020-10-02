using System;
using System.Runtime.InteropServices;

[ComImport, Guid("AFA0DC11-C313-11D0-831A-00C04FD5AE38"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IUrlHistoryStg2 : IUrlHistoryStg
{
    new void AddUrl(string pocsUrl, string pocsTitle, Enums.ADDURL_FLAG dwFlags);
    new void DeleteUrl(string pocsUrl, int dwFlags);
    new void QueryUrl([MarshalAs(UnmanagedType.LPWStr)] string pocsUrl, Enums.STATURL_QUERYFLAGS dwFlags, ref Structures.STATURL lpSTATURL);
    new void BindToObject([In] string pocsUrl, [In] Structures.UUID riid, IntPtr ppvOut);

    new object EnumUrls { [return: MarshalAs(UnmanagedType.IUnknown)] get; }
    void AddUrlAndNotify(string pocsUrl, string pocsTitle, int dwFlags, int fWriteHistory, object poctNotify, object punkISFolder);
    void ClearHistory();
}