using System;
using Microsoft.SPOT;
using System.Collections;
using System.Net;
using System.IO;

namespace MicroWebServer.Web
{
    public class ApiRequestHandler : MicroWebServer.Web.IRequestHandler
    {
        Hashtable controllers = new Hashtable();

        public void Register(string name, Type controllerType)
        {
            controllers.Add(name.ToLower(), controllerType);
        }

        public bool TryHandle(HttpListenerContext context)
        {
            var url = context.Request.RawUrl.ToLower();

            var apiIndex = url.IndexOf("/api/");
            if (apiIndex != 0) return false;

            var name = url.Substring(5).ToLower();
            if (!controllers.Contains(name)) return false;

            var controllerType = controllers[name] as Type;
            if (controllerType == null) return false;

            var controllerConstructor = controllerType.GetConstructor(new Type[] { });
            var controller = controllerConstructor.Invoke(new object[] { }) as IApiController;

            if (controller == null) return false;

            var response = context.Response;
            var method = context.Request.HttpMethod.ToLower();
            switch (method)
            {
                case "get":
                    HttpResponse.Json(response, controller.Get());
                    return true;
            }

            return false;
        }
    }
}
