using System;
using System.Runtime.InteropServices;

[ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("3C374A41-BAE4-11CF-BF7D-00AA006946EE")]
public interface IUrlHistoryStg
{
    void AddUrl(string pocsUrl, string pocsTitle, Enums.ADDURL_FLAG dwFlags);
    void DeleteUrl(string pocsUrl, int dwFlags);
    void QueryUrl([MarshalAs(UnmanagedType.LPWStr)] string pocsUrl, Enums.STATURL_QUERYFLAGS dwFlags, ref Structures.STATURL lpSTATURL);
    void BindToObject([In] string pocsUrl, [In] Structures.UUID riid, IntPtr ppvOut);
    object EnumUrls { [return: MarshalAs(UnmanagedType.IUnknown)] get; }
}