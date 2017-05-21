using System;
using System.Collections.Generic;
using Zags.Logging.Events;

namespace Zags.Domain.Tracking
{
    public interface IDomainTracking<T> where T : AggregateRoot<T>
    {
        void LogChanges(T aggregate);

        void LogChanges(T aggregate, Func<T, T, List<DomainTrackingDifference>> comparer);
    }
}
