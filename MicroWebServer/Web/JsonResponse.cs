using System;
using Microsoft.SPOT;
using System.Net;
using System.IO;
using System.Collections;

namespace MicroWebServer.Web
{
    public static class HttpResponse
    {
        static readonly JsonSerializer jsonSerializer = new JsonSerializer();
        public static void Json(HttpListenerResponse response, object data)
        {
            response.StatusCode = 200;
            response.StatusDescription = "OK";
            response.ContentType = "application/json";

            using (var stream = response.OutputStream)
            using (var writer = new StreamWriter(stream))
            {
                jsonSerializer.Serialize(data, writer);
            }
        }
    }
}
