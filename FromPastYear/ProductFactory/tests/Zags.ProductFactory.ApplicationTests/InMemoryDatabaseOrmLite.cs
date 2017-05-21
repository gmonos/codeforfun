using System;
using System.Collections.Generic;
using System.Text;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Sqlite;
using System.Data;

namespace Zags.OrganizationService.IntegrationTests
{
    public class InMemoryDatabaseOrmLite : IDisposable
    {
        public InMemoryDatabaseOrmLite()
        {
        }

        private readonly OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(
            connectionString: ":memory:",
            dialectProvider: SqliteOrmLiteDialectProvider.Instance,
            setGlobalDialectProvider: false);

        private IDbConnection _inMemoryDbConnection;
        public IDbConnection InMemoryDbConnection
        {
            get
            {
                if (_inMemoryDbConnection == null)
                    _inMemoryDbConnection = OpenConnection();

                return _inMemoryDbConnection;
            }
        }

        private IDbConnection OpenConnection()
        {
            OrmLiteConfig.DialectProvider = dbFactory.DialectProvider;
            return dbFactory.OpenDbConnection();
        }

        public void Add<T>(IEnumerable<T> items)
        {
            using (var db = this.InMemoryDbConnection)
            {
                db.CreateTableIfNotExists<T>();
                foreach(var item in items)
                {
                    db.Insert(item);
                }
            }
        }

        public void Dispose()
        {
            InMemoryDbConnection.Dispose();
        }
    }
}
