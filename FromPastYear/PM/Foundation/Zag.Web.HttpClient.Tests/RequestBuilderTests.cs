using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using Xunit;
using Zags.Web.Http;

namespace Zags.Web.HttpClient.Test
{
    public class RequestBuilderTests
    {
        [Fact]
        public void BuildedQueryIsassignableToInterface()
        {
            var requestBuilder = new RequestBuilder();
            var buildedQuery = requestBuilder.Build();

            buildedQuery.Should().BeAssignableTo<IRequest>();
        }

        [Fact]
        public void EmptyBuildedQueryIsNotNull()
        {
            var requestBuilder = new RequestBuilder();
            var buildedQuery = requestBuilder.Build();

            buildedQuery.Should().NotBe(null);
        }

        [Fact]
        public void GetRequestToGoogleShouldBeValid()
        {
            var requestBuilder = new RequestBuilder();
            requestBuilder.CreateRequest( new HttpMethod("GET"), "http://google.com");
            var buildedQuery = requestBuilder.Build();
            var messageValidation = buildedQuery.ValidateMessage();
            var requestMessage = requestBuilder.Message;

            messageValidation.Should().BeTrue();
            requestMessage.Method.Should().Be(new HttpMethod("GET"));
            requestMessage.RequestUri.Should().Be(new Uri("http://google.com"));
        }

        [Fact]
        public void UsingApiVersionShouldReturnAIRequestBuilder()
        {
            var requestBuilder = new RequestBuilder();
            var requestBuilderWithApiVersion =  requestBuilder.UsingApiVersion("v1");

            requestBuilderWithApiVersion.Should().BeAssignableTo<IRequestBuilder>();
            requestBuilderWithApiVersion.Should().BeAssignableTo<RequestBuilder>();
        }

        [Fact]
        public void SettingApiVersionShouldSetVersionInHeaders()
        {
            var requestBuilder = new RequestBuilder();
            requestBuilder.CreateRequest(new HttpMethod("GET"), "http://google.com");
            var requestBuilderWithApiVersion = requestBuilder.UsingApiVersion("v1");
            var requestMessage = (requestBuilderWithApiVersion as RequestBuilder).Message;
            var version = requestMessage.Headers.GetValues(CustomHeaderNames.ApiVersion);

            requestBuilderWithApiVersion.Should().BeAssignableTo<IRequestBuilder>();
            requestBuilderWithApiVersion.Should().BeAssignableTo<RequestBuilder>();
            version.Should().Contain("v1");
        }


        [Fact]
        public void SettingNullApiVersionShouldReturnTheCurrentBuilder()
        {
            IRequestBuilder requestBuilder = new RequestBuilder();
            var requestBuilderWithApiVersion = requestBuilder.UsingApiVersion(null);

            requestBuilderWithApiVersion.Should().Be(requestBuilder);
        }


        [Fact]
        public void SettingNullContentShouldReturnTheCurrentBuilder()
        {
            IRequestBuilder requestBuilder = new RequestBuilder();
            var requestBuilderWithContentVersion = requestBuilder.WithContent(null);

            requestBuilderWithContentVersion.Should().Be(requestBuilder);
        }

        [Fact]
        public void SettingContentShouldReturnTheCurrentBuilder()
        {
            object contentObject = new object[]
            {
                "test","test2","test3"
            };
            var serializeObject = JsonConvert.SerializeObject(contentObject);

            IRequestBuilder requestBuilder = new RequestBuilder();
            requestBuilder.CreateRequest(new HttpMethod("GET"), "http://google.com");
            var requestBuilderWithContentVersion = requestBuilder.WithContent(contentObject);
            var requestMessage = (requestBuilderWithContentVersion as RequestBuilder).Message;
            var requestContent = requestMessage.Content.ReadAsStringAsync().Result;


            requestContent.Should().Be(serializeObject);
        }
    }
}
