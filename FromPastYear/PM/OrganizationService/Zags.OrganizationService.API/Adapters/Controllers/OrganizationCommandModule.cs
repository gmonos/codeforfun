using Nancy;
using paramore.brighter.commandprocessor;
using Zags.Application.Brighter.Nancy;
using Zags.Logging;
using Zags.Logging.Events;
using Zags.OrganizationService.Application.Ports.Commands;
using Zags.Web.Nancy.Response;

namespace Zags.OrganizationService.API.Adapters.Controllers
{
    public class OrganizationCommandModule : NancyModule
    {
        private readonly IAmACommandProcessor _commandProcessor;
        private readonly ICommandFactory<AddOrganizationCommand> _createCommandFactory;
        private readonly ICommandFactory<ChangeOrganizationCommand> _modifyCommandFactory;
        private readonly ILog Logger = LogManager.GetLogger<OrganizationCommandModule>();

        public OrganizationCommandModule(IAmACommandProcessor commandProcessor,
                               ICommandFactory<AddOrganizationCommand> createCommandFactory, ICommandFactory<ChangeOrganizationCommand> modifyCommandFactory)
        {

            //this.RequiresOwinAuthentication();

            _commandProcessor = commandProcessor;
            _createCommandFactory = createCommandFactory;
            _modifyCommandFactory = modifyCommandFactory;

            Post["/Organizations"] = parameters =>
            {
                var addPMCommand = _createCommandFactory.CreateCommand(this);
                _commandProcessor.Send(addPMCommand);
                return this.CreatedResponse("/Organization", addPMCommand.OrganizationId);
            };

            
            Put["/Organizations/{OrganizationId}"] = parameters =>
            {

                var changePMCommand = _modifyCommandFactory.CreateCommand(this);
                
                _commandProcessor.Send(changePMCommand);
                return HttpStatusCode.OK;
            };

            Post["/Organizations/{OrganizationId}/addresses"] = parameters =>
            {
                return HttpStatusCode.OK;
            };

            Post["/Organizations/{OrganizationId}/ibans"] = parameters =>
            {
                return HttpStatusCode.OK;
            };

            Delete["/Organizations/{OrganizationId}"] = parameters =>
            {
                return HttpStatusCode.OK;
            };
        }

    }
}
