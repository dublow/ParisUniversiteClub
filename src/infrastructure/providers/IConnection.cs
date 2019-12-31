using SQLite;

namespace infrastructure.providers
{
    public interface IConnection
    {
        SQLiteConnection Create();
    }
}