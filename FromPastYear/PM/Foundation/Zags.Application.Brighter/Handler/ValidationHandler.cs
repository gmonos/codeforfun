using paramore.brighter.commandprocessor;
using paramore.brighter.commandprocessor.Logging;
using System;
using System.Collections.Generic;
using Zags.Domain.Validation;
using Zags.Utilities;
using Zags.Utilities.Functional;

namespace Zags.Application.Brighter.Handler
{
    public class ValidationHandler<TRequest> : RequestHandler<TRequest>
        where TRequest : class, IRequest, ICanBeValidated<TRequest>
    {
        public ValidationHandler()
            : this(LogProvider.For<ValidationHandler<TRequest>>())
        { }

        public ValidationHandler(ILog logger)
            : base(logger)
        { }

        public override TRequest Handle(TRequest command)
        {
            command.Validate().Match(
                Right: x => new Unit(),
                Left: errors => HandleErrors(errors)
                );

            return base.Handle(command);
        }

        private void HandleErrors(List<Error> errors)
        {
            //TODO : Create a ValidationException
            throw new ArgumentException("The commmand was not valid");
        }
    }
}
