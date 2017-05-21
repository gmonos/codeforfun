using paramore.brighter.commandprocessor;
using paramore.brighter.commandprocessor.Logging;
using Zags.OrganizationService.Application.Ports.Events;

namespace Zags.OrganizationService.Legacy.Ports.Handlers
{
    public class OrganizationAddedLegacyEventHandler : RequestHandler<OrganizationAddedEvent>
    {
        public OrganizationAddedLegacyEventHandler(ILog logger) : base(logger)
        {
        }

        public override OrganizationAddedEvent Handle(OrganizationAddedEvent organizationAddedEvent)
        {
            //Call Legacy Repository
            return base.Handle(organizationAddedEvent);
        }
    }
}
