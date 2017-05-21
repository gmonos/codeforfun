using System.Collections.Generic;
using Zags.Domain.Repositories;

namespace Zags.ProductFactory.Domain.Retrievers
{
    public interface IProductRetriever : IRetriever
    {
        IEnumerable<ProductQueryResult> Search(string productName);

        bool CheckIsAttached(int productId);
    }
}
