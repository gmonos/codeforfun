using System.Collections.Generic;
using CsQuery.ExtensionMethods;
using Moq;
using Nancy;
using Nancy.Testing;
using Newtonsoft.Json;
using Ploeh.AutoFixture;
using Xunit;
using Zags.Domain;
using Zags.OrganizationService.Application.Adapters.DataAccess;
using Zags.OrganizationService.Application.Axa.Ports.Domain;
using Zags.OrganizationService.API.Adapters.Controllers;
using Zags.OrganizationService.Domain;
using Zags.OrganizationService.Domain.Models;

namespace Zags.OrganizationService.API.Test
{
    public class OrganizationQueryModuleTests
    {
        [Fact]
        public void HttpOkWhenCallingGetPM()
        {
            var fixture = new Fixture();
            fixture.Register<IExtension>(() => fixture.Create<OrganizationAxa>());
            var repository = new Mock<IOrganizationRepository>();
            var returnedCommand = fixture.Create<IEnumerable<Organization>>();
            var serializedCommand = JsonConvert.SerializeObject(returnedCommand);

            repository.Setup(o => o.FindAll()).Returns(returnedCommand);

            var browser = new Browser(with =>
            {
                with.Module(new OrganizationQueryModule(repository.Object));
                with.ViewFactory<TestViewFactory>();
            });

            var response = browser.Get("/Organization", (with) => {
                with.HttpRequest();
                with.Header("content-type", "application/json");
                with.Body(serializedCommand);
            });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public void HttpOkWhenCallingGetById()
        {
            var fixture = new Fixture();
            fixture.Register<IExtension>(() => fixture.CreateAnonymous<OrganizationAxa>());
            var repository = new Mock<IOrganizationRepository>();
            var returnedCommand = fixture.Create<Organization>();
            var serializedCommand = JsonConvert.SerializeObject(returnedCommand);

            repository.Setup(o => o.GetDeepById(It.IsAny<int>())).Returns(returnedCommand);

            var browser = new Browser(with =>
            {
                with.Module(new OrganizationQueryModule(repository.Object));
                with.ViewFactory<TestViewFactory>();
            });

            var response = browser.Get("/Organization/42", (with) => {
                with.HttpRequest();
                with.Header("content-type", "application/json");
                with.Body(serializedCommand);
            });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var jsonObjectFromResponse = JsonConvert.DeserializeObject<OrganizationModel>( response.Context.Items["model"].ToJSON());

            Assert.Equal(jsonObjectFromResponse.CodeNAF, returnedCommand.CodeNAF);
            Assert.Equal(jsonObjectFromResponse.Effectif, returnedCommand.Effectif);
            Assert.Equal(jsonObjectFromResponse.FormeJuridique, returnedCommand.FormeJuridique);
            Assert.Equal(jsonObjectFromResponse.RaisonSociale, returnedCommand.RaisonSociale);
            Assert.Equal(jsonObjectFromResponse.SIRET, returnedCommand.SIRET);

        }
    }
}
