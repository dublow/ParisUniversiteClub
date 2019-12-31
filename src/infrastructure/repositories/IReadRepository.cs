using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using infrastructure.tables;

namespace infrastructure.repositories
{
    public interface IReadRepository
    {
        T Get<T>(Expression<Func<T, bool>> predicate) where T : ITable, new();
        IEnumerable<T> Find<T>(Expression<Func<T, bool>> predicate) where T : ITable, new();
    }
}