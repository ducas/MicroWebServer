using System;
using Microsoft.SPOT;
using System.Collections;
using System.IO;

namespace MicroWebServer.Web
{
    public class ResourceRequestHandler : IRequestHandler
    {
        Hashtable map = new Hashtable();

        public void Register(string url, Resource resource)
        {
            map.Add(url.ToLower(), resource);
        }

        public bool TryHandle(System.Net.HttpListenerContext context)
        {
            var request = context.Request;
            var url = request.RawUrl.ToLower();

            using (var response = context.Response)
            {
                var result = map.Contains(url) ? map[url] as Resource : null;
                if (result == null)
                {
                    return false;
                }

                var lastModified = request.Headers["If-Modified-Since"];
                var resultLastModified = result.LastModified.ToString("ddd, dd MMM yyyy hh:mm:ss ") + "GMT";
                if (lastModified != null && lastModified != "" && lastModified == resultLastModified)
                {
                    response.StatusCode = 304;
                    response.StatusDescription = "Not Modified";
                    return true;
                }

                response.StatusCode = 200;
                response.StatusDescription = "OK";
                response.ContentType = result.MimeType;
                response.Headers.Add("Last-Modified", resultLastModified);

                using (var s = response.OutputStream)
                using (var w = new StreamWriter(s))
                    w.Write(Resources.GetString((Resources.StringResources)result.StringResource));

                return true;
            }
        }
    }
}
