using infrastructure.tables;

namespace infrastructure.repositories
{
    public interface IWriteRepository
    {
        void Add<T>(T entity) where T : ITable;
        void Update<T>(T entity) where T : ITable;
        void Delete<T>(T entity) where T : ITable;
    }
}
