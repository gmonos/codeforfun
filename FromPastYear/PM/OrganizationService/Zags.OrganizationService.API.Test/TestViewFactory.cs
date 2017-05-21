using Nancy.Responses;
using Nancy.ViewEngines;

namespace Zags.OrganizationService.API.Test
{
    public class TestViewFactory : IViewFactory
    {
        #region IViewFactory Members

        public Nancy.Response RenderView(string viewName, dynamic model, ViewLocationContext viewLocationContext)
        {
            viewLocationContext.Context.Items["model"] = model;
            return new HtmlResponse();
        }

        #endregion
    }
}