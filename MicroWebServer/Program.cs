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
        static ResourceMap map = new ResourceMap();
        static IRequestHandler[] handlers;

        public static void Main()
        {
            var api = new ApiRequestHandler();
            api.Register("Tasks", typeof(TasksController));

            var resource = new ResourceRequestHandler();
            resource.Register("/test.html", new Resource { StringResource = (short)Resources.StringResources.TestHtml, MimeType = "text/html" });
            resource.Register("/scripts/jquery.js", new Resource { StringResource = (short)Resources.StringResources.ScriptsJqueryJs, MimeType = "text/javascript" });
            resource.Register("/scripts/knockout.js", new Resource { StringResource = (short)Resources.StringResources.ScriptsKnockoutJs, MimeType = "text/javascript" });

            handlers = new IRequestHandler[] { api, resource };

            var server = new HttpServer("http", 9997, ProcessRequest);

            Thread.Sleep(Timeout.Infinite);
        }

        public static void ProcessRequest(HttpListenerContext context)
        {
            foreach (var handler in handlers)
            {
                if (handler.TryHandle(context)) return;
            }
                
            context.Response.StatusCode = 404;
            context.Response.StatusDescription = "Not Found";
        }

        internal class ResourceMap
        {
            System.Collections.Hashtable table = new System.Collections.Hashtable();

            public ResourceMap()
            {
            }

            public Resource Get(string url)
            {
                if (!table.Contains(url)) return null;
                return (Resource)table[url];
            }
        }

    }
}
