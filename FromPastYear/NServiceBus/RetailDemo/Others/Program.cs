using Messages;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Logging;
using NServiceBus.Persistence.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Others
{
    class Program
    {
        static void Main()
        {
            AsyncMain().GetAwaiter().GetResult();
        }

        static async Task AsyncMain()
        {
            Console.Title = "Others";

            var endpointConfiguration = new EndpointConfiguration("Others");

            var routing = endpointConfiguration.UseTransport<MsmqTransport>()
                .Routing();

            //routing.RouteToEndpoint(typeof(PlaceOrder), "Sales");
            routing.RegisterPublisher(typeof(OrderPlaced), "Sales");

            endpointConfiguration.UseSerialization<JsonSerializer>();

            endpointConfiguration.DisableFeature<TimeoutManager>();
            endpointConfiguration.UsePersistence<MsmqPersistence>();

            //endpointConfiguration.UsePersistence<InMemoryPersistence>();

            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.EnableInstallers();

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
        .ConfigureAwait(false);

            //await RunLoop(endpointInstance);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }

        static ILog logger = LogManager.GetLogger<Program>();

        static async Task RunLoop(IEndpointInstance endpointInstance)
        {
            while (true)
            {
                var command = new PlaceOrder
                {
                    OrderId = Guid.NewGuid().ToString()
                };

                logger.Info($"Sending PlaceOrder command, OrderId = {command.OrderId}");
                await endpointInstance.Send(command)
                    .ConfigureAwait(false);
            }
        }
    }
}
