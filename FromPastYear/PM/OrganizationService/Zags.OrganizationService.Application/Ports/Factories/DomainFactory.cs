using Zags.OrganizationService.Application.Ports.Commands;
using Zags.OrganizationService.Domain;

namespace Zags.OrganizationService.Application.Ports.Factories
{
    public class DomainFactory : IDomainFactory
    {
        public virtual Organization CreateOrganizationDomainObject(AddOrganizationCommand addOrganizationCommand)
        {
            return new Organization(addOrganizationCommand.OrganizationId, addOrganizationCommand.Reference,
                                        addOrganizationCommand.RaisonSociale, addOrganizationCommand.FormeJuridique,
                                        addOrganizationCommand.Effectif, addOrganizationCommand.SIRET, addOrganizationCommand.CodeNAF, addOrganizationCommand.IdentifiantConventionCollective);
        }
    }
}
