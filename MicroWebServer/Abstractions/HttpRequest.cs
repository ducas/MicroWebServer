using System;
using System.Net;

namespace MicroWebServer.Abstractions
{
    public interface IHttpRequest
    {
        Uri Url { get; }
        string RawUrl { get; }
        WebHeaderCollection Headers { get; }
        string HttpMethod { get; }
    }

    public class HttpRequestAdapter : IHttpRequest
    {
        private HttpListenerRequest request;

        public HttpRequestAdapter(HttpListenerRequest request)
        {
            this.request = request;
        }

        public Uri Url
        {
            get { return request.Url; }
        }

        public string RawUrl
        {
            get { return request.RawUrl; }
        }

        public WebHeaderCollection Headers
        {
            get { return request.Headers; }
        }

        public string HttpMethod
        {
            get { return request.HttpMethod; }
        }
    }
}
