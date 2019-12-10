using System;
using System.Data;
using System.Data.SQLite;
using infrastructure.configurations;

namespace infrastructure.connections
{
    public class SqlLiteConnection : IConnection
    {
        private readonly string _connectionString;

        public SqlLiteConnection(IDbConfiguration dbConfiguration)
        {
            _connectionString = dbConfiguration.ConnectionString;
        }

        public void Execute(Action<IDbConnection> act)
        {
            using var cnx = new SQLiteConnection(_connectionString);
            cnx.Open();
            act(cnx);
        }
    }
}
