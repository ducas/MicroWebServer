using System;
using Microsoft.SPOT;
using MicroWebServer.Results;
using MicroWebServer.Abstractions;
using System.Resources;

namespace MicroWebServer.Routing.Handlers
{
    public class ResourceRouteHandler : IRouteHandler
    {
        public IActionResult Handle(IHttpContext context, RouteData routeData)
        {
            var resource = routeData.Data as Resource;
            if (resource == null) return null;

            var resourceManager = resource.ResourceManager;
            if (resourceManager == null) return null;

            var body = Microsoft.SPOT.ResourceUtility.GetObject(resourceManager, resource.StringResource) as string;
            if (body == null) return null;

            return new ContentResult { Content = body, ContentType = resource.ContentType, LastModified = resource.LastModified };
        }
    }
}
