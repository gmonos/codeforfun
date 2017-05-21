using System;
using Zags.OrganizationService.Domain.Models;

namespace Zags.OrganizationService.HttpClient
{
    public interface IOrganizationHttpClient
    {
        OrganizationListModel GetOrganizations(string accessToken, Guid correlationId);
    }
}