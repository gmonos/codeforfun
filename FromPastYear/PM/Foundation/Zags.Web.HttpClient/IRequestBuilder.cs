using System;
using System.Net.Http;

namespace Zags.Web.HttpClient
{
    public interface IRequestBuilder
    {
        IRequestBuilder CreateRequest(HttpMethod httpMethod, string url);
        IRequestBuilder WithIntent(string intent);
        IRequestBuilder WithCorrelationToken(Guid correlationId);
        IRequestBuilder WithAccessToken(string accessToken);
        IRequestBuilder UsingApiVersion(string version);
        IRequestBuilder WithContent(object content);

        IRequest Build();
    }
}
