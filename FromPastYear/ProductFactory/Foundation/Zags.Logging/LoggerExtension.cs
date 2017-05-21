using Microsoft.Extensions.Logging;
using Zags.Logging.Events;
using Zags.Utility.Errors;

namespace Zags.Logging
{
    public static class LoggerExtension
    {
        public static void LogEvent(this ILogger logger, LogEvent logEvent)
        {
            logEvent.Log(logger);
        }

        public static void LogEvent(this ILogger logger, Error error)
        {
            logger.LogError(error.Message);
        }
    }
}
