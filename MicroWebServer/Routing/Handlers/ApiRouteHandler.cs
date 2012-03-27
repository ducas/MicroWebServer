using System;
using Microsoft.SPOT;
using MicroWebServer.Results;
using MicroWebServer.Abstractions;

namespace MicroWebServer.Routing.Handlers
{
    public class ApiRouteHandler : IRouteHandler
    {
        public IActionResult Handle(IHttpContext context, RouteData routeData)
        {
            var controller = routeData.Controller as IApiController;
            if (controller == null) return null;

            var response = context.Response;
            var method = context.Request.HttpMethod.ToLower();
            switch (method)
            {
                case "get":
                    return new JsonResult { Data = controller.Get() };
            }

            return null;
        }
    }
}
