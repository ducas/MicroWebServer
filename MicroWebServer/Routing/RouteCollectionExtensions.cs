using System;
using Microsoft.SPOT;
using System.Reflection;

namespace MicroWebServer.Routing
{
    public static class RouteCollectionExtensions
    {
        static readonly IRouteHandler resourceHandler;
        public static void MapResourceRoute(this RouteCollection routes, string url, Resource resource)
        {
            routes.Add(new Route(url, resourceHandler, resource));
        }

        static readonly IRouteHandler apiHandler;
        public static void MapApiRoute(this RouteCollection routes, string urlPrefix, Assembly controllerAssembly)
        {
            routes.Add(new Route(urlPrefix, apiHandler, controllerAssembly));
        }
    }
}
