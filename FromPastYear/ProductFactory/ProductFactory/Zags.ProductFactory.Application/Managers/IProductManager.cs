using System.Collections.Generic;
using Zags.ProductFactory.Domain;
using Zags.Utility.Errors;
using Zags.Utility.Functional;

namespace Zags.ProductFactory.Application.Managers
{
    public interface IProductManager
    {
        Either<IList<Error>, Product> CreateProduct(Product product);

        Either<IList<Error>, Unit> UpdateProduct(Product product);

        Either<IList<Error>, Unit> DeleteProduct(int id);

        Either<Error, Product> GetProductById(int id);

    }
}
