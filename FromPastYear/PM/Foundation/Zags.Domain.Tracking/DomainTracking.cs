using System;
using System.Collections.Generic;
using Zags.Logging;
using Zags.Logging.Events;

namespace Zags.Domain.Tracking
{
    public class DomainTracking<T> : IDomainTracking<T> where T : AggregateRoot<T>
    {
        private readonly ILog Logger = LogManager.GetLogger<DomainTracking<T>>();

        public DomainTracking()
        {

        }

        public void LogChanges(T aggregate, Func<T,T, List<DomainTrackingDifference>> comparer)
        {
            foreach (var @event in aggregate.GetUncommittedChanges())
            {
                if (@event.AggregateId == Guid.Empty)
                    throw new Exception("Aggregate Exception");

                if (@event.EventId == Guid.Empty)
                    throw new Exception("Aggregate Exception");

                @event.AsOf = DateTime.UtcNow;

                List<DomainTrackingDifference> differences = comparer(@event.OldValue, @event.NewValue);

                Logger.Log(new DomainTrakingEvent<T>(@event.OldValue, @event.NewValue, differences));
            }
        }

        public void LogChanges(T aggregate)
        {
            foreach (var @event in aggregate.GetUncommittedChanges())
            {
                if (@event.AggregateId == Guid.Empty)
                    throw new Exception("Aggregate Exception");

                if (@event.EventId == Guid.Empty)
                    throw new Exception("Aggregate Exception");

                @event.AsOf = DateTime.UtcNow;

                List<DomainTrackingDifference> differences = DomainTrackingHelper<T>.Compare(@event.OldValue, @event.NewValue);

                Logger.Log(new DomainTrakingEvent<T>(@event.OldValue, @event.NewValue, differences));
            }
        }
    }
}
