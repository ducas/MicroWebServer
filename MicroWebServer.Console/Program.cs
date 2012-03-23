using System;
using Microsoft.SPOT;
using System.Threading;
using Microsoft.SPOT.Net.NetworkInformation;
using System.Net;
using System.IO;
using MicroWebServer.Web;
using MicroWebServer.Api;

namespace MicroWebServer
{
    public class Program
    {
        static IRequestHandler[] handlers;

        public static void Main()
        {
            var api = new ApiRequestHandler();
            api.Register("Tasks", typeof(TasksController));

            var resource = new ResourceRequestHandler(Resources.ResourceManager);
            resource.Register("/test.html", new Resource { StringResource = Resources.StringResources.TestHtml, MimeType = "text/html" });
            resource.Register("/scripts/jquery.js", new Resource { StringResource = Resources.StringResources.ScriptsJqueryJs, MimeType = "text/javascript" });
            resource.Register("/scripts/knockout.js", new Resource { StringResource = Resources.StringResources.ScriptsKnockoutJs, MimeType = "text/javascript" });

            handlers = new IRequestHandler[] { api, resource };

            var server = new HttpServer("http", 9997, handlers);

            Thread.Sleep(Timeout.Infinite);
        }
    }
}
