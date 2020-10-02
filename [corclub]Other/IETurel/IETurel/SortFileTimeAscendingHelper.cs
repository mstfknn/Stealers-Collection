using System.Collections;

public sealed class SortFileTimeAscendingHelper : IComparer
{
    public static IComparer SortFileTimeAscending()
    {
        return new SortFileTimeAscendingHelper();
    }

    int IComparer.Compare(object a, object b)
    {
        var staturl = (Structures.STATURL)a;
        var staturl2 = (Structures.STATURL)b;
        return NativeMethods.CompareFileTime(ref staturl.ftLastVisited, ref staturl2.ftLastVisited);
    }
}