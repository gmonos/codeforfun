using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zags.Logging
{
    public abstract class LoggingFactoryDefinition
    {
        protected internal abstract ILoggerFactory GetLoggingFactory();
    }
}
