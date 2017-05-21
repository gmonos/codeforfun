using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zags.Logging.Events
{
    public abstract class LogEvent
    {
        

        public string Message { get; set; }

        public abstract LogLevel Level { get; }

        public abstract void Log();

    }
}
