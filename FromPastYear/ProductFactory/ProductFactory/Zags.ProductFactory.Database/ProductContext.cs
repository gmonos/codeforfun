using Microsoft.EntityFrameworkCore;
using Zags.ProductFactory.Domain;

namespace Zags.ProductFactory.Database
{
    public class ProductDbContext: DbContext
    {
       

        public ProductDbContext(DbContextOptions<ProductDbContext> options)
           : base(options)
        {
           
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Coverage> Coverages { get; set; }
        public DbSet<Pack> Packs { get; set; }

    }
}
