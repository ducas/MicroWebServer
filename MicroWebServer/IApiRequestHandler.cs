using System;
using System.Net;

namespace MicroWebServer
{
    public interface IRequestHandler
    {
        bool TryHandle(HttpListenerContext context);
    }
}
