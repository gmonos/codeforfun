using System;

namespace Zags.Domain
{
    public interface IAggregateTrackingEvent<T>
    {
        Guid AggregateId { get; set; }
        Guid EventId { get; set; }
        DateTime AsOf { get; set; }

        T OldValue { get; set; }
        T NewValue { get; set; }

        //Others properties can be added for tracking reason

    }
}
