using System;
using MicroWebServer.Abstractions;

namespace MicroWebServer.Routing
{
    public abstract class RouteBase
    {
        public IRouteHandler Handler { get; protected set; }
        public object Data { get; protected set; }

        public abstract RouteData GetRouteData(IHttpContext context);
    }
}
