namespace SuperStealer
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Data;
    using System.Data.SqlClient;

    public class SQLiteCommand : IDisposable
    {
        private SQLiteConnection _con;
        SqlCommand command = new SqlCommand();
        public string CommandText {get; set;}
        public SQLiteCommand(SQLiteConnection connection)
        {
            this._con = connection;
        }

        public SQLiteCommand(string commandText, SQLiteConnection connnecion)
        {
            command = CommandText;
            _con = connnecion;
        }

        public void Dispose()
        {
        }

        public int ExecuteNonQuery()
        {
            return this._con.Base.ExecuteNonQuery(this.CommandText);
        }

        public SQLiteBase.DataReader ExecuteReader()
        {
            return this._con.Base.ExecuteReader(this.CommandText);
        }

        public object ExecuteScalar()
        {
            return this._con.Base.ExecuteQuery(this.CommandText).Rows[0][0];
        }
    }
}