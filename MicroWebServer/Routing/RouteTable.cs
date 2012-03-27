using System;
using Microsoft.SPOT;
using System.Collections;
using MicroWebServer.Abstractions;

namespace MicroWebServer.Routing
{
    public static class RouteTable
    {
        readonly static RouteCollection routes = new RouteCollection();

        public static RouteCollection Routes
        {
            get { return routes; }
        }
    }

    public class RouteCollection
    {
        ArrayList routes = new ArrayList();

        public RouteCollection() { }

        public void Add(RouteBase route)
        {
            routes.Add(route);
        }

        public RouteBase ResolveFor(IHttpContext context)
        {
            foreach (var item in routes)
            {
                var route = item as RouteBase;
                if (route != null && route.CanHandle(context)) return route;
            }
            return null;
        }
    }
}
