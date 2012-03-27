using System;
using MicroWebServer.Abstractions;

namespace MicroWebServer.Routing
{
    public class Route : RouteBase
    {
        public string Url { get; private set; }

        public Route(string url, IRouteHandler handler) : this(url, handler, null) { }

        public Route(string url, IRouteHandler handler, object data)
        {
            Url = url;
            Handler = handler;
            Data = data;
        }

        public override RouteData GetRouteData(IHttpContext context)
        {
            var url = Url.ToLower();

            if (!url.Equals(context.Request.RawUrl.ToLower())) return null;

            return new RouteData { Handler = Handler, Url = url, Data = Data };
        }
    }
}
