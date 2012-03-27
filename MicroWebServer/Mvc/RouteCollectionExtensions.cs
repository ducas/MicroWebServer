using System;
using Microsoft.SPOT;
using System.Reflection;
using MicroWebServer.Routing.Handlers;
using MicroWebServer.Routing;

namespace MicroWebServer.Mvc
{
    public static class RouteCollectionExtensions
    {
        static readonly IRouteHandler resourceHandler = new ResourceRouteHandler();
        public static void MapResourceRoute(this RouteCollection routes, string url, Resource resource)
        {
            routes.Add(new Route(url, resourceHandler, resource));
        }

        static readonly IRouteHandler apiHandler = new ApiRouteHandler();
        public static void MapApiRoute(this RouteCollection routes, string urlBase, Assembly controllerAssembly)
        {
            routes.Add(new ApiRoute(urlBase, apiHandler, controllerAssembly));
        }
    }
}
