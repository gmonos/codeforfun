using paramore.brighter.commandprocessor;
using paramore.brighter.commandprocessor.logging.Attributes;
using paramore.brighter.commandprocessor.Logging;
using paramore.brighter.commandprocessor.policy.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Zags.Application.Brighter.Handler;
using Zags.DataAccess;
using Zags.DataAccess.EF;
using Zags.OrganizationService.Application.Adapters.DataAccess;
using Zags.OrganizationService.Application.Ports.Commands;
using Zags.OrganizationService.Application.Ports.Events;
using Zags.OrganizationService.Application.Ports.Factories;
using Zags.OrganizationService.Domain;

namespace Zags.OrganizationService.Application.Ports.Handlers
{
    public class AddEFOrganisationCommandHandler : RequestHandler<AddOrganizationCommand>
    {
        private readonly IAmACommandProcessor _commandProcessor;
        private readonly IGenericRepository<Organization> _repository;
        private readonly IDomainFactory _domainFactory;

        public AddEFOrganisationCommandHandler(IAmACommandProcessor commandProcessor, IGenericRepository<Organization> repository, IDomainFactory domainFactory, ILog logger) : base(logger)
        {
            _commandProcessor = commandProcessor;
            _repository = repository;
            _domainFactory = domainFactory;
        }

        [RequestLogging(step: 1, timing: HandlerTiming.Before)]
        [Validation(step: 2, timing: HandlerTiming.Before)]
        [UsePolicy(paramore.brighter.commandprocessor.CommandProcessor.RETRYPOLICY, step: 3)]
        public override AddOrganizationCommand Handle(AddOrganizationCommand addOrganizationCommand)
        {
            Logger.Info("EFAddOrganizationCommand handles command");
            Organization pm = _domainFactory.CreateOrganizationDomainObject(addOrganizationCommand);

            //Save in the local repository
            _repository.Insert(pm);

            //Assign Id to the command
            addOrganizationCommand.OrganizationId = pm.Id;

            //Publishes event to others subscribers
            _commandProcessor.Publish(new OrganizationAddedEvent());
            return base.Handle(addOrganizationCommand);
        }
    }
}
