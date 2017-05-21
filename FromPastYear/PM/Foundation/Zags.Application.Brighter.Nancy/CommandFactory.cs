using Nancy;
using Nancy.Extensions;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using Zags.Application.Brighter.Command;
using Zags.Domain.Command;

namespace Zags.Application.Brighter.Nancy
{
    public class CommandFactory<T> : ICommandFactory<T> where T : CommandBase
    {
        public virtual T CreateCommand(INancyModule module)
        {
            return module.Bind<T>();
        }
    }

    public class CommandFactory<T, U> : CommandFactory<T> where T : CommandBase
                                                         where U : ICommandExtension
    {
        public override T CreateCommand(INancyModule module)
        {
            dynamic request = JsonConvert.DeserializeObject(module.Request.Body.AsString());

            var createCommand = base.CreateCommand(module);

            var extension = JsonConvert.DeserializeObject<U>(request["ext"].ToString());
            createCommand.Extension = extension;
            return createCommand;
        }
    }
}
