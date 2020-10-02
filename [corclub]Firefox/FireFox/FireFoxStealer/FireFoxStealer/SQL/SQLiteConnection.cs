using System;
using System.Data;

public class SQLiteConnection : IDisposable
{
    private SQLiteCommand Com;
    private string DPath;
    internal SQLiteBase Base;
    private string CommandTextBackingField;
    private ConnectionState State;
    public SQLiteConnection(string connStr)
    {
        foreach (string str in connStr.Split(new char[] { ';' }))
        {
            if (str.StartsWith("Data Source", StringComparison.CurrentCultureIgnoreCase))
            {
                this.DPath = str.Substring(str.IndexOf('=') + 1).Replace("\"", "");
                break;
            }
        }
        this.State = ConnectionState.Closed;
    }
    public void Close()
    {
        this.Base.Close();
        this.State = ConnectionState.Closed;
    }
    public SQLiteCommand CreateCommand()
    {
        if (this.Com == null)
        {
            this.Com = new SQLiteCommand(this);
        }
        return this.Com;
    }
    public void Dispose()
    {
        this.Close();
    }
    public void Open()
    {
        if (this.State != ConnectionState.Open){
            this.Base = new SQLiteBase(this.DPath);
            this.State = ConnectionState.Open;
        }
    }
    protected string CommandTextTwo
    {
        get
        {
            return this.CommandTextBackingField;
        }
        set
        {
            this.CommandTextBackingField = value;
        }
    }
}