using System;
using Zags.Domain;

namespace Zags.OrganizationService.Domain.Events
{
    public class OrganizationChangedValuesEvent<T> : IAggregateTrackingEvent<T>
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

        public int OrganizationId { get; set; }

        public T OldValue { get; set; }

        public T NewValue { get; set; }
    }
}
