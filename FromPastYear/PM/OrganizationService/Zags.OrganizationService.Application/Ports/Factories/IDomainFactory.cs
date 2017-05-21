using Zags.OrganizationService.Application.Ports.Commands;
using Zags.OrganizationService.Domain;

namespace Zags.OrganizationService.Application.Ports.Factories
{
    public interface IDomainFactory
    {
        Organization CreateOrganizationDomainObject(AddOrganizationCommand addOrganizationCommand);

    }
}