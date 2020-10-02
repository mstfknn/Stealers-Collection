using System.Collections;

public partial class ExplorerUrlHistory
{
    public class STATURLEnumerator
    {
        private readonly IEnumSTATURL _enumerator;
        private int _index;
        private Structures.STATURL _staturl;

        public STATURLEnumerator(IEnumSTATURL enumerator)
        {
            _enumerator = enumerator;
        }

        public STATURLEnumerator Clone()
        {
            _enumerator.Clone(out IEnumSTATURL mstaturl);
            return new STATURLEnumerator(mstaturl);
        }

        public void GetUrlHistory(IList list)
        {
            while (true)
            {
                _staturl = new Structures.STATURL();
                _enumerator.Next(1, ref _staturl, out _index);
                if (_index == 0)
                {
                    _enumerator.Reset();
                    return;
                }
                list.Add(_staturl);
            }
        }

        public bool MoveNext()
        {
            _staturl = new Structures.STATURL();
            _enumerator.Next(1, ref _staturl, out _index);
            if (_index == 0)
            {
                return false;
            }
            return true;
        }

        public void Reset() => _enumerator.Reset();

        public void SetFilter(string poszFilter, Enums.STATURLFLAGS dwFlags) => _enumerator.SetFilter(poszFilter, dwFlags);

        public void Skip(int celt) => _enumerator.Skip(celt);

        public Structures.STATURL Current
        {
            get
            {
                return _staturl;
            }
        }
    }
}