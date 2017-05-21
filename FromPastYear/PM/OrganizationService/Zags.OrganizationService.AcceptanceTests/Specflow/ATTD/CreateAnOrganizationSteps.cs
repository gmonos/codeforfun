using System;
using System.Linq;
using Moq;
using Nancy;
using Nancy.Testing;
using Newtonsoft.Json;
using paramore.brighter.commandprocessor;
using TechTalk.SpecFlow;
using Xunit;
using Zags.Application.Brighter.Nancy;
using Zags.OrganizationService.Application.Adapters.DataAccess;
using Zags.OrganizationService.Application.Ports.Commands;
using Zags.OrganizationService.API.Adapters.Controllers;
using Zags.OrganizationService.Domain;
using Zags.OrganizationService.Domain.Models;

namespace Zags.OrganizationService.AcceptanceTests.Specflow.ATTD
{
    [Binding]
    public class CreateAnOrganizationSteps
    {
        private Organization _expectedOrganization;

        [Given(@"I have entered into the new organization form the '(.*)' and '(.*)' and (.*) and '(.*)'")]
        public void GivenIHaveEnteredIntoTheNewOrganizationFormTheAndAndAnd(string raisonSociale, string reference, int effectif, string formeJuridique)
        {
            _expectedOrganization = new Organization(1, reference, raisonSociale, formeJuridique, effectif);
        }

        [When(@"I save the new organization '(.*)'")]
        public void WhenISaveTheNewOrganization(string raisonSociale)
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

        [Then(@"the new organization '(.*)' should be selected")]
        public void ThenTheNewOrganizationShouldBeSelected(string raisonSociale)
        {
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

            var actualOrganizations = response.Context.Items["Model"] as OrganizationListModel;
            Assert.NotNull(actualOrganizations);
            Assert.NotEmpty(actualOrganizations.Items);
            Assert.True(actualOrganizations.Items.Any(o => o.RaisonSociale == raisonSociale));
        }
    }
}
