using System.Data.Entity;
using Zags.OrganizationService.Domain;

namespace Zags.OrganizationService.Application.Adapters.DataAccess
{
    public class OrganisationDbContext : DbContext
    {
        public OrganisationDbContext() : 
            base ("DefaultConnection")
        {
            Database.SetInitializer<OrganisationDbContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("Organization");
            modelBuilder.Entity<Organization>().ToTable("Organization");
            modelBuilder.Entity<Address>().ToTable("Address");
            modelBuilder.Entity<Iban>().ToTable("Iban");
        }
    }
}
