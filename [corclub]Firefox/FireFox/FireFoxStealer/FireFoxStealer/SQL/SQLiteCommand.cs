using System;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

public class SQLiteCommand : IDisposable
{
    private SQLiteConnection DT;
    SqlCommand command = new SqlCommand();
    public string CommandText { get; set; }
    private string CommandTextBackingField;
    public SQLiteCommand(SQLiteConnection connection)
    {
        DT = connection;
    }
    public SQLiteCommand(string commandText, SQLiteConnection connnecion)
    {
        DT = connnecion;
    }
    public void Close()
    {
        this.DT.Close();
    }
    public void Dispose()
    {
        this.Close();
    }
    public int ExecuteNonQuery()
    {
        return this.DT.Base.ExecuteNonQuery(this.CommandText);
    }
    public SQLiteBase.DataReader ExecuteReader()
    {
        return this.DT.Base.ExecuteReader(this.CommandText);
    }
    public object ExecuteScalar()
    {
        return this.DT.Base.ExecuteQuery(this.CommandText).Rows[0][0];
    }
    public string CommandTextTwo
    {
        [CompilerGenerated]
        get
        {
            return this.CommandTextBackingField;
        }
        [CompilerGenerated]
        set
        {
            this.CommandTextBackingField = value;
        }
    }
}