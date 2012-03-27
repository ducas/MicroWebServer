using System;
using Microsoft.SPOT;
using MicroWebServer.Routing;
using MicroWebServer.Results;
using MicroWebServer.Abstractions;

namespace MicroWebServer.Mvc
{
    public class MvcRequestHandler : IRequestHandler
    {
        public RouteCollection Routes { get; private set; }

        public MvcRequestHandler()
        {
            Routes = new RouteCollection();
        }

        public IActionResult TryHandle(IHttpContext context)
        {
            RouteBase route = null;
            RouteData routeData = null;

            foreach (var item in Routes)
            {
                route = item as RouteBase;
                routeData = route.GetRouteData(context);
                if (routeData != null) break;
            }

            if (routeData == null) return null;
            
            return route.Handler.Handle(context, routeData);
        }
    }
}
