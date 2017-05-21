using System;
using Microsoft.Practices.Unity;
using Nancy;
using Nancy.Bootstrapper;
using Zags.Web.Nancy.Pipeline;

namespace Zags.Web.Nancy.Unity.Bootstrapper
{
    public class NancyUnityBootstrapper : UnityNancyBootstrapper
    {
        private IUnityContainer _container;

        public NancyUnityBootstrapper(IUnityContainer container)
        {
            _container = container;
        }

        protected override void ApplicationStartup(IUnityContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            pipelines.EnabledUnhandledExceptionHandling();
        }

        protected override IUnityContainer GetApplicationContainer()
        {
            return _container;
        }

        protected override INancyEngine GetEngineInternal()
        {
            return this.ApplicationContainer.Resolve<INancyEngine>();
        }
    }
}
