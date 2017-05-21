using System;
using Zags.OrganizationService.Application.Adapters.DataAccess;
using Zags.OrganizationService.Domain;
using Zags.OrganizationService.Domain.Models;
using Zags.Utilities;
using Zags.Utilities.Functional;
using System.Linq;

namespace Zags.OrganizationService.Application.Ports.ViewModelRetrievers
{
    public class OrganizationModelRetriever
    {
        private readonly IOrganizationRepository _repository;
        private readonly string _hostName;

        public OrganizationModelRetriever(string hostName, IOrganizationRepository repository)
        {
            _hostName = hostName;
            _repository = repository;
        }

        public OrganizationListModel RetrieveOrganizations()
        {
            var pms = _repository.FindAll();
            var pmListModel = new OrganizationListModel(pms, _hostName);
            return pmListModel;
        }

        public Either<Error, OrganizationModel> RetrieveOrganization(int pmId)
        {
            Option<Organization> organization = _repository.FindById(pmId);
            if (organization.IsSome)
            {
                var pmModel = new OrganizationModel(organization.AsEnumerable().First(), _hostName);
                return pmModel;
            }
            else
            {
                return F.Error("Customer with such Id is not found: " + pmId);
            }

        }
    }
}
