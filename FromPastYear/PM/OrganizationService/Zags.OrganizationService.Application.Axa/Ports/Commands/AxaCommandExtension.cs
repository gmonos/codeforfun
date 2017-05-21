using Zags.Domain.Command;

namespace Zags.OrganizationService.Application.Axa.Ports.Commands
{
    public class AxaAddOrganizationCommandExtension : ICommandExtension
    {
        public int OrganizationId { get; set; }
        public string NumAbonne { get; set; }
    }
}
