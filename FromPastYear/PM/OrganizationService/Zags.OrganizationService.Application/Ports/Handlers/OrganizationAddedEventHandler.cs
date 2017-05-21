using paramore.brighter.commandprocessor;
using paramore.brighter.commandprocessor.Logging;
using Zags.OrganizationService.Application.Ports.Events;

namespace Zags.OrganizationService.Application.Ports.Handlers
{
    public class OrganizationAddedEventHandler : RequestHandler<OrganizationAddedEvent>
    {
        public OrganizationAddedEventHandler(ILog logger) : base(logger)
        {
        }

        public override OrganizationAddedEvent Handle(OrganizationAddedEvent organizationAddedEvent)
        {
            //Call ViewDenormalizer
            return base.Handle(organizationAddedEvent);
        }
    }
}
