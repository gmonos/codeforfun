using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Zags.Utilities.Alias;
using Zags.Web.Http;

namespace Zags.Web.HttpClient
{
    public class RequestBuilder : IRequestBuilder
    {
        private HttpRequestMessage _message;
        public HttpRequestMessage Message => _message;

        public IRequest Build()
        {
            return new Request(_message);
        }

        public IRequestBuilder CreateRequest(HttpMethod httpMethod, string url)
        {
            _message = new HttpRequestMessage(httpMethod, url);

            return this;
        }

        public IRequestBuilder UsingApiVersion(string version)
        {
            if (version == null||_message == null)
            {
                return this;
            }

            if (_message.Content == null)
            {
                _message.Content = new StringContent(string.Empty);
            }
            _message.Headers.Add(CustomHeaderNames.ApiVersion, new[] { version });

            return this;
        }

        public IRequestBuilder WithCorrelationToken(Guid correlationId)
        {
            if (correlationId == null || _message == null)
            {
                return this;
            }

            _message.Headers.Add("Correlation-Token", correlationId.ToString());

            return this;
        }

        public IRequestBuilder WithAccessToken(string accessToken)
        {
            if (accessToken == null || _message == null)
            {
                return this;
            }


            _message.Headers.Add("Authorization", "Bearer " + accessToken);

            return this;
        }

        public IRequestBuilder WithContent(object content)
        {
            if (content != null && _message!=null)
            {
                var json = JsonConvert.SerializeObject(content);
                _message.Content = new StringContent(json);
                _message.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            return this;
        }

        public IRequestBuilder WithIntent(string intent)
        {
            if (intent != null)
            {
                if (_message.Content == null)
                {
                    _message.Content = new StringContent(string.Empty);
                }

                _message.Headers.Add(CustomHeaderNames.Intent, new[] { intent });
            }

            return this;
        }

        public IRequestBuilder WithIntent(Intent intent)
        {
            return WithIntent(intent.GetAlias());
        }
    }
}
