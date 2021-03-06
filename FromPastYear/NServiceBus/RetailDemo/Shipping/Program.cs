﻿using Messages;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Persistence.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping
{
    class Program
    {
        static void Main()
        {
            AsyncMain().GetAwaiter().GetResult();
        }

        static async Task AsyncMain()
        {
            Console.Title = "Shipping";

            var endpointConfiguration = new EndpointConfiguration("Shipping");
            var routing = endpointConfiguration.UseTransport<MsmqTransport>()
                .Routing();

            routing.RegisterPublisher(typeof(OrderPlaced), "Sales");
            routing.RegisterPublisher(typeof(OrderBilled), "Billing");

            endpointConfiguration.UseSerialization<JsonSerializer>();

            endpointConfiguration.DisableFeature<TimeoutManager>();
            endpointConfiguration.UsePersistence<MsmqPersistence>();

            //endpointConfiguration.UsePersistence<InMemoryPersistence>();

            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.EnableInstallers();

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}
