using System;
using Microsoft.SPOT;

namespace MicroWebServer.Routing
{
    public class RouteData
    {
        public IRouteHandler Handler { get; set; }
        public string Url { get; set; }
        public object Controller { get; set; }
        public object Data { get; set; }
    }
}
