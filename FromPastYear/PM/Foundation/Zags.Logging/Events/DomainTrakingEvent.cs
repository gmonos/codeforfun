using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zags.Logging.Events
{
    public class DomainTrakingEvent<T> : LogEvent 
    {
        protected readonly ILog Logger = LogManager.GetLogger("DomainTrakingEvent");
        public List<DomainTrackingDifference> Differences { get; set; }

        public T OldValue { get; set; }

        public T NewValue { get; set; }
        public override LogLevel Level => LogLevel.Debug;

        public DomainTrakingEvent(T newValue)
        {
            NewValue = newValue;
        }
        public DomainTrakingEvent(T oldValue, T newValue, List<DomainTrackingDifference> differences)
        {
            OldValue = oldValue;
            NewValue = newValue;
            Differences = differences;       
        }

        public override void Log()
        {
            if (OldValue != null)
            {
                Logger.Info("{@OldValue} {@NewValue} {@Difference}", OldValue, NewValue, Differences);
            } else {
                Logger.Info("{@NewValue}", NewValue);
            }
        }
    }
}
