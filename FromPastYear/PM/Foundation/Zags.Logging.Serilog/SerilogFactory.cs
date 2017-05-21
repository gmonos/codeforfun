using Serilog;

namespace Zags.Logging.Serilog
{
    public class SerilogFactory : LoggingFactoryDefinition
    {
        ILogger loggerToUse = Log.Logger;

        protected override ILoggerFactory GetLoggingFactory()
        {
            return new LoggerFactory(loggerToUse);
        }

        public void WithLogger(ILogger logger)
        {
            loggerToUse = logger;
        }
    }
}
