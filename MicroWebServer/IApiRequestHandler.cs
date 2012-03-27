using System;
using System.Net;
using MicroWebServer.Abstractions;

namespace MicroWebServer
{
    public interface IRequestHandler
    {
        bool TryHandle(IHttpContext context);
    }
}
