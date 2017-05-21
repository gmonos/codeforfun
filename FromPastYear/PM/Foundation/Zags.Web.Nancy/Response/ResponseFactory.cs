using Nancy;


namespace Zags.Web.Nancy.Response
{
    public static class ResponseExt
    {
        public static dynamic CreatedResponse(this NancyModule module, string resourcePath, int id)
        {
            return module.Negotiate.WithStatusCode(HttpStatusCode.Created).WithHeader("Location", module.Request.Url.SiteBase + resourcePath + "/" + id);
        }
    }
}
