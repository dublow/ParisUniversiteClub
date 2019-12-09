using System;
using System.Data;
using System.Data.SQLite;

namespace infrastructure.connections
{
    public class SqlLiteConnection : IConnection
    {
        private readonly string _connectionString;

        public SqlLiteConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Execute(Action<IDbConnection> act)
        {
            using (var cnx = new SQLiteConnection(_connectionString))
            {
                act(cnx);
            }
        }
    }
}
