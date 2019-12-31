using infrastructure.repositories;
using SQLite;

namespace infrastructure.providers
{
    public class Connection : IConnection
    {
        private readonly string _cnx;

        public Connection(string cnx)
        {
            _cnx = cnx;
        }

        public SQLiteConnection Create()
        {
            return new SQLiteConnection(_cnx);
        }
    }
}