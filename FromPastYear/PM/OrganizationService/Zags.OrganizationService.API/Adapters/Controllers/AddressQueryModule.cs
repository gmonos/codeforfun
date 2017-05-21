using Nancy;
using Nancy.Security;
using Zags.DataAccess.Dapper;
using Zags.OrganizationService.Application;
using Zags.OrganizationService.Application.Adapters.DataAccess;
using Zags.OrganizationService.Application.Ports.ViewModelRetrievers;
using Zags.OrganizationService.Domain;

namespace Zags.OrganizationService.API.Adapters.Controllers
{
    public class AddressQueryModule : NancyModule
    {
        private readonly IDapperGenericRepository<Address> _repository;

        public AddressQueryModule(IDapperGenericRepository<Address> repository)
        {
            _repository = repository;

            Get["/Address"] = parameters =>
            {
                return _repository.FindAll();
            };
        }
    }
}
