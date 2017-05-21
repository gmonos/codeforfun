using System.Net;
using System.Net.Http;

namespace Zags.Web.HttpClient
{
    public class Response
    {
        public HttpResponseMessage HttpResponseMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
