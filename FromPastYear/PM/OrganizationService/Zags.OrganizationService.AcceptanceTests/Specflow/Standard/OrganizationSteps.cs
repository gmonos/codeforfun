using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Nancy;
using Nancy.Testing;
using Newtonsoft.Json;
using paramore.brighter.commandprocessor;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;
using Zags.Application.Brighter.Nancy;
using Zags.OrganizationService.Application.Adapters.DataAccess;
using Zags.OrganizationService.Application.Ports.Commands;
using Zags.OrganizationService.API.Adapters.Controllers;
using Zags.OrganizationService.Domain.Models;

namespace Zags.OrganizationService.AcceptanceTests.Specflow.Standard
{
    [Binding]
    public class OrganizationSteps
    {
        private OrganizationModel _expectedOrganization;
        private IEnumerable<OrganizationModel> _expectedOrganizations;
        private OrganizationListModel _actualOrganizations;
        private OrganizationModel _actualOrganization;
        
        [Given(@"I create a new organization with following data:")]
        public void GivenICreateANewOrganizationWithFollowingData(Table table)
        {
            _expectedOrganization = table.CreateInstance<OrganizationModel>();
        }

        [Given(@"I save the organization")]
        public void GivenISaveTheOrganization()
        {
            var expectedCommand = new AddOrganizationCommand()
            {
                Id = Guid.NewGuid(),
                RaisonSociale = _expectedOrganization.RaisonSociale,
                Reference = _expectedOrganization.Reference,
                FormeJuridique = _expectedOrganization.FormeJuridique,
                Effectif = _expectedOrganization.Effectif.Value
            };

            var browser = new Browser(with =>
            {
            var commandProcessor = new Mock<IAmACommandProcessor>();
                var createCommandFactory = new Mock<ICommandFactory<AddOrganizationCommand>>();
                var modifyCommandFactory = new Mock<ICommandFactory<ChangeOrganizationCommand>>();
                
                createCommandFactory.Setup(o => o.CreateCommand(It.IsAny<INancyModule>())).Returns(expectedCommand);
                commandProcessor.Setup(o => o.Send(expectedCommand));

                with.Module(new OrganizationCommandModule(commandProcessor.Object, createCommandFactory.Object, modifyCommandFactory.Object));
                with.ViewFactory<TestViewFactory>();
            });
            
            var serializedCommand = JsonConvert.SerializeObject(expectedCommand);
            var response = browser.Post("/Organization", (with) => {
                with.Header("content-type", "application/json");
                with.Body(serializedCommand);
            });

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [When(@"I search with the following criteria:")]
        public void WhenISearchWithTheFollowingCriteria(Table table)
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
            var actuals = response.Context.Items["Model"] as OrganizationListModel;

            Assert.NotNull(actuals);
            Assert.NotEmpty(actuals.Items);
            _actualOrganization = actuals.Items.First();
        }

        [Then(@"The search result contains an organization with following data:")]
        public void ThenTheSearchResultContainsAnOrganizationWithFollowingData(Table table)
        {
            var expected = table.CreateInstance<OrganizationModel>();

            Assert.NotNull(_actualOrganization);
            Assert.Equal(_actualOrganization.RaisonSociale, expected.RaisonSociale);
            Assert.Equal(_actualOrganization.Reference, expected.Reference);
            Assert.Equal(_actualOrganization.FormeJuridique, expected.FormeJuridique);
            Assert.Equal(_actualOrganization.Effectif, expected.Effectif);
        }

        [Given(@"I create (.*) organizations with following data:")]
        public void GivenICreateOrganizationsWithFollowingData(int p0, Table table)
        {
            _expectedOrganizations = table.CreateSet<OrganizationModel>();
        }

        [Given(@"I save these organizations")]
        public void GivenISaveTheseOrganizations()
        {

        }

        [Given(@"I search organizations without criteria")]
        public void GivenISearchOrganizationsWithoutCriteria()
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

            Assert.NotNull(_actualOrganizations);
            Assert.NotEmpty(_actualOrganizations.Items);
        }

        [When(@"I select the organization with the following data:")]
        public void WhenISelectTheOrganizationWithTheFollowingData(Table table)
        {
            var expectedOrganization = table.CreateInstance<OrganizationModel>();
            _actualOrganization = _actualOrganizations.Items.First(o => o.RaisonSociale == expectedOrganization.RaisonSociale);
        }

        [Then(@"the result should be display on the screen the following data:")]
        public void ThenTheResultShouldBeDisplayOnTheScreenTheFollowingData(Table table)
        {
            var expected = table.CreateInstance<OrganizationModel>();

            Assert.NotNull(_actualOrganization);
            Assert.Equal(_actualOrganization.RaisonSociale, expected.RaisonSociale);
            Assert.Equal(_actualOrganization.Reference, expected.Reference);
            Assert.Equal(_actualOrganization.FormeJuridique, expected.FormeJuridique);
            Assert.Equal(_actualOrganization.Effectif, expected.Effectif);
        }

    }
}
