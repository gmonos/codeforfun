using System.Net.Http;

namespace Zags.Web.HttpClient
{
    public interface IRequest
    {
        HttpRequestMessage Message { get; }
        Response GetResponse();

        bool ValidateMessage();
    }
}
