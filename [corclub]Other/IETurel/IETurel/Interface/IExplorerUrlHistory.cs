using System.Collections.Generic;

public interface IExplorerUrlHistory
{
    Structures.STATURL this[int index] { get; set; }

    int Count { get; }
    List<Structures.STATURL> UrlHistoryList { get; set; }

    void AddHistoryEntry(string pocsUrl, string pocsTitle, Enums.ADDURL_FLAG dwFlags);
    void ClearHistory();
    bool DeleteHistoryEntry(string pocsUrl, int dwFlags);
    void Dispose();
    ExplorerUrlHistory.STATURLEnumerator GetEnumerator();
    Structures.STATURL QueryUrl(string pocsUrl, Enums.STATURL_QUERYFLAGS dwFlags);
}