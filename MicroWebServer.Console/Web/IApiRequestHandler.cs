using System;
using System.Net;
namespace MicroWebServer.Web
{
    public interface IRequestHandler
    {
        bool TryHandle(HttpListenerContext context);
    }
}
