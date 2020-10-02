using System.Runtime.InteropServices;

namespace SimpleChromepwrec
{
    public partial class SQLiteHandler
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct Sqlite_master_entry
        {
            public long row_id;
            public string item_type;
            public string item_name;
            public string astable_name;
            public long root_num;
            public string sql_statement;
        }
    }
}
