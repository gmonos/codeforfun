using Nancy;
using Zags.Application.Brighter.Command;

namespace Zags.Application.Brighter.Nancy
{
    public interface ICommandFactory<T> where T : CommandBase
    {
        T CreateCommand(INancyModule module);
    }


}
