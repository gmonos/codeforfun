using System;
using System.Collections.Generic;

namespace Zags.Domain
{
    public abstract class AggregateRoot<T>
    {
        public readonly List<IAggregateTrackingEvent<T>> _trackingChanges = new List<IAggregateTrackingEvent<T>>();
        public Guid AggregateId { get; protected set; }
        public int Version { get; protected set; }

        public IEnumerable<IAggregateTrackingEvent<T>> GetUncommittedChanges()
        {
            lock (_trackingChanges)
            {
                return _trackingChanges.ToArray();
            }
        }

        public void MarkChangesAsCommitted()
        {
            lock (_trackingChanges)
            {
                Version = Version + _trackingChanges.Count;
                _trackingChanges.Clear();
            }
        }

        protected void ApplyTrackingChange(IAggregateTrackingEvent<T> @event)
        {
            ApplyTrackingChange(@event, true);
        }

        private void ApplyTrackingChange(IAggregateTrackingEvent<T> @event, bool isNew)
        {
            lock (_trackingChanges)
            {
                //this.Apply(@event);
                if (isNew)
                {
                    _trackingChanges.Add(@event);
                }
                else
                {
                    AggregateId = @event.AggregateId;
                    Version++;
                }
            }
        }
    }
}
