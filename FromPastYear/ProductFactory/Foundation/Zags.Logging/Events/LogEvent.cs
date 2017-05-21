using Microsoft.Extensions.Logging;

namespace Zags.Logging.Events
{
    abstract public class LogEvent
    {
        public LogEvent()
        {

        }
        public abstract string LogMessage { get; }

        internal abstract void Log(ILogger logger);
    }


    public enum EnumDomainActionType
    {
        Creation,
        Modification,
        Deletion
    }

}