using System;
using Zags.OrganizationService.IntegrationTests;
using Xunit;

namespace Zags.ProductFactory.Application.Tests
{
    public class InMemoryDatabaseOrmLiteFixture : IDisposable
    {
        public InMemoryDatabaseOrmLite InMemoryDB { get; private set; }

        public InMemoryDatabaseOrmLiteFixture()
        {
            // This code run only one time
            InMemoryDB = new InMemoryDatabaseOrmLite();
        }
        public void Dispose()
        {
            // Here is run only one time too
            InMemoryDB.Dispose();
        }
    }

    [CollectionDefinition("MemoryDataBase Collection")]
    public class InMemoryDatabaseCollection : ICollectionFixture<InMemoryDatabaseOrmLiteFixture>
    {
    }
}
