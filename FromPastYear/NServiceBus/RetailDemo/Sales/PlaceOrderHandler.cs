﻿using Messages;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales
{
    public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
    {
        static ILog logger = LogManager.GetLogger<PlaceOrderHandler>();

        public Task Handle(PlaceOrder message, IMessageHandlerContext context)
        {
            logger.Info($"Received PlaceOrder, OrderId = {message.OrderId}");

            // This is normally where some business logic would occur
      

            var orderPlaced = new OrderPlaced
            {
                OrderId = message.OrderId
            };

            return context.Publish(orderPlaced);
        }
    }
}
