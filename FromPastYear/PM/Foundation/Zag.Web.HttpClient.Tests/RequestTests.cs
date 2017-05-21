using System;
using System.Net;
using System.Net.Http;
using Xunit;
using Zags.Web.HttpClient;

namespace Zags.Web.HttpClient.Test
{
    public class RequestTest
    {

        [Fact]
        public void ValidateMessage_WhenRequestURIIsNull_ThenRequestIsNotValid()
        {
            var request = new Request(null);
            var result = request.ValidateMessage();
            Assert.False(result);
        }

        [Fact]
        public void ValidateMessage_WhenCreatingAnEmptyRequest_ThenRequestIsNotValid()
        {
            var request = new Request(new HttpRequestMessage());
            var result = request.ValidateMessage();
            Assert.False(result);
        }

        [Fact]
        public void ValidateMessage_WhenCreatingAPostRequestOnGoogle_ThenRequestIsValid()
        {
            var request = new Request(new HttpRequestMessage(new HttpMethod("POST"),new Uri("http://google.com") ));
            var result = request.ValidateMessage();
            Assert.True(result);
        }

        [Fact]
        public void GetResponse_WhenCreatingGetRequestOnGoogle_ThenHttpStatusCodeIsOk()
        {
            var request = new Request(new HttpRequestMessage(new HttpMethod("GET"), new Uri("http://google.com")));
            var response = request.GetResponse();
            Assert.Equal(response.StatusCode,HttpStatusCode.OK );
        }

        [Fact]
        public void GetResponse_WhenRequestMessageIsEmpty_ThenHttpStatusCodeIsBadRequest()
        {
            var Req = new Request(new HttpRequestMessage());
            var response = Req.GetResponse();
            Assert.Equal(response.StatusCode, HttpStatusCode.BadRequest);
        }

    }
}
