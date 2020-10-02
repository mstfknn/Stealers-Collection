using System;
using System.Collections.Generic;
using System.IO;

public partial class ExplorerUrlHistory : IDisposable, IExplorerUrlHistory
{
    private UrlHistoryClass urlHistory = new UrlHistoryClass();

    public ExplorerUrlHistory()
    {
        obj = (IUrlHistoryStg2)urlHistory;
        _urlHistoryList = new List<Structures.STATURL>();
        new STATURLEnumerator((IEnumSTATURL)obj.EnumUrls).GetUrlHistory(_urlHistoryList);
    }

    public STATURLEnumerator GetEnumerator()
    {
        return new STATURLEnumerator((IEnumSTATURL)obj.EnumUrls);
    }

    public void AddHistoryEntry(string pocsUrl, string pocsTitle, Enums.ADDURL_FLAG dwFlags)
    {
        obj.AddUrl(pocsUrl, pocsTitle, dwFlags);
    }

    public void ClearHistory()
    {
        obj.ClearHistory();
    }

    public bool DeleteHistoryEntry(string pocsUrl, int dwFlags)
    {
        try
        {
            obj.DeleteUrl(pocsUrl, dwFlags);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private List<Structures.STATURL> _urlHistoryList;

    private readonly IUrlHistoryStg2 obj;

    public int Count
    {
        get
        {
            return _urlHistoryList.Count;
        }
    }

    public List<Structures.STATURL> UrlHistoryList { get => _urlHistoryList; set => _urlHistoryList = value; }

    public Structures.STATURL QueryUrl(string pocsUrl, Enums.STATURL_QUERYFLAGS dwFlags)
    {
        var lpSTATURL = new Structures.STATURL();
        try
        {
            obj.QueryUrl(pocsUrl, dwFlags, ref lpSTATURL);
            return lpSTATURL;
        }
        catch (FileNotFoundException)
        {
            return lpSTATURL;
        }
    }

    public Structures.STATURL this[int index]
    {
        get
        {
            return _urlHistoryList[index];
        }
        set
        {
            _urlHistoryList[index] = value;
        }
    }

    #region IDisposable Support

    private bool disposedValue = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion
}