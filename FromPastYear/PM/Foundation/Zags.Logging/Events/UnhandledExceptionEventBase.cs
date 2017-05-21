using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zags.Logging.Events
{
    public class UnhandledExceptionEvent: LogEvent
    {
        private readonly Exception _exception;

        public override LogLevel Level => LogLevel.Fatal;

        public override void Log()
        {
            throw new NotImplementedException();
        }


    }
}
