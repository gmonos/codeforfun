using Zags.DataAccess.EF;
using Zags.Domain.Repositories;
using Zags.ProductFactory.Domain;
using Zags.Utility.Functional;

namespace Zags.ProductFactory.Database.Repositories
{
    public class ProductRepository : GenericEFRepository<Product, ProductDbContext>, IRepository<Product>
    {
        public ProductRepository(ProductDbContext context) : base(context)
        {
        }


        public override Option<Product> GetById(int id)
        {
                return base.GetDeepById(id, x => x.Coverages, x => x.Packs);   
        }

        override public void Delete(Product product)
        {
            foreach (var coverage in product.Coverages)
            {
                base.Delete<Coverage>(coverage);
            }

            foreach (var pack in product.Packs)
            {
                base.Delete<Pack>(pack);
            }

            base.Delete(product);
        }


       
    }
}
