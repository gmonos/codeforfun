using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using Zags.Domain.Entity;

namespace Zags.Logging.Events
{
    public class DomainTrakingEvent<T> : LogEvent
        where T : IEntity
    {
        protected T Entity { get; set; }


        protected EnumDomainActionType DomainTrackingType { get; set; }

        public DomainTrakingEvent( T entity, EnumDomainActionType domainTrackingType) :base()
        {
            Entity = entity;
            DomainTrackingType = domainTrackingType;
        }

        override public string LogMessage
        {
            get
            {
                var entityType = (typeof(T)).Name;
                var newJson = JsonConvert.SerializeObject(Entity);
                var id = Entity.Id;

                return $"A {DomainTrackingType} has been successfully proceeded on {entityType} (id={id}) {Environment.NewLine}Json entity:{newJson}";
            }
        }

        override internal void Log(ILogger logger) => logger.LogInformation(LogMessage);
    }


 
}
