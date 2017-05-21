using Nancy;
using Nancy.Responses.Negotiation;
using System;
using System.Collections.Generic;
using Zags.DataAccess;
using Zags.Domain;
using Zags.OrganizationService.Domain;

namespace Zags.OrganizationService.API.Adapters.Controllers
{
    public class EFAddressQueryModule : NancyModule
    {
        private readonly IGenericRepository<Address> _repository;

        public EFAddressQueryModule(IGenericRepository<Address> repository)
        {
            _repository = repository;
            Get["/EFAddress"] = parameters =>
            {
                return QueryModuleUtilities<Address>.FindBy(Request, Negotiate, _repository);
            };
        }
    }
}
