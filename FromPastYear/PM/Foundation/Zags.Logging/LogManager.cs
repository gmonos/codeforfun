using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zags.Logging
{
    public static class LogManager
    {
        static LogManager()
        {
            
        }

        /// <summary>
        /// Used to inject an instance of ILoggerFactory
        /// </summary>
        public static T Use<T>() where T : LoggingFactoryDefinition, new()
        {
            var loggingDefinition = new T();

            loggerFactory = new Lazy<ILoggerFactory>(loggingDefinition.GetLoggingFactory);

            return loggingDefinition;
        }


        public static void UseFactory(ILoggerFactory loggerFactory)
        {

            LogManager.loggerFactory = new Lazy<ILoggerFactory>(() => loggerFactory);
        }
        public static ILog GetLogger<T>()
        {
            return GetLogger(typeof(T));
        }
        public static ILog GetLogger(Type type)
        {
            return loggerFactory.Value.GetLogger(type);
        }

        public static ILog GetLogger(string name)
        {
            return loggerFactory.Value.GetLogger(name);
        }

        static Lazy<ILoggerFactory> loggerFactory;
    }
}
