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

        public static void OK(HttpListenerResponse response, string mimeType, string lastModified, string body)
        {
            response.StatusCode = 200;
            response.StatusDescription = "OK";
            response.ContentType = mimeType;
            response.Headers.Add("Last-Modified", lastModified);

            using (var s = response.OutputStream)
            using (var w = new StreamWriter(s))
                w.Write(body);
        }

        public static void NotModified(HttpListenerResponse response)
        {
            response.StatusCode = 304;
            response.StatusDescription = "Not Modified";
        }

        public static void NotFound(HttpListenerResponse response)
        {
            response.StatusCode = 404;
            response.StatusDescription = "Not Found";
        }
    }
}
