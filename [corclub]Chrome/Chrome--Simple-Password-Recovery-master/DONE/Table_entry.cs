using System.Runtime.InteropServices;

namespace SimpleChromepwrec
{
    public partial class SQLiteHandler
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct Table_entry
        {
            public long row_id;
            public string[] content;
        }
    }
}
