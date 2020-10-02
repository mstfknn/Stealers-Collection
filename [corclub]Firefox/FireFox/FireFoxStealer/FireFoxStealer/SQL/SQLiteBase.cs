using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;

public class SQLiteBase
{
    private static int _count;
    private const int SQL_DONE = 0x65;
    private const int SQL_OK = 0;
    private const int SQL_ROW = 100;
    private IntPtr database;
    private static CounterCallbackDelegate CounterCallback = delegate(IntPtr param1, int param2, IntPtr param3, IntPtr param4)
    {
        _count++;
        return 0;
    };
    #region Delegate
    private static sqlite3_close_delegate sqlite3_close;
    private static sqlite3_column_blob_delegate sqlite3_column_blob;
    private static sqlite3_column_bytes_delegate sqlite3_column_bytes;
    private static sqlite3_column_count_delegate sqlite3_column_count;
    private static sqlite3_column_double_delegate sqlite3_column_double;
    private static sqlite3_column_int_delegate sqlite3_column_int;
    private static sqlite3_column_name_delegate sqlite3_column_name;
    private static sqlite3_column_table_name_delegate sqlite3_column_table_name;
    private static sqlite3_column_text_delegate sqlite3_column_text;
    private static sqlite3_column_type_delegate sqlite3_column_type;
    private static sqlite3_errmsg_delegate sqlite3_errmsg;
    private static sqlite3_exec_delegate sqlite3_exec;
    private static sqlite3_finalize_delegate sqlite3_finalize;
    private static sqlite3_open_delegate sqlite3_open;
    private static sqlite3_prepare_v2_delegate sqlite3_prepare_v2;
    private static sqlite3_step_delegate sqlite3_step;
    private delegate int sqlite3_close_delegate(IntPtr database);
    private delegate IntPtr sqlite3_column_blob_delegate(IntPtr statement, int columnNumber);
    private delegate int sqlite3_column_bytes_delegate(IntPtr statement, int columnNumber);
    private delegate int sqlite3_column_count_delegate(IntPtr statement);
    private delegate double sqlite3_column_double_delegate(IntPtr statement, int columnNumber);
    private delegate long sqlite3_column_int_delegate(IntPtr statement, int columnNumber);
    private delegate IntPtr sqlite3_column_name_delegate(IntPtr statement, int columnNumber);
    private delegate IntPtr sqlite3_column_table_name_delegate(IntPtr statement, int columnNumber);
    private delegate IntPtr sqlite3_column_text_delegate(IntPtr statement, int columnNumber);
    private delegate int sqlite3_column_type_delegate(IntPtr statement, int columnNumber);
    private delegate IntPtr sqlite3_errmsg_delegate(IntPtr database);
    private delegate int sqlite3_exec_delegate(IntPtr database, IntPtr query, IntPtr callback, IntPtr arguments, out IntPtr error);
    private delegate int sqlite3_finalize_delegate(IntPtr handle);
    private delegate int sqlite3_open_delegate(IntPtr fileName, out IntPtr database);
    private delegate int sqlite3_prepare_v2_delegate(IntPtr database, IntPtr query, int length, out IntPtr statement, out IntPtr tail);
    private delegate int sqlite3_step_delegate(IntPtr statement);
    private delegate int CounterCallbackDelegate(IntPtr param1, int param2, IntPtr param3, IntPtr param4);
    #endregion Delegate
    public SQLiteBase()
    {
    #region SQLlite3_x64
        if (SystemInterop.Is64BitWindows())
        {
            sqlite3_open = new sqlite3_open_delegate(SQLiteBase.sqlite3_open_x64);
            sqlite3_close = new sqlite3_close_delegate(SQLiteBase.sqlite3_close_x64);
            sqlite3_exec = new sqlite3_exec_delegate(SQLiteBase.sqlite3_exec_x64);
            sqlite3_errmsg = new sqlite3_errmsg_delegate(SQLiteBase.sqlite3_errmsg_x64);
            sqlite3_prepare_v2 = new sqlite3_prepare_v2_delegate(SQLiteBase.sqlite3_prepare_v2_x64);
            sqlite3_step = new sqlite3_step_delegate(SQLiteBase.sqlite3_step_x64);
            sqlite3_column_count = new sqlite3_column_count_delegate(SQLiteBase.sqlite3_column_count_x64);
            sqlite3_column_name = new sqlite3_column_name_delegate(SQLiteBase.sqlite3_column_name_x64);
            sqlite3_column_type = new sqlite3_column_type_delegate(SQLiteBase.sqlite3_column_type_x64);
            sqlite3_column_int = new sqlite3_column_int_delegate(SQLiteBase.sqlite3_column_int_x64);
            sqlite3_column_double = new sqlite3_column_double_delegate(SQLiteBase.sqlite3_column_double_x64);
            sqlite3_column_text = new sqlite3_column_text_delegate(SQLiteBase.sqlite3_column_text_x64);
            sqlite3_column_blob = new sqlite3_column_blob_delegate(SQLiteBase.sqlite3_column_blob_x64);
            sqlite3_column_bytes = new sqlite3_column_bytes_delegate(SQLiteBase.sqlite3_column_bytes_x64);
            sqlite3_column_table_name = new sqlite3_column_table_name_delegate(SQLiteBase.sqlite3_column_table_name_x64);
            sqlite3_finalize = new sqlite3_finalize_delegate(SQLiteBase.sqlite3_finalize_x64);
    #endregion SQLlite3_x64
    #region SQLlite3_86
        }
        else
        {
            sqlite3_open = new sqlite3_open_delegate(SQLiteBase.sqlite3_open_x86);
            sqlite3_close = new sqlite3_close_delegate(SQLiteBase.sqlite3_close_x86);
            sqlite3_exec = new sqlite3_exec_delegate(SQLiteBase.sqlite3_exec_x86);
            sqlite3_errmsg = new sqlite3_errmsg_delegate(SQLiteBase.sqlite3_errmsg_x86);
            sqlite3_prepare_v2 = new sqlite3_prepare_v2_delegate(SQLiteBase.sqlite3_prepare_v2_x86);
            sqlite3_step = new sqlite3_step_delegate(SQLiteBase.sqlite3_step_x86);
            sqlite3_column_count = new sqlite3_column_count_delegate(SQLiteBase.sqlite3_column_count_x86);
            sqlite3_column_name = new sqlite3_column_name_delegate(SQLiteBase.sqlite3_column_name_x86);
            sqlite3_column_type = new sqlite3_column_type_delegate(SQLiteBase.sqlite3_column_type_x86);
            sqlite3_column_int = new sqlite3_column_int_delegate(SQLiteBase.sqlite3_column_int_x86);
            sqlite3_column_double = new sqlite3_column_double_delegate(SQLiteBase.sqlite3_column_double_x86);
            sqlite3_column_text = new sqlite3_column_text_delegate(SQLiteBase.sqlite3_column_text_x86);
            sqlite3_column_blob = new sqlite3_column_blob_delegate(SQLiteBase.sqlite3_column_blob_x86);
            sqlite3_column_bytes = new sqlite3_column_bytes_delegate(SQLiteBase.sqlite3_column_bytes_x86);
            sqlite3_column_table_name = new sqlite3_column_table_name_delegate(SQLiteBase.sqlite3_column_table_name_x86);
            sqlite3_finalize = new sqlite3_finalize_delegate(SQLiteBase.sqlite3_finalize_x86);
        }
        this.database = IntPtr.Zero;
    #endregion SQLlite3_86
    }
    public SQLiteBase(string baseName)
        : this()
    {
        this.OpenDatabase(baseName);
    }
    public void Close()
    {
        if (this.database != IntPtr.Zero)
        {
            sqlite3_close(this.database);
        }
    }
    public int ExecuteNonQuery(string query)
    {
        IntPtr ptr;
        _count = 0;
        IntPtr ptr2 = Marshal.StringToHGlobalAnsi(query);
        sqlite3_exec(this.database, ptr2, IntPtr.Zero, IntPtr.Zero, out ptr);
        Marshal.FreeHGlobal(ptr2);
        if (ptr != IntPtr.Zero)
        {
            throw new Exception("Error with executing non-query: \"" + query + "\"!\n" + PointerToString(sqlite3_errmsg(ptr)));
        }
        return 1;
    }
    public DataTable ExecuteQuery(string query)
    {
        IntPtr ptr;
        IntPtr ptr2;
        IntPtr ptr3 = Marshal.StringToHGlobalAnsi(query);
        sqlite3_prepare_v2(this.database, ptr3, query.Length, out ptr, out ptr2);
        Marshal.FreeHGlobal(ptr3);
        DataTable table = new DataTable();
        for (int i = this.ReadFirstRow(ptr, ref table); i == 100; i = this.ReadNextRow(ptr, ref table))
        {
        }
        sqlite3_finalize(ptr);
        return table;
    }
    public DataReader ExecuteReader(string query)
    {
        return new DataReader(this.database, query);
    }
    private static byte[] GetBlob(IntPtr statement, int column)
    {
        int length = sqlite3_column_bytes(statement, column);
        byte[] destination = new byte[length];
        Marshal.Copy(sqlite3_column_blob(statement, column), destination, 0, length);
        return destination;

    }
    public ArrayList GetTables()
    {
        string query = "SELECT name FROM sqlite_master WHERE type IN ('table','view') AND name NOT LIKE 'sqlite_%'UNION ALL SELECT name FROM sqlite_temp_master WHERE type IN ('table','view') ORDER BY 1";
        DataTable table = this.ExecuteQuery(query);
        ArrayList list = new ArrayList();
        foreach (DataRow row in table.Rows)
        {
            list.Add(row.ItemArray[0].ToString());
        }
        return list;
    }
    public void OpenDatabase(string baseName)
    {
        IntPtr fileName = Marshal.StringToHGlobalAnsi(baseName);
        if (sqlite3_open(fileName, out this.database) != 0)
        {
            this.database = IntPtr.Zero;
            Marshal.FreeHGlobal(fileName);
            throw new Exception("Error with opening database " + baseName + "!");
        }
    }
    private IntPtr StringToPointer(String str)
    {
        if (str == null)
        {
            return IntPtr.Zero;
        }
        else
        {
            Encoding encoding = Encoding.UTF8;
            Byte[] bytes = encoding.GetBytes(str);
            int length = bytes.Length + 1;
            IntPtr pointer = HeapAlloc(GetProcessHeap(), 0, (UInt32)length);
            Marshal.Copy(bytes, 0, pointer, bytes.Length);
            Marshal.WriteByte(pointer, bytes.Length, 0);
            return pointer;
        }
    }
    private static string PointerToString(IntPtr ptr)
    {
        if (ptr == IntPtr.Zero)
        {
            return null;
        }
        return Marshal.PtrToStringAnsi(ptr);
    }
    private int ReadFirstRow(IntPtr statement, ref DataTable table)
    {
        table = new DataTable("resultTable");
        if (sqlite3_step(statement) == 100)
        {
            int num2 = sqlite3_column_count(statement);
            string columnName = "";
            object[] values = new object[num2];
            for (int i = 0; i < num2; i++)
            {
                columnName = PointerToString(sqlite3_column_name(statement, i));
                switch (sqlite3_column_type(statement, i))
                {
                    case 1:
                        table.Columns.Add(columnName, typeof(long));
                        values[i] = sqlite3_column_int(statement, i);
                        break;

                    case 2:
                        table.Columns.Add(columnName, typeof(float));
                        values[i] = sqlite3_column_double(statement, i);
                        break;

                    case 3:
                        table.Columns.Add(columnName, typeof(string));
                        values[i] = PointerToString(sqlite3_column_text(statement, i));
                        break;

                    case 4:
                        table.Columns.Add(columnName, typeof(object));
                        values[i] = GetBlob(statement, i);
                        break;

                    default:
                        table.Columns.Add(columnName, typeof(string));
                        values[i] = "";
                        break;
                }
            }
            table.Rows.Add(values);
        }
        return sqlite3_step(statement);
    }
    private int ReadNextRow(IntPtr statement, ref DataTable table)
    {
        int num = sqlite3_column_count(statement);
        object[] values = new object[num];
        for (int i = 0; i < num; i++)
        {
            switch (sqlite3_column_type(statement, i))
            {
                case 1:
                    values[i] = sqlite3_column_int(statement, i);
                    break;

                case 2:
                    values[i] = sqlite3_column_double(statement, i);
                    break;

                case 3:
                    values[i] = PointerToString(sqlite3_column_text(statement, i));
                    break;

                case 4:
                    values[i] = GetBlob(statement, i);
                    break;

                default:
                    values[i] = "";
                    break;
            }
        }
        table.Rows.Add(values);
        return sqlite3_step(statement);
    }
    public class DataReader : IDisposable
    {
        private List<string> _columnNames;
        private ArrayList _columnValues;
        private bool _isFirstRow;
        private IntPtr _statement;

