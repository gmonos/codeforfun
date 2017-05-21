using Microsoft.Extensions.Logging;
using Zags.Domain.Entity;

namespace Zags.Logging.Events
{
    public class ResourceNotFoundEvent<TEntity>:LogEvent
        where TEntity : IEntity, new()
    {

        readonly int _resourceId;
        public ResourceNotFoundEvent(int id) : base()
        {
            _resourceId = id;
        }

        override public string LogMessage
        {
            get
            {
                var entityType = (typeof(TEntity)).Name;
             

                return $"Enable to find  {entityType} (id: {_resourceId})";
            }

        }

        override internal void Log(ILogger logger) => logger.LogError(LogMessage);
    }
}
