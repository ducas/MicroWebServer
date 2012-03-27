using System;
using Microsoft.SPOT;
using System.Threading;
using Microsoft.SPOT.Net.NetworkInformation;
using System.Net;
using System.IO;
using MicroWebServer.Api;
using System.Reflection;

namespace MicroWebServer.Console
{
    public class Program
    {
        static IRequestHandler[] handlers;

        public static void Main()
        {
            var api = new ApiRequestHandler("api");
            api.RegisterAll(Assembly.GetAssembly(typeof(Program)));

            var resource = new ResourceRequestHandler(Resources.ResourceManager);
            resource.Register("/test.html", new Resource { StringResource = Resources.StringResources.TestHtml, ContentType = "text/html" });
            resource.Register("/scripts/jquery.js", new Resource { StringResource = Resources.StringResources.ScriptsJqueryJs, ContentType = "text/javascript" });
            resource.Register("/scripts/knockout.js", new Resource { StringResource = Resources.StringResources.ScriptsKnockoutJs, ContentType = "text/javascript" });

            handlers = new IRequestHandler[] { api, resource };

            var server = new HttpServer("http", 9997, handlers);

            Thread.Sleep(Timeout.Infinite);
        }
    }
}
