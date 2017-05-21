using System.Transactions;
using paramore.brighter.commandprocessor;
using paramore.brighter.commandprocessor.logging.Attributes;
using paramore.brighter.commandprocessor.Logging;
using paramore.brighter.commandprocessor.policy.Attributes;
using Zags.OrganizationService.Application.Adapters.DataAccess;
using Zags.OrganizationService.Application.Ports.Commands;
using Zags.OrganizationService.Application.Ports.Events;
using Zags.OrganizationService.Application.Ports.Factories;
using Zags.OrganizationService.Domain;
using Zags.Application.Brighter.Handler;

namespace Zags.OrganizationService.Application.Ports.Handlers
{
    public class AddOrganizationCommandHandler : RequestHandler<AddOrganizationCommand>
    {
        
        private readonly IAmACommandProcessor _commandProcessor;
        private readonly IOrganizationRepository _repository;
        private readonly IDomainFactory _domainFactory;

        public AddOrganizationCommandHandler(IAmACommandProcessor commandProcessor, IOrganizationRepository repository, IDomainFactory domainFactory, ILog logger) : base(logger)
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
            Logger.Info("Normal AddOrganizationCommand handles command");
            using (TransactionScope scope = new TransactionScope())
            {

                Organization pm = _domainFactory.CreateOrganizationDomainObject(addOrganizationCommand);

                //Save in the local repository
                _repository.Insert(pm);

                //Assign Id to the command
                addOrganizationCommand.OrganizationId = pm.Id;

                scope.Complete();
            }

            //Publishes event synchronously to others subscribers
            _commandProcessor.Publish(new OrganizationAddedEvent());
            return base.Handle(addOrganizationCommand);
        }
    }
}
