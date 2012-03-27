using System;
using Microsoft.SPOT;
using MicroWebServer.Results;
using MicroWebServer.Abstractions;

namespace MicroWebServer.Routing
{
    public interface IRouteHandler
    {
        IActionResult Handle(IHttpContext context);
    }
}
