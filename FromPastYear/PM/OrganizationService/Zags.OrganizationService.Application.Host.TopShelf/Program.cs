using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using Zags.OrganizationService.Application.Host.TopShelf.ServiceHost;

namespace Zags.OrganizationService.Application.Host.TopShelf
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x => x.Service<OrganizationApplicationService>(sc =>
            {
                sc.ConstructUsing(() => new OrganizationApplicationService());

                // the start and stop methods for the service
                sc.WhenStarted((s, hostcontrol) => s.Start(hostcontrol));
                sc.WhenStopped((s, hostcontrol) => s.Stop(hostcontrol));

                // optional, when shutdown is supported
                sc.WhenShutdown((s, hostcontrol) => s.Shutdown(hostcontrol));
            }));
        }
    }
}
