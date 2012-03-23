using System;
using Microsoft.SPOT;
using System.Net;

namespace MicroWebServer.Abstractions
{
    public interface IHttpContext
    {
        IHttpRequest Request { get; }
        IHttpResponse Response { get; }
    }

    public class HttpContextAdapter : IHttpContext
    {
        HttpListenerContext context;

        IHttpRequest requestAdapter = null;
        IHttpResponse responseAdapter = null;

        public HttpContextAdapter(HttpListenerContext context)
        {
            this.context = context;
        }

        public IHttpRequest Request { get { return requestAdapter ?? (requestAdapter = new HttpRequestAdapter(context.Request)); } }
        public IHttpResponse Response { get { return responseAdapter ?? (responseAdapter = new HttpResponseAdapter(context.Response)); } }
    }
}
