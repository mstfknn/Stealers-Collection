using System.Runtime.InteropServices;

namespace SimpleChromepwrec
{
    public partial class SQLiteHandler
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct Record_header_field
        {
            public long size;
            public long type;
        }
    }
}
