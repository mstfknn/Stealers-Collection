namespace SuperStealer
{
    using System;
    using System.Runtime.CompilerServices;

    public class SQLiteCommand : IDisposable
    {
        private SQLiteConnection _con;
        [CompilerGenerated]
        private string <CommandText>k__BackingField;

        public SQLiteCommand(SQLiteConnection connection)
        {
            this._con = connection;
        }

        public SQLiteCommand(string commandText, SQLiteConnection connnecion)
        {
            this.CommandText = commandText;
            this._con = connnecion;
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

        public string CommandText
        {
            [CompilerGenerated]
            get
            {
                return this.<CommandText>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<CommandText>k__BackingField = value;
            }
        }
    }
}

