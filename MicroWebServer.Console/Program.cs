using System.Reflection;
using System.Threading;
using MicroWebServer.Mvc;

namespace MicroWebServer.Console
{
    public class Program
    {
        public static void Main()
        {
            var server = new MvcHttpServer("http", 9997);
            var routes = server.Routes;

            routes.MapApiRoute("api", Assembly.GetAssembly(typeof(Program)));
            routes.MapResourceRoute("/test.html", new Resource(Resources.ResourceManager, Resources.StringResources.TestHtml) { ContentType = "text/html" });
            routes.MapResourceRoute("/scripts/jquery.js", new Resource(Resources.ResourceManager, Resources.StringResources.ScriptsJqueryJs) { ContentType = "text/javascript" });
            routes.MapResourceRoute("/scripts/knockout.js", new Resource(Resources.ResourceManager, Resources.StringResources.ScriptsKnockoutJs) { ContentType = "text/javascript" });

            Thread.Sleep(Timeout.Infinite);
        }
    }
}
