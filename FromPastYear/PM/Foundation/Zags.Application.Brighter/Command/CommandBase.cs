using System;
using Zags.Domain.Command;

namespace Zags.Application.Brighter.Command
{
    public class CommandBase : paramore.brighter.commandprocessor.Command
    {
        public CommandBase(Guid id) : base(id)
        {
        }
        public ICommandExtension Extension { get; set; }
    }
}
