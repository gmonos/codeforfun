using System;
using System.Collections.Generic;
using System.Data;
using Zags.DataAccess.Dapper;
using Zags.ProductFactory.Domain.Retrievers;

namespace Zags.ProductFactory.Database.Retrievers
{
    public class ProductRetriever : GenericDapperRetriever, IProductRetriever
    {
        public ProductRetriever(string connectionString) : base(connectionString)
        {
        }

        public bool CheckIsAttached(int productId)
        {
            return false;
        }

        public IEnumerable<ProductQueryResult> Search(string productName)
        {
            string sql = "Select Id, Name from Products where Name like @pName";

            return base.Search<ProductQueryResult>(sql, new { pName = productName});
        }
    }
}
