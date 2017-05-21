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
    public class SelectAnOrganizationSteps
    {
        private OrganizationListModel _allOrganizations;
        private OrganizationModel _actualOrganization;

        [Given(@"I have this following organizations in the search list:")]
        public void GivenIHaveThisFollowingOrganizationsInTheSearchList(Table table)
        {
            var browser = new Browser(with =>
            {
                with.Module(new OrganizationQueryModule(new OrganizationRepository()));
                with.ViewFactory<TestViewFactory>();
            });

            var response = browser.Get("/organization", (with) =>
            {
                with.Header("content-type", "application/json");
                with.HttpRequest();
            });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            _allOrganizations = response.Context.Items["Model"] as OrganizationListModel;
            Assert.NotNull(_allOrganizations);
            Assert.NotEmpty(_allOrganizations.Items);
        }

        [When(@"I select the organization (.*)")]
        public void WhenISelectTheOrganization(string raisonSociale)
        {
            var selectedOrganization = _allOrganizations.Items.First(o => o.RaisonSociale == raisonSociale);

            var browser = new Browser(with =>
            {
                with.Module(new OrganizationQueryModule(new OrganizationRepository()));
                with.ViewFactory<TestViewFactory>();
            });

            var response = browser.Get($"/organization/{selectedOrganization.Id}", (with) =>
            {
                with.Header("content-type", "application/json");
                with.HttpRequest();
            });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            _actualOrganization = response.Context.Items["Model"] as OrganizationModel;
        }

        [Then(@"the organization (.*) with these (.*) and (.*) and (.*) details should be displayed on the screen")]
        public void ThenTheOrganizationWithTheseAndAndDetailsShouldBeDisplayedOnTheScreen(string raisonSociale,
            string reference, int effectif, string formeJuridique)
        {
            Assert.NotNull(_actualOrganization);
            Assert.Equal(_actualOrganization.RaisonSociale, raisonSociale);
            Assert.Equal(_actualOrganization.Reference, reference);
            Assert.Equal(_actualOrganization.FormeJuridique, formeJuridique);
            Assert.Equal(_actualOrganization.Effectif, effectif);
        }

        [Given(@"I have this following organizations in the search list:")]
        public void GivenIHaveThisFollowingOrganizationsInTheSearchList()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
