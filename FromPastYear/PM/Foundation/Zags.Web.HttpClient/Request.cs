using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Zags.Web.HttpClient
{
    public class Request : IRequest
    {
        public HttpRequestMessage Message { get; }

        public Request(HttpRequestMessage message)
        {
            Message = message;
        }

        public bool ValidateMessage()
        {
            bool result = true;
                
            if(Message == null)
                result = false;
            if (Message?.Method == null)
                result = false;
            if (Message?.RequestUri == null)
                result = false;
            return result;
        }

        public Response GetResponse()
        {
            if (ValidateMessage())
            {
                return ExecuteRequest();
            }
            return new Response()
            {
                HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest),
                StatusCode = HttpStatusCode.BadRequest
            };

        }

        protected virtual Response ExecuteRequest()
        {
            HttpResponseMessage response;
            using (var client = new System.Net.Http.HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = client.SendAsync(Message).Result;
            }
            return new Response
            {
                HttpResponseMessage = response,
                StatusCode = response.StatusCode
            };
        }
    }
}
