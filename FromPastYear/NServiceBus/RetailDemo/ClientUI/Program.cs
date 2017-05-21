using Messages;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Logging;
using NServiceBus.Persistence.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

class Program
{
    static void Main()
    {

        AsyncMain().GetAwaiter().GetResult();
    }

    static async Task AsyncMain()
    {
        Console.Title = "ClientUI";

        var endpointConfiguration = new EndpointConfiguration("ClientUI");

        var transport = endpointConfiguration.UseTransport<MsmqTransport>();

        transport.Transactions(TransportTransactionMode.TransactionScope);
        transport.TransactionScopeOptions(isolationLevel: IsolationLevel.RepeatableRead);

        var routing = transport.Routing();
        routing.RouteToEndpoint(typeof(PlaceOrder), "Sales");
        //routing.RouteToEndpoint(typeof(PlaceOrder), "Others");

        endpointConfiguration.UseSerialization<JsonSerializer>();

        //endpointConfiguration.DisableFeature<TimeoutManager>();
        //endpointConfiguration.UsePersistence<MsmqPersistence>();

          endpointConfiguration.UsePersistence<InMemoryPersistence>();

        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.EnableInstallers();

        var endpointInstance = await Endpoint.Start(endpointConfiguration)
    .ConfigureAwait(false);

        await RunLoop(endpointInstance);

        await endpointInstance.Stop()
            .ConfigureAwait(false);
    }

    static ILog logger = LogManager.GetLogger<Program>();

    static async Task RunLoop(IEndpointInstance endpointInstance)
    {
        while (true)
        {
           
                logger.Info("Press 'P' to place an order, or 'Q' to quit.");
                var key = Console.ReadKey();
                Console.WriteLine();

                switch (key.Key)
                {
                    case ConsoleKey.P:
                    var option = new TransactionOptions();
                    option.IsolationLevel = IsolationLevel.ReadCommitted;

                    using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
                    {
                        var command = new PlaceOrder
                        {
                            OrderId = Guid.NewGuid().ToString()
                        };

                        // Send the command to the local endpoint
                        logger.Info($"Sending PlaceOrder command, OrderId = {command.OrderId}");
                        await endpointInstance.Send(command)
                            .ConfigureAwait(false);

                        transactionScope.Complete();

                    }
                    break;

                case ConsoleKey.Q:
                        return;

                    default:
                        logger.Info("Unknown input. Please try again.");
                        break;
                }
        }
    }
}
