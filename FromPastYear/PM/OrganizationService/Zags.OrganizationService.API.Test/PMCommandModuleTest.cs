using System;
using Moq;
using Nancy;
using Nancy.Testing;
using Newtonsoft.Json;
using paramore.brighter.commandprocessor;
using Serilog;
using Serilog.Filters;
using Xunit;
using Xunit.Sdk;
using Zags.OrganizationService.Application.Ports.Commands;
using Zags.OrganizationService.API.Adapters.Controllers;
using Zags.Application.Brighter.Nancy;
using Zags.Logging;
using Zags.Logging.Serilog;

namespace Zags.OrganizationService.API.Test
{
    public class OrganizationCommandModuleTest
    {

        private void InitializeLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .Enrich.WithProperty("ServiceName", "OrganizationService")
                .Enrich.FromLogContext()
                .MinimumLevel.Verbose()
                .WriteTo.Logger(c =>
                    c.Enrich.FromLogContext()
                    .Filter.ByIncludingOnly(Matching.FromSource("DomainTrakingEvent")))
                .CreateLogger();
            LogManager.Use<SerilogFactory>();
        }

        [Fact]
        public void CreatedHttpStatusCodeCreatedWhenCallingCorporateEntityAddEndpoint()
        {
            InitializeLogger();
            var commandProcessor = new Mock<IAmACommandProcessor>();
            var CreateCommandFactory = new Mock<ICommandFactory<AddOrganizationCommand>>();
            var ModifyCommandFactory = new Mock<ICommandFactory<ChangeOrganizationCommand>>();
            var returnedCommand = new AddOrganizationCommand()
            {
                CodeNAF = "456546",
                FormeJuridique = "Toto",
                Id = Guid.NewGuid()
            };
            var serializedCommand = JsonConvert.SerializeObject(returnedCommand);
            CreateCommandFactory.Setup(o => o.CreateCommand(It.IsAny<INancyModule>())).Returns(returnedCommand);
            commandProcessor.Setup(o => o.Send(returnedCommand));

            var browser = new Browser(with =>
            {
                with.Module(new OrganizationCommandModule(commandProcessor.Object, CreateCommandFactory.Object,ModifyCommandFactory.Object));
                with.ViewFactory<TestViewFactory>();
            });

            var response = browser.Post("/Organization", (with) => {
                with.Header("content-type", "application/json");
                with.Body(serializedCommand);
            });

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }


        [Fact]
        public void HttpNotFoundWhenCallingWrongEndpoint()
        {
            InitializeLogger();
            var commandProcessor = new Mock<IAmACommandProcessor>();
            var CreateCommandFactory = new Mock<ICommandFactory<AddOrganizationCommand>>();
            var ModifyCommandFactory = new Mock<ICommandFactory<ChangeOrganizationCommand>>();
            var returnedCommand = new AddOrganizationCommand()
            {
                CodeNAF = "456546",
                FormeJuridique = "Toto",
                Id = Guid.NewGuid()
            };
            var serializedCommand = JsonConvert.SerializeObject(returnedCommand);
            CreateCommandFactory.Setup(o => o.CreateCommand(It.IsAny<INancyModule>())).Returns(returnedCommand);
            commandProcessor.Setup(o => o.Send(returnedCommand));

            var browser = new Browser(with =>
            {
                with.Module(new OrganizationCommandModule(commandProcessor.Object, CreateCommandFactory.Object, ModifyCommandFactory.Object));
                with.ViewFactory<TestViewFactory>();
            });

            var response = browser.Post("/toto", (with) => {
                with.Header("content-type", "application/json");
                with.Body(serializedCommand);
            });

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public void HttpOkWhenCallingPutEndpoint()
        {
            InitializeLogger();
            var commandProcessor = new Mock<IAmACommandProcessor>();
            var CreateCommandFactory = new Mock<ICommandFactory<AddOrganizationCommand>>();
            var ModifyCommandFactory = new Mock<ICommandFactory<ChangeOrganizationCommand>>();
            var returnedCommand = new ChangeOrganizationCommand()
            {
                NAF = "456546",
                FormeJuridique = "Toto",
                Id = Guid.NewGuid(),
                Effectif = 42,
                Extension = null,
                IdentifiantConventionCollective = "tutu",
                OrganizationId = 42,
                RaisonSociale = "titi",SIRET = "453645343453345"
            };
            var serializedCommand = JsonConvert.SerializeObject(returnedCommand);
            ModifyCommandFactory.Setup(o => o.CreateCommand(It.IsAny<INancyModule>())).Returns(returnedCommand);
            commandProcessor.Setup(o => o.Send(returnedCommand));

            var browser = new Browser(with =>
            {
                with.Module(new OrganizationCommandModule(commandProcessor.Object, CreateCommandFactory.Object, ModifyCommandFactory.Object));
                with.ViewFactory<TestViewFactory>();
            });

            var response = browser.Put("/Organization/42", (with) => {
                with.Header("content-type", "application/json");
                with.Body(serializedCommand);
            });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }



    }
}
