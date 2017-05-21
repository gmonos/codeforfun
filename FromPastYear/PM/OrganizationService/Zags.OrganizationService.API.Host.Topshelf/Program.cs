using System;
using Topshelf;
using Zags.OrganizationService.API.Adapters.Service;

namespace Zags.OrganizationService.Host.Topshelf
{
    class Program
    {
        static void Main(string[] args)
        {

            HostFactory.Run(x =>
            {
                x.Service<API.Adapters.Service.OrganizationService>(s =>
                {
                    s.ConstructUsing(name => new API.Adapters.Service.OrganizationService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                    s.WhenShutdown(tc => tc.Shutdown());
                });               
            });
        }
    }
}
