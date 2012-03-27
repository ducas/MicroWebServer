using System;
using Microsoft.SPOT;
using System.IO;
using System.Collections;
using MicroWebServer.Abstractions;

namespace MicroWebServer
{
    public static class HttpResponse
    {
        static readonly JsonSerializer jsonSerializer = new JsonSerializer();
        public static void Json(IHttpResponse response, object data)
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

        public static void OK(IHttpResponse response)
        {
            OK(response, null, null, null);
        }

        public static void OK(IHttpResponse response, string mimeType, string body)
        {
            OK(response, mimeType, null, body);
        }

        public static void OK(IHttpResponse response, string mimeType, string lastModified, string body)
        {
            response.StatusCode = 200;
            response.StatusDescription = "OK";
            
            if (!StringHelpers.IsNullOrEmpty(mimeType)) response.ContentType = mimeType;
            if (!StringHelpers.IsNullOrEmpty(lastModified)) response.Headers.Add("Last-Modified", lastModified);

            if (StringHelpers.IsNullOrEmpty(body)) return;

            using (var s = response.OutputStream)
            using (var w = new StreamWriter(s))
                w.Write(body);
        }

        public static void NotModified(IHttpResponse response)
        {
            response.StatusCode = 304;
            response.StatusDescription = "Not Modified";
        }

        public static void NotFound(IHttpResponse response, string body)
        {
            response.StatusCode = 404;
            response.StatusDescription = "Not Found";
            response.ContentType = "text/html";

            using (var s = response.OutputStream)
            using (var w = new StreamWriter(s))
                w.Write(body);
        }
    }
}
