using System.Data.Entity;
using Zags.OrganizationService.Domain;

namespace Zags.OrganizationService.DB
{
    public class DefaultConnection : DbContext
    {
        public DbSet<Organization> PersonneMorales { get; set; }
    }
}
