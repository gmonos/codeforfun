using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zags.DataAccess;
using Zags.DataAccess.EF;
using Zags.Domain;
using Zags.OrganizationService.Domain;

namespace Zags.OrganizationService.API.Adapters.Controllers
{
    public class EFOrganizationQueryModule : NancyModule
    {
        private readonly IGenericRepository<Organization> _repository;

        public EFOrganizationQueryModule(IGenericRepository<Organization> repository)
        {
            _repository = repository;

            Get["/EFOrganization"] = parameters =>
            {
                return repository.FindBy(new PaginationInfo(), null, null);
            };
        }
    }
}
