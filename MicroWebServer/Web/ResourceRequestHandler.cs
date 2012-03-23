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

            var result = map.Contains(url) ? map[url] as Resource : null;
            if (result == null)
            {
                return false;
            }

            using (var response = context.Response)
            {
                var lastModified = request.Headers["If-Modified-Since"];
                var resultLastModified = result.LastModified.ToString("ddd, dd MMM yyyy hh:mm:ss ") + "GMT";
                if (!!StringHelpers.IsNullOrEmpty(lastModified) && lastModified == resultLastModified)
                {
                    HttpResponse.NotModified(response);
                    return true;
                }

                HttpResponse.OK(response, result.MimeType, resultLastModified, Resources.GetString((Resources.StringResources)result.StringResource));
                return true;
            }
        }
    }
}
