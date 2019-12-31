using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using infrastructure.providers;
using infrastructure.tables;

namespace infrastructure.repositories
{
    public class Repository : IWriteRepository, IReadRepository
    {
        private readonly IConnection _connection;

        public Repository(IConnection sqLiteConnection)
        {
            _connection = sqLiteConnection;
        }

        public void Add<T>(T entity) where T : ITable
        {
            using var cnx = _connection.Create();
            cnx.Insert(entity);
        }

        public void Update<T>(T entity) where T : ITable
        {
            using var cnx = _connection.Create();
            cnx.Update(entity);
        }

        public void Delete<T>(T entity) where T : ITable
        {
            using var cnx = _connection.Create();
            cnx.Delete(entity);
        }

        public T Get<T>(Expression<Func<T, bool>> predicate) where T : ITable, new()
        {
            using var cnx = _connection.Create();
            return cnx.Get(predicate);
        }

        public IEnumerable<T> Find<T>(Expression<Func<T, bool>> predicate) where T : ITable, new()
        {
            using var cnx = _connection.Create();
            return cnx.Table<T>().Where(predicate);
        }
    }
}
