using System;
using System.Data;

namespace infrastructure.connections
{
    public interface IConnection
    {
        void Execute(Action<IDbConnection> act);
    }
}
