using Messages;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZAGS.BusinessLogic.Framework.Transactions;

namespace Sales
{
    public class TransactionObjectsToSendHandler : IHandleMessages<TransactionObjectsToSend>
    {
        static ILog logger = LogManager.GetLogger<TransactionObjectsToSend>();
        
        public Task Handle(TransactionObjectsToSend message, IMessageHandlerContext context)
        {
            logger.Info($"Received TransactionObjectsToSend, Montext = {message.Montext}");

            return Task.CompletedTask;
        }
    }
}
