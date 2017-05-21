using Zags.OrganizationService.Application.Axa.Ports.Commands;
using Zags.OrganizationService.Application.Axa.Ports.Domain;
using Zags.OrganizationService.Application.Ports.Commands;
using Zags.OrganizationService.Application.Ports.Factories;
using Zags.OrganizationService.Domain;

namespace Zags.OrganizationService.Application.Axa.Ports.Factories
{

    public class AxaDomainFactory : DomainFactory
    {
        public override Organization CreateOrganizationDomainObject(AddOrganizationCommand addOrganizationCommand)
        {
            Organization pm = base.CreateOrganizationDomainObject(addOrganizationCommand);

            pm.Extension = new OrganizationAxa() {
                OrganizationId = addOrganizationCommand.OrganizationId,
                                         NumAbonne = ((AxaAddOrganizationCommandExtension)addOrganizationCommand.Extension).NumAbonne };

            return pm;
        }
    }
}
