using System;
using Microsoft.SPOT;
using MicroWebServer.Routing;
using MicroWebServer.Results;
using MicroWebServer.Abstractions;

namespace MicroWebServer.Tests.Mocks
{
    public class MockRoute : RouteBase
    {
        public MockRoute(IRouteHandler handler)
        {
            Handler = handler;
        }

        public RouteData GetRouteDataResult { get; set; }

        public override RouteData GetRouteData(IHttpContext context)
        {
            return GetRouteDataResult;
        }
    }

    public class MockRouteHandler : IRouteHandler
    {
        public IActionResult HandleResult { get; set; }

        public IActionResult Handle(IHttpContext context, RouteData routeData)
        {
            return HandleResult;
        }
    }
}
