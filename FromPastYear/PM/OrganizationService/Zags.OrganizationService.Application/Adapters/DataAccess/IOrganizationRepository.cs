using Zags.DataAccess.Dapper;
using Zags.OrganizationService.Domain;
using Zags.Utilities.Functional;

namespace Zags.OrganizationService.Application.Adapters.DataAccess
{
    public interface IOrganizationRepository : IDapperGenericRepository<Organization>
    {
        Option<Organization> GetDeepById(int id);
    }
}