        internal DataReader(IntPtr database, string query)
        {
            IntPtr ptr;
            this._columnValues = new ArrayList();
            this._columnNames = new List<string>();
            IntPtr ptr2 = Marshal.StringToHGlobalAnsi(query);
            SQLiteBase.sqlite3_prepare_v2(database, ptr2, query.Length, out this._statement, out ptr);
            Marshal.FreeHGlobal(ptr2);
        }
        public void Close()
        {
            SQLiteBase.sqlite3_finalize(this._statement);
        }
        public void Dispose()
        {
            this.Close();
        }
        public long GetInt32(int index)
        {
            return (long)Convert.ToInt32(this._columnValues[index]);
        }
        public long GetInt64(int index)
        {
            return Convert.ToInt64(this._columnValues[index]);
        }
        public string GetString(int index)
        {
            return Convert.ToString(this._columnValues[index]);
        }
        public object GetValue(int index)
        {
            return this._columnValues[index];
        }
        public bool Read()
        {
            if (SQLiteBase.sqlite3_step(this._statement) == 100)
            {
                int num3;
                int num2 = SQLiteBase.sqlite3_column_count(this._statement);
                this._columnValues.Capacity = num2;
                this._columnValues.Clear();
                if (this._isFirstRow)
                {
                    this._columnNames.Capacity = num2;
                    this._columnNames.Clear();
                    for (num3 = 0; num3 < num2; num3++)
                    {
                        this._columnNames.Add(Marshal.PtrToStringAnsi(SQLiteBase.sqlite3_column_name(this._statement, num3)));
                    }
                    this._isFirstRow = false;
                }
                for (num3 = 0; num3 < num2; num3++)
                {
                    switch (SQLiteBase.sqlite3_column_type(this._statement, num3))
                    {
                        case 1:
                            this._columnValues.Add(SQLiteBase.sqlite3_column_int(this._statement, num3));
                            break;

                        case 2:
                            this._columnValues.Add(SQLiteBase.sqlite3_column_double(this._statement, num3));
                            break;

                        case 3:
                            this._columnValues.Add(Marshal.PtrToStringAnsi(SQLiteBase.sqlite3_column_text(this._statement, num3)));
                            break;

                        case 4:
                            this._columnValues.Add(SQLiteBase.GetBlob(this._statement, num3));
                            break;

                        default:
                            this._columnValues[num3] = "";
                            break;
                    }
                }
                return true;
            }
            return false;
        }
        public object this[int i]
        {
            get
            {
                return this._columnValues[i];
            }
        }
        public object this[string name]
        {
            get
            {
                return this._columnValues[this._columnNames.IndexOf(name)];
            }
        }
    }
    public enum SQLiteDataTypes
    {
        INT = 1,
        FLOAT = 2,
        TEXT = 3,
        BLOB = 4,
        NULL = 5
    }
    #region DLL_Imports
    [DllImport("kernel32")]
    private extern static IntPtr GetProcessHeap();
    [DllImport("kernel32")]
    private extern static IntPtr HeapAlloc(IntPtr heap, UInt32 flags, UInt32 bytes);
    [DllImport("sqlite3_x64", EntryPoint = "sqlite3_close")]
    private static extern int sqlite3_close_x64(IntPtr database);
    [DllImport("sqlite3_x86", EntryPoint = "sqlite3_close")]
    private static extern int sqlite3_close_x86(IntPtr database);
    [DllImport("sqlite3_x64", EntryPoint = "sqlite3_column_blob")]
    private static extern IntPtr sqlite3_column_blob_x64(IntPtr statement, int columnNumber);
    [DllImport("sqlite3_x86", EntryPoint = "sqlite3_column_blob")]
    private static extern IntPtr sqlite3_column_blob_x86(IntPtr statement, int columnNumber);
    [DllImport("sqlite3_x64", EntryPoint = "sqlite3_column_bytes")]
    private static extern int sqlite3_column_bytes_x64(IntPtr statement, int columnNumber);
    [DllImport("sqlite3_x86", EntryPoint = "sqlite3_column_bytes")]
    private static extern int sqlite3_column_bytes_x86(IntPtr statement, int columnNumber);
    [DllImport("sqlite3_x64", EntryPoint = "sqlite3_column_count")]
    private static extern int sqlite3_column_count_x64(IntPtr statement);
    [DllImport("sqlite3_x86", EntryPoint = "sqlite3_column_count")]
    private static extern int sqlite3_column_count_x86(IntPtr statement);
    [DllImport("sqlite3_x64", EntryPoint = "sqlite3_column_double")]
    private static extern double sqlite3_column_double_x64(IntPtr statement, int columnNumber);
    [DllImport("sqlite3_x86", EntryPoint = "sqlite3_column_double")]
    private static extern double sqlite3_column_double_x86(IntPtr statement, int columnNumber);
    [DllImport("sqlite3_x64", EntryPoint = "sqlite3_column_int64")]
    private static extern long sqlite3_column_int_x64(IntPtr statement, int columnNumber);
    [DllImport("sqlite3_x86", EntryPoint = "sqlite3_column_int64")]
    private static extern long sqlite3_column_int_x86(IntPtr statement, int columnNumber);
    [DllImport("sqlite3_x64", EntryPoint = "sqlite3_column_name")]
    private static extern IntPtr sqlite3_column_name_x64(IntPtr statement, int columnNumber);
    [DllImport("sqlite3_x86", EntryPoint = "sqlite3_column_name")]
    private static extern IntPtr sqlite3_column_name_x86(IntPtr statement, int columnNumber);
    [DllImport("sqlite3_x64", EntryPoint = "sqlite3_column_table_name")]
    private static extern IntPtr sqlite3_column_table_name_x64(IntPtr statement, int columnNumber);
    [DllImport("sqlite3_x86", EntryPoint = "sqlite3_column_table_name")]
    private static extern IntPtr sqlite3_column_table_name_x86(IntPtr statement, int columnNumber);
    [DllImport("sqlite3_x64", EntryPoint = "sqlite3_column_text")]
    private static extern IntPtr sqlite3_column_text_x64(IntPtr statement, int columnNumber);
    [DllImport("sqlite3_x86", EntryPoint = "sqlite3_column_text")]
    private static extern IntPtr sqlite3_column_text_x86(IntPtr statement, int columnNumber);
    [DllImport("sqlite3_x64", EntryPoint = "sqlite3_column_type")]
    private static extern int sqlite3_column_type_x64(IntPtr statement, int columnNumber);
    [DllImport("sqlite3_x86", EntryPoint = "sqlite3_column_type")]
    private static extern int sqlite3_column_type_x86(IntPtr statement, int columnNumber);
    [DllImport("sqlite3_x64", EntryPoint = "sqlite3_errmsg")]
    private static extern IntPtr sqlite3_errmsg_x64(IntPtr database);
    [DllImport("sqlite3_x86", EntryPoint = "sqlite3_errmsg")]
    private static extern IntPtr sqlite3_errmsg_x86(IntPtr database);
    [DllImport("sqlite3_x64", EntryPoint = "sqlite3_exec")]
    private static extern int sqlite3_exec_x64(IntPtr database, IntPtr query, IntPtr callback, IntPtr arguments, out IntPtr error);
    [DllImport("sqlite3_x86", EntryPoint = "sqlite3_exec")]
    private static extern int sqlite3_exec_x86(IntPtr database, IntPtr query, IntPtr callback, IntPtr arguments, out IntPtr error);
    [DllImport("sqlite3_x64", EntryPoint = "sqlite3_finalize")]
    private static extern int sqlite3_finalize_x64(IntPtr handle);
    [DllImport("sqlite3_x86", EntryPoint = "sqlite3_finalize")]
    private static extern int sqlite3_finalize_x86(IntPtr handle);
    [DllImport("sqlite3_x64", EntryPoint = "sqlite3_open")]
    private static extern int sqlite3_open_x64(IntPtr fileName, out IntPtr database);
    [DllImport("sqlite3_x86", EntryPoint = "sqlite3_open")]
    private static extern int sqlite3_open_x86(IntPtr fileName, out IntPtr database);
    [DllImport("sqlite3_x64", EntryPoint = "sqlite3_prepare_v2")]
    private static extern int sqlite3_prepare_v2_x64(IntPtr database, IntPtr query, int length, out IntPtr statement, out IntPtr tail);
    [DllImport("sqlite3_x86", EntryPoint = "sqlite3_prepare_v2")]
    private static extern int sqlite3_prepare_v2_x86(IntPtr database, IntPtr query, int length, out IntPtr statement, out IntPtr tail);
    [DllImport("sqlite3_x64", EntryPoint = "sqlite3_step")]
    private static extern int sqlite3_step_x64(IntPtr statement);
    [DllImport("sqlite3_x86", EntryPoint = "sqlite3_step")]
    private static extern int sqlite3_step_x86(IntPtr statement);
    #endregion DLL_Imports
}