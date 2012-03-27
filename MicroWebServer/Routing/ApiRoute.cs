using System;
using Microsoft.SPOT;
using MicroWebServer.Abstractions;
using System.Reflection;
using System.Collections;

namespace MicroWebServer.Routing
{
    public class ApiRoute : RouteBase
    {
        public string UrlPrefix { get; private set; }

        public ApiRoute(string urlBase, IRouteHandler handler, Assembly controllerAssembly)
        {
            if (urlBase.IndexOf("/") != 0) urlBase = "/" + urlBase;
            if (urlBase.LastIndexOf("/") != urlBase.Length - 1) urlBase += "/";

            UrlPrefix = urlBase;
            Handler = handler;
            Data = FindApiControllers(controllerAssembly);
        }

        public Hashtable FindApiControllers(Assembly assembly)
        {
            var result = new Hashtable();
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (!IsValidApiControllerType(type)) continue;

                result.Add(type.Name.Substring(0, type.Name.IndexOf("Controller")).ToLower(), type);
            }

            return result;
        }

        private bool IsValidApiControllerType(Type type)
        {
            var name = type.Name;

            var index = name.IndexOf("Controller");
            if (index == -1 || index != name.Length - 10) return false;

            var constructor = type.GetConstructor(new Type[] { });
            if (constructor == null) return false;

            var instance = constructor.Invoke(new object[] { });
            if (!(instance is IApiController)) return false;

            return true;
        }

        public override RouteData GetRouteData(IHttpContext context)
        {
            var url = context.Request.RawUrl.ToLower();

            var apiIndex = url.IndexOf(UrlPrefix);
            if (apiIndex != 0) return null;

            var controllers = Data as Hashtable;
            if (controllers == null) return null;

            var name = url.Substring(UrlPrefix.Length).ToLower();
            if (!controllers.Contains(name)) return null;

            var controllerType = controllers[name] as Type;
            if (controllerType == null) return null;

            var controllerConstructor = controllerType.GetConstructor(new Type[] { });
            var controller = controllerConstructor.Invoke(new object[] { }) as IApiController;

            if (controller == null) return null;

            return new RouteData { Handler = Handler, Url = url, Controller = controller };
        }
    }
}
