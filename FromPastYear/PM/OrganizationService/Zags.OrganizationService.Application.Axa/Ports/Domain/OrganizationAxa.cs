using Zags.Domain;

namespace Zags.OrganizationService.Application.Axa.Ports.Domain
{
    public class OrganizationAxa : IExtension
    {
        public int OrganizationId { get; set; }
        public string NumAbonne { get; set; }
    }
}
