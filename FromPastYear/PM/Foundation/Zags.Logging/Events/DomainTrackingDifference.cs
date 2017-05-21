using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zags.Logging.Events
{ 

    public class DomainTrackingDifference
    {
        public string PropertyName { get; set; }
        public object OldValue { get; set; }
        public string NewValue { get; set; }
    }
}
