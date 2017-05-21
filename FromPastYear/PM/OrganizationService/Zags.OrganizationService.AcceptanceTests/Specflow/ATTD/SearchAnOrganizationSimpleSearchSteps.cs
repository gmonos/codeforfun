using System.Linq;
using Nancy;
using Nancy.Testing;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;
using Zags.OrganizationService.Application.Adapters.DataAccess;
using Zags.OrganizationService.API.Adapters.Controllers;
using Zags.OrganizationService.Domain.Models;

namespace Zags.OrganizationService.AcceptanceTests.Specflow.ATTD
{
    [Binding]
    public class SearchAnOrganizationSimpleSearchSteps
    {
        private OrganizationListModel _actualOrganizations;

        [Given(@"There are (.*) organizations with this following data:")]
        public void GivenThereAreOrganizationsWithThisFollowingData(int organizationCount, Table table)
        {
            var _organizations = table.CreateSet<OrganizationModel>();
        }
        
        [When(@"I search an organization by the (.*) and (.*)")]
        public void WhenISearchAnOrganizationByTheAnd(string raisonSociale, string reference)
        {
            //// Exercise SUT
            var browser = new Browser(with =>
            {
                with.Module(new OrganizationQueryModule(new OrganizationRepository()));
                with.ViewFactory<TestViewFactory>();
            });

            var response = browser.Get("/Organization", (with) =>
            {
                with.Header("content-type", "application/json");
                with.HttpRequest();
            });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            _actualOrganizations = response.Context.Items["Model"] as OrganizationListModel;
        }
        
        [Then(@"The result should be the organization (.*)")]
        public void ThenTheResultShouldBeTheOrganization(string result)
        {
            Assert.NotEmpty(_actualOrganizations.Items);
            Assert.Equal(_actualOrganizations.Items.Count(), 1);
            Assert.True(_actualOrganizations.Items.Any(o => o.RaisonSociale == result));
        }

        [Given(@"There are (.*) organizations")]
        public void GivenThereAreOrganizations(int organizationCount)
        {
           
        }

        [When(@"I search an organization")]
        public void WhenISearchAnOrganization()
        {
            //// Exercise SUT
            var browser = new Browser(with =>
            {
                with.Module(new OrganizationQueryModule(new OrganizationRepository()));
                with.ViewFactory<TestViewFactory>();
            });

            var response = browser.Get("/Organization", (with) =>
            {
                with.Header("content-type", "application/json");
                with.HttpRequest();
            });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            _actualOrganizations = response.Context.Items["Model"] as OrganizationListModel;
        }

        [Then(@"The result should contains (.*) organizations")]
        public void ThenTheResultShouldContainsOrganizations(int organizationCount)
        {
            Assert.NotEmpty(_actualOrganizations.Items);
            Assert.Equal(_actualOrganizations.Items.Count(), organizationCount);
        }

        [Then(@"The result should be (.*)")]
        public void ThenTheResultShouldBe(int organizationCount)
        {
            Assert.NotEmpty(_actualOrganizations.Items);
            Assert.Equal(_actualOrganizations.Items.Count(), organizationCount);
        }

        [Then(@"The result should be empty")]
        public void ThenTheResultShouldBeEmpty()
        {
            Assert.Empty(_actualOrganizations.Items);
        }

    }
}
