using Nancy;
using paramore.brighter.commandprocessor;

namespace Zags.OrganizationService.Application.Axa.Controllers
{
    public class AxaOrganizationCommandModule : NancyModule
    {
        private readonly IAmACommandProcessor _commandProcessor;

        public AxaOrganizationCommandModule(IAmACommandProcessor commandProcessor)
        {
            _commandProcessor = commandProcessor;

            Post["/axaOrganization"] = parameters =>
            {
                return HttpStatusCode.OK;
            };


        }
    }
}
