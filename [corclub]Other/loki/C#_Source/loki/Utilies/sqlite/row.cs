namespace loki.sqlite
{
    public struct ROW
    {
        public long ID
        {
            get;
            set;
        }

        public string[] RowData
        {
            get;
            set;
        }
    }
}