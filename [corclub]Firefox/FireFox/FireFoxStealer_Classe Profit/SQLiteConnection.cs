using System;
using System.Data;
using System.Runtime.CompilerServices;
using SuperStealer;

public class SQLiteConnection : IDisposable
{
    private SQLiteCommand _cmd;
    private string _dataPath;
    internal SQLiteBase Base;
    private ConnectionState State;
    public SQLiteConnection(string connStr)
    {
        foreach (string str in connStr.Split(new char[] { ';' }))
        {
            if (str.StartsWith("Data Source", StringComparison.CurrentCultureIgnoreCase))
            {
                this._dataPath = str.Substring(str.IndexOf('=') + 1).Replace("\"", "");
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
        if (this._cmd == null)
        {
            this._cmd = new SQLiteCommand(this);
        }
        return this._cmd;
    }
    public void Dispose()
    {
        this.Close();
    }
    public void Open()
    {

        if (this.State != ConnectionState.Open)
        {
            this.Base = new SQLiteBase(this._dataPath);
            this.State = ConnectionState.Open;
        }
    }
}