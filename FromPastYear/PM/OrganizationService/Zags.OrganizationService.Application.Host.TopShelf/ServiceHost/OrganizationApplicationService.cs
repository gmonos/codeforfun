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
using Zags.OrganizationService.Legacy.Ports.Handlers;
using Zags.Application.Brighter.Unity;
using Zags.Domain.Tracking;
using Zags.DataAccess.EF;
using Zags.Disptacher.Brighter.Unity;
using Zags.OrganizationService.Application.Ports.Mappers;
using paramore.brighter.commandprocessor.messaginggateway.rmq;
using System.Configuration;
using paramore.brighter.serviceactivator;
using Topshelf;
using Zags.DataAccess;
using Zags.DataAccess.Dapper;

namespace Zags.OrganizationService.Application.Host.TopShelf.ServiceHost
{
    class OrganizationApplicationService : ServiceControl
    {
        private Dispatcher _dispatcher;

        public OrganizationApplicationService()
        {
            log4net.Config.XmlConfigurator.Configure();

            var container = new UnityContainer();
            var logger = LogProvider.For<OrganizationApplicationService>();
            container.RegisterInstance(typeof(ILog), LogProvider.For<OrganizationApplicationService>(), new ContainerControlledLifetimeManager());
            container.RegisterType<AddOrganizationCommandHandler>();
            //container.RegisterType<AddEFOrganisationCommandHandler>();



            container.RegisterType<ChangeOrganizationCommandHandler>();
            container.RegisterType<OrganizationAddedEventHandler>();
            container.RegisterType<OrganizationAddedLegacyEventHandler>();

            //Factories
            //container.RegisterType(typeof(ICommandFactory<AddOrganizationCommand>),typeof(CommandFactory<AddOrganizationCommand, AxaAddOrganizationCommandExtension>));
            //container.RegisterType<IDomainFactory, AxaDomainFactory>();
            //container.RegisterType(typeof(ICommandFactory<>), typeof(CommandFactory<>));

            container.RegisterType<IDomainFactory, DomainFactory>();
            container.RegisterType(typeof(IDomainTracking<>), typeof(DomainTracking<>));

            //Repositories
            container.RegisterType<IOrganizationRepository, OrganizationRepository>();
            container.RegisterType(typeof(IEFGenericRepository<>), typeof(EFGenericRepository<>));
            container.RegisterType(typeof(IDapperGenericRepository<>), typeof(DapperGenericRepository<>));

            //container.RegisterType<DbContext, OrganisationDbContext>(new ContainerControlledLifetimeManager());
            container.RegisterType(typeof(IGenericRepository<>), typeof(EFGenericRepository<>));

            //Command Processor
            paramore.brighter.commandprocessor.CommandProcessor commandProcessor = CreateCommandProcesor(container);

            container.RegisterInstance(typeof(IAmACommandProcessor), commandProcessor);
           
            var rmqMessageConsumerFactory = new RmqMessageConsumerFactory(logger);
            var rmqMessageProducerFactory = new RmqMessageProducerFactory(logger);

            var messageMapperFactory = new UnityMessageMapperFactory(container);
            var messageMapperRegistry = new MessageMapperRegistry(messageMapperFactory);
            messageMapperRegistry.Register<AddOrganizationCommand, AddOrganizationCommandMessageMapper>();

            _dispatcher = DispatchBuilder.With()
                .CommandProcessor(commandProcessor)
                .MessageMappers(messageMapperRegistry)
                .ChannelFactory(new InputChannelFactory(rmqMessageConsumerFactory, rmqMessageProducerFactory))
                .ConnectionsFromConfiguration()
                .Build();
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
            
            var commandProcessor = CommandProcessorBuilder.With()
                    .Handlers(new HandlerConfiguration(subscriberRegistry, handlerFactory))
                    .Policies(policyRegistry)
                    .NoTaskQueues()
                    .RequestContextFactory(new InMemoryRequestContextFactory())
                    .Build();

            return commandProcessor;
        }

        public bool Start(HostControl hostControl)
        {
            _dispatcher.Receive();
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _dispatcher.End().Wait();
            _dispatcher = null;
            return true;
        }

        public void Shutdown(HostControl hostcontrol)
        {
            if (_dispatcher != null)
                _dispatcher.End().Wait();
        }
    }
}
