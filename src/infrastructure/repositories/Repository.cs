using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using infrastructure.tables;
using SQLite;

namespace infrastructure.repositories
{
    public class Repository : IWriteRepository, IReadRepository
    {
        private readonly SQLiteConnection _sqLiteConnection;

        public Repository(SQLiteConnection sqLiteConnection)
        {
            _sqLiteConnection = sqLiteConnection;
        }

        public void Add<T>(T entity) where T : ITable
        {
            _sqLiteConnection.Insert(entity);
        }

        public void Update<T>(T entity) where T : ITable
        {
            _sqLiteConnection.Update(entity);
        }

        public void Delete<T>(T entity) where T : ITable
        {
            _sqLiteConnection.Delete(entity);
        }

        public T Get<T>(Expression<Func<T, bool>> predicate) where T : ITable, new()
        {
            return _sqLiteConnection.Get(predicate);
        }

        public IEnumerable<T> Find<T>(Expression<Func<T, bool>> predicate) where T : ITable, new()
        {
            return _sqLiteConnection.Table<T>().Where(predicate);
        }
    }
}
