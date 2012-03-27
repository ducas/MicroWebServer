using System;
using Microsoft.SPOT;
using MicroWebServer.Routing;

namespace MicroWebServer.Mvc
{
    public class MvcHttpServer : HttpServer
    {
        public RouteCollection Routes { get { return requestHandler.Routes; } }

        readonly MvcRequestHandler requestHandler = new MvcRequestHandler();

        public MvcHttpServer(string prefix, int port)
            : base(prefix, port, null)
        {
            base.AddHandler(requestHandler);
        }
    }
}
