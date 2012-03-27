using System;
using System.Net;
using MicroWebServer.Abstractions;
using MicroWebServer.Results;

namespace MicroWebServer
{
    public interface IRequestHandler
    {
        IActionResult TryHandle(IHttpContext context);
    }
}
