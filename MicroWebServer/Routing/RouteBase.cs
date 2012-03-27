using System;
using MicroWebServer.Abstractions;

namespace MicroWebServer.Routing
{
    public abstract class RouteBase
    {
        public IRequestHandler Handler { get; protected set; }
        public object Data { get; protected set; }

        public abstract bool CanHandle(IHttpContext context);
    }
}
