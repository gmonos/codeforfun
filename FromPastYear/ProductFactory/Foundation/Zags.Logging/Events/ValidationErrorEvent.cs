using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Zags.Domain.Entity;
using Zags.Utility.Errors;

namespace Zags.Logging.Events
{
    public class ValidationErrorEvent<TEntity>:LogEvent
        where TEntity : IEntity, new()
    {
        protected TEntity Entity { get; set; }

        protected EnumDomainActionType DomainActionType { get; set; }

        protected IList<Error> Errors { get; set; }

        public ValidationErrorEvent(TEntity entity, EnumDomainActionType domainActionType, IList<Error> errors) :base()
        {
            Entity = entity;
            DomainActionType = domainActionType;
            Errors = errors;
        }

        public ValidationErrorEvent(int id, EnumDomainActionType domainActionType, IList<Error> errors) : 
            this( new TEntity { Id = id }, domainActionType, errors)
        {
           
        }

        override public string LogMessage
        {
            get
            {
                var entityType = (typeof(TEntity)).Name;
                var json = JsonConvert.SerializeObject(Entity);
                var errors = Errors.Aggregate((s1, s2) => s1 + Environment.NewLine + s2);
                var id = Entity.Id;

                return $"Enable to proceed with the {DomainActionType} of {entityType} (id: {id}) {Environment.NewLine} Errors detail: {Environment.NewLine}{errors}{Environment.NewLine}Json entity: {json}";
            }

        }

        override internal void Log(ILogger logger) => logger.LogError(LogMessage);
    }
}
