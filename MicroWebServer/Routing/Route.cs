using System;
using MicroWebServer.Abstractions;

namespace MicroWebServer.Routing
{
    public class Route : RouteBase
    {
        public string Url { get; private set; }

        public Route(string url, IRequestHandler handler) : this(url, handler, null) { }

        public Route(string url, IRequestHandler handler, object data)
        {
            Url = url;
            Handler = handler;
            Data = data;
        }

        public override bool CanHandle(IHttpContext context)
        {
            return Url.ToLower().Equals(context.Request.RawUrl.ToLower());
        }
    }
}
