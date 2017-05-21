using Serilog;
using Serilog.Context;
using System;

namespace Zags.Logging.Serilog
{
    class Logger : ILog
    {
        ILogger logger;

        public Logger(ILogger logger)
        {
            this.logger = logger;
        }

        public void Debug(string message)
        {
            logger.Debug("{Text:l}", message);
        }

        public void Log(Zags.Logging.Events.LogEvent @event)
        {
            @event.Log();
        }


        public void Debug(string message, Exception exception)
        {
            logger.Debug(exception, "{Text:l}", message);
        }


        public void Info(string message)
        {
            logger.Information("{Text:l}", message);
        }

        public void Info(string message, Exception exception)
        {
            logger.Information(exception, "{Text:l}", message);
        }


        public void Warn(string message)
        {
            logger.Warning("{Text:l}", message);
        }

        public void Warn(string message, Exception exception)
        {
            logger.Warning(exception, "{Text:l}", message);
        }


        public void Error(string message)
        {
            logger.Error("{Text:l}", message);
        }

        public void Error(string message, Exception exception)
        {
            logger.Error(exception, "{Text:l}", message);
        }


        public void Fatal(string message)
        {
            logger.Fatal("{Text:l}", message);
        }

        public void Fatal(string message, Exception exception)
        {
            logger.Fatal(exception, "{Text:l}", message);
        }

        public IDisposable PushProperty(string property, object value)
        {
            return LogContext.PushProperty(property, value);
        }

        public void Info(string message, params object[] properties)
        {
            logger.Information(message, properties);
        }
    }
}
