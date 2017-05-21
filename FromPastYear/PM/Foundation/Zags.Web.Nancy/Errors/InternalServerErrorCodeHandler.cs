using Nancy;
using Nancy.ErrorHandling;

namespace Zags.Web.Nancy.Errors
{
    public class InternalServerErrorCodeHandler : IStatusCodeHandler
    {
        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {

        }

        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        {
            return statusCode == HttpStatusCode.InternalServerError;
        }
    }
}
