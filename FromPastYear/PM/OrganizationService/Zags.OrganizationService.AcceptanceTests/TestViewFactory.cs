using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Responses;
using Nancy.ViewEngines;

namespace Zags.OrganizationService.AcceptanceTests
{
    public class TestViewFactory : IViewFactory
    {
        #region IViewFactory Members

        public Nancy.Response RenderView(string viewName, dynamic model, ViewLocationContext viewLocationContext)
        {
            viewLocationContext.Context.Items["Model"] = model;
            return new HtmlResponse();
        }

        #endregion
    }
}
