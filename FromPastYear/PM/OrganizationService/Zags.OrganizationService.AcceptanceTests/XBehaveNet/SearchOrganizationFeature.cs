using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xbehave;
using Xunit;
using Zags.OrganizationService.Domain;
using Zags.OrganizationService.Domain.Models;

namespace Zags.OrganizationService.AcceptanceTests.XBehaveNet
{
    public class OrganizationRetrieverFake
    {
        public OrganizationListModel Retrieve(string raisonSociale, string internalReference, string externalReference)
        {
            var listPMs = new List<Organization>();
            listPMs.Add(new Organization(1, "1", "The First Test Company"));
            listPMs.Add(new Organization(2, "2", "The Second Test Company"));
            listPMs.Add(new Organization(3, "3", "The Third Test Company"));

            var results = new OrganizationListModel(listPMs, string.Empty);
            
            return results; 
        }
    }
    

    public class SearchOrganizationFeature
    {
        [Scenario]
        [Example("The First Test Company", "1", "1")]
        public void SearchingAnOrganizationBytheCorporateNameExactMatch(string raisonSociale, string internalReference, string externalReference, OrganizationRetrieverFake retriever, OrganizationListModel answer)
        {
           "Given I have an organization with the corporate name {0} "
                .x(() => { });

            "When I search with the corporate name {0} "
                .x(() =>
                {
                    retriever = new OrganizationRetrieverFake();
                    answer = retriever.Retrieve(raisonSociale, internalReference, externalReference);
                });

            "search result contains an organization with the expected corporate name {0}"
                .x(() =>
                {
                    Assert.NotNull(answer);
                    Assert.NotEmpty(answer.Items);
                    var actual = answer.Items.FirstOrDefault();
                    Assert.NotNull(actual);
                    actual.RaisonSociale.Should().Be(raisonSociale);
                });
        }
    }
}
