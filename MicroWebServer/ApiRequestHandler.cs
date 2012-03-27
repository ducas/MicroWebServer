using System;
using Microsoft.SPOT;
using System.Collections;
using System.Net;
using System.IO;
using System.Reflection;
using MicroWebServer.Abstractions;
using MicroWebServer.Results;

namespace MicroWebServer
{
    public class ApiRequestHandler : IRequestHandler
    {
        Hashtable controllers = new Hashtable();
        public string Prefix { get; private set; }

        public ApiRequestHandler(string prefix)
        {
            if (prefix.IndexOf("/") != 0) prefix = "/" + prefix;
            if (prefix.LastIndexOf("/") != prefix.Length - 1) prefix += "/";
            Prefix = prefix;
        }

        public void RegisterAll(Assembly assembly)
        {
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (!IsValidApiControllerType(type)) continue;

                controllers.Add(type.Name.Substring(0, type.Name.IndexOf("Controller")).ToLower(), type);
            }
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

        public Type GetControllerType(string name)
        {
            var key = name.ToLower();
            if (!controllers.Contains(key)) return null;
            return controllers[key] as Type;
        }

        public void Register(string name, Type controllerType)
        {
            if (!IsValidApiControllerType(controllerType))
                throw new ApplicationException("Type " + controllerType.Name + " is not a valid controller type. Please ensure it implements IApiController type and has a parameterless constructor.");
            if (controllers.Contains(name))
                throw new ApplicationException("A controller has already been registered with the name " + name);
            controllers.Add(name.ToLower(), controllerType);
        }

        public IActionResult TryHandle(IHttpContext context)
        {
            var url = context.Request.RawUrl.ToLower();

            var apiIndex = url.IndexOf(Prefix);
            if (apiIndex != 0) return null;

            var name = url.Substring(5).ToLower();
            if (!controllers.Contains(name)) return null;

            var controllerType = controllers[name] as Type;
            if (controllerType == null) return null;

            var controllerConstructor = controllerType.GetConstructor(new Type[] { });
            var controller = controllerConstructor.Invoke(new object[] { }) as IApiController;

            if (controller == null) return null;

            var response = context.Response;
            var method = context.Request.HttpMethod.ToLower();
            switch (method)
            {
                case "get":
                    return new JsonResult { Data = controller.Get() };
            }

            return null;
        }
    }
}
