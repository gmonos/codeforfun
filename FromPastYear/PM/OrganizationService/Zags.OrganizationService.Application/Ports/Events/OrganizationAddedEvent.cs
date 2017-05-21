using System;
using paramore.brighter.commandprocessor;
using Zags.Domain;

namespace Zags.OrganizationService.Application.Ports.Events
{
    public class OrganizationAddedEvent : Event
    {
        public Guid AggregateId
        {
            get; set;
        }

        public DateTime AsOf
        {
            get; set;
        }

        public Guid EventId
        {
            get; set;
        }

        public OrganizationAddedEvent() : base(Guid.NewGuid())
        {
            EventId = Id;
        }

        
    }
}
