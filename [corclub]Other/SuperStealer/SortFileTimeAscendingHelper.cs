namespace SuperStealer
{
    using System;
    using System.Collections;
    using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.ComTypes;

    public class SortFileTimeAscendingHelper : IComparer
    {
        [DllImport("Kernel32.dll")]
        private static extern int CompareFileTime([In] ref System.Runtime.InteropServices.ComTypes.FILETIME lpFileTime1, [In] ref System.Runtime.InteropServices.ComTypes.FILETIME lpFileTime2);
        public static IComparer SortFileTimeAscending()
        {
            return new SortFileTimeAscendingHelper();
        }

        int IComparer.Compare(object a, object b)
        {
            STATURL staturl = (STATURL) a;
            STATURL staturl2 = (STATURL) b;
            return CompareFileTime(ref staturl.ftLastVisited, ref staturl2.ftLastVisited);
        }
    }
}

