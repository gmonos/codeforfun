using paramore.brighter.commandprocessor;
using System;

namespace Zags.Application.Brighter.Handler
{
    public class ValidationAttribute : RequestHandlerAttribute
    {
        public ValidationAttribute(int step, HandlerTiming timing)
            : base(step, timing)
        { }

        public override Type GetHandlerType()
        {
            return typeof(ValidationHandler<>);
        }
    }
}
