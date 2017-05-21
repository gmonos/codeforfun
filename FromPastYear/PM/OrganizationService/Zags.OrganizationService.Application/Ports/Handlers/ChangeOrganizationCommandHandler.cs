using System;
using System.Transactions;
using paramore.brighter.commandprocessor;
using paramore.brighter.commandprocessor.logging.Attributes;
using paramore.brighter.commandprocessor.Logging;
using paramore.brighter.commandprocessor.policy.Attributes;
using Zags.OrganizationService.Application.Adapters.DataAccess;
using Zags.OrganizationService.Application.Ports.Commands;
using Zags.OrganizationService.Application.Ports.Factories;
using Zags.Application.Brighter.Handler;
using Zags.OrganizationService.Domain;
using Zags.Domain.Tracking;

namespace Zags.OrganizationService.Application.Ports.Handlers
{
    public class ChangeOrganizationCommandHandler : RequestHandler<ChangeOrganizationCommand>
    {
        private readonly IAmACommandProcessor _commandProcessor;
        private readonly IOrganizationRepository _repository;
        private readonly IDomainFactory _domainFactory;
        private readonly IDomainTracking<Organization> _domainTracking;

        public ChangeOrganizationCommandHandler(IAmACommandProcessor commandProcessor, 
                                        IOrganizationRepository repository, 
                                        IDomainFactory domainFactory,
                                        IDomainTracking<Organization> domainTracking, 
                                        ILog logger) : base(logger)
        {
            _commandProcessor = commandProcessor;
            _repository = repository;
            _domainFactory = domainFactory;
            _domainTracking = domainTracking;
        }

        [RequestLogging(step: 1, timing: HandlerTiming.Before)]
        [Validation(step: 2, timing: HandlerTiming.Before)]
        [UsePolicy(paramore.brighter.commandprocessor.CommandProcessor.RETRYPOLICY, step: 3)]
        public override ChangeOrganizationCommand Handle(ChangeOrganizationCommand changeOrganizationCommand)
        {
            Logger.Info("ChangeOrganizationCommand handles command");

            var personneMorale = _repository.GetDeepById(changeOrganizationCommand.OrganizationId).Match(
                   Some: pm =>
                   {
                       pm.ChangeValues(
                            changeOrganizationCommand.RaisonSociale,
                            changeOrganizationCommand.FormeJuridique,
                            changeOrganizationCommand.Effectif,
                            changeOrganizationCommand.SIRET,
                            changeOrganizationCommand.NAF,
                            changeOrganizationCommand.IdentifiantConventionCollective);
                       using (TransactionScope scope = new TransactionScope())
                       {
                           _repository.Update(pm);
                           scope.Complete();
                       }

                       _domainTracking.LogChanges(pm);
                       pm.MarkChangesAsCommitted();

                       return pm;
                   },
                   None: () => { throw new Exception(string.Format("Personne Morale not found for id = {0}", changeOrganizationCommand.OrganizationId)); });


            return base.Handle(changeOrganizationCommand);
        }
    }
}
