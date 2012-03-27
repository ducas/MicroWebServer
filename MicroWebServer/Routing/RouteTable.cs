using System;
using Microsoft.SPOT;
using System.Collections;
using MicroWebServer.Abstractions;

namespace MicroWebServer.Routing
{
    public class RouteCollection
    {
        ArrayList routes = new ArrayList();

        public RouteCollection() { }

        public void Add(RouteBase route)
        {
            routes.Add(route);
        }

        public RouteData ResolveFor(IHttpContext context)
        {
            foreach (var item in routes)
            {
                var route = item as RouteBase;
                if (route == null) continue;

                var routeData = route.GetRouteData(context);
                if (routeData != null) return routeData;
            }
            return null;
        }
    }
}
