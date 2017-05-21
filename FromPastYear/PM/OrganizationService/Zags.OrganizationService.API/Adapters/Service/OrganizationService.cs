using System;
using Microsoft.Owin.Hosting;
using Zags.OrganizationService.Application;
using Zags.OrganizationService.API.Adapters.Configuration;

namespace Zags.OrganizationService.API.Adapters.Service
{
    public class OrganizationService
    {
        private IDisposable _app;
        public bool Start()
        {
            var configuration = OrganizationServerConfiguration.GetConfiguration();
            var uri = configuration.Address.Uri;
            Globals.HostName = uri.Host + ":" + uri.Port;
            _app = WebApp.Start<StartUp>(configuration.Address.Uri.AbsoluteUri);
            return true;
        }


        public bool Stop()
        {
            _app.Dispose();
            return true;
        }

        public void Shutdown()
        {
            return;
        }
    }
}
