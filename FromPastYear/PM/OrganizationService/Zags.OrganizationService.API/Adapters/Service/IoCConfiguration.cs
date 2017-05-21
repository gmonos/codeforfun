using System;
using Microsoft.Practices.Unity;
using paramore.brighter.commandprocessor;
using paramore.brighter.commandprocessor.Logging;
using Polly;
using Zags.OrganizationService.Application.Adapters.DataAccess;
using Zags.OrganizationService.Application.Ports.Commands;
using Zags.OrganizationService.Application.Ports.Events;
using Zags.OrganizationService.Application.Ports.Factories;
using Zags.OrganizationService.Application.Ports.Handlers;
using Zags.OrganizationService.API.Adapters.Controllers;
using Zags.OrganizationService.Legacy.Ports.Handlers;
using Zags.Application.Brighter.Unity;
using Zags.Application.Brighter.Nancy;
using Zags.OrganizationService.Domain;
using Zags.Domain.Tracking;
using Zags.DataAccess.EF;
using System.Data.Entity;
using Zags.Disptacher.Brighter.Unity;
using Zags.OrganizationService.Application.Ports.Mappers;
using paramore.brighter.commandprocessor.messaginggateway.rmq;
using paramore.brighter.commandprocessor.messagestore.mssql;
using System.Configuration;
using Zags.DataAccess.Dapper;
using Zags.DataAccess;

namespace Zags.OrganizationService.API.Adapters.Service
{
    internal static class IoCConfiguration
    {

        public static void Run(UnityContainer container)
        {
            container.RegisterType<OrganizationQueryModule>();
            container.RegisterType<EFOrganizationQueryModule>();
            container.RegisterType<AddressQueryModule>();
            container.RegisterType<EFAddressQueryModule>();

            container.RegisterType<OrganizationCommandModule>();
            container.RegisterInstance(typeof(ILog), LogProvider.For<OrganizationService>(), new ContainerControlledLifetimeManager());
            container.RegisterType<AddOrganizationCommandHandler>();
            container.RegisterType<AddEFOrganisationCommandHandler>();


            
            container.RegisterType<ChangeOrganizationCommandHandler>();
            container.RegisterType<OrganizationAddedEventHandler>();
            container.RegisterType<OrganizationAddedLegacyEventHandler>();

            //Factories
            //container.RegisterType(typeof(ICommandFactory<AddOrganizationCommand>),typeof(CommandFactory<AddOrganizationCommand, AxaAddOrganizationCommandExtension>));
            //container.RegisterType<IDomainFactory, AxaDomainFactory>();
            container.RegisterType(typeof(ICommandFactory<>), typeof(CommandFactory<>));

            container.RegisterType<IDomainFactory, DomainFactory>();
            container.RegisterType(typeof(IDomainTracking<>), typeof(DomainTracking<>));

            //Repositories
            container.RegisterType<IOrganizationRepository, OrganizationRepository>();
            container.RegisterType(typeof(IDapperGenericRepository<>), typeof(DapperGenericRepository<>));
            container.RegisterType(typeof(IEFGenericRepository<>), typeof(EFGenericRepository<>));

            container.RegisterType<DbContext, OrganisationDbContext>(new ContainerControlledLifetimeManager());
            container.RegisterType(typeof(IGenericRepository<>), typeof(EFGenericRepository<>));

            //Command Processor
            paramore.brighter.commandprocessor.CommandProcessor commandProcessor = CreateCommandProcesor(container);

            container.RegisterInstance(typeof(IAmACommandProcessor), commandProcessor);
        }

        private static paramore.brighter.commandprocessor.CommandProcessor CreateCommandProcesor(UnityContainer container)
        {
            var logger = container.Resolve<ILog>();
            var handlerFactory = new UnityHandlerFactory(container);

            var subscriberRegistry = new SubscriberRegistry();
            subscriberRegistry.Register<AddOrganizationCommand, AddOrganizationCommandHandler>();
            //subscriberRegistry.Register<AddOrganizationCommand, AddEFOrganisationCommandHandler>();

            subscriberRegistry.Register<ChangeOrganizationCommand, ChangeOrganizationCommandHandler>();
            subscriberRegistry.Register<OrganizationAddedEvent, OrganizationAddedEventHandler>();
            subscriberRegistry.Register<OrganizationAddedEvent, OrganizationAddedLegacyEventHandler>();

            //create retry policies
            var retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetry(new[]
                    {
                        TimeSpan.FromMilliseconds(50),
                        TimeSpan.FromMilliseconds(100),
                        TimeSpan.FromMilliseconds(150)
                    });

            //create circuit breaker policies
            var circuitBreakerPolicy = Policy
                .Handle<Exception>()
                .CircuitBreaker(1, TimeSpan.FromMilliseconds(500));

            var policyRegistry = new PolicyRegistry()
            {
                {paramore.brighter.commandprocessor.CommandProcessor.RETRYPOLICY, retryPolicy},
                {paramore.brighter.commandprocessor.CommandProcessor.CIRCUITBREAKER, circuitBreakerPolicy}
            };


            var messageMapperFactory = new UnityMessageMapperFactory(container);
            var messageMapperRegistry = new MessageMapperRegistry(messageMapperFactory);
            messageMapperRegistry.Register<AddOrganizationCommand, AddOrganizationCommandMessageMapper>();
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            var gateway = new RmqMessageProducer(container.Resolve<ILog>());
            IAmAMessageStore<Message> sqlMessageStore = new MsSqlMessageStore(new MsSqlMessageStoreConfiguration(connectionString, "Messages", MsSqlMessageStoreConfiguration.DatabaseType.MsSqlServer), logger);

            var commandProcessor = CommandProcessorBuilder.With()
                    .Handlers(new HandlerConfiguration(subscriberRegistry, handlerFactory))
                    .Policies(policyRegistry)
                    .TaskQueues(new MessagingConfiguration(sqlMessageStore, gateway, messageMapperRegistry))
                    .RequestContextFactory(new InMemoryRequestContextFactory())
                    .Build();

            return commandProcessor;
        }
    }
}
