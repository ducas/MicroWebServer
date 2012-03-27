using System;
using Microsoft.SPOT;
using MicroWebServer.Abstractions;

namespace MicroWebServer.Tests.Mocks
{
    public class MockHttpRequest : IHttpRequest
    {
        public MockHttpRequest()
        {
            Headers = new System.Net.WebHeaderCollection();
        }

        public System.Uri Url { get; set; }

        public string RawUrl { get; set; }

        public System.Net.WebHeaderCollection Headers { get; private set; }

        public string HttpMethod { get; set; }
    }

    public class MockHttpContext : IHttpContext
    {
        public IHttpRequest Request { get; set; }

        public IHttpResponse Response { get; set; }
    }
}
